using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace Bfs.Core.Interfaces
{
    public interface INewCrudService<T>  
    {
        Task<T?> GetAsync(long id);
        Task<List<T>> GetAsync();

        Task<T> CreateAsync(T contract, bool isCommit = true);
        Task<T?> UpdateAsync(T contract, bool isCommit = true);
        Task DeleteAsync(long id, bool isCommit = true);
        Task<T> UploadAsync(T contract, bool isCommit = true);
    }
}
