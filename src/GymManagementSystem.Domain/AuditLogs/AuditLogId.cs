using GymManagementSystem.Domain.Exceptions;

namespace GymManagementSystem.Domain.AuditLogs;

public sealed record AuditLogId(Guid Value)
{
    public static AuditLogId Of(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("AuditLogId cannot be empty.");

        return new(value);
    }

    public static AuditLogId New() =>
        new(Guid.NewGuid());
}
