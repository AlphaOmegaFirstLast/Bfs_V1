using Bfs.Auth.Client;
using Bfs.Core.Config;
using Bfs.Core.Interfaces;
using Bfs.Core.Services.Auth;
using [TemplateSln].Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace [TemplateSln].Web.Pages
{
    public class IndexModel : PageModel
    {
        public ClaimsPrincipal ClaimsPrinciple { get; set; }
        public string? ClaimsJson { get; set; }
        public string? SystemApplicationsJson { get; set; }
        public List<SystemApplication> AvailableApplications { get; set; }

        private readonly ILogger<IndexModel> _logger;
        private readonly BfsSettings _settings;
        private readonly AuthClient _authClient;
        private readonly SignInManager<AuthUser> _signInManager;
        private readonly UserManager<AuthUser> _userManager;
        private readonly ITokenService _tokenService;
        public IndexModel(ILogger<IndexModel> logger,  
            IOptions<BfsSettings> settings, 
            AuthClient authClient, 
            SignInManager<AuthUser> signInManager, 
            UserManager<AuthUser> userManager,
            ITokenService tokenService
            )
        {
            _logger = logger;
            _authClient = authClient;
            _settings = settings.Value;
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public void OnGet()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var userIdString = _userManager.GetUserId(User);
                if (!string.IsNullOrEmpty(userIdString))
                {
                    var userId = long.Parse(userIdString);
                    var tokenResponse = _authClient.RequestTokensAsync(userId).Result;
                    if (tokenResponse != null && !string.IsNullOrEmpty(tokenResponse.AccessToken))
                    {
                        //to display after login in [TemplateSln].Web
                        var claimsPrinciple = _tokenService.ReadToken(tokenResponse.AccessToken);
                        var userApps = claimsPrinciple.Claims.Where(x => x.Type == "app").Select(x => x.Value).ToList();
                        var systemApps = SystemApplication.Get();

                        // intersection between system applications and user applications
                        AvailableApplications = systemApps.Where(y => userApps.Any(x => x == y.Name)).ToList();

                        // to set in session for frontend main application
                        ClaimsJson = _tokenService.TokenClaimsToJson(tokenResponse.AccessToken);
                        SystemApplicationsJson = JsonSerializer.Serialize(systemApps, new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // enforce camelCase
                        });
                    }
                }
            }
        }
    }

    public class SystemApplication 
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string System { get; set; }
        public string Image { get; set; }

        public static List<SystemApplication> Get()
        {
            var apps = new List<SystemApplication>();
            apps.Add(new SystemApplication() { Name = "stkex.b.ofc", DisplayName = "Back Office App", System = "StockEx", Image = "stockexbackoffice.svg" });
            apps.Add(new SystemApplication() { Name = "stkex.f.ofc", DisplayName = "Front Office App", System = "StockEx", Image = "stockexfrontoffice.svg" });
            apps.Add(new SystemApplication() { Name = "stkex.admin", DisplayName = "Admin App", System = "StockEx", Image = "stockexadmin.svg" });
            apps.Add(new SystemApplication() { Name = "bfs.admin", DisplayName = "BestFit Admin App", System = "StockEx", Image = "bfsadmin.svg" });
            return apps;
        }
    }
}
