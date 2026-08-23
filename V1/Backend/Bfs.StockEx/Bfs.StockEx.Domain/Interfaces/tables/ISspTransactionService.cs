using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface ISspTransactionService: ICrudService<SspTransaction>
    {
        Task<SspTransaction> UploadAsync(SspTransaction contract);

        Task<QueryResponse<SspTransactionListItem>> ListAsync(QueryRequest<SspTransactionListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

