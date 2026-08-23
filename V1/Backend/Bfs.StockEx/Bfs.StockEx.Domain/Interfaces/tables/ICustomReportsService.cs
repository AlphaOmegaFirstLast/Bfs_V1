using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface ICustomReportsService: ICrudService<CustomReports>
    {
        Task<CustomReports> UploadAsync(CustomReports contract);

        Task<QueryResponse<CustomReportsListItem>> ListAsync(QueryRequest<CustomReportsListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
