using Bfs.StockEx.Data.Models;
//Template_Start_Code_DontOverwrite_1
using Bfs.StockEx.Data.Interfaces;
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Data.Repositories;

public interface IUnitOfWork
{
    StockExDbContext _context { get; }
    //Template_Field_ChildrenMatrix_AddIUnitOfWorkEntry

    //Template_Start_Code_DontOverwrite_2
    ISspTransactionRepository SspTransactionRepo { get; }
    ICashTransactionRepository CashTransactionRepo { get; }

    Task<SspTransactionEntity> RolloutSspTransactionAsync(SspTransactionEntity entity);
    Task<CashTransactionEntity> RolloutCashTransactionAsync(CashTransactionEntity entity);

    Task RollbackSspTransactionAsync(long id);
    Task RollbackCashTransactionAsync(long id);

    Task<SspTransactionEntity> ChangeSspTransactionAsync(SspTransactionEntity entity);
    Task<CashTransactionEntity> ChangeCashTransactionAsync(CashTransactionEntity entity);
    //Template_End_Code_DontOverwrite_2

}
