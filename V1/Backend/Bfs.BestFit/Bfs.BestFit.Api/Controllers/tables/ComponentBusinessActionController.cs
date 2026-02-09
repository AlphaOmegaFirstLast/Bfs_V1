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

public class ComponentBusinessActionController
{
    private readonly IComponentBusinessActionService _componentBusinessActionService;
    private IValidator<ComponentBusinessAction> _validator;

    public ComponentBusinessActionController(IComponentBusinessActionService componentBusinessActionService, IValidator<ComponentBusinessAction> validator)
    {
        _componentBusinessActionService = componentBusinessActionService;
        _validator = validator;
    }

    [HttpGet]
    [CustomAuthorize("role=bfs.admin")]
    public async Task<List<ComponentBusinessAction>> Get()
    {
        var result = await _componentBusinessActionService.GetAsync().ConfigureAwait(false);
        return result;
    }

    [HttpGet("{id}")]
    [CustomAuthorize("method=q.componentBusinessAction")]
    public async Task<ComponentBusinessAction?> Get(long id)
    {
        var result = await _componentBusinessActionService.GetAsync(id).ConfigureAwait(false);
        return result;
    }

    [HttpPost]
    [CustomAuthorize("method=a.componentBusinessAction")]
    public async Task<Results<Ok<ComponentBusinessAction>, BadRequest<ProblemDetails>>> Post([FromBody] ComponentBusinessAction value)
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

        var createdComponentBusinessAction = await _componentBusinessActionService.CreateAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(createdComponentBusinessAction);
    }

    [HttpPut]
    [CustomAuthorize("method=u.componentBusinessAction")]
    public async Task<Results<Ok<ComponentBusinessAction>, BadRequest<ProblemDetails>>> Put([FromBody] ComponentBusinessAction value)
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

        var updatedComponentBusinessAction = await _componentBusinessActionService.UpdateAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(updatedComponentBusinessAction);
    }

    [HttpDelete("{id}")]
    [CustomAuthorize("method=d.componentBusinessAction")]
    public async Task<Results<Ok, BadRequest<ProblemDetails>>> Delete(long id)
    {
        try
        {
            await _componentBusinessActionService.DeleteAsync(id).ConfigureAwait(false);
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
    [CustomAuthorize("method=q.componentBusinessAction")]
    public async Task<Results<Ok<QueryResponse<ComponentBusinessActionListItem>>, BadRequest<ProblemDetails>>> List([FromBody] QueryRequest<ComponentBusinessActionListFilter> listRequest)
    {
        var result = await _componentBusinessActionService.ListAsync(listRequest).ConfigureAwait(false);
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
            var recordList = await JsonSerializer.DeserializeAsync<List<ComponentBusinessAction>>(stream, options);

            if (recordList == null)
            {
                problemDetails.Title = "Deserialization Failed";
                problemDetails.Detail = "The uploaded file could not be deserialized into a ComponentBusinessAction list.";
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
                await _componentBusinessActionService.UploadAsync(record).ConfigureAwait(false);
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

