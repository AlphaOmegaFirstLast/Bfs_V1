using FluentValidation;
using Bfs.Master.Contracts;
using Bfs.Master.Domain;

namespace Bfs.Master.Api.Validators
{
    public class BfsSystemValidator : AbstractValidator<BfsSystem>
    {
        public BfsSystemValidator()
        {
        RuleFor(x => x.Notes)
.MaximumLength(500)
;
RuleFor(x => x.DbPrefix)
.MinimumLength(2)
.MaximumLength(50)
;
RuleFor(x => x.Logo)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidLogo)
.MinimumLength(3)
.MaximumLength(300)
;
RuleFor(x => x.Name)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidName)
.MinimumLength(3)
.MaximumLength(50)
;

        }
    }
}
