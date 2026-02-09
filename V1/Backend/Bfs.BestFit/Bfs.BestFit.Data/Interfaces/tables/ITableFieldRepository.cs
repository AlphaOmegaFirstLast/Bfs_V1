using Bfs.BestFit.Data.Models;
using Bfs.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bfs.BestFit.Data.Interfaces
{
    public interface ITableFieldRepository : IRepository<TableFieldEntity>
    {
        //Template_Start_Code_DontOverwrite_1
        Task<List<TableFieldEntity>> GetByComponentIdAsync(long componentId);
        Task DeleteByComponentIdAsync(long componentId);

        //Template_Start_Code_DontOverwrite_1
    }
}
