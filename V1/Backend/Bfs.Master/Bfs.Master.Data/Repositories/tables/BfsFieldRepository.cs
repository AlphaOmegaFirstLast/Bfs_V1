using Bfs.Core.Data;
using Bfs.Core.Interfaces;
using Bfs.Master.Data.Interfaces;
using Bfs.Master.Data.Models;
using Microsoft.EntityFrameworkCore;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Master.Data.Repositories
{
    public class BfsFieldRepository : SqlRepository<BfsFieldEntity, MasterDbContext>, IBfsFieldRepository
    {
        private readonly MasterDbContext _context;
        //Template_Start_Code_DontOverwrite_2
        //Template_End_Code_DontOverwrite_2

        public BfsFieldRepository(MasterDbContext dbContext, IScopeData scopeData
            //Template_Start_Code_DontOverwrite_3
            //Template_End_Code_DontOverwrite_3

            ) : base(dbContext, scopeData)
        {
            _context = dbContext;
            //Template_Start_Code_DontOverwrite_4
            //Template_End_Code_DontOverwrite_4

        }

        //Template_Start_Code_DontOverwrite_5
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
        //Template_End_Code_DontOverwrite_5
    }
}
