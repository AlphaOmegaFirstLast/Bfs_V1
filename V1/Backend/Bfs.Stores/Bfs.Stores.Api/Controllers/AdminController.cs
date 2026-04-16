using Bfs.Stores.Data;
using Bfs.Core.Config;
using Bfs.Core.Middleware;
using Bfs.Core.TenantManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Bfs.Stores.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]

public class AdminController
{
    private readonly BfsSettings _bfsSettings;
    public AdminController(
        IOptions<BfsSettings> bfsSettings
        )
    {
        _bfsSettings = bfsSettings.Value;
    }

    [HttpPost("Migrate")]
   // [CustomAuthorize("method=r.migrate")]
    public async Task MigrateTenants()
    {
        var masterConnection = _bfsSettings?.DbConnections?.MasterConnection;
        await TenantManager.ApplyMigrations< StoresDbContext >(masterConnection, "Stores");
    }
}

