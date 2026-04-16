using FluentValidation;
using Bfs.Stores.Contracts;
using Bfs.Stores.Domain;

namespace Bfs.Stores.Api.Validators
{
    public class TransactionValidator : AbstractValidator<Transaction>
    {
        public TransactionValidator()
        {

RuleFor(x => x.Notes)
.MaximumLength(1000)
;

        }
    }
}
