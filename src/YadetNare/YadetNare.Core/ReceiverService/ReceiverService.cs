using YadetNare.Core.ReceiverService.Abstract;

namespace YadetNare.Core.ReceiverService;

public class ReceiverService(ITelegramBotClient botClient, UpdateHandler.UpdateHandler updateHandler, ILogger<ReceiverServiceBase<UpdateHandler.UpdateHandler>> logger)
    : ReceiverServiceBase<UpdateHandler.UpdateHandler>(botClient, updateHandler, logger);