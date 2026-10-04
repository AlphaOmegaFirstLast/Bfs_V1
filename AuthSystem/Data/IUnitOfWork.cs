using Bfs.Auth.Data.Models;

namespace Bfs.Auth.Data.Repositories;

public interface IUnitOfWork
{
    Task<List<RoleComponentSystemActionEntity>> UpdateRoleComponentSystemActionMatrixAsync(long parentId, List<RoleComponentSystemActionEntity> matrix);
//Template_Field_ChildrenMatrix_AddIUnitOfWorkEntry
}
