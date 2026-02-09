using Bfs.Core.Contracts;
using Bfs.BestFit.Contracts;

namespace Bfs.BestFit.Domain.Interfaces
{
    public interface IDeploymentAzureStagingService
    {
        Task<DeploymentAzureStaging?> GetAsync(long id);
        Task<List<DeploymentAzureStaging>> GetAsync();

        Task<DeploymentAzureStaging> CreateAsync(DeploymentAzureStaging contract);
        Task<DeploymentAzureStaging?> UpdateAsync(DeploymentAzureStaging contract);
        Task DeleteAsync(long id);
        Task<DeploymentAzureStaging> UploadAsync(DeploymentAzureStaging contract);

        Task<QueryResponse<DeploymentAzureStagingListItem>> ListAsync(QueryRequest<DeploymentAzureStagingListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
