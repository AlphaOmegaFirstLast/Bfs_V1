using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentValidation.Results;
using Bfs.Core.Contracts;
using Bfs.Core.Middleware;
using Bfs.Infrastructure.Contracts;
using Bfs.Infrastructure.Domain.Interfaces;

namespace Bfs.Infrastructure.Api.Controllers;

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

    [HttpPost("StructureReportReport")]
    [CustomAuthorize("method=q.structureReport")]
    public async Task<Results<Ok<QueryResponse<StructureReportItem>>, BadRequest<ProblemDetails>>> StructureReportReport([FromBody] QueryRequest<StructureReportFilter> ReportRequest)
    {
        var result = await _reportsService.StructureReportReportAsync(ReportRequest).ConfigureAwait(false);
        return TypedResults.Ok(result);
    }

//Template_Component_AddControllerEntry
}

