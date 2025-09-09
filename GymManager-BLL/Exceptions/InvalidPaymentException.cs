namespace GymManager_BLL.Exceptions;

public class InvalidPaymentException : Exception
{
    public InvalidPaymentException()
    {
    }

    public InvalidPaymentException(string? message) : base(message)
    {
    }
}