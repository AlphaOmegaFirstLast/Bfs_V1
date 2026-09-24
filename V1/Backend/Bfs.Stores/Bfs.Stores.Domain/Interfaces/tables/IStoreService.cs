using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Stores.Contracts;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Stores.Domain.Interfaces
{
    public interface IStoreService: ICrudService<Store>
    {
        Task<Store> UploadAsync(Store contract);

        Task<QueryResponse<StoreListItem>> ListAsync(QueryRequest<StoreListFilter> contractRequest);

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2
    }
}
