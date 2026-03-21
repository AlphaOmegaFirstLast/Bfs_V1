using Bfs.Core.Data;
using Bfs.Infrastructure.Data;

namespace Bfs.Infrastructure.Data.Interfaces
{
    public interface IBusinessActionList
    {
        Task<QueryResponse<BusinessActionListItem>> GetAsync(QueryRequest<BusinessActionListFilter> request);
    }
}
//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1

