using Bfs.Core.Contracts;
using Bfs.Master.Contracts;

namespace Bfs.Master.Domain.Interfaces
{
    public interface IActionLocationService
    {
        Task<ActionLocation?> GetAsync(long id);
        Task<List<ActionLocation>> GetAsync();

        Task<ActionLocation> CreateAsync(ActionLocation contract);
        Task<ActionLocation?> UpdateAsync(ActionLocation contract);
        Task DeleteAsync(long id);
        Task<ActionLocation> UploadAsync(ActionLocation contract);

        Task<QueryResponse<ActionLocationListItem>> ListAsync(QueryRequest<ActionLocationListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
