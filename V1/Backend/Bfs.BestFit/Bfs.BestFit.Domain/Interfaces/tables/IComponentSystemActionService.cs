using Bfs.Core.Contracts;
using Bfs.BestFit.Contracts;

namespace Bfs.BestFit.Domain.Interfaces
{
    public interface IComponentSystemActionService
    {
        Task<ComponentSystemAction?> GetAsync(long id);
        Task<List<ComponentSystemAction>> GetAsync();

        Task<ComponentSystemAction> CreateAsync(ComponentSystemAction contract);
        Task<ComponentSystemAction?> UpdateAsync(ComponentSystemAction contract);
        Task DeleteAsync(long id);
        Task<ComponentSystemAction> UploadAsync(ComponentSystemAction contract);

        Task<QueryResponse<ComponentSystemActionListItem>> ListAsync(QueryRequest<ComponentSystemActionListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
