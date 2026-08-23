using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface ICashTransactionService: ICrudService<CashTransaction>
    {
        Task<CashTransaction> UploadAsync(CashTransaction contract);

        Task<QueryResponse<CashTransactionListItem>> ListAsync(QueryRequest<CashTransactionListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

