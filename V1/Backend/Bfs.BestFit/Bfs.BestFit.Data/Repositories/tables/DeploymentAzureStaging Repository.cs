using Bfs.Core.Data;
using Bfs.Core.Interfaces;
using Bfs.BestFit.Data.Interfaces;
using Bfs.BestFit.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Bfs.BestFit.Data.Repositories
{
    public class DeploymentAzureStagingRepository : SqlRepository<DeploymentAzureStagingEntity, BestFitDbContext>, IDeploymentAzureStagingRepository
    {
        private readonly BestFitDbContext _context;
        public DeploymentAzureStagingRepository(BestFitDbContext dbContext, IScopeData scopeData) : base(dbContext, scopeData)
        {
            _context = dbContext;
        }

        //Template_Start_DontOverwrite_1
        //Template_End_DontOverwrite_1
    }
}
