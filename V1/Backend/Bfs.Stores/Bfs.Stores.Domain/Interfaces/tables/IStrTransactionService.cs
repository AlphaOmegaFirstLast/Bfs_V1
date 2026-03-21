using Bfs.Core.Contracts;
using Bfs.Stores.Contracts;

namespace Bfs.Stores.Domain.Interfaces
{
    public interface IStrTransactionService
    {
        Task<StrTransaction?> GetAsync(long id);
        Task<List<StrTransaction>> GetAsync();

        Task<StrTransaction> CreateAsync(StrTransaction contract);
        Task<StrTransaction?> UpdateAsync(StrTransaction contract);
        Task DeleteAsync(long id);
        Task<StrTransaction> UploadAsync(StrTransaction contract);

        Task<QueryResponse<StrTransactionListItem>> ListAsync(QueryRequest<StrTransactionListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
