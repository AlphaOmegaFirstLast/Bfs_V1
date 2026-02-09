using Bfs.Infrastructure.Contracts;

namespace Bfs.Infrastructure.Domain.Interfaces;

public interface IOperationsService
{
Task<List<BfsComponentSystemAction>> UpdateBfsComponentSystemActionMatrixAsync(long parentId, List<BfsComponentSystemAction> matrix);
Task<List<BfsComponentBusinessAction>> UpdateBfsComponentBusinessActionMatrixAsync(long parentId, List<BfsComponentBusinessAction> matrix);
//Template_Field_ChildrenMatrix_AddIServiceEntry
}
