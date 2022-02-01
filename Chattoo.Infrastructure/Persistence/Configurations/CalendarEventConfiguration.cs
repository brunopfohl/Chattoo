using Chattoo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chattoo.Infrastructure.Persistence.Configurations
{
    public class CalendarEventConfiguration : IEntityTypeConfiguration<CalendarEvent>
    {
        public void Configure(EntityTypeBuilder<CalendarEvent> builder)
        {
            // builder.OwnsOne(m => m.Address, a =>
            // {
            //     a.Property(p => p.Name).HasMaxLength(150)
            //         .HasColumnName("Name")
            //         .HasDefaultValue("");
            //     a.Property(p => p.Country).HasMaxLength(150)
            //         .HasColumnName("Country")
            //         .HasDefaultValue("");
            //     a.Property(p => p.City).HasMaxLength(150)
            //         .HasColumnName("City")
            //         .HasDefaultValue("");
            //     a.Property(p => p.Street).HasMaxLength(150)
            //         .HasColumnName("Street")
            //         .HasDefaultValue("");
            //     a.Property(p => p.StreetNumber).HasMaxLength(12)
            //         .HasColumnName("StreetNumber")
            //         .HasDefaultValue("");
            // }).HasNoKey();

            builder.OwnsOne(e => e.Address);

            builder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.AuthorId);
            
            builder.HasKey(e => e.Id);
            
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();
            
            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();
            
            builder.Property(e => e.StartsAt)
                .IsRequired();
            
            builder.Property(e => e.AuthorId)
                .IsRequired();
        }
    }
}