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
    public class BestFitComponent : ICodeWriter
    {
        // Properties
        public long Id { get; set; }
        public string Name { get; set; }            // component is table then it is table name in DB
        public string DisplayName { get; set; }
        public DataType DataType { get; set; }

        public string MenuName { get; set; }
        public string MenuPlaceHolder { get; set; }
        public string QueryBaseTable { get; set; } // used in defining Report component
        public string Notes { get; set; }

        // Fields
        public string NameCapital = string.Empty;

        public string NameSmall = string.Empty;

        public string FileName = string.Empty;

        public ReportType ReportType = ReportType.None;

        public ComponentType ComponentType = ComponentType.None;

        public List<BestFitField> FieldList { get; set; } = new List<BestFitField>();

        public BestFitComponent(IBestFitComponent source, string systemName, List<IBestFitField> allFieldList)
        {
            this.Id = source.Id;
            this.Name = source.Name;
            this.DisplayName = source.DisplayName;
            this.DataType = (DataType)source.DataTypeId;

            this.MenuName = source.MenuName;
            this.MenuPlaceHolder = source.MenuPlaceHolder;

            this.QueryBaseTable = source.QueryBaseTable; //todo add field to ComponentEntity // source.QueryBaseTable;
            this.Notes = source.Notes;

            var result = CodeGeneratorBase.GetNames(Name);
            NameCapital = result.Item1;
            NameSmall = result.Item2;
            FileName = result.Item3;

            ComponentType = this.DataType == DataType.Tables || this.DataType == DataType.Seed ? ComponentType.Table : ComponentType.Report;
            this.ReportType = ComponentType == ComponentType.Table? ReportType.List : ReportType.Compare;
            QueryBaseTable = string.IsNullOrEmpty(QueryBaseTable) ? NameCapital : QueryBaseTable;

            SetFieldList(allFieldList);
        }

        public string SetRelated(CodeGeneratorBase codeInfo, string input, PlaceHolderInfo? placeHolder)
        {
            input = codeInfo.CurrentSystem?.ToContent(codeInfo, input, placeHolder) ?? input;
            input = ToContent(codeInfo, input, placeHolder);
            return input;
        }

        public void SetFieldList(List<IBestFitField> allFieldList)
        {
            FieldList = allFieldList.Where(x => x.BfsComponentId == Id).Select(x => new BestFitField(x)).ToList();

            var tabIndex = 1;
            foreach (var field in FieldList)
            {
                field.SetInternalFields(ComponentType, NameCapital,QueryBaseTable);

                //set tab index only for ChildrenList and ChildrenMatrix fields
                if (field.FieldDefinition == FieldDefinition.ChildrenList || field.FieldDefinition == FieldDefinition.ChildrenMatrix)
                {
                    tabIndex++;
                    field.tabIndex = tabIndex.ToString();
                }
            }
        }

        public virtual string ToContent(CodeGeneratorBase codeInfo, string input, PlaceHolderInfo? placeHolder)
        {
            var outputContent = input.Replace("[ComponentNameCapital]", NameCapital.Trim());
            outputContent = outputContent.Replace("[ComponentNameSmall]", NameSmall.Trim());
            outputContent = outputContent.Replace("[ComponentFileName]", FileName.Trim());
            outputContent = outputContent.Replace("[ComponentMenuName]", MenuName.Trim());
            outputContent = outputContent.Replace("[MenuPlaceHolder]", MenuPlaceHolder.Trim());
            outputContent = outputContent.Replace("[QueryBaseTable]", QueryBaseTable);
            outputContent = outputContent.Replace("[ComponentType]", ComponentType.ToString());
            outputContent = outputContent.Replace("[ReportTypeCapital]", ReportType.ToString());
            outputContent = outputContent.Replace("[ReportTypeSmall]", ReportType.ToString().ToLower());

            return outputContent;
        }
    }
}