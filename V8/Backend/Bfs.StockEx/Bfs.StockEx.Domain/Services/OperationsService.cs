using Bfs.Core.Helpers;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Interfaces;
using Bfs.StockEx.Data.Repositories;
using Bfs.StockEx.Domain.Interfaces;
using Bfs.StockEx.Domain.Mapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Domain.Services;

public class OperationsService : IOperationsService
{
    private readonly IUnitOfWork _unitOfwork;
    //Template_Start_Code_DontOverwrite_2
    private readonly IStockShareService _stockShareService;
    private readonly ICurrentPriceService _currentPriceService;

    //Template_End_Code_DontOverwrite_2
    public OperationsService(IUnitOfWork unitOfwork
//Template_Start_Code_DontOverwrite_3
        , IStockShareService stockShareService
        , ICurrentPriceService currentPriceService
//Template_End_Code_DontOverwrite_3

)
    {

        _unitOfwork = unitOfwork;
        //Template_Start_Code_DontOverwrite_4
        _stockShareService = stockShareService;
        _currentPriceService = currentPriceService;
        //Template_End_Code_DontOverwrite_4
    }

    //Template_Field_ChildrenMatrix_AddServiceEntry    
    //Template_Start_Code_DontOverwrite_5
    public async Task<SspTransaction?> RolloutSspTransactionAsync(SspTransaction contract)
    {
        var entity = contract.ToEntity();
        var newEntity = await _unitOfwork.RolloutSspTransactionAsync(entity)
            .ConfigureAwait(false);
        var result = await _unitOfwork.SspTransactionRepo.GetAsync(newEntity.Id)
            .ConfigureAwait(false);
        return result?.ToContract();
    }

    public async Task<CashTransaction?> RolloutCashTransactionAsync(CashTransaction contract)
    {
        var entity = contract.ToEntity();
        var newEntity = await _unitOfwork.RolloutCashTransactionAsync(entity);
        var result = await _unitOfwork.CashTransactionRepo.GetAsync(newEntity.Id);
        return result?.ToContract();
    }

    public async Task RollbackSspTransactionAsync(long id)
    {
        await _unitOfwork.RollbackSspTransactionAsync(id);
    }

    public async Task RollbackCashTransactionAsync(long id)
    {
        await _unitOfwork.RollbackCashTransactionAsync(id);
    }

    public async Task<SspTransaction?> ChangeSspTransactionAsync(SspTransaction contract)
    {
        var entity = contract.ToEntity();
        var newEntity = await _unitOfwork.ChangeSspTransactionAsync(entity);
        var result = await _unitOfwork.SspTransactionRepo.GetAsync(newEntity.Id);
        return result?.ToContract();
    }

    public async Task<CashTransaction?> ChangeCashTransactionAsync(CashTransaction contract)
    {
        var entity = contract.ToEntity();
        var newEntity = await _unitOfwork.ChangeCashTransactionAsync(entity);
        var result = await _unitOfwork.CashTransactionRepo.GetAsync(newEntity.Id);
        return result?.ToContract();
    }

    public async Task<Results<Ok, BadRequest<ProblemDetails>>> UploadExcel(IFormFile excelFile)
    {
        var problemDetails = new ProblemDetails();

        if (excelFile == null || excelFile.Length == 0)
        {
            problemDetails.Title = "Upload Failed";
            problemDetails.Detail = "No file uploaded.";
            return TypedResults.BadRequest(problemDetails);
        }

        try
        {
            using var stream = excelFile.OpenReadStream();
            await using var ms = new MemoryStream();
            await stream.CopyToAsync(ms).ConfigureAwait(false);
            var bytes = ms.ToArray();
            var filePath = $".\\Temp\\{DateTime.Today.Year}_{DateTime.Today.Month}_{DateTime.Today.Day}_CurrentPrice.xlsx";
            await File.WriteAllBytesAsync(filePath, bytes).ConfigureAwait(false);

            var json = ExcelConverter.ConvertExcelToJson(filePath);

            json = json.Replace("اسم الشركة", "Name");
            json = json.Replace("آخر سعر", "Price");
            json = json.Replace("القطاع", "Sector");
            json = json.Replace("الإقفال السابق", "PreviousClose");
            json = json.Replace("سعر الفتح", "OpenPrice");
            json = json.Replace("سعر الاغلاق", "ClosePrice");
            json = json.Replace("نسبة التغير%", "ChangePercentage");
            json = json.Replace("اعلى سعر", "HighestPrice");
            json = json.Replace("اقل سعر", "LowestPrice");
            json = json.Replace("القيمة (جنيه)", "Value");
            json = json.Replace("الكمية", "Quantity");
            json = json.Replace("عدد العمليات", "NumberOfTransactions");
            json = json.Replace("رأس المال السوقى (مليون جنيه)", "MarketCapitalization");
            json = json.Replace("\"Unknown\"", "null");


            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // Keeps Arabic text readable
                PropertyNameCaseInsensitive = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };

            //deserialize json as string to a list of recordJson objects

            var recordList = JsonSerializer.Deserialize<List<recordJson>>(json, options);

            if (recordList != null)
            {
                decimal? price = 0;
                foreach (var record in recordList)
                {
                    if (record.Name == null)
                    {
                        price = record.Price;
                    }
                    else
                    {
                        record.Price = price;
                    }
                }


                var currentPriceList = recordList?.Where(r => r.Name != null).Select(r => new CurrentPrice
                {
                    Name = r.Name ?? "Unknown",
                    Price = r.Price ?? 0,
                    TransactionDate = DateTime.Today,
                }).ToList();

                foreach (var record in currentPriceList)
                {

                    //ValidationResult validResult = await _validator.ValidateAsync(record);
                    //if (!validResult.IsValid)
                    //{
                    //    problemDetails.Title = "Validation Failed";
                    //    problemDetails.Detail = string.Join("; ", validResult.Errors.Select(e => e.ErrorMessage));
                    //    problemDetails.Extensions = new Dictionary<string, object?>() { { "errors", validResult.Errors.Select(x => new { errorCode = x.ErrorCode, message = x.ErrorMessage }) } };
                    //    return TypedResults.BadRequest(problemDetails);
                    //}

                    var result = await _unitOfwork._context.StockShares.FirstOrDefaultAsync(x => x.Name == record.Name);

                    StockShare? newStockShare = null;
                    if (result == null)
                    {
                        newStockShare = await _stockShareService.CreateAsync(new StockShare
                        {
                            Name = record.Name,
                            Notes = "",
                            TradingRoomId = 10000000001,  // todo
                            CurrencyId = 878237827011761,
                        });
                    }
                    record.StockShareId = result?.Id ?? newStockShare?.Id ?? 0;
                    await _currentPriceService.UploadAsync(record).ConfigureAwait(false);
                }
            }
            return TypedResults.Ok();
        }
        catch (Exception ex)
        {
            problemDetails.Title = "Invalid Excel format";
            problemDetails.Detail = ex.Message;
            return TypedResults.BadRequest(problemDetails);
        }
    }
    //Template_End_Code_DontOverwrite_5

}

