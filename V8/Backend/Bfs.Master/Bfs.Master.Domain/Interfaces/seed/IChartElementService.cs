using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Master.Contracts;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Master.Domain.Interfaces
{
    public interface IChartElementService: ICrudService<ChartElement>
    {
        Task<ChartElement> UploadAsync(ChartElement contract);

        Task<QueryResponse<ChartElementListItem>> ListAsync(QueryRequest<ChartElementListFilter> contractRequest);

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    }
}
