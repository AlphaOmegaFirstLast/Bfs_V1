using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Stores.Contracts;

namespace Bfs.Stores.Domain.Interfaces
{
    public interface IDocumentDetailsService: ICrudService<DocumentDetails>
    {
        Task<DocumentDetails> UploadAsync(DocumentDetails contract);

        Task<QueryResponse<DocumentDetailsListItem>> ListAsync(QueryRequest<DocumentDetailsListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

