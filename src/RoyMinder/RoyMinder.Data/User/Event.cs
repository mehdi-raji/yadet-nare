using System.ComponentModel.DataAnnotations;

namespace RoyMinder.Data.User;

public class Event
{
    public int Id { get; set; }
    
    public required string Title { get; set; }
    public string Description { get; set; }
    
    public int ChatId { get; set; }
    public Chat Chat { get; set; }
    
}