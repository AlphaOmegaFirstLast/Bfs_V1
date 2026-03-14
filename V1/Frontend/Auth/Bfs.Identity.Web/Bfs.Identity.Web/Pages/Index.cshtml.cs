using Bfs.Core.Interfaces;
using Bfs.Core.Services.Auth;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public List<Tenant> UserTenantList { get; set; } = new List<Tenant>();

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
                    // TenantList = Tenant.Get();
                    UserTenantList = TenantList.Where(x => userClaims.Any(c => c.Type == "Tenant" && x.Id.ToString() == c.Value)).ToList();
                }
            }
        }

        public async Task<IActionResult> OnPostSelectTenantAsync(string tenantOrder)
        {
            if (string.IsNullOrEmpty(tenantOrder)) return Page();

            var tenant = TenantList.FirstOrDefault(t => t.order.ToString() == tenantOrder);
            var aspNetUserId = _userManager.GetUserId(User);

            if (tenant== null || aspNetUserId==null) return Page();

            _tokenService.AttachRefreshTokenCookie(Response, Constants.RefreshTokenCookieName, tenant.Id, aspNetUserId);

            // return Redirect("/main");
            return Redirect("http://localhost:4200/");
            //return Redirect("http://bfsfrontend.localhost/main/");
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
}
