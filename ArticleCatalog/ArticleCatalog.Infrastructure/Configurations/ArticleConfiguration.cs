using ArticleCatalog.Domain.Models.Articles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArticleCatalog.Infrastructure.Configurations;
internal class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder
            .HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder
            .Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(ArticleModelConstants.MaxTitleLength);

        builder
            .Property(p => p.Subtitle)
            .IsRequired()
            .HasMaxLength(ArticleModelConstants.MaxSubtitleLength);

        builder
            .Property(p => p.Text)
            .IsRequired()
            .HasMaxLength(ArticleModelConstants.MaxTextLength);

        builder
            .HasOne(p => p.Category);

        builder
            .HasOne(p => p.Thumbnail);
    }
}
