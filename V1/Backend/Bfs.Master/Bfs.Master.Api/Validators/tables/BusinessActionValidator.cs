using FluentValidation;
using Bfs.Master.Contracts;
using Bfs.Master.Domain;

namespace Bfs.Master.Api.Validators
{
    public class BusinessActionValidator : AbstractValidator<BusinessAction>
    {
        public BusinessActionValidator()
        {
        RuleFor(x => x.ShortName)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidShortName)
.MinimumLength(1)
.MaximumLength(3)
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
RuleFor(x => x.Notes)
.MaximumLength(1000)
;

        }
    }
}

