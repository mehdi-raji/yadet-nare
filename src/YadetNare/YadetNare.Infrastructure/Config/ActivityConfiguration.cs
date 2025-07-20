using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YadetNare.Domain.Activity;

namespace YadetNare.Infrastructure.Config;

public class ActivityConfiguration :IEntityTypeConfiguration<ActivityModel> 
{
    public void Configure(EntityTypeBuilder<ActivityModel> builder)
    {
        builder.Property(x=>x.Title).IsRequired().HasMaxLength(100);
        builder.Property(x=>x.Description).HasMaxLength(500);
    }
}