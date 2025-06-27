using Microsoft.Extensions.Logging;

namespace RoyMinder.Service.PollingService;

public class PollingService(IServiceProvider serviceProvider, ILogger<PollingService> logger)
    : PollingServiceBase<ReceiverService.ReceiverService>(serviceProvider, logger);



// Compose Receiver and UpdateHandler implementation