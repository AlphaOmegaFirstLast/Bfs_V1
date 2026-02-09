using Bfs.Core.Contracts;
using Bfs.Infrastructure.Contracts;

namespace Bfs.Infrastructure.Domain.Interfaces
{
    public interface ICustomFieldDefinitionService
    {
        Task<CustomFieldDefinition?> GetAsync(long id);
        Task<List<CustomFieldDefinition>> GetAsync();

        Task<CustomFieldDefinition> CreateAsync(CustomFieldDefinition contract);
        Task<CustomFieldDefinition?> UpdateAsync(CustomFieldDefinition contract);
        Task DeleteAsync(long id);
        Task<CustomFieldDefinition> UploadAsync(CustomFieldDefinition contract);

        Task<QueryResponse<CustomFieldDefinitionListItem>> ListAsync(QueryRequest<CustomFieldDefinitionListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
