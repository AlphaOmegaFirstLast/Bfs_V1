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

public class AuthRoleComponentSystemActionController
{
    private readonly IAuthRoleComponentSystemActionService _authRoleComponentSystemActionService;
    private IValidator<AuthRoleComponentSystemAction> _validator;

    public AuthRoleComponentSystemActionController(IAuthRoleComponentSystemActionService authRoleComponentSystemActionService, IValidator<AuthRoleComponentSystemAction> validator)
    {
        _authRoleComponentSystemActionService = authRoleComponentSystemActionService;
        _validator = validator;
    }

    [HttpGet]
    [CustomAuthorize("role=bfs.admin")]
    public async Task<List<AuthRoleComponentSystemAction>> Get()
    {
        var result = await _authRoleComponentSystemActionService.GetAsync().ConfigureAwait(false);
        return result;
    }

    [HttpGet("{id}")]
    [CustomAuthorize("method=q.authRoleComponentSystemAction")]
    public async Task<AuthRoleComponentSystemAction?> Get(long id)
    {
        var result = await _authRoleComponentSystemActionService.GetAsync(id).ConfigureAwait(false);
        return result;
    }

    [HttpPost]
    [CustomAuthorize("method=a.authRoleComponentSystemAction")]
    public async Task<Results<Ok<AuthRoleComponentSystemAction>, BadRequest<ProblemDetails>>> Post([FromBody] AuthRoleComponentSystemAction value)
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

        var createdAuthRoleComponentSystemAction = await _authRoleComponentSystemActionService.CreateAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(createdAuthRoleComponentSystemAction);
    }

    [HttpPut]
    [CustomAuthorize("method=u.authRoleComponentSystemAction")]
    public async Task<Results<Ok<AuthRoleComponentSystemAction>, BadRequest<ProblemDetails>>> Put([FromBody] AuthRoleComponentSystemAction value)
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

        var updatedAuthRoleComponentSystemAction = await _authRoleComponentSystemActionService.UpdateAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(updatedAuthRoleComponentSystemAction);
    }

    [HttpDelete("{id}")]
    [CustomAuthorize("method=d.authRoleComponentSystemAction")]
    public async Task<Results<Ok, BadRequest<ProblemDetails>>> Delete(long id)
    {
        try
        {
            await _authRoleComponentSystemActionService.DeleteAsync(id).ConfigureAwait(false);
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
    [CustomAuthorize("method=q.authRoleComponentSystemAction")]
    public async Task<Results<Ok<QueryResponse<AuthRoleComponentSystemActionListItem>>, BadRequest<ProblemDetails>>> List([FromBody] QueryRequest<AuthRoleComponentSystemActionListFilter> listRequest)
    {
        var result = await _authRoleComponentSystemActionService.ListAsync(listRequest).ConfigureAwait(false);
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
            var recordList = await JsonSerializer.DeserializeAsync<List<AuthRoleComponentSystemAction>>(stream, options);

            if (recordList == null)
            {
                problemDetails.Title = "Deserialization Failed";
                problemDetails.Detail = "The uploaded file could not be deserialized into a AuthRoleComponentSystemAction list.";
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
                await _authRoleComponentSystemActionService.UploadAsync(record).ConfigureAwait(false);
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

