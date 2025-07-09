using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using YadetNare.Domain.Infrastructure;
using YadetNare.Entity.Activity;
using YadetNare.Persistence.DbContext;


namespace YadetNare.Domain.Activity;

public class ActivityService(AppDbContext dbContext, ITelegramBotClient bot)
    : IActivityService
{
    public async Task<IList<ActivityEntity>> GetAll(long chatId)
    {
        return await dbContext.Activity.AsNoTracking()
            .Where(x => x.ChatId == chatId).ToListAsync();
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
    
    public async Task Manage(CallbackQuery callbackQuery)
    {
        var data = callbackQuery.Data!.Split(":");
        
        // refactor: magic numbers in here!! 
        // refactor: Hard coded things !!!
        var dataOp = data[1];
        switch (dataOp)
        {
            case "add":
                await Show(callbackQuery.Message!.Chat.Id, new ActivityEntity());
                break;
            case "show":
                await Show(callbackQuery.Message!.Chat.Id, await Get(data[2]));
                break;
            case "edit":
                await Edit(callbackQuery, await Get(data[2]));
                break;
            default:
                await Show(callbackQuery.Message!.Chat.Id, new ActivityEntity());
                break;
                
        }

    }

    public async Task HandleEdit(Message message, UserState userState)
    {
        var activty = await GetOrCreate(userState.EntityId);
        activty.ChatId = message.Chat.Id;
        switch ( userState.AffectedColumn)
        {
            case "title":
                activty.Title = message.Text;
                break;
            case "description":
                activty.Description = message.Text;
                break;
            default:
                ChatInfo.States.Remove(message.Chat.Id);
                throw new Exception();
        }

        await dbContext.SaveChangesAsync();
        await Show(message.Chat.Id,activty);
        ChatInfo.States.Remove(message.Chat.Id);
        
    }
    
    private async Task Edit(CallbackQuery callbackQuery, ActivityEntity activity)
    {
        var data = callbackQuery.Data!.Split(":");
        var field = data[3];
        
        switch (field)
        {
            case "title":
                await bot.SendMessage(callbackQuery.Message!.Chat.Id, "عنوان جدید را وارد کنید!", replyMarkup: new ForceReplyMarkup());
                ChatInfo.States[callbackQuery.Message.Chat.Id] = new UserState(State.Edit, field, activity?.Id, EntityType.Activity);
                
                break;
            case "description":
                await bot.SendMessage(callbackQuery.Message!.Chat.Id, "توضیحات جدید را وارد کنید!");
                ChatInfo.States[callbackQuery.Message.Chat.Id] = new UserState(State.Edit, field, activity?.Id, EntityType.Activity);

                break;
        }
    }
    private async Task Show(long chatId, ActivityEntity activity)
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
                CallbackData = $"activity:edit:{activity.Id}:description",
            },
            new()
            {
                Text = "ویرایش عنوان",
                CallbackData = $"activity:edit:{activity.Id}:title",
            }
        };


        var inlineMarkup = new InlineKeyboardMarkup()
                .AddNewRow()
                .AddButtons(keyBoardButtons)
            ;

        await bot.SendMessage(chatId, message, parseMode: ParseMode.Html,
            replyMarkup: inlineMarkup);
    }
    
    private async Task<ActivityEntity> GetOrCreate(int? activityId)
    {
        if (activityId != null) return await GetForEdit(activityId.Value);
        
        var activity = new ActivityEntity();
        await dbContext.AddAsync(activity);
        return activity;
    }
    private async Task<ActivityEntity> Get(int id)
    {
        return await dbContext.Activity.AsNoTracking().SingleAsync(a => a.Id == id);
    }

    private async Task<ActivityEntity> GetForEdit(int id)
    {
        return await dbContext.Activity.SingleAsync(a => a.Id == id);
    }
    private async Task<ActivityEntity> Get(string id)
    {
        if (!string.IsNullOrEmpty(id) && id != "0" && int.TryParse(id, out var entityId))
            return await Get(entityId);
        
        return null;
    }

}

public interface IActivityService
{
    public Task<IList<ActivityEntity>> GetAll(long chatId);
    Task HandleEdit(Message message, UserState userState);
    Task ShowAll(Message msg);
    Task Manage(CallbackQuery callBackQuery);
}