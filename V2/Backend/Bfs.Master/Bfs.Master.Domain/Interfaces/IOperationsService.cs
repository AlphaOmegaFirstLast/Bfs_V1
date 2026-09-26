using Bfs.Master.Contracts;

namespace Bfs.Master.Domain.Interfaces;

public interface IOperationsService
{
Task<List<BfsComponentSystemAction>> UpdateBfsComponentSystemActionMatrixAsync(long parentId, List<BfsComponentSystemAction> matrix);
Task<List<BfsComponentBusinessAction>> UpdateBfsComponentBusinessActionMatrixAsync(long parentId, List<BfsComponentBusinessAction> matrix);
Task<List<BfsTenantSystem>> UpdateBfsTenantSystemMatrixAsync(long parentId, List<BfsTenantSystem> matrix);
//Template_Field_ChildrenMatrix_AddIServiceEntry
}
