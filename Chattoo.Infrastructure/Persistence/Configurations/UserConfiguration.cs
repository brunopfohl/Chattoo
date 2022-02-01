using Chattoo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chattoo.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.UserName).IsRequired().HasMaxLength(150);

            builder
                .HasMany(e => e.Aliases)
                .WithOne()
                .HasForeignKey(e => e.UserId);

            builder
                .HasMany(e => e.GroupRoles)
                .WithMany(e => e.Users);
            
            builder
                .HasMany(e => e.ChannelRoles)
                .WithMany(e => e.Users);
        }
    }
}
