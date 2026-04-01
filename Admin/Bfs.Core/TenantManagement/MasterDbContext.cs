using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Bfs.Core.TenantManagement;

public class MasterDbContext : DbContext
{
    public DbSet<TenantEntity> BfsTenant { get; set; }

    public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options)
    {
    }
}
