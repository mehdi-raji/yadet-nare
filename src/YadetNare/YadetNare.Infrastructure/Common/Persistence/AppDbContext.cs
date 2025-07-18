using System.Reflection;
using Microsoft.EntityFrameworkCore;
using YadetNare.Domain.Activity;
using YadetNare.Domain.Alarm;

namespace YadetNare.Infrastructure.Common.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : Microsoft.EntityFrameworkCore.DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<ActivityEntity> Activity { get; set; }
    public DbSet<AlarmEntity> Alarm { get; set; }
}