using Bfs.Core.Contracts;
using Bfs.BestFit.Contracts;

namespace Bfs.BestFit.Domain.Interfaces
{
    public interface ICustomReportsService
    {
        Task<CustomReports?> GetAsync(long id);
        Task<List<CustomReports>> GetAsync();

        Task<CustomReports> CreateAsync(CustomReports contract);
        Task<CustomReports?> UpdateAsync(CustomReports contract);
        Task DeleteAsync(long id);
        Task<CustomReports> UploadAsync(CustomReports contract);

        Task<QueryResponse<CustomReportsListItem>> ListAsync(QueryRequest<CustomReportsListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
