using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RoyMinder.Data.User;

namespace RoyMinder.Repository.DbContext
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : Microsoft.EntityFrameworkCore.DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<User> User { get; set; }
        public DbSet<Activity> Activity { get; set; }
        public DbSet<Alarm> Alarm { get; set; }
    }
}