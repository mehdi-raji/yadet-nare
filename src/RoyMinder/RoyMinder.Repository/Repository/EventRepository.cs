using RoyMinder.Repository.DbContext;

namespace RoyMinder.Repository.Repository;

public class EventRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;
    
    
    
}