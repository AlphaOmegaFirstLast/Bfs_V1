using Bfs.Core.Contracts;
using Bfs.Master.Contracts;

namespace Bfs.Master.Domain.Interfaces
{
    public interface IAggregateTypeService
    {
        Task<AggregateType?> GetAsync(long id);
        Task<List<AggregateType>> GetAsync();

        Task<AggregateType> CreateAsync(AggregateType contract);
        Task<AggregateType?> UpdateAsync(AggregateType contract);
        Task DeleteAsync(long id);
        Task<AggregateType> UploadAsync(AggregateType contract);

        Task<QueryResponse<AggregateTypeListItem>> ListAsync(QueryRequest<AggregateTypeListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
