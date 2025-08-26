using System.Runtime.Serialization;

namespace GymManager_BLL.Exceptions;

[Serializable]
public class UserNotFoundException : Exception
{
    public UserNotFoundException() : base ("Usuario no encontrado")
    {
    }

    public UserNotFoundException(string? message) : base(message)
    {
    }

    public UserNotFoundException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }

    protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info,
        context)
    {
    }
}