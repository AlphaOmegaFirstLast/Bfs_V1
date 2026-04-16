using FluentValidation;
using Bfs.Stores.Contracts;
using Bfs.Stores.Domain;

namespace Bfs.Stores.Api.Validators
{
    public class StoreValidator : AbstractValidator<Store>
    {
        public StoreValidator()
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

