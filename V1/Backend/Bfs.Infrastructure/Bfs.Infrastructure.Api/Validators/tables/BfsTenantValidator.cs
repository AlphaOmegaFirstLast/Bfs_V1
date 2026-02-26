using FluentValidation;
using Bfs.Infrastructure.Contracts;
using Bfs.Infrastructure.Domain;

namespace Bfs.Infrastructure.Api.Validators
{
    public class BfsTenantValidator : AbstractValidator<BfsTenant>
    {
        public BfsTenantValidator()
        {
        RuleFor(x => x.DbConnection)
.MaximumLength(300)
;
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
