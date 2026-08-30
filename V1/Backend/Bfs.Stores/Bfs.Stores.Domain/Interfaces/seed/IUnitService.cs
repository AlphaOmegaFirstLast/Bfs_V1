using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Stores.Contracts;

namespace Bfs.Stores.Domain.Interfaces
{
    public interface IUnitService: ICrudService<Unit>
    {
        Task<Unit> UploadAsync(Unit contract);

        Task<QueryResponse<UnitListItem>> ListAsync(QueryRequest<UnitListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

