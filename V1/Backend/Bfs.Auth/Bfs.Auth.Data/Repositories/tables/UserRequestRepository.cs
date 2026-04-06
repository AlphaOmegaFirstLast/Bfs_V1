using Bfs.Core.Data;
using Bfs.Core.Interfaces;
using Bfs.Auth.Data.Interfaces;
using Bfs.Auth.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Bfs.Auth.Data.Repositories
{
    public class UserRequestRepository : SqlRepository<UserRequestEntity, AuthDbContext>, IUserRequestRepository
    {
        private readonly AuthDbContext _context;
        public UserRequestRepository(AuthDbContext dbContext, IScopeData scopeData) : base(dbContext, scopeData)
        {
            _context = dbContext;
        }

        //Template_Start_DontOverwrite_1
        //Template_End_DontOverwrite_1
    }
}

