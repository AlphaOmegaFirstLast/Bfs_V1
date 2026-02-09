using System.Security.Claims;
using Bfs.Core.Contracts.Auth;

namespace Bfs.Core.Services.Auth;

public interface ITokenService
{
    Task<TokenResponse> CreateTokensAsync(TokenRequest request);
    Task<TokenResponse> RefreshTokensAsync(RefreshTokenRequest request);
    ClaimsPrincipal ReadToken(string token);
    string TokenClaimsToJson(string token);
}