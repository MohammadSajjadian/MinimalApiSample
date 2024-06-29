using ApiDemo.Application.Dto;

namespace ApiDemo.Application.Mapper.Order;

public class OrderMapper : IOrderMapper
{
    public OrderDto Map(Domain.Entities.Order order) =>
        new(order.Id, order.Date, order.IsConfirmed);

    public Domain.Entities.Order Map(OrderDto orderDto) =>
        new() { Id = orderDto.Id, Date = orderDto.Date, IsConfirmed = orderDto.IsConfirm };

    public List<Domain.Entities.Order> Map(List<OrderDto> orderDto)
    {
        return [..orderDto.Select(d => new Domain.Entities.Order
        {
            Id = d.Id,
            Date = d.Date,
            IsConfirmed = d.IsConfirm
        })];
    }
}
