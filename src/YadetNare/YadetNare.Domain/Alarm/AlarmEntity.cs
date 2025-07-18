using YadetNare.Domain.Activity;

namespace YadetNare.Domain.Alarm;

public class AlarmEntity
{
    public int Id { get; set; }
    
    public required DateTime Time { get; set; }
    
    public int ActivityId { get; set; }

    public ActivityEntity Activity { get; set; } = null!;

}