using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentValidation.Results;
using Bfs.Core.Contracts;
using Bfs.Core.Middleware;
using Bfs.Stores.Contracts;
using Bfs.Stores.Domain.Interfaces;

namespace Bfs.Stores.Api.Controllers;

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

    [HttpPost("ProductTransactionCompare")]
    [CustomAuthorize("method=q.productTransactionCompare")]
    public async Task<Results<Ok<QueryResponse<ProductTransactionCompareItem>>, BadRequest<ProblemDetails>>> ProductTransactionCompare([FromBody] QueryRequest<ProductTransactionCompareFilter> ReportRequest)
    {
        var result = await _reportsService.ProductTransactionCompareAsync(ReportRequest).ConfigureAwait(false);
        return TypedResults.Ok(result);
    }

//Template_Component_AddControllerEntry
}

