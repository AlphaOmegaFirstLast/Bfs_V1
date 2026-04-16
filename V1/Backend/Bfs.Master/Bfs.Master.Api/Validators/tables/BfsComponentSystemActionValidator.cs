using FluentValidation;
using Bfs.Master.Contracts;
using Bfs.Master.Domain;

namespace Bfs.Master.Api.Validators
{
    public class BfsComponentSystemActionValidator : AbstractValidator<BfsComponentSystemAction>
    {
        public BfsComponentSystemActionValidator()
        {

        }
    }
}
