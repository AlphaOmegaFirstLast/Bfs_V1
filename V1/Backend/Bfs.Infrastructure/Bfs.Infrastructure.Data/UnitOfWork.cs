using Bfs.Core.Interfaces;
using Bfs.Infrastructure.Data.Interfaces;
using Bfs.Infrastructure.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace Bfs.Infrastructure.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly InfrastructureDbContext _context;
private readonly IBfsComponentSystemActionRepository _bfsComponentSystemActionRepo;

private readonly IBfsComponentBusinessActionRepository _bfsComponentBusinessActionRepo;

//Template_Field_ChildrenMatrix_AddDeclareEntry

    public UnitOfWork(InfrastructureDbContext dbContext, IScopeData scopeData
        , IBfsComponentSystemActionRepository bfsComponentSystemActionRepo

        , IBfsComponentBusinessActionRepository bfsComponentBusinessActionRepo

//Template_Field_ChildrenMatrix_AddParameterEntry

    )
    {
        _context = dbContext;
 _bfsComponentSystemActionRepo = bfsComponentSystemActionRepo;

 _bfsComponentBusinessActionRepo = bfsComponentBusinessActionRepo;

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
