using ApiDemo.Application.Mapper.Order;
using ApiDemo.Application.Mapper.User;
using ApiDemo.Application.Repositories.Account;
using ApiDemo.Application.Repositories.Order;
using ApiDemo.Application.Validator;
using ApiDemo.Domain.Entities;
using ApiDemo.Infrastructure.Sql.Repositories;
using ApiDemo.Infrastructure.Sql.Services.Account;
using ApiDemo.Infrastructure.Sql.Services.Background;
using ApiDemo.Infrastructure.Sql.Services.Order;
using FluentValidation;
using Microsoft.Extensions.Options;
using MinimalApiDemo.Configuration;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MinimalApiDemo.CrossCutting;

public static class IoCContainer
{
    public static void RegisterDependencies(this IServiceCollection services)
    {
        services.AddSingleton<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigurationsOptions>();

        services.AddSingleton<IOrderBackground, OrderBackground>();

        services.AddScoped<IValidator<Order>, OrderValidator>();

        services.AddScoped<IOrderMapper, OrderMapper>();
        services.AddScoped<IUserMapper, UserMapper>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.Decorate<IOrderRepository, CachedOrderRepository>();

        services.AddHttpContextAccessor();
    }
}
