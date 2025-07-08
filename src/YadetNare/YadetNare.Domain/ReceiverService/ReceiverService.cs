using YadetNare.Domain.ReceiverService.Abstract;

namespace YadetNare.Domain.ReceiverService;

public class ReceiverService(ITelegramBotClient botClient, UpdateHandler.UpdateHandler updateHandler, ILogger<ReceiverServiceBase<UpdateHandler.UpdateHandler>> logger)
    : ReceiverServiceBase<UpdateHandler.UpdateHandler>(botClient, updateHandler, logger);