using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<AppDataUser>
    {
        public void Configure(EntityTypeBuilder<AppDataUser> builder)
        {
            builder.Property(n => n.FirstName)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(l => l.LastName)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(m => m.MiddleName)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
