using Bfs.Core.Config;

namespace Bfs.Auth.Client
{
    public interface IAuthClient
    {
        Task<HttpResponseMessage> AddUserRequest(string jwtToken, string aspnetUserId, string email, string name, RequestStatus userRequestStatusId, DateTime requestDate);
    }
}