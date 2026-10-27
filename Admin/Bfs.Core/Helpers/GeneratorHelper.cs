using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Bfs.Core.Helpers
{
    public class GeneratorHelper
    {

        /// <summary>
        /// Function 1: Reads and returns a dictionary mapping each QueryName to its current DbName.
        /// </summary>
        public static Dictionary<string, string> ExtractDbNamesByQueryName(string input)
        {
            var mapping = new Dictionary<string, string>();
            string pattern = @"DbName\s*=\s*""([^""]*)"".*?QueryName\s*=\s*""([^""]*)""";

            MatchCollection matches = Regex.Matches(input, pattern, RegexOptions.Singleline);
            foreach (Match match in matches)
            {
                string dbName = match.Groups[1].Value;
                string queryName = match.Groups[2].Value;

                if (!string.IsNullOrEmpty(queryName))
                {
                    mapping[queryName] = dbName;
                }
            }

            return mapping;
        }

        /// <summary>
        /// Function 2: Replaces the DbName for a specific QueryName with a new value.
        /// </summary>
        public static string ReplaceDbName(string input, string targetQueryName, string newDbName)
        {
            // Pattern matches the line containing the specific QueryName and captures the DbName part
            string pattern = $@"(@?DbName\s*=\s*""[^""]*"")(.*?QueryName\s*=\s*""{Regex.Escape(targetQueryName)}"")";

            return Regex.Replace(input, pattern, match =>
            {
                string middleText = match.Groups[2].Value;
                return $"DbName = \"{newDbName}\"{middleText}";
            }, RegexOptions.Singleline);
        }
    }
}

    //static void Main()
    //{
    //    string input = @"
    //        _fieldList.Add(new QueryField() { DbName = ""stkxSsPortfolio.Name"", QueryName = ""SsPortfolioName"", IsAggregare = false });
    //        _fieldList.Add(new QueryField() { DbName = ""stkxStockShare.Name"", QueryName = ""StockShareName"", IsAggregare = false });
    //        _fieldList.Add(new QueryField() { DbName = ""stkxCurrency.Name"", QueryName = ""CurrencyName"", IsAggregare = false });
    //    ";

    //    // 1. Read / Extract existing mappings (QueryName -> Old DbName)
    //    Dictionary<string, string> dbNameMappings = ExtractDbNamesByQueryName(input);

    //    // Example: Print what was extracted
    //    Console.WriteLine("--- Extracted Mappings ---");
    //    foreach (var kvp in dbNameMappings)
    //    {
    //        Console.WriteLine($"QueryName: {kvp.Key} => DbName: {kvp.Value}");
    //    }

    //    // Define your new values to replace with, mapped by QueryName
    //    var newValues = new Dictionary<string, string>
    //    {
    //        { "SsPortfolioName", "newTable.PortfolioName" },
    //        { "StockShareName", "newTable.ShareName" },
    //        { "CurrencyName", "newTable.CurrName" }
    //    };

    //    // 2. Replace the old DbName with the new DbName based on QueryName
    //    string updatedText = input;
    //    foreach (var pair in newValues)
    //    {
    //        updatedText = ReplaceDbName(updatedText, pair.Key, pair.Value);
    //    }


