using GymManagementSystem.Domain.Abstractions;
using GymManagementSystem.Domain.Enums;
using GymManagementSystem.Domain.Users;

namespace GymManagementSystem.Domain.AuditLogs;

public class AuditLog : Entity<AuditLogId>
{
    private AuditLog() { }
    public AuditLog(AuditLogId id, string tableName, string entityName, string entityId, AuditAction action,
        string? oldValues, string? newValue, Guid? userId, string? ipAddress)
    {
        Id = id;
        TableName = tableName;
        EntityName = entityName;
        EntityId = entityId;
        Action = action;
        OldValues = oldValues;
        NewValues = newValue;
        UserId = userId.HasValue ? UserId.Of(userId.Value) : null;
        IpAddress = ipAddress;
        CreatedAt = DateTime.Now;
    }
    public string TableName { get; private set; } = default!;
    public string EntityName { get; private set; } = default!;
    public string EntityId { get; private set; } = default!;
    public AuditAction Action { get; private set; } = default!;

    public string? OldValues { get; private set; }
    public string? NewValues { get; private set; }

    public UserId? UserId { get; private set; }
    public string? IpAddress { get; private set; }
}
