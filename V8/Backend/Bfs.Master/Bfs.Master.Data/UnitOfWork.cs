using Bfs.Core.Interfaces;
using Bfs.Master.Data.Interfaces;
using Bfs.Master.Data.Models;

using Microsoft.EntityFrameworkCore;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Master.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public MasterDbContext _context {get;}
private readonly IBfsComponentSystemActionRepository _bfsComponentSystemActionRepo;

private readonly IBfsComponentBusinessActionRepository _bfsComponentBusinessActionRepo;

private readonly IBfsTenantSystemRepository _bfsTenantSystemRepo;

//Template_Field_ChildrenMatrix_AddDeclareEntry

//Template_Start_Code_DontOverwrite_2
public IBfsComponentRepository ComponentRepo { get; set; }
    public IBfsFieldRepository FieldRepo { get; set; }
    public IDeploymentAzureRepository DeploymentAzureRepo { get; set; }
    public IDeploymentLocalRepository DeploymentLocalRepo { get; set; }
//Template_End_Code_DontOverwrite_2

    public UnitOfWork(MasterDbContext dbContext, IScopeData scopeData
        , IBfsComponentSystemActionRepository bfsComponentSystemActionRepo

        , IBfsComponentBusinessActionRepository bfsComponentBusinessActionRepo

        , IBfsTenantSystemRepository bfsTenantSystemRepo

//Template_Field_ChildrenMatrix_AddParameterEntry

//Template_Start_Code_DontOverwrite_3
, IBfsComponentRepository componentRepo
        , IBfsFieldRepository fieldRepo
        , IDeploymentAzureRepository deploymentAzureRepo
        , IDeploymentLocalRepository deploymentLocalRepo
//Template_End_Code_DontOverwrite_3

    )
    {
        _context = dbContext;
 _bfsComponentSystemActionRepo = bfsComponentSystemActionRepo;

 _bfsComponentBusinessActionRepo = bfsComponentBusinessActionRepo;

 _bfsTenantSystemRepo = bfsTenantSystemRepo;

//Template_Field_ChildrenMatrix_AddInitEntry

//Template_Start_Code_DontOverwrite_4
_context = dbContext;
        ComponentRepo = componentRepo;
        FieldRepo = fieldRepo;
        DeploymentAzureRepo = deploymentAzureRepo;
        DeploymentLocalRepo = deploymentLocalRepo;
//Template_End_Code_DontOverwrite_4

    }

public async Task<List<BfsComponentSystemActionEntity>> UpdateBfsComponentSystemActionMatrixAsync(long parentId, List<BfsComponentSystemActionEntity> matrix)
{
    // Remove existing matrix entries for this parentId
    var existingEntries = _context.BfsComponentSystemActions.Where(x => x.BfsComponentId == parentId);

    _context.BfsComponentSystemActions.RemoveRange(existingEntries);

    // Add new Entries
    foreach (var matrixEntity in matrix)
    {
        await _bfsComponentSystemActionRepo.CreateAsync(matrixEntity);  // it sets id & tenantId and add entity to the DbSet
    }

    await _context.SaveChangesAsync();

    // Return updated list
    return await _context.BfsComponentSystemActions.Where(x => x.BfsComponentId  == parentId).ToListAsync();
}

public async Task<List<BfsComponentBusinessActionEntity>> UpdateBfsComponentBusinessActionMatrixAsync(long parentId, List<BfsComponentBusinessActionEntity> matrix)
{
    // Remove existing matrix entries for this parentId
    var existingEntries = _context.BfsComponentBusinessActions.Where(x => x.BfsComponentId == parentId);

    _context.BfsComponentBusinessActions.RemoveRange(existingEntries);

    // Add new Entries
    foreach (var matrixEntity in matrix)
    {
        await _bfsComponentBusinessActionRepo.CreateAsync(matrixEntity);  // it sets id & tenantId and add entity to the DbSet
    }

    await _context.SaveChangesAsync();

    // Return updated list
    return await _context.BfsComponentBusinessActions.Where(x => x.BfsComponentId  == parentId).ToListAsync();
}

public async Task<List<BfsTenantSystemEntity>> UpdateBfsTenantSystemMatrixAsync(long parentId, List<BfsTenantSystemEntity> matrix)
{
    // Remove existing matrix entries for this parentId
    var existingEntries = _context.BfsTenantSystems.Where(x => x.BfsTenantId == parentId);

    _context.BfsTenantSystems.RemoveRange(existingEntries);

    // Add new Entries
    foreach (var matrixEntity in matrix)
    {
        await _bfsTenantSystemRepo.CreateAsync(matrixEntity);  // it sets id & tenantId and add entity to the DbSet
    }

    await _context.SaveChangesAsync();

    // Return updated list
    return await _context.BfsTenantSystems.Where(x => x.BfsTenantId  == parentId).ToListAsync();
}

//Template_Field_ChildrenMatrix_AddUnitOfWorkEntry

//Template_Start_Code_DontOverwrite_5
//Template_End_Code_DontOverwrite_5

}
