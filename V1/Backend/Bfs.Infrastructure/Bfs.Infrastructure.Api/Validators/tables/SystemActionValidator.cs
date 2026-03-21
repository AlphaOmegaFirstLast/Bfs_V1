using FluentValidation;
using Bfs.Infrastructure.Contracts;
using Bfs.Infrastructure.Domain;

namespace Bfs.Infrastructure.Api.Validators
{
    public class SystemActionValidator : AbstractValidator<SystemAction>
    {
        public SystemActionValidator()
        {
        RuleFor(x => x.ShortName)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidShortName)
.MinimumLength(1)
.MaximumLength(3)
;
RuleFor(x => x.Notes)
.MaximumLength(1000)
;
RuleFor(x => x.MatchProperty)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidMatchProperty)
.MinimumLength(3)
.MaximumLength(1000)
;
RuleFor(x => x.MatchValues)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidMatchValues)
.MinimumLength(3)
.MaximumLength(1000)
;
RuleFor(x => x.Name)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidName)
.MinimumLength(3)
.MaximumLength(50)
;

        }
    }
}

//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1

