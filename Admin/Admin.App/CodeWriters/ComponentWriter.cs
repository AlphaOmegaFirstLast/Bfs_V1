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
    public class ComponentWriter : ICodeWriter
    {
        // Properties
        public long Id { get; set; }
        public string Name { get; set; }            // component is table then it is table name in DB
        public string DisplayName { get; set; }
        public DataType DataType { get; set; }
        public ReportType ReportType { get; set; }

        public string MenuName { get; set; }
        public string MenuPlaceHolder { get; set; }
        public string QueryBaseTable { get; set; } // used in defining Report component
        public string InterfaceRequired { get; set; }
        public string Notes { get; set; }

        // Fields
        public string DbComponentName = string.Empty;

        public string ComponentNameCapital = string.Empty;

        public string ComponentNameSmall = string.Empty;

        public string ComponentFileName = string.Empty;

        public string ReportNameCapital = string.Empty;

        public string ReportNameSmall = string.Empty;

        public string ReportFileName = string.Empty;
        public string DbParentTable { get; set; } = string.Empty;

        public ComponentType ComponentType = ComponentType.None;

        public List<FieldWriter> FieldList { get; set; } = new List<FieldWriter>();

        public ComponentWriter(IComponentEntity source, string systemName, List<IFieldEntity> allFieldList)
        {
            this.Id = source.Id;
            this.Name = source.Name;
            this.DisplayName = source.DisplayName;
            this.DataType = (DataType)source.DataTypeId;

            this.MenuName = source.MenuName;
            this.MenuPlaceHolder = source.MenuPlaceHolder;

            this.QueryBaseTable = source.QueryBaseTable; //todo add field to ComponentEntity // source.QueryBaseTable;
            this.Notes = source.Notes;
            this.InterfaceRequired = source.InterfaceRequired;

            var result = CodeGeneratorBase.GetNames(Name);
            ComponentNameCapital = result.Item1;
            ComponentNameSmall = result.Item2;
            ComponentFileName = result.Item3;

            ComponentType = this.DataType == DataType.Tables || this.DataType == DataType.Seed ? ComponentType.Table : ComponentType.Report;
            this.ReportType = ComponentType == ComponentType.Table? ReportType.List : ReportType.Compare;
            QueryBaseTable = string.IsNullOrEmpty(QueryBaseTable) ? ComponentNameCapital : QueryBaseTable;

            var reportName = $"{ComponentNameCapital}{ReportType.ToString()}";
            result = CodeGeneratorBase.GetNames(reportName);
            ReportNameCapital = result.Item1;
            ReportNameSmall = result.Item2;
            ReportFileName = result.Item3;

            SetFieldList(allFieldList);
        }

        public string SetRelated(CodeGeneratorBase codeInfo, string input, PlaceHolderInfo? placeHolder)
        {
            input = codeInfo.CurrentSystem?.ToContent(codeInfo, input, placeHolder) ?? input;
            input = ToContent(codeInfo, input, placeHolder);
            return input;
        }

        public void SetFieldList(List<IFieldEntity> allFieldList)
        {
            FieldList = allFieldList.Where(x => x.BfsComponentId == Id).Select(x => new FieldWriter(x)).ToList();

            var tabIndex = 1;
            foreach (var field in FieldList)
            {
                field.SetInternalFields(ComponentType, ComponentNameCapital,QueryBaseTable);

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
            //table names in the database have prefixes to uniqulely identify which system they belong to, so we need to add the prefix to the parent table name when generating code related to database.
            DbComponentName = codeInfo.CurrentSystem.IsMaster ? ComponentNameCapital : $"{codeInfo?.CurrentSystem?.DbPrefix}{ComponentNameCapital}";
            DbParentTable = codeInfo.CurrentSystem.IsMaster ? QueryBaseTable : $"{codeInfo?.CurrentSystem?.DbPrefix}{QueryBaseTable}";

            var outputContent = input.Replace("[ComponentType]", ComponentType.ToString());

            outputContent = outputContent.Replace("[BestFitDB]", codeInfo.BestFitDB);
            outputContent = outputContent.Replace("[BestFitSystemName]", codeInfo.BestFitSystemName);

            outputContent = outputContent.Replace("[DbComponentName]", DbComponentName.Trim());
            outputContent = outputContent.Replace("[ComponentNameCapital]", ComponentNameCapital.Trim());
            outputContent = outputContent.Replace("[ComponentNameSmall]", ComponentNameSmall.Trim());
            outputContent = outputContent.Replace("[ComponentFileName]", ComponentFileName.Trim());

            outputContent = outputContent.Replace("[ComponentMenuName]", MenuName.Trim());
            outputContent = outputContent.Replace("[MenuPlaceHolder]", MenuPlaceHolder.Trim());
            outputContent = outputContent.Replace("[QueryBaseTable]", QueryBaseTable);
            outputContent = outputContent.Replace("[DbParentTable]", DbParentTable);
            outputContent = outputContent.Replace("[InterfaceRequired]", string.IsNullOrEmpty(InterfaceRequired)?"":$",{InterfaceRequired}" );

            outputContent = outputContent.Replace("[ReportNameCapital]", ReportNameCapital.Trim());
            outputContent = outputContent.Replace("[ReportNameSmall]", ReportNameSmall.Trim());
            outputContent = outputContent.Replace("[ReportFileName]", ReportFileName.Trim());

            return outputContent;
        }
    }
}