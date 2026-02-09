using Admin.App.Constants;
using Admin.App;
using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.App
{
    public class PlaceHolderInfo
    {
        public List<string> ParentId { get; set; } = new List<string>();
        public string Name { get; set; } = string.Empty;
        public WriterType WriterType { get; set; } = WriterType.None;
        public PlaceHolderType PlaceHolderType { get; set; } = PlaceHolderType.StartEnd;

        public string TemplateFile { get; set; } = string.Empty;
        public TemplateContentType TemplateContentType { get; set; } = TemplateContentType.Embedded;
        public bool KeepPlaceHolder { get; set; } = false;
        public string Notes { get; set; } = string.Empty;


        public List<PlaceHolderInfo> flatPlaceHolderList = new List<PlaceHolderInfo>();

        public static List<PlaceHolderInfo> GetList(string jsonFilePath)
        {
            // Read SystemInfo.json and return a list of SystemInfo objects
            var list = FileHelper.ReadJson<BestFitPlaceHolder>(jsonFilePath);
            return list.SelectMany(x => x.PlaceHolderInfoList).ToList();
        }

        public List<PlaceHolderInfo> ChildListOfTemplate(TemplateInfo templateInfo)
        {
            var placeHolders = flatPlaceHolderList.Where(x => x.ParentId.Contains(templateInfo.Id)).ToList();
            return placeHolders;
        }

        public List<PlaceHolderInfo> ListOfPlaceHolderType(PlaceHolderType placeHolderType)
        {
            var placeHolders = flatPlaceHolderList.Where(x => x.PlaceHolderType == placeHolderType).ToList();
            return placeHolders;
        }
    }
}
