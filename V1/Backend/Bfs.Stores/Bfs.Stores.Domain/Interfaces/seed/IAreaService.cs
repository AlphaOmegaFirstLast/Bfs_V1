using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Stores.Contracts;

namespace Bfs.Stores.Domain.Interfaces
{
    public interface IAreaService: ICrudService<Area>
    {
        Task<Area> UploadAsync(Area contract);

        Task<QueryResponse<AreaListItem>> ListAsync(QueryRequest<AreaListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
