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

public class BfsManualCodeController
{
    private readonly IBfsManualCodeService _bfsManualCodeService;
    private IValidator<BfsManualCode> _validator;

    public BfsManualCodeController(IBfsManualCodeService bfsManualCodeService, IValidator<BfsManualCode> validator)
    {
        _bfsManualCodeService = bfsManualCodeService;
        _validator = validator;
    }

    [HttpGet]
    [CustomAuthorize("role=bfs.admin")]
    public async Task<List<BfsManualCode>> Get()
    {
        var result = await _bfsManualCodeService.GetAsync().ConfigureAwait(false);
        return result;
    }

    [HttpGet("{id}")]
    [CustomAuthorize("method=q.bfsManualCode")]
    public async Task<BfsManualCodeListItem?> Get(long id)
    {
        var listRequest = new QueryRequest<BfsManualCodeListFilter>();
        listRequest.Filter.Id = id;
        var response = await _bfsManualCodeService.ListAsync(listRequest).ConfigureAwait(false);
        return response?.Items?.FirstOrDefault();
    }

    [HttpPost]
    [CustomAuthorize("method=a.bfsManualCode")]
    public async Task<Results<Ok<BfsManualCode>, BadRequest<ProblemDetails>>> Post([FromBody] BfsManualCode value)
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

        var createdBfsManualCode = await _bfsManualCodeService.CreateAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(createdBfsManualCode);
    }

    [HttpPut]
    [CustomAuthorize("method=u.bfsManualCode")]
    public async Task<Results<Ok<BfsManualCode>, BadRequest<ProblemDetails>>> Put([FromBody] BfsManualCode value)
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

        var updatedBfsManualCode = await _bfsManualCodeService.UpdateAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(updatedBfsManualCode);
    }

    [HttpDelete("{id}")]
    [CustomAuthorize("method=d.bfsManualCode")]
    public async Task<Results<Ok, BadRequest<ProblemDetails>>> Delete(long id)
    {
        try
        {
            await _bfsManualCodeService.DeleteAsync(id).ConfigureAwait(false);
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
    [CustomAuthorize("method=q.bfsManualCode")]
    public async Task<Results<Ok<QueryResponse<BfsManualCodeListItem>>, BadRequest<ProblemDetails>>> List([FromBody] QueryRequest<BfsManualCodeListFilter> listRequest)
    {
        var result = await _bfsManualCodeService.ListAsync(listRequest).ConfigureAwait(false);
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
            var recordList = await JsonSerializer.DeserializeAsync<List<BfsManualCode>>(stream, options);

            if (recordList == null)
            {
                problemDetails.Title = "Deserialization Failed";
                problemDetails.Detail = "The uploaded file could not be deserialized into a BfsManualCode list.";
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
                await _bfsManualCodeService.UploadAsync(record).ConfigureAwait(false);
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
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1   

}

