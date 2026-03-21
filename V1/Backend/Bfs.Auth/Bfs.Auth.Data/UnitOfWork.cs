using Bfs.Core.Interfaces;
using Bfs.Auth.Data.Interfaces;
using Bfs.Auth.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace Bfs.Auth.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AuthDbContext _context;
private readonly IAuthRoleComponentSystemActionRepository _authRoleComponentSystemActionRepo;

//Template_Field_ChildrenMatrix_AddDeclareEntry

    public UnitOfWork(AuthDbContext dbContext, IScopeData scopeData
        , IAuthRoleComponentSystemActionRepository authRoleComponentSystemActionRepo

//Template_Field_ChildrenMatrix_AddParameterEntry

    )
    {
        _context = dbContext;
 _authRoleComponentSystemActionRepo = authRoleComponentSystemActionRepo;

//Template_Field_ChildrenMatrix_AddInitEntry
    }

 public async Task<List<AuthRoleComponentSystemActionEntity>> UpdateAuthRoleComponentSystemActionMatrixAsync(long parentId, List<AuthRoleComponentSystemActionEntity> matrix)
{
    // Remove existing matrix entries for this parentId
    var existingEntries = _context.AuthRoleComponentSystemActions.Where(x => x.AuthRoleId == parentId);

    _context.AuthRoleComponentSystemActions.RemoveRange(existingEntries);

    // Add new Entries
    foreach (var matrixEntity in matrix)
    {
        await _authRoleComponentSystemActionRepo.CreateAsync(matrixEntity);  // it sets id & tenantId and add entity to the DbSet
    }

    await _context.SaveChangesAsync();

    // Return updated list
    return await _context.AuthRoleComponentSystemActions.Where(x => x.AuthRoleId  == parentId).ToListAsync();
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
