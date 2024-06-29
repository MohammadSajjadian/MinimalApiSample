
namespace MinimalApiDemo.Filters
{
    public class AccessTokenValidatorFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var authorizationHeader = context.HttpContext.Request.Headers["access_token"];

            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.ToString().StartsWith("Bearer"))
                return await next.Invoke(context);

            return StatusCodes.Status401Unauthorized;
        }
    }
}
