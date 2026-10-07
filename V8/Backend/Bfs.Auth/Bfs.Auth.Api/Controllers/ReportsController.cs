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
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1
namespace Bfs.Auth.Api.Controllers;

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

    [HttpPost("RoleRepCompare")]
    [CustomAuthorize("method=q.roleRepCompare")]
    public async Task<Results<Ok<QueryResponse<RoleRepCompareItem>>, BadRequest<ProblemDetails>>> RoleRepCompare([FromBody] QueryRequest<RoleRepCompareFilter> ReportRequest)
    {
        var result = await _reportsService.RoleRepCompareAsync(ReportRequest).ConfigureAwait(false);
        return TypedResults.Ok(result);
    }

//Template_Component_AddControllerEntry
//Template_Start_Code_DontOverwrite_5
//Template_End_Code_DontOverwrite_5
}

