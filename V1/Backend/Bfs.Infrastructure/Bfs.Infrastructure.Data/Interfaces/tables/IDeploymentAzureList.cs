using Bfs.Core.Data;
using Bfs.Infrastructure.Data;

namespace Bfs.Infrastructure.Data.Interfaces
{
    public interface IDeploymentAzureList
    {
        Task<QueryResponse<DeploymentAzureListItem>> GetAsync(QueryRequest<DeploymentAzureListFilter> request);
    }
}