using Bfs.Core.Interfaces;
using Bfs.Master.Data.Models;

namespace Bfs.Master.Data.Interfaces
{
    public interface IBfsFieldRepository : IRepository<BfsFieldEntity>
    {
        //Template_Start_Code_DontOverwrite_1
        Task<List<BfsFieldEntity>> GetByComponentIdAsync(long componentId);
        Task DeleteByComponentIdAsync(long componentId);
        //Template_End_Code_DontOverwrite_1

    }
}
