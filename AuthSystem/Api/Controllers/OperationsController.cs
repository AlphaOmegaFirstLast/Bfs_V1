using Bfs.Auth.Contracts;
using Bfs.Auth.Domain.Interfaces;
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

namespace Bfs.Auth.Api.Controllers;

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

    [HttpPut("RoleComponentSystemAction/matrix/{parentId}")]
    [CustomAuthorize("method=o.RoleComponentSystemAction")]
    public async Task<Results<Ok<List<RoleComponentSystemAction>>, BadRequest<ProblemDetails>>> UpdateRoleComponentSystemActionMatrixAsync([FromRoute] long parentId, [FromBody] List<RoleComponentSystemAction> matrix)
    {
        var result = await _operationsService.UpdateRoleComponentSystemActionMatrixAsync(parentId, matrix).ConfigureAwait(false);
        return TypedResults.Ok(result);
    }
//Template_Field_ChildrenMatrix_AddControllerEntry
}

