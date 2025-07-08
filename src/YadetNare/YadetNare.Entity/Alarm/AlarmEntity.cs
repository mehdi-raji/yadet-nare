using YadetNare.Entity.Activity;

namespace YadetNare.Entity.Alarm;

public class AlarmEntity
{
    public int Id { get; set; }
    
    public required DateTime Time { get; set; }
    
    public int ActivityId { get; set; }
    
    public ActivityEntity Activity { get; set; }
    
}