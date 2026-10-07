using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Auth.Contracts;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Auth.Domain.Interfaces
{
    public interface IUserService: ICrudService<User>
    {
        Task<User> UploadAsync(User contract);

        Task<QueryResponse<UserListItem>> ListAsync(QueryRequest<UserListFilter> contractRequest);

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2
    }
}
