using Admin.App.Constants;
using Admin.App;
using Admin.App;
using Admin.App;
using Admin.App;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using Admin.App.CodeWriters;

namespace Admin.App
{
    public class BestFitAction : ICodeWriter
    {
        // Properties
        public long Id { get; set; }
        public string Name { get; set; }
        public long BfsComponentId { get; set; }
        public string ActionTemplate { get; set; }

        public ActionType ActionTypeId { get; set; }
        public ActionLocation ActionLocationId { get; set; }
        public ActionSource ActionSourceId { get; set; }

        public WriterType WriterTypeId { get; set; }
        public string MatchProperty { get; set; }
        public string[] MatchValues { get; set; }
        public string Notes { get; set; }
        public BestFitAction()
        { }
        public BestFitAction(IBestFitAction source)
        {
            this.Id = source.Id;
            this.Name = source.Name;
            this.BfsComponentId = source.BfsComponentId;
            this.ActionTemplate = source.ActionTemplate;
            this.ActionTypeId = source.ActionType;
            this.ActionLocationId = source.ActionLocation;
            this.WriterTypeId = source.WriterType;
            this.MatchProperty = source.MatchProperty;
            this.MatchValues = source.MatchValues;
            this.Notes = source.Notes;
        }

        public string SetRelated(CodeBase codeInfo, string input, PlaceHolderInfo? placeHolder)
        {
            input = codeInfo.CurrentSystem?.ToContent(codeInfo, input, placeHolder) ?? input;
            input = codeInfo.CurrentComponent?.ToContent(codeInfo, input, placeHolder) ?? input;
            return input;
        }

        public virtual bool IsMatch(CodeBase codeInfo, string input, PlaceHolderInfo? placeHolder)
        {
            var subsititute = input.Replace("[ActionType]", this.ActionTypeId.ToString());
            return placeHolder.Name == subsititute;
        }

        public virtual string ToContent(CodeBase codeInfo, string input, PlaceHolderInfo? placeHolder)
        {
            var output = input;
            output = output.Replace("[FrontendLink]", GetActionList(codeInfo, output, placeHolder, ActionType.FrontendLink));
            output = output.Replace("[FrontendFunction]", GetActionList(codeInfo, output, placeHolder, ActionType.FrontendFunction));
            return output;
        }

        public string GetActionList(CodeBase codeInfo, string input, PlaceHolderInfo? placeHolder, ActionType actionType)
        {
            var outputContent = new StringBuilder();
            var actionList = codeInfo.SystemActionList.Where(a => a.ActionTypeId == actionType);
            outputContent.AppendLine();

            foreach (var action in actionList)
            {
                switch (action.WriterTypeId)
                {
                    case WriterType.System:
                        var system = codeInfo.CurrentSystem;
                        if (system != null)
                        {
                            var propertyValue = system.ToContent(codeInfo, action.MatchProperty, null);
                            if (action.MatchValues.Contains(propertyValue))
                            {
                                outputContent.AppendLine("links.push({");
                                outputContent.Append($"actionSource:'{action.ActionSourceId.ToString()}', actionType:'{action.ActionTypeId.ToString()}', actionLocation:'{action.ActionLocationId.ToString()}'");
                                outputContent.Append(",");
                                outputContent.Append(system.SetRelated(codeInfo, action.ActionTemplate, placeHolder));
                                outputContent.AppendLine("});");
                            }
                        }
                        break;

                    case WriterType.Component:
                        var component = codeInfo.CurrentComponent;
                        if (component != null)
                        {
                            var propertyValue = component.ToContent(codeInfo, action.MatchProperty, null);
                            if (action.MatchValues.Contains(propertyValue))
                            {
                                outputContent.AppendLine("links.push({");
                                outputContent.Append($"actionSource:'{action.ActionSourceId.ToString()}', actionType:'{action.ActionTypeId.ToString()}', actionLocation:'{action.ActionLocationId.ToString()}'");
                                outputContent.Append(",");
                                outputContent.AppendLine(component.SetRelated(codeInfo, action.ActionTemplate, placeHolder));
                                outputContent.AppendLine("});");
                            }
                        }
                        break;

                    case WriterType.Field:
                        var fieldList = codeInfo.CurrentComponent?.FieldList;
                        if (fieldList != null)
                        {
                            foreach (ICodeWriter field in fieldList)
                            {
                                var propertyValue = field.ToContent(codeInfo, action.MatchProperty, null);
                                if (action.MatchValues.Contains(propertyValue))
                                {
                                    outputContent.AppendLine("links.push({");
                                    outputContent.Append($"actionSource:'{action.ActionSourceId.ToString()}', actionType:'{action.ActionTypeId.ToString()}', actionLocation:'{action.ActionLocationId.ToString()}'");
                                    outputContent.Append(",");
                                    outputContent.AppendLine(field.SetRelated(codeInfo, action.ActionTemplate, placeHolder));
                                    outputContent.AppendLine("});");
                                }
                            }
                        }
                        break;
                }
            }
            return outputContent.ToString();
        }
    }
}
    