using Bfs.Core.Data;
using Bfs.BestFit.Data;

namespace Bfs.BestFit.Data.Interfaces
{
    public interface ISystemInfoList
    {
        Task<QueryResponse<SystemInfoListItem>> GetSystemInfoListAsync(QueryRequest<SystemInfoListFilter> request);
    }
}