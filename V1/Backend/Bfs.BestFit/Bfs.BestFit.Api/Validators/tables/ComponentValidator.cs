using FluentValidation;
using Bfs.BestFit.Contracts;
using Bfs.BestFit.Domain;

namespace Bfs.BestFit.Api.Validators
{
    public class ComponentValidator : AbstractValidator<Component>
    {
        public ComponentValidator()
        {
        RuleFor(x => x.Name)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidName)
.MinimumLength(3)
.MaximumLength(100)
;
RuleFor(x => x.Notes)
.MaximumLength(1000)
;

        }
    }
}
