using MinimalApiDemo.Filters;

namespace MinimalApiDemo.Endpoints.Account
{
    public static class AccountGroup
    {
        public static void AddAcountEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("account")
                .AddEndpointFilter<PrintPerformanceFilter>();

            group.MapPost("/register", AccountEndpoints.Register);
            group.MapPost("/login", AccountEndpoints.Login);
            group.MapGet("/profile/{userName}", AccountEndpoints.GetUser);
            group.MapGet("/profile", AccountEndpoints.GetCurrentUser);
            group.MapPost("/logout", AccountEndpoints.LogOut);
        }
    }
}
