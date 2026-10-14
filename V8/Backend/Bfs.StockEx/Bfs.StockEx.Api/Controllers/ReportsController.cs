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
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class ReportsController
{
    private readonly IReportsService _reportsService;
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    public ReportsController(IReportsService reportsService
//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3

)
    {
        _reportsService = reportsService;
//Template_Start_Code_DontOverwrite_4
//Template_End_Code_DontOverwrite_4

    }

    [HttpPost("TradingRoomRepCompare")]
    [CustomAuthorize("method=q.tradingRoomRepCompare")]
    public async Task<Results<Ok<QueryResponse<TradingRoomRepCompareItem>>, BadRequest<ProblemDetails>>> TradingRoomRepCompare([FromBody] QueryRequest<TradingRoomRepCompareFilter> ReportRequest)
    {
        var result = await _reportsService.TradingRoomRepCompareAsync(ReportRequest).ConfigureAwait(false);
        return TypedResults.Ok(result);
    }

    [HttpPost("PortfolioStockShareTransactionCompare")]
    [CustomAuthorize("method=q.portfolioStockShareTransactionCompare")]
    public async Task<Results<Ok<QueryResponse<PortfolioStockShareTransactionCompareItem>>, BadRequest<ProblemDetails>>> PortfolioStockShareTransactionCompare([FromBody] QueryRequest<PortfolioStockShareTransactionCompareFilter> ReportRequest)
    {
        var result = await _reportsService.PortfolioStockShareTransactionCompareAsync(ReportRequest).ConfigureAwait(false);
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

    [HttpPost("NavReportCompare")]
    [CustomAuthorize("method=q.navReportCompare")]
    public async Task<Results<Ok<QueryResponse<NavReportCompareItem>>, BadRequest<ProblemDetails>>> NavReportCompare([FromBody] QueryRequest<NavReportCompareFilter> ReportRequest)
    {
        var result = await _reportsService.NavReportCompareAsync(ReportRequest).ConfigureAwait(false);
        return TypedResults.Ok(result);
    }

    [HttpPost("OverdraftRepCompare")]
    [CustomAuthorize("method=q.overdraftRepCompare")]
    public async Task<Results<Ok<QueryResponse<OverdraftRepCompareItem>>, BadRequest<ProblemDetails>>> OverdraftRepCompare([FromBody] QueryRequest<OverdraftRepCompareFilter> ReportRequest)
    {
        var result = await _reportsService.OverdraftRepCompareAsync(ReportRequest).ConfigureAwait(false);
        return TypedResults.Ok(result);
    }

//Template_Component_AddControllerEntry
//Template_Start_Code_DontOverwrite_5
//Template_End_Code_DontOverwrite_5

}

