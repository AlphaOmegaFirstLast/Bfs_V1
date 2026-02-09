using Bfs.Core.Contracts;
using Bfs.Infrastructure.Contracts;

namespace Bfs.Infrastructure.Domain.Interfaces
{
    public interface IBfsSystemService
    {
        Task<BfsSystem?> GetAsync(long id);
        Task<List<BfsSystem>> GetAsync();

        Task<BfsSystem> CreateAsync(BfsSystem contract);
        Task<BfsSystem?> UpdateAsync(BfsSystem contract);
        Task DeleteAsync(long id);
        Task<BfsSystem> UploadAsync(BfsSystem contract);

        Task<QueryResponse<BfsSystemListItem>> ListAsync(QueryRequest<BfsSystemListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
