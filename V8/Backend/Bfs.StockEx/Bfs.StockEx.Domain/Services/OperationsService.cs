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
using static Bfs.Core.Helpers.ExcelConverter;
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

    public async Task ProcessCoupon(DateTime processDate)
    {
        await _unitOfwork.ProcessCoupon(processDate);
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
            var filePath = $".\\Temp\\{DateTime.Today.Year}_{DateTime.Today.Month}_{DateTime.Today.Day}_CurrentPrice.xlsx";
            await ExcelConverter.SaveFileAsync(excelFile, filePath).ConfigureAwait(false);

            var json = ExcelConverter.ConvertExcelToJson(filePath);
            json = ExcelConverter.MapHeadersToFieldNames(json);

            var recordList = ExcelConverter.ReadJsonAsRecordList(json);
            var currentPriceList = PrepareCurrentPriceList(recordList);

            foreach (var record in currentPriceList)
            {
                var tradingRoomId = 10000000001;  // todo
                var currencyId = 816930258453823;  // todo

                record.StockShareId = await _stockShareService.GetStockShareByName(record.Name, tradingRoomId, currencyId);

                if (record.StockShareId != 0)
                {
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

    private static List<CurrentPrice>? PrepareCurrentPriceList(List<recordJson> recordList)
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
        return currentPriceList;
    }

    //Template_End_Code_DontOverwrite_5
}

