using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Stores.Contracts;

namespace Bfs.Stores.Domain.Interfaces
{
    public interface IDocumentService: ICrudService<Document>
    {
        Task<Document> UploadAsync(Document contract);

        Task<QueryResponse<DocumentListItem>> ListAsync(QueryRequest<DocumentListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

