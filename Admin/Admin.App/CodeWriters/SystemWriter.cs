using Admin.App.CodeWriters;
using Admin.App;
using Admin.App;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Admin.App
{
    public class SystemWriter : ICodeWriter
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string BasePortNumber { get; set; } = string.Empty;
        public string DbPrefix { get; set; } = string.Empty;

        public SystemWriter(IBestFitSystem source)
        {
            this.Id = source.Id;
            this.Name = source.Name;
            this.BasePortNumber = source.BasePortNumber;
            this.DbPrefix = source.DbPrefix;
        }

        public string SetRelated(CodeGeneratorBase codeInfo, string input, PlaceHolderInfo? placeHolder)
        {
            return ToContent(codeInfo, input, placeHolder);
        }

        public virtual string ToContent(CodeGeneratorBase codeInfo, string input, PlaceHolderInfo? placeHolder)
        {
            var outputContent = input.Replace("[SystemPrefixSmall]", DbPrefix.Trim().ToLower());
            outputContent = outputContent.Replace("[SystemNameCapital]", Name.Trim());
            outputContent = outputContent.Replace("[SystemNameSmall]", Name.ToLower());
            outputContent = outputContent.Replace("[BasePortNumber]", BasePortNumber);
            return outputContent;
        }

        public virtual string ToDestination(CodeGeneratorBase codeInfo, string input, PlaceHolderInfo? placeHolder)
        {
            var sourceProperty = Name.Trim();
            var destinationProperty = sourceProperty;
            var outputContent = input.Replace("[AddRouteEntry]", Name.Trim());
            return outputContent;
        }
    }
}