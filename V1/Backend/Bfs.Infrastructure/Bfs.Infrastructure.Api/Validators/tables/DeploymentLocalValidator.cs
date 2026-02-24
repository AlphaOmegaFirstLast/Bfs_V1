using FluentValidation;
using Bfs.Infrastructure.Contracts;
using Bfs.Infrastructure.Domain;

namespace Bfs.Infrastructure.Api.Validators
{
    public class DeploymentLocalValidator : AbstractValidator<DeploymentLocal>
    {
        public DeploymentLocalValidator()
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
RuleFor(x => x.WebSite)
.MaximumLength(1000)
;
RuleFor(x => x.AppPoolName)
.MaximumLength(1000)
;
RuleFor(x => x.Port)
.MaximumLength(1000)
;

        }
    }
}
