using Bfs.Auth.Contracts;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Auth.Domain.Interfaces;

public interface IOperationsService
{
Task<List<RoleComponentSystemAction>> UpdateRoleComponentSystemActionMatrixAsync(long parentId, List<RoleComponentSystemAction> matrix);
//Template_Field_ChildrenMatrix_AddIServiceEntry
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2
}
