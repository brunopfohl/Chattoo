using System;
using Chattoo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chattoo.Infrastructure.Persistence.Configurations
{
    public class CalendarEventWishToCalendarEventTypeConfiguration : IEntityTypeConfiguration<CalendarEventWishToCalendarEventType>
    {
        public void Configure(EntityTypeBuilder<CalendarEventWishToCalendarEventType> builder)
        {
            builder.HasKey(e => new { e.TypeId, e.WishId });
            builder.HasOne<CalendarEventType>().WithMany().HasForeignKey(e => e.TypeId);
            builder.HasOne<CalendarEventWish>().WithMany().HasForeignKey(e => e.WishId);
        }
    }
}