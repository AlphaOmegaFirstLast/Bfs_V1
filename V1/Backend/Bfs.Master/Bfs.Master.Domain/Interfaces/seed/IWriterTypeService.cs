using Bfs.Core.Contracts;
using Bfs.Master.Contracts;

namespace Bfs.Master.Domain.Interfaces
{
    public interface IWriterTypeService
    {
        Task<WriterType?> GetAsync(long id);
        Task<List<WriterType>> GetAsync();

        Task<WriterType> CreateAsync(WriterType contract);
        Task<WriterType?> UpdateAsync(WriterType contract);
        Task DeleteAsync(long id);
        Task<WriterType> UploadAsync(WriterType contract);

        Task<QueryResponse<WriterTypeListItem>> ListAsync(QueryRequest<WriterTypeListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
