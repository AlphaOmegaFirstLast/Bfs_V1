using Bfs.Infrastructure.Contracts;

namespace Bfs.Infrastructure.Domain.Interfaces;

public interface IOperationsService
{
    //Template_Start_Code_DontOverwrite_1

    Task<long> DuplicateComponentTreeAsync(long componentId);
    Task DeleteComponentTreeAsync(long componentId);
    Task DeployToAzureStaging(long id);

    //Template_End_Code_DontOverwrite_1

    Task<List<BfsComponentSystemAction>> UpdateBfsComponentSystemActionMatrixAsync(long parentId, List<BfsComponentSystemAction> matrix);
    Task<List<BfsComponentBusinessAction>> UpdateBfsComponentBusinessActionMatrixAsync(long parentId, List<BfsComponentBusinessAction> matrix);
    //Template_Field_ChildrenMatrix_AddIServiceEntry
}
