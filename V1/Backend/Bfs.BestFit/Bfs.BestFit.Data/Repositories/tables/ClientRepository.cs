using Bfs.Core.Data;
using Bfs.Core.Interfaces;
using Bfs.BestFit.Data.Interfaces;
using Bfs.BestFit.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Bfs.BestFit.Data.Repositories
{
    public class ClientRepository : SqlRepository<ClientEntity, BestFitDbContext>, IClientRepository
    {
        private readonly BestFitDbContext _context;
        public ClientRepository(BestFitDbContext dbContext, IScopeData scopeData) : base(dbContext, scopeData)
        {
            _context = dbContext;
        }

        //Template_Start_DontOverwrite_1
        //Template_End_DontOverwrite_1
    }
}
