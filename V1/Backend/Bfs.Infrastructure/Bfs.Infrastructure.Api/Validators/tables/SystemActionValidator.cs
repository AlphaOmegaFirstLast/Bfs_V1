using FluentValidation;
using Bfs.Infrastructure.Contracts;
using Bfs.Infrastructure.Domain;

namespace Bfs.Infrastructure.Api.Validators
{
    public class SystemActionValidator : AbstractValidator<SystemAction>
    {
        public SystemActionValidator()
        {
        RuleFor(x => x.Name)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidName)
.MinimumLength(3)
.MaximumLength(50)
;
RuleFor(x => x.Notes)
.MaximumLength(50)
;
RuleFor(x => x.MatchProprty)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidMatchProprty)
.MinimumLength(3)
.MaximumLength(50)
;
RuleFor(x => x.MatchValues)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidMatchValues)
.MinimumLength(3)
.MaximumLength(50)
;

        }
    }
}
