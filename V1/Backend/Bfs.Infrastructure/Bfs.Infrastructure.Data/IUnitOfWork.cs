using Bfs.Infrastructure.Data.Models;

namespace Bfs.Infrastructure.Data.Repositories;

public interface IUnitOfWork
{
    Task<List<BfsComponentSystemActionEntity>> UpdateBfsComponentSystemActionMatrixAsync(long parentId, List<BfsComponentSystemActionEntity> matrix);
Task<List<BfsComponentBusinessActionEntity>> UpdateBfsComponentBusinessActionMatrixAsync(long parentId, List<BfsComponentBusinessActionEntity> matrix);
//Template_Field_ChildrenMatrix_AddIUnitOfWorkEntry
}
