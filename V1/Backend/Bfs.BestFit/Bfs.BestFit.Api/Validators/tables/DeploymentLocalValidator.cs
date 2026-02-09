using FluentValidation;
using Bfs.BestFit.Contracts;
using Bfs.BestFit.Domain;

namespace Bfs.BestFit.Api.Validators
{
    public class DeploymentLocalValidator : AbstractValidator<DeploymentLocal>
    {
        public DeploymentLocalValidator()
        {

        }
    }
}
