using Bfs.Core.Data;
using Bfs.Core.Interfaces;
using Bfs.Stores.Data.Interfaces;
using Bfs.Stores.Data.Models;
using Microsoft.EntityFrameworkCore;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Stores.Data.Repositories
{
    public class StoreRepository : SqlRepository<StoreEntity, StoresDbContext>, IStoreRepository
    {
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        private readonly StoresDbContext _context;
        public StoreRepository(StoresDbContext dbContext, IScopeData scopeData
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
