using Bfs.Core.Data;
using Bfs.BestFit.Data;

namespace Bfs.BestFit.Data.Interfaces
{
    public interface IDeploymentAzureStagingList
    {
        Task<QueryResponse<DeploymentAzureStagingListItem>> GetDeploymentAzureStagingListAsync(QueryRequest<DeploymentAzureStagingListFilter> request);
    }
}