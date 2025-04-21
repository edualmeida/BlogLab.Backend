using Bookmarks.Domain.Models.Bookmarks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookmarks.Infrastructure.Configurations;
internal class BookmarkConfiguration : IEntityTypeConfiguration<Bookmark>
{
    public void Configure(EntityTypeBuilder<Bookmark> builder)
    {
        builder.HasKey(o => o.Id);
        
        builder.Property(o => o.Id)
            .ValueGeneratedOnAdd();

        builder
            .Property(o => o.UserId)
            .IsRequired();

        builder
            .Property(o => o.ArticleId)
            .IsRequired();

        builder.HasIndex(p => new { p.UserId, p.ArticleId, p.Enabled }).IsUnique();

        builder.ToTable("Bookmarks");
    }
}