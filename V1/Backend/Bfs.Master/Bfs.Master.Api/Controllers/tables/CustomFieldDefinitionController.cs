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

public class CustomFieldDefinitionController
{
    private readonly ICustomFieldDefinitionService _customFieldDefinitionService;
    private IValidator<CustomFieldDefinition> _validator;

    public CustomFieldDefinitionController(ICustomFieldDefinitionService customFieldDefinitionService, IValidator<CustomFieldDefinition> validator)
    {
        _customFieldDefinitionService = customFieldDefinitionService;
        _validator = validator;
    }

    [HttpGet]
    [CustomAuthorize("role=bfs.admin")]
    public async Task<List<CustomFieldDefinition>> Get()
    {
        var result = await _customFieldDefinitionService.GetAsync().ConfigureAwait(false);
        return result;
    }

    [HttpGet("{id}")]
    [CustomAuthorize("method=q.customFieldDefinition")]
    public async Task<CustomFieldDefinition?> Get(long id)
    {
        var result = await _customFieldDefinitionService.GetAsync(id).ConfigureAwait(false);
        return result;
    }

    [HttpPost]
    [CustomAuthorize("method=a.customFieldDefinition")]
    public async Task<Results<Ok<CustomFieldDefinition>, BadRequest<ProblemDetails>>> Post([FromBody] CustomFieldDefinition value)
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

        var createdCustomFieldDefinition = await _customFieldDefinitionService.CreateAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(createdCustomFieldDefinition);
    }

    [HttpPut]
    [CustomAuthorize("method=u.customFieldDefinition")]
    public async Task<Results<Ok<CustomFieldDefinition>, BadRequest<ProblemDetails>>> Put([FromBody] CustomFieldDefinition value)
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

        var updatedCustomFieldDefinition = await _customFieldDefinitionService.UpdateAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(updatedCustomFieldDefinition);
    }

    [HttpDelete("{id}")]
    [CustomAuthorize("method=d.customFieldDefinition")]
    public async Task<Results<Ok, BadRequest<ProblemDetails>>> Delete(long id)
    {
        try
        {
            await _customFieldDefinitionService.DeleteAsync(id).ConfigureAwait(false);
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
    [CustomAuthorize("method=q.customFieldDefinition")]
    public async Task<Results<Ok<QueryResponse<CustomFieldDefinitionListItem>>, BadRequest<ProblemDetails>>> List([FromBody] QueryRequest<CustomFieldDefinitionListFilter> listRequest)
    {
        var result = await _customFieldDefinitionService.ListAsync(listRequest).ConfigureAwait(false);
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
            var recordList = await JsonSerializer.DeserializeAsync<List<CustomFieldDefinition>>(stream, options);

            if (recordList == null)
            {
                problemDetails.Title = "Deserialization Failed";
                problemDetails.Detail = "The uploaded file could not be deserialized into a CustomFieldDefinition list.";
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
                await _customFieldDefinitionService.UploadAsync(record).ConfigureAwait(false);
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

