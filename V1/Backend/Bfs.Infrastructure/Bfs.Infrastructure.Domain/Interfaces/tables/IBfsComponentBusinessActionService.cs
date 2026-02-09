using Bfs.Core.Contracts;
using Bfs.Infrastructure.Contracts;

namespace Bfs.Infrastructure.Domain.Interfaces
{
    public interface IBfsComponentBusinessActionService
    {
        Task<BfsComponentBusinessAction?> GetAsync(long id);
        Task<List<BfsComponentBusinessAction>> GetAsync();

        Task<BfsComponentBusinessAction> CreateAsync(BfsComponentBusinessAction contract);
        Task<BfsComponentBusinessAction?> UpdateAsync(BfsComponentBusinessAction contract);
        Task DeleteAsync(long id);
        Task<BfsComponentBusinessAction> UploadAsync(BfsComponentBusinessAction contract);

        Task<QueryResponse<BfsComponentBusinessActionListItem>> ListAsync(QueryRequest<BfsComponentBusinessActionListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
