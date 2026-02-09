using Bfs.BestFit.Data.Interfaces;
using Bfs.BestFit.Data.Models;
using Bfs.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bfs.BestFit.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public BestFitDbContext _context { get; set; }

    //Template_Start_Code_DontOverwrite_1
    public IComponentRepository ComponentRepo { get; set; }
    public ITableFieldRepository TableFieldRepo { get; set; }
    public IDeploymentAzureStagingRepository DeploymentAzureStagingRepo { get; set; }
    //Template_End_Code_DontOverwrite_1

    private readonly IComponentSystemActionRepository _componentSystemActionRepo;
    private readonly IComponentBusinessActionRepository _componentBusinessActionRepo;

    //Template_Field_ChildrenMatrix_AddDeclareEntry

    public UnitOfWork(BestFitDbContext dbContext, IScopeData scopeData
    //Template_Start_Code_DontOverwrite_2

        , IComponentRepository componentRepo
        , ITableFieldRepository tableFieldRepo
        , IDeploymentAzureStagingRepository deploymentAzureStagingRepo
    //Template_End_Code_DontOverwrite_2

        , IComponentSystemActionRepository componentSystemActionRepo
        , IComponentBusinessActionRepository componentBusinessActionRepo

    //Template_Field_ChildrenMatrix_AddParameterEntry

    )
    {
        _context = dbContext;

        ComponentRepo = componentRepo;
        TableFieldRepo = tableFieldRepo;
        DeploymentAzureStagingRepo = deploymentAzureStagingRepo;
        _componentSystemActionRepo = componentSystemActionRepo;
        _componentBusinessActionRepo = componentBusinessActionRepo;

        //Template_Field_ChildrenMatrix_AddInitEntry
    }

    public async Task<List<ComponentSystemActionEntity>> UpdateComponentSystemActionMatrixAsync(long parentId, List<ComponentSystemActionEntity> matrix)
    {
        // Remove existing matrix entries for this parentId
        var existingEntries = _context.ComponentSystemActions.Where(x => x.Id == parentId);

        _context.ComponentSystemActions.RemoveRange(existingEntries);

        // Add new Entries
        foreach (var matrixEntity in matrix)
        {
            await _componentSystemActionRepo.CreateAsync(matrixEntity);  // it sets id & tenantId and add entity to the DbSet
        }

        await _context.SaveChangesAsync();

        // Return updated list
        return await _context.ComponentSystemActions.Where(x => x.Id == parentId).ToListAsync();
    }

    public async Task<List<ComponentBusinessActionEntity>> UpdateComponentBusinessActionMatrixAsync(long parentId, List<ComponentBusinessActionEntity> matrix)
    {
        // Remove existing matrix entries for this parentId
        var existingEntries = _context.ComponentBusinessActions.Where(x => x.Id == parentId);

        _context.ComponentBusinessActions.RemoveRange(existingEntries);

        // Add new Entries
        foreach (var matrixEntity in matrix)
        {
            await _componentBusinessActionRepo.CreateAsync(matrixEntity);  // it sets id & tenantId and add entity to the DbSet
        }

        await _context.SaveChangesAsync();

        // Return updated list
        return await _context.ComponentBusinessActions.Where(x => x.Id == parentId).ToListAsync();
    }

//    public async Task<List<ComponentSystemActionEntity>> UpdateComponentSystemActionMatrixAsync(long parentId, List<ComponentSystemActionEntity> matrix)
//{
//    // Remove existing matrix entries for this parentId
//    var existingEntries = _context.ComponentSystemActions.Where(x => x.BestFitComponentId == parentId);

//    _context.ComponentSystemActions.RemoveRange(existingEntries);

//    // Add new Entries
//    foreach (var matrixEntity in matrix)
//    {
//        await _componentSystemActionRepo.CreateAsync(matrixEntity);  // it sets id & tenantId and add entity to the DbSet
//    }

//    await _context.SaveChangesAsync();

//    // Return updated list
//    return await _context.ComponentSystemActions.Where(x => x.BestFitComponentId  == parentId).ToListAsync();
//}
//public async Task<List<ComponentBusinessActionEntity>> UpdateComponentBusinessActionMatrixAsync(long parentId, List<ComponentBusinessActionEntity> matrix)
//{
//    // Remove existing matrix entries for this parentId
//    var existingEntries = _context.ComponentBusinessActions.Where(x => x.BestFitComponentId == parentId);

//    _context.ComponentBusinessActions.RemoveRange(existingEntries);

//    // Add new Entries
//    foreach (var matrixEntity in matrix)
//    {
//        await _componentBusinessActionRepo.CreateAsync(matrixEntity);  // it sets id & tenantId and add entity to the DbSet
//    }

//    await _context.SaveChangesAsync();

//    // Return updated list
//    return await _context.ComponentBusinessActions.Where(x => x.BestFitComponentId  == parentId).ToListAsync();
//}
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
