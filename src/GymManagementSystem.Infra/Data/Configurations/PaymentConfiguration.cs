using GymManagementSystem.Domain.Members;
using GymManagementSystem.Domain.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementSystem.Infra.Data.Configurations;

public sealed class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => PaymentId.Of(value))
            .ValueGeneratedNever();

        builder.Property(x => x.MemberId)
            .HasConversion(
                id => id.Value,
                value => MemberId.Of(value))
            .IsRequired();

        builder.Property(x => x.MembershipId)
            .HasConversion(
                id => id.Value,
                value => MembershipId.Of(value))
            .IsRequired();

        builder.Property(x => x.Amount)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(x => x.Method)
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.PaidAt);

        builder.Property(x => x.TransactionId)
            .HasMaxLength(200);

        builder.HasIndex(x => x.MemberId);

        builder.HasIndex(x => x.MembershipId);

        builder.HasIndex(x => x.CreatedAt);

        builder.HasIndex(x => x.TransactionId)
            .IsUnique()
            .HasFilter("\"TransactionId\" IS NOT NULL");
    }
}
