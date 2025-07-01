using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using YadetNare.Persistence.DbContext;
using ActivityAlarm = YadetNare.Entity.User.Alarm;

namespace YadetNare.Domain.Alarm;

public class AlarmService(AppDbContext dbContext) : IAlarmService
{
    public async Task<List<ActivityAlarm>> GetByActivity(int activityId)
    {
        return await dbContext.Alarm.AsNoTracking().Where(a => a.ActivityId == activityId).ToListAsync();
    }
}

public interface IAlarmService
{
    public Task <List<ActivityAlarm>> GetByActivity(int activityId);
}