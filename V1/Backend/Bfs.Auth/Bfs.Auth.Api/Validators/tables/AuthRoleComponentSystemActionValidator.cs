using FluentValidation;
using Bfs.Auth.Contracts;
using Bfs.Auth.Domain;

namespace Bfs.Auth.Api.Validators
{
    public class AuthRoleComponentSystemActionValidator : AbstractValidator<AuthRoleComponentSystemAction>
    {
        public AuthRoleComponentSystemActionValidator()
        {

        }
    }
}
