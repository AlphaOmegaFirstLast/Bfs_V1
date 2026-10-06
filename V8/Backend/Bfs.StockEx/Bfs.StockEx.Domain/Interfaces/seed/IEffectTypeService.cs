using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface IEffectTypeService: ICrudService<EffectType>
    {
        Task<EffectType> UploadAsync(EffectType contract);

        Task<QueryResponse<EffectTypeListItem>> ListAsync(QueryRequest<EffectTypeListFilter> contractRequest);

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2
    }
}
