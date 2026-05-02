using Bfs.Auth.Contracts;
using Bfs.Core.Contracts.Auth;
using Bfs.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Bfs.Core.Services.Auth
{
    public interface IUserTenantService
    {
        Task SetUpUser<T>(IAspnetUserRequest userRequest, ICrudService<T> userService) where T: IAuthUser, new();
    }
}