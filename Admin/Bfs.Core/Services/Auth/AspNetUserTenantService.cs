using Bfs.Auth.Contracts;
using Bfs.Core.Config;
using Bfs.Core.Contracts;
using Bfs.Core.Contracts.Auth;
using Bfs.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Bfs.Core.Services.Auth
{

    public class AspNetUserTenantService : IAspNetUserTenantService
    {

        private readonly UserManager<IdentityUser> _userManager;

        public AspNetUserTenantService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Adds a "Tenant" claim to the specified user if it does not already exist.
        /// </summary>
        /// <param name="aspnetUserId">The ID of the AspNet user.</param>
        /// <param name="tenantId">The tenant ID to assign as a claim value.</param>
        /// <returns>IdentityResult indicating success or failure.</returns>
        public async Task<IdentityResult> AddTenantClaimAsync(string aspnetUserId, string tenantId)
        {
            //ToDo this endpoint must be secured to prevent unauthorized users from linking tenants to users. Consider implementing authorization policies or using a message handler that listens for user creation events and automatically links the tenant based on the event data.
            if (string.IsNullOrWhiteSpace(aspnetUserId))
                throw new ArgumentException("User ID cannot be null or empty.", nameof(aspnetUserId));

            if (string.IsNullOrWhiteSpace(tenantId))
                throw new ArgumentException("Tenant ID cannot be null or empty.", nameof(tenantId));

            // 1. Look up the user
            var user = await _userManager.FindByIdAsync(aspnetUserId);
            if (user == null)
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = $"User with ID '{aspnetUserId}' was not found."
                });

            // 2. Check whether the Tenant claim already exists
            var existingClaims = await _userManager.GetClaimsAsync(user);
            bool tenantClaimExists = existingClaims.Any(c =>
                c.Type == "Tenant" && c.Value == tenantId);

            if (tenantClaimExists)
                return IdentityResult.Success; // nothing to do

            // 3. Add the new Tenant claim
            var tenantClaim = new Claim("Tenant", tenantId);
            return await _userManager.AddClaimAsync(user, tenantClaim);
        }

        public async Task<IdentityResult> RemoveTenantClaimAsync(string aspnetUserId, string tenantId)
        {
            if (string.IsNullOrWhiteSpace(aspnetUserId))
                throw new ArgumentException("User ID cannot be null or empty.", nameof(aspnetUserId));
            if (string.IsNullOrWhiteSpace(tenantId))
                throw new ArgumentException("Tenant ID cannot be null or empty.", nameof(tenantId));
            // 1. Look up the user
            var user = await _userManager.FindByIdAsync(aspnetUserId);
            if (user == null)
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = $"User with ID '{aspnetUserId}' was not found."
                });
            // 2. Check whether the Tenant claim exists
            var existingClaims = await _userManager.GetClaimsAsync(user);
            var tenantClaim = existingClaims.FirstOrDefault(c =>
                c.Type == "Tenant" && c.Value == tenantId);
            if (tenantClaim == null)
                return IdentityResult.Success; // Idempotent — nothing to do
            // 3. Remove the Tenant claim

            return await _userManager.RemoveClaimAsync(user, tenantClaim);
        }
    }
}
