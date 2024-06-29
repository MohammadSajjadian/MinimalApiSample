namespace MinimalApiDemo.CrossCutting;

public static class Extension
{
    public static void AddApiDemo(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddConfiguredServices(configuration);
        services.RegisterDependencies();
    }
}
