using Bfs.Core.Contracts;
using Bfs.Infrastructure.Contracts;

namespace Bfs.Infrastructure.Domain.Interfaces
{
    public interface IBusinessActionService
    {
        Task<BusinessAction?> GetAsync(long id);
        Task<List<BusinessAction>> GetAsync();

        Task<BusinessAction> CreateAsync(BusinessAction contract);
        Task<BusinessAction?> UpdateAsync(BusinessAction contract);
        Task DeleteAsync(long id);
        Task<BusinessAction> UploadAsync(BusinessAction contract);

        Task<QueryResponse<BusinessActionListItem>> ListAsync(QueryRequest<BusinessActionListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1

