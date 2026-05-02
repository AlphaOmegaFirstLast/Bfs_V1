using Bfs.Auth.Contracts;
using Bfs.Core.Config;
using Bfs.Core.Contracts.Auth;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Bfs.Auth.Client
{
    public class AuthClient : IAuthClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress = "";
        public AuthClient(HttpClient httpClient, IOptions<BfsSettings> settings)
        {
            _httpClient = httpClient;
            _baseAddress = settings.Value?.ApiBaseUrls?.AuthApi ?? _baseAddress;
        }

        public async Task<HttpResponseMessage> AddUserRequest(string jwtToken, string aspnetUserId, string email, string name, RequestStatus userRequestStatusId, DateTime requestDate)
        {
            // todo read token from ScopedData or pass token as parameter, currently pass token as parameter for simplicity. we can refactor it later to read from scopedData if needed.
            var request = new HttpRequestMessage(HttpMethod.Post, $@"{_baseAddress}/api/admin/UserRequest");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            var userRequest = new UserRequest() { AspNetUserId = aspnetUserId, Email= email, Name = name, UserRequestStatusId = (int)userRequestStatusId, RequestDate= requestDate};
            request.Content = JsonContent.Create(userRequest);
            //ToDo return a type instead of HttpResponseMessage
            return await _httpClient.SendAsync(request);
        }
    }
}
