using Bfs.BestFit.Contracts;

namespace Bfs.BestFit.Domain.Interfaces;

public interface IOperationsService
{
    //Template_Start_Code_DontOverwrite_1

    Task<long> DuplicateComponentTreeAsync(long componentId);
    Task DeleteComponentTreeAsync(long componentId);
    Task DeployToAzureStaging(long id);

    //Template_End_Code_DontOverwrite_1

    Task<List<ComponentSystemAction>> UpdateComponentSystemActionMatrixAsync(long parentId, List<ComponentSystemAction> matrix);
Task<List<ComponentBusinessAction>> UpdateComponentBusinessActionMatrixAsync(long parentId, List<ComponentBusinessAction> matrix);
//Template_Field_ChildrenMatrix_AddIServiceEntry
}
