using Microsoft.AspNetCore.Http;

namespace Polls.Domain.Errors;

public class BaseException : Exception
{
    public int StatusCode { get; }

    public BaseException(string message, int statusCode = StatusCodes.Status400BadRequest)
        : base(message)
    {
        StatusCode = statusCode;
    }

    public BaseException(string message, Exception innerException, int statusCode = StatusCodes.Status400BadRequest)
        : base(message, innerException)
    {
        StatusCode = statusCode;
    }
}