using FluentValidation;
using Bfs.Auth.Contracts;
using Bfs.Auth.Domain;

namespace Bfs.Auth.Api.Validators
{
    public class AuthRoleUserValidator : AbstractValidator<AuthRoleUser>
    {
        public AuthRoleUserValidator()
        {
            RuleFor(x => x.AuthUserId).NotEmpty();

        }
    }
}
