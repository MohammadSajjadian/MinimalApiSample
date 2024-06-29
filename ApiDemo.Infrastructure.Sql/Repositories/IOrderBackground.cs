namespace ApiDemo.Infrastructure.Sql.Repositories;

public interface IOrderBackground
{
    Task<int> DeleteCancelledOrdersAsync(CancellationToken cancellationToken);
}
