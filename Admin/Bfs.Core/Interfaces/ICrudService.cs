using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bfs.Core.Interfaces
{
    public interface ICrudService<T>  
    {
        Task<T?> GetAsync(long id);
        Task<List<T>> GetAsync();

        Task<T> CreateAsync(T contract);
        Task<T?> UpdateAsync(T contract);
        Task DeleteAsync(long id);
    }
}
