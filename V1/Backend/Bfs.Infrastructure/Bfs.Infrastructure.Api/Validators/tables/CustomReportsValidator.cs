using FluentValidation;
using Bfs.Infrastructure.Contracts;
using Bfs.Infrastructure.Domain;

namespace Bfs.Infrastructure.Api.Validators
{
    public class CustomReportsValidator : AbstractValidator<CustomReports>
    {
        public CustomReportsValidator()
        {
        RuleFor(x => x.Name)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidName)
.MinimumLength(3)
.MaximumLength(50)
;

        }
    }
}
