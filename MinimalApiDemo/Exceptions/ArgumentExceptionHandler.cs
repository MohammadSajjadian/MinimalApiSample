using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MinimalApiDemo.Exceptions
{
    public class ArgumentExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is ArgumentException)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;

                await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
                {
                    Status = (int)HttpStatusCode.UnprocessableEntity,
                    Title = "Parameter format",
                    Detail = exception.Message,
                    Type = exception.GetType().ToString(),
                    Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
                }, cancellationToken);

                return true;
            }
            return false;
        }
    }
}
