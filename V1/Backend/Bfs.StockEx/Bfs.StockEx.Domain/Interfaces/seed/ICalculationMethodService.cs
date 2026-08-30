using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface ICalculationMethodService: ICrudService<CalculationMethod>
    {
        Task<CalculationMethod> UploadAsync(CalculationMethod contract);

        Task<QueryResponse<CalculationMethodListItem>> ListAsync(QueryRequest<CalculationMethodListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

