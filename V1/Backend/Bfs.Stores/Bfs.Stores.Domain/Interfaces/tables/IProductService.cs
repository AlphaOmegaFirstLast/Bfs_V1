using Bfs.Core.Contracts;
using Bfs.Stores.Contracts;

namespace Bfs.Stores.Domain.Interfaces
{
    public interface IProductService
    {
        Task<Product?> GetAsync(long id);
        Task<List<Product>> GetAsync();

        Task<Product> CreateAsync(Product contract);
        Task<Product?> UpdateAsync(Product contract);
        Task DeleteAsync(long id);
        Task<Product> UploadAsync(Product contract);

        Task<QueryResponse<ProductListItem>> ListAsync(QueryRequest<ProductListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

