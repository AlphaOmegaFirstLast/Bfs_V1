using FluentValidation;
using Bfs.Stores.Contracts;
using Bfs.Stores.Domain;

namespace Bfs.Stores.Api.Validators
{
    public class DocumentValidator : AbstractValidator<Document>
    {
        public DocumentValidator()
        {
        RuleFor(x => x.Name)
.MaximumLength(1000)
;
RuleFor(x => x.Notes)
.MaximumLength(1000)
;

        }
    }
}

