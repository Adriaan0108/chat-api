namespace chat_api.Exceptions;

public enum ErrorType
{
    BadRequest,
    NotFound,
    InternalServerError,
    Forbidden,
    Unauthorized,
    ServiceUnavailable,
    UnprocessableEntity,
    Conflict
}