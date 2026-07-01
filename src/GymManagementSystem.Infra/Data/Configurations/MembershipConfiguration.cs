using GymManagementSystem.Domain.Members;
using GymManagementSystem.Domain.MembershipPlans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementSystem.Infra.Data.Configurations;

public sealed class MembershipConfiguration : IEntityTypeConfiguration<Membership>
{
    public void Configure(EntityTypeBuilder<Membership> builder)
    {
        builder.ToTable("Memberships");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .HasConversion(
                id => id.Value,
                value => MembershipId.Of(value))
            .ValueGeneratedNever();

        builder.Property(x => x.MembershipPlanId)
            .HasConversion(
                id => id.Value,
                value => MembershipPlanId.Of(value))
            .IsRequired();

        builder.Property(m => m.StartDate)
            .IsRequired();

        builder.Property(m => m.EndDate)
            .IsRequired();

        builder.Property(m => m.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.HasOne<MembershipPlan>()
            .WithMany()
            .HasForeignKey(x => x.MembershipPlanId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.MembershipPlanId);
    }
}
