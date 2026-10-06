
using Bfs.StockEx.Data.Models;
//Template_Start_Code_DontOverwrite_1
using Bfs.StockEx.Data.Interfaces;
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Data.Repositories;

public interface IUnitOfWork
{
    //Template_Field_ChildrenMatrix_AddIUnitOfWorkEntry

    //Template_Start_Code_DontOverwrite_2
    ISspTransactionRepository SspTransactionRepo { get; }
    ICashTransactionRepository CashTransactionRepo { get; }
    Task<SspTransactionEntity> RolloutTransactionAsync(SspTransactionEntity entity);
    Task<CashTransactionEntity> RolloutTransactionAsync(CashTransactionEntity entity);
    //Template_End_Code_DontOverwrite_2


    

}
