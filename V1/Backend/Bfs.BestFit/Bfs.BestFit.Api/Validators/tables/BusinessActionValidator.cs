using FluentValidation;
using Bfs.BestFit.Contracts;
using Bfs.BestFit.Domain;

namespace Bfs.BestFit.Api.Validators
{
    public class BusinessActionValidator : AbstractValidator<BusinessAction>
    {
        public BusinessActionValidator()
        {
        RuleFor(x => x.Name)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidName)
.MinimumLength(3)
.MaximumLength(50)
;
RuleFor(x => x.Notes)
.MaximumLength(50)
;

        }
    }
}
