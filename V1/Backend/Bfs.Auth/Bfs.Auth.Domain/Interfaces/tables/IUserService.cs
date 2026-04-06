using Bfs.Core.Contracts;
using Bfs.Auth.Contracts;

namespace Bfs.Auth.Domain.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetAsync(long id);
        Task<List<User>> GetAsync();

        Task<User> CreateAsync(User contract);
        Task<User?> UpdateAsync(User contract);
        Task DeleteAsync(long id);
        Task<User> UploadAsync(User contract);

        Task<QueryResponse<UserListItem>> ListAsync(QueryRequest<UserListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

