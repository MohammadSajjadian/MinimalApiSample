using ApiDemo.Infrastructure.Sql.Repositories;
using Microsoft.Extensions.Hosting;

namespace ApiDemo.Infrastructure.Sql.BackgroundTasks;

public class OrderBackgroundService(IOrderBackground _data) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            int deletedNumber = await _data.DeleteCancelledOrdersAsync(stoppingToken);

            if (deletedNumber > 0)
                Console.WriteLine("{0} order" + (deletedNumber > 1 ? "'s are" : "") + " deleted.", deletedNumber);
            else
                Console.WriteLine("No unconfirmed order found.");

            await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
        }
    }
}

    //private Timer? _timer;

    //public async Task StartAsync(CancellationToken cancellationToken)
    //{
    //    _timer = new(Delete, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
    //    await Task.CompletedTask;
    //}

    //public async Task StopAsync(CancellationToken cancellationToken)
    //{
    //    _timer?.Change(Timeout.Infinite, 0);
    //    await Task.CompletedTask;
    //}

    //private void Delete(object? state) =>
    //    DeleteAsync().Wait();

    //private async Task DeleteAsync()
    //{
    //    int deletedNumber = await _data.DeleteCancelledOrdersAsync();

    //    if (deletedNumber > 0)
    //        Console.WriteLine("{0} order" + (deletedNumber > 0 ? "'s are" : "") + " deleted.", deletedNumber);
    //    else
    //        Console.WriteLine("No unconfirmed order found.");
    //}

    //public void Dispose() =>
    //    _timer?.Dispose();
//}
