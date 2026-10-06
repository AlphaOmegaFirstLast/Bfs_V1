using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface IBrokerService: ICrudService<Broker>
    {
        Task<Broker> UploadAsync(Broker contract);

        Task<QueryResponse<BrokerListItem>> ListAsync(QueryRequest<BrokerListFilter> contractRequest);

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2
    }
}
