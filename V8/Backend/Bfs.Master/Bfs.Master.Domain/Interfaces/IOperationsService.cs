using Bfs.Master.Contracts;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Master.Domain.Interfaces;

public interface IOperationsService
{
Task<List<BfsComponentSystemAction>> UpdateBfsComponentSystemActionMatrixAsync(long parentId, List<BfsComponentSystemAction> matrix);
Task<List<BfsComponentBusinessAction>> UpdateBfsComponentBusinessActionMatrixAsync(long parentId, List<BfsComponentBusinessAction> matrix);
Task<List<BfsTenantSystem>> UpdateBfsTenantSystemMatrixAsync(long parentId, List<BfsTenantSystem> matrix);
//Template_Field_ChildrenMatrix_AddIServiceEntry
//Template_Start_Code_DontOverwrite_2
Task<long> DuplicateComponentTreeAsync(long componentId);
    Task DeleteComponentTreeAsync(long componentId);

    Task PublishToLocal(long id);
    Task DeployToAzure(long id);
    Task DeployToLocal(long id);
//Template_End_Code_DontOverwrite_2

}
