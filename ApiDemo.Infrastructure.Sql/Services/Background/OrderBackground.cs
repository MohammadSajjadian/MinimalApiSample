using ApiDemo.Infrastructure.Sql.Context;
using ApiDemo.Infrastructure.Sql.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ApiDemo.Infrastructure.Sql.Services.Background;

public class OrderBackground(IServiceScopeFactory _serviceScope) : IOrderBackground
{
    public async Task<int> DeleteCancelledOrdersAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceScope.CreateScope();
        var context = scope.ServiceProvider.GetService<ApiDemoContext>();

        var orders = context!.Orders.Where(o => o.IsConfirmed == false);

        if (orders.Any())
            return await orders.ExecuteDeleteAsync(cancellationToken);

        return 0;
    }
}
