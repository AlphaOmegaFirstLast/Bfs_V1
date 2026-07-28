using Bfs.Core.Data;
using Bfs.Stores.Data;

namespace Bfs.Stores.Data.Interfaces
{
    public interface IDocumentDetailsList
    {
        Task<QueryResponse<DocumentDetailsListItem>> GetAsync(QueryRequest<DocumentDetailsListFilter> request);
    }
}

