using System.Threading;

namespace YadetNare.Domain.ReceiverService.Abstract;

public interface IReceiverService
{
    Task ReceiveAsync(CancellationToken stoppingToken);
}