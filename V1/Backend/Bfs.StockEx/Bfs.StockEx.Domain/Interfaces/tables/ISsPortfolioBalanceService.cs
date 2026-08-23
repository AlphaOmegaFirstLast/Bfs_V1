using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface ISsPortfolioBalanceService: ICrudService<SsPortfolioBalance>
    {
        Task<SsPortfolioBalance> UploadAsync(SsPortfolioBalance contract);

        Task<QueryResponse<SsPortfolioBalanceListItem>> ListAsync(QueryRequest<SsPortfolioBalanceListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
