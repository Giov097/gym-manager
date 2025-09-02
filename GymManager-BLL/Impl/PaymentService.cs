using GymManager_BE;
using GymManager_BLL.Exceptions;
using GymManager_MPP;

namespace GymManager_BLL.Impl;

public class PaymentService : IPaymentService
{
    private readonly PaymentMapper _mapper = new();

    public Task<List<Payment>> GetPayments()
    {
        return _mapper.GetAll();
    }

    public Task<List<Payment>> SearchPayments(DateOnly from, DateOnly to, long userId)
    {
        return _mapper.Search(from, to, userId);
    }

    public Task<Payment> GetPaymentById(long paymentId)
    {
        return _mapper.GetById(paymentId)
            .ContinueWith(task => task.Result ?? throw new PaymentNotFoundException());
    }

    public Task<Payment> AddPayment(Payment payment)
    {
        payment.PaymentDate = DateOnly.FromDateTime(DateTime.Now);
        return _mapper.Create(payment);
    }

    public Task<Payment> UpdatePayment(long paymentId, Payment payment)
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
                case CardPayment card when payment is CardPayment updatedCard:
                    card.Brand = updatedCard.Brand;
                    card.LastFourDigits = updatedCard.LastFourDigits;
                    taskResult = card;
                    break;
                default:
                    throw new ProcessingException("No se puede cambiar el tipo de pago");
            }

            return _mapper.Update(taskResult)
                .ContinueWith(success =>
                    success.Result
                        ? taskResult
                        : throw new ProcessingException("No se pudo actualizar el pago")).Result;
        });
    }

    public Task<bool> DeletePayment(long paymentId)
    {
        return _mapper.GetById(paymentId).ContinueWith(payment =>
            payment.Result == null
                ? throw new PaymentNotFoundException()
                : _mapper.Delete(paymentId).Result);
    }
}