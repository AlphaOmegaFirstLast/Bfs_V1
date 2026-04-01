using Bfs.Core.Config;
using Bfs.Core.Contracts.Auth;
using Bfs.Core.TenantManagement;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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
    public async Task<string> CreateTokenAsync(string masterDbConnection, string tenantId, string aspnetUserId)
    {
        var tenantList = TenantManager.GetAllTenants(masterDbConnection);
        var tenant = tenantList.FirstOrDefault(t => t.Id.ToString() == tenantId);
        var UserRolelist = await GetAuthUserRoles(tenant.DbConnection, aspnetUserId);
        var claims = GetClaims(tenantId, UserRolelist);
        var jwtToken = CreateJwtToken(claims);
        return jwtToken;
    }

    // this method is called when user select tenant, the refresh token cookie is set for the selected tenant and user,
    // frontend will use it to get JWT token for API calls. The cookie value format is {tenantId}|{aspnetUserId},
    // frontend will extract tenantId and aspnetUserId from the cookie and pass to GetTokenAsync to get JWT token.
    // this method is NOT called from the Token controller. it is called only in index.cshtml page.
    // the OnPostSelectTenantAsync method in the Index.cshtml.cs when user select tenant, so the tenantId and aspnetUserId can be passed to this method to generate the refresh token cookie.
    public void GetRefreshTokenCookie(HttpResponse Response, string cookieName, long tenantId, string aspnetUserId, long systemId)
    {
        var cookieValue = $"{tenantId}|{aspnetUserId}|{systemId}";
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

    public static async Task<List<AuthRoleUser>> GetAuthUserRoles(string tenantConnection, string aspNetUserId)
    {
        using var db = new SqlConnection(tenantConnection);
        var sqlSelect = "select u.id as UserId, ru.AuthRoleId as RoleId " +
            "from AuthUser u " +
            "left join AuthRoleUser ru on ru.AuthUserId=u.id " +
            "where u.AspNetUserId =@AspNetUserId";

        var sqlStatement = sqlSelect.ToString();
        var parameters = new { AspNetUserId = aspNetUserId };

        var items = await db.QueryAsync<AuthRoleUser>(sqlSelect.ToString(), parameters);
        return items.ToList();
    }

    public List<Claim> GetClaims(string tenantId, List<AuthRoleUser> list)
    {
        var addOnce = true;
        var claims = new List<Claim>();

        claims.Add(new Claim("tenantId", tenantId));

        foreach (var userRole in list)
        {
            if (addOnce)
            {
                claims.Add(new Claim("userId", userRole.UserId.ToString()));
                addOnce = false;
            }

            claims.Add(new Claim("roleId", userRole.RoleId.ToString()));
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

public class AuthRoleUser
{
    public long UserId { get; set; }
    public long RoleId { get; set; }
}
