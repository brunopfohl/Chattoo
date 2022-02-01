using Chattoo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chattoo.Infrastructure.Persistence.Configurations
{
    public class CalendarEventWishConfiguration : IEntityTypeConfiguration<CalendarEventWish>
    {
        public void Configure(EntityTypeBuilder<CalendarEventWish> builder)
        {
            // builder.OwnsMany(m => m.DateIntervals, a =>
            // {
            //     a.Property(p => p.StartsAt).IsRequired()
            //         .HasColumnName("StartsAt");
            //
            //     a.Property(p => p.EndsAt).IsRequired()
            //         .HasColumnName("EndsAt");
            // }).HasNoKey();

            builder.OwnsMany(e => e.DateIntervals);
            
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