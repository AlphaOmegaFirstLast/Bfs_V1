using Bfs.Core.Contracts;
using Bfs.BestFit.Contracts;

namespace Bfs.BestFit.Domain.Interfaces
{
    public interface ITableFieldService
    {
        Task<TableField?> GetAsync(long id);
        Task<List<TableField>> GetAsync();

        Task<TableField> CreateAsync(TableField contract);
        Task<TableField?> UpdateAsync(TableField contract);
        Task DeleteAsync(long id);
        Task<TableField> UploadAsync(TableField contract);

        Task<QueryResponse<TableFieldListItem>> ListAsync(QueryRequest<TableFieldListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
