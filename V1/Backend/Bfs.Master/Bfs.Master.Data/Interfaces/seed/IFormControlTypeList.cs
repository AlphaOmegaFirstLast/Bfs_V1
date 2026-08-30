using Bfs.Core.Data;
using Bfs.Master.Data;

namespace Bfs.Master.Data.Interfaces
{
    public interface IFormControlTypeList
    {
        Task<QueryResponse<FormControlTypeListItem>> GetAsync(QueryRequest<FormControlTypeListFilter> request);
    }
}