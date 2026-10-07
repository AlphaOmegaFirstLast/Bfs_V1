using Bfs.Core.Interfaces;
using Bfs.Auth.Data.Interfaces;
using Bfs.Auth.Data.Models;

using Microsoft.EntityFrameworkCore;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Auth.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public AuthDbContext _context {get;}
private readonly IRoleComponentSystemActionRepository _roleComponentSystemActionRepo;

//Template_Field_ChildrenMatrix_AddDeclareEntry

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    public UnitOfWork(AuthDbContext dbContext, IScopeData scopeData
        , IRoleComponentSystemActionRepository roleComponentSystemActionRepo

//Template_Field_ChildrenMatrix_AddParameterEntry

//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3
    )
    {
        _context = dbContext;
 _roleComponentSystemActionRepo = roleComponentSystemActionRepo;

//Template_Field_ChildrenMatrix_AddInitEntry

//Template_Start_Code_DontOverwrite_4
//Template_End_Code_DontOverwrite_4
    }

public async Task<List<RoleComponentSystemActionEntity>> UpdateRoleComponentSystemActionMatrixAsync(long parentId, List<RoleComponentSystemActionEntity> matrix)
{
    // Remove existing matrix entries for this parentId
    var existingEntries = _context.RoleComponentSystemActions.Where(x => x.RoleId == parentId);

    _context.RoleComponentSystemActions.RemoveRange(existingEntries);

    // Add new Entries
    foreach (var matrixEntity in matrix)
    {
        await _roleComponentSystemActionRepo.CreateAsync(matrixEntity);  // it sets id & tenantId and add entity to the DbSet
    }

    await _context.SaveChangesAsync();

    // Return updated list
    return await _context.RoleComponentSystemActions.Where(x => x.RoleId  == parentId).ToListAsync();
}

//Template_Field_ChildrenMatrix_AddUnitOfWorkEntry

//Template_Start_Code_DontOverwrite_5
//Template_End_Code_DontOverwrite_5
}

//Template_Start_Code_DontOverwrite_6
//Template_End_Code_DontOverwrite_6