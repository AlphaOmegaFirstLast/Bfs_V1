using Bfs.Core.Data;
using Bfs.Master.Data;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Master.Data.Interfaces
{
    public interface IStructureCompare
    {
        Task<QueryResponse<StructureCompareItem>> GetAsync(QueryRequest<StructureCompareFilter> request);
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    }
}