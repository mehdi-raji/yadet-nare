using Microsoft.EntityFrameworkCore;
using YadetNare.Entity.User;
using YadetNare.Persistence.DbContext;

namespace YadetNare.Domain.Chat;

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

