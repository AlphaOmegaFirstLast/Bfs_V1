using FluentValidation;
using Bfs.BestFit.Contracts;
using Bfs.BestFit.Domain;

namespace Bfs.BestFit.Api.Validators
{
    public class TableFieldValidator : AbstractValidator<TableField>
    {
        public TableFieldValidator()
        {

        }
    }
}
