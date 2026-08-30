using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface ISourceTypeService: ICrudService<SourceType>
    {
        Task<SourceType> UploadAsync(SourceType contract);

        Task<QueryResponse<SourceTypeListItem>> ListAsync(QueryRequest<SourceTypeListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

