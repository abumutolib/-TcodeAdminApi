using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Persistence.Configurations
{
    public class TechnologyConfiguration : IEntityTypeConfiguration<Technology>
    {
        public void Configure(EntityTypeBuilder<Technology> builder)
        {
            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(t => t.Text)
                .IsRequired()
                .HasMaxLength(2048);
            builder.Property(i => i.ImagePath)
                .HasMaxLength(255);
        }
    }
}
