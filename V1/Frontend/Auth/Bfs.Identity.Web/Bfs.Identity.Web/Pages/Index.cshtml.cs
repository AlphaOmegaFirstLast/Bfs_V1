using Bfs.Core.Interfaces;
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

        private readonly ILogger<IndexModel> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public IndexModel(ILogger<IndexModel> logger,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager
            )
        {
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

        // Mocking your service that gets the token
        private async Task<string> GetUserTenantTokenAsync(string tenantId)
        {
            await Task.Delay(1); // Simulate async work
            return "secure_token_abc123";
        }

        public async Task<IActionResult> OnPostSelectTenantAsync(string tenantOrder)
        {
            if (string.IsNullOrEmpty(tenantOrder)) return Page();

            var tenant = TenantList.FirstOrDefault(t => t.order.ToString() == tenantOrder);
            var aspNetUserId = _userManager.GetUserId(User);

            var authUserId = await Tenant.GetTenantUser(tenant, aspNetUserId);

            // 1. Call the function to get the token

            // string token = await GetUserTenantTokenAsync(tenantId);

            // 2. Store in a cookie
            // 'HttpOnly' prevents JS access, 'SameSite.Lax' allows it across your domain
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddHours(8),
                HttpOnly = true, // Set to true if the SPA doesn't need to read it via JS
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Path = "/" // Important for sharing across the domain
            };

            Response.Cookies.Append(Constants.RefreshTokenCookieName, $"'tenantId':{tenant.Id}-'aspNetUserId':{aspNetUserId}", options);

            // 3. Redirect to the SPA (assuming it's at /spa or a similar path)
            return Redirect("/main");
            // return Redirect("http://bfsfrontend.localhost/main/");
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

        //public static List<Tenant> Get()
        //{
        //    var x = "Server=localhost;Database=Tenant10; User Id=sa;Password=12Remember!; TrustServerCertificate=True";
        //    var apps = new List<Tenant>();
        //    apps.Add(new Tenant() {order=1,  Id = 686190962443871, Name = "Best Fit", Logo = "stockexbackoffice.svg", DbConnection = x});
        //    apps.Add(new Tenant() {order=2,  Id = 686348794702758, Name = "Ahmed Sami", Logo = "stockexfrontoffice.svg", DbConnection = x });
        //    apps.Add(new Tenant() {order=3,  Id = 686358956320974, Name = "Hani Ayad", Logo = "stockexadmin.svg", DbConnection = x });
        //    return apps;
        //}

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

        public static async Task<long> GetTenantUser(Tenant tenant, string aspNetUserId)
        {
            var bfsDbConnection = "Server=localhost;Database=Tenant10; User Id=sa;Password=12Remember!; TrustServerCertificate=True";
         //   var dbConnection = tenant.DbConnection;

            var sqlSelect = "select * from AuthUser where AspNetUserId = @AspNetUserId";
            var sqlStatement = sqlSelect.ToString();
            var parameters = new { AspNetUserId = aspNetUserId };

            using var db = new SqlConnection(bfsDbConnection);
            var items = await db.QueryAsync<Tenant>(sqlSelect.ToString(), parameters);

            var firstItem = items.FirstOrDefault();
            if (firstItem == null)
            {
                throw new InvalidOperationException("No AuthUser found for the given AspNetUserId.");
            }

            long id = firstItem.Id;
            return id; 
        }
    }
}
