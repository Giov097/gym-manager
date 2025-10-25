using GymManager_BE;
using GymManager_BLL.Exceptions;
using GymManager_MPP;

namespace GymManager_BLL.Impl;

public class CardPaymentService : PaymentService
{
    private readonly PaymentMapper _mapper = new();

    public override Task<Payment> AddPayment(Payment payment, long feeId)
    {
        if (payment is not CardPayment)
            throw new InvalidPaymentException("El pago debe ser de tipo tarjeta.");
        payment.PaymentDate = DateOnly.FromDateTime(DateTime.Now);
        return !payment.Validate(out var reason)
            ? throw new InvalidPaymentException(reason)
            : _mapper.Create(payment, feeId);
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
                case CardPayment card when payment is CardPayment updatedCard:
                    card.Brand = updatedCard.Brand;
                    card.LastFourDigits = updatedCard.LastFourDigits;
                    taskResult = card;
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