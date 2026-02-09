using Bfs.Infrastructure.Contracts;
using Bfs.Infrastructure.Domain.Interfaces;
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

namespace Bfs.Infrastructure.Api.Controllers;

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
//Template_Field_ChildrenMatrix_AddControllerEntry
}

