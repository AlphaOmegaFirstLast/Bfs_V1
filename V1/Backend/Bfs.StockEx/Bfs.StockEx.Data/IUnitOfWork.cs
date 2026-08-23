using Bfs.StockEx.Data.Interfaces;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Data.Repositories;

public interface IUnitOfWork
{
    ISspTransactionRepository SspTransactionRepo { get; }
    ICashTransactionRepository CashTransactionRepo { get; }

    //Template_Field_ChildrenMatrix_AddIUnitOfWorkEntry
    Task<SspTransactionEntity> RolloutTransactionAsync(SspTransactionEntity entity);
    Task<CashTransactionEntity> RolloutTransactionAsync(CashTransactionEntity entity);
}
