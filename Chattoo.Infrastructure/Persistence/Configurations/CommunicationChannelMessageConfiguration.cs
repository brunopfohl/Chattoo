using Chattoo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chattoo.Infrastructure.Persistence.Configurations
{
    public class CommunicationChannelMessageConfiguration : IEntityTypeConfiguration<CommunicationChannelMessage>
    {
        public void Configure(EntityTypeBuilder<CommunicationChannelMessage> builder)
        {
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();
            
            builder.Property(e => e.ChannelId)
                .IsRequired();
            
            builder.Property(e => e.UserId)
                .IsRequired();

            builder.Property(e => e.Content)
                .HasMaxLength(1000)
                .IsRequired();
        }
    }
}
