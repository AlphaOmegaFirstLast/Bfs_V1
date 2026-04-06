using FluentValidation;
using Bfs.Auth.Contracts;
using Bfs.Auth.Domain;

namespace Bfs.Auth.Api.Validators
{
    public class AppValidator : AbstractValidator<App>
    {
        public AppValidator()
        {
        RuleFor(x => x.Name)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidName)
.MinimumLength(3)
.MaximumLength(50)
;
RuleFor(x => x.Notes)
.MaximumLength(1000)
;
RuleFor(x => x.Logo)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidLogo)
.MinimumLength(3)
.MaximumLength(300)
;

        }
    }
}

