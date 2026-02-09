using FluentValidation;
using Bfs.Infrastructure.Contracts;
using Bfs.Infrastructure.Domain;

namespace Bfs.Infrastructure.Api.Validators
{
    public class BfsFieldValidator : AbstractValidator<BfsField>
    {
        public BfsFieldValidator()
        {

        }
    }
}
