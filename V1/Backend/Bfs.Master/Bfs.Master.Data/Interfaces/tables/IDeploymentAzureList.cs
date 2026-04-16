using Bfs.Core.Data;
using Bfs.Master.Data;

namespace Bfs.Master.Data.Interfaces
{
    public interface IDeploymentAzureList
    {
        Task<QueryResponse<DeploymentAzureListItem>> GetAsync(QueryRequest<DeploymentAzureListFilter> request);
    }
}