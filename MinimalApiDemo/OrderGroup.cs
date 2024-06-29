using ApiDemo.Domain.Entities;
using Asp.Versioning.Builder;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MinimalApiDemo.Filters;

namespace MinimalApiDemo
{
    public static class OrderGroup
    {
        public static RouteGroupBuilder OrderGroupBuilderV1(this RouteGroupBuilder group, ApiVersionSet apiVersionSet)
        {

            var orders = new List<Order>()
            {
                new(){Id = 1, Date =  DateTime.Now, IsConfirmed = true},
                new(){Id = 2, Date = DateTime.Now.AddDays(-1), IsConfirmed = true},
                new(){Id = 3, Date = DateTime.Now.AddDays(-2), IsConfirmed = false}
            };

            group.MapGet("/", () => orders).WithApiVersionSet(apiVersionSet).HasApiVersion(1, 0).Produces(StatusCodes.Status404NotFound).Produces(StatusCodes.Status500InternalServerError);
            group.MapGet("/Ids", ([FromQuery(Name = "id")] int[] ids) =>
            {
                return orders.Where(order => ids.Contains(order.Id)).ToList();
            });
            group.MapPost("/", (Order order) =>
            {
                try
                {
                    orders.Add(order);
                    return StatusCodes.Status200OK;
                }
                catch
                {
                    return StatusCodes.Status500InternalServerError;
                }
            });

            group.MapDelete("/{orderId}", (int orderId) =>
            {
                try
                {
                    var order = orders.Find(o => o.Id == orderId);
                    if (order is null)
                        return StatusCodes.Status404NotFound;

                    orders.Remove(order);
                    return StatusCodes.Status200OK;
                }
                catch
                {
                    return StatusCodes.Status500InternalServerError;
                }
            }).ExcludeFromDescription();

            group.MapPut("/{orderId}", (int orderId, Order newOrder) =>
            {
                try
                {
                    var oldOrder = orders.Find(o => o.Id == orderId);
                    if (oldOrder is null)
                        return Results.BadRequest();

                    oldOrder.Date = newOrder.Date;
                    oldOrder.IsConfirmed = newOrder.IsConfirmed;

                    return Results.Ok();
                }
                catch
                {
                    return Results.BadRequest();
                }
            }).WithApiVersionSet(apiVersionSet).HasApiVersion(1, 0).AddEndpointFilter<InputValidator<Order>>();
            //WithOpenApi(opt => new(opt)
            //{
            //    Deprecated = true,

            //})

            return group;
        }
    }
}
