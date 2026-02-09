using Bfs.Core.Data;
using Bfs.Infrastructure.Data;

namespace Bfs.Infrastructure.Data.Interfaces
{
    public interface IBusinessActionList
    {
        Task<QueryResponse<BusinessActionListItem>> GetAsync(QueryRequest<BusinessActionListFilter> request);
    }
}