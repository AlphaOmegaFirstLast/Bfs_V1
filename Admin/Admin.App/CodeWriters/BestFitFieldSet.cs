using Admin.App.CodeWriters;
using Admin.App.Constants;
using Admin.App;
using Admin.App;
using Admin.App;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;

namespace Admin.App
{
    public class BestFitFieldSet : ICodeWriter
    {
        // Properties
        //public FieldSetDefinition FieldSetDefinition = FieldSetDefinition.Basic;
        public List<BestFitField> FieldList { get; set; } = new List<BestFitField>();

        //ICodeWriter implementation
        public string Name { get; set; } = string.Empty;

        //Inner Fields
        public int FormColumnCount = 1;
        public string htmlFormFields = string.Empty;
        public string backendValidators = string.Empty;

        public BestFitFieldSet(List<BestFitField>? fieldList)
        {
            this.Name = "FieldSet";
            FieldList = fieldList ?? new List<BestFitField>();
            foreach (var field in FieldList)
            {
                //set row & column for Form controls
                FormColumnCount = field.FormInfo.Column.HasValue && field.FormInfo.Column > FormColumnCount ? (int)field.FormInfo.Column : FormColumnCount;
            }
        }

        public string SetRelated(CodeBase codeInfo, string input, PlaceHolderInfo? placeHolder)
        {
            input =  codeInfo.CurrentSystem?.ToContent(codeInfo, input, placeHolder)?? input;
            return codeInfo.CurrentComponent?.ToContent(codeInfo, input, placeHolder) ?? input;
        }

        public virtual string ToContent(CodeBase codeInfo, string input, PlaceHolderInfo? placeHolder)
        {
            input = input.Replace("[HtmlFormControls]", SetHtmlFormFields(codeInfo, placeHolder));
            input = input.Replace("[Validators]", SetValidatorFieldSet(codeInfo, placeHolder));
            input = input.Replace("[FrontendLink]", SetActions(codeInfo, placeHolder));
            input = input.Replace("[FrontendLink]", SetActions(codeInfo, placeHolder));
            return input;
        }

        public string SetHtmlFormFields(CodeBase codeInfo, PlaceHolderInfo placeHolder)
        {
            var generatedFieldList = new StringBuilder();

            if (placeHolder.Name.Contains("HtmlFormControls"))
            {
                //default to 2 columns if not specified, "class ='col-6' "
                var columnWidth = FormColumnCount == 1 ? 6 : (12 / FormColumnCount);

                for (var i = 1; i <= FormColumnCount; i++)
                {
                    var fieldSetOfColumn = FieldList
                        .Where(f => f.FormInfo != null && f.FormInfo.Column == i && f.FormInfo.Row > 0)
                        .OrderBy(f => f.FormInfo.Row.HasValue ? f.FormInfo.Row.Value : 1).ToList();

                    generatedFieldList.AppendLine($"<div class=\"col-{columnWidth}\">");
                    foreach (var fieldInfo in fieldSetOfColumn)
                    {
                        var filePath = fieldInfo.ToContent(codeInfo, placeHolder.TemplateFile, placeHolder);
                        var fieldTemplate = TemplateHelper.GetTemplateFileContent(codeInfo.TemplateRootDir, filePath);
                        var templateContent = fieldInfo.ToContent(codeInfo, fieldTemplate, placeHolder);

                        var generatedField = fieldInfo.SetRelated(codeInfo, templateContent, placeHolder);
                        generatedFieldList.AppendLine(generatedField);
                    }

                    generatedFieldList.AppendLine($"</div>");
                }
            }

            return generatedFieldList.ToString();
        }

        public string SetActions(CodeBase codeInfo, PlaceHolderInfo placeHolder)
        {
            var generatedActionList = new StringBuilder();

            if (placeHolder.Name.Contains("FrontendLink"))
            {
                var actionList = codeInfo.SystemActionList.Where(a => a.BfsComponentId == codeInfo.CurrentComponent?.Id
                                && a.WriterTypeId == WriterType.FieldSet
                                && a.ActionTypeId == ActionType.FrontendLink)
                                .ToList();
                foreach (var actionInfo in actionList)
                {
                    var actionTemplate = actionInfo.ActionTemplate;
                    var generatedAction = this.SetRelated(codeInfo, actionTemplate, placeHolder);
                    generatedActionList.AppendLine(generatedAction);
                }
                ;
            }

            if (placeHolder.Name.Contains("FrontendFunction"))
            {
                var actionList = codeInfo.SystemActionList.Where(a => a.BfsComponentId == codeInfo.CurrentComponent?.Id
                                && a.WriterTypeId == WriterType.FieldSet
                                && a.ActionTypeId == ActionType.FrontendFunction)
                                .ToList();
                foreach (var actionInfo in actionList)
                {
                    var actionTemplate = actionInfo.ActionTemplate;
                    var generatedAction = this.SetRelated(codeInfo, actionTemplate, placeHolder);
                    generatedActionList.AppendLine(generatedAction);
                }
                ;
            }

            return generatedActionList.ToString();
        }

