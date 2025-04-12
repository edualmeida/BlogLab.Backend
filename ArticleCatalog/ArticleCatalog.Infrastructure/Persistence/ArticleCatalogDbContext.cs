using System.Reflection;
using ArticleCatalog.Domain.Models.Articles;
using ArticleCatalog.Domain.Models.Categories;
using ArticleCatalog.Domain.Models.Thumbnails;
using Microsoft.EntityFrameworkCore;

namespace ArticleCatalog.Infrastructure.Persistence;
internal class ArticleCatalogDbContext : BaseDbContext<ArticleCatalogDbContext>
{
    public ArticleCatalogDbContext(
        DbContextOptions<ArticleCatalogDbContext> options)
        : base(options)
    {
    }

    public DbSet<Article> Articles { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<Thumbnail> Thumbnails { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
