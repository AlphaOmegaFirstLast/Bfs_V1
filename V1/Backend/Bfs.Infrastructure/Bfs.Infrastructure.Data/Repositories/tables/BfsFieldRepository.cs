using Bfs.Core.Data;
using Bfs.Core.Interfaces;
using Bfs.Infrastructure.Data.Interfaces;
using Bfs.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Bfs.Infrastructure.Data.Repositories
{
    public class BfsFieldRepository : SqlRepository<BfsFieldEntity, InfrastructureDbContext>, IBfsFieldRepository
    {
        private readonly InfrastructureDbContext _context;
        public BfsFieldRepository(InfrastructureDbContext dbContext, IScopeData scopeData) : base(dbContext, scopeData)
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
            // Remove existing actions for this component
            var existingList = DbSet.Where(x => x.BfsComponentId == componentId);

            DbSet.RemoveRange(existingList);
        }
        //Template_End_DontOverwrite_1
    }
}

