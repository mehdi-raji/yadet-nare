using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using YadetNare.Persistence.DbContext;
using YadetNare.Domain.Chat;
using YadetNare.Entity.Activity;
using YadetNare.Shared;


namespace YadetNare.Domain.Activity;

public class ActivityService(IChatService chatService, AppDbContext dbContext, ITelegramBotClient bot)
    : IActivityService
{
    public async Task<IList<ActivityEntity>> GetAll(long chatId)
    {
        var chat = await chatService.Get(chatId);
        if (chat is null)
            return new List<ActivityEntity>();

        return await dbContext.Activity.AsNoTracking()
            .Where(x => x.UserId == chat.Id).ToListAsync();
    }

    public async Task<ActivityEntity> Get(int id)
    {
        return await dbContext.Activity.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<ActivityEntity> Get(string id)
    {
        if (int.TryParse(id, out var entityId))
            return await Get(entityId);
        
        return null;
    }


    public async Task ShowAll(Message msg)
    {
        var inlineMarkup = new InlineKeyboardMarkup();

        await GenerateMessage();

        // todo: Remove Keyboard
        await bot.SendMessage(msg.Chat.Id, "یادت نره!", replyMarkup: inlineMarkup);

        return;

        async Task GenerateMessage()
        {
            // refactor: USE InlineKeyboardButton[] 
            var activities = await GetAll(msg.Chat.Id);
            inlineMarkup = activities.Aggregate(inlineMarkup,
                (current, activity) =>
                    current.AddButton($"{Emoji.Pushpin} {activity.Title}",
                        $"activity:show:{activity.Id}"));
            inlineMarkup.AddNewRow()
                .AddButton(Text.AddActivity, "activity:add");
        }
    }

    public async Task<IList<ActivityEntity>> GetAll(int chatId)
    {
        return await dbContext.Activity.AsNoTracking()
            .Where(x => x.UserId == chatId).ToListAsync();
    }

    public async Task Manage(CallbackQuery callbackQuery)
    {
        var data = callbackQuery.Data!.Split();
        // refactor: magic numbers in here!! 
        var dataOp = data[1];
        switch (dataOp)
        {
            case "add":
                await Show(callbackQuery, new ActivityEntity());
                break;
            case "show":
                await Show(callbackQuery, await Get(data[2]));
                break;
            case "edit":
                await Edit(callbackQuery, await Get(data[2]));
                break;
            default:
                await Show(callbackQuery, new ActivityEntity());
                break;
                
        }

    }


    private async Task Edit(CallbackQuery callbackQuery, ActivityEntity activity)
    {
        var data = callbackQuery.Data!.Split(":");
        var field = data[3];
        switch (field)
        {
            case "title":
                await bot.SendMessage(callbackQuery.Message!.Chat.Id, "عنوان جدید را وارد کنید!", replyMarkup: new ReplyKeyboardRemove());
                break;
            case "description":
                await bot.SendMessage(callbackQuery.Message!.Chat.Id, "توضیحات جدید را وارد کنید!");
                break;
        }
    }
    private async Task Show(CallbackQuery callbackQuery, ActivityEntity activity)
    {
        var message = $"""
                           <b>مشخصات رویداد{Emoji.Fire}</b>
                           
                           عنوان:<b> {activity.Title ?? "خالی!"} </b>
                           
                            توضیحات اضافه: {activity.Description ?? "خالی!"}
                       """;

        var keyBoardButtons = new InlineKeyboardButton[]
        {
            new()
            {
                Text = "حذف",
                CallbackData = $"activity:delete:{activity.Id}",
            },
            new()
            {
                Text = "ویرایش توضیحات",
                CallbackData = $"activity:edit:{activity.Id}:title",
            },
            new()
            {
                Text = "ویرایش عنوان",
                CallbackData = $"activity:edit:{activity.Id}:description",
            }
        };


        var inlineMarkup = new InlineKeyboardMarkup()
                .AddNewRow()
                .AddButtons(keyBoardButtons)
            ;

        await bot.SendMessage(callbackQuery.Message!.Chat.Id, message, parseMode: ParseMode.Html,
            replyMarkup: inlineMarkup);
    }
}

public interface IActivityService
{
    public Task<IList<ActivityEntity>> GetAll(long chatId);
    Task ShowAll(Message msg);
    Task Manage(CallbackQuery callBackQuery);
}