using Admin.App.Constants;
using Admin.App;
using Admin.App;
using System.Diagnostics;
using Microsoft.AspNetCore.Routing.Template;
using OpenTelemetry.Resources;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Admin.App.CodeWriters;

namespace Admin.App
{
    public class TemplateManager
    {
        public static void InitFrameWork(CodeGeneratorBase codeInfo, TemplateInfo generatorTemplate)
        {
            var templateFile = generatorTemplate.TemplateFile.Replace("*.*", "");
            var sourceDir = TemplateHelper.GetTemplateFilePath(codeInfo.TemplateRootDir, templateFile);
            var destinationDir = generatorTemplate.GetOutputFilePath(codeInfo);
            FileHelper.CopyDirectory(sourceDir, destinationDir);
        }

        public static string Generate(CodeGeneratorBase codeInfo, TemplateInfo generatorTemplate)
        {
            // Get the template content of a component. replace System, Component, and Field-related terms in the template.
            var input = TemplateHelper.GetTemplateFileContent(codeInfo.TemplateRootDir, generatorTemplate.TemplateFile);

            ApplyTemplates(codeInfo, generatorTemplate, input); //CodeChangeLogList track all the changes that were applied to generatedCode 
            var generatedCode = codeInfo.CodeTracker.GetGeneratedCode();

            // Remove templates after processing, if it has its flag set to not keep placeholders. Just need to loop through the Marks (i.e [FieldDefinition], [ReportDefinition], ..)
            generatedCode = ClearPlaceHolders(codeInfo, generatedCode);
            generatedCode = HandleSpecialCases(generatedCode);

            if (generatorTemplate.SaveGeneratedCode == SaveGeneratedCode.PerAllWriters)
            {
                return TemplateManager.SaveFile(codeInfo, generatorTemplate, generatedCode, false); // dont save file if generatedCode is empty
            }
            else if (generatorTemplate.SaveGeneratedCode == SaveGeneratedCode.PerWriter)
            {
                var codeChangeList = codeInfo.CodeTracker.CodeChangeList;
                foreach (var changeEntry in codeChangeList)
                {
                    foreach (var snippetEntry in changeEntry.SnippetList)
                    {

                        var outputFilePath = generatorTemplate.GetOutputFilePath(codeInfo);
                        var writerOutputFilePath = snippetEntry.Writer.ToContent(codeInfo, outputFilePath, snippetEntry.PlaceHolderInfo);
                        FileHelper.SaveFile(writerOutputFilePath, snippetEntry.Snippet);
                    }
                }
            }
            return string.Empty;
        }

        public static string Modify(CodeGeneratorBase codeInfo, TemplateInfo modifierTemplate)
        {
            var fileToModifyFilePath = modifierTemplate.GetOutputFilePath(codeInfo);
            var input = FileHelper.ReadFile(fileToModifyFilePath);

            ApplyTemplates(codeInfo, modifierTemplate, input); //codeInfo.CodeTracker track all the changes that were applied to generatedCode 
            var generatedCode = codeInfo.CodeTracker.GetGeneratedCode();

            // Remove templates after processing, if it has its flag set to not keep placeholders. Just need to loop through the Marks (i.e [FieldDefinition], [ReportDefinition], ..)
            generatedCode = ClearPlaceHolders(codeInfo, generatedCode);
            generatedCode = HandleSpecialCases(generatedCode);

            var snippetList = codeInfo.CodeTracker.GetSnippetList();
            return TemplateManager.SaveFile(codeInfo, modifierTemplate, generatedCode, true); // dont save file if generatedCode is empty
        }

        public static void RollBackModify(CodeGeneratorBase codeInfo, TemplateInfo modifierTemplate)
        {
            var fileToModifyFilePath = modifierTemplate.GetOutputFilePath(codeInfo);
            var input = FileHelper.ReadFile(fileToModifyFilePath);

            ApplyTemplates(codeInfo, modifierTemplate, input); //codeInfo.CodeTracker track all the changes that were applied to generatedCode 
            var generatedCode = codeInfo.CodeTracker.GetGeneratedCode(); // at this point, generatedCode has Not Any modifications applied (because they already exist). In theory, generatedCode = inpu

            // Remove templates after processing, if it has its flag set to not keep placeholders. Just need to loop through the Marks (i.e [FieldDefinition], [ReportDefinition], ..)
            generatedCode = HandleSpecialCases(generatedCode);

            // After replay the "Modify Logic", track all changes and replace them from the generated code
            var snippetList = codeInfo.CodeTracker.GetSnippetList();
            foreach (var snippet in snippetList)
            {
                generatedCode = generatedCode.Replace(snippet, "");
            }

            TemplateManager.SaveFile(codeInfo, modifierTemplate, generatedCode, true); // dont save file if generatedCode is empty
        }

