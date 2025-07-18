using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YadetNare.Domain.Activity;

namespace YadetNare.Infrastructure.Config;

public class ActivityConfiguration :IEntityTypeConfiguration<ActivityEntity> 
{
    public void Configure(EntityTypeBuilder<ActivityEntity> builder)
    {
        builder.Property(x=>x.Title).IsRequired().HasMaxLength(100);
        builder.Property(x=>x.Description).HasMaxLength(500);
    }
}