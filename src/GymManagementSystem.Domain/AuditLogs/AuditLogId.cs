using GymManagementSystem.Domain.Exceptions;

namespace GymManagementSystem.Domain.AuditLogs;

public class AuditLogId
{
    public Guid Value { get; }

    private AuditLogId(Guid value) => Value = value;

    public static AuditLogId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("AuditLogId cannot be empty.");
        }
        return new AuditLogId(value);
    }
}
