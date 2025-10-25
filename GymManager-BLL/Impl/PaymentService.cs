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

    public virtual Task<Payment> AddPayment(Payment payment, long feeId)
    {
        throw new NotImplementedException();
    }

    public virtual Task<Payment> UpdatePayment(long paymentId, Payment payment)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeletePayment(long paymentId)
    {
        return _mapper.GetById(paymentId).ContinueWith(payment =>
            payment.Result == null
                ? throw new PaymentNotFoundException()
                : _mapper.Delete(paymentId).Result);
    }
}