using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Stores.Contracts;

namespace Bfs.Stores.Domain.Interfaces
{
    public interface ITransactionService: ICrudService<Transaction>
    {
        Task<Transaction> UploadAsync(Transaction contract);

        Task<QueryResponse<TransactionListItem>> ListAsync(QueryRequest<TransactionListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

