using Bfs.Core.Data;
using Bfs.BestFit.Data;

namespace Bfs.BestFit.Data.Interfaces
{
    public interface IDeploymentLocalList
    {
        Task<QueryResponse<DeploymentLocalListItem>> GetDeploymentLocalListAsync(QueryRequest<DeploymentLocalListFilter> request);
    }
}