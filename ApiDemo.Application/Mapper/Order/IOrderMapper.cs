using ApiDemo.Application.Dto;

namespace ApiDemo.Application.Mapper.Order;

public interface IOrderMapper
{
    OrderDto Map(Domain.Entities.Order order);
    Domain.Entities.Order Map(OrderDto orderDto);
    List<Domain.Entities.Order> Map(List<OrderDto> orderDto);
}
