using Bfs.Core.Data;
using Bfs.Core.Interfaces;
using Bfs.Auth.Data.Interfaces;
using Bfs.Auth.Data.Models;
using Microsoft.EntityFrameworkCore;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Auth.Data.Repositories
{
    public class ResourceRuleRepository : SqlRepository<ResourceRuleEntity, AuthDbContext>, IResourceRuleRepository
    {
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        private readonly AuthDbContext _context;
        public ResourceRuleRepository(AuthDbContext dbContext, IScopeData scopeData
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
