using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace RoyMinder.Data.User;

public class Activity
{
    public int Id { get; set; }
    
    public required string Title { get; set; }
    public string Description { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }

    public List<Alarm> Alarms { get; set; }
    
}

public class Alarm
{
    public int Id { get; set; }
    public required DateTime Time { get; set; }
    
    public int ActivityId { get; set; }
    public Activity Activity { get; set; }
    
}