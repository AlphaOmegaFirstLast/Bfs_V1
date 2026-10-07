using Bfs.Auth.Data.Models;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Auth.Data.Repositories;

public interface IUnitOfWork
{
    AuthDbContext _context { get;}
    Task<List<RoleComponentSystemActionEntity>> UpdateRoleComponentSystemActionMatrixAsync(long parentId, List<RoleComponentSystemActionEntity> matrix);

//Template_Field_ChildrenMatrix_AddIUnitOfWorkEntry

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2
}
