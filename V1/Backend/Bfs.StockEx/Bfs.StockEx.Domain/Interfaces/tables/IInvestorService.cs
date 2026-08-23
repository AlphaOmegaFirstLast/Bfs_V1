using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface IInvestorService: ICrudService<Investor>
    {
        Task<Investor> UploadAsync(Investor contract);

        Task<QueryResponse<InvestorListItem>> ListAsync(QueryRequest<InvestorListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
