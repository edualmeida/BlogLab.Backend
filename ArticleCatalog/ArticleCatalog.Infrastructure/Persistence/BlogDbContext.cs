using System.Reflection;
using Microsoft.EntityFrameworkCore;

internal class BlogDbContext : BaseDbContext<BlogDbContext>
{
    public BlogDbContext(
        DbContextOptions<BlogDbContext> options)
        : base(options)
    {
    }

    public DbSet<Article> Articles { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<Color> Colors { get; set; } = default!;
    public DbSet<Thumbnail> Thumbnails { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
