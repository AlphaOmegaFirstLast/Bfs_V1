using FluentValidation;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Domain;

namespace Bfs.StockEx.Api.Validators
{
    public class SsPortfolioValidator : AbstractValidator<SsPortfolio>
    {
        public SsPortfolioValidator()
        {

        }
    }
}

