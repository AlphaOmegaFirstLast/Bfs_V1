using FluentValidation;
using Bfs.Master.Contracts;
using Bfs.Master.Domain;

namespace Bfs.Master.Api.Validators
{
    public class SystemTemplateValidator : AbstractValidator<SystemTemplate>
    {
        public SystemTemplateValidator()
        {
        RuleFor(x => x.Name)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidName)
.MinimumLength(3)
.MaximumLength(50)
;
RuleFor(x => x.Notes)
.MaximumLength(1000)
;
RuleFor(x => x.OutputDirectory)
.MaximumLength(1000)
;
RuleFor(x => x.SolutionDirectory)
.MaximumLength(1000)
;
RuleFor(x => x.Template)
.MaximumLength(1000)
;

        }
    }
}
