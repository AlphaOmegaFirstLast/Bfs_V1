using FluentValidation;
using Bfs.Stores.Contracts;
using Bfs.Stores.Domain;

namespace Bfs.Stores.Api.Validators
{
    public class DocumentDetailsValidator : AbstractValidator<DocumentDetails>
    {
        public DocumentDetailsValidator()
        {
        RuleFor(x => x.Quantity)
.NotEmpty().WithErrorCode(ErrorCodes.InvalidQuantity)
;
RuleFor(x => x.Notes)
.MaximumLength(1000)
;

        }
    }
}

