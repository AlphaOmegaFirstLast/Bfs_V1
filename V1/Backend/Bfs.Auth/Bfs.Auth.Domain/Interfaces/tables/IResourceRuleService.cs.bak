using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Auth.Contracts;

namespace Bfs.Auth.Domain.Interfaces
{
    public interface IResourceRuleService: ICrudService<ResourceRule>
    {
        Task<ResourceRule> UploadAsync(ResourceRule contract);

        Task<QueryResponse<ResourceRuleListItem>> ListAsync(QueryRequest<ResourceRuleListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

