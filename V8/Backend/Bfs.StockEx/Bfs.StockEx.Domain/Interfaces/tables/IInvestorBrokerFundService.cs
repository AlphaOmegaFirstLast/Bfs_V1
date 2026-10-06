using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface IInvestorBrokerFundService: ICrudService<InvestorBrokerFund>
    {
        Task<InvestorBrokerFund> UploadAsync(InvestorBrokerFund contract);

        Task<QueryResponse<InvestorBrokerFundListItem>> ListAsync(QueryRequest<InvestorBrokerFundListFilter> contractRequest);

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2
    }
}
