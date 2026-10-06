using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Master.Contracts;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Master.Domain.Interfaces
{
    public interface IActionTypeService: ICrudService<ActionType>
    {
        Task<ActionType> UploadAsync(ActionType contract);

        Task<QueryResponse<ActionTypeListItem>> ListAsync(QueryRequest<ActionTypeListFilter> contractRequest);

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    }
}
