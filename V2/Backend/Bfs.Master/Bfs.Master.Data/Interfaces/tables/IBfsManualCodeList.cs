using Bfs.Core.Data;
using Bfs.Master.Data;

namespace Bfs.Master.Data.Interfaces
{
    public interface IBfsManualCodeList
    {
        Task<QueryResponse<BfsManualCodeListItem>> GetAsync(QueryRequest<BfsManualCodeListFilter> request);
    }
}