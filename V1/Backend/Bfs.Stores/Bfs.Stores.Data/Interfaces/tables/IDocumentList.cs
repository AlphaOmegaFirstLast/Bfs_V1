using Bfs.Core.Data;
using Bfs.Stores.Data;

namespace Bfs.Stores.Data.Interfaces
{
    public interface IDocumentList
    {
        Task<QueryResponse<DocumentListItem>> GetAsync(QueryRequest<DocumentListFilter> request);
    }
}

