using Bfs.Core.Data;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Data.Interfaces;
using Bfs.StockEx.Data.Models;
using Microsoft.EntityFrameworkCore;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Data.Repositories
{
    public class StockShareRepository : SqlRepository<StockShareEntity, StockExDbContext>, IStockShareRepository
    {
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        private readonly StockExDbContext _context;
        public StockShareRepository(StockExDbContext dbContext, IScopeData scopeData
//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3

        ) : base(dbContext, scopeData)
        {
            _context = dbContext;
//Template_Start_Code_DontOverwrite_4
//Template_End_Code_DontOverwrite_4

        }

//Template_Start_Code_DontOverwrite_5
//Template_End_Code_DontOverwrite_5
    }
}
