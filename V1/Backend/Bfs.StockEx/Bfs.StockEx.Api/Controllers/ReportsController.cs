using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentValidation.Results;
using Bfs.Core.Contracts;
using Bfs.Core.Middleware;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Domain.Interfaces;

namespace Bfs.StockEx.Api.Controllers;

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

    [HttpPost("TradingRoomRepCompare")]
    [CustomAuthorize("method=q.tradingRoomRepCompare")]
    public async Task<Results<Ok<QueryResponse<TradingRoomRepCompareItem>>, BadRequest<ProblemDetails>>> TradingRoomRepCompare([FromBody] QueryRequest<TradingRoomRepCompareFilter> ReportRequest)
    {
        var result = await _reportsService.TradingRoomRepCompareAsync(ReportRequest).ConfigureAwait(false);
        return TypedResults.Ok(result);
    }

//Template_Component_AddControllerEntry
}

