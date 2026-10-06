using Bfs.StockEx.Contracts;
using Bfs.StockEx.Domain.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.Middleware;
using Bfs.Core.Services.AI;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Text.Json;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class OperationsController
{
    private readonly IOperationsService _operationsService;
//Template_Start_Code_DontOverwrite_2
private IValidator<SspTransaction> _sspTransactionValidator;
    private IValidator<CashTransaction> _cashTransactionValidator;
//Template_End_Code_DontOverwrite_2

    public OperationsController(IOperationsService operationsService
//Template_Start_Code_DontOverwrite_3
, IValidator<SspTransaction> validator
        , IValidator<CashTransaction> cashValidator
//Template_End_Code_DontOverwrite_3

    )
    {
        _operationsService = operationsService;
//Template_Start_Code_DontOverwrite_4
_sspTransactionValidator = validator;
        _cashTransactionValidator = cashValidator;
//Template_End_Code_DontOverwrite_4

    }
//Template_Field_ChildrenMatrix_AddControllerEntry
//Template_Start_Code_DontOverwrite_5
[HttpPost("SspTransaction/Rollout")]
    [CustomAuthorize("method=a.sspTransaction")]
    public async Task<Results<Ok<SspTransaction>, BadRequest<ProblemDetails>>> Post([FromBody] SspTransaction value)
    {
        ValidationResult validResult = await _sspTransactionValidator.ValidateAsync(value);

        if (!validResult.IsValid)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Validation Failed",
                Detail = string.Join("; ", validResult.Errors.Select(e => e.ErrorMessage)),
                Extensions = new Dictionary<string, object?>() { { "errors", validResult.Errors.Select(x => new { errorCode = x.ErrorCode, message = x.ErrorMessage }) } }

            };
            return TypedResults.BadRequest(problemDetails);
        }

        var createdSspTransaction = await _operationsService.RolloutTransactionAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(createdSspTransaction);
    }

    [HttpPost("CashTransaction/Rollout")]
    [CustomAuthorize("method=a.cashTransaction")]
    public async Task<Results<Ok<CashTransaction>, BadRequest<ProblemDetails>>> Post([FromBody] CashTransaction value)
    {
        ValidationResult validResult = await _cashTransactionValidator.ValidateAsync(value);

        if (!validResult.IsValid)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Validation Failed",
                Detail = string.Join("; ", validResult.Errors.Select(e => e.ErrorMessage)),
                Extensions = new Dictionary<string, object?>() { { "errors", validResult.Errors.Select(x => new { errorCode = x.ErrorCode, message = x.ErrorMessage }) } }

            };
            return TypedResults.BadRequest(problemDetails);
        }

        var createdSspTransaction = await _operationsService.RolloutTransactionAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(createdSspTransaction);
    }
//Template_End_Code_DontOverwrite_5

}

