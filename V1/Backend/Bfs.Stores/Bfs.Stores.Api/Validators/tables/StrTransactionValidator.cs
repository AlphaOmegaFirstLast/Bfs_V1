using FluentValidation;
using Bfs.Stores.Contracts;
using Bfs.Stores.Domain;

namespace Bfs.Stores.Api.Validators
{
    public class StrTransactionValidator : AbstractValidator<StrTransaction>
    {
        public StrTransactionValidator()
        {
RuleFor(x => x.Notes)
.MaximumLength(1000)
;

        }
    }
}
