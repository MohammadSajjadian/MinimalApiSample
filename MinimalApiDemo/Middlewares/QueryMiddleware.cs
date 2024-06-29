using Microsoft.Extensions.Primitives;

namespace MinimalApiDemo.Middlewares
{
    public class QueryMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext ctx)
        {
            StringValues query = ctx.Request.Query["hello"];

            if (!string.IsNullOrEmpty(query))
                Console.WriteLine($"Request path: {ctx.Request.Path}, Request result: {query + 's'}");
            
            await _next.Invoke(ctx);
        }
    }
}
