using Bfs.Auth.Contracts;
using Bfs.Core.Contracts.Auth;
using Bfs.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Bfs.Core.Services.Auth
{
    public interface IAspNetUserTenantService
    {
        Task<IdentityResult> AddTenantClaimAsync(string aspnetUserId, string tenantId);
        Task<IdentityResult> RemoveTenantClaimAsync(string aspnetUserId, string tenantId);
    }
}