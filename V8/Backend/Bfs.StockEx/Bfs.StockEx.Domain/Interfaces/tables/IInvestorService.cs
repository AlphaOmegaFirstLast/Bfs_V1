using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface IInvestorService: ICrudService<Investor>
    {
        Task<Investor> UploadAsync(Investor contract);

        Task<QueryResponse<InvestorListItem>> ListAsync(QueryRequest<InvestorListFilter> contractRequest);

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2
    }
}
