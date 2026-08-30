using Bfs.Core.Data;
using Bfs.Stores.Data;

namespace Bfs.Stores.Data.Interfaces
{
    public interface IEffectTypeList
    {
        Task<QueryResponse<EffectTypeListItem>> GetAsync(QueryRequest<EffectTypeListFilter> request);
    }
}