        public string SetValidatorFieldSet(CodeBase codeInfo, PlaceHolderInfo placeHolder )
        {
            var fieldListContent = new StringBuilder();
            if (placeHolder.Name.Contains("Validators"))
            {
                var modifierTemplate = codeInfo.CurrentTemplate?.ModifierTemplateList.FirstOrDefault(x => x.OutputFile.Contains("ErrorCodes"));
                var fieldList = FieldList.Where(f => f.FieldDefinition == FieldDefinition.Primitive && !f.isLookup).ToList();

                foreach (var fieldInfo in fieldList)
                {
                    var fieldRules = GenerateValidatorLine(fieldInfo);
                    if (!string.IsNullOrEmpty(fieldRules))
                    {
                        fieldListContent.AppendLine(fieldRules);
                        ModifyErrorFile(codeInfo, fieldInfo, modifierTemplate);
                    }
                }
            }

            return fieldListContent.ToString();
        }

        public static void ModifyErrorFile(CodeBase codeInfo, BestFitField fieldInfo, TemplateInfo modifierTemplate)
        {
            var functionName = "Invalid" + $@"{fieldInfo.fieldCapitalName}";
            var generatedCode = $"public const string {functionName} = \"{functionName}\";";
            var outputFile = modifierTemplate.GetOutputFilePath(codeInfo);
            var source = FileHelper.ReadFile(outputFile);
            if (!source.Contains(generatedCode))
            {
                var generatedLine = new StringBuilder();
                generatedLine.AppendLine(generatedCode);
                var placeHolder = codeInfo.FlatPlaceHolderList.FirstOrDefault(x => x.Name.Contains("ErrorCodes"));
                var templateHelper = new TemplateHelper(placeHolder, placeHolder.Name);
                var result = templateHelper.ReplaceMarker(source, generatedLine.ToString(), true);

                FileHelper.SaveFile(outputFile, result);
            }
        }

        public static string GenerateValidatorLine(BestFitField fieldInfo)
        {
            var fieldRules = new StringBuilder();
            var i = 0;

            fieldRules.AppendLine("RuleFor(x => x." + $@"{fieldInfo.fieldCapitalName}" + ")");

            if (fieldInfo.FieldValidation.IsRequired ?? false)
            {
                i++;
                fieldRules.AppendLine(".NotEmpty().WithErrorCode(ErrorCodes.Invalid" + $@"{fieldInfo.fieldCapitalName}" + ")");
            }

            if (!string.IsNullOrEmpty(fieldInfo.FieldValidation.MinLength))
            {
                i++;
                fieldRules.AppendLine(".MinimumLength(" + fieldInfo.FieldValidation.MinLength + ")");
            }

            if (!string.IsNullOrEmpty(fieldInfo.FieldValidation.MaxLength))
            {
                i++;
                fieldRules.AppendLine(".MaximumLength(" + fieldInfo.FieldValidation.MaxLength + ")");
            }

            if ((!string.IsNullOrEmpty(fieldInfo.FieldValidation.MinValue)) && (fieldInfo.BackendDataTypeId == BackendDataType.DT_DateTime))
            {
                i++;
                fieldRules.AppendLine(".GreaterThanOrEqualTo(new DateTime(" + fieldInfo.FieldValidation.MinValue + ",1,1))" + ".WithErrorCode(ErrorCodes.Invalid" + $@"{fieldInfo.fieldCapitalName}" + ")");
            }

            if ((!string.IsNullOrEmpty(fieldInfo.FieldValidation.MaxValue)) && (fieldInfo.BackendDataTypeId == BackendDataType.DT_DateTime))
            {
                i++;
                fieldRules.AppendLine(".LessThanOrEqualTo(new DateTime(" + fieldInfo.FieldValidation.MaxValue + ",1,1))" + ".WithErrorCode(ErrorCodes.Invalid" + $@"{fieldInfo.fieldCapitalName} )");
            }

            return i>0? $@"{fieldRules.ToString()};" : string.Empty;
        }
    }
}