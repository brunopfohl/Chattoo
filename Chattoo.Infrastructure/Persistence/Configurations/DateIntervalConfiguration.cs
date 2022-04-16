using Chattoo.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chattoo.Infrastructure.Persistence.Configurations
{
    public class DateIntervalConfiguration : IEntityTypeConfiguration<DateInterval>
    {
        public void Configure(EntityTypeBuilder<DateInterval> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.StartsAt).IsRequired();
            
            builder.Property(e => e.EndsAt).IsRequired();
        }
    }
}