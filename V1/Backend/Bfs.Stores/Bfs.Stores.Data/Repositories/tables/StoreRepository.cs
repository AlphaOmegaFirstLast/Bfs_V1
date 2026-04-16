using Bfs.Core.Data;
using Bfs.Core.Interfaces;
using Bfs.Stores.Data.Interfaces;
using Bfs.Stores.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Bfs.Stores.Data.Repositories
{
    public class StoreRepository : SqlRepository<StoreEntity, StoresDbContext>, IStoreRepository
    {
        private readonly StoresDbContext _context;
        public StoreRepository(StoresDbContext dbContext, IScopeData scopeData) : base(dbContext, scopeData)
        {
            _context = dbContext;
        }

        //Template_Start_DontOverwrite_1
        //Template_End_DontOverwrite_1
    }
}

