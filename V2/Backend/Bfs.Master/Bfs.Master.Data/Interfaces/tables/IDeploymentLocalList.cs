using Bfs.Core.Data;
using Bfs.Master.Data;

namespace Bfs.Master.Data.Interfaces
{
    public interface IDeploymentLocalList
    {
        Task<QueryResponse<DeploymentLocalListItem>> GetAsync(QueryRequest<DeploymentLocalListFilter> request);
    }
}