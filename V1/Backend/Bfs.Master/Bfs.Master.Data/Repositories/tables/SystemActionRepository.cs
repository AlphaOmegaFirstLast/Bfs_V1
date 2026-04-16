using Bfs.Core.Data;
using Bfs.Core.Interfaces;
using Bfs.Master.Data.Interfaces;
using Bfs.Master.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Bfs.Master.Data.Repositories
{
    public class SystemActionRepository : SqlRepository<SystemActionEntity, MasterDbContext>, ISystemActionRepository
    {
        private readonly MasterDbContext _context;
        public SystemActionRepository(MasterDbContext dbContext, IScopeData scopeData) : base(dbContext, scopeData)
        {
            _context = dbContext;
        }

        //Template_Start_DontOverwrite_1
        //Template_End_DontOverwrite_1
    }
}
