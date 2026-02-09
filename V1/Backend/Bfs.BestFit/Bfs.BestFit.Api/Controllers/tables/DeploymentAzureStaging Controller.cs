using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentValidation.Results;
using Bfs.Core.Contracts;
using Bfs.Core.Middleware;
using Bfs.BestFit.Contracts;
using Bfs.BestFit.Domain.Interfaces;

namespace Bfs.BestFit.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class DeploymentAzureStagingController
{
    private readonly IDeploymentAzureStagingService _deploymentAzureStagingService;
    private IValidator<DeploymentAzureStaging> _validator;

    public DeploymentAzureStagingController(IDeploymentAzureStagingService deploymentAzureStagingService, IValidator<DeploymentAzureStaging> validator)
    {
        _deploymentAzureStagingService = deploymentAzureStagingService;
        _validator = validator;
    }

    [HttpGet]
    [CustomAuthorize("role=bfs.admin")]
    public async Task<List<DeploymentAzureStaging>> Get()
    {
        var result = await _deploymentAzureStagingService.GetAsync().ConfigureAwait(false);
        return result;
    }

    [HttpGet("{id}")]
    [CustomAuthorize("method=q.deploymentAzureStaging")]
    public async Task<DeploymentAzureStaging?> Get(long id)
    {
        var result = await _deploymentAzureStagingService.GetAsync(id).ConfigureAwait(false);
        return result;
    }

    [HttpPost]
    [CustomAuthorize("method=a.deploymentAzureStaging")]
    public async Task<Results<Ok<DeploymentAzureStaging>, BadRequest<ProblemDetails>>> Post([FromBody] DeploymentAzureStaging value)
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

        var createdDeploymentAzureStaging = await _deploymentAzureStagingService.CreateAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(createdDeploymentAzureStaging);
    }

    [HttpPut]
    [CustomAuthorize("method=u.deploymentAzureStaging")]
    public async Task<Results<Ok<DeploymentAzureStaging>, BadRequest<ProblemDetails>>> Put([FromBody] DeploymentAzureStaging value)
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

        var updatedDeploymentAzureStaging = await _deploymentAzureStagingService.UpdateAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(updatedDeploymentAzureStaging);
    }

    [HttpDelete("{id}")]
    [CustomAuthorize("method=d.deploymentAzureStaging")]
    public async Task<Results<Ok, BadRequest<ProblemDetails>>> Delete(long id)
    {
        try
        {
            await _deploymentAzureStagingService.DeleteAsync(id).ConfigureAwait(false);
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
    [CustomAuthorize("method=q.deploymentAzureStaging")]
    public async Task<Results<Ok<QueryResponse<DeploymentAzureStagingListItem>>, BadRequest<ProblemDetails>>> List([FromBody] QueryRequest<DeploymentAzureStagingListFilter> listRequest)
    {
        var result = await _deploymentAzureStagingService.ListAsync(listRequest).ConfigureAwait(false);
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
            var recordList = await JsonSerializer.DeserializeAsync<List<DeploymentAzureStaging>>(stream, options);

            if (recordList == null)
            {
                problemDetails.Title = "Deserialization Failed";
                problemDetails.Detail = "The uploaded file could not be deserialized into a DeploymentAzureStaging list.";
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
                await _deploymentAzureStagingService.UploadAsync(record).ConfigureAwait(false);
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

