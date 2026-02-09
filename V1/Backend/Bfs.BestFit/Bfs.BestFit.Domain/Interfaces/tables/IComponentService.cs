using Bfs.Core.Contracts;
using Bfs.BestFit.Contracts;

namespace Bfs.BestFit.Domain.Interfaces
{
    public interface IComponentService
    {
        Task<Component?> GetAsync(long id);
        Task<List<Component>> GetAsync();

        Task<Component> CreateAsync(Component contract);
        Task<Component?> UpdateAsync(Component contract);
        Task DeleteAsync(long id);
        Task<Component> UploadAsync(Component contract);

        Task<QueryResponse<ComponentListItem>> ListAsync(QueryRequest<ComponentListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
