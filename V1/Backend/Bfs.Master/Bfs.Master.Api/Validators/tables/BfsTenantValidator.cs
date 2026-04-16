using FluentValidation;
using Bfs.Master.Contracts;
using Bfs.Master.Domain;

namespace Bfs.Master.Api.Validators
{
    public class BfsTenantValidator : AbstractValidator<BfsTenant>
    {
        public BfsTenantValidator()
        {
        RuleFor(x => x.DbConnection)
.MaximumLength(300)
;
RuleFor(x => x.Notes)
.MaximumLength(1000)
;
RuleFor(x => x.Name)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidName)
.MinimumLength(3)
.MaximumLength(50)
;
RuleFor(x => x.CompanyName)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidCompanyName)
.MinimumLength(3)
.MaximumLength(50)
;
RuleFor(x => x.Logo)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidLogo)
.MinimumLength(3)
.MaximumLength(300)
;

        }
    }
}
