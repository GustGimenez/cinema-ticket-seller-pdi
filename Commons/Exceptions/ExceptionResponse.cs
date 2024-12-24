using System.Net;

namespace Commons.Exceptions;

public record ExceptionResponse(string Message, HttpStatusCode StatusCode);