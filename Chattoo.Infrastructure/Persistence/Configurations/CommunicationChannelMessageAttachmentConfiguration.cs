using Chattoo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chattoo.Infrastructure.Persistence.Configurations
{
    public class CommunicationChannelMessageAttachmentConfiguration : IEntityTypeConfiguration<CommunicationChannelMessageAttachment>
    {
        public void Configure(EntityTypeBuilder<CommunicationChannelMessageAttachment> builder)
        {
            builder.Property(e => e.MessageId)
                .IsRequired();

            builder.Property(e => e.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(e => e.Content)
                .IsRequired();

            builder.Property(e => e.Type)
                .IsRequired();
        }
    }
}
