using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using YadetNare.Persistence.Alarm;
using YadetNare.Persistence.DbContext;


namespace YadetNare.Domain.Alarm;
// todo: refactor
public class AlarmService(AppDbContext dbContext) : IAlarmService
{
    public async Task<List<AlarmEntity>> GetByActivity(int activityId)
    {
        return await dbContext.Alarm.AsNoTracking().Where(a => a.ActivityId == activityId).ToListAsync();
    }
}

public interface IAlarmService
{
    public Task <List<AlarmEntity>> GetByActivity(int activityId);
}