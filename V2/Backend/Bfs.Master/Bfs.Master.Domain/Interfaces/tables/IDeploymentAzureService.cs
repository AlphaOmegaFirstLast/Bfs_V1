using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Master.Contracts;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Master.Domain.Interfaces
{
    public interface IDeploymentAzureService: ICrudService<DeploymentAzure>
    {
        Task<DeploymentAzure> UploadAsync(DeploymentAzure contract);

        Task<QueryResponse<DeploymentAzureListItem>> ListAsync(QueryRequest<DeploymentAzureListFilter> contractRequest);

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    }
}
