using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentValidation.Results;
using Bfs.Core.Contracts;
using Bfs.Core.Middleware;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Domain.Interfaces;

namespace Bfs.StockEx.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class SsPortfolioBalanceController
{
    private readonly ISsPortfolioBalanceService _ssPortfolioBalanceService;
    private IValidator<SsPortfolioBalance> _validator;

    public SsPortfolioBalanceController(ISsPortfolioBalanceService ssPortfolioBalanceService, IValidator<SsPortfolioBalance> validator)
    {
        _ssPortfolioBalanceService = ssPortfolioBalanceService;
        _validator = validator;
    }

    [HttpGet]
    [CustomAuthorize("role=bfs.admin")]
    public async Task<List<SsPortfolioBalance>> Get()
    {
        var result = await _ssPortfolioBalanceService.GetAsync().ConfigureAwait(false);
        return result;
    }

    [HttpGet("{id}")]
    [CustomAuthorize("method=q.ssPortfolioBalance")]
    public async Task<SsPortfolioBalanceListItem?> Get(long id)
    {
        var listRequest = new QueryRequest<SsPortfolioBalanceListFilter>();
        listRequest.Filter.Id = id;
        var response = await _ssPortfolioBalanceService.ListAsync(listRequest).ConfigureAwait(false);
        return response?.Items?.FirstOrDefault();
    }

    [HttpPost]
    [CustomAuthorize("method=a.ssPortfolioBalance")]
    public async Task<Results<Ok<SsPortfolioBalance>, BadRequest<ProblemDetails>>> Post([FromBody] SsPortfolioBalance value)
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

        var createdSsPortfolioBalance = await _ssPortfolioBalanceService.CreateAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(createdSsPortfolioBalance);
    }

    [HttpPut]
    [CustomAuthorize("method=u.ssPortfolioBalance")]
    public async Task<Results<Ok<SsPortfolioBalance>, BadRequest<ProblemDetails>>> Put([FromBody] SsPortfolioBalance value)
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

        var updatedSsPortfolioBalance = await _ssPortfolioBalanceService.UpdateAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(updatedSsPortfolioBalance);
    }

    [HttpDelete("{id}")]
    [CustomAuthorize("method=d.ssPortfolioBalance")]
    public async Task<Results<Ok, BadRequest<ProblemDetails>>> Delete(long id)
    {
        try
        {
            await _ssPortfolioBalanceService.DeleteAsync(id).ConfigureAwait(false);
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
    [CustomAuthorize("method=q.ssPortfolioBalance")]
    public async Task<Results<Ok<QueryResponse<SsPortfolioBalanceListItem>>, BadRequest<ProblemDetails>>> List([FromBody] QueryRequest<SsPortfolioBalanceListFilter> listRequest)
    {
        var result = await _ssPortfolioBalanceService.ListAsync(listRequest).ConfigureAwait(false);
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
            var recordList = await JsonSerializer.DeserializeAsync<List<SsPortfolioBalance>>(stream, options);

            if (recordList == null)
            {
                problemDetails.Title = "Deserialization Failed";
                problemDetails.Detail = "The uploaded file could not be deserialized into a SsPortfolioBalance list.";
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
                await _ssPortfolioBalanceService.UploadAsync(record).ConfigureAwait(false);
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

