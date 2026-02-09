using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Bfs.Core.Config;
using Bfs.Core.Contracts.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Bfs.Core.Services.Auth;

public class TokenService : ITokenService
{
    private readonly BfsSettings _settings;

    public TokenService(IOptions<BfsSettings> options)
    {
        _settings = options.Value;
    }

    public async Task<TokenResponse> CreateTokensAsync(TokenRequest request)
    {
        var tokenResponse = new TokenResponse();
        tokenResponse.AccessToken = await CreateAccessToken(request.UserId);
        tokenResponse.RefreshToken = "Refresh1";
        return tokenResponse;
    }

    public async Task<TokenResponse> RefreshTokensAsync(RefreshTokenRequest request)
    {
        // var userId = getUserIdFromRefreshToken;
        var userId = 1;
        var tokenResponse = new TokenResponse();
        tokenResponse.AccessToken = await CreateAccessToken(userId);
        tokenResponse.RefreshToken = "Refresh2";
        return tokenResponse;
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

    public async Task<string> CreateAccessToken(long userId)
    {
        var jwtSettings = _settings.JwtSettings;
        var claims = await GetClaimsAsync(userId);
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