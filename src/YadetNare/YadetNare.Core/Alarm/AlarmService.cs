using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using YadetNare.Domain.Alarm;
using YadetNare.Infrastructure.Common.Persistence;

namespace YadetNare.Core.Alarm;
// todo: refactor
public class AlarmService(AppDbContext dbContext) : IAlarmService
{
    public async Task<List<AlarmModel>> GetByActivity(int activityId)
    {
        return await dbContext.Alarm.AsNoTracking().Where(a => a.ActivityId == activityId).ToListAsync();
    }
}

public interface IAlarmService
{
    public Task <List<AlarmModel>> GetByActivity(int activityId);
}