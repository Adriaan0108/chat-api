namespace chat_api.Exceptions;

public class CustomException : Exception
{
    public CustomException(ErrorType errorType, string message)
        : base(message)
    {
        ErrorType = errorType;
    }

    public CustomException(ErrorType errorType, string message, Exception inner)
        : base(message, inner)
    {
        ErrorType = errorType;
    }

    public ErrorType ErrorType { get; }
}