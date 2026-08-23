using FluentValidation;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Domain;

namespace Bfs.StockEx.Api.Validators
{
    public class BrokerAgreementValidator : AbstractValidator<BrokerAgreement>
    {
        public BrokerAgreementValidator()
        {
        RuleFor(x => x.Name)
.MinimumLength(0)
.MaximumLength(0)
;
RuleFor(x => x.Notes)
.MaximumLength(0)
;

        }
    }
}
