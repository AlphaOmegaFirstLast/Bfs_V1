using Bfs.Core.Interfaces;
using Bfs.StockEx.Data.Interfaces;
using Bfs.StockEx.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace Bfs.StockEx.Data.Repositories;

public enum enumEffectTypes
{
    None = 1,
    Add = 2,
    Sub = 3,
    Set = 4,
}

public enum enumCalculationMethods
{
    None = 1,
    Merge = 2,
    Split = 3,
    Divident = 4,
}

public enum enumStockEntityType
{
    None = 1,
    Cash = 2,
    StockShare = 3,
}

public enum TransactionTypes
{
    None = 1,
    StartingBalance = 2,
    Deposite = 3,
    Withdrawel = 4,
    DebitInterest = 5,
    CreditInterest = 6,
    Coupon = 7,
    Expenses = 8,
    TransferToAnotherPortfolio = 9,
    TransferFromAnotherPortfolio = 10,
    SellStockShare = 11,
    SellStockShareCash = 12,
    BuyStockShare = 13,
    BuyStockShareCash = 14,
    Merge = 15,
    Split = 16,
    Divident = 17,
}


public class UnitOfWork : IUnitOfWork
{
    private readonly StockExDbContext _context;
    private readonly ISspTransactionRepository sspTransactionRepo;
    private readonly ICashTransactionRepository cashTransactionRepo;
    private readonly ISsPortfolioBalanceRepository ssPortfolioBalanceRepo;
    private readonly ISspStockRepository sspStockRepo;

    //Template_Field_ChildrenMatrix_AddDeclareEntry

    public UnitOfWork(StockExDbContext dbContext, IScopeData scopeData
, ISspTransactionRepository sspTransactionRepo
, ICashTransactionRepository cashTransactionRepo
, ISsPortfolioBalanceRepository ssPortfolioBalanceRepo
, ISspStockRepository sspStockRepo
    //Template_Field_ChildrenMatrix_AddParameterEntry
    )
    {
        _context = dbContext;
        this.sspTransactionRepo = sspTransactionRepo;
        this.cashTransactionRepo = cashTransactionRepo;
        this.ssPortfolioBalanceRepo = ssPortfolioBalanceRepo;
        this.sspStockRepo = sspStockRepo;
        //Template_Field_ChildrenMatrix_AddInitEntry
    }

    public ISspTransactionRepository SspTransactionRepo => sspTransactionRepo;
    public ICashTransactionRepository CashTransactionRepo => cashTransactionRepo;

    public async Task<SspTransactionEntity> RolloutTransactionAsync(SspTransactionEntity ssptEntity)
    {
        var transactionTypeList = await _context.TransactionTypes.ToListAsync();
        var sspTransactionType = transactionTypeList.FirstOrDefault(x => x.Id == ssptEntity.TransactionTypeId);
        if (sspTransactionType!=null)
        {
            // all in memory changes: assign value to ssp transaction.Id modify stock.balance
            var inMemorySspTransactionRecord = await RolloutSspTransactionAsync(sspTransactionType, ssptEntity);

            if (sspTransactionType.NextTransactionTypeId != (int)TransactionTypes.None)
            {
                var cashTransactionType = transactionTypeList.FirstOrDefault(x => x.Id == sspTransactionType.NextTransactionTypeId);

                var cashEntity = new CashTransactionEntity()
                {
                    Name = ssptEntity.Name,
                    Notes = ssptEntity.Notes,
                    SourceDate = ssptEntity.SourceDate,
                    TransactionDate = ssptEntity.TransactionDate,
                    Source = ssptEntity.Source,
                    Value = (ssptEntity.Quantity * ssptEntity.Price),  // calculated value here?
                    SsPortfolioId = ssptEntity.SsPortfolioId,
                    SspTransactionId = ssptEntity.Id,
                    TransactionTypeId = (int)cashTransactionType.Id  //new field need to be added to TransactionTable.
                };

                // all in memory changes: assign value to cash transaction.Id modify cash.balance
                var inMemoryCashTransactionRecord = await RolloutCashTransactionAsync(cashTransactionType, cashEntity);
            }
        }
        _context.SaveChanges();
        return ssptEntity;
    }

    public async Task<CashTransactionEntity> RolloutTransactionAsync(CashTransactionEntity cashEntity)
    {
        var transactionTypeList = await _context.TransactionTypes.ToListAsync();
        var currentTransactionType = transactionTypeList.FirstOrDefault(x => x.Id == cashEntity.TransactionTypeId);
        if (currentTransactionType != null)
        {
            var y = await RolloutCashTransactionAsync(currentTransactionType, cashEntity);
        }
        _context.SaveChanges();
        return cashEntity;
    }

