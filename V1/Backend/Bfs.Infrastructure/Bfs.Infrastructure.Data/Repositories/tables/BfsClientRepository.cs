using Bfs.Core.Data;
using Bfs.Core.Interfaces;
using Bfs.Infrastructure.Data.Interfaces;
using Bfs.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Bfs.Infrastructure.Data.Repositories
{
    public class BfsClientRepository : SqlRepository<BfsClientEntity, InfrastructureDbContext>, IBfsClientRepository
    {
        private readonly InfrastructureDbContext _context;
        public BfsClientRepository(InfrastructureDbContext dbContext, IScopeData scopeData) : base(dbContext, scopeData)
        {
            _context = dbContext;
        }

        //Template_Start_DontOverwrite_1
        //Template_End_DontOverwrite_1
    }
}

//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1
//Template_Start_Code_DontOverwrite_2

//Template_End_Code_DontOverwrite_2

