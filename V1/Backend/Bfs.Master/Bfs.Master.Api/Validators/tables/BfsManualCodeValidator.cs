using FluentValidation;
using Bfs.Master.Contracts;
using Bfs.Master.Domain;

namespace Bfs.Master.Api.Validators
{
    public class BfsManualCodeValidator : AbstractValidator<BfsManualCode>
    {
        public BfsManualCodeValidator()
        {

        }
    }
}
