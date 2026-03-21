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

    [HttpPut("AuthRoleComponentSystemAction/matrix/{parentId}")]
    [CustomAuthorize("method=o.AuthRoleComponentSystemAction")]
    public async Task<Results<Ok<List<AuthRoleComponentSystemAction>>, BadRequest<ProblemDetails>>> UpdateAuthRoleComponentSystemActionMatrixAsync([FromRoute] long parentId, [FromBody] List<AuthRoleComponentSystemAction> matrix)
    {
        var result = await _operationsService.UpdateAuthRoleComponentSystemActionMatrixAsync(parentId, matrix).ConfigureAwait(false);
        return TypedResults.Ok(result);
    }
//Template_Field_ChildrenMatrix_AddControllerEntry
}

