namespace MinimalApiDemo.Middlewares
{
    public static class QueryExtensions
    {
        public static IApplicationBuilder UseQuery(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app);

            app.UseWhen(ctx => !string.IsNullOrEmpty(ctx.Request.Query["hello"]), builder =>
            {
                builder.Use(async (ctx, next) =>
                {
                    Console.WriteLine($"Request path: {ctx.Request.Path}, Request result: {ctx.Request.Query["hello"] + 's'}");

                    await next();
                });
            });

            //app.UseMiddleware<QueryMiddleware>();

            return app;
        }
    }
}
