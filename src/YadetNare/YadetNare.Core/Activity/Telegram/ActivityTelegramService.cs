using System.Collections.Generic;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using YadetNare.Core.Activity.Queries;
using YadetNare.Core.Infrastructure;
using YadetNare.Domain.Activity;
using YadetNare.Infrastructure.Common.Persistence;

// todo: refactor

namespace YadetNare.Core.Activity.Telegram;

public class ActivityTelegramService(AppDbContext dbContext, ITelegramBotClient bot, IActivityQueryService queries)
    : IActivityTelegramService
{
    public async Task ShowAll(Message msg)
    {
        var activities = await queries.GetAllAsync(msg.Chat.Id);
        var inlineMarkup = ListMarkUpBuilder(activities);
        
        // todo: Remove Keyboard
        await bot.SendMessage(msg.Chat.Id, "یادت نره!", replyMarkup: inlineMarkup);
        
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
                await Show(callbackQuery.Message!.Chat.Id, new ActivityModel());
                break;
            case "show":
                await Show(callbackQuery.Message!.Chat.Id, await queries.GetAsync(data[2]));
                break;
            case "edit":
                await Edit(callbackQuery, await queries.GetAsync(data[2]));
                break;
            default:
                await Show(callbackQuery.Message!.Chat.Id, new ActivityModel());
                break;
        }
    }

    public InlineKeyboardMarkup ListMarkUpBuilder(IEnumerable<ActivityModel> activities)
    {
        var markup = new InlineKeyboardMarkup();

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var activity in activities)
        {
            markup = markup.AddButton(activity.GetListButtonText(), activity.ToShowCallBackData());
        }

        markup
            .AddNewRow()
            .AddButton(ActivityTelegramHelper.AddButtonText, ActivityTelegramHelper.AddCallBack);
        return markup;
    }

    public async Task HandleEdit(Message message, UserState userState)
    {
        var activty = await GetOrCreate(userState.EntityId);
        activty.ChatId = message.Chat.Id;
        switch (userState.AffectedColumn)
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
        await Show(message.Chat.Id, activty);
        ChatInfo.States.Remove(message.Chat.Id);
    }

    private async Task Edit(CallbackQuery callbackQuery, ActivityModel activity)
    {
        var data = callbackQuery.Data!.Split(":");
        var field = data[3];

        switch (field)
        {
            case "title":
                await bot.SendMessage(callbackQuery.Message!.Chat.Id, "عنوان جدید را وارد کنید!",
                    replyMarkup: new ForceReplyMarkup());
                ChatInfo.States[callbackQuery.Message.Chat.Id] =
                    new UserState(State.Edit, field, activity?.Id, EntityType.Activity);

                break;
            case "description":
                await bot.SendMessage(callbackQuery.Message!.Chat.Id, "توضیحات جدید را وارد کنید!");
                ChatInfo.States[callbackQuery.Message.Chat.Id] =
                    new UserState(State.Edit, field, activity?.Id, EntityType.Activity);

                break;
        }
    }

    private async Task Show(long chatId, ActivityModel activity)
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

    private async Task<ActivityModel> GetOrCreate(int? activityId)
    {
        if (activityId != null) return await queries.GetForEditAsync(activityId.Value);

        var activity = new ActivityModel();
        await dbContext.AddAsync(activity);
        return activity;
    }
}