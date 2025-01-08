using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.HasIndex(p => p.Email)
            .IsUnique();

        builder.Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(UserModelConstants.User.MaxEmailLength);

        builder.Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(UserModelConstants.User.MaxNameLength);

        builder.Property(p => p.MiddleName)
            .HasMaxLength(UserModelConstants.User.MaxNameLength);

        builder.Property(p => p.LastName)
            .IsRequired()
            .HasMaxLength(UserModelConstants.User.MaxNameLength);

        builder.Property(p => p.PasswordHash)
            .IsRequired()
            .HasMaxLength(UserModelConstants.User.MaxPasswordLength);

        builder
            .HasMany(p => p.Roles)
            .WithMany(s => s.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UserRole",
                j => j.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId"));
    }
}
