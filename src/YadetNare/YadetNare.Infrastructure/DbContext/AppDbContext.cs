using System.Reflection;
using Microsoft.EntityFrameworkCore;
using YadetNare.Entity.Activity;
using YadetNare.Persistence.Alarm;

namespace YadetNare.Persistence.DbContext
{
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
}