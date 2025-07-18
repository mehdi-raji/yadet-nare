using System.Threading;

namespace YadetNare.Core.ReceiverService.Abstract;

public interface IReceiverService
{
    Task ReceiveAsync(CancellationToken stoppingToken);
}