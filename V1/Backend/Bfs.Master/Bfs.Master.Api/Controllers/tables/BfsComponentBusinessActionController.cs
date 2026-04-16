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

public class BfsComponentBusinessActionController
{
    private readonly IBfsComponentBusinessActionService _bfsComponentBusinessActionService;
    private IValidator<BfsComponentBusinessAction> _validator;

    public BfsComponentBusinessActionController(IBfsComponentBusinessActionService bfsComponentBusinessActionService, IValidator<BfsComponentBusinessAction> validator)
    {
        _bfsComponentBusinessActionService = bfsComponentBusinessActionService;
        _validator = validator;
    }

    [HttpGet]
    [CustomAuthorize("role=bfs.admin")]
    public async Task<List<BfsComponentBusinessAction>> Get()
    {
        var result = await _bfsComponentBusinessActionService.GetAsync().ConfigureAwait(false);
        return result;
    }

    [HttpGet("{id}")]
    [CustomAuthorize("method=q.bfsComponentBusinessAction")]
    public async Task<BfsComponentBusinessAction?> Get(long id)
    {
        var result = await _bfsComponentBusinessActionService.GetAsync(id).ConfigureAwait(false);
        return result;
    }

    [HttpPost]
    [CustomAuthorize("method=a.bfsComponentBusinessAction")]
    public async Task<Results<Ok<BfsComponentBusinessAction>, BadRequest<ProblemDetails>>> Post([FromBody] BfsComponentBusinessAction value)
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

        var createdBfsComponentBusinessAction = await _bfsComponentBusinessActionService.CreateAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(createdBfsComponentBusinessAction);
    }

    [HttpPut]
    [CustomAuthorize("method=u.bfsComponentBusinessAction")]
    public async Task<Results<Ok<BfsComponentBusinessAction>, BadRequest<ProblemDetails>>> Put([FromBody] BfsComponentBusinessAction value)
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

        var updatedBfsComponentBusinessAction = await _bfsComponentBusinessActionService.UpdateAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(updatedBfsComponentBusinessAction);
    }

    [HttpDelete("{id}")]
    [CustomAuthorize("method=d.bfsComponentBusinessAction")]
    public async Task<Results<Ok, BadRequest<ProblemDetails>>> Delete(long id)
    {
        try
        {
            await _bfsComponentBusinessActionService.DeleteAsync(id).ConfigureAwait(false);
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
    [CustomAuthorize("method=q.bfsComponentBusinessAction")]
    public async Task<Results<Ok<QueryResponse<BfsComponentBusinessActionListItem>>, BadRequest<ProblemDetails>>> List([FromBody] QueryRequest<BfsComponentBusinessActionListFilter> listRequest)
    {
        var result = await _bfsComponentBusinessActionService.ListAsync(listRequest).ConfigureAwait(false);
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
            var recordList = await JsonSerializer.DeserializeAsync<List<BfsComponentBusinessAction>>(stream, options);

            if (recordList == null)
            {
                problemDetails.Title = "Deserialization Failed";
                problemDetails.Detail = "The uploaded file could not be deserialized into a BfsComponentBusinessAction list.";
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
                await _bfsComponentBusinessActionService.UploadAsync(record).ConfigureAwait(false);
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

