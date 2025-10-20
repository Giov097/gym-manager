namespace GymManager_DDAL;

public class DatabaseException(string message, Exception innerException)
    : Exception(message, innerException);