using System.ComponentModel.DataAnnotations;
using YadetNare.Entity.Alarm;
using YadetNare.Entity.User;

namespace YadetNare.Entity.Activity;

public class ActivityEntity
{
    public int Id { get; set; }
    
    [Required]
    public string Title { get; set; }
    public string Description { get; set; }
    
    public int UserId { get; set; }
    public UserEntity User { get; set; }

    public List<AlarmEntity> Alarms { get; set; }
    
}

