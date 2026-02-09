using FluentValidation;
using Bfs.Infrastructure.Contracts;
using Bfs.Infrastructure.Domain;

namespace Bfs.Infrastructure.Api.Validators
{
    public class BfsComponentBusinessActionValidator : AbstractValidator<BfsComponentBusinessAction>
    {
        public BfsComponentBusinessActionValidator()
        {

        }
    }
}
