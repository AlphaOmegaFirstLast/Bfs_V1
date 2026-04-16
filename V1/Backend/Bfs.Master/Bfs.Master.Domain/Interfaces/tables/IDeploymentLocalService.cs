using Bfs.Core.Contracts;
using Bfs.Master.Contracts;

namespace Bfs.Master.Domain.Interfaces
{
    public interface IDeploymentLocalService
    {
        Task<DeploymentLocal?> GetAsync(long id);
        Task<List<DeploymentLocal>> GetAsync();

        Task<DeploymentLocal> CreateAsync(DeploymentLocal contract);
        Task<DeploymentLocal?> UpdateAsync(DeploymentLocal contract);
        Task DeleteAsync(long id);
        Task<DeploymentLocal> UploadAsync(DeploymentLocal contract);

        Task<QueryResponse<DeploymentLocalListItem>> ListAsync(QueryRequest<DeploymentLocalListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
