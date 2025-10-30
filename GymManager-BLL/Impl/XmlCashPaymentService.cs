using GymManager_BE;
using GymManager_BLL.Exceptions;
using GymManager_MPP;

namespace GymManager_BLL.Impl;

public class XmlCashPaymentService : XmlPaymentService
{
    private readonly XmlPaymentMapper _mapper = new("../../../data.xml");


    public override Task<Payment> AddPayment(Payment payment, long feeId)
    {
        if (payment is not CashPayment)
            throw new InvalidPaymentException("El pago debe ser de tipo efectivo.");
        payment.PaymentDate = DateOnly.FromDateTime(DateTime.Now);
        if (!payment.Validate(out var reason))
        {
            throw new InvalidPaymentException(reason);
        }

        return _mapper.Create(payment, feeId);
    }

    public override Task<Payment> UpdatePayment(long paymentId, Payment payment)
    {
        return _mapper.GetById(paymentId).ContinueWith(task =>
        {
            var taskResult = task.Result;
            if (taskResult == null)
            {
                throw new PaymentNotFoundException();
            }

            taskResult.Amount = payment.Amount;
            taskResult.Status = payment.Status;
            taskResult.PaymentDate = payment.PaymentDate;
            switch (taskResult)
            {
                case CashPayment cash when payment is CashPayment updatedCash:
                    cash.ReceiptNumber = updatedCash.ReceiptNumber;
                    taskResult = cash;
                    break;
                default:
                    throw new ProcessingException("No se puede cambiar el tipo de pago");
            }

            if (!payment.Validate(out var reason))
            {
                throw new InvalidPaymentException(reason);
            }

            return _mapper.Update(taskResult)
                .ContinueWith(success =>
                    success.Result
                        ? taskResult
                        : throw new ProcessingException("No se pudo actualizar el pago")).Result;
        });
    }
}