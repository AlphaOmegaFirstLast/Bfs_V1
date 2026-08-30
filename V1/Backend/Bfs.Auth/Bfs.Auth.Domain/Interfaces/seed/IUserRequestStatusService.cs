using Bfs.Core.Contracts;
using Bfs.Auth.Contracts;

namespace Bfs.Auth.Domain.Interfaces
{
    public interface IUserRequestStatusService
    {
        Task<UserRequestStatus?> GetAsync(long id);
        Task<List<UserRequestStatus>> GetAsync();

        Task<UserRequestStatus> CreateAsync(UserRequestStatus contract);
        Task<UserRequestStatus?> UpdateAsync(UserRequestStatus contract);
        Task DeleteAsync(long id);
        Task<UserRequestStatus> UploadAsync(UserRequestStatus contract);

        Task<QueryResponse<UserRequestStatusListItem>> ListAsync(QueryRequest<UserRequestStatusListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
