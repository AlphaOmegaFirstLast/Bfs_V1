using Bfs.Core.Interfaces;
using Bfs.Stores.Data.Interfaces;
using Bfs.Stores.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace Bfs.Stores.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly StoresDbContext _context;
//Template_Field_ChildrenMatrix_AddDeclareEntry

    public UnitOfWork(StoresDbContext dbContext, IScopeData scopeData
//Template_Field_ChildrenMatrix_AddParameterEntry

    )
    {
        _context = dbContext;
//Template_Field_ChildrenMatrix_AddInitEntry
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

