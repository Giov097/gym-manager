using GymManager_BE;
using GymManager_BLL.Exceptions;
using GymManager_MPP;

namespace GymManager_BLL;

public class PaymentService : IPaymentService
{
    private readonly PaymentMapper _mapper = new();

    //TODO: implementar filtros
    public Task<List<Payment>> GetPayments(DateOnly from, DateOnly to, long userId)
    {
        return _mapper.GetAll();
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
            if (task.Result == null)
            {
                throw new PaymentNotFoundException();
            }

            task.Result.Amount = payment.Amount;
            task.Result.Status = payment.Status;
            return _mapper.Update(task.Result)
                .ContinueWith(success =>
                    success.Result
                        ? task.Result
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