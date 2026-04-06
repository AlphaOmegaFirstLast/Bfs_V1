using Bfs.Core.Contracts;
using Bfs.Auth.Contracts;

namespace Bfs.Auth.Domain.Interfaces
{
    public interface IUserRequestService
    {
        Task<UserRequest?> GetAsync(long id);
        Task<List<UserRequest>> GetAsync();

        Task<UserRequest> CreateAsync(UserRequest contract);
        Task<UserRequest?> UpdateAsync(UserRequest contract);
        Task DeleteAsync(long id);
        Task<UserRequest> UploadAsync(UserRequest contract);

        Task<QueryResponse<UserRequestListItem>> ListAsync(QueryRequest<UserRequestListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

