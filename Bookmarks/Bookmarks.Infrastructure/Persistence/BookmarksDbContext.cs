using System.Reflection;
using Microsoft.EntityFrameworkCore;

internal class BookmarksDbContext : BaseDbContext<BookmarksDbContext>
{
    public BookmarksDbContext(
        DbContextOptions<BookmarksDbContext> options)
        : base(options)
    {
    }

    public DbSet<Bookmark> Bookmarks { get; set; } = default!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
