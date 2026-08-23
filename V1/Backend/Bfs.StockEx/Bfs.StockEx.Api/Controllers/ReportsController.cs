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

    [HttpPost("PortfolioCompare")]
    [CustomAuthorize("method=q.portfolioCompare")]
    public async Task<Results<Ok<QueryResponse<PortfolioCompareItem>>, BadRequest<ProblemDetails>>> PortfolioCompare([FromBody] QueryRequest<PortfolioCompareFilter> ReportRequest)
    {
        var result = await _reportsService.PortfolioCompareAsync(ReportRequest).ConfigureAwait(false);
        return TypedResults.Ok(result);
    }

    [HttpPost("PortfolioAggregateCompare")]
    [CustomAuthorize("method=q.portfolioAggregateCompare")]
    public async Task<Results<Ok<QueryResponse<PortfolioAggregateCompareItem>>, BadRequest<ProblemDetails>>> PortfolioAggregateCompare([FromBody] QueryRequest<PortfolioAggregateCompareFilter> ReportRequest)
    {
        var result = await _reportsService.PortfolioAggregateCompareAsync(ReportRequest).ConfigureAwait(false);
        return TypedResults.Ok(result);
    }

    [HttpPost("PortfolioCashTransactionCompare")]
    [CustomAuthorize("method=q.portfolioCashTransactionCompare")]
    public async Task<Results<Ok<QueryResponse<PortfolioCashTransactionCompareItem>>, BadRequest<ProblemDetails>>> PortfolioCashTransactionCompare([FromBody] QueryRequest<PortfolioCashTransactionCompareFilter> ReportRequest)
    {
        var result = await _reportsService.PortfolioCashTransactionCompareAsync(ReportRequest).ConfigureAwait(false);
        return TypedResults.Ok(result);
    }

    [HttpPost("PortfolioCashTransactionAggregateCompare")]
    [CustomAuthorize("method=q.portfolioCashTransactionAggregateCompare")]
    public async Task<Results<Ok<QueryResponse<PortfolioCashTransactionAggregateCompareItem>>, BadRequest<ProblemDetails>>> PortfolioCashTransactionAggregateCompare([FromBody] QueryRequest<PortfolioCashTransactionAggregateCompareFilter> ReportRequest)
    {
        var result = await _reportsService.PortfolioCashTransactionAggregateCompareAsync(ReportRequest).ConfigureAwait(false);
        return TypedResults.Ok(result);
    }

//Template_Component_AddControllerEntry
}

