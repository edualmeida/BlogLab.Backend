using System.Reflection;
using Bookmarks.Domain.Models.Bookmarks;
using Microsoft.EntityFrameworkCore;

namespace Bookmarks.Infrastructure.Persistence;
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
