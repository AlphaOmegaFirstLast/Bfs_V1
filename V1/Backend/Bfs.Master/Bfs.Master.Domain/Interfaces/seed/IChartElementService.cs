using Bfs.Core.Contracts;
using Bfs.Master.Contracts;

namespace Bfs.Master.Domain.Interfaces
{
    public interface IChartElementService
    {
        Task<ChartElement?> GetAsync(long id);
        Task<List<ChartElement>> GetAsync();

        Task<ChartElement> CreateAsync(ChartElement contract);
        Task<ChartElement?> UpdateAsync(ChartElement contract);
        Task DeleteAsync(long id);
        Task<ChartElement> UploadAsync(ChartElement contract);

        Task<QueryResponse<ChartElementListItem>> ListAsync(QueryRequest<ChartElementListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
