using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
            .HasMaxLength(ArticleModelConstants.Article.MaxTitleLength);

        builder
            .Property(p => p.Subtitle)
            .IsRequired()
            .HasMaxLength(ArticleModelConstants.Article.MaxSubtitleLength);

        builder
            .Property(p => p.Text)
            .IsRequired()
            .HasMaxLength(ArticleModelConstants.Article.MaxTextLength);

        builder
            .HasOne(p => p.Category);

        builder
            .HasOne(p => p.Thumbnail);
    }
}
