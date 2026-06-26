using GymManagementSystem.Domain.Members;
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

        // Email is a value object mapped to a single column with a unique index.
        builder.OwnsOne(m => m.Email, email =>
        {
            email.Property(e => e.Value)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(256);

            email.HasIndex(e => e.Value)
                .IsUnique();
        });

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
