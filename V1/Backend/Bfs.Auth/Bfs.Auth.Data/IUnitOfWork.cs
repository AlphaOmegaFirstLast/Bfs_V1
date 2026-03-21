using Bfs.Auth.Data.Models;

namespace Bfs.Auth.Data.Repositories;

public interface IUnitOfWork
{
    Task<List<AuthRoleComponentSystemActionEntity>> UpdateAuthRoleComponentSystemActionMatrixAsync(long parentId, List<AuthRoleComponentSystemActionEntity> matrix);
//Template_Field_ChildrenMatrix_AddIUnitOfWorkEntry
}
