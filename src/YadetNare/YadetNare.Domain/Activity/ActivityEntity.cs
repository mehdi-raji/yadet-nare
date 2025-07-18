using YadetNare.Domain.Alarm;

namespace YadetNare.Domain.Activity;

public class ActivityEntity
{
    public int Id { get; set; } 
    
    public string Title { get; set; }  =  null!;
    public string Description { get; set; } =  null!;

    public long? ChatId { get; set; }
    public List<AlarmEntity> Alarms { get; set; } = null!;

}

