using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using YadetNare.Persistence.DbContext;
using YadetNare.Domain.Chat;
using UserActivity = YadetNare.Entity.User.Activity;


namespace YadetNare.Domain.Activity;

public class ActivityService(IChatService chatService,AppDbContext dbContext) : IActivityService
{
    public async Task<IList<UserActivity>> GetAll(long chatId)
    {
        var chat = await chatService.Get(chatId);
        if (chat is null)
            return new List<UserActivity>();
        
        return await dbContext.Activity.AsNoTracking()
            .Where(x=>x.UserId == chat.Id).ToListAsync();
        
    }
    public async Task<IList<UserActivity>> GetAll(int chatId)
    {
        return await dbContext.Activity.AsNoTracking()
            .Where(x=>x.UserId == chatId).ToListAsync();
        
    }
}

public interface IActivityService
{
    public Task<IList<UserActivity>> GetAll(long chatId);
}