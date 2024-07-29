using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using cinema_ticket_seller_pdi.Exceptions;

namespace cinema_ticket_seller_pdi.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var statusCode = context.Exception switch
            {
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                NotFoundException => StatusCodes.Status404NotFound,
                _ => 500
            };

            context.ExceptionHandled = true;
            context.Result = new ObjectResult(new { message = context.Exception.Message })
            {
                StatusCode = statusCode
            };
        }
    }
}
