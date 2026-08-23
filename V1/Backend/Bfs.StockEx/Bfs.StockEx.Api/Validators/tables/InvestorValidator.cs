using FluentValidation;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Domain;

namespace Bfs.StockEx.Api.Validators
{
    public class InvestorValidator : AbstractValidator<Investor>
    {
        public InvestorValidator()
        {
        RuleFor(x => x.Name)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidName)
.MinimumLength(3)
.MaximumLength(50)
;
RuleFor(x => x.Notes)
.MaximumLength(0)
;
RuleFor(x => x.Code)
.MinimumLength(1)
.MaximumLength(15)
;
RuleFor(x => x.Email)
.MinimumLength(1)
.MaximumLength(100)
;

        }
    }
}

