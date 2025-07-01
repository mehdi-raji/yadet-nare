using Microsoft.Extensions.Logging;
using YadetNare.Domain.PollingService;

namespace YadetNare.Domain.ReceiverService;

public class PollingService(IServiceProvider serviceProvider, ILogger<PollingService> logger)
    : PollingServiceBase<ReceiverService>(serviceProvider, logger);



// Compose Receiver and UpdateHandler implementation