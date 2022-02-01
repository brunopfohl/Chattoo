using Chattoo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chattoo.Infrastructure.Persistence.Configurations
{
    public class UserToCalendarEventConfiguration : IEntityTypeConfiguration<UserToCalendarEvent>
    {
        public void Configure(EntityTypeBuilder<UserToCalendarEvent> builder)
        {
            builder.HasKey(e => new { e.UserId, e.EventId });
            
            builder
                .HasOne<User>()
                .WithMany(e => e.JoinedEvents)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasOne<CalendarEvent>()
                .WithMany(e => e.Participants)
                .HasForeignKey(e => e.EventId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}