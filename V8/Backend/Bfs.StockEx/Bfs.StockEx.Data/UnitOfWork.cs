using Bfs.Core.Helpers;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Data.Interfaces;
using Bfs.StockEx.Data.Models;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Schema;
using System.Collections;
using System.Linq.Expressions;
using System.Net.WebSockets;
using static Dapper.SqlMapper;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public StockExDbContext _context { get; }
    //Template_Field_ChildrenMatrix_AddDeclareEntry

    //Template_Start_Code_DontOverwrite_2
    private readonly ISspTransactionRepository _sspTransactionRepo;
    private readonly ICashTransactionRepository _cashTransactionRepo;
    private readonly ISsPortfolioBalanceRepository _ssPortfolioBalanceRepo;
    private readonly ISspStockRepository _sspStockRepo;
    private readonly ICouponRepository _couponRepo;
    private readonly ISsPortfolioRepository _portfolioRepo;
    private readonly ITransactionTypeRepository _transactionTypeRepo;
    private readonly ICurrentPriceRepository _currentPriceRepo;
    private readonly IStockShareRepository _stockShareRepo;
    private readonly ICouponProcessRepository _couponProcessRepo;

    // Properties to expose the repositories
    public ISspTransactionRepository SspTransactionRepo => _sspTransactionRepo;
    public ICashTransactionRepository CashTransactionRepo => _cashTransactionRepo;
    //Template_End_Code_DontOverwrite_2

    public UnitOfWork(StockExDbContext dbContext, IScopeData scopeData

    //Template_Start_Code_DontOverwrite_3
    , ISspTransactionRepository sspTransactionRepo
    , ICashTransactionRepository cashTransactionRepo
    , ISsPortfolioBalanceRepository ssPortfolioBalanceRepo
    , ISspStockRepository sspStockRepo
    , ICouponRepository couponRepo
    , ISsPortfolioRepository portfolioRepo
    , ITransactionTypeRepository transactionTypeRepo
    , ICurrentPriceRepository currentPriceRepo
    , ICouponProcessRepository couponProcessRepo
, IStockShareRepository stockShareRepo

    //Template_End_Code_DontOverwrite_3
    //Template_Field_ChildrenMatrix_AddParameterEntry
    )
    {
        _context = dbContext;
        //Template_Field_ChildrenMatrix_AddInitEntry

        //Template_Start_Code_DontOverwrite_4
        _sspTransactionRepo = sspTransactionRepo;
        _cashTransactionRepo = cashTransactionRepo;
        _ssPortfolioBalanceRepo = ssPortfolioBalanceRepo;
        _sspStockRepo = sspStockRepo;
        _couponRepo = couponRepo;
        _portfolioRepo = portfolioRepo;
        _transactionTypeRepo = transactionTypeRepo;
        _currentPriceRepo = currentPriceRepo;
        _stockShareRepo = stockShareRepo;
        _couponProcessRepo = couponProcessRepo;
        //Template_End_Code_DontOverwrite_4

    }

    //Template_Field_ChildrenMatrix_AddUnitOfWorkEntry

    //Template_Start_Code_DontOverwrite_5
    public async Task ProcessCoupon(DateTime processDate)
    {
        var portfolioList = await _portfolioRepo.GetAsync();
        var couponList = await _couponRepo.GetAsync(x => x.DueDate <= processDate && x.CouponStatusId == (int)enumCouponStatus.NotYet);

        foreach (var coupon in couponList)
        {
            foreach (var portfolio in portfolioList)
            {
                var quantityStock = await StockAtDate(coupon.ValueDate, coupon.StockShareId, portfolio.Id);
                var stockShareRecord = await _stockShareRepo.FirstOrDefaultAsync(x => x.Id == coupon.StockShareId) ;
                if (quantityStock > 0)
                {
                    if (coupon.CouponTypeId == (int)enumCouponType.Cash)
                    {
                        await ProcessCouponCash(coupon, quantityStock, portfolio.Id, processDate,stockShareRecord.CurrencyId);
                    }
                    else if (coupon.CouponTypeId == (int)enumCouponType.StockShare)
                    {
                        await ProcessCouponStockShare(coupon, quantityStock, portfolio.Id, processDate, stockShareRecord.CurrencyId);
                    }

                }
                coupon.CouponStatusId = (int)enumCouponStatus.Done;
                await _couponRepo.UpdateAsync(coupon);
                var couponProcessEntity = await _couponProcessRepo.CreateAsync(new CouponProcessEntity() {CouponId= coupon.Id, ProcessDate= processDate, ProcessStatusId=(int)enumProcessStatus.Sucess });
            }
        }
        throw new Exception("Success");
      //  await _context.SaveChangesAsync();
    }

    public async Task ProcessCouponCash(CouponEntity coupon, decimal quantityStock, long portfolioId, DateTime processDate,long currencyId)
    {
        decimal valueToAdd = 0;
        if (coupon.Value > 0)
        {
            valueToAdd = quantityStock * coupon.Value;
        }
        else if (coupon.CouponPercent > 0)
        {
            var currentPriceRecord = await _currentPriceRepo.FirstOrDefaultAsync(x => x.StockShareId == coupon.StockShareId && x.TransactionDate <= processDate);
            if (currentPriceRecord != null)
            {
                valueToAdd = quantityStock * currentPriceRecord.Price * (coupon.CouponPercent/100);
            }
        }
        var cashEntity = new CashTransactionEntity()
        {
            SsPortfolioId = portfolioId,
            Value = valueToAdd,
            TransactionDate = processDate,
            SourceDate = processDate,
            Source = "from coupon Process",
            Notes = "Coupon",
            ExpensesTypeId = 1,
            TransactionTypeId = (int)TransactionTypes.CouponCash,
            SspTransactionId = 0,
            CurrencyId = currencyId //to do get currency of stockshare 
        };

        await RolloutCashTransactionAsync(cashEntity, false);
    }

    public async Task ProcessCouponStockShare(CouponEntity coupon, decimal quantityStock, long portfolioId, DateTime processDate, long currencyId)
    {
        decimal quantityToAdd = 0;
        var currentPriceRecord = await _currentPriceRepo.FirstOrDefaultAsync(x => x.StockShareId == coupon.StockShareId && x.TransactionDate <= processDate);
        if (coupon.Quantity > 0)
        {
            quantityToAdd = ((quantityStock / coupon.Quantity ) * coupon.BonusQuantity ) ;
        }
        var sspEntity = new SspTransactionEntity()
        {
            SsPortfolioId = portfolioId,
            Quantity = quantityToAdd,
            TransactionDate = processDate,
            SourceDate = processDate,
            Source = "from coupon Process",
            Notes = "Coupon",
            TransactionTypeId = (int)TransactionTypes.CouponStockShare,
            Price = currentPriceRecord != null? currentPriceRecord.Price:0 ,
            ToQuantity = 0,
            ToPortfolioId = 0,
            ToSspTransactionId = 0,
            CurrencyId = currencyId //to do get currency of stockshare 
        };

        await RolloutSspTransactionAsync(sspEntity, false);
    }

    public async Task<decimal> StockAtDate(DateTime valueDate, long stockShareId, long portfolioId)
    {
        decimal quantityStock = 0;
        var startDate = DateTime.MinValue;
        var transactionTypeList = await _transactionTypeRepo.GetAsync();
        var startBalanceRecord = await _sspTransactionRepo.FirstOrDefaultAsync(x => x.StockShareId == stockShareId && x.SsPortfolioId == portfolioId && x.TransactionDate <= valueDate
                                                                      && (x.TransactionTypeId == (int)TransactionTypes.StartingBalanceCash || x.TransactionTypeId == (int)TransactionTypes.StartingBalanceStockShare)
                                                                      , q => q.OrderByDescending(x => x.TransactionDate));

        if (startBalanceRecord != null)
        {
            startDate = startBalanceRecord.TransactionDate;
            quantityStock = startBalanceRecord.Quantity;
        }

        var portfolioTransactionList = await _sspTransactionRepo.GetAsync(x => x.StockShareId == stockShareId && x.SsPortfolioId == portfolioId
                                                                            && x.TransactionDate <= valueDate && x.TransactionDate > startDate
                                                                            , q => q.OrderBy(x => x.TransactionDate));

        var stock = new SspStockEntity() { StockShareId = stockShareId, SsPortfolioId = portfolioId, Quantity = quantityStock };  //in-memory stockEntity as base for replaying Transactions
        foreach (var portfolioTransactionRecord in portfolioTransactionList)
        {
            var sspTransactionType = transactionTypeList.FirstOrDefault(x => x.Id == portfolioTransactionRecord.TransactionTypeId);
            stock = await SetStockQuantity(stock, sspTransactionType, portfolioTransactionRecord);
        }

        return stock.Quantity;
    }

    public async Task<SspTransactionEntity> RolloutSspTransactionAsync(SspTransactionEntity ssptEntity, bool isCommit = true)
    {
        var transactionTypeList = await _transactionTypeRepo.GetAsync();
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
                    var inMemoryCashEntity = await _cashTransactionRepo.CreateAsync(cashEntity);
                    await RolloutPortfolioBalance(nextTransactionType, inMemoryCashEntity);
                }

                if (nextTransactionType != null && nextTransactionType.StockEntityTypeId == (int)enumStockEntityType.StockShare)
                {
                    var toSspEntity = PopulateSspEntityFromSspTransaction(ssptEntity, (int)nextTransactionType.Id);
                    var inMemoryToSspTransactionEntity = await SspTransactionRepo.CreateAsync(toSspEntity);
                    await RolloutStockQuantity(nextTransactionType, toSspEntity);
                }
            }

            if (isCommit)
            {
                await _context.SaveChangesAsync();
            }

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
            var transactionTypeList = await _transactionTypeRepo.GetAsync();
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
        var transactionTypeList = await _transactionTypeRepo.GetAsync();

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

    public async Task<CashTransactionEntity> RolloutCashTransactionAsync(CashTransactionEntity cashEntity, bool isCommit = true)
    {
        var transactionTypeList = await _transactionTypeRepo.GetAsync();
        var currentTransactionType = transactionTypeList.FirstOrDefault(x => x.Id == cashEntity.TransactionTypeId);
        if (currentTransactionType != null)
        {
            var inMemoryCashEntity = await _cashTransactionRepo.CreateAsync(cashEntity);
            await RolloutPortfolioBalance(currentTransactionType, inMemoryCashEntity);
            if (isCommit)
            {
                await _context.SaveChangesAsync();
            }

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
            var transactionTypeList = await _transactionTypeRepo.GetAsync();
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
        var transactionTypeList = await _transactionTypeRepo.GetAsync();

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

        // ------------------------

        if (currentTransactionType.StockEntityTypeId == (int)enumStockEntityType.StockShare)
        {
            //resultQuantity = DoCalculations((enumCalculationMethods)currentTransactionType.CalculationMethodId, stock.Quantity, ssptEntity.Quantity, ssptEntity.ToQuantity);
            //switch (currentTransactionType.EffectTypeId)
            //{
            //    case (int)enumEffectTypes.Add:
            //        stock.AverageCost = ((stock.Quantity * stock.AverageCost) + (resultQuantity * ssptEntity.Price)) / (stock.Quantity + resultQuantity);
            //        stock.Quantity = stock.Quantity + resultQuantity;
            //        break;
            //    case (int)enumEffectTypes.Sub:
            //        stock.Quantity = stock.Quantity - resultQuantity;
            //        break;
            //    case (int)enumEffectTypes.Set:
            //        stock.AverageCost = ssptEntity.Price;
            //        stock.Quantity = resultQuantity;
            //        break;
            //}

            stock = await SetStockQuantity(stock, currentTransactionType, ssptEntity);
        }
    }

    private async Task<SspStockEntity> SetStockQuantity(SspStockEntity stockEntity, TransactionTypeEntity currentTransactionType, SspTransactionEntity ssptEntity)
    {
        decimal resultQuantity = 0;

        if (currentTransactionType.StockEntityTypeId == (int)enumStockEntityType.StockShare)
        {
            resultQuantity = DoCalculations((enumCalculationMethods)currentTransactionType.CalculationMethodId, stockEntity.Quantity, ssptEntity.Quantity, ssptEntity.ToQuantity);
            switch (currentTransactionType.EffectTypeId)
            {
                case (int)enumEffectTypes.Add:
                    stockEntity.AverageCost = ((stockEntity.Quantity * stockEntity.AverageCost) + (resultQuantity * ssptEntity.Price)) / (stockEntity.Quantity + resultQuantity);
                    stockEntity.Quantity = stockEntity.Quantity + resultQuantity;
                    break;
                case (int)enumEffectTypes.Sub:
                    stockEntity.Quantity = stockEntity.Quantity - resultQuantity;
                    break;
                case (int)enumEffectTypes.Set:
                    stockEntity.AverageCost = ssptEntity.Price;
                    stockEntity.Quantity = resultQuantity;
                    break;
            }
        }
        return stockEntity;
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
            stock = await _sspStockRepo.CreateAsync(new SspStockEntity()
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
            balance = await _ssPortfolioBalanceRepo.CreateAsync(new SsPortfolioBalanceEntity()
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

public enum enumCouponStatus
{
    Done = 1,
    NotYet = 2,
}

public enum enumProcessStatus
{
    Sucess = 1,
    Failure = 2,
    PartialSuccess = 3,
}

public enum enumCouponType
{
    Cash = 1,
    StockShare = 2,
}
public enum TransactionTypes
{
    None = 1,
    StartingBalanceCash = 2,
    Deposite = 3,
    Withdrawel = 4,
    DebitInterest = 5,
    CreditInterest = 6,
    CouponCash = 7,
    Expenses = 8,
    TransferToAnotherPortfolio = 9,
    TransferFromAnotherPortfolio = 10,
    SellStockShare = 11,
    SellStockShareCash = 12,
    BuyStockShare = 13,
    BuyStockShareCash = 14,
    Merge = 15,
    Split = 16,
    CouponStockShare = 17,
    StartingBalanceStockShare =18,

}
//Template_End_Code_DontOverwrite_6
