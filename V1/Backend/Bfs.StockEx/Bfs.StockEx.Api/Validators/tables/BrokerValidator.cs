using FluentValidation;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Domain;

namespace Bfs.StockEx.Api.Validators
{
    public class BrokerValidator : AbstractValidator<Broker>
    {
        public BrokerValidator()
        {
        RuleFor(x => x.Id)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidId)
;
RuleFor(x => x.Code)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidCode)
.MinimumLength(1)
;
RuleFor(x => x.Name)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidName)
;

        }
    }
}
