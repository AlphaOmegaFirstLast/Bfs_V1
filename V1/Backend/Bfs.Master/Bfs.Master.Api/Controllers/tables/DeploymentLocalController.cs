using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentValidation.Results;
using Bfs.Core.Contracts;
using Bfs.Core.Middleware;
using Bfs.Master.Contracts;
using Bfs.Master.Domain.Interfaces;

namespace Bfs.Master.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class DeploymentLocalController
{
    private readonly IDeploymentLocalService _deploymentLocalService;
    private IValidator<DeploymentLocal> _validator;

    public DeploymentLocalController(IDeploymentLocalService deploymentLocalService, IValidator<DeploymentLocal> validator)
    {
        _deploymentLocalService = deploymentLocalService;
        _validator = validator;
    }

    [HttpGet]
    [CustomAuthorize("role=bfs.admin")]
    public async Task<List<DeploymentLocal>> Get()
    {
        var result = await _deploymentLocalService.GetAsync().ConfigureAwait(false);
        return result;
    }

    [HttpGet("{id}")]
    [CustomAuthorize("method=q.deploymentLocal")]
    public async Task<DeploymentLocal?> Get(long id)
    {
        var result = await _deploymentLocalService.GetAsync(id).ConfigureAwait(false);
        return result;
    }

    [HttpPost]
    [CustomAuthorize("method=a.deploymentLocal")]
    public async Task<Results<Ok<DeploymentLocal>, BadRequest<ProblemDetails>>> Post([FromBody] DeploymentLocal value)
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

        var createdDeploymentLocal = await _deploymentLocalService.CreateAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(createdDeploymentLocal);
    }

    [HttpPut]
    [CustomAuthorize("method=u.deploymentLocal")]
    public async Task<Results<Ok<DeploymentLocal>, BadRequest<ProblemDetails>>> Put([FromBody] DeploymentLocal value)
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

        var updatedDeploymentLocal = await _deploymentLocalService.UpdateAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(updatedDeploymentLocal);
    }

    [HttpDelete("{id}")]
    [CustomAuthorize("method=d.deploymentLocal")]
    public async Task<Results<Ok, BadRequest<ProblemDetails>>> Delete(long id)
    {
        try
        {
            await _deploymentLocalService.DeleteAsync(id).ConfigureAwait(false);
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
    [CustomAuthorize("method=q.deploymentLocal")]
    public async Task<Results<Ok<QueryResponse<DeploymentLocalListItem>>, BadRequest<ProblemDetails>>> List([FromBody] QueryRequest<DeploymentLocalListFilter> listRequest)
    {
        var result = await _deploymentLocalService.ListAsync(listRequest).ConfigureAwait(false);
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
            var recordList = await JsonSerializer.DeserializeAsync<List<DeploymentLocal>>(stream, options);

            if (recordList == null)
            {
                problemDetails.Title = "Deserialization Failed";
                problemDetails.Detail = "The uploaded file could not be deserialized into a DeploymentLocal list.";
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
                await _deploymentLocalService.UploadAsync(record).ConfigureAwait(false);
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

