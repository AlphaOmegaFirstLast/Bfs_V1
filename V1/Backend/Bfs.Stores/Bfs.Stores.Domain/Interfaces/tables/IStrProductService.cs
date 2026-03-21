using Bfs.Core.Contracts;
using Bfs.Stores.Contracts;

namespace Bfs.Stores.Domain.Interfaces
{
    public interface IStrProductService
    {
        Task<StrProduct?> GetAsync(long id);
        Task<List<StrProduct>> GetAsync();

        Task<StrProduct> CreateAsync(StrProduct contract);
        Task<StrProduct?> UpdateAsync(StrProduct contract);
        Task DeleteAsync(long id);
        Task<StrProduct> UploadAsync(StrProduct contract);

        Task<QueryResponse<StrProductListItem>> ListAsync(QueryRequest<StrProductListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
