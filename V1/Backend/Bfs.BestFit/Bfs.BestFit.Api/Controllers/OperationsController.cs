using Bfs.BestFit.Contracts;
using Bfs.BestFit.Domain.Interfaces;
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

namespace Bfs.BestFit.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class OperationsController
{
    private readonly IOperationsService _operationsService;

    public OperationsController(IOperationsService operationsService)
    {
        _operationsService = operationsService;
    }

    //Template_Start_Code_DontOverwrite_1

    [HttpPost("Component/DuplicateTree")]
    [CustomAuthorize("method=o.DuplicateTree")]
    public async Task<Results<Ok<long>, BadRequest<ProblemDetails>>> DuplicateComponentTreeAsync([FromBody] long componentId)
    {
        var result = await _operationsService.DuplicateComponentTreeAsync(componentId).ConfigureAwait(false);
        return TypedResults.Ok(result);
    }

    [HttpDelete("Component/FieldList/{componentId}")]
    [CustomAuthorize("method=o.DeleteComponentTree")]
    public async Task<Results<Ok, BadRequest<ProblemDetails>>> DeleteComponentTreeAsync([FromRoute] long componentId)
    {
        await _operationsService.DeleteComponentTreeAsync(componentId).ConfigureAwait(false);
        return TypedResults.Ok();
    }

    [HttpPut("System/Deploy/Staging/{id}")]
    [CustomAuthorize("method=o.SystemDeployStaging")]
    public async Task<Results<Ok<string>, InternalServerError<ProblemDetails>>> DeployToAzureStaging(long id)
    {
        try
        {
            await _operationsService.DeployToAzureStaging(id).ConfigureAwait(false);
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

    [HttpPut("ComponentSystemAction/matrix/{parentId}")]
    [CustomAuthorize("method=o.ComponentSystemAction")]
    public async Task<Results<Ok<List<ComponentSystemAction>>, BadRequest<ProblemDetails>>> UpdateComponentSystemActionMatrixAsync([FromRoute] long parentId, [FromBody] List<ComponentSystemAction> matrix)
    {
        var result = await _operationsService.UpdateComponentSystemActionMatrixAsync(parentId, matrix).ConfigureAwait(false);
        return TypedResults.Ok(result);
    }

    [HttpPut("ComponentBusinessAction/matrix/{parentId}")]
    [CustomAuthorize("method=o.ComponentBusinessAction")]
    public async Task<Results<Ok<List<ComponentBusinessAction>>, BadRequest<ProblemDetails>>> UpdateComponentBusinessActionMatrixAsync([FromRoute] long parentId, [FromBody] List<ComponentBusinessAction> matrix)
    {
        var result = await _operationsService.UpdateComponentBusinessActionMatrixAsync(parentId, matrix).ConfigureAwait(false);
        return TypedResults.Ok(result);
    }
    //Template_Field_ChildrenMatrix_AddControllerEntry

}

