using Microsoft.AspNetCore.Http;

namespace Polls.Domain.Errors;

public class BadRequestException(string? message = null)
    : BaseException(message ?? "The request is invalid.");

public class NotFoundException(string? message = null)
    : BaseException(message ?? "The requested resource was not found.", StatusCodes.Status404NotFound);

public class ConflictException(string? message = null)
    : BaseException(message ?? "A conflict occurred with the current state of the resource.", StatusCodes.Status409Conflict);

public class InternalServerErrorException(string? message = null)
    : BaseException(message ?? "An unexpected error occurred on the server.", StatusCodes.Status500InternalServerError);
