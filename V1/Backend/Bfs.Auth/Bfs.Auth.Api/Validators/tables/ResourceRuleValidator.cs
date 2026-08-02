using FluentValidation;
using Bfs.Auth.Contracts;
using Bfs.Auth.Domain;

namespace Bfs.Auth.Api.Validators
{
    public class ResourceRuleValidator : AbstractValidator<ResourceRule>
    {
        public ResourceRuleValidator()
        {
        RuleFor(x => x.SelectBlackList)
.MaximumLength(1000)
;
RuleFor(x => x.JoinStatement)
.MaximumLength(1000)
;
RuleFor(x => x.WhereStatement)
.MaximumLength(1000)
;
RuleFor(x => x.ParameterName)
.MaximumLength(1000)
;
RuleFor(x => x.ParameterValue)
.MaximumLength(1000)
;
RuleFor(x => x.ParameterType)
.MaximumLength(1000)
;

        }
    }
}
