using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentValidation.Results;
using Bfs.Core.Contracts;
using Bfs.Core.Middleware;
using Bfs.Auth.Contracts;
using Bfs.Auth.Domain.Interfaces;

namespace Bfs.Auth.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class ReportsController
{
    private readonly IReportsService _reportsService;

    public ReportsController(IReportsService reportsService)
    {
        _reportsService = reportsService;
    }

    [HttpPost("RoleRepCompare")]
    [CustomAuthorize("method=q.roleRepCompare")]
    public async Task<Results<Ok<QueryResponse<RoleRepCompareItem>>, BadRequest<ProblemDetails>>> RoleRepCompare([FromBody] QueryRequest<RoleRepCompareFilter> ReportRequest)
    {
        var result = await _reportsService.RoleRepCompareAsync(ReportRequest).ConfigureAwait(false);
        return TypedResults.Ok(result);
    }

//Template_Component_AddControllerEntry
}

