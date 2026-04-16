using Bfs.Core.Contracts;
using Bfs.Master.Contracts;

namespace Bfs.Master.Domain.Interfaces
{
    public interface IBfsFieldService
    {
        Task<BfsField?> GetAsync(long id);
        Task<List<BfsField>> GetAsync();

        Task<BfsField> CreateAsync(BfsField contract);
        Task<BfsField?> UpdateAsync(BfsField contract);
        Task DeleteAsync(long id);
        Task<BfsField> UploadAsync(BfsField contract);

        Task<QueryResponse<BfsFieldListItem>> ListAsync(QueryRequest<BfsFieldListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
