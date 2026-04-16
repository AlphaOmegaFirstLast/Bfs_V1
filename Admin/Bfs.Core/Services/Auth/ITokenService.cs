using Bfs.Core.Contracts.Auth;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Bfs.Core.Services.Auth;

public interface ITokenService
{
    // Refresh token cookie format: {tenantId}|{aspnetUserId}. set by Identity.Web when user select tenant, used by frontend to get access token for API calls.
    void GetRefreshTokenCookie(HttpResponse Response, string cookieName, long tenantId, string aspnetUserId, long systemId);

    void SetWelcomeCookie(HttpResponse Response, string cookieName, string tenantName, string companyName, string systemName, long systemId, string systemLogo);

    // Create JWT token for API calls, called by Main frontend to get its roles and permissions for the selected tenant.
    // The tenantId and aspnetUserId are extracted from the refresh token cookie by frontend and passed to this method.
    Task<string> CreateTokenAsync(string masterConnection, string tenantId, string aspnetUserId);
}