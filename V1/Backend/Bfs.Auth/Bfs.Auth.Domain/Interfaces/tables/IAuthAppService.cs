using Bfs.Core.Contracts;
using Bfs.Auth.Contracts;

namespace Bfs.Auth.Domain.Interfaces
{
    public interface IAuthAppService
    {
        Task<AuthApp?> GetAsync(long id);
        Task<List<AuthApp>> GetAsync();

        Task<AuthApp> CreateAsync(AuthApp contract);
        Task<AuthApp?> UpdateAsync(AuthApp contract);
        Task DeleteAsync(long id);
        Task<AuthApp> UploadAsync(AuthApp contract);

        Task<QueryResponse<AuthAppListItem>> ListAsync(QueryRequest<AuthAppListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
