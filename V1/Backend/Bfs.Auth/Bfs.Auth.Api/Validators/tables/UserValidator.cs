using FluentValidation;
using Bfs.Auth.Contracts;
using Bfs.Auth.Domain;

namespace Bfs.Auth.Api.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
        RuleFor(x => x.AspNetUserId)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidAspNetUserId)
.MinimumLength(3)
.MaximumLength(50)
;
RuleFor(x => x.Notes)
.MaximumLength(1000)
;
RuleFor(x => x.Name)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidName)
.MinimumLength(3)
.MaximumLength(50)
;
RuleFor(x => x.Email)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidEmail)
.MinimumLength(3)
.MaximumLength(50)
;

        }
    }
}
