using Bfs.Core.Data;
using Bfs.Master.Data;

namespace Bfs.Master.Data.Interfaces
{
    public interface IDataTypeList
    {
        Task<QueryResponse<DataTypeListItem>> GetAsync(QueryRequest<DataTypeListFilter> request);
    }
}