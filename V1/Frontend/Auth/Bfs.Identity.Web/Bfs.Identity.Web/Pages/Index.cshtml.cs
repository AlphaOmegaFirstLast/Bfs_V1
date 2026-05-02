using Bfs.Auth.Client;
using Bfs.Auth.Contracts;
using Bfs.Core.Config;
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
using static Dapper.SqlMapper;

namespace Bfs.Identity.Web.Pages
{
    public class IndexModel : PageModel
    {
        public List<Tenant> TenantList { get; set; } = new List<Tenant>();
        public List<TenantSystem> SystemList { get; set; } = new List<TenantSystem>();
        public List<Tenant> UserTenantList { get; set; } = new List<Tenant>();
        public List<Tenant> NewTenantList { get; set; } = new List<Tenant>();

        public bool ShowTenants = false;
        public bool ShowSystems = false;
        public bool ShowNewTenants = false;
        public bool ShowRequestSentConfirmation = false;

        private readonly string _masterConnection;
        private readonly BfsSettings _settings;
        private readonly ITokenService _tokenService;
        private readonly ILogger<IndexModel> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AuthClient _authClient;

        public IndexModel(
            IOptions<BfsSettings> settings,
            ITokenService tokenService, ILogger<IndexModel> logger,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            AuthClient authClient)
        {
            _settings = settings.Value;
            _tokenService = tokenService;
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;

            _masterConnection = _settings?.DbConnections?.MasterConnection;
            //Get Tenants from DB, //ToDo Get tenants from Cache.
            TenantList = Tenant.GetTenants(_masterConnection).Result;
            _authClient = authClient;
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

        public async Task<IActionResult> OnPostFindNewTenantAsync()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var userClaims = User.Claims;     // Returns list of tenants that the user is not associated with,
                if (userClaims.Any())
                {
                    ShowTenants = false;
                    ShowNewTenants = true;
                    NewTenantList = TenantList.Where(x => !userClaims.Any(c => c.Type == "Tenant" && x.Id.ToString() == c.Value)).ToList();
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostRequestTenantAsync(string newTenantOrder, string dummy)
        {
            var tenant = TenantList.FirstOrDefault(t => t.order.ToString() == newTenantOrder);
            var jwtToken = await _tokenService.CreateIdentityTokenAsync(_settings?.DbConnections?.MasterConnection, tenant.Id.ToString());
            var aspnetId = _userManager.GetUserId(User);
            var email = User.Identity.Name;
            var name = email.Split('@')[0];
            var response = await _authClient.AddUserRequest(jwtToken, aspnetId, email, name, RequestStatus.WaitingApproval, DateTime.Now);
            if (response.IsSuccessStatusCode)
            {
                ShowRequestSentConfirmation = true;
            }
            else
            {
                //ToDo handle errors properly
                ModelState.AddModelError(string.Empty, "There was an error sending the request. Please try again later.");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostSelectTenantAsync(string tenantOrder)
        {
            var tenant = TenantList.FirstOrDefault(t => t.order.ToString() == tenantOrder);
            SystemList = await TenantSystem.GetTenantSystems(_masterConnection, tenant.Id); //Todo use the tenant systems to show the system selection page if there are more than 1 system for the tenant, otherwise directly call the GetTenantSystemCookie and redirect to the system.
            ShowTenants = false;
            ShowSystems = true;

            var tenantId = tenant?.Id.ToString();
            var aspNetUserId = _userManager.GetUserId(User);

            if (tenant == null || aspNetUserId == null) return Page();

            // Generate the refresh token and set it in the cookie, Attach the cookie to the Response, so the frontend can use it to get access token and call APIs.
            // set systemId to 0 first, after user select system, call GetRefreshTokenCookie again to update the cookie with the selected systemId, so the frontend can use it to get access token and call APIs.
            _tokenService.SetRefreshTokenCookie(Response, Constants.RefreshTokenCookieName, tenant.Id, aspNetUserId, 0);
            _tokenService.SetWelcomeCookie(Response, Constants.WelcomeCookieName, tenant.Name, tenant.CompanyName, "", 0, "");
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

            var welcomeData = Request.Cookies[Constants.WelcomeCookieName];
            if (welcomeData == null)
                return Unauthorized(); //todo unauthenticated
            var tenantData = welcomeData?.Split('|');
            var tenantName = tenantData[0];
            var tenantCompanyName = tenantData[1];

            SystemList = await TenantSystem.GetTenantSystems(_masterConnection, tenantId); //Todo use the tenant systems to show the system selection page if there are more than 1 system for the tenant, otherwise directly call the GetTenantSystemCookie and redirect to the system.
            var system = SystemList.FirstOrDefault(s => s.Id == systemId);
            // Generate the refresh token and set it in the cookie, Attach the cookie to the Response,
            // so the frontend can use it to get access token and call APIs.
            _tokenService.SetRefreshTokenCookie(Response, Constants.RefreshTokenCookieName, tenantId, aspNetUserId, systemId);
            _tokenService.SetWelcomeCookie(Response, Constants.WelcomeCookieName, tenantName, tenantCompanyName, system?.Name??"", systemId, system?.Logo??"");

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

        public static async Task<List<Tenant>> GetTenants(string masterConnection)
        {
            var sqlSelect = "select * from bfsTenant";
            var sqlStatement = sqlSelect.ToString();

            using var db = new SqlConnection(masterConnection);
            var items = await db.QueryAsync<Tenant>(sqlSelect.ToString(), null);

            var i = 0;
            var tenantList = new List<Tenant>();
            foreach (var item in items)
            {
                i++;
                tenantList.Add(new Tenant() { order = i.ToString(), Id = item.Id, Name = item.Name, CompanyName = item.CompanyName, Logo = item.Logo, DbConnection = item.DbConnection });
            }

            return tenantList;
        }
    }

    public class TenantSystem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }

        public static async Task<List<TenantSystem>> GetTenantSystems(string masterConnection, long tenantId)
        {
            var sqlSelect = "select  s.* from bfsSystem s inner join bfsTenantSystem ts on ts.bfsSystemId = s.id";
            sqlSelect = sqlSelect + " where ts.BfsTenantId = @TenantId";
            var sqlStatement = sqlSelect.ToString();

            var sqlParameters = new DynamicParameters();
            sqlParameters.Add("@TenantId", tenantId); 

            using var db = new SqlConnection(masterConnection);
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
