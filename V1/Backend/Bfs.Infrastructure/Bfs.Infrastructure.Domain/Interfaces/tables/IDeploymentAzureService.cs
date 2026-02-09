using Bfs.Core.Contracts;
using Bfs.Infrastructure.Contracts;

namespace Bfs.Infrastructure.Domain.Interfaces
{
    public interface IDeploymentAzureService
    {
        Task<DeploymentAzure?> GetAsync(long id);
        Task<List<DeploymentAzure>> GetAsync();

        Task<DeploymentAzure> CreateAsync(DeploymentAzure contract);
        Task<DeploymentAzure?> UpdateAsync(DeploymentAzure contract);
        Task DeleteAsync(long id);
        Task<DeploymentAzure> UploadAsync(DeploymentAzure contract);

        Task<QueryResponse<DeploymentAzureListItem>> ListAsync(QueryRequest<DeploymentAzureListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
