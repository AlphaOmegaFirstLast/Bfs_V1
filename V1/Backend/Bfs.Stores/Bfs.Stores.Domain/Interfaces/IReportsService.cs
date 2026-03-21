using Bfs.Core.Contracts;
using Bfs.Stores.Contracts;

namespace Bfs.Stores.Domain.Interfaces
{
    public interface IReportsService
    {

        Task<QueryResponse<ProductTransactionCompareItem>> ProductTransactionCompareAsync(QueryRequest<ProductTransactionCompareFilter> contractRequest);

//Template_Component_AddIServiceEntry
  }
}
