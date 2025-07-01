using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YadetNare.Entity.User;

namespace YadetNare.Persistence.Config;

public class ChatConfiguration :IEntityTypeConfiguration<User> 
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        
    }
}