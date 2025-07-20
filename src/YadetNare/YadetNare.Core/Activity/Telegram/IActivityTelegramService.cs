using System.Collections.Generic;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using YadetNare.Core.Infrastructure;
using YadetNare.Domain.Activity;

namespace YadetNare.Core.Activity.Telegram;

public interface IActivityTelegramService
{
    Task HandleEdit(Message message, UserState userState);
    Task ShowAll(Message msg);
    Task Manage(CallbackQuery callBackQuery);
    
    protected InlineKeyboardMarkup ListMarkUpBuilder(IEnumerable<ActivityModel> activities);
}