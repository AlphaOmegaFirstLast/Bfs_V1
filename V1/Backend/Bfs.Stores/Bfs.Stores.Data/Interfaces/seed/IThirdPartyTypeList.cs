using Bfs.Core.Data;
using Bfs.Stores.Data;

namespace Bfs.Stores.Data.Interfaces
{
    public interface IThirdPartyTypeList
    {
        Task<QueryResponse<ThirdPartyTypeListItem>> GetAsync(QueryRequest<ThirdPartyTypeListFilter> request);
    }
}

