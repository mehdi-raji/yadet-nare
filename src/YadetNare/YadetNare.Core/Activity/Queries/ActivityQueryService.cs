using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using YadetNare.Domain.Activity;
using YadetNare.Infrastructure.Common.Persistence;

namespace YadetNare.Core.Activity.Queries;

public class ActivityQueryService(AppDbContext dbContext) : IActivityQueryService
{
    public async Task<IList<ActivityModel>> GetAllAsync(long chatId)
    {
        return await dbContext.Activity.AsNoTracking()
            .Where(x => x.ChatId == chatId).ToListAsync();
    }
    
    public async Task<ActivityModel> GetAsync(string id)
    {
        if (!string.IsNullOrEmpty(id) && id != "0" && int.TryParse(id, out var entityId))
            return await GetAsync(entityId);
        
        return null!;
    }
    
    public async Task<ActivityModel> GetForEditAsync(int id)
    {
        return await dbContext.Activity.SingleAsync(a => a.Id == id);
    }
    public async Task<ActivityModel> GetAsync(int id)
    {
        return await dbContext.Activity.AsNoTracking().SingleAsync(a => a.Id == id);
    }
    
}
