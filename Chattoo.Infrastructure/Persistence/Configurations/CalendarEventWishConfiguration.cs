using Chattoo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chattoo.Infrastructure.Persistence.Configurations
{
    public class CalendarEventWishConfiguration : IEntityTypeConfiguration<CalendarEventWish>
    {
        public void Configure(EntityTypeBuilder<CalendarEventWish> builder)
        {
            builder
                .HasMany(e => e.DateIntervals)
                .WithOne()
                .HasForeignKey(e => e.CalendarEventWishId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.AuthorId);
            
            builder
                .HasOne<Group>()
                .WithMany()
                .HasForeignKey(e => e.GroupId);

            builder
                .HasOne<CommunicationChannel>()
                .WithMany()
                .HasForeignKey(e => e.CommunicationChannelId);
            
            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();
            
            builder
                .HasOne<CalendarEvent>()
                .WithMany()
                .HasForeignKey(e => e.CalendarEventId);
            
            builder.HasKey(e => e.Id);
            
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();
            
            builder.Property(e => e.AuthorId)
                .IsRequired();
        }
    }
}