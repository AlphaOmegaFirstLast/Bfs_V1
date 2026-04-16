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

public class BfsTenantSystemController
{
    private readonly IBfsTenantSystemService _bfsTenantSystemService;
    private IValidator<BfsTenantSystem> _validator;

    public BfsTenantSystemController(IBfsTenantSystemService bfsTenantSystemService, IValidator<BfsTenantSystem> validator)
    {
        _bfsTenantSystemService = bfsTenantSystemService;
        _validator = validator;
    }

    [HttpGet]
    [CustomAuthorize("role=bfs.admin")]
    public async Task<List<BfsTenantSystem>> Get()
    {
        var result = await _bfsTenantSystemService.GetAsync().ConfigureAwait(false);
        return result;
    }

    [HttpGet("{id}")]
    [CustomAuthorize("method=q.bfsTenantSystem")]
    public async Task<BfsTenantSystem?> Get(long id)
    {
        var result = await _bfsTenantSystemService.GetAsync(id).ConfigureAwait(false);
        return result;
    }

    [HttpPost]
    [CustomAuthorize("method=a.bfsTenantSystem")]
    public async Task<Results<Ok<BfsTenantSystem>, BadRequest<ProblemDetails>>> Post([FromBody] BfsTenantSystem value)
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

        var createdBfsTenantSystem = await _bfsTenantSystemService.CreateAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(createdBfsTenantSystem);
    }

    [HttpPut]
    [CustomAuthorize("method=u.bfsTenantSystem")]
    public async Task<Results<Ok<BfsTenantSystem>, BadRequest<ProblemDetails>>> Put([FromBody] BfsTenantSystem value)
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

        var updatedBfsTenantSystem = await _bfsTenantSystemService.UpdateAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(updatedBfsTenantSystem);
    }

    [HttpDelete("{id}")]
    [CustomAuthorize("method=d.bfsTenantSystem")]
    public async Task<Results<Ok, BadRequest<ProblemDetails>>> Delete(long id)
    {
        try
        {
            await _bfsTenantSystemService.DeleteAsync(id).ConfigureAwait(false);
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
    [CustomAuthorize("method=q.bfsTenantSystem")]
    public async Task<Results<Ok<QueryResponse<BfsTenantSystemListItem>>, BadRequest<ProblemDetails>>> List([FromBody] QueryRequest<BfsTenantSystemListFilter> listRequest)
    {
        var result = await _bfsTenantSystemService.ListAsync(listRequest).ConfigureAwait(false);
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
            var recordList = await JsonSerializer.DeserializeAsync<List<BfsTenantSystem>>(stream, options);

            if (recordList == null)
            {
                problemDetails.Title = "Deserialization Failed";
                problemDetails.Detail = "The uploaded file could not be deserialized into a BfsTenantSystem list.";
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
                await _bfsTenantSystemService.UploadAsync(record).ConfigureAwait(false);
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

