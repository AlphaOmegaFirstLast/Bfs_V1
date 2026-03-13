using Azure.Core;
using Bfs.Core.Config;
using Bfs.Core.Contracts.Auth;
using Bfs.Core.Services.Auth;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Bfs.Identity.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TokenController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly BfsSettings _settings;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public TokenController(ITokenService tokenService, IOptions<BfsSettings> settings, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _tokenService = tokenService; _settings = settings.Value;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetToken()
    {
        if (_signInManager.IsSignedIn(User))
        {
            var userIdString = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized();
            }

            var userId = long.Parse(userIdString);
            var request = new TokenRequest() { UserId = userId };
            var tokenResponse = await _tokenService.CreateTokensAsync(request);
            if (tokenResponse == null)
                return Unauthorized();

            SetRefreshTokenCookie(tokenResponse);
            return Ok(new { accessToken = tokenResponse.AccessToken });
        }

        return Unauthorized();
    }

    [HttpGet("refresh")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies[Constants.RefreshTokenCookieName];
        var request = new RefreshTokenRequest() { RefreshToken = refreshToken };
        var tokenResponse = await _tokenService.RefreshTokensAsync(request);
        if (tokenResponse == null)
            return Unauthorized();

        SetRefreshTokenCookie(tokenResponse);
        return Ok(new { accessToken = tokenResponse.AccessToken });
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        Response.Cookies.Delete(Constants.RefreshTokenCookieName); // Ensure 'RefreshTokenCookieName' exists in the 'Constants' class
        return Ok();
    }

    private void SetRefreshTokenCookie(TokenResponse tokenResponse)
    {
        Response.Cookies.Append(Constants.RefreshTokenCookieName, tokenResponse.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(_settings.JwtSettings.RefreshTokenExpireInDay)
        });
    }
}

