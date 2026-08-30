using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface IEffectTypeList
    {
        Task<QueryResponse<EffectTypeListItem>> GetAsync(QueryRequest<EffectTypeListFilter> request);
    }
}

