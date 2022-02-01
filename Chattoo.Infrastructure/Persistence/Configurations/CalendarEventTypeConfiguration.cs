using Chattoo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chattoo.Infrastructure.Persistence.Configurations
{
    public class CalendarEventTypeConfiguration : IEntityTypeConfiguration<CalendarEventType>
    {
        public void Configure(EntityTypeBuilder<CalendarEventType> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();
            
            builder.Property(e => e.Name)
                .IsRequired();
        }
    }
}