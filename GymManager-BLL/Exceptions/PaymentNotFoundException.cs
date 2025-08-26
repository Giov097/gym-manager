using System.Runtime.Serialization;

namespace GymManager_BLL.Exceptions;

[Serializable]
public class PaymentNotFoundException : Exception
{
    public PaymentNotFoundException() : base("Pago no encontrado")
    {
    }

    public PaymentNotFoundException(string? message) : base(message)
    {
    }

    public PaymentNotFoundException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }

    protected PaymentNotFoundException(SerializationInfo info, StreamingContext context) : base(
        info,
        context)
    {
    }
}