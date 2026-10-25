using Bfs.Core.Contracts;

namespace Bfs.StockEx.Contracts
{
    public class PortfolioProfitRepCompareFilter
    {

        public long? SspStock_SsPortfolioId { get; set; }
public long? SspStock_StockShareId { get; set; }
public long? SspStock_CurrencyId { get; set; }

        public NumericRange? CurrentPrice_Profit { get; set; }

    }
}