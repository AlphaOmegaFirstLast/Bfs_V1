using Bfs.StockEx.Contracts;

namespace Bfs.StockEx.Domain.Interfaces;

public interface IOperationsService
{
    //Template_Field_ChildrenMatrix_AddIServiceEntry
    Task<SspTransaction?> RolloutTransactionAsync(SspTransaction contract);
    Task<CashTransaction?> RolloutTransactionAsync(CashTransaction contract);
}
