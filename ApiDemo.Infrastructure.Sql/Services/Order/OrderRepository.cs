using ApiDemo.Application.Dto;
using ApiDemo.Application.Mapper.Order;
using ApiDemo.Application.Repositories.Order;
using ApiDemo.Infrastructure.Sql.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;
using System.Data;

namespace ApiDemo.Infrastructure.Sql.Services.Order;

public class OrderRepository(ApiDemoContext _db, IOrderMapper _mapper) : IOrderRepository
{
    public async Task<int> CreateAsync(OrderDto orderDto, CancellationToken cancellationToken)
    {
        var orderEntity = _mapper.Map(orderDto);

        _db.Add(orderEntity);
        await _db.SaveChangesAsync(cancellationToken);

        return orderEntity.Id;
    }

    public async Task<int> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        return await _db.Orders
            .Where(o => o.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<List<OrderDto>> GetAllAsync(int? pageIndex, int? pageSize, CancellationToken cancellationToken)
    {
        return await _db.Orders
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .Paging(pageIndex, pageSize)
            .Select(o => new OrderDto(o.Id, o.Date, o.IsConfirmed))
            .ToListAsync(cancellationToken);
    }

    public async Task<OrderDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _db.Orders
            .AsNoTracking()
            .Where(o => o.Id == id)
            .Select(o => new OrderDto(o.Id, o.Date, o.IsConfirmed))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<int> UpdateAsync(OrderDto order, CancellationToken cancellationToken)
    {
        return await _db.Orders
            .Where(o => o.Id == order.Id)
            .ExecuteUpdateAsync(setter => setter
            .SetProperty(o => o.Date, order.Date)
            .SetProperty(o => o.IsConfirmed, order.IsConfirm), cancellationToken);
    }
}
