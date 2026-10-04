using Bfs.Auth.Contracts;
using Bfs.Auth.Data;
using Bfs.Auth.Domain.Interfaces;
using Bfs.Core.Config;
using Bfs.Core.Middleware;
using Bfs.Core.TenantManagement;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Bfs.Auth.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]

public class AdminController
{
    private readonly IUserRequestService _userRequestService;
    private IValidator<UserRequest> _validator;


    private readonly BfsSettings _bfsSettings;
    public AdminController(
        IOptions<BfsSettings> bfsSettings,
        IUserRequestService userRequestService,
        IValidator<UserRequest> validator)
    {
        _bfsSettings = bfsSettings.Value;
        _userRequestService = userRequestService;
        _validator = validator;
    }

    [HttpPost("Migrate")]
    //  [CustomAuthorize("method=r.migrate")]
    public async Task MigrateTenants()
    {
        var masterConnection = _bfsSettings.DbConnections.MasterConnection;
        await TenantManager.ApplyMigrations<AuthDbContext>(masterConnection, "Auth");
    }

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

}