        public static void ApplyTemplates(CodeGeneratorBase codeInfo, TemplateInfo templateInfo, string? generatedCode = "")
        {
            // Track code-changes
            codeInfo.CodeTracker = new CodeTracker();
            codeInfo.CodeTracker.Start(generatedCode);

            // the Generator templates allow duplicate lines (e.g a component may have field initialization showing more than once)
            var allowDuplicateLines = templateInfo.TemplateType == TemplateType.Generator;

            //Filter PlaceHolderLists to return only PlaceHolders that match the current template being processed.
            var placeHolderList = codeInfo.GetPlaceHolderListOfTemplate(templateInfo);

            // Loop through filtered place holders. 
            foreach (var placeHolder in placeHolderList)
            {
                var writerList = codeInfo.CodeWriter.GetWriterList(placeHolder.WriterType);

                // Get each model to substitute its info in the Template contents.
                ApplyPlaceHolder(codeInfo, generatedCode, placeHolder, writerList, allowDuplicateLines);
                generatedCode = codeInfo.CodeTracker.GetGeneratedCode();
            }

            // Just in case no writers were applied, set System & Component related terms in the generated code.
            generatedCode = codeInfo.CurrentSystem?.SetRelated(codeInfo, generatedCode, null);
            generatedCode = codeInfo.CurrentComponent?.SetRelated(codeInfo, generatedCode, null) ?? generatedCode;
            codeInfo.CodeTracker.CreateEntry(generatedCode);
        }

        public static void ApplyPlaceHolder(CodeGeneratorBase codeInfo, string source, PlaceHolderInfo placeHolder, IEnumerable<ICodeWriter> writerList, bool allowDuplicateLines)
        {
            var codeChangeEntry = codeInfo.CodeTracker.CreateEntry(source);

            foreach (var writer in writerList)
            {
                // placeHolderName after replacing any Model-related terms if applicable.
                var placeHolderName = placeHolder.PlaceHolderType == PlaceHolderType.Marker ?
                    placeHolder.Name : writer.ToContent(codeInfo, placeHolder.Name, placeHolder);

                var isMatchingPlaceHolderFound = source.IndexOf(placeHolderName) >= 0;
                if (isMatchingPlaceHolderFound)
                {
                    var templateHelper = new TemplateHelper(placeHolder, placeHolderName);
                    var templatesCount = templateHelper.GetTemplateCount(source);
                    for (int i = 1; i <= templatesCount; i++)
                    {
                        var input = codeChangeEntry.GeneratedCode;
                        var templateContent = templateHelper.GetTemplateContentType(codeInfo, writer, input, i);

                        var generatedContent = writer.ToContent(codeInfo, templateContent, placeHolder);
                        generatedContent = writer.SetRelated(codeInfo, generatedContent, placeHolder);
                        generatedContent = placeHolder.TemplateContentType == TemplateContentType.WriterProperty && (generatedContent == templateContent) ?
                                           string.Empty : generatedContent;
                        if (!string.IsNullOrEmpty(generatedContent))
                        {
                            var snippet = codeChangeEntry.CreateSnippet(generatedContent);
                            var isFound = input.IndexOf(generatedContent.Trim()) >= 0;
                            if ((allowDuplicateLines) || (!allowDuplicateLines && !isFound))
                            {
                                input = templateHelper.ReplaceTemplate(input, snippet, true, i);
                            }
                            // whether snippet was found or not found and added to code, record the snippet. so it could be tracked down if Rollback is needed.
                            codeChangeEntry.UpdateEntry(input, snippet, writer, placeHolder, templateHelper.Name, i);
                        }
                    }
                }
            }
        }

        public static string ClearPlaceHolders(CodeGeneratorBase codeInfo, string input)
        {
            //Clear Replacable PlaceHolders
            input = ClearPlaceHolder(codeInfo, input, "[ComponentType]", Enum.GetNames(typeof(ComponentType)).ToList());
            input = ClearPlaceHolder(codeInfo, input, "[FieldDefinition]", Enum.GetNames(typeof(FieldDefinition)).ToList());
            input = ClearPlaceHolder(codeInfo, input, "[ReportDefinition]", Enum.GetNames(typeof(ReportDefinition)).ToList());
            input = ClearPlaceHolder(codeInfo, input, "[FilterDefinition]", Enum.GetNames(typeof(FilterDefinition)).ToList());
            input = ClearPlaceHolder(codeInfo, input, "[ChartDefinition]", Enum.GetNames(typeof(ChartDefinition)).ToList());
            input = ClearPlaceHolder(codeInfo, input, "[ReportType]", Enum.GetNames(typeof(ReportType)).ToList());

            //Finally clear Markers
            var markerList = codeInfo.FlatPlaceHolderList.Where(x => x.PlaceHolderType == PlaceHolderType.Marker).ToList();
            foreach (var marker in markerList)
            {
                var templateHelper = new TemplateHelper(marker, marker.Name);
                input = templateHelper.ReplaceMarker(input, string.Empty, marker.KeepPlaceHolder);
            }

            return input;
        }

