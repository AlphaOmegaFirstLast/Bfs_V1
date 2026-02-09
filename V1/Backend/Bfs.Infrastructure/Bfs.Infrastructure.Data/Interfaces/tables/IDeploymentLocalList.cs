using Bfs.Core.Data;
using Bfs.Infrastructure.Data;

namespace Bfs.Infrastructure.Data.Interfaces
{
    public interface IDeploymentLocalList
    {
        Task<QueryResponse<DeploymentLocalListItem>> GetAsync(QueryRequest<DeploymentLocalListFilter> request);
    }
}