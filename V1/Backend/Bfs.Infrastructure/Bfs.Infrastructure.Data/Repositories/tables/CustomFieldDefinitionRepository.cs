using Bfs.Core.Data;
using Bfs.Core.Interfaces;
using Bfs.Infrastructure.Data.Interfaces;
using Bfs.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Bfs.Infrastructure.Data.Repositories
{
    public class CustomFieldDefinitionRepository : SqlRepository<CustomFieldDefinitionEntity, InfrastructureDbContext>, ICustomFieldDefinitionRepository
    {
        private readonly InfrastructureDbContext _context;
        public CustomFieldDefinitionRepository(InfrastructureDbContext dbContext, IScopeData scopeData) : base(dbContext, scopeData)
        {
            _context = dbContext;
        }

        //Template_Start_DontOverwrite_1
        //Template_End_DontOverwrite_1
    }
}
