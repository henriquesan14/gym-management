using GymManagementSystem.Domain.AuditLogs;
using GymManagementSystem.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementSystem.Infra.Data.Configurations;

public sealed class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("AuditLogs");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => AuditLogId.Of(value))
            .ValueGeneratedNever();

        builder.Property(x => x.TableName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.EntityName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.EntityId)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Action)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.OldValues)
            .HasColumnType("jsonb");

        builder.Property(x => x.NewValues)
            .HasColumnType("jsonb");

        builder.Property(x => x.IpAddress)
            .HasMaxLength(45);

        builder.Property(x => x.UserId)
            .HasConversion(
                id => id == null ? (Guid?)null : id.Value,
                value => value == null ? null : UserId.Of(value.Value))
            .IsRequired(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.TableName);
        builder.HasIndex(x => x.EntityName);
        builder.HasIndex(x => x.EntityId);
        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.CreatedAt);
    }
}
