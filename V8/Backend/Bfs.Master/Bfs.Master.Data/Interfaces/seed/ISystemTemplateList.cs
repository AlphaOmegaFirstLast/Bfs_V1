using Bfs.Core.Data;
using Bfs.Master.Data;

namespace Bfs.Master.Data.Interfaces
{
    public interface ISystemTemplateList
    {
        Task<QueryResponse<SystemTemplateListItem>> GetAsync(QueryRequest<SystemTemplateListFilter> request);
    }
}