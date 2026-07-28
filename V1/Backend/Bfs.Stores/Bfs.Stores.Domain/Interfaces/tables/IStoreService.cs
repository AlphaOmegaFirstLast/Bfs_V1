using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Stores.Contracts;

namespace Bfs.Stores.Domain.Interfaces
{
    public interface IStoreService: ICrudService<Store>
    {
        Task<Store> UploadAsync(Store contract);

        Task<QueryResponse<StoreListItem>> ListAsync(QueryRequest<StoreListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

