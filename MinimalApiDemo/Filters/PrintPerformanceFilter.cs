
using System.Diagnostics;

namespace MinimalApiDemo.Filters;

public class PrintPerformanceFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        long sw = Stopwatch.GetTimestamp();
        var result = await next(context);
        TimeSpan ts = Stopwatch.GetElapsedTime(sw);
        Console.WriteLine($"Route is: {context.HttpContext.Request.Method} {context.HttpContext.Request.Path}, Total execution time is: {ts.TotalMilliseconds} ms");

        return result;
    }
}
