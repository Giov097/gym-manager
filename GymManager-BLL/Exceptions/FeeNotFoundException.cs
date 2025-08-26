using System.Runtime.Serialization;

namespace GymManager_BLL.Exceptions;

[Serializable]
public class FeeNotFoundException : Exception
{
    public FeeNotFoundException() : base("Cuota no encontrada")
    {
    }

    public FeeNotFoundException(string? message) : base(message)
    {
    }

    public FeeNotFoundException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }

    protected FeeNotFoundException(SerializationInfo info, StreamingContext context) : base(
        info,
        context)
    {
    }
}