using GymManagementSystem.Domain.Members;
using GymManagementSystem.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementSystem.Infra.Data.Configurations;

public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("Members");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .HasConversion(
                id => id.Value,
                value => MemberId.Of(value))
            .ValueGeneratedNever();

        builder.Property(m => m.FullName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(u => u.Email)
            .HasConversion(
                e => e.Value,
                v => Email.Of(v))
            .HasColumnName("Email");

        builder.HasIndex(m => m.Email)
            .IsUnique();

        // Member is the aggregate root; Memberships is a private backing-field collection.
        builder.HasMany(m => m.Memberships)
            .WithOne()
            .HasForeignKey("MemberId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Metadata
            .FindNavigation(nameof(Member.Memberships))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
