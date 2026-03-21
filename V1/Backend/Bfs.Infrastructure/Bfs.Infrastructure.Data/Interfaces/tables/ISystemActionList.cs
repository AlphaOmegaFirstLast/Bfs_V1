using Bfs.Core.Data;
using Bfs.Infrastructure.Data;

namespace Bfs.Infrastructure.Data.Interfaces
{
    public interface ISystemActionList
    {
        Task<QueryResponse<SystemActionListItem>> GetAsync(QueryRequest<SystemActionListFilter> request);
    }
}
//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1

