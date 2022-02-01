using Chattoo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chattoo.Infrastructure.Persistence.Configurations
{
    public class CommunicationChannelConfiguration : IEntityTypeConfiguration<CommunicationChannel>
    {
        public void Configure(EntityTypeBuilder<CommunicationChannel> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();
            
            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.Description)
                .HasMaxLength(255);

            builder
                .HasMany(e => e.Messages)
                .WithOne()
                .HasForeignKey(e => e.ChannelId);
            
            builder
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.ChannelId);
        }
    }
}
