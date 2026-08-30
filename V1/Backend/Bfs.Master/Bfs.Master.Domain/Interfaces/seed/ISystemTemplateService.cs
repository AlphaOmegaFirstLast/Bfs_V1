using Bfs.Core.Contracts;
using Bfs.Master.Contracts;

namespace Bfs.Master.Domain.Interfaces
{
    public interface ISystemTemplateService
    {
        Task<SystemTemplate?> GetAsync(long id);
        Task<List<SystemTemplate>> GetAsync();

        Task<SystemTemplate> CreateAsync(SystemTemplate contract);
        Task<SystemTemplate?> UpdateAsync(SystemTemplate contract);
        Task DeleteAsync(long id);
        Task<SystemTemplate> UploadAsync(SystemTemplate contract);

        Task<QueryResponse<SystemTemplateListItem>> ListAsync(QueryRequest<SystemTemplateListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
