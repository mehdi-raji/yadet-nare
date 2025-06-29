using Microsoft.EntityFrameworkCore;
using RoyMinder.Data.User;
using RoyMinder.Repository.DbContext;

namespace RoyMinder.Service.Chat;

public class ChatService(AppDbContext dbContext) : IChatService
{
    public async Task<User> Get(long chatId)
    {
        return await dbContext.User.AsNoTracking().FirstOrDefaultAsync(x => x.ChatId == chatId);
    }
}

public interface IChatService
{
    public Task<User> Get(long chatId);
}

