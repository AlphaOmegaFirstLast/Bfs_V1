using Bfs.Core.Contracts;
using Bfs.Infrastructure.Contracts;

namespace Bfs.Infrastructure.Domain.Interfaces
{
    public interface IBfsClientService
    {
        Task<BfsClient?> GetAsync(long id);
        Task<List<BfsClient>> GetAsync();

        Task<BfsClient> CreateAsync(BfsClient contract);
        Task<BfsClient?> UpdateAsync(BfsClient contract);
        Task DeleteAsync(long id);
        Task<BfsClient> UploadAsync(BfsClient contract);

        Task<QueryResponse<BfsClientListItem>> ListAsync(QueryRequest<BfsClientListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1
//Template_Start_Code_DontOverwrite_2

//Template_End_Code_DontOverwrite_2

