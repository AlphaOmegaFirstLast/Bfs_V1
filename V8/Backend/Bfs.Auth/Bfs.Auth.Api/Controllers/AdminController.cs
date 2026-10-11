using Bfs.Auth.Contracts;
using Bfs.Auth.Data;
using Bfs.Auth.Domain.Interfaces;
using Bfs.Core.Config;
using Bfs.Core.Middleware;
using Bfs.Core.TenantManagement;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
//Template_Start_Code_DontOverwrite_1
using FluentValidation.Results;
//Template_End_Code_DontOverwrite_1

namespace Bfs.Auth.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class AdminController
{
    private readonly BfsSettings _bfsSettings;
    //Template_Start_Code_DontOverwrite_2
    private readonly IUserRequestService _userRequestService;
    private IValidator<UserRequest> _validator;

    //Template_End_Code_DontOverwrite_2
    public AdminController(
        IOptions<BfsSettings> bfsSettings
        //Template_Start_Code_DontOverwrite_3
        , IUserRequestService userRequestService
        , IValidator<UserRequest> validator
        //Template_End_Code_DontOverwrite_3
        )
    {
        _bfsSettings = bfsSettings.Value;
        //Template_Start_Code_DontOverwrite_4
        _userRequestService = userRequestService;
        _validator = validator;
        //Template_End_Code_DontOverwrite_4
    }

    [HttpPost("Migrate")]
    [CustomAuthorize("method=r.migrate")]
    public async Task MigrateTenants()
    {
        var masterConnection = _bfsSettings.DbConnections.MasterConnection;
        await TenantManager.ApplyMigrations<AuthDbContext>(masterConnection, "Auth");
    }

    //Template_Start_Code_DontOverwrite_5
    [HttpPost("UserRequest")]
    [CustomAuthorize("method=a.userRequest")]
    public async Task<Results<Ok<UserRequest>, BadRequest<ProblemDetails>>> PostUserRequest([FromBody] UserRequest value)
    {
        ValidationResult validResult = await _validator.ValidateAsync(value);

        if (!validResult.IsValid)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Validation Failed",
                Detail = string.Join("; ", validResult.Errors.Select(e => e.ErrorMessage)),
                Extensions = new Dictionary<string, object?>() { { "errors", validResult.Errors.Select(x => new { errorCode = x.ErrorCode, message = x.ErrorMessage }) } }

            };
            return TypedResults.BadRequest(problemDetails);
        }

        var createdUserRequest = await _userRequestService.CreateAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(createdUserRequest);
    }
    //Template_End_Code_DontOverwrite_5
}

