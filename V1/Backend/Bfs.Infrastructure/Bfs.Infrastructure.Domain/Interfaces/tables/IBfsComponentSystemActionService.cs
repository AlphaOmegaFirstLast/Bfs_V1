using Bfs.Core.Contracts;
using Bfs.Infrastructure.Contracts;

namespace Bfs.Infrastructure.Domain.Interfaces
{
    public interface IBfsComponentSystemActionService
    {
        Task<BfsComponentSystemAction?> GetAsync(long id);
        Task<List<BfsComponentSystemAction>> GetAsync();

        Task<BfsComponentSystemAction> CreateAsync(BfsComponentSystemAction contract);
        Task<BfsComponentSystemAction?> UpdateAsync(BfsComponentSystemAction contract);
        Task DeleteAsync(long id);
        Task<BfsComponentSystemAction> UploadAsync(BfsComponentSystemAction contract);

        Task<QueryResponse<BfsComponentSystemActionListItem>> ListAsync(QueryRequest<BfsComponentSystemActionListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
