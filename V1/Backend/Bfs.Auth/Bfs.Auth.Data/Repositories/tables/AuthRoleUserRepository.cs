using Bfs.Core.Data;
using Bfs.Core.Interfaces;
using Bfs.Auth.Data.Interfaces;
using Bfs.Auth.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Bfs.Auth.Data.Repositories
{
    public class AuthRoleUserRepository : SqlRepository<AuthRoleUserEntity, AuthDbContext>, IAuthRoleUserRepository
    {
        private readonly AuthDbContext _context;
        public AuthRoleUserRepository(AuthDbContext dbContext, IScopeData scopeData) : base(dbContext, scopeData)
        {
            _context = dbContext;
        }

        //Template_Start_DontOverwrite_1
        //Template_End_DontOverwrite_1
    }
}
