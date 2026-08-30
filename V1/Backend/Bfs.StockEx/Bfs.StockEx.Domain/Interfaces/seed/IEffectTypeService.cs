using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface IEffectTypeService: ICrudService<EffectType>
    {
        Task<EffectType> UploadAsync(EffectType contract);

        Task<QueryResponse<EffectTypeListItem>> ListAsync(QueryRequest<EffectTypeListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

