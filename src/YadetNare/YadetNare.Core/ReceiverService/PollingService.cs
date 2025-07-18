using YadetNare.Core.ReceiverService.Abstract;

namespace YadetNare.Core.ReceiverService;

public class PollingService(IServiceProvider serviceProvider, ILogger<PollingService> logger)
    : PollingServiceBase<ReceiverService>(serviceProvider, logger);



// Compose Receiver and UpdateHandler implementation