        public static string ClearPlaceHolder(CodeGeneratorBase codeInfo, string input, string placeHolderMark, List<string> enumNameList)
        {
            var placeHolderList = codeInfo.FlatPlaceHolderList.Where(x => x.Name.Contains(placeHolderMark)).ToList();

            foreach (var placeHolder in placeHolderList)
                foreach (string? enumName in enumNameList)
                {
                    // Get all the possible PlaceHolder names by replacing the mark with enum names.
                    var placeHolderName = placeHolder.Name.Replace(placeHolderMark, enumName);
                    var isMatchingPlaceHolderFound = input.IndexOf(placeHolderName) >= 0;
                    if (isMatchingPlaceHolderFound)
                    {
                        var templateHelper = new TemplateHelper(placeHolder, placeHolderName);
                        var templatesCount = templateHelper.GetTemplateCount(input);
                        for (int i = 1; i <= templatesCount; i++)
                        {
                            var templateContent = templateHelper.GetTemplateContentType(codeInfo, null, input, i); // writer=null. no template externalFile required here
                            input = templateHelper.ReplaceTemplate(input, string.Empty, placeHolder.KeepPlaceHolder, i);
                        }
                    }
                }

            return input;
        }

        private static string HandleSpecialCases(string input)
        {
            string pattern;
            string result = input;

            ////Investor has no "Name" field yet required in list query for dropdowns
            //pattern = "Investor.Name";
            //result = result.Replace(pattern, "(Investor.FirstName + ' ' + Investor.LastName) ");

            ////Broker has no "Name" field yet required in list query for dropdowns
            //pattern = "Broker.Name";
            //result = result.Replace(pattern, "(Broker.FirstName + ' ' + Broker.LastName) ");

            return result;
        }

        public static void KeepExisitingSnipts(CodeGeneratorBase codeInfo, string outputFilePath, string generatedCode)
        {
            var input = FileHelper.ReadFile(outputFilePath);

            var placeHolderName = "Code_DontOverwrite";
            var existingCodeList = new StringBuilder();

            var placeHolder = codeInfo.FlatPlaceHolderList.FirstOrDefault(x => x.Name == placeHolderName);
            if (placeHolder != null)
            {
                var templateHelper = new TemplateHelper(placeHolder, placeHolderName);
                var templatesCount = templateHelper.GetTemplateCount(input);

                for (int i = 1; i <= templatesCount; i++)
                {
                    if (placeHolder.TemplateContentType == TemplateContentType.Embedded)
                    {
                        var existingSnippet = templateHelper.ExtractEmbededTemplate(input, i);
                        if (!string.IsNullOrEmpty(existingSnippet))
                        {
                            existingCodeList.AppendLine($@"//Template_Start_{placeHolderName}_{i}");
                            existingCodeList.AppendLine(existingSnippet);
                            existingCodeList.AppendLine($@"//Template_End_{placeHolderName}_{i}");
                        }
                    }
                }
            }

            var output = new StringBuilder();
            output.AppendLine(generatedCode);
            output.AppendLine(existingCodeList.ToString());

            FileHelper.SaveFile(outputFilePath, output.ToString());
        }

        public static string SaveFile(CodeGeneratorBase codeInfo, TemplateInfo templateInfo, string generatedCode, bool saveEmpty = true)
        {
            var outputFilePath = templateInfo.GetOutputFilePath(codeInfo);
            // We need to keep code if new file to be generated to protect existing code from being overwritten.
            if (templateInfo.TemplateType == TemplateType.Generator && codeInfo.KeepExistingCode && File.Exists(outputFilePath))
            {
                KeepExisitingSnipts(codeInfo, outputFilePath, generatedCode);
            }
            else
            {
                if (string.IsNullOrEmpty(generatedCode) && !saveEmpty)
                    return string.Empty;

                FileHelper.SaveFile(outputFilePath, generatedCode);
            }
            return outputFilePath;
        }

        public static string DeleteFile(CodeGeneratorBase codeInfo, TemplateInfo generatorTemplate)
        {
            var outputFilePath = generatorTemplate.GetOutputFilePath(codeInfo);
            FileHelper.DeleteFile(outputFilePath);
            return outputFilePath;
        }
    }
}

/* Angular Validation Forms
 
 FormGroup instance. formGroup expects a FormGroup instance. Please pass one in.
  
In HTML template:
      <div [formGroup]="myGroup">
        <input formControlName="firstName">
      </div>


In Typescript class:
      this.myGroup = new FormGroup({
          firstName: new FormControl()
      });
 */