using Bfs.Core.Contracts;
using Bfs.Auth.Contracts;

namespace Bfs.Auth.Domain.Interfaces
{
    public interface IAppService
    {
        Task<App?> GetAsync(long id);
        Task<List<App>> GetAsync();

        Task<App> CreateAsync(App contract);
        Task<App?> UpdateAsync(App contract);
        Task DeleteAsync(long id);
        Task<App> UploadAsync(App contract);

        Task<QueryResponse<AppListItem>> ListAsync(QueryRequest<AppListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

