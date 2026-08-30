using Bfs.Core.Data;
using Bfs.Master.Data;

namespace Bfs.Master.Data.Interfaces
{
    public interface IBackendDataTypeList
    {
        Task<QueryResponse<BackendDataTypeListItem>> GetAsync(QueryRequest<BackendDataTypeListFilter> request);
    }
}