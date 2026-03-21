using Dapper;
using Bfs.Core.Config;
using Bfs.Core.Contracts.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Bfs.Core.Services.Auth;

public class TokenService : ITokenService
{
    private readonly BfsSettings _settings;

    public TokenService(IOptions<BfsSettings> options)
    {
        _settings = options.Value;
    }

    // This method is called by frontend to get JWT token for API calls after user select tenant.
    // the tenantId and aspnetUserId are extracted from the refresh token cookie by frontend and passed to this method to generate JWT token.
    // It returns the JWT token which contains the roles for the user in the selected tenant, frontend will use this JWT token to call APIs.
    public async Task<string> CreateTokenAsync(string tenantId, string aspnetUserId)
    {
        var tenantList = await GetTenants();
        var tenant = tenantList.FirstOrDefault(t => t.Id.ToString() == tenantId);
        var UserRolelist = await GetAuthUserRoles(tenant, aspnetUserId);
        var claims = GetClaims(UserRolelist);
        var jwtToken = CreateJwtToken(claims);
        return jwtToken;
    }

    // this method is called when user select tenant, the refresh token cookie is set for the selected tenant and user,
    // frontend will use it to get JWT token for API calls. The cookie value format is {tenantId}|{aspnetUserId},
    // frontend will extract tenantId and aspnetUserId from the cookie and pass to GetTokenAsync to get JWT token.
    // this method is NOT called from the Token controller. it is called only in index.cshtml page.
    // the OnPostSelectTenantAsync method in the Index.cshtml.cs when user select tenant, so the tenantId and aspnetUserId can be passed to this method to generate the refresh token cookie.
    public void GetRefreshTokenCookie(HttpResponse Response, string cookieName, long tenantId, string aspnetUserId)
    {
        var cookieValue = $"{tenantId}|{aspnetUserId}";
        var options = new CookieOptions
        {
            Expires = DateTime.Now.AddHours(8),
            HttpOnly = true, // Set to true if the SPA doesn't need to read it via JS
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Path = "/" // Important for sharing across the domain
        };

        // Cookie set up
        Response.Cookies.Append(cookieName, cookieValue, options);
    }

    public static async Task<List<Tenant>> GetTenants()
    {
        var bfsDbConnection = "Server=localhost;Database=BestFit_V4; User Id=sa;Password=12Remember!; TrustServerCertificate=True";
        using var db = new SqlConnection(bfsDbConnection);
        var sqlSelect = "select * from bfsTenant";
        var sqlStatement = sqlSelect.ToString();
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

    public static async Task<List<AuthRoleUser>> GetAuthUserRoles(Tenant tenant, string aspNetUserId)
    {
        // var bfsDbConnection = "Server=localhost;Database=Tenant10; User Id=sa;Password=12Remember!; TrustServerCertificate=True";
        var dbConnection = tenant.DbConnection;
        using var db = new SqlConnection(dbConnection);

        var sqlSelect = "select u.id as UserId, ru.AuthRoleId as RoleId " +
            "from AuthUser u " +
            "left join AuthRoleUser ru on ru.AuthUserId=u.id " +
            "where u.AspNetUserId =@AspNetUserId";

        var sqlStatement = sqlSelect.ToString();
        var parameters = new { AspNetUserId = aspNetUserId };

        var items = await db.QueryAsync<AuthRoleUser>(sqlSelect.ToString(), parameters);
        return items.ToList();
    }

    // Reads a JWT token without validation and returns the ClaimsPrincipal
    public ClaimsPrincipal ReadToken(string token)
    {
        var jwtSettings = _settings.JwtSettings;
        var tokenValidationParameters = TokenValidationParameters(jwtSettings, false);
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        return principal;
    }

    public string TokenClaimsToJson(string token)
    {
        var principal = ReadToken(token);
        var tokenParsed = new TokenParsed();

        if (principal != null)
        {
            var claims = principal.Claims.ToList();
            foreach (var claim in claims)
                switch (claim.Type.Replace(@"http://schemas.microsoft.com/ws/2008/06/identity/claims/", ""))
                {
                    case "userId":
                        tokenParsed.UserId = claim.Value; // Fix: Ensure UserId is an instance property, not static.
                        break;
                    case "exp":
                        tokenParsed.Exp = claim.Value;
                        break;
                    case "role":
                        tokenParsed.Role.Add(claim.Value);
                        break;
                    case "app":
                        tokenParsed.App.Add(claim.Value);
                        break;
                    case "api":
                        tokenParsed.Api.Add(claim.Value);
                        break;
                    case "method":
                        tokenParsed.Method.Add(claim.Value);
                        break;
                }
        }

        var tokenParsedJson = JsonSerializer.Serialize(tokenParsed, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase // enforce camelCase
        });
        return tokenParsedJson;
    }

