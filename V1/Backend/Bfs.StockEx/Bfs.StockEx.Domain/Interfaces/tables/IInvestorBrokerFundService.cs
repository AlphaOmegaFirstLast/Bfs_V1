using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface IInvestorBrokerFundService: ICrudService<InvestorBrokerFund>
    {
        Task<InvestorBrokerFund> UploadAsync(InvestorBrokerFund contract);

        Task<QueryResponse<InvestorBrokerFundListItem>> ListAsync(QueryRequest<InvestorBrokerFundListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
