using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Auth.Contracts;

namespace Bfs.Auth.Domain.Interfaces
{
    public interface IUserService: ICrudService<User>
    {
        Task<User> UploadAsync(User contract);

        Task<QueryResponse<UserListItem>> ListAsync(QueryRequest<UserListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

