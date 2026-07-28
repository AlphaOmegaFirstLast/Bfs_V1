using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Stores.Contracts;

namespace Bfs.Stores.Domain.Interfaces
{
    public interface IProductService: ICrudService<Product>
    {
        Task<Product> UploadAsync(Product contract);

        Task<QueryResponse<ProductListItem>> ListAsync(QueryRequest<ProductListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

