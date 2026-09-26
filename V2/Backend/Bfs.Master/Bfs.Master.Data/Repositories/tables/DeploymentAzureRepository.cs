using Bfs.Core.Data;
using Bfs.Core.Interfaces;
using Bfs.Master.Data.Interfaces;
using Bfs.Master.Data.Models;
using Microsoft.EntityFrameworkCore;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Master.Data.Repositories
{
    public class DeploymentAzureRepository : SqlRepository<DeploymentAzureEntity, MasterDbContext>, IDeploymentAzureRepository
    {
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        private readonly MasterDbContext _context;
        public DeploymentAzureRepository(MasterDbContext dbContext, IScopeData scopeData
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
