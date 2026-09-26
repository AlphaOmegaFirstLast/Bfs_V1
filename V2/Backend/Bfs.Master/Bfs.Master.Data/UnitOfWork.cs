using Bfs.Core.Interfaces;
using Bfs.Master.Data.Interfaces;
using Bfs.Master.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace Bfs.Master.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly MasterDbContext _context;
private readonly IBfsComponentSystemActionRepository _bfsComponentSystemActionRepo;

private readonly IBfsComponentBusinessActionRepository _bfsComponentBusinessActionRepo;

private readonly IBfsTenantSystemRepository _bfsTenantSystemRepo;

//Template_Field_ChildrenMatrix_AddDeclareEntry

    public UnitOfWork(MasterDbContext dbContext, IScopeData scopeData
        , IBfsComponentSystemActionRepository bfsComponentSystemActionRepo

        , IBfsComponentBusinessActionRepository bfsComponentBusinessActionRepo

        , IBfsTenantSystemRepository bfsTenantSystemRepo

//Template_Field_ChildrenMatrix_AddParameterEntry

    )
    {
        _context = dbContext;
 _bfsComponentSystemActionRepo = bfsComponentSystemActionRepo;

 _bfsComponentBusinessActionRepo = bfsComponentBusinessActionRepo;

 _bfsTenantSystemRepo = bfsTenantSystemRepo;

//Template_Field_ChildrenMatrix_AddInitEntry
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

    //public async Task MergeStockShares(long stockShareId, int factor)
    //{
    //    var stockShareList = await _context.SspStocks.Where(x => x.StockShareId == stockShareId).ToListAsync();
    //    stockShareList.ForEach(stockShare =>
    //    {
    //        stockShare.Quantity = stockShare.Quantity / factor;
    //        stockShare.AverageCost = stockShare.Quantity * factor;
    //    });

    //    await _context.SaveChangesAsync();
    //}
}
