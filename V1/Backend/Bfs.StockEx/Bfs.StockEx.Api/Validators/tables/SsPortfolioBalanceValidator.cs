using FluentValidation;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Domain;

namespace Bfs.StockEx.Api.Validators
{
    public class SsPortfolioBalanceValidator : AbstractValidator<SsPortfolioBalance>
    {
        public SsPortfolioBalanceValidator()
        {
        RuleFor(x => x.Name)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidName)
.MinimumLength(3)
.MaximumLength(50)
;
RuleFor(x => x.Notes)
.MaximumLength(1000)
;

        }
    }
}
