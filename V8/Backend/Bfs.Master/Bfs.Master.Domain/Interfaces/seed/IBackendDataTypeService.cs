using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Master.Contracts;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Master.Domain.Interfaces
{
    public interface IBackendDataTypeService: ICrudService<BackendDataType>
    {
        Task<BackendDataType> UploadAsync(BackendDataType contract);

        Task<QueryResponse<BackendDataTypeListItem>> ListAsync(QueryRequest<BackendDataTypeListFilter> contractRequest);

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    }
}
