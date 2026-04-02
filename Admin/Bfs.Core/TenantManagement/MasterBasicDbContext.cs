using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Bfs.Core.TenantManagement;

public class MasterBasicDbContext : DbContext
{
    public DbSet<TenantEntity> BfsTenant { get; set; }

    public MasterBasicDbContext(DbContextOptions<MasterBasicDbContext> options) : base(options)
    {
    }
}
