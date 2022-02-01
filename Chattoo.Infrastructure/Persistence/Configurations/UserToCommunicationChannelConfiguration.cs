using Chattoo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chattoo.Infrastructure.Persistence.Configurations
{
    public class UserToCommunicationChannelConfiguration : IEntityTypeConfiguration<UserToCommunicationChannel>
    {
        public void Configure(EntityTypeBuilder<UserToCommunicationChannel> builder)
        {
            builder.HasKey(e => new { e.UserId, e.ChannelId });
            
            builder
                .HasOne<User>()
                .WithMany(e => e.Channels)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder
                .HasOne<CommunicationChannel>()
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.ChannelId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}