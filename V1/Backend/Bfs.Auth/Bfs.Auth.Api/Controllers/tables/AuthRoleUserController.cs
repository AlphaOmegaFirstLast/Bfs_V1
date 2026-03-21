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

public class AuthRoleUserController
{
    private readonly IAuthRoleUserService _authRoleUserService;
    private IValidator<AuthRoleUser> _validator;

    public AuthRoleUserController(IAuthRoleUserService authRoleUserService, IValidator<AuthRoleUser> validator)
    {
        _authRoleUserService = authRoleUserService;
        _validator = validator;
    }

    [HttpGet]
    [CustomAuthorize("role=bfs.admin")]
    public async Task<List<AuthRoleUser>> Get()
    {
        var result = await _authRoleUserService.GetAsync().ConfigureAwait(false);
        return result;
    }

    [HttpGet("{id}")]
    [CustomAuthorize("method=q.authRoleUser")]
    public async Task<AuthRoleUser?> Get(long id)
    {
        var result = await _authRoleUserService.GetAsync(id).ConfigureAwait(false);
        return result;
    }

    [HttpPost]
    [CustomAuthorize("method=a.authRoleUser")]
    public async Task<Results<Ok<AuthRoleUser>, BadRequest<ProblemDetails>>> Post([FromBody] AuthRoleUser value)
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

        var createdAuthRoleUser = await _authRoleUserService.CreateAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(createdAuthRoleUser);
    }

    [HttpPut]
    [CustomAuthorize("method=u.authRoleUser")]
    public async Task<Results<Ok<AuthRoleUser>, BadRequest<ProblemDetails>>> Put([FromBody] AuthRoleUser value)
    {
        ValidationResult validResult = await _validator.ValidateAsync(value);

        if (!validResult.IsValid)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Validation Failed",
                Detail = string.Join("; ", validResult.Errors.Select(e => e.ErrorMessage))
            };
            return TypedResults.BadRequest(problemDetails);
        }

        var updatedAuthRoleUser = await _authRoleUserService.UpdateAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(updatedAuthRoleUser);
    }

    [HttpDelete("{id}")]
    [CustomAuthorize("method=d.authRoleUser")]
    public async Task<Results<Ok, BadRequest<ProblemDetails>>> Delete(long id)
    {
        try
        {
            await _authRoleUserService.DeleteAsync(id).ConfigureAwait(false);
            return TypedResults.Ok();
        }
        catch (Exception ex)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Error",
                Detail = ex.Message
            };
            return TypedResults.BadRequest(problemDetails);
        }
    }

    [HttpPost("List")]
    [CustomAuthorize("method=q.authRoleUser")]
    public async Task<Results<Ok<QueryResponse<AuthRoleUserListItem>>, BadRequest<ProblemDetails>>> List([FromBody] QueryRequest<AuthRoleUserListFilter> listRequest)
    {
        var result = await _authRoleUserService.ListAsync(listRequest).ConfigureAwait(false);
        return TypedResults.Ok(result);
    }

    [HttpPost("upload")]
    [CustomAuthorize("role=bfs.admin")]
    public async Task<Results<Ok, BadRequest<ProblemDetails>>> UploadJson(IFormFile file)
    {
        var problemDetails = new ProblemDetails();

        if (file == null || file.Length == 0)
        {
            problemDetails.Title = "Upload Failed";
            problemDetails.Detail = "No file uploaded.";
            return TypedResults.BadRequest(problemDetails);
        }

        try
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            using var stream = file.OpenReadStream();
            var recordList = await JsonSerializer.DeserializeAsync<List<AuthRoleUser>>(stream, options);

            if (recordList == null)
            {
                problemDetails.Title = "Deserialization Failed";
                problemDetails.Detail = "The uploaded file could not be deserialized into a AuthRoleUser list.";
                return TypedResults.BadRequest(problemDetails);
            }

            foreach (var record in recordList)
            {
                ValidationResult validResult = await _validator.ValidateAsync(record);
                if (!validResult.IsValid)
                {
                    problemDetails.Title = "Validation Failed";
                    problemDetails.Detail = string.Join("; ", validResult.Errors.Select(e => e.ErrorMessage));
                    problemDetails.Extensions = new Dictionary<string, object?>() { { "errors", validResult.Errors.Select(x => new { errorCode = x.ErrorCode, message = x.ErrorMessage }) } };
                    return TypedResults.BadRequest(problemDetails);
                }
                await _authRoleUserService.UploadAsync(record).ConfigureAwait(false);
            }

            return TypedResults.Ok();
        }
        catch (JsonException jsonEx)
        {
            problemDetails.Title = "Invalid JSON format";
            problemDetails.Detail = jsonEx.Message;
            return TypedResults.BadRequest(problemDetails);
        }
    }
//Template_Start_DontOverwrite_1
//Template_End_DontOverwrite_1   

}

