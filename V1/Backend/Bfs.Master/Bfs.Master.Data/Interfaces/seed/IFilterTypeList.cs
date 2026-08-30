using Bfs.Core.Data;
using Bfs.Master.Data;

namespace Bfs.Master.Data.Interfaces
{
    public interface IFilterTypeList
    {
        Task<QueryResponse<FilterTypeListItem>> GetAsync(QueryRequest<FilterTypeListFilter> request);
    }
}