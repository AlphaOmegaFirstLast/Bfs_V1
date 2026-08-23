using FluentValidation;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Domain;

namespace Bfs.StockEx.Api.Validators
{
    public class CustomReportsValidator : AbstractValidator<CustomReports>
    {
        public CustomReportsValidator()
        {
        RuleFor(x => x.Name)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidName)
.MinimumLength(3)
.MaximumLength(50)
;
RuleFor(x => x.BaseReport)
.MaximumLength(1000)
;
RuleFor(x => x.CreatedBy)
.MaximumLength(1000)
;
RuleFor(x => x.Url)
.MaximumLength(1000)
;

        }
    }
}
