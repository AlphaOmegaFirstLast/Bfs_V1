using FluentValidation;
using Bfs.Infrastructure.Contracts;
using Bfs.Infrastructure.Domain;

namespace Bfs.Infrastructure.Api.Validators
{
    public class DeploymentAzureValidator : AbstractValidator<DeploymentAzure>
    {
        public DeploymentAzureValidator()
        {
        RuleFor(x => x.Project)
.MaximumLength(1000)
;
RuleFor(x => x.ScriptFile)
.MaximumLength(1000)
;
RuleFor(x => x.SourceProject)
.MaximumLength(1000)
;
RuleFor(x => x.SourcePath)
.MaximumLength(1000)
;
RuleFor(x => x.PublishPath)
.MaximumLength(1000)
;
RuleFor(x => x.Config)
.MaximumLength(1000)
;
RuleFor(x => x.EnvironmentValue)
.MaximumLength(1000)
;
RuleFor(x => x.TargetVirtualFolder)
.MaximumLength(1000)
;
RuleFor(x => x.PublishProfilePath)
.MaximumLength(1000)
;
RuleFor(x => x.AppService)
.MaximumLength(1000)
;
RuleFor(x => x.ResourceGroup)
.MaximumLength(1000)
;

        }
    }
}
