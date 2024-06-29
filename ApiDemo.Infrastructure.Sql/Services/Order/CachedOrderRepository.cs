using ApiDemo.Application.Dto;
using ApiDemo.Application.Repositories.Order;
using Microsoft.Extensions.Caching.Memory;

namespace ApiDemo.Infrastructure.Sql.Services.Order;

public class CachedOrderRepository(IOrderRepository _orderRepository, IMemoryCache _memoryCache) : IOrderRepository
{
    public async Task<int> CreateAsync(OrderDto order, CancellationToken cancellationToken = default)
    {
        return await _orderRepository.CreateAsync(order, cancellationToken);
    }

    public async Task<int> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _orderRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<List<OrderDto>> GetAllAsync(int? pageIndex, int? pageSize, CancellationToken cancellationToken = default)
    {
        var cachedOrder = await _memoryCache.GetOrCreateAsync($"orders-{pageIndex}-{pageSize}", async cacheEntry =>
        {
            cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(30));
            return await _orderRepository.GetAllAsync(pageIndex, pageSize, cancellationToken);
        });
        return cachedOrder!;
    }

    public async Task<OrderDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _orderRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<int> UpdateAsync(OrderDto order, CancellationToken cancellationToken = default)
    {
        return await _orderRepository.UpdateAsync(order, cancellationToken);
    }
}
