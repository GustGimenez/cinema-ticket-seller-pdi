using System.Net;
using Commons.Exceptions;
using ApplicationException = Commons.Exceptions.ApplicationException;

namespace Infra.Exceptions.Mappers;

public class ExceptionToResponseMapper
{
    public ExceptionResponse Map(Exception exception) =>
        exception switch
        {
            DataNotFoundException ex
                => new ExceptionResponse(ex.Message, HttpStatusCode.NotFound),
            LogicException ex
                => new ExceptionResponse(ex.Message, HttpStatusCode.BadRequest),
            UnauthorizedException ex
                => new ExceptionResponse(ex.Message, HttpStatusCode.Unauthorized),
            ApplicationException ex
                => new ExceptionResponse(ex.Message, HttpStatusCode.UnprocessableEntity),
            _
                => new ExceptionResponse(
                    "Algo deu errado. Tente novamente mais tarde.",
                    HttpStatusCode.InternalServerError
                )
        };
}