//using Newtonsoft.Json;
//using Newtonsoft.Json.Converters;
using Admin.App.Constants;
using Admin.App;

namespace Admin.App
{
    public class TemplateInfo
    {
        public string Id { get; set; } = string.Empty;
        public string TemplateFile { get; set; }= string.Empty;

        public string OutputFile { get; set; } = string.Empty;

        public TemplateElementType TemplateElementType { get; set; }

        public CodeType CodeType { get; set; }
        public TemplateType TemplateType { get; set; }

        public SaveGeneratedCode SaveGeneratedCode { get; set; } = SaveGeneratedCode.PerAllWriters;

        public string GetOutputFilePath(CodeGeneratorBase codeInfo)
        {
            var dataType = codeInfo.CurrentComponent?.DataType;
            dataType = dataType.HasValue ? (DataType)dataType.Value : DataType.None;
            return OutputFile
                              .Replace("[SystemRootDir]", codeInfo.SystemRootDir)
                              .Replace("[AppDir]", codeInfo.AppDir)

                              .Replace("[SystemNameCapital]", codeInfo.CurrentSystem?.Name)
                              .Replace("[SystemNameSmall]", codeInfo.CurrentSystem?.Name.ToLower())

                              .Replace("[ComponentNameCapital]", codeInfo.CurrentComponent?.NameCapital)
                              .Replace("[ComponentFileName]", codeInfo.CurrentComponent?.FileName)
            
                              .Replace("[DataType]", dataType?.ToString().ToLower());
        }
    }
}
