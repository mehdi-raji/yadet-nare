using Microsoft.Extensions.Logging;
using RoyMinder.Service.ReceiverService.Abstract;
using Telegram.Bot;

namespace RoyMinder.Service.ReceiverService;

public class ReceiverService(ITelegramBotClient botClient, UpdateHandler.UpdateHandler updateHandler, ILogger<ReceiverServiceBase<UpdateHandler.UpdateHandler>> logger)
    : ReceiverServiceBase<UpdateHandler.UpdateHandler>(botClient, updateHandler, logger);