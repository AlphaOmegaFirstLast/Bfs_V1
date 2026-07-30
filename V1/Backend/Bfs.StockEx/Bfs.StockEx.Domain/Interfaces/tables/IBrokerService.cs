using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface IBrokerService: ICrudService<Broker>
    {
        Task<Broker> UploadAsync(Broker contract);

        Task<QueryResponse<BrokerListItem>> ListAsync(QueryRequest<BrokerListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
