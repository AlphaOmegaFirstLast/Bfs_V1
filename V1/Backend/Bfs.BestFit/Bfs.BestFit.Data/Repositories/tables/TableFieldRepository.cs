using Bfs.Core.Data;
using Bfs.Core.Interfaces;
using Bfs.BestFit.Data.Interfaces;
using Bfs.BestFit.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Bfs.BestFit.Data.Repositories
{
    public class TableFieldRepository : SqlRepository<TableFieldEntity, BestFitDbContext>, ITableFieldRepository
    {
        private readonly BestFitDbContext _context;
        public TableFieldRepository(BestFitDbContext dbContext, IScopeData scopeData) : base(dbContext, scopeData)
        {
            _context = dbContext;
        }

        //Template_Start_Code_DontOverwrite_1
        public async Task<List<TableFieldEntity>> GetByComponentIdAsync(long componentId)
        {
            return await DbSet.Where(e => e.ComponentId == componentId).ToListAsync().ConfigureAwait(false);
        }

        public async Task DeleteByComponentIdAsync(long componentId)
        {
            // Remove existing actions for this component
            var existingList = _context.TableFields.Where(x => x.ComponentId == componentId);

            _context.TableFields.RemoveRange(existingList);
        }
        //Template_End_Code_DontOverwrite_1
    }
}
