using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface IExpensesTypeService: ICrudService<ExpensesType>
    {
        Task<ExpensesType> UploadAsync(ExpensesType contract);

        Task<QueryResponse<ExpensesTypeListItem>> ListAsync(QueryRequest<ExpensesTypeListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
