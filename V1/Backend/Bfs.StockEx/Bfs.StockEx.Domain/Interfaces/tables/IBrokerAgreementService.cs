using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface IBrokerAgreementService: ICrudService<BrokerAgreement>
    {
        Task<BrokerAgreement> UploadAsync(BrokerAgreement contract);

        Task<QueryResponse<BrokerAgreementListItem>> ListAsync(QueryRequest<BrokerAgreementListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
