using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YadetNare.Entity.User;

namespace YadetNare.Persistence.Config;

public class EventConfiguration :IEntityTypeConfiguration<Activity> 
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.Property(x=>x.Title).IsRequired().HasMaxLength(100);
        builder.Property(x=>x.Description).HasMaxLength(500);
    }
}