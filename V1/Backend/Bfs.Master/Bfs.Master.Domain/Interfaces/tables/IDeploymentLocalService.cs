using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Master.Contracts;

namespace Bfs.Master.Domain.Interfaces
{
    public interface IDeploymentLocalService: ICrudService<DeploymentLocal>
    {
        Task<DeploymentLocal> UploadAsync(DeploymentLocal contract);

        Task<QueryResponse<DeploymentLocalListItem>> ListAsync(QueryRequest<DeploymentLocalListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

