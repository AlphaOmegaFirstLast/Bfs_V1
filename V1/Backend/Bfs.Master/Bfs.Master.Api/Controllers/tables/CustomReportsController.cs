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

public class CustomReportsController
{
    private readonly ICustomReportsService _customReportsService;
    private IValidator<CustomReports> _validator;

    public CustomReportsController(ICustomReportsService customReportsService, IValidator<CustomReports> validator)
    {
        _customReportsService = customReportsService;
        _validator = validator;
    }

    [HttpGet]
    [CustomAuthorize("role=bfs.admin")]
    public async Task<List<CustomReports>> Get()
    {
        var result = await _customReportsService.GetAsync().ConfigureAwait(false);
        return result;
    }

    [HttpGet("{id}")]
    [CustomAuthorize("method=q.customReports")]
    public async Task<CustomReports?> Get(long id)
    {
        var result = await _customReportsService.GetAsync(id).ConfigureAwait(false);
        return result;
    }

    [HttpPost]
    [CustomAuthorize("method=a.customReports")]
    public async Task<Results<Ok<CustomReports>, BadRequest<ProblemDetails>>> Post([FromBody] CustomReports value)
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

        var createdCustomReports = await _customReportsService.CreateAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(createdCustomReports);
    }

    [HttpPut]
    [CustomAuthorize("method=u.customReports")]
    public async Task<Results<Ok<CustomReports>, BadRequest<ProblemDetails>>> Put([FromBody] CustomReports value)
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

        var updatedCustomReports = await _customReportsService.UpdateAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(updatedCustomReports);
    }

    [HttpDelete("{id}")]
    [CustomAuthorize("method=d.customReports")]
    public async Task<Results<Ok, BadRequest<ProblemDetails>>> Delete(long id)
    {
        try
        {
            await _customReportsService.DeleteAsync(id).ConfigureAwait(false);
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
    [CustomAuthorize("method=q.customReports")]
    public async Task<Results<Ok<QueryResponse<CustomReportsListItem>>, BadRequest<ProblemDetails>>> List([FromBody] QueryRequest<CustomReportsListFilter> listRequest)
    {
        var result = await _customReportsService.ListAsync(listRequest).ConfigureAwait(false);
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
            var recordList = await JsonSerializer.DeserializeAsync<List<CustomReports>>(stream, options);

            if (recordList == null)
            {
                problemDetails.Title = "Deserialization Failed";
                problemDetails.Detail = "The uploaded file could not be deserialized into a CustomReports list.";
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
                await _customReportsService.UploadAsync(record).ConfigureAwait(false);
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

