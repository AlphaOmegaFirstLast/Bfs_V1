using FluentValidation;
using Bfs.Master.Contracts;
using Bfs.Master.Domain;

namespace Bfs.Master.Api.Validators
{
    public class DeploymentAzureValidator : AbstractValidator<DeploymentAzure>
    {
        public DeploymentAzureValidator()
        {
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
RuleFor(x => x.TargetVirtualDir)
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
