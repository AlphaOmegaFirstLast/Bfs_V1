using Bfs.Core.Contracts;
using Bfs.Infrastructure.Contracts;

namespace Bfs.Infrastructure.Domain.Interfaces
{
    public interface ISystemActionService
    {
        Task<SystemAction?> GetAsync(long id);
        Task<List<SystemAction>> GetAsync();

        Task<SystemAction> CreateAsync(SystemAction contract);
        Task<SystemAction?> UpdateAsync(SystemAction contract);
        Task DeleteAsync(long id);
        Task<SystemAction> UploadAsync(SystemAction contract);

        Task<QueryResponse<SystemActionListItem>> ListAsync(QueryRequest<SystemActionListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1

