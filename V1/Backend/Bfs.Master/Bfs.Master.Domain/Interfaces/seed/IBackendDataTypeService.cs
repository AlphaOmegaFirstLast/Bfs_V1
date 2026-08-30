using Bfs.Core.Contracts;
using Bfs.Master.Contracts;

namespace Bfs.Master.Domain.Interfaces
{
    public interface IBackendDataTypeService
    {
        Task<BackendDataType?> GetAsync(long id);
        Task<List<BackendDataType>> GetAsync();

        Task<BackendDataType> CreateAsync(BackendDataType contract);
        Task<BackendDataType?> UpdateAsync(BackendDataType contract);
        Task DeleteAsync(long id);
        Task<BackendDataType> UploadAsync(BackendDataType contract);

        Task<QueryResponse<BackendDataTypeListItem>> ListAsync(QueryRequest<BackendDataTypeListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
