using Chattoo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chattoo.Infrastructure.Persistence.Configurations
{
    public class CommunicationChannelCalendarEventConfiguration : IEntityTypeConfiguration<CommunicationChannelCalendarEvent>
    {
        public void Configure(EntityTypeBuilder<CommunicationChannelCalendarEvent> builder)
        {
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();
            
            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();
            
            builder.Property(e => e.StartsAt)
                .IsRequired();
        }
    }
}