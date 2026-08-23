using Bfs.Core.Data;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Data.Interfaces;
using Bfs.StockEx.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Bfs.StockEx.Data.Repositories
{
    public class StockShareRepository : SqlRepository<StockShareEntity, StockExDbContext>, IStockShareRepository
    {
        private readonly StockExDbContext _context;
        public StockShareRepository(StockExDbContext dbContext, IScopeData scopeData) : base(dbContext, scopeData)
        {
            _context = dbContext;
        }

        //Template_Start_DontOverwrite_1
        //Template_End_DontOverwrite_1
    }
}
