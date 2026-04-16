using Bfs.Core.Contracts;
using Bfs.Stores.Contracts;

namespace Bfs.Stores.Domain.Interfaces
{
    public interface IStoreService
    {
        Task<Store?> GetAsync(long id);
        Task<List<Store>> GetAsync();

        Task<Store> CreateAsync(Store contract);
        Task<Store?> UpdateAsync(Store contract);
        Task DeleteAsync(long id);
        Task<Store> UploadAsync(Store contract);

        Task<QueryResponse<StoreListItem>> ListAsync(QueryRequest<StoreListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

