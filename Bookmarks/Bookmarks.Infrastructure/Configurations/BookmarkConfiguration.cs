using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class BookmarkConfiguration : IEntityTypeConfiguration<Bookmark>
{
    public void Configure(EntityTypeBuilder<Bookmark> builder)
    {
        builder.HasKey(o => o.Id);
        
        builder.Property(o => o.Id)
            .ValueGeneratedOnAdd();

        builder
            .Property(o => o.CreatedBy)
            .IsRequired();

        builder
            .Property(o => o.ArticleId)
            .IsRequired();

        builder.HasIndex(p => new { p.CreatedBy, p.ArticleId }).IsUnique();

        builder.ToTable("Bookmarks");
    }
}