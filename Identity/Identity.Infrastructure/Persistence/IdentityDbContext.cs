using System.Reflection;
using Identity.Domain;
using Identity.Domain.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Persistence;
internal class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : 
    IdentityDbContext<User, Role, Guid>(options)
{
    public new DbSet<User> Users { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}