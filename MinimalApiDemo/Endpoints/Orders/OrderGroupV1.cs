using ApiDemo.Domain.Entities;
using MinimalApiDemo.Filters;

namespace MinimalApiDemo.Endpoints.Orders
{
    public static class OrderGroupV1
    {
        public static void AddOrderEndPoints(this WebApplication app)
        {
            var group = app.MapGroup("/v1/orders");

            group.MapPost("/", OrderEndpointsV1.CreateAsync)
                .AddEndpointFilter<PrintPerformanceFilter>()
                .AddEndpointFilter<InputValidator<Order>>();

            group.MapDelete("/{id}", OrderEndpointsV1.DeleteAsync)
                .AddEndpointFilter<PrintPerformanceFilter>();

            group.MapGet("/", OrderEndpointsV1.GetAllAsync)
                .AddEndpointFilter<PrintPerformanceFilter>()
                .RequireAuthorization();

            group.MapGet("/{id}", OrderEndpointsV1.GetByIdAsync)
                .AddEndpointFilter<PrintPerformanceFilter>();

            group.MapPut("/{id}", OrderEndpointsV1.UpdateAsync)
                .AddEndpointFilter<PrintPerformanceFilter>()
                .AddEndpointFilter<InputValidator<Order>>();

            group.MapGet("/exception", OrderEndpointsV1.Exception);
            group.MapGet("/pattern/{ids}", OrderEndpointsV1.GetOrderIds);
        }
    }
}
