using Bfs.Core.Contracts;
using Bfs.BestFit.Contracts;

namespace Bfs.BestFit.Domain.Interfaces
{
    public interface ISystemInfoService
    {
        Task<SystemInfo?> GetAsync(long id);
        Task<List<SystemInfo>> GetAsync();

        Task<SystemInfo> CreateAsync(SystemInfo contract);
        Task<SystemInfo?> UpdateAsync(SystemInfo contract);
        Task DeleteAsync(long id);
        Task<SystemInfo> UploadAsync(SystemInfo contract);

        Task<QueryResponse<SystemInfoListItem>> ListAsync(QueryRequest<SystemInfoListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
