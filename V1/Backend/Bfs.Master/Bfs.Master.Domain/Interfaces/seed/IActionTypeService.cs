using Bfs.Core.Contracts;
using Bfs.Master.Contracts;

namespace Bfs.Master.Domain.Interfaces
{
    public interface IActionTypeService
    {
        Task<ActionType?> GetAsync(long id);
        Task<List<ActionType>> GetAsync();

        Task<ActionType> CreateAsync(ActionType contract);
        Task<ActionType?> UpdateAsync(ActionType contract);
        Task DeleteAsync(long id);
        Task<ActionType> UploadAsync(ActionType contract);

        Task<QueryResponse<ActionTypeListItem>> ListAsync(QueryRequest<ActionTypeListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
