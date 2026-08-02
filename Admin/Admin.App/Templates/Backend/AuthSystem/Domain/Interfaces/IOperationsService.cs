using Bfs.Auth.Contracts;

namespace Bfs.Auth.Domain.Interfaces;

public interface IOperationsService
{
Task<List<RoleComponentSystemAction>> UpdateRoleComponentSystemActionMatrixAsync(long parentId, List<RoleComponentSystemAction> matrix);
//Template_Field_ChildrenMatrix_AddIServiceEntry
}
