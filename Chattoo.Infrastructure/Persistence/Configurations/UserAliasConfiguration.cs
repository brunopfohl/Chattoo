using Chattoo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chattoo.Infrastructure.Persistence.Configurations
{
    public class UserAliasConfiguration : IEntityTypeConfiguration<UserAlias>
    {
        public void Configure(EntityTypeBuilder<UserAlias> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();
            
            builder.Property(e => e.Alias)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
