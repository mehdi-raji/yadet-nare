namespace RoyMinder.Service.ReceiverService.Abstract;

public interface IReceiverService
{
    Task ReceiveAsync(CancellationToken stoppingToken);
}