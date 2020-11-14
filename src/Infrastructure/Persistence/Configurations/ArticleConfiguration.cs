using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Persistence.Configurations
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.Property(e => e.Title)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(e => e.Description)
                .HasMaxLength(290)
                .IsRequired();
            builder.Property(e => e.HtmlContent)
                .IsRequired();
            builder.Property(e => e.IsActive)
                .IsRequired();
        }
    }

    public class ArticleImageConfiguration : IEntityTypeConfiguration<ArticleImage>
    {
        public void Configure(EntityTypeBuilder<ArticleImage> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(e => e.Path)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(e => e.IsPrimary)
                .IsRequired();
            builder.Property(e => e.ArticleId)
                .IsRequired();
        }
    }
}
