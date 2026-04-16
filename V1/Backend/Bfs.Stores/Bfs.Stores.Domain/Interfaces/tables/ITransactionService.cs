using Bfs.Core.Contracts;
using Bfs.Stores.Contracts;

namespace Bfs.Stores.Domain.Interfaces
{
    public interface ITransactionService
    {
        Task<Transaction?> GetAsync(long id);
        Task<List<Transaction>> GetAsync();

        Task<Transaction> CreateAsync(Transaction contract);
        Task<Transaction?> UpdateAsync(Transaction contract);
        Task DeleteAsync(long id);
        Task<Transaction> UploadAsync(Transaction contract);

        Task<QueryResponse<TransactionListItem>> ListAsync(QueryRequest<TransactionListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

