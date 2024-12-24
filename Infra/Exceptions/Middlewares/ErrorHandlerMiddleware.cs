using System.Net;
using Infra.Exceptions.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Infra.Exceptions.Middlewares;

public class ErrorHandlerMiddleware : IMiddleware
{
    private readonly ILogger<ErrorHandlerMiddleware> _logger;
    private readonly ExceptionToResponseMapper _exceptionToResponseMapper;

    public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger,
        ExceptionToResponseMapper exceptionToResponseMapper)
    {
        _logger = logger;
        _exceptionToResponseMapper = exceptionToResponseMapper;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var errorResponse = _exceptionToResponseMapper.Map(exception);

        context.Response.StatusCode = (int)errorResponse.StatusCode;

        await context.Response.WriteAsJsonAsync(errorResponse);
    }
}