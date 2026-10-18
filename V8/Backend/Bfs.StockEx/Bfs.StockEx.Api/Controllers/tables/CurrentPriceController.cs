using Bfs.Core.Contracts;
using Bfs.Core.Middleware;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Lists;
using Bfs.StockEx.Domain.Interfaces;
using Bfs.StockEx.Domain.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class CurrentPriceController
{
    private readonly ICurrentPriceService _currentPriceService;
    private IValidator<CurrentPrice> _validator;
    //Template_Start_Code_DontOverwrite_2
    private readonly IStockShareService _stockShareService;
    //Template_End_Code_DontOverwrite_2

    public CurrentPriceController(ICurrentPriceService currentPriceService, IValidator<CurrentPrice> validator
//Template_Start_Code_DontOverwrite_3
, IStockShareService stockShareService
//Template_End_Code_DontOverwrite_3

    )
    {
        _currentPriceService = currentPriceService;
        _validator = validator;
        //Template_Start_Code_DontOverwrite_4
        _stockShareService = stockShareService;
        //Template_End_Code_DontOverwrite_4

    }

    [HttpGet]
    [CustomAuthorize("role=bfs.admin")]
    public async Task<List<CurrentPrice>> Get()
    {
        var result = await _currentPriceService.GetAsync().ConfigureAwait(false);
        return result;
    }

    [HttpGet("{id}")]
    [CustomAuthorize("method=q.currentPrice")]
    public async Task<CurrentPriceListItem?> Get(long id)
    {
        var listRequest = new QueryRequest<CurrentPriceListFilter>();
        listRequest.Filter.Id = id;
        var response = await _currentPriceService.ListAsync(listRequest).ConfigureAwait(false);
        return response?.Items?.FirstOrDefault();
    }

    [HttpPost]
    [CustomAuthorize("method=a.currentPrice")]
    public async Task<Results<Ok<CurrentPrice>, BadRequest<ProblemDetails>>> Post([FromBody] CurrentPrice value)
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

        var createdCurrentPrice = await _currentPriceService.CreateAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(createdCurrentPrice);
    }

    [HttpPut]
    [CustomAuthorize("method=u.currentPrice")]
    public async Task<Results<Ok<CurrentPrice>, BadRequest<ProblemDetails>>> Put([FromBody] CurrentPrice value)
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

        var updatedCurrentPrice = await _currentPriceService.UpdateAsync(value).ConfigureAwait(false);
        return TypedResults.Ok(updatedCurrentPrice);
    }

    [HttpDelete("{id}")]
    [CustomAuthorize("method=d.currentPrice")]
    public async Task<Results<Ok, BadRequest<ProblemDetails>>> Delete(long id)
    {
        try
        {
            await _currentPriceService.DeleteAsync(id).ConfigureAwait(false);
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
    [CustomAuthorize("method=q.currentPrice")]
    public async Task<Results<Ok<QueryResponse<CurrentPriceListItem>>, BadRequest<ProblemDetails>>> List([FromBody] QueryRequest<CurrentPriceListFilter> listRequest)
    {
        var result = await _currentPriceService.ListAsync(listRequest).ConfigureAwait(false);
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
            var recordList = await JsonSerializer.DeserializeAsync<List<CurrentPrice>>(stream, options);

            if (recordList == null)
            {
                problemDetails.Title = "Deserialization Failed";
                problemDetails.Detail = "The uploaded file could not be deserialized into a CurrentPrice list.";
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
                await _currentPriceService.UploadAsync(record).ConfigureAwait(false);
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

    //Template_Start_Code_DontOverwrite_5
    [HttpPost("uploadExcel")]
    [CustomAuthorize("role=bfs.admin")]
    public async Task<Results<Ok, BadRequest<ProblemDetails>>> UploadExcel(IFormFile file)
    {
        return await _stockShareService.UploadExcel(file);
        //var problemDetails = new ProblemDetails();

        //if (file == null || file.Length == 0)
        //{
        //    problemDetails.Title = "Upload Failed";
        //    problemDetails.Detail = "No file uploaded.";
        //    return TypedResults.BadRequest(problemDetails);
        //}

        //try
        //{
        //    using var stream = file.OpenReadStream();
        //    await using var ms = new MemoryStream();
        //    await stream.CopyToAsync(ms).ConfigureAwait(false);
        //    var bytes = ms.ToArray();
        //    var filePath = $".\\Temp\\{DateTime.Today.Year}_{DateTime.Today.Month}_{DateTime.Today.Day}_CurrentPrice.xlsx";
        //    await File.WriteAllBytesAsync(filePath, bytes).ConfigureAwait(false);
        //    /*

        //       {
        //        "Name": "Unknown",
        //        "القطاع": "عقارات",
        //        "الإقفال السابق": "6350",
        //        "سعر الفتح": "6350",
        //        "سعر الاغلاق": "6450",
        //        "نسبة التغير%": "1.57",
        //        "Price": "6450",
        //        "اعلى سعر": "6540",
        //        "اقل سعر": "6250",
        //        "القيمة (جنيه)": "24,112,362.95",
        //        "الكمية": "3,759,613",
        //        "عدد العمليات": "976",
        //        "رأس المال السوقى (مليون جنيه)": "6.754"
        //      },
        //     */
        //    var json = Bfs.Core.Helpers.ExcelConverter.ConvertExcelToJson(filePath);

        //    json = json.Replace("اسم الشركة", "Name");
        //    json = json.Replace("آخر سعر", "Price");
        //    json = json.Replace("القطاع", "Sector");
        //    json = json.Replace("الإقفال السابق", "PreviousClose");
        //    json = json.Replace("سعر الفتح", "OpenPrice");
        //    json = json.Replace("سعر الاغلاق", "ClosePrice");
        //    json = json.Replace("نسبة التغير%", "ChangePercentage");
        //    json = json.Replace("اعلى سعر", "HighestPrice");
        //    json = json.Replace("اقل سعر", "LowestPrice");
        //    json = json.Replace("القيمة (جنيه)", "Value");
        //    json = json.Replace("الكمية", "Quantity");
        //    json = json.Replace("عدد العمليات", "NumberOfTransactions");
        //    json = json.Replace("رأس المال السوقى (مليون جنيه)", "MarketCapitalization");


        //    var options = new JsonSerializerOptions
        //    {
        //        WriteIndented = true,
        //        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // Keeps Arabic text readable
        //        PropertyNameCaseInsensitive = true,
        //        NumberHandling = JsonNumberHandling.AllowReadingFromString
        //    };

        //    //deserialize json as string to a list of recordJson objects
        //    json = json.Replace("\"Unknown\"", "null");

        //    var recordList = JsonSerializer.Deserialize<List<recordJson>>(json, options);

        //    if (recordList == null)
        //    {
        //        decimal? price = 0;
        //        foreach (var record in recordList)
        //        {
        //            if (record.Name == null)
        //            {
        //                price = record.Price;
        //            }
        //            else
        //            {
        //                record.Price = price;
        //            }
        //        }

        //        var currentPriceList = recordList?.Where(r => r.Name != null).Select(r => new CurrentPrice
        //        {
        //            Name = r.Name ?? "Unknown",
        //            Price = r.Price ?? 0,
        //            TransactionDate = DateTime.Today,
        //        }).ToList();

        //        foreach (var record in currentPriceList)
        //        {
        //            ValidationResult validResult = await _validator.ValidateAsync(record);
        //            if (!validResult.IsValid)
        //            {
        //                problemDetails.Title = "Validation Failed";
        //                problemDetails.Detail = string.Join("; ", validResult.Errors.Select(e => e.ErrorMessage));
        //                problemDetails.Extensions = new Dictionary<string, object?>() { { "errors", validResult.Errors.Select(x => new { errorCode = x.ErrorCode, message = x.ErrorMessage }) } };
        //                return TypedResults.BadRequest(problemDetails);
        //            }

        //            await _currentPriceService.UploadAsync(record).ConfigureAwait(false);
        //        }
        //    }
        //    return TypedResults.Ok();
        //}
        //catch (Exception ex)
        //{
        //    problemDetails.Title = "Invalid Excel format";
        //    problemDetails.Detail = ex.Message;
        //    return TypedResults.BadRequest(problemDetails);
        //}
    }
    private class recordJson
    {
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Sector { get; set; }
        public decimal? PreviousClose { get; set; }
        public decimal? OpenPrice { get; set; }
        public decimal? ClosePrice { get; set; }
        public string? ChangePercentage { get; set; }
        public string? HighestPrice { get; set; }
        public string? LowestPrice { get; set; }
        public string? Value { get; set; }
        public string? Quantity { get; set; }
        public string? NumberOfTransactions { get; set; }
        public string? MarketCapitalization { get; set; }
    }

    //Template_End_Code_DontOverwrite_5
}

