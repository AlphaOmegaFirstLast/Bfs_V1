using Bfs.Core.Helpers;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Data.Interfaces;
using Bfs.StockEx.Data.Models;

using Microsoft.EntityFrameworkCore;
using static Dapper.SqlMapper;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public StockExDbContext _context { get; }
    //Template_Field_ChildrenMatrix_AddDeclareEntry

    //Template_Start_Code_DontOverwrite_2
    private readonly ISspTransactionRepository sspTransactionRepo;
    private readonly ICashTransactionRepository cashTransactionRepo;
    private readonly ISsPortfolioBalanceRepository ssPortfolioBalanceRepo;
    private readonly ISspStockRepository sspStockRepo;

    // Properties to expose the repositories
    public ISspTransactionRepository SspTransactionRepo => sspTransactionRepo;
    public ICashTransactionRepository CashTransactionRepo => cashTransactionRepo;
    //Template_End_Code_DontOverwrite_2

    public UnitOfWork(StockExDbContext dbContext, IScopeData scopeData
//Template_Field_ChildrenMatrix_AddParameterEntry

//Template_Start_Code_DontOverwrite_3
, ISspTransactionRepository sspTransactionRepo
, ICashTransactionRepository cashTransactionRepo
, ISsPortfolioBalanceRepository ssPortfolioBalanceRepo
, ISspStockRepository sspStockRepo
    //Template_End_Code_DontOverwrite_3

    )
    {
        _context = dbContext;
        //Template_Field_ChildrenMatrix_AddInitEntry

        //Template_Start_Code_DontOverwrite_4
        this.sspTransactionRepo = sspTransactionRepo;
        this.cashTransactionRepo = cashTransactionRepo;
        this.ssPortfolioBalanceRepo = ssPortfolioBalanceRepo;
        this.sspStockRepo = sspStockRepo;
        //Template_End_Code_DontOverwrite_4

    }

    //Template_Field_ChildrenMatrix_AddUnitOfWorkEntry

    //Template_Start_Code_DontOverwrite_5
    public async Task<SspTransactionEntity> RolloutSspTransactionAsync(SspTransactionEntity ssptEntity)
    {
        var transactionTypeList = await _context.TransactionTypes.ToListAsync();
        var sspTransactionType = transactionTypeList.FirstOrDefault(x => x.Id == ssptEntity.TransactionTypeId);
        if (sspTransactionType != null)
        {
            // all in memory changes: assign value to ssp transaction.Id modify stock.quantity
            var inMemorySspTransactionEntity = await SspTransactionRepo.CreateAsync(ssptEntity);
            await RolloutStockQuantity(sspTransactionType, ssptEntity);

            if (sspTransactionType.NextTransactionTypeId != (int)TransactionTypes.None)
            {
                var nextTransactionType = transactionTypeList.FirstOrDefault(x => x.Id == sspTransactionType.NextTransactionTypeId);

                if (nextTransactionType != null && nextTransactionType.StockEntityTypeId == (int)enumStockEntityType.Cash)
                {
                    var cashEntity = PopulateCashEntityFromSspTransaction(ssptEntity, (int)nextTransactionType.Id);
                    var inMemoryCashEntity = await cashTransactionRepo.CreateAsync(cashEntity);
                    await RolloutPortfolioBalance(nextTransactionType, inMemoryCashEntity);
                }

                if (nextTransactionType != null && nextTransactionType.StockEntityTypeId == (int)enumStockEntityType.StockShare)
                {
                    var toSspEntity = PopulateSspEntityFromSspTransaction(ssptEntity, (int)nextTransactionType.Id);
                    var inMemoryToSspTransactionEntity = await SspTransactionRepo.CreateAsync(toSspEntity);
                    await RolloutStockQuantity(nextTransactionType, toSspEntity);
                }
            }

            await _context.SaveChangesAsync();
            return inMemorySspTransactionEntity;
        }
        else
        {
            throw new Exception("Transaction Type not found.");
        }
    }

    public async Task RollbackSspTransactionAsync(long id)
    {
        var ssptEntity = await _context.SspTransactions.FirstOrDefaultAsync(x => x.Id == id);
        if (ssptEntity != null)
        {
            var transactionTypeList = await _context.TransactionTypes.ToListAsync();
            var sspTransactionType = transactionTypeList.FirstOrDefault(x => x.Id == ssptEntity.TransactionTypeId);
            if (sspTransactionType != null)
            {
                var isTransactionFoundAfterMyRecord = await _context.SspTransactions.AnyAsync(x => x.StockShareId == ssptEntity.StockShareId && x.SsPortfolioId == ssptEntity.SsPortfolioId && x.TransactionDate > ssptEntity.TransactionDate);

                if (!isTransactionFoundAfterMyRecord)
                {
                    if (sspTransactionType.NextTransactionTypeId != (int)TransactionTypes.None)
                    {
                        var cashEntity = await _context.CashTransactions.FirstOrDefaultAsync(x => x.SspTransactionId == ssptEntity.Id);
                        var cashTransactionType = transactionTypeList.FirstOrDefault(x => x.Id == sspTransactionType.NextTransactionTypeId);
                        if (cashTransactionType != null && cashEntity != null)
                        {
                            await RollbackPortfolioBalance(cashTransactionType, cashEntity);
                            _context.CashTransactions.Remove(cashEntity);
                        }
                    }

                    await RollbackStockQuantity(sspTransactionType, ssptEntity);
                    _context.SspTransactions.Remove(ssptEntity);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new ApplicationException("Rollback Transaction is not allowed since later transactions have been recorded.");
                }
            }
            else
            {
                throw new Exception("Transaction Type not found.");
            }
        }
    }

    public async Task<SspTransactionEntity> ChangeSspTransactionAsync(SspTransactionEntity sspEntity)
    {
        var transactionTypeList = await _context.TransactionTypes.ToListAsync();

        var oldSspEntity = await _context.SspTransactions.FirstOrDefaultAsync(x => x.Id == sspEntity.Id);
        if (oldSspEntity == null)
        {
            throw new Exception("Ssp Transaction not found.");
        }

        var oldTransactionType = transactionTypeList.FirstOrDefault(x => x.Id == oldSspEntity.TransactionTypeId);
        if (oldTransactionType != null)
        {
            await RollbackStockQuantity(oldTransactionType, oldSspEntity);
        }

        var currentTransactionType = transactionTypeList.FirstOrDefault(x => x.Id == sspEntity.TransactionTypeId);
        if (currentTransactionType != null)
        {
            await RolloutStockQuantity(currentTransactionType, sspEntity);
        }

        // Automatically copies all values from the incoming entity to the tracked database entity
        _context.Entry(oldSspEntity).CurrentValues.SetValues(sspEntity);

        // Use asynchronous SaveChanges to match your async method signature
        await _context.SaveChangesAsync();

        return oldSspEntity;
    }

    public async Task<CashTransactionEntity> RolloutCashTransactionAsync(CashTransactionEntity cashEntity)
    {
        var transactionTypeList = await _context.TransactionTypes.ToListAsync();
        var currentTransactionType = transactionTypeList.FirstOrDefault(x => x.Id == cashEntity.TransactionTypeId);
        if (currentTransactionType != null)
        {
            var inMemoryCashEntity = await cashTransactionRepo.CreateAsync(cashEntity);
            await RolloutPortfolioBalance(currentTransactionType, inMemoryCashEntity);

            await _context.SaveChangesAsync();
            return inMemoryCashEntity;
        }
        else
        {
            throw new Exception("Transaction Type not found.");
        }
    }

    public async Task RollbackCashTransactionAsync(long id)
    {
        var cashEntity = await _context.CashTransactions.FirstOrDefaultAsync(x => x.Id == id);
        if (cashEntity != null)
        {
            var transactionTypeList = await _context.TransactionTypes.ToListAsync();
            var currentTransactionType = transactionTypeList.FirstOrDefault(x => x.Id == cashEntity.TransactionTypeId);
            if (currentTransactionType != null)
            {
                await RollbackPortfolioBalance(currentTransactionType, cashEntity);
                _context.CashTransactions.Remove(cashEntity);
            }
            await _context.SaveChangesAsync();
        }
    }

    public async Task<CashTransactionEntity> ChangeCashTransactionAsync(CashTransactionEntity cashEntity)
    {
        var transactionTypeList = await _context.TransactionTypes.ToListAsync();

        var oldCashEntity = await _context.CashTransactions.FirstOrDefaultAsync(x => x.Id == cashEntity.Id);
        if (oldCashEntity == null)
        {
            throw new Exception("Cash Transaction not found.");
        }

        var oldTransactionType = transactionTypeList.FirstOrDefault(x => x.Id == oldCashEntity.TransactionTypeId);
        if (oldTransactionType != null)
        {
            await RollbackPortfolioBalance(oldTransactionType, oldCashEntity);
        }

        var currentTransactionType = transactionTypeList.FirstOrDefault(x => x.Id == cashEntity.TransactionTypeId);
        if (currentTransactionType != null)
        {
            await RolloutPortfolioBalance(currentTransactionType, cashEntity);
        }

        // Automatically copies all values from the incoming entity to the tracked database entity
        _context.Entry(oldCashEntity).CurrentValues.SetValues(cashEntity);

        // Use asynchronous SaveChanges to match your async method signature
        await _context.SaveChangesAsync();

        return oldCashEntity;
    }

    private async Task RolloutStockQuantity(TransactionTypeEntity currentTransactionType, SspTransactionEntity ssptEntity)
    {
        var stock = await GetSspStockById(ssptEntity.SsPortfolioId, ssptEntity.StockShareId, ssptEntity.CurrencyId);

        decimal resultQuantity = 0;
        // ------------------------

        if (currentTransactionType.StockEntityTypeId == (int)enumStockEntityType.StockShare)
        {
            resultQuantity = DoCalculations((enumCalculationMethods)currentTransactionType.CalculationMethodId, stock.Quantity, ssptEntity.Quantity, ssptEntity.ToQuantity);
            switch (currentTransactionType.EffectTypeId)
            {
                case (int)enumEffectTypes.Add:
                    stock.AverageCost = ((stock.Quantity * stock.AverageCost) + (resultQuantity * ssptEntity.Price)) / (stock.Quantity + resultQuantity);
                    stock.Quantity = stock.Quantity + resultQuantity;
                    break;
                case (int)enumEffectTypes.Sub:
                    stock.Quantity = stock.Quantity - resultQuantity;
                    break;
                case (int)enumEffectTypes.Set:
                    stock.AverageCost = ssptEntity.Price;
                    stock.Quantity = resultQuantity;
                    break;
            }
        }
    }

    private async Task RollbackStockQuantity(TransactionTypeEntity currentTransactionType, SspTransactionEntity ssptEntity)
    {
        var stock = await GetSspStockById(ssptEntity.SsPortfolioId, ssptEntity.StockShareId, ssptEntity.CurrencyId);

        decimal resultQuantity = 0;
        // ------------------------

        if (currentTransactionType.StockEntityTypeId == (int)enumStockEntityType.StockShare)
        {
            resultQuantity = DoRollBackCalculations((enumCalculationMethods)currentTransactionType.CalculationMethodId, stock.Quantity, ssptEntity.Quantity, ssptEntity.ToQuantity);
            switch (currentTransactionType.EffectTypeId)
            {
                case (int)enumEffectTypes.Add:
                    stock.Quantity = stock.Quantity - resultQuantity;
                    break;
                case (int)enumEffectTypes.Sub:
                    stock.Quantity = stock.Quantity + resultQuantity;
                    break;
                case (int)enumEffectTypes.Set:
                    stock.Quantity = resultQuantity;
                    break;
            }
        }
    }

    private async Task RolloutPortfolioBalance(TransactionTypeEntity currentTransactionType, CashTransactionEntity cashEntity)
    {
        var portfolioBalance = await GetCashBalanceById(cashEntity.SsPortfolioId, cashEntity.CurrencyId);

        decimal resultAmount = 0;
        // ------------------------

        if (currentTransactionType.StockEntityTypeId == (int)enumStockEntityType.Cash)
        {
            resultAmount = DoCalculations((enumCalculationMethods)currentTransactionType.CalculationMethodId, portfolioBalance.Balance, cashEntity.Value, 0);
            switch (currentTransactionType.EffectTypeId)
            {
                case (int)enumEffectTypes.Add:
                    portfolioBalance.Balance = portfolioBalance.Balance + resultAmount;
                    break;
                case (int)enumEffectTypes.Sub:
                    portfolioBalance.Balance = portfolioBalance.Balance - resultAmount;
                    break;
                case (int)enumEffectTypes.Set:
                    portfolioBalance.Balance = resultAmount;
                    break;
            }
        }
    }

    private async Task RollbackPortfolioBalance(TransactionTypeEntity currentTransactionType, CashTransactionEntity cashEntity)
    {
        var portfolioBalance = await GetCashBalanceById(cashEntity.SsPortfolioId, cashEntity.CurrencyId);

        decimal resultAmount = 0;
        // ------------------------

        if (currentTransactionType.StockEntityTypeId == (int)enumStockEntityType.Cash)
        {
            resultAmount = DoCalculations((enumCalculationMethods)currentTransactionType.CalculationMethodId, portfolioBalance.Balance, cashEntity.Value, 0);
            switch (currentTransactionType.EffectTypeId)
            {
                case (int)enumEffectTypes.Add:
                    portfolioBalance.Balance = portfolioBalance.Balance - resultAmount;
                    break;
                case (int)enumEffectTypes.Sub:
                    portfolioBalance.Balance = portfolioBalance.Balance + resultAmount;
                    break;
                case (int)enumEffectTypes.Set:
                    new NotImplementedException("Rollback for is not allowed for Balance Set Transactions.");
                    break;
            }
        }
    }

    private CashTransactionEntity PopulateCashEntityFromSspTransaction(SspTransactionEntity ssptEntity, int cashTransactionTypeId)
    {
        var cashEntity = new CashTransactionEntity()
        {
            Notes = ssptEntity.Notes,
            SourceDate = ssptEntity.SourceDate,
            TransactionDate = ssptEntity.TransactionDate,
            Source = ssptEntity.Source,
            Value = (ssptEntity.Quantity * ssptEntity.Price),  // calculated value here?
            SsPortfolioId = ssptEntity.SsPortfolioId,
            SspTransactionId = ssptEntity.Id,
            TransactionTypeId = (int)cashTransactionTypeId, //new field need to be added to TransactionTable.
            CurrencyId = ssptEntity.CurrencyId
        };

        return cashEntity;
    }

    private SspTransactionEntity PopulateSspEntityFromSspTransaction(SspTransactionEntity ssptEntity, int nextTransactionTypeId)
    {
        var ToSspEntity = new SspTransactionEntity()
        {
            Notes = ssptEntity.Notes,
            SourceDate = ssptEntity.SourceDate,
            TransactionDate = ssptEntity.TransactionDate,
            Source = ssptEntity.Source,
            Quantity = ssptEntity.Quantity,
            Price = ssptEntity.Price,
            SsPortfolioId = ssptEntity.ToPortfolioId,
            ToSspTransactionId = ssptEntity.Id,
            StockShareId = ssptEntity.StockShareId,
            TransactionTypeId = (int)nextTransactionTypeId, //new field need to be added to TransactionTable.
            CurrencyId = ssptEntity.CurrencyId
        };

        return ToSspEntity;
    }

    private async Task<SspStockEntity> GetSspStockById(long SsPortfolioId, long stockShareId, long currencyId)
    {
        var stock = await _context.SspStocks.FirstOrDefaultAsync(x => x.SsPortfolioId == SsPortfolioId && x.StockShareId == stockShareId && x.CurrencyId == currencyId);
        if (stock == null)
        {
            stock = await sspStockRepo.CreateAsync(new SspStockEntity()
            {
                SsPortfolioId = SsPortfolioId,
                StockShareId = stockShareId,
                CurrencyId = currencyId,
                Quantity = 0,
                AverageCost = 0,
            });
        }

        return stock;
    }

    private async Task<SsPortfolioBalanceEntity> GetCashBalanceById(long ssPortfolioId, long currencyId)
    {
        var balance = await _context.SsPortfolioBalances.FirstOrDefaultAsync(x => x.SsPortfolioId == ssPortfolioId && x.CurrencyId == currencyId);
        if (balance == null)
        {
            balance = await ssPortfolioBalanceRepo.CreateAsync(new SsPortfolioBalanceEntity()
            {
                SsPortfolioId = ssPortfolioId,
                CurrencyId = currencyId,
                Balance = 0
            });
        }

        return balance;
    }

    private decimal DoCalculations(enumCalculationMethods CalculationMethod, decimal balance, decimal original, decimal toResult)
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
                result = (balance * toResult) / original;
                break;
        }
        return result;
    }

    private decimal DoRollBackCalculations(enumCalculationMethods CalculationMethod, decimal balance, decimal original, decimal toResult)
    {
        decimal result = 0;
        switch (CalculationMethod)
        {
            case enumCalculationMethods.None:
                result = original;
                break;
            case enumCalculationMethods.Merge:
                result = (balance * original) / toResult;
                break;
            case enumCalculationMethods.Split:
                result = (balance * original) / toResult;
                break;
            case enumCalculationMethods.Divident:
                result = (balance * original) / toResult;
                break;
        }
        return result;
    }

    //Template_End_Code_DontOverwrite_5

}

//Template_Start_Code_DontOverwrite_6
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
//Template_End_Code_DontOverwrite_6
