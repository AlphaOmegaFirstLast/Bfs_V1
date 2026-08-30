using Bfs.Core.Data;
using Bfs.Master.Data;

namespace Bfs.Master.Data.Interfaces
{
    public interface IChartElementList
    {
        Task<QueryResponse<ChartElementListItem>> GetAsync(QueryRequest<ChartElementListFilter> request);
    }
}