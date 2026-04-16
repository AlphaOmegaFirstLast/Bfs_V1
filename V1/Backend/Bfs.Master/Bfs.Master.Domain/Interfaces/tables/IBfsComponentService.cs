using Bfs.Core.Contracts;
using Bfs.Master.Contracts;

namespace Bfs.Master.Domain.Interfaces
{
    public interface IBfsComponentService
    {
        Task<BfsComponent?> GetAsync(long id);
        Task<List<BfsComponent>> GetAsync();

        Task<BfsComponent> CreateAsync(BfsComponent contract);
        Task<BfsComponent?> UpdateAsync(BfsComponent contract);
        Task DeleteAsync(long id);
        Task<BfsComponent> UploadAsync(BfsComponent contract);

        Task<QueryResponse<BfsComponentListItem>> ListAsync(QueryRequest<BfsComponentListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
