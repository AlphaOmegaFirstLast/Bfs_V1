using Bfs.Auth.Client;
using Bfs.Core.Config;
using Bfs.Core.Contracts.Auth;
using [TemplateSln].Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace [TemplateSln].Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TokenController : ControllerBase
{
    private readonly AuthClient _authClient;
    private readonly BfsSettings _settings;
    private readonly SignInManager<AuthUser> _signInManager;
    private readonly UserManager<AuthUser> _userManager;

    public TokenController(AuthClient authClient, IOptions<BfsSettings> settings, SignInManager<AuthUser> signInManager, UserManager<AuthUser> userManager)
    {
        _authClient = authClient;
        _settings = settings.Value;
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
            var tokenResponse = await _authClient.RequestTokensAsync(userId);
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
        var tokenResponse = await _authClient.RefreshTokensAsync(refreshToken);
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

