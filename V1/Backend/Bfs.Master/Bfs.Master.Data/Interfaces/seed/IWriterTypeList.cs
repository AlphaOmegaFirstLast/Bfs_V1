using Bfs.Core.Data;
using Bfs.Master.Data;

namespace Bfs.Master.Data.Interfaces
{
    public interface IWriterTypeList
    {
        Task<QueryResponse<WriterTypeListItem>> GetAsync(QueryRequest<WriterTypeListFilter> request);
    }
}