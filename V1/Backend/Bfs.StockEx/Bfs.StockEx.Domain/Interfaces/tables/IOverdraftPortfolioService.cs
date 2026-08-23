using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface IOverdraftPortfolioService: ICrudService<OverdraftPortfolio>
    {
        Task<OverdraftPortfolio> UploadAsync(OverdraftPortfolio contract);

        Task<QueryResponse<OverdraftPortfolioListItem>> ListAsync(QueryRequest<OverdraftPortfolioListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
