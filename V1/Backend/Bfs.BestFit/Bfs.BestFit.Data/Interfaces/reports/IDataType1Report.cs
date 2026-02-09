using Bfs.Core.Data;
using Bfs.BestFit.Data;

namespace Bfs.BestFit.Data.Interfaces
{
    public interface IDataType1Report
    {
        Task<QueryResponse<DataType1Item>> GetAsync(QueryRequest<DataType1Filter> request);
    }
}