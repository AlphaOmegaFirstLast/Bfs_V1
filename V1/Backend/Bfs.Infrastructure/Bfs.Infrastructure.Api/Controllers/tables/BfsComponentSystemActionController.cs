using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentValidation.Results;
using Bfs.Core.Contracts;
using Bfs.Core.Middleware;
using Bfs.Infrastructure.Contracts;
using Bfs.Infrastructure.Domain.Interfaces;

namespace Bfs.Infrastructure.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class BfsComponentSystemActionController
{
    private readonly IBfsComponentSystemActionService _bfsComponentSystemActionService;
    private IValidator<BfsComponentSystemAction> _validator;

    public BfsComponentSystemActionController(IBfsComponentSystemActionService bfsComponentSystemActionService, IValidator<BfsComponentSystemAction> validator)
    {
        _bfsComponentSystemActionService = bfsComponentSystemActionService;
        _validator = validator;
    }

    [HttpGet]
    [CustomAuthorize("role=bfs.admin")]
    public async Task<List<BfsComponentSystemAction>> Get()
    {
        var result = await _bfsComponentSystemActionService.GetAsync().ConfigureAwait(false);
        return result;
    }

    [HttpGet("{id}")]
    [CustomAuthorize("method=q.bfsComponentSystemAction")]
    public async Task<BfsComponentSystemAction?> Get(long id)
    {
        var result = await _bfsComponentSystemActionService.GetAsync(id).ConfigureAwait(false);
        return result;
    }

    [HttpPost]
    [CustomAuthorize("method=a.bfsComponentSystemAction")]
    public async Task<Results<Ok<BfsComponentSystemAction>, BadRequest<ProblemDetails>>> Post([FromBody] BfsComponentSystemAction value)
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

        var createdBfsComponentSystemAction = await _bfsComponentSystemActionService.CreateAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(createdBfsComponentSystemAction);
    }

    [HttpPut]
    [CustomAuthorize("method=u.bfsComponentSystemAction")]
    public async Task<Results<Ok<BfsComponentSystemAction>, BadRequest<ProblemDetails>>> Put([FromBody] BfsComponentSystemAction value)
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

        var updatedBfsComponentSystemAction = await _bfsComponentSystemActionService.UpdateAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(updatedBfsComponentSystemAction);
    }

    [HttpDelete("{id}")]
    [CustomAuthorize("method=d.bfsComponentSystemAction")]
    public async Task<Results<Ok, BadRequest<ProblemDetails>>> Delete(long id)
    {
        try
        {
            await _bfsComponentSystemActionService.DeleteAsync(id).ConfigureAwait(false);
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
    [CustomAuthorize("method=q.bfsComponentSystemAction")]
    public async Task<Results<Ok<QueryResponse<BfsComponentSystemActionListItem>>, BadRequest<ProblemDetails>>> List([FromBody] QueryRequest<BfsComponentSystemActionListFilter> listRequest)
    {
        var result = await _bfsComponentSystemActionService.ListAsync(listRequest).ConfigureAwait(false);
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
            var recordList = await JsonSerializer.DeserializeAsync<List<BfsComponentSystemAction>>(stream, options);

            if (recordList == null)
            {
                problemDetails.Title = "Deserialization Failed";
                problemDetails.Detail = "The uploaded file could not be deserialized into a BfsComponentSystemAction list.";
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
                await _bfsComponentSystemActionService.UploadAsync(record).ConfigureAwait(false);
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

