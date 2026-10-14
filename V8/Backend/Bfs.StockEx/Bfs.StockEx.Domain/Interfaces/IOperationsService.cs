using Bfs.StockEx.Contracts;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Domain.Interfaces;

public interface IOperationsService
{
    //Template_Field_ChildrenMatrix_AddIServiceEntry
    //Template_Start_Code_DontOverwrite_2
    Task<SspTransaction?> RolloutSspTransactionAsync(SspTransaction contract);
    Task<CashTransaction?> RolloutCashTransactionAsync(CashTransaction contract);
    Task RollbackSspTransactionAsync(long id);
    Task RollbackCashTransactionAsync(long id);
    Task<SspTransaction?> ChangeSspTransactionAsync(SspTransaction contract);
    Task<CashTransaction?> ChangeCashTransactionAsync(CashTransaction contract);
    //Template_End_Code_DontOverwrite_2

}
