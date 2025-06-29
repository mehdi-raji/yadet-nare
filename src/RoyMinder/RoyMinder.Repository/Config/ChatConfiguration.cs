using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoyMinder.Data.User;

namespace RoyMinder.Repository.Config;

public class ChatConfiguration :IEntityTypeConfiguration<User> 
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        
    }
}