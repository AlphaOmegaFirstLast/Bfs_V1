using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace Bfs.Core.Helpers;

public class ExcelConverter
{
    /// <summary>
    /// Reads an Excel file (.xlsx) and converts its first worksheet data to a JSON string.
    /// </summary>
    /// <param name="filePath">Path to the Excel file.</param>
    /// <returns>A JSON formatted string representing the Excel data.</returns>
    public static string ConvertExcelToJson(string filePath)
    {
        var rowDataList = new List<Dictionary<string, object>>();

        using (var workbook = new XLWorkbook(filePath))
        {
            // Get the first worksheet in the workbook
            var worksheet = workbook.Worksheet(1);
            var rows = worksheet.RowsUsed().ToList();

            if (rows.Count < 2)
            {
                return "[]"; // Return empty JSON array if there's no data below the header
            }

            // Extract headers from the first row
            var headers = rows[0].Cells().Select(c => c.Value.ToString().Trim()).ToList();

            // Iterate through the remaining rows
            for (int i = 1; i < rows.Count; i++)
            {
                var row = rows[i];
                var rowDict = new Dictionary<string, object>();

                for (int j = 0; j < headers.Count; j++)
                {
                    // ClosedXML cell indexing is 1-based
                    var cell = row.Cell(j + 1);

                    // Handle blank cells or get value as object
                    if (cell.IsEmpty())
                    {
                        rowDict[headers[j]] = "Unknown";
                    }
                    else
                    {
                        rowDict[headers[j]] = cell.Value.ToString() ?? "Unknown";
                    }
                }

                rowDataList.Add(rowDict);
            }
        }

        // Serialize the list of dictionaries to a pretty-printed JSON string
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // Keeps Arabic text readable
            PropertyNameCaseInsensitive = true
        };
        
        return JsonSerializer.Serialize(rowDataList, options);
    }

    public static async Task SaveFileAsync(IFormFile excelFile, string filePath) 
    {
        using var stream = excelFile.OpenReadStream();
        await using var ms = new MemoryStream();
        await stream.CopyToAsync(ms).ConfigureAwait(false);
        var bytes = ms.ToArray();
        await File.WriteAllBytesAsync(filePath, bytes).ConfigureAwait(false);
    }

    public static List<recordJson>? ReadJsonAsRecordList(string json) 
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // Keeps Arabic text readable
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        //deserialize json as string to a list of recordJson objects

        var recordList = JsonSerializer.Deserialize<List<recordJson>>(json, options);

        return recordList;
    }

    public static string MapHeadersToFieldNames(string input) 
    {
        input = input.Replace("اسم الشركة", "Name");
        input = input.Replace("آخر سعر", "Price");

        input = input.Replace("القطاع", "Sector");
        input = input.Replace("الإقفال السابق", "PreviousClose");
        input = input.Replace("سعر الفتح", "OpenPrice");
        input = input.Replace("سعر الاغلاق", "ClosePrice");
        input = input.Replace("نسبة التغير%", "ChangePercentage");
        input = input.Replace("اعلى سعر", "HighestPrice");
        input = input.Replace("اقل سعر", "LowestPrice");
        input = input.Replace("القيمة (جنيه)", "Value");
        input = input.Replace("الكمية", "Quantity");
        input = input.Replace("عدد العمليات", "NumberOfTransactions");
        input = input.Replace("رأس المال السوقى (مليون جنيه)", "MarketCapitalization");

        input = input.Replace("\"Unknown\"", "null");
        return input;
    }

    public class recordJson
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

}