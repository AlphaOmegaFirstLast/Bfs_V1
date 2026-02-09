using Bfs.Core.Contracts;
using Bfs.BestFit.Contracts;

namespace Bfs.BestFit.Domain.Interfaces
{
    public interface IComponentBusinessActionService
    {
        Task<ComponentBusinessAction?> GetAsync(long id);
        Task<List<ComponentBusinessAction>> GetAsync();

        Task<ComponentBusinessAction> CreateAsync(ComponentBusinessAction contract);
        Task<ComponentBusinessAction?> UpdateAsync(ComponentBusinessAction contract);
        Task DeleteAsync(long id);
        Task<ComponentBusinessAction> UploadAsync(ComponentBusinessAction contract);

        Task<QueryResponse<ComponentBusinessActionListItem>> ListAsync(QueryRequest<ComponentBusinessActionListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
