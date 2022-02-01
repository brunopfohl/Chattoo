using Chattoo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chattoo.Infrastructure.Persistence.Configurations
{
    public class UserToGroupConfiguration : IEntityTypeConfiguration<UserToGroup>
    {
        public void Configure(EntityTypeBuilder<UserToGroup> builder)
        {
            builder.HasKey(e => new { e.UserId, e.GroupId });
            
            builder
                .HasOne<User>()
                .WithMany(e => e.Groups)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder
                .HasOne<Group>()
                .WithMany(e => e.Participants)
                .HasForeignKey(e => e.GroupId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}