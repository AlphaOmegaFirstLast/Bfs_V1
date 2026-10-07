using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Auth.Contracts;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Auth.Domain.Interfaces
{
    public interface IResourceRuleService: ICrudService<ResourceRule>
    {
        Task<ResourceRule> UploadAsync(ResourceRule contract);

        Task<QueryResponse<ResourceRuleListItem>> ListAsync(QueryRequest<ResourceRuleListFilter> contractRequest);

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2
    }
}
