using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface IStockShareService: ICrudService<StockShare>
    {
        Task<StockShare> UploadAsync(StockShare contract);

        Task<QueryResponse<StockShareListItem>> ListAsync(QueryRequest<StockShareListFilter> contractRequest);
        Task<Results<Ok, BadRequest<ProblemDetails>>> UploadExcel(IFormFile excelFile);
        Task<long> GetStockShareByName(string stockShareName, long tradingRoomId, long currencyId);

        //Template_Start_Code_DontOverwrite_2
        //Template_End_Code_DontOverwrite_2
    }
}
