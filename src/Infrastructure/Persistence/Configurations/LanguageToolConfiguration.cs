using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Persistence.Configurations
{
    public class LanguageToolConfiguration : IEntityTypeConfiguration<LanguageTool>
    {
        public void Configure(EntityTypeBuilder<LanguageTool> builder)
        {
            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(i => i.ImagePath)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(d => d.Description)
                .IsRequired()
                .HasMaxLength(1000);
            builder.Property(t => t.TechnologyId)
                .IsRequired();
            builder.Property(g => g.GroupId)
                .IsRequired();
        }
    }

    public class GroupLanguageToolConfiguration : IEntityTypeConfiguration<GroupLanguageTool>
    {
        public void Configure(EntityTypeBuilder<GroupLanguageTool> builder)
        {
            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
