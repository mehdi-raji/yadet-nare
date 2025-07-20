using YadetNare.Domain.Activity;

namespace YadetNare.Domain.Alarm;

public class AlarmModel
{
    public int Id { get; set; }
    
    public required DateTime Time { get; set; }
    
    public int ActivityId { get; set; }

    public ActivityModel Activity { get; set; } = null!;

}