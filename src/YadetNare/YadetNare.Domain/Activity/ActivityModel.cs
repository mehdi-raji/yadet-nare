using YadetNare.Domain.Alarm;

namespace YadetNare.Domain.Activity;

public class ActivityModel
{
    public int Id { get; set; } 
    
    public string Title { get; set; }  =  null!;
    public string Description { get; set; } =  null!;

    public long? ChatId { get; set; }
    public List<AlarmModel> Alarms { get; set; } = null!;
    

}

