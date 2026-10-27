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
    public async Task<Results<Ok<SspTransaction>, BadRequest<ProblemDetails>>> RolloutSspTransactionAsync([FromBody] SspTransaction value)
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

        var createdSspTransaction = await _operationsService.RolloutSspTransactionAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(createdSspTransaction);
    }

    [HttpPost("CashTransaction/Rollout")]
    [CustomAuthorize("method=a.cashTransaction")]
    public async Task<Results<Ok<CashTransaction>, BadRequest<ProblemDetails>>> RolloutCashTransactionAsync([FromBody] CashTransaction value)
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

        var createdSspTransaction = await _operationsService.RolloutCashTransactionAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(createdSspTransaction);
    }

    [HttpDelete("SspTransaction/Rollback/{id}")]
    [CustomAuthorize("method=d.sspTransaction")]
    public async Task<Results<Ok, BadRequest<ProblemDetails>>> RollbackSspTransactionAsync(long id)
    {
        await _operationsService.RollbackSspTransactionAsync(id).ConfigureAwait(false);
        return TypedResults.Ok();
    }

    [HttpDelete("CashTransaction/Rollback/{id}")]
    [CustomAuthorize("method=d.cashTransaction")]
    public async Task<Results<Ok, BadRequest<ProblemDetails>>> RollbackCashTransactionAsync(long id)
    {
        await _operationsService.RollbackCashTransactionAsync(id).ConfigureAwait(false);
        return TypedResults.Ok();
    }

    [HttpPut("SspTransaction/Change")]
    [CustomAuthorize("method=u.sspTransaction")]
    public async Task<Results<Ok<SspTransaction>, BadRequest<ProblemDetails>>> ChangeSspTransactionAsync([FromBody] SspTransaction value)
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

        var createdSspTransaction = await _operationsService.ChangeSspTransactionAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(createdSspTransaction);
    }

    [HttpPut("CashTransaction/Change")]
    [CustomAuthorize("method=u.cashTransaction")]
    public async Task<Results<Ok<CashTransaction>, BadRequest<ProblemDetails>>> ChangeCashTransactionAsync([FromBody] CashTransaction value)
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

        var createdSspTransaction = await _operationsService.ChangeCashTransactionAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(createdSspTransaction);
    }

    [HttpPost("Coupon/Process")]
   // [CustomAuthorize("method=u.cashTransaction")]
    public async Task<Results<Ok, BadRequest<ProblemDetails>>> CouponProcess([FromBody] DateTime processDate)
    {
        await _operationsService.ProcessCoupon(processDate).ConfigureAwait(false);
        return TypedResults.Ok();
    }

    //Template_End_Code_DontOverwrite_5
}

