using GymManagementSystem.Domain.Members;
using GymManagementSystem.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GymManagementSystem.Infra.Data;

public class GymManagementDbContext : DbContext
{
    public GymManagementDbContext(DbContextOptions<GymManagementDbContext> options)
    : base(options)
    {
    }

    public DbSet<Member> Members => Set<Member>();
    public DbSet<Membership> Memberships => Set<Membership>();
    public DbSet<User> Users => Set<User>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<CheckIn> CheckIns => Set<CheckIn>();

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
