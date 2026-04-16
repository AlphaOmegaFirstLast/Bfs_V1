using FluentValidation;
using Bfs.Master.Contracts;
using Bfs.Master.Domain;

namespace Bfs.Master.Api.Validators
{
    public class BfsComponentValidator : AbstractValidator<BfsComponent>
    {
        public BfsComponentValidator()
        {
        RuleFor(x => x.Name)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidName)
.MinimumLength(3)
.MaximumLength(50)
;
RuleFor(x => x.Notes)
.MaximumLength(500)
;
RuleFor(x => x.InterfaceRequired)
.MinimumLength(0)
.MaximumLength(100)
;

        }
    }
}
