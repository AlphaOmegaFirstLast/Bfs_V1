using Bfs.Core.Contracts;
using Bfs.Master.Contracts;

namespace Bfs.Master.Domain.Interfaces
{
    public interface IFormControlTypeService
    {
        Task<FormControlType?> GetAsync(long id);
        Task<List<FormControlType>> GetAsync();

        Task<FormControlType> CreateAsync(FormControlType contract);
        Task<FormControlType?> UpdateAsync(FormControlType contract);
        Task DeleteAsync(long id);
        Task<FormControlType> UploadAsync(FormControlType contract);

        Task<QueryResponse<FormControlTypeListItem>> ListAsync(QueryRequest<FormControlTypeListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
