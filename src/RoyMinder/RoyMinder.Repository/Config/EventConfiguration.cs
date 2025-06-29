using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoyMinder.Data.User;

namespace RoyMinder.Repository.Config;

public class EventConfiguration :IEntityTypeConfiguration<Activity> 
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.Property(x=>x.Title).IsRequired().HasMaxLength(100);
        builder.Property(x=>x.Description).HasMaxLength(500);
    }
}