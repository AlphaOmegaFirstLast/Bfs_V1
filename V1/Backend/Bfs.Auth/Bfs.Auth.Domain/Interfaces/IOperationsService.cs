using Bfs.Auth.Contracts;

namespace Bfs.Auth.Domain.Interfaces;

public interface IOperationsService
{
Task<List<AuthRoleComponentSystemAction>> UpdateAuthRoleComponentSystemActionMatrixAsync(long parentId, List<AuthRoleComponentSystemAction> matrix);
//Template_Field_ChildrenMatrix_AddIServiceEntry
}
