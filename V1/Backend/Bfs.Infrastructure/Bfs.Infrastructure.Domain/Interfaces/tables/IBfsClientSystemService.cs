using Bfs.Core.Contracts;
using Bfs.Infrastructure.Contracts;

namespace Bfs.Infrastructure.Domain.Interfaces
{
    public interface IBfsClientSystemService
    {
        Task<BfsClientSystem?> GetAsync(long id);
        Task<List<BfsClientSystem>> GetAsync();

        Task<BfsClientSystem> CreateAsync(BfsClientSystem contract);
        Task<BfsClientSystem?> UpdateAsync(BfsClientSystem contract);
        Task DeleteAsync(long id);
        Task<BfsClientSystem> UploadAsync(BfsClientSystem contract);

        Task<QueryResponse<BfsClientSystemListItem>> ListAsync(QueryRequest<BfsClientSystemListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1

