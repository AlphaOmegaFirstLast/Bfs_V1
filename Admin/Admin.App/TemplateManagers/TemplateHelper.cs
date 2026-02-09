using Admin.App.Constants;
using Admin.App;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Text;
using System.Text.RegularExpressions;
using Admin.App.CodeWriters;

namespace Admin.App
{
    public class TemplateHelper
    {
        public readonly string Name;
        public readonly PlaceHolderInfo placeHolder;


        public TemplateHelper(PlaceHolderInfo placeHolder, string templateName)
        {
            this.placeHolder = placeHolder;
            Name = templateName;
        }

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   

        public string GetPlaceHolder()
        {
            return $"//Template_{Name}";
        }

        public string GetStartTemplate(int? i)
        {
            return (i.HasValue) ? $"//Template_Start_{Name}_{i.ToString()}" : $"//Template_Start_{Name}";
        }

        public string GetEndTemplate(int? i)
        {
            return (i.HasValue) ? $"//Template_End_{Name}_{i.ToString()}" : $"//Template_End_{Name}";
        }

        public string ReplaceTemplate(string source, string replacement, bool keepTemplate, int? i)
        {
            var output = source;
            switch (placeHolder.PlaceHolderType)
            {
                case PlaceHolderType.StartEnd:
                    output = ReplaceStartEndTemplate(source, replacement, keepTemplate, i);
                    break;
                case PlaceHolderType.PlaceHolder:
                    output = ReplacePlaceHolder(source, replacement, keepTemplate);
                    break;
                case PlaceHolderType.Marker:
                    output = ReplaceMarker(source, replacement, keepTemplate);
                    break;
            }

            return output;
        }


        // Include the template tags in replacement. output has no tags
        public string ReplaceStartEndTemplate(string source, string replacement, bool keepTemplate, int? i)
        {
            var TemplateToKeep = keepTemplate ? GetTemplateIncludingTags(source, i) : string.Empty;
            if (!string.IsNullOrEmpty(TemplateToKeep))
            {
                var temp = new StringBuilder();
                temp.Append(replacement);
                temp.Append(TemplateToKeep);
                replacement = temp.ToString();
            }
            string pattern = $@"{GetStartTemplate(i)}\s*.*?\s*{GetEndTemplate(i)}";
            source = Regex.Replace(source, pattern, replacement, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            return source;
        }

        public string ReplacePlaceHolder(string source, string replacement, bool keepTemplate)
        {
            string pattern = $@"//Template_{Name}";
            var TemplateToKeep = keepTemplate ? pattern : string.Empty;
            if (!string.IsNullOrEmpty(TemplateToKeep))
            {
                var temp = new StringBuilder();
                temp.Append(replacement);
                temp.Append(TemplateToKeep);
                replacement = temp.ToString();
            }
            source = Regex.Replace(source, pattern, replacement, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            return source;
        }

        public string ReplaceMarker(string source, string replacement, bool keepTemplate)
        {
            string template = $@"//Template_{Name}";
            string pattern = $@"//Template_{Name}".Replace(@"[", @"\[").Replace(@"]", @"\]"); //to escape reg ex specific brackets
            var TemplateToKeep = keepTemplate ? template : string.Empty;
            if (!string.IsNullOrEmpty(TemplateToKeep))
            {
                var temp = new StringBuilder();
                temp.Append(replacement);
                temp.Append(TemplateToKeep);
                replacement = temp.ToString();
            }
            source = Regex.Replace(source, pattern, replacement, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            return source;
        }

        // Exclude the template tags from replacement, output has tags and new contents in-between tags.
        // Used to keep code that was added manually. "DontOverwrite"
        public string ReplaceStartEndTemplateContent(string source, string replacement, int? i)
        {
            string start = Regex.Escape(GetStartTemplate(i));
            string end = Regex.Escape(GetEndTemplate(i));

            // Uses Lookbehind for the start tag and Lookahead for the end tag
            // string pattern = $@"(?<={start}\s*).*?(?=\s*{end})";
            string pattern = $@"{start}\s*.*?\s*{end}";
            var sb = new StringBuilder();
            sb.AppendLine(start);
            sb.AppendLine(replacement);
            sb.AppendLine(end);
            source = Regex.Replace(source, pattern, sb.ToString(), RegexOptions.Singleline | RegexOptions.IgnoreCase);
            return source;
        }

        public int GetTemplateCount(string generatedComponent)
        {
            var startTemplate = GetStartTemplate(null);
            var count = Regex.Split(generatedComponent, startTemplate).Length;
            return count;
        }

        public string GetTemplateIncludingTags(string input, int? index)
        {
            // Define the regex pattern to capture text between Start_FieldTemplate and End_Template
            string pattern = $@"{GetStartTemplate(index)}\s*(.*?)\s*{GetEndTemplate(index)}";

            // Use Regex to extract the content
            Match match = Regex.Match(input, pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            return match.Success ? (match.Value.Trim()) : string.Empty;
        }

        public string ExtractEmbededTemplate(string input, int? index)
        {
            // Define the regex pattern to capture text between Start_FieldTemplate and End_Template
            string pattern = $@"{GetStartTemplate(index)}\s*(.*?)\s*{GetEndTemplate(index)}";

            // Use Regex to extract the content
            Match match = Regex.Match(input, pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            return match.Success ? (match.Groups[1].Value).Trim() : string.Empty;
        }

        public string ExtractMarker(string input)
        {
            // Define the regex pattern to capture between brackets
            string pattern = @"//Template_[a-zA-Z]+_\[([a-zA-Z]+)\]";
            // Use Regex to extract the content
            Match match = Regex.Match(input, pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            return match.Success ? $"[{(match.Groups[1].Value).Trim()}]" : string.Empty;
        }

        public string GetTemplateContentType(CodeBase codeInfo, ICodeWriter? writer, string input, int i)
        {
            var templateFilePath = placeHolder.TemplateFile;
            switch (placeHolder.TemplateContentType)
            {
                case TemplateContentType.Embedded:
                    return ExtractEmbededTemplate(input, i);

                case TemplateContentType.ExternalFile:
                    templateFilePath = writer?.ToContent(codeInfo, templateFilePath, placeHolder)?? templateFilePath;
                    return GetTemplateFileContent(codeInfo.TemplateRootDir, templateFilePath);

                case TemplateContentType.WriterProperty:
                    var propertyName = placeHolder.Name.Substring(placeHolder.Name.LastIndexOf("_") + 1);
                    return $@"[{propertyName}]";
            }

            return String.Empty;
        }

        public static string GetTemplateFileContent(string? templateDir,string? templateFile)
        {
            var templateFilePath = GetTemplateFilePath(templateDir, templateFile);
            if (!string.IsNullOrEmpty(templateFilePath))
            {
                if (File.Exists(templateFilePath))
                {
                    return FileHelper.ReadFile(templateFilePath);
                }
            }

            return string.Empty;
        }

        public static string GetTemplateFilePath(string? templateDir, string? templateFile)
        {
            if (!(string.IsNullOrEmpty(templateDir) || string.IsNullOrEmpty(templateFile)))
            {
               return Path.Combine(templateDir, templateFile);
            }

            return string.Empty;
        }
    }
}