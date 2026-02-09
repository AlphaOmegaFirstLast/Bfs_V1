using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Bfs.Core.Data;
using [TemplateSln].Web.Models;

namespace [TemplateSln].Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<AuthUser, AuthRole, long>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public override int SaveChanges()
        {
            AssignUserId();
            AssignRoleId();

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AssignUserId();
            AssignRoleId();

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void AssignUserId()
        {
            var newUsers = ChangeTracker.Entries<AuthUser>()
                .Where(e => e.State == EntityState.Added);

            foreach (var entry in newUsers)
            {
                entry.Entity.Id = IdGenerator.GetId();
            }
        }

        private void AssignRoleId()
        {
            var newRoles = ChangeTracker.Entries<AuthRole>()
                .Where(e => e.State == EntityState.Added);

            foreach (var entry in newRoles)
            {
                entry.Entity.Id = IdGenerator.GetId();
            }
        }
    }
}

