using Bfs.Core.Data;
using Bfs.Auth.Data;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Auth.Data.Interfaces
{
    public interface IRoleRepCompare
    {
        Task<QueryResponse<RoleRepCompareItem>> GetAsync(QueryRequest<RoleRepCompareFilter> request);
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2
    }
}