using FluentValidation;
using Bfs.Infrastructure.Contracts;
using Bfs.Infrastructure.Domain;

namespace Bfs.Infrastructure.Api.Validators
{
    public class BfsSystemValidator : AbstractValidator<BfsSystem>
    {
        public BfsSystemValidator()
        {
        RuleFor(x => x.Name)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidName)
.MinimumLength(3)
.MaximumLength(50)
;
RuleFor(x => x.Notes)
.MaximumLength(500)
;
RuleFor(x => x.DbPrefix)
.MinimumLength(2)
.MaximumLength(50)
;

        }
    }
}
