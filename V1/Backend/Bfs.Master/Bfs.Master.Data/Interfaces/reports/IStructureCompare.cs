using Bfs.Core.Data;
using Bfs.Master.Data;

namespace Bfs.Master.Data.Interfaces
{
    public interface IStructureCompare
    {
        Task<QueryResponse<StructureCompareItem>> GetAsync(QueryRequest<StructureCompareFilter> request);
    }
}