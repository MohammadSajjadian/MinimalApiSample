using ApiDemo.Domain.Entities;
using ApiDemo.Infrastructure.Sql.BackgroundTasks;
using ApiDemo.Infrastructure.Sql.Context;
using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using MinimalApiDemo.Exceptions;
using System.Net;
using System.Threading.RateLimiting;

namespace MinimalApiDemo.CrossCutting;

public static class ServiceConfiguration
{
    public static void AddConfiguredServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient("Client", opt =>
        {
            opt.BaseAddress = new Uri(configuration.GetValue<string>("LocalAddress")!);
        });

        services.AddDbContextPool<ApiDemoContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("DbDemo"), conf =>
            {
                conf.EnableRetryOnFailure(3);
                conf.CommandTimeout(30);
            });
        });

        services.AddEndpointsApiExplorer();
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = new HeaderApiVersionReader("api version");
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VV"; // Formats the version as follow: "'v'major[.minor]"
        });

        services.AddRateLimiter(conf =>
        {
            conf.RejectionStatusCode = (int)HttpStatusCode.TooManyRequests;
            conf.OnRejected = async (context, token) =>
            {
                await context.HttpContext.Response.WriteAsync("Too many requests. please try again later.", token);
            };
            conf.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                RateLimitPartition.GetFixedWindowLimiter(partitionKey: context.Connection.RemoteIpAddress!.ToString(),
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        Window = TimeSpan.FromHours(1),
                        PermitLimit = 100,
                        QueueLimit = 10,
                        AutoReplenishment = true
                    })
            );
        });

        services.AddCors(opt =>
        {
            opt.AddPolicy("internal", conf =>
            {
                conf.WithOrigins(["localhost:7777"]);
                conf.WithMethods(["GET", "POST", "PUT", "DELETE"]);
                conf.AllowAnyHeader();
            });
        });

        services.AddExceptionHandler<ArgumentExceptionHandler>();
        services.AddExceptionHandler<DefaultExceptionHandler>();

        services.AddSwaggerGen();

        services.AddHostedService<OrderBackgroundService>();

        services.AddMemoryCache();

        services.AddHealthChecks().AddSqlServer(configuration.GetConnectionString("DbDemo")!);

        services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApiDemoContext>();

        services.ConfigureApplicationCookie(options =>
        {
            // Cookie settings
            options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        });

        services.AddAuthorizationBuilder()
            .AddPolicy("adminPolicy", confPolicy => confPolicy.RequireRole("admin"));
    }
}
