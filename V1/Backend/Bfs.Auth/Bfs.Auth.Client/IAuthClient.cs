//Template_Start_Code_DontOverwrite_1
using Bfs.Core.Config;

namespace Bfs.Auth.Client
{
    public interface IAuthClient
    {
        Task<HttpResponseMessage> AddUserRequest(string jwtToken, string aspnetUserId, string email, string name, RequestStatus userRequestStatusId, DateTime requestDate);
    }
}
//Template_End_Code_DontOverwrite_1    
