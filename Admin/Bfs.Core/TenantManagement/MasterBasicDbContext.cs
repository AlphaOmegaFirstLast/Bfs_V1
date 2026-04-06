using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Bfs.Core.TenantManagement;

public class MasterBasicDbContext : DbContext
{
    public DbSet<TenantEntity> BfsTenant { get; set; }
    public DbSet<SystemEntity> BfsSystem { get; set; }
    public DbSet<TenantSystemEntity> BfsTenantSystem { get; set; }

    public MasterBasicDbContext(DbContextOptions<MasterBasicDbContext> options) : base(options)
    {
    }
}

public class TenantEntity
{
    public long TenantId { get; set; }
    public string order { get; set; } // UI selection, so the tenant id is not exposed to the user
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Logo { get; set; } = string.Empty;
    public string DbConnection { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;
}

public class SystemEntity
{
    public bool IsDeleted { get; set; } = false;
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class TenantSystemEntity
{
    public bool IsDeleted { get; set; } = false;
    public long Id { get; set; }
    public long BfsTenantId { get; set; }
    public long BfsSystemId { get; set; }
}
