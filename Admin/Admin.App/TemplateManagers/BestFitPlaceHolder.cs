using Admin.App.Constants;
using Admin.App;

namespace Admin.App
{
    public class BestFitPlaceHolder
    {
        public TemplateElementType TemplateElementType { get; set; } = TemplateElementType.None;
        public CodeType CodeType { get; set; } = CodeType.Backend;

        public WriterType WriterType { get; set; } = WriterType.None;
        public List<PlaceHolderInfo> PlaceHolderInfoList{ get; set; } = new List<PlaceHolderInfo>();

        public static List<BestFitPlaceHolder> GetList(string jsonFilePath)
        {
            // Read SystemInfo.json and return a list of SystemInfo objects
            var list =  FileHelper.ReadJson<BestFitPlaceHolder>(jsonFilePath);
            foreach (var bestFitPlaceHolder in list)
            {
                foreach (var placeHolder in bestFitPlaceHolder.PlaceHolderInfoList)
                {
                    placeHolder.WriterType = bestFitPlaceHolder.WriterType;
                    //placeHolder.TemplateElementType = bestFitPlaceHolder.TemplateElementType;
                    //placeHolder.CodeType = bestFitPlaceHolder.CodeType;
                }
            }
            return list;
        }

        public static List<PlaceHolderInfo> GetFlatList(List<BestFitPlaceHolder> list)
        {
           return list.SelectMany(x => x.PlaceHolderInfoList).ToList();
        }
    }
}
