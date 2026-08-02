using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Auth.Contracts;

namespace Bfs.Auth.Domain.Interfaces
{
    public interface IAppService: ICrudService<App>
    {
        Task<App> UploadAsync(App contract);

        Task<QueryResponse<AppListItem>> ListAsync(QueryRequest<AppListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

