using FluentValidation;
using Bfs.Infrastructure.Contracts;
using Bfs.Infrastructure.Domain;

namespace Bfs.Infrastructure.Api.Validators
{
    public class BfsTenantValidator : AbstractValidator<BfsTenant>
    {
        public BfsTenantValidator()
        {
        RuleFor(x => x.DbConnection)
.MaximumLength(300)
;
RuleFor(x => x.Logo)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidLogo)
.MinimumLength(3)
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

        }
    }
}

//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1
//Template_Start_Code_DontOverwrite_2

//Template_End_Code_DontOverwrite_2

