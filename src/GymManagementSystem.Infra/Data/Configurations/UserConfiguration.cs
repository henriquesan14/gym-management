using GymManagementSystem.Domain.Users;
using GymManagementSystem.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementSystem.Infra.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasConversion(
                id => id.Value,
                value => UserId.Of(value))
            .ValueGeneratedNever();

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(u => u.Role)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.Email)
            .HasConversion(
                e => e.Value,
                v => Email.Of(v))
            .HasColumnName("Email");

        builder.HasIndex(m => m.Email)
            .IsUnique();

        builder.Metadata
            .FindNavigation(nameof(User.RefreshTokens))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
