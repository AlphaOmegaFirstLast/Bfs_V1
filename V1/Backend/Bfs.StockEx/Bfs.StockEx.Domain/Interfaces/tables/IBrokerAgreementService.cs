using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface IBrokerAgreementService: ICrudService<BrokerAgreement>
    {
        Task<BrokerAgreement> UploadAsync(BrokerAgreement contract);

        Task<QueryResponse<BrokerAgreementListItem>> ListAsync(QueryRequest<BrokerAgreementListFilter> contractRequest);

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    }
}
