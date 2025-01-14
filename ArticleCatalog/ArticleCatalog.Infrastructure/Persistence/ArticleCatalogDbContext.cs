using System.Reflection;
using Microsoft.EntityFrameworkCore;

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
