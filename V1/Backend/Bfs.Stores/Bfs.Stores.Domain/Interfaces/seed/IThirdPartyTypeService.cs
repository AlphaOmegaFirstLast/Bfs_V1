using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Stores.Contracts;

namespace Bfs.Stores.Domain.Interfaces
{
    public interface IThirdPartyTypeService: ICrudService<ThirdPartyType>
    {
        Task<ThirdPartyType> UploadAsync(ThirdPartyType contract);

        Task<QueryResponse<ThirdPartyTypeListItem>> ListAsync(QueryRequest<ThirdPartyTypeListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

