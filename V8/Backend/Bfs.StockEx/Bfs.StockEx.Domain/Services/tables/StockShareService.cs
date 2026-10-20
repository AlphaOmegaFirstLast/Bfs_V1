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

namespace Bfs.StockEx.Domain.Services
{
    public class StockShareService : IStockShareService
    {
        private readonly IStockShareRepository _repo;
        private readonly IStockShareList _list;
        //Template_Start_Code_DontOverwrite_2
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentPriceService _currentPriceService;

        //Template_End_Code_DontOverwrite_2

        public StockShareService(
          IStockShareRepository repo
        , IStockShareList list
  //Template_Start_Code_DontOverwrite_3
  , ICurrentPriceService currentPriceService
              , IUnitOfWork unitOfWork
        //Template_End_Code_DontOverwrite_3
        )
        {
            _repo = repo;
            _list = list;
            //Template_Start_Code_DontOverwrite_4
            _currentPriceService = currentPriceService;
            _unitOfWork = unitOfWork;
            //Template_End_Code_DontOverwrite_4
        }

        public async Task<StockShare?> GetAsync(long id)
        {
            var result = await _repo.GetAsync(id).ConfigureAwait(false);
            return result?.ToContract();
        }

        public async Task<List<StockShare>?> GetAsync()
        {
            var result = await _repo.GetAsync().ConfigureAwait(false);
            return result?.ToContract();
        }

        public async Task<StockShare> CreateAsync(StockShare contract)

        {

            var entity = contract.ToEntity();
            var newEntity = await _repo.CreateAsync(entity)
                .ConfigureAwait(false);

            await _repo.SaveAsync()
                .ConfigureAwait(false);

            var result = await GetAsync(newEntity.Id)
                .ConfigureAwait(false);

            return result;
        }

        public async Task<StockShare?> UpdateAsync(StockShare contract)
        {
            //ToDo fluent validation, error or exception

            var existingEntity = await _repo.GetAsync(contract.Id).ConfigureAwait(false);
            var updatedEntity = contract.ToEntity(existingEntity);

            await _repo.UpdateAsync(updatedEntity).ConfigureAwait(false);
            await _repo.SaveAsync().ConfigureAwait(false);

            return updatedEntity?.ToContract();
        }

        public async Task DeleteAsync(long id)
        {
            var existingEntity = await _repo.GetAsync(id).ConfigureAwait(false);

            await _repo.DeleteAsync(existingEntity).ConfigureAwait(false);

            await _repo.SaveAsync().ConfigureAwait(false);

        }

        public async Task<StockShare> UploadAsync(StockShare contract)
        {

            var entity = contract.ToEntity();
            var newEntity = await _repo.UploadAsync(entity).ConfigureAwait(false);

            await _repo.SaveAsync().ConfigureAwait(false);

            var result = await GetAsync(newEntity.Id).ConfigureAwait(false);

            return result;
        }

        public async Task<Bfs.Core.Contracts.QueryResponse<StockShareListItem>> ListAsync(Bfs.Core.Contracts.QueryRequest<StockShareListFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<StockShareListFilter>, Bfs.Core.Data.QueryRequest<Data.StockShareListFilter>>(contractRequest);

            var entityResult = await _list.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.StockShareListItem>, Bfs.Core.Contracts.QueryResponse<StockShareListItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<StockShareListItem> { Items = new List<StockShareListItem>(), TotalItems = 0, TotalPages = 0 };
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

                    record.StockShareId = await GetStockShareByName(record.Name, tradingRoomId, currencyId);

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

        public async Task<long> GetStockShareByName(string stockShareName, long tradingRoomId, long currencyId)
        {
            try
            {
                var stockshareEntity = await _unitOfWork._context.StockShares.FirstOrDefaultAsync(s => s.Name == stockShareName && s.TradingRoomId == tradingRoomId && s.CurrencyId == currencyId).ConfigureAwait(false);
                StockShare newStockShare = null;
                if (stockshareEntity == null)
                {
                    newStockShare = await CreateAsync(new StockShare
                    {
                        Name = stockShareName,
                        Notes = "",
                        TradingRoomId = tradingRoomId,
                        CurrencyId = currencyId,
                    });
                }

                return stockshareEntity?.Id ?? newStockShare?.Id ?? 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<CurrentPrice>? PrepareCurrentPriceList(List<recordJson> recordList)
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

        //Template_Start_Code_DontOverwrite_5
        //Template_End_Code_DontOverwrite_5
    }
}

