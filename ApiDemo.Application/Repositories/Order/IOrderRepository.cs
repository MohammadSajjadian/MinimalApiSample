using ApiDemo.Application.Dto;

namespace ApiDemo.Application.Repositories.Order;

public interface IOrderRepository
{
    Task<int> CreateAsync(OrderDto order, CancellationToken cancellationToken = default);
    Task<List<OrderDto>> GetAllAsync(int? pageIndex, int? pageSize, CancellationToken cancellationToken = default);
    Task<OrderDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<int> UpdateAsync(OrderDto order, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
