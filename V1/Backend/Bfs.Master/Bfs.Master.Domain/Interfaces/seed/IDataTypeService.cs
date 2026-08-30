using Bfs.Core.Contracts;
using Bfs.Master.Contracts;

namespace Bfs.Master.Domain.Interfaces
{
    public interface IDataTypeService
    {
        Task<DataType?> GetAsync(long id);
        Task<List<DataType>> GetAsync();

        Task<DataType> CreateAsync(DataType contract);
        Task<DataType?> UpdateAsync(DataType contract);
        Task DeleteAsync(long id);
        Task<DataType> UploadAsync(DataType contract);

        Task<QueryResponse<DataTypeListItem>> ListAsync(QueryRequest<DataTypeListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
