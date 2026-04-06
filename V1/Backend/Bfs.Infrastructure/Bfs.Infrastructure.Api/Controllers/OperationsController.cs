using Azure;
using Bfs.Core.Contracts;
using Bfs.Core.Middleware;
using Bfs.Core.Services.AI;
using Bfs.Infrastructure.Contracts;
using Bfs.Infrastructure.Domain.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Metrics;
using System.ComponentModel;
using System.Text.Json;

namespace Bfs.Infrastructure.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OperationsController : ControllerBase
{
    private readonly IOperationsService _operationsService;

    public OperationsController(IOperationsService operationsService)
    {
        _operationsService = operationsService;
    }


    [HttpGet("TenantToken")]
    public IResult SetTenantCookie([FromQuery] string subject)
    {
        // Append a cookie named "TenantToken". Use a safe default for null subject.
        Response.Cookies.Append("Tenant-token", subject ?? string.Empty, new CookieOptions { HttpOnly = true, Secure = true });
        return TypedResults.Ok();
    }

    [HttpPut("BfsComponentSystemAction/matrix/{parentId}")]
    [CustomAuthorize("method=o.BfsComponentSystemAction")]
    public async Task<Results<Ok<List<BfsComponentSystemAction>>, BadRequest<ProblemDetails>>> UpdateBfsComponentSystemActionMatrixAsync([FromRoute] long parentId, [FromBody] List<BfsComponentSystemAction> matrix)
    {
        var result = await _operationsService.UpdateBfsComponentSystemActionMatrixAsync(parentId, matrix).ConfigureAwait(false);
        return TypedResults.Ok(result);
    }

    [HttpPut("BfsComponentBusinessAction/matrix/{parentId}")]
    [CustomAuthorize("method=o.BfsComponentBusinessAction")]
    public async Task<Results<Ok<List<BfsComponentBusinessAction>>, BadRequest<ProblemDetails>>> UpdateBfsComponentBusinessActionMatrixAsync([FromRoute] long parentId, [FromBody] List<BfsComponentBusinessAction> matrix)
    {
        var result = await _operationsService.UpdateBfsComponentBusinessActionMatrixAsync(parentId, matrix).ConfigureAwait(false);
        return TypedResults.Ok(result);
    }

    //Template_Start_Code_DontOverwrite_1

    [HttpPut("BfsTenantSystem/matrix/{parentId}")]
    [CustomAuthorize("method=o.BfsTenantSystem")]
    public async Task<Results<Ok<List<BfsTenantSystem>>, BadRequest<ProblemDetails>>> UpdateBfsTenantSystemMatrixAsync([FromRoute] long parentId, [FromBody] List<BfsTenantSystem> matrix)
    {
        var result = await _operationsService.UpdateBfsTenantSystemMatrixAsync(parentId, matrix).ConfigureAwait(false);
        return TypedResults.Ok(result);
    }

    [HttpPost("BfsComponent/DuplicateTree")]
    [CustomAuthorize("method=o.DuplicateTree")]
    public async Task<Results<Ok<long>, BadRequest<ProblemDetails>>> DuplicateComponentTreeAsync([FromBody] long componentId)
    {
        var result = await _operationsService.DuplicateComponentTreeAsync(componentId).ConfigureAwait(false);
        return TypedResults.Ok(result);
    }

    [HttpDelete("BfsComponent/DeleteTree/{componentId}")]
    [CustomAuthorize("method=o.DeleteComponentTree")]
    public async Task<Results<Ok, BadRequest<ProblemDetails>>> DeleteComponentTreeAsync([FromRoute] long componentId)
    {
        await _operationsService.DeleteComponentTreeAsync(componentId).ConfigureAwait(false);
        return TypedResults.Ok();
    }

    [HttpPut("BfsSystem/Publish/Local/{id}")]
    [CustomAuthorize("method=o.publish")]
    public async Task<Results<Ok<string>, InternalServerError<ProblemDetails>>> Publish(long id)
    {
        try
        {
            await _operationsService.PublishToLocal(id).ConfigureAwait(false);
            return TypedResults.Ok("Done");
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError(new ProblemDetails() { Detail = ex.Message });
        }
    }

    [HttpPut("BfsSystem/Deploy/Azure/{id}")]
    [CustomAuthorize("method=o.DeployAzure")]
    public async Task<Results<Ok<string>, InternalServerError<ProblemDetails>>> DeployToAzure(long id)
    {
        try
        {
            await _operationsService.DeployToAzure(id).ConfigureAwait(false);
            return TypedResults.Ok("Done");
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError(new ProblemDetails() { Detail = ex.Message });
        }
    }

    [HttpPut("BfsSystem/Deploy/Local/{id}")]
    [CustomAuthorize("method=o.DeployLocal")]
    public async Task<Results<Ok<string>, InternalServerError<ProblemDetails>>> DeployToLocal(long id)
    {
        try
        {
            await _operationsService.DeployToLocal(id).ConfigureAwait(false);
            return TypedResults.Ok("Done");
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError(new ProblemDetails() { Detail = ex.Message });
        }
    }

    [HttpPost("TestData")]
    [CustomAuthorize("method=o.TestData")]
    public async Task<Results<Ok<string>, BadRequest<ProblemDetails>>> TestDataAsync([FromBody] string subject)
    {
        var result = await ChatGpt.SendCompletionRequest(subject).ConfigureAwait(false);
        return TypedResults.Ok(result);
    }
    //Template_End_Code_DontOverwrite_1

    [HttpPut("BfsClientSystem/matrix/{parentId}")]
    [CustomAuthorize("method=o.BfsClientSystem")]
    public async Task<Results<Ok<List<BfsClientSystem>>, BadRequest<ProblemDetails>>> UpdateBfsClientSystemMatrixAsync([FromRoute] long parentId, [FromBody] List<BfsClientSystem> matrix)
    {
        var result = await _operationsService.UpdateBfsClientSystemMatrixAsync(parentId, matrix).ConfigureAwait(false);
        return TypedResults.Ok(result);
    }

//Template_Field_ChildrenMatrix_AddControllerEntry
}