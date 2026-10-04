using Bfs.Core.Contracts.Auth;
using System.Net.Http.Json;

namespace Bfs.Auth.Client
{
    public class AuthClient
    {
        private readonly HttpClient _httpClient;
        private const string BaseAddress = "https://localhost:6100";
        public AuthClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TokenResponse?> RequestTokensAsync(long userId)
        {
            var response = await _httpClient.PostAsJsonAsync($@"{BaseAddress}/api/token/create", new
            {
                userId
            });

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TokenResponse>();
        }

        public async Task<TokenResponse?> RefreshTokensAsync(string? refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
                return null;

            var response = await _httpClient.PostAsJsonAsync($@"{BaseAddress}/api/token/refresh", new { refreshToken });

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<TokenResponse>();
        }
    }
}
