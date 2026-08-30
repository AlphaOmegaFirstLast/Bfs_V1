using Bfs.Core.Contracts;
using Bfs.Master.Contracts;

namespace Bfs.Master.Domain.Interfaces
{
    public interface IFilterTypeService
    {
        Task<FilterType?> GetAsync(long id);
        Task<List<FilterType>> GetAsync();

        Task<FilterType> CreateAsync(FilterType contract);
        Task<FilterType?> UpdateAsync(FilterType contract);
        Task DeleteAsync(long id);
        Task<FilterType> UploadAsync(FilterType contract);

        Task<QueryResponse<FilterTypeListItem>> ListAsync(QueryRequest<FilterTypeListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
