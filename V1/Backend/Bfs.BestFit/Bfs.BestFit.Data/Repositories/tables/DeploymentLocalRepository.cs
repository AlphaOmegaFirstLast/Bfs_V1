using Bfs.Core.Data;
using Bfs.Core.Interfaces;
using Bfs.BestFit.Data.Interfaces;
using Bfs.BestFit.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Bfs.BestFit.Data.Repositories
{
    public class DeploymentLocalRepository : SqlRepository<DeploymentLocalEntity, BestFitDbContext>, IDeploymentLocalRepository
    {
        private readonly BestFitDbContext _context;
        public DeploymentLocalRepository(BestFitDbContext dbContext, IScopeData scopeData) : base(dbContext, scopeData)
        {
            _context = dbContext;
        }

        //Template_MatrixField_AddRepositoryEntry

        //Template_Start_DontOverwrite_1
        //Template_End_DontOverwrite_1
    }
}
