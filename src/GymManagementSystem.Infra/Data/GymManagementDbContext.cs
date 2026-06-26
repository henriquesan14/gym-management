using GymManagementSystem.Domain.Members;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GymManagementSystem.Infra.Data;

public class GymManagementDbContext : DbContext
{
    public GymManagementDbContext(DbContextOptions<GymManagementDbContext> options)
    : base(options)
    {
    }

    public DbSet<Member> Tenants => Set<Member>();
    public DbSet<Membership> Clients => Set<Membership>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                {
                    property.SetColumnType("timestamp");
                }
            }
        }

        foreach (var ownedType in builder.Model.GetEntityTypes().Where(t => t.IsOwned()))
        {
            foreach (var property in ownedType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                {
                    property.SetColumnType("timestamp");
                }
            }
        }

        base.OnModelCreating(builder);
    }
}
