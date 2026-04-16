using Bfs.Core.Data;
using Bfs.Core.Interfaces;
using Bfs.Master.Data.Interfaces;
using Bfs.Master.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Bfs.Master.Data.Repositories
{
    public class BfsFieldRepository : SqlRepository<BfsFieldEntity, MasterDbContext>, IBfsFieldRepository
    {
        private readonly MasterDbContext _context;
        public BfsFieldRepository(MasterDbContext dbContext, IScopeData scopeData) : base(dbContext, scopeData)
        {
            _context = dbContext;
        }

        //Template_Start_DontOverwrite_1
        public async Task<List<BfsFieldEntity>> GetByComponentIdAsync(long componentId)
        {
            return await DbSet.Where(e => e.BfsComponentId == componentId).ToListAsync().ConfigureAwait(false);
        }

        public async Task DeleteByComponentIdAsync(long componentId)
        {
            // Remove existing fields for this component
            var existingList = DbSet.Where(x => x.BfsComponentId == componentId);

            DbSet.RemoveRange(existingList);
        }
        //Template_End_DontOverwrite_1
    }
}
