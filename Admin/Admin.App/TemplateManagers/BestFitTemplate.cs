//using Newtonsoft.Json;
//using Newtonsoft.Json.Converters;
using Admin.App.Constants;
using Admin.App;

namespace Admin.App
{
    public class BestFitTemplate
    {
        //only public properties with { get; set; } will be serialized \ deserialized in TemplateData.json
        public string Id { get; set; } = string.Empty;
        public TemplateElementType TemplateElementType { get; set; }

        public CodeType CodeType { get; set; }

        public string TemplateRootDir = string.Empty;

        public List<TemplateInfo> GeneratorTemplateList { get; set; } = new List<TemplateInfo>();
        public List<TemplateInfo> ModifierTemplateList { get; set; } = new List<TemplateInfo>();

        public static List<BestFitTemplate> GetList(string jsonFilePath)
        {
            // Read SystemInfo.json and return a list of SystemInfo objects
            var list = FileHelper.ReadJson<BestFitTemplate>(jsonFilePath);
            foreach (var bestFitTemplate in list)
            {
                foreach (var generator in bestFitTemplate.GeneratorTemplateList)
                {
                    generator.TemplateType = TemplateType.Generator;
                    generator.TemplateElementType = bestFitTemplate.TemplateElementType;
                    generator.CodeType = bestFitTemplate.CodeType;
                }
                foreach (var modifier in bestFitTemplate.ModifierTemplateList)
                {
                    modifier.TemplateType = TemplateType.Modifier;
                    modifier.TemplateElementType = bestFitTemplate.TemplateElementType;
                    modifier.CodeType = bestFitTemplate.CodeType;
                }
            }

            return list;
        }

        public static List<TemplateInfo> GetFlatList(List<BestFitTemplate> list)
        {
            var generatorTemplates = list.SelectMany(x => x.GeneratorTemplateList);
            var modifierTemplates = list.SelectMany(x => x.ModifierTemplateList);
            
            var flatList = new List<TemplateInfo>();
            flatList.AddRange(generatorTemplates);
            flatList.AddRange(modifierTemplates);
            return flatList;
        }
    }
}
