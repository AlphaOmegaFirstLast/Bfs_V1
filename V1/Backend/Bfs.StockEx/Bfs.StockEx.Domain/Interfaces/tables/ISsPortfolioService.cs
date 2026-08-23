using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface ISsPortfolioService: ICrudService<SsPortfolio>
    {
        Task<SsPortfolio> UploadAsync(SsPortfolio contract);

        Task<QueryResponse<SsPortfolioListItem>> ListAsync(QueryRequest<SsPortfolioListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