    public List<Claim> GetClaims(List<AuthRoleUser> list)
    {
        var addOnce = true;
        var claims = new List<Claim>();
        foreach (var userRole in list)
        {
            if (addOnce)
            {
                claims.Add(new Claim("userId", userRole.UserId.ToString()));
                addOnce = false;
            }

            claims.Add(new Claim("role", userRole.RoleId.ToString()));
        };

        return claims;
    }

    public async Task<List<Claim>> GetClaimsAsync(long userId)
    {
        var claims = new List<Claim>
        {
            new("userId", userId.ToString()),
            new("role", "admin"),
            new("api", "auth"),
            new("api", "stockex"),
            // new Claim("app", "bfs.admin"),
            new("app", "stkex.admin"),
            new("app", "stkex.b.ofc"),
            new("app", "stkex.f.ofc"),
            new("method", "q.tradingroom"),
            new("method", "a.tradingroom"),
            new("method", "u.tradingroom"),
            new("method", "q.broker"),
            new("method", "d.broker")
        };

        return claims;
    }

    public string CreateJwtToken(List<Claim> claims)
    {
        var jwtSettings = _settings.JwtSettings;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));

        var token = new JwtSecurityToken(
            jwtSettings.Issuer,
            jwtSettings.Audience,
            expires: DateTime.UtcNow.AddMinutes(jwtSettings.AccessTokenExpireInMin),
            claims: claims,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    //public async Task<string> CreateAccessToken(long userId)
    //{
    //    var jwtSettings = _settings.JwtSettings;
    //    var claims = await GetClaimsAsync(userId);
    //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));

    //    var token = new JwtSecurityToken(
    //        jwtSettings.Issuer,
    //        jwtSettings.Audience,
    //        expires: DateTime.UtcNow.AddMinutes(jwtSettings.AccessTokenExpireInMin),
    //        claims: claims,
    //        signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
    //    );

    //    return new JwtSecurityTokenHandler().WriteToken(token);
    //}

    public static TokenValidationParameters TokenValidationParameters(JwtSettings jwtSettings,
        bool isValidateLifeTime = true)
    {
        return new TokenValidationParameters
        {
            ValidateLifetime = isValidateLifeTime, //false means Ignore expiration, in case of refreshToken

            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,

            ValidateAudience = true,
            ValidAudience = jwtSettings.Audience,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
        };
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

}

public class AuthRoleUser
{
    public long UserId { get; set; }
    public long RoleId { get; set; }
}

/*
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
private readonly UserManager<ApplicationUser> _userManager;
private readonly TokenService _tokenService;
private readonly IConfiguration _config;

public AuthController(UserManager<ApplicationUser> userManager, TokenService tokenService, IConfiguration config)
{
    _userManager = userManager;
    _tokenService = tokenService;
    _config = config;
}

[HttpPost("login")]
public async Task<IActionResult> Login([FromBody] LoginDto model)
{
    var user = await _userManager.FindByEmailAsync(model.Email);
    if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
        return Unauthorized();

    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Email, user.Email)
    };

    var roles = await _userManager.GetRolesAsync(user);
    claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

    var accessToken = _tokenService.CreateAccessToken(claims);
    var refreshToken = _tokenService.CreateRefreshToken();

    user.RefreshToken = refreshToken;
    user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(int.Parse(_config["JwtSettings:RefreshTokenExpirationDays"]));
    await _userManager.UpdateAsync(user);

    return Ok(new TokenResponse
    {
        AccessToken = accessToken,
        RefreshToken = refreshToken
    });
}

[HttpPost("refresh")]
public async Task<IActionResult> Refresh([FromBody] TokenResponse tokenModel)
{
    var principal = GetPrincipalFromExpiredToken(tokenModel.AccessToken);
    var userId = principal?.FindFirstValue(ClaimTypes.NameIdentifier);
    var user = await _userManager.FindByIdAsync(userId);

    if (user == null || user.RefreshToken != tokenModel.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        return Unauthorized();

    var newAccessToken = _tokenService.CreateAccessToken(principal.Claims);
    var newRefreshToken = _tokenService.CreateRefreshToken();

    user.RefreshToken = newRefreshToken;
    user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(int.Parse(_config["JwtSettings:RefreshTokenExpirationDays"]));
    await _userManager.UpdateAsync(user);

    return Ok(new TokenResponse
    {
        AccessToken = newAccessToken,
        RefreshToken = newRefreshToken
    });
}

private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
{
    var tokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = false, // Ignore expiration
        ValidIssuer = _config["JwtSettings:Issuer"],
        ValidAudience = _config["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Secret"]))
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
    return principal;
}
}


*/