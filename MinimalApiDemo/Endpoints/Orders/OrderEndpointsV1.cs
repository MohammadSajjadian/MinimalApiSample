using ApiDemo.Application.Mapper.Order;
using ApiDemo.Application.Repositories.Order;
using ApiDemo.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MinimalApiDemo.Endpoints.Orders
{
    public static class OrderEndpointsV1
    {
        public static async Task<IResult> CreateAsync(Order order, IOrderRepository orderRepository, IOrderMapper mapper, CancellationToken cancellationToken)
        {
            var orderDto = mapper.Map(order);
            int entityId = await orderRepository.CreateAsync(orderDto, cancellationToken);
            return Results.Ok(entityId);
        }

        public static async Task<IResult> DeleteAsync(int id, IOrderRepository orderRepository, CancellationToken cancellationToken)
        {
            int deletedItems = await orderRepository.DeleteAsync(id, cancellationToken);

            if (deletedItems < 1)
                return Results.NotFound();

            return Results.Ok();
        }

        public static async Task<IResult> GetAllAsync([FromQuery] int? pageIndex, [FromQuery] int? pageSize, IOrderMapper mapper, IOrderRepository orderRepository, CancellationToken cancellationToken)
        {
            var orders = await orderRepository.GetAllAsync(pageIndex, pageSize, cancellationToken);
            return Results.Ok(mapper.Map(orders));
        }

        public static async Task<IResult> GetByIdAsync(int id, IOrderMapper mapper, IOrderRepository orderRepository, CancellationToken cancellationToken)
        {
            var OrderDto = await orderRepository.GetByIdAsync(id, cancellationToken);

            if (OrderDto is null)
                return Results.NotFound();

            return Results.Ok(mapper.Map(OrderDto));
        }

        public static async Task<IResult> UpdateAsync(Order order, IOrderMapper mapper, IOrderRepository orderRepository, CancellationToken cancellationToken)
        {
            int updatedItems = await orderRepository.UpdateAsync(mapper.Map(order), cancellationToken);

            if (updatedItems < 1)
                return Results.NotFound();

            return Results.Ok();
        }

        public static IResult GetOrderIds([FromHeader] OrderIds ids)
        {
            return Results.NoContent();
        }

        public static IResult Exception()
        {
            throw new Exception();
        }
    }
}
