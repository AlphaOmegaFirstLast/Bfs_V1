using Bfs.Master.Data.Models;

namespace Bfs.Master.Data.Repositories;

public interface IUnitOfWork
{
    Task<List<BfsComponentSystemActionEntity>> UpdateBfsComponentSystemActionMatrixAsync(long parentId, List<BfsComponentSystemActionEntity> matrix);
Task<List<BfsComponentBusinessActionEntity>> UpdateBfsComponentBusinessActionMatrixAsync(long parentId, List<BfsComponentBusinessActionEntity> matrix);
Task<List<BfsTenantSystemEntity>> UpdateBfsTenantSystemMatrixAsync(long parentId, List<BfsTenantSystemEntity> matrix);
//Template_Field_ChildrenMatrix_AddIUnitOfWorkEntry
}
