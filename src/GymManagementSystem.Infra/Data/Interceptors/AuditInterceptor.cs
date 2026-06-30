using GymManagementSystem.Domain.Abstractions;
using GymManagementSystem.Domain.AuditLogs;
using GymManagementSystem.Domain.Enums;
using GymManagementSystem.Domain.Users.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text.Json;

namespace GymManagementSystem.Infra.Data.Interceptors;

public sealed class AuditInterceptor : SaveChangesInterceptor
{
    private readonly IUserContext userContext;

    public AuditInterceptor(IUserContext userContext)
    {
        this.userContext = userContext;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;

        if (context is null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);

        AddAuditLogs(context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void AddAuditLogs(DbContext context)
    {
        var entries = context.ChangeTracker.Entries()
            .Where(e =>
                e.State is EntityState.Added
                or EntityState.Modified
                or EntityState.Deleted)
            .Where(e => e.Entity is IAuditableEntity &&
                e.Entity is not AuditLog)
            .ToList();

        var auditLogs = new List<AuditLog>();

        foreach (var entry in entries)
        {
            auditLogs.Add(CreateAuditLog(entry));
        }

        context.Set<AuditLog>().AddRange(auditLogs);
    }

    private AuditLog CreateAuditLog(EntityEntry entry)
    {
        var entityName = entry.Metadata.ClrType.Name;
        var tableName = entry.Metadata.GetTableName();
        var entityId = GetPrimaryKey(entry);

        var action = entry.State switch
        {
            EntityState.Added => AuditAction.Create,
            EntityState.Modified => AuditAction.Update,
            EntityState.Deleted => AuditAction.Delete,
            _ => throw new InvalidOperationException()
        };

        var oldValues = entry.State == EntityState.Modified || entry.State == EntityState.Deleted
            ? Serialize(entry.OriginalValues)
            : null;

        var newValues = entry.State == EntityState.Modified || entry.State == EntityState.Added
            ? Serialize(entry.CurrentValues)
            : null;

        return new AuditLog(
            AuditLogId.New(),
            tableName!,
            entityName,
            entityId,
            action,
            oldValues,
            newValues,
            userContext.UserId,
            userContext.IpAddress
        );
    }

    private static string GetPrimaryKey(EntityEntry entry)
    {
        var key = entry.Properties.FirstOrDefault(p => p.Metadata.IsPrimaryKey());

        return key?.CurrentValue?.ToString() ?? string.Empty;
    }

    private static string Serialize(PropertyValues values)
    {
        var dict = values.Properties.ToDictionary(
            p => p.Name,
            p => values[p]);

        return JsonSerializer.Serialize(dict);
    }
}
