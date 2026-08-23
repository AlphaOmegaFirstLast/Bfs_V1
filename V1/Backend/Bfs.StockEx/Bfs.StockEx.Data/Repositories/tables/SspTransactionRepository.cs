using Bfs.Core.Data;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Data.Interfaces;
using Bfs.StockEx.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Bfs.StockEx.Data.Repositories
{
    public class SspTransactionRepository : SqlRepository<SspTransactionEntity, StockExDbContext>, ISspTransactionRepository
    {
        private readonly StockExDbContext _context;
        public SspTransactionRepository(StockExDbContext dbContext, IScopeData scopeData) : base(dbContext, scopeData)
        {
            _context = dbContext;
        }

        //Template_Start_DontOverwrite_1
        //Template_End_DontOverwrite_1
    }
}

