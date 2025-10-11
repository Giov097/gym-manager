namespace GymManager_DAL;

public class DatabaseException(string message, Exception innerException)
    : Exception(message, innerException);