using Microsoft.AspNetCore.Mvc;
using Bfs.Core.Interfaces;
using Bfs.Core.Services.Auth;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data.Common;
using System.Runtime;
using System.Security.Claims;
using System.Text.Json;

namespace Bfs.Identity.Web.Pages
{
    public class IndexModel : PageModel
    {
        public List<Tenant> TenantList { get; set; } = new List<Tenant>();
        public List<TenantSystem> SystemList { get; set; } = new List<TenantSystem>();
        public List<Tenant> UserTenantList { get; set; } = new List<Tenant>();

        public bool ShowTenants = false;
        public bool ShowSystems = false;

        private readonly ITokenService _tokenService;
        private readonly ILogger<IndexModel> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public IndexModel(ITokenService tokenService, ILogger<IndexModel> logger,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager
            )
        {
            _tokenService = tokenService;
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;

            //Get Tenants from DB, //ToDo Get tenants from Cache.
            TenantList = Tenant.GetTenants().Result;
        }



        public void OnGet(System.Security.Claims.ClaimsPrincipal user)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var userClaims = User.Claims;     // Returns the user claims, a list of tenant id associated with this user (claim.Type = 'Tenant' , claim.Value = tenantId)
                if (userClaims.Any())
                {
                    ShowTenants = true;
                    UserTenantList = TenantList.Where(x => userClaims.Any(c => c.Type == "Tenant" && x.Id.ToString() == c.Value)).ToList();
                }
            }
        }

        public async Task<IActionResult> OnPostSelectTenantAsync(string tenantOrder)
        {
            var tenant = TenantList.FirstOrDefault(t => t.order.ToString() == tenantOrder);
            SystemList = await TenantSystem.GetTenantSystems(tenant.Id); //Todo use the tenant systems to show the system selection page if there are more than 1 system for the tenant, otherwise directly call the GetTenantSystemCookie and redirect to the system.
            ShowTenants = false;
            ShowSystems = true;

            var tenantId = tenant?.Id.ToString();
            var aspNetUserId = _userManager.GetUserId(User);

            if (tenant == null || aspNetUserId == null) return Page();

            // Generate the refresh token and set it in the cookie, Attach the cookie to the Response, so the frontend can use it to get access token and call APIs.
            // set systemId to 0 first, after user select system, call GetRefreshTokenCookie again to update the cookie with the selected systemId, so the frontend can use it to get access token and call APIs.
            _tokenService.GetRefreshTokenCookie(Response, Constants.RefreshTokenCookieName, tenant.Id, aspNetUserId, 0);
            return Page();
            //return Redirect("http://bfsfrontend.localhost/main/");
        }

        public async Task<IActionResult> OnPostSelectSystemAsync(long systemId)
        {
            var refreshToken = Request.Cookies[Constants.RefreshTokenCookieName];
            if (refreshToken == null)
                return Unauthorized(); //todo unauthenticated
            var ids = refreshToken?.Split('|');
            var tenantId = long.Parse(ids[0]);
            var aspNetUserId = ids[1];

            if (tenantId == 0 || aspNetUserId == null || systemId==0) return Page();

            // Generate the refresh token and set it in the cookie, Attach the cookie to the Response,
            // so the frontend can use it to get access token and call APIs.
            _tokenService.GetRefreshTokenCookie(Response, Constants.RefreshTokenCookieName, tenantId, aspNetUserId, systemId);

            // return Redirect("/main");
            return Redirect("http://localhost:4200");
        }
    }

    public class Tenant
    {
        public string order { get; set; } // UI selection, so the tenant id is not exposed to the user
        public long Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Logo { get; set; }
        public string DbConnection { get; set; }

        public static async Task<List<Tenant>> GetTenants()
        {
            var sqlSelect = "select * from bfsTenant";
            var sqlStatement = sqlSelect.ToString();

            var bfsDbConnection = "Server=localhost;Database=BestFit_V4; User Id=sa;Password=12Remember!; TrustServerCertificate=True";

            using var db = new SqlConnection(bfsDbConnection);
            var items = await db.QueryAsync<Tenant>(sqlSelect.ToString(), null);

            var i = 0;
            var tenantList = new List<Tenant>();
            foreach (var item in items)
            {
                i++;
                tenantList.Add(new Tenant() { order = i.ToString(), Id = item.Id, Name = item.Name, Logo = item.Logo, DbConnection = item.DbConnection });
            }

            return tenantList;
        }
    }

    public class TenantSystem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }

        public static async Task<List<TenantSystem>> GetTenantSystems(long tenantId)
        {
            var bfsDbConnection = "Server=localhost;Database=BestFit_V4; User Id=sa;Password=12Remember!; TrustServerCertificate=True";

            var sqlSelect = "select  s.* from bfsSystem s inner join bfsTenantSystem ts on ts.bfsSystemId = s.id";
            sqlSelect = sqlSelect + " where ts.BfsTenantId = @TenantId";
            var sqlStatement = sqlSelect.ToString();

            var sqlParameters = new DynamicParameters();
            sqlParameters.Add("@TenantId", tenantId); 

            using var db = new SqlConnection(bfsDbConnection);
            var items = await db.QueryAsync<Tenant>(sqlSelect.ToString(), sqlParameters);

            var i = 0;
            var list = new List<TenantSystem>();
            foreach (var item in items)
            {
                i++;
                list.Add(new TenantSystem() { Id = item.Id, Name = item.Name, Logo = item.Logo });
            }

            return list;
        }
    }

}
