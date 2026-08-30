using FluentValidation;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Domain;

namespace Bfs.StockEx.Api.Validators
{
    public class SsPortfolioBalanceValidator : AbstractValidator<SsPortfolioBalance>
    {
        public SsPortfolioBalanceValidator()
        {

        }
    }
}

