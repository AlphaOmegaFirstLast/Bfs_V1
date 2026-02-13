using Bfs.Core.Data;
using Bfs.Infrastructure.Data;

namespace Bfs.Infrastructure.Data.Interfaces
{
    public interface IStructureCompare
    {
        Task<QueryResponse<StructureCompareItem>> GetAsync(QueryRequest<StructureCompareFilter> request);
    }
}