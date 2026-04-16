using FluentValidation;
using Bfs.Master.Contracts;
using Bfs.Master.Domain;

namespace Bfs.Master.Api.Validators
{
    public class BfsComponentBusinessActionValidator : AbstractValidator<BfsComponentBusinessAction>
    {
        public BfsComponentBusinessActionValidator()
        {

        }
    }
}
