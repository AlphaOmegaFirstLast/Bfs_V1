using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Admin.App
{
    public class FileHelper
    {
        public static bool IsFileExist(string outputFilePath, string content)
        {
            return Directory.Exists(Path.GetDirectoryName(outputFilePath));
        }

        public static string ReadFile(string filePath)
        { 
            if (System.IO.File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }

            return string.Empty;
        }

        public static bool SaveFile(string outputFilePath, string content)
        {
            try 
            {
                var result = RemoveConsecutiveEmptyLinesRegex(content);
                result = RemoveExtraCommas(result);

                if (!Directory.Exists(Path.GetDirectoryName(outputFilePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(outputFilePath));
                    Console.WriteLine("Directory created.");
                }

                File.WriteAllText(outputFilePath, result);
                Console.WriteLine($"File saved to {outputFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating directory: {ex.Message}");
                return false;
            }


            return true;
        }

        public static bool DeleteFile(string outputFilePath)
        {
            try
            {
                if (Directory.Exists(Path.GetDirectoryName(outputFilePath)))
                {
                    File.Delete(outputFilePath);
                    Console.WriteLine($"File deleted {outputFilePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating directory: {ex.Message}");
                return false;
            }

            return true;
        }

        public static List<T> ReadJson<T>(string jsonFilePath)
        {
            List<T> resultList = null;
            if (System.IO.File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var options = new JsonSerializerOptions
                {
                    Converters = { new JsonStringEnumConverter() },
                    PropertyNameCaseInsensitive = true,
                    AllowTrailingCommas = true
                };
                resultList = JsonSerializer.Deserialize<List<T>>(jsonContent, options);
            }

            return resultList;
        }

        public static string RemoveConsecutiveEmptyLinesRegex(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            // Pattern Explanation:
            // (\r\n|\r|\n)      -> Match a newline (Group 1)
            // (                 -> Start Group 2
            //   [ \t]* -> Match 0 or more spaces or tabs (but NOT newlines)
            //   (\r\n|\r|\n)    -> Match another newline
            // )+                -> Repeat Group 2 one or more times

            // We replace the whole sequence with just the first newline ($1) 
            // effectively collapsing the stack of empty lines.

            string pattern = @"(\r\n|\r|\n)(?:[ \t]*(\r\n|\r|\n))+";

            // Note: To preserve exactly ONE empty line between text blocks (rather than removing all gaps), 
            // we replace with two newlines if we want to normalize to standard paragraph spacing, 
            // but strictly based on "replace 2 empty lines with 1", the below standardizes the gap:

            var output = input; //Todo loop while 2 successive similar outputs
            for (var i = 1; i < 3; i++)
                output = Regex.Replace(output, pattern, "$1$2");
            return output;
        }

        public static string RemoveExtraCommas(string input)
        {
            // Pattern: Capture the ( and spaces into $1, match the comma, capture the letter into $2
            string pattern = @"(\(\s*),([a-zA-Z])";

            // Replace with Group 1 ($1) and Group 2 ($2)
            string result = Regex.Replace(input, pattern, "$1$2");
            return result;
        }

        internal static void CopyDirectory(string sourceDir, string destinationDir)
        {
            // Modify code to copy all files from sourceDir to destinationDir With preserving subdirectories
            if (Directory.Exists(sourceDir))
            {
                Directory.CreateDirectory(destinationDir);
                foreach (var dir in Directory.GetDirectories(sourceDir, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dir.Replace(sourceDir, destinationDir));
                }
                foreach (var file in Directory.GetFiles(sourceDir, "*.*", SearchOption.AllDirectories))
                {
                    var destFile = file.Replace(sourceDir, destinationDir);
                    File.Copy(file, destFile, true);
                }
            }
        }
    }
}
