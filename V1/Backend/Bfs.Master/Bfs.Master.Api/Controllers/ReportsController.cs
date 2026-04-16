using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentValidation.Results;
using Bfs.Core.Contracts;
using Bfs.Core.Middleware;
using Bfs.Master.Contracts;
using Bfs.Master.Domain.Interfaces;

namespace Bfs.Master.Api.Controllers;

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

    [HttpPost("StructureCompare")]
    [CustomAuthorize("method=q.structureCompare")]
    public async Task<Results<Ok<QueryResponse<StructureCompareItem>>, BadRequest<ProblemDetails>>> StructureCompare([FromBody] QueryRequest<StructureCompareFilter> ReportRequest)
    {
        var result = await _reportsService.StructureCompareAsync(ReportRequest).ConfigureAwait(false);
        return TypedResults.Ok(result);
    }

//Template_Component_AddControllerEntry
}

