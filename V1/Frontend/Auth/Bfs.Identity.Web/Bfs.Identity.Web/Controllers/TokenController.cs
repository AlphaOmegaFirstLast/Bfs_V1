using Azure.Core;
using Bfs.Core.Config;
using Bfs.Core.Contracts.Auth;
using Bfs.Core.Services.Auth;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IO.Enumeration;

namespace Bfs.Identity.Web.Controllers;

[ApiController]
[Route("identity/[controller]")]
public class TokenController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly BfsSettings _settings;

    public TokenController(ITokenService tokenService, IOptions<BfsSettings> settings, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _tokenService = tokenService; 
        _signInManager = signInManager;
        _settings = settings.Value;
    }

    /// <summary>   
    /// This method retrieves a new JWT token using the refresh token stored in the cookies. 
    /// It checks for the presence of the refresh token, validates it, and if valid, generates a new JWT token. 
    /// If the refresh token is missing or invalid, it returns an unauthorized response.
    /// </summary>

    [HttpGet("refresh")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies[Constants.RefreshTokenCookieName];
        if (refreshToken == null)
            return Unauthorized(); //todo unauthenticated
        var ids = refreshToken?.Split('|');
        var jwtToken = await _tokenService.CreateTokenAsync(_settings?.DbConnections?.MasterConnection, ids[0], ids[1]);
        if (jwtToken == null)
            return Unauthorized(); //todo unauthenticated

        return Ok(new { jwtToken = jwtToken });
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        Response.Cookies.Delete(Constants.RefreshTokenCookieName); // Ensure 'RefreshTokenCookieName' exists in the 'Constants' class
        return Ok();
    }
}