    public async Task<SspTransactionEntity> RolloutSspTransactionAsync(TransactionTypeEntity currentTransactionType, SspTransactionEntity ssptEntity)
    {
        var newTransaction = await SspTransactionRepo.CreateAsync(ssptEntity);
        var stock = await GetSspStockById(newTransaction.SsPortfolioId, newTransaction.StockShareId);

        decimal resultQuantity = 0;
        // ------------------------

        if (currentTransactionType.StockEntityTypeId == (int)enumStockEntityType.StockShare)
        {
            resultQuantity = DoCalculations((enumCalculationMethods)currentTransactionType.CalculationMethodId, stock.Quantity, newTransaction.Quantity, newTransaction.ToQuantity);
            switch (currentTransactionType.EffectTypeId)
            {
                case (int)enumEffectTypes.Add:
                    stock.Quantity = stock.Quantity + resultQuantity;
                    break;
                case (int)enumEffectTypes.Sub:
                    stock.Quantity = stock.Quantity - resultQuantity;
                    break;
                case (int)enumEffectTypes.Set:
                    stock.Quantity = resultQuantity;
                    break;
            }
        }

        return newTransaction;
    }

    public async Task<CashTransactionEntity> RolloutCashTransactionAsync(TransactionTypeEntity currentTransactionType, CashTransactionEntity cashEntity)
    {
        var newTransaction = await cashTransactionRepo.CreateAsync(cashEntity);
        var cash = await GetCashBalanceById(newTransaction.SsPortfolioId);

        decimal resultAmount = 0;
        // ------------------------

        if (currentTransactionType.StockEntityTypeId == (int)enumStockEntityType.Cash)
        {
            resultAmount = DoCalculations((enumCalculationMethods)currentTransactionType.CalculationMethodId, cash.Balance, newTransaction.Value, 0);
            switch (currentTransactionType.EffectTypeId)
            {
                case (int)enumEffectTypes.Add:
                    cash.Balance = cash.Balance + resultAmount; 
                    break;
                case (int)enumEffectTypes.Sub:
                    cash.Balance = cash.Balance - resultAmount; 
                    break;
                case (int)enumEffectTypes.Set:
                    cash.Balance = resultAmount;
                    break;
            }
        }

        return newTransaction;
    }

    public async Task<SspStockEntity> GetSspStockById(long SsPortfolioId, long stockShareId)
    {
        var stock = await _context.SspStocks.FirstOrDefaultAsync(x => x.SsPortfolioId == SsPortfolioId && x.StockShareId == stockShareId);
        if (stock == null)
        {
            stock = await sspStockRepo.CreateAsync(new SspStockEntity()
            {
                SsPortfolioId = SsPortfolioId,
                StockShareId = stockShareId,
                Quantity = 0,
                AverageCost = 0,
            });
        }

        return stock;
    }

    public async Task<SsPortfolioBalanceEntity> GetCashBalanceById(long ssPortfolioId)
    {
        var balance = await _context.SsPortfolioBalances.FirstOrDefaultAsync(x => x.SsPortfolioId == ssPortfolioId);
        if (balance == null)
        {
            balance = await ssPortfolioBalanceRepo.CreateAsync(new SsPortfolioBalanceEntity()
            {
                SsPortfolioId = ssPortfolioId,
                Balance = 0
            });
        }

        return balance;
    }

    public decimal DoCalculations(enumCalculationMethods CalculationMethod, decimal balance, decimal original, decimal toResult)
    {
        decimal result = 0;
        switch (CalculationMethod)
        {
            case enumCalculationMethods.None:
                result = original;
                break;
            case enumCalculationMethods.Merge:
                result = (balance * toResult) / original;
                break;
            case enumCalculationMethods.Split:
                result = (balance * toResult) / original;
                break;
            case enumCalculationMethods.Divident:
                result = (balance  / original ) * toResult;
                break;
        }
        return result;
    }


    //Template_Field_ChildrenMatrix_AddUnitOfWorkEntry


    //public async Task MergeStockShares(long stockShareId, int factor)
    //{
    //    var stockShareList = await _context.SspStocks.Where(x => x.StockShareId == stockShareId).ToListAsync();
    //    stockShareList.ForEach(stockShare =>
    //    {
    //        stockShare.Quantity = stockShare.Quantity / factor;
    //        stockShare.AverageCost = stockShare.Quantity * factor;
    //    });

    //    await _context.SaveChangesAsync();
    //}
}
