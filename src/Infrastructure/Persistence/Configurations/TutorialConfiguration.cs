using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Persistence.Configurations
{
    public class TutorialConfiguration : IEntityTypeConfiguration<Tutorial>
    {
        public void Configure(EntityTypeBuilder<Tutorial> builder)
        {
            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(c => c.HtmlContent)
                .IsRequired();
            builder.Property(g => g.GroupId)
                .IsRequired();
            builder.Property(l => l.LanguageToolId)
                .IsRequired();
            builder.Property(cr => cr.Created)
                .IsRequired();
            builder.Property(crb => crb.CreatedBy)
                .IsRequired();
        }
    }

    public class TutorialImageConfiguration : IEntityTypeConfiguration<TutorialImage>
    {
        public void Configure(EntityTypeBuilder<TutorialImage> builder)
        {
            builder.Property(n => n.Name)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(p => p.Path)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(p => p.IsPrimary)
                .IsRequired();
            builder.Property(t => t.TutorialId)
                .IsRequired();
        }
    }

    public class GroupTutorialConfiguration : IEntityTypeConfiguration<GroupTutorial>
    {
        public void Configure(EntityTypeBuilder<GroupTutorial> builder)
        {
            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(cr => cr.Created)
                .IsRequired();
            builder.Property(crb => crb.CreatedBy)
                .IsRequired();
        }
    }
}
