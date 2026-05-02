using Bfs.Core.Contracts.Auth;
using Bfs.Core.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Bfs.Identity.Web.Controllers
{
    [ApiController]
    [Route("identity/[controller]")]
    public class UserTenantController : ControllerBase
    {
        private readonly IAspNetUserTenantService _userTenantService;

        public UserTenantController(IAspNetUserTenantService userTenantService)
        {
            _userTenantService = userTenantService;
        }

        [HttpPost("link")]
        public async Task<IActionResult> LinkUserTenant([FromBody] UserTenantRequest request)
        {
            var result = await _userTenantService.AddTenantClaimAsync(request.UserId, request.TenantId);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Tenant claim linked successfully.");
        }

        [HttpPost("unlink")]
        public async Task<IActionResult> UnlinkUserTenant([FromBody] UserTenantRequest request)
        {
            var result = await _userTenantService.RemoveTenantClaimAsync(request.UserId, request.TenantId);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Tenant claim unlinked successfully.");
        }
    }
}
