using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
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
}