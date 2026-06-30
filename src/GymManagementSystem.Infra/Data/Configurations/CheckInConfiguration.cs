using GymManagementSystem.Domain.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementSystem.Infra.Data.Configurations;

public sealed class CheckInConfiguration : IEntityTypeConfiguration<CheckIn>
{
    public void Configure(EntityTypeBuilder<CheckIn> builder)
    {
        builder.ToTable("CheckIns");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => CheckInId.Of(value))
            .ValueGeneratedNever();

        builder.Property(x => x.MemberId)
            .HasConversion(
                id => id.Value,
                value => MemberId.Of(value));

        builder.HasIndex(x => x.MemberId);

        builder.HasOne<Member>()
            .WithMany(m => m.CheckIns)
            .HasForeignKey(x => x.MemberId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
