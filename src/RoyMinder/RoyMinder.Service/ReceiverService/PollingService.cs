using Microsoft.Extensions.Logging;
using RoyMinder.Service.PollingService;

namespace RoyMinder.Service.ReceiverService;

public class PollingService(IServiceProvider serviceProvider, ILogger<PollingService> logger)
    : PollingServiceBase<ReceiverService>(serviceProvider, logger);



// Compose Receiver and UpdateHandler implementation