using Bfs.Master.Contracts;

namespace Bfs.Master.Domain.Interfaces;

public interface IOperationsService
{
    //Template_Start_Code_DontOverwrite_1

    Task<long> DuplicateComponentTreeAsync(long componentId);
    Task DeleteComponentTreeAsync(long componentId);

    Task PublishToLocal(long id);
    Task DeployToAzure(long id);
    Task DeployToLocal(long id);

    //Template_End_Code_DontOverwrite_1

    Task<List<BfsComponentSystemAction>> UpdateBfsComponentSystemActionMatrixAsync(long parentId, List<BfsComponentSystemAction> matrix);
    Task<List<BfsComponentBusinessAction>> UpdateBfsComponentBusinessActionMatrixAsync(long parentId, List<BfsComponentBusinessAction> matrix);
    Task<List<BfsTenantSystem>> UpdateBfsTenantSystemMatrixAsync(long parentId, List<BfsTenantSystem> matrix);
    //Template_Field_ChildrenMatrix_AddIServiceEntry
}
