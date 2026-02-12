using Admin.App.CodeWriters;
using Admin.App.Constants;
using Admin.App;
using Admin.App;
using Bfs.Core.ObjectFields;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OpenTelemetry.Resources;

namespace Admin.App
{
    public class BestFitField : ICodeWriter , IBestFitField
    {
        public long Id { get; set; }
        public long BfsComponentId { get; set; }
        public string Field { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public bool IsQueryColumn { get; set; } = true;
        public bool IsJoinField { get; set; } = false;
        public string ParentTable { get; set; } = string.Empty;
        public FilterType FilterTypeId { get; set; } = FilterType.None;
        public BackendDataType BackendDataTypeId { get; set; } = BackendDataType.DT_Default;

        public FieldValidation FieldValidation { get; set; } = new FieldValidation();
        public FormInfo FormInfo { get; set; } = new FormInfo();
        public ReportInfo ReportInfo { get; set; } = new ReportInfo();
        public MatrixInfo MatrixInfo { get; set; } = new MatrixInfo();
        public ToolTipInfo ToolTipInfo { get; set; } = new ToolTipInfo();
        public FormControlType FormControlTypeId { get; set; } = FormControlType.Default;

        //ICodeWriter implementation
        public string Name { get; set; } = string.Empty;

        // internal Base fields, dont get serialized
        public string parentTableSmall;
        public string fieldCapitalName;
        public string fieldSmallName;
        public string fieldFileName;

        public bool isLookup;
        public string lookupNameCapital;
        public string lookupNameSmall;
        public string lookupFileName;
        public string joinName;
        public string sortName;

        public FieldDefinition FieldDefinition = FieldDefinition.Primitive;
        public ReportDefinition ReportDefinition = ReportDefinition.None;
        public FilterDefinition FilterDefinition = FilterDefinition.None;
        public ChartDefinition ChartDefinition = ChartDefinition.None;
       // public MatrixDefinition MatrixDefinition = MatrixDefinition.None;

        //Table internal fields
        public string backendDataType = "string";
        public string frontendDataType = "string";

        public string backendDefaultValue = string.Empty;
        public string frontendDefaultValue = string.Empty;

        // Form internal fields, dont get serialized
        public string tabIndex = string.Empty;
        public string uiFormControl = "";

        // Report internal fields, dont get serialized
        public bool isAggregate;
        public string aggregateFunction;
        public string aggregateName;
        public string reportFieldNameCapital;
        public string reportFieldNameSmall;

        public string filterValueName = string.Empty;
        public string filterLookupName = string.Empty;
        public string filterRangeName = string.Empty;
        public string backendRangeType = string.Empty;

        public bool isChartVerticalField = false;
        public bool isChartHorizontalField = false;

        public string toolTipNote = string.Empty;
        public string toolTipVisibility = string.Empty;

        public string matrixNameCapital = string.Empty;
        public string matrixNameSmall = string.Empty;
        public string matrixFileName = string.Empty;

        public BestFitField(IBestFitField source)
        {
            if (source != null)
            {
                this.Name = source.Field ?? string.Empty;
                this.Field = source.Field ?? string.Empty;
                this.DisplayName = source.DisplayName ?? string.Empty;

                this.IsQueryColumn = source.IsQueryColumn;
                this.IsJoinField = source.IsJoinField;
                this.ParentTable = source.ParentTable ?? string.Empty;

                this.BackendDataTypeId = source.BackendDataTypeId;
                this.FilterTypeId = source.FilterTypeId;

                //Object Fields. mainly used in Bestfir components & fields. to collect meta data.
                this.FieldValidation = source.FieldValidation ?? new FieldValidation();
                this.FormInfo = source.FormInfo ?? new FormInfo();
                this.ReportInfo = source.ReportInfo ?? new ReportInfo();
                this.MatrixInfo = source.MatrixInfo ?? new MatrixInfo();
                this.ToolTipInfo = source.ToolTipInfo ?? new ToolTipInfo();
            }
            else
            {
                MessageBox.Show("FieldEntity is null.");
            }
        }

        public string SetRelated(CodeGeneratorBase codeInfo, string input, PlaceHolderInfo? placeHolder)
        {
            input = codeInfo.CurrentSystem?.ToContent(codeInfo, input, placeHolder) ?? input;
            input = codeInfo.CurrentComponent?.ToContent(codeInfo, input, placeHolder) ?? input;
            input = ToContent(codeInfo, input, placeHolder);
            return input;
        }


        public virtual string ToContent(CodeGeneratorBase codeInfo, string input, PlaceHolderInfo? placeHolder)
        {
            var fieldTemplate = input;

            fieldTemplate = fieldTemplate.Replace("[CapitalFieldName]", fieldCapitalName);
            fieldTemplate = fieldTemplate.Replace("[SmallFieldName]", fieldSmallName);
            fieldTemplate = fieldTemplate.Replace("[DisplayName]", DisplayName);

            fieldTemplate = fieldTemplate.Replace("[LookupNameCapital]", lookupNameCapital);
            fieldTemplate = fieldTemplate.Replace("[LookupFileName]", lookupFileName);
            fieldTemplate = fieldTemplate.Replace("[LookupNameSmall]", lookupNameSmall);
            fieldTemplate = fieldTemplate.Replace("[JoinName]", joinName);
            fieldTemplate = fieldTemplate.Replace("[SortName]", sortName);
            fieldTemplate = fieldTemplate.Replace("[IsQueryColumn]", IsQueryColumn.ToString().ToLower());

            fieldTemplate = fieldTemplate.Replace("[FieldDefinition]", FieldDefinition.ToString());
            fieldTemplate = fieldTemplate.Replace("[ReportDefinition]", ReportDefinition.ToString());
            fieldTemplate = fieldTemplate.Replace("[FilterDefinition]", FilterDefinition.ToString());
            fieldTemplate = fieldTemplate.Replace("[ChartDefinition]", ChartDefinition.ToString());

            fieldTemplate = fieldTemplate.Replace("[BackendFieldType]", backendDataType);
            fieldTemplate = fieldTemplate.Replace("[FrontendFieldType]", frontendDataType);
            fieldTemplate = fieldTemplate.Replace("[ParentTable]", ParentTable);
            fieldTemplate = fieldTemplate.Replace("[ParentTableSmall]", parentTableSmall);

            //Table specific
            fieldTemplate = fieldTemplate.Replace("[BackendDefaultValue]", backendDefaultValue);
            fieldTemplate = fieldTemplate.Replace("[FrontendDefaultValue]", frontendDefaultValue);

            //Form specific
            fieldTemplate = fieldTemplate.Replace("[UIFormControl]", uiFormControl);
            fieldTemplate = fieldTemplate.Replace("[TabIndex]", tabIndex);

            //Report specific
            fieldTemplate = fieldTemplate.Replace("[ReportFieldNameCapital]", reportFieldNameCapital);
            fieldTemplate = fieldTemplate.Replace("[ReportFieldNameSmall]", reportFieldNameSmall);
            fieldTemplate = fieldTemplate.Replace("[AggregateFunction]", aggregateFunction);
            fieldTemplate = fieldTemplate.Replace("[AggregateName]", aggregateName);

            //Filters
            fieldTemplate = fieldTemplate.Replace("[FilterLookupName]", filterLookupName);
            fieldTemplate = fieldTemplate.Replace("[FilterValueName]", filterValueName);
            fieldTemplate = fieldTemplate.Replace("[FilterRangeName]", filterRangeName);
            fieldTemplate = fieldTemplate.Replace("[BackendRangeType]", backendRangeType);

            //Matrix Specific
            fieldTemplate = fieldTemplate.Replace("[MatrixNameCapital]", matrixNameCapital);
            fieldTemplate = fieldTemplate.Replace("[MatrixNameSmall]", matrixNameSmall);
            fieldTemplate = fieldTemplate.Replace("[MatrixFileName]", matrixFileName);
            fieldTemplate = fieldTemplate.Replace("[WriterFileName]", matrixFileName);

            fieldTemplate = fieldTemplate.Replace("[ParentApiName]", MatrixInfo?.ParentApi);
            fieldTemplate = fieldTemplate.Replace("[HorizontalApiName]", MatrixInfo?.HorizontalApi);
            fieldTemplate = fieldTemplate.Replace("[VerticalApiName]", MatrixInfo?.VerticalApi);
            fieldTemplate = fieldTemplate.Replace("[HorizontalApiNameSmall]", MakeFirstLetterSmall(MatrixInfo?.HorizontalApi));
            fieldTemplate = fieldTemplate.Replace("[VerticalApiNameSmall]", MakeFirstLetterSmall(MatrixInfo?.VerticalApi));

            //ToolTip Specific
            fieldTemplate = fieldTemplate.Replace("[ToolTip]", toolTipNote);
            fieldTemplate = fieldTemplate.Replace("[isToolTipVisible]", toolTipVisibility);

            return fieldTemplate;
        }

        public virtual void SetInternalFields(ComponentType componentType, string componentNameCapital, string QueryBaseTable)
        {
            var result = CodeGeneratorBase.GetNames(Field);
            fieldCapitalName = result.Item1;
            fieldSmallName = result.Item2;
            fieldFileName = result.Item3;

            isLookup = BackendDataTypeId == BackendDataType.DT_Lookup || BackendDataTypeId == BackendDataType.DT_SeedLookup;
            lookupNameCapital = result.Item1.Replace("Id", "");
            lookupNameSmall = MakeFirstLetterSmall(lookupNameCapital);
            lookupFileName = result.Item3.Replace("-Id", "").Replace("-id", "");
            sortName = !string.IsNullOrEmpty(lookupNameCapital) ? lookupNameCapital : fieldCapitalName;
            joinName = !string.IsNullOrEmpty(lookupNameCapital) ? lookupNameCapital : fieldCapitalName;

            ParentTable = string.IsNullOrEmpty(ParentTable)? QueryBaseTable : ParentTable;
            ParentTable = string.IsNullOrEmpty(ParentTable) ? componentNameCapital : ParentTable;
            parentTableSmall = MakeFirstLetterSmall(ParentTable);

            // Set Data Types
            SetBackendDataType();
            SetFrontendDataType(BackendDataTypeId);

            // Set Default Values
            SetBackendDefaultValue(BackendDataTypeId);
            SetFrontendDefaultValue(BackendDataTypeId, frontendDataType);

            uiFormControl = GetUIFormControl(BackendDataTypeId, FormControlTypeId);

            SetReportInfo(componentType);

            SetFilters(BackendDataTypeId, isAggregate);

            SetFieldDefinition(BackendDataTypeId, isLookup);

            SetReportDefinition(IsJoinField, isAggregate);

            SetMatrixDefinition(MatrixInfo);

            SetToolTipDefinition(ToolTipInfo);
        }

        private void SetToolTipDefinition(ToolTipInfo toolTipInfo)
        {
            toolTipNote = ToolTipInfo?.Note ?? string.Empty;
            toolTipVisibility = string.IsNullOrEmpty(toolTipNote)? "style='display:none'" :  string.Empty;
        }

        public void SetFilters(BackendDataType BackendDataTypeId, bool isAggregate)
        {
            var isQueryFilter = (FilterTypeId) != FilterType.None;
            var isLookupFilter = isLookup;

            var isValueStringFilter = isQueryFilter && BackendDataTypeId == BackendDataType.DT_string;
            var isValueNumberFilter = isQueryFilter && FilterTypeId == FilterType.ValueNumberFilter;  // must be set explicitly. FilterType.Default will not set it

            var isRangeNumberFilter = isQueryFilter && !isLookup && (BackendDataTypeId == BackendDataType.DT_int || BackendDataTypeId == BackendDataType.DT_decimal);
            var isRangeDateFilter = isQueryFilter && BackendDataTypeId == BackendDataType.DT_DateTime;
            var isRangeAggregateFilter = isQueryFilter && isAggregate;

            FilterDefinition = isValueStringFilter ? FilterDefinition.ValueString
                : isValueNumberFilter ? FilterDefinition.ValueNumber
                : isLookupFilter ? FilterDefinition.Lookup
                : isRangeAggregateFilter ? FilterDefinition.AggregateRange
                : (isRangeDateFilter || isRangeNumberFilter) ? FilterDefinition.Range
                : FilterDefinition.None;

            backendRangeType = isRangeDateFilter ? "DateRange?" : "NumericRange?";
            filterRangeName = isAggregate ? aggregateName : fieldCapitalName;
            filterLookupName = $"{lookupNameCapital}Id";
            filterValueName = fieldCapitalName;
        }

        public void SetReportInfo(ComponentType componentType)
        {
            var aggregateType = ReportInfo.AggregateTypeId == null ? AggregateType.None : (AggregateType)ReportInfo.AggregateTypeId;
            isAggregate = aggregateType != AggregateType.None;
            aggregateFunction = aggregateType.ToString();
            if (isAggregate)
                aggregateName = MakeFirstLetterSmall($"{aggregateFunction}{fieldCapitalName}");

            if (componentType == ComponentType.Report)
            {
                //if Report, use tableName as suffix to avoid field name conflict between tables in join scenario. if not,like in case of list or matrix, use field name only.
                reportFieldNameCapital = $"{ParentTable}_{fieldCapitalName}";
                reportFieldNameSmall = $"{parentTableSmall}_{fieldCapitalName}";
            }
            else
            {
                reportFieldNameCapital = fieldCapitalName;
                reportFieldNameSmall = fieldSmallName;
            }

            var chartElement = ReportInfo.ChartElementId == null ? ChartElement.None : (ChartElement)ReportInfo.ChartElementId;
            isChartHorizontalField = chartElement == ChartElement.HorizontalField;
            isChartVerticalField = chartElement == ChartElement.VerticalField;
            ChartDefinition = isChartHorizontalField ?
                ChartDefinition.Horizontal
                : isChartVerticalField ?
                ChartDefinition.Vertical
                : ChartDefinition.None;
        }

        private void SetMatrixDefinition(MatrixInfo matrixInfo)
        {
            //this.MatrixDefinition = (!string.IsNullOrEmpty(matrixInfo.ParentApi)
            //                && !string.IsNullOrEmpty(matrixInfo.ParentApi)
            //                && !string.IsNullOrEmpty(matrixInfo.ParentApi)) ?
            //                  MatrixDefinition.Default
            //                : MatrixDefinition.None;

            if (!string.IsNullOrEmpty(matrixInfo.ParentApi))
            {
                var result = CodeGeneratorBase.GetNames(matrixInfo.ParentApi);
                matrixNameCapital = result.Item1;
                matrixNameSmall = result.Item2;
                matrixFileName = result.Item3;
            }
        }

        public void SetBackendDataType()
        {
            var modifiedBackendDataTypeId = BackendDataTypeId == BackendDataType.DT_Lookup ? BackendDataType.DT_long
                  : BackendDataTypeId == BackendDataType.DT_SeedLookup ? BackendDataType.DT_int
                  : BackendDataTypeId == BackendDataType.DT_Default ? (fieldSmallName == "id" ? BackendDataType.DT_long : BackendDataType.DT_string)
                  : BackendDataTypeId;

            BackendDataTypeId = modifiedBackendDataTypeId;
            backendDataType = BackendDataTypeId == BackendDataType.DT_CustomFieldList ? "List<CustomField>" : BackendDataTypeId.ToString().Substring(3);
        }

        public void SetFrontendDataType(BackendDataType BackendDataTypeId)
        {
            frontendDataType = BackendDataTypeId == BackendDataType.DT_bool ? "boolean"
                                     : BackendDataTypeId == BackendDataType.DT_int || BackendDataTypeId == BackendDataType.DT_decimal ? "number"
                                     : BackendDataTypeId == BackendDataType.DT_long ? "string"  // fronend missing long type accuracy, using string
                                     : BackendDataTypeId == BackendDataType.DT_DateTime ? "date"
                                     : BackendDataTypeId == BackendDataType.DT_TabList ? "[]"
                                     : BackendDataTypeId == BackendDataType.DT_CustomFieldList ? "ICustomFieldList"
                                     : BackendDataTypeId == BackendDataType.DT_FieldValidation ? "IFieldValidation"
                                     : BackendDataTypeId == BackendDataType.DT_FormInfo ? "IFormInfo"
                                     : BackendDataTypeId == BackendDataType.DT_ReportInfo ? "IReportInfo"
                                     : BackendDataTypeId == BackendDataType.DT_MatrixInfo ? "IMatrixInfo"
                                     : "string";
        }

        public void SetBackendDefaultValue(BackendDataType BackendDataTypeId)
        {
            switch (BackendDataTypeId)
            {
                case BackendDataType.DT_string:
                    backendDefaultValue = "string.Empty";
                    break;
                case BackendDataType.DT_DateTime:
                    backendDefaultValue = "DateTime.MinValue";
                    break;
                case BackendDataType.DT_long:
                case BackendDataType.DT_int:
                    backendDefaultValue = "0";
                    break;
                case BackendDataType.DT_decimal:
                    backendDefaultValue = "0.0";
                    break;
                case BackendDataType.DT_bool:
                    backendDefaultValue = "false";
                    break;
                case BackendDataType.DT_CustomFieldList:
                    backendDefaultValue = "new List<CustomField>()";
                    break;
                case BackendDataType.DT_FieldValidation:
                    backendDefaultValue = "new FieldValidation()";
                    break;
                case BackendDataType.DT_ToolTipInfo:
                    backendDefaultValue = "new ToolTipInfo()";
                    break;
                case BackendDataType.DT_FormInfo:
                    backendDefaultValue = "new FormInfo()";
                    break;
                case BackendDataType.DT_ReportInfo:
                    backendDefaultValue = "new ReportInfo()";
                    break;
                case BackendDataType.DT_MatrixInfo:
                    backendDefaultValue = "new MatrixInfo()";
                    break;
                default:
                    backendDefaultValue = "string.Empty";
                    break;
            }
        }

        public void SetFrontendDefaultValue(BackendDataType BackendDataTypeId, string frontendDataType)
        {

            frontendDefaultValue = frontendDataType.ToLower() == "boolean" ? "false"
                       : frontendDataType.ToLower() == "number" ? "0"
                       : frontendDataType.ToLower() == "[]" ? "[]"
                       : frontendDataType.ToLower() == "icustomfieldlist" ? "initCustomFieldList()"
                       : frontendDataType.ToLower() == "ifieldvalidation" ? "initFieldValidation()"
                       : frontendDataType.ToLower() == "itooltip" ? "initToolTip()"
                       : frontendDataType.ToLower() == "iforminfo" ? "initFormInfo()"
                       : frontendDataType.ToLower() == "ireportinfo" ? "initReportInfo()"
                       : frontendDataType.ToLower() == "imatrixinfo" ? "initMatrixInfo()"
                       : frontendDataType.ToLower() == "string" && BackendDataTypeId == BackendDataType.DT_long ? "'0'"
                       : "''";
        }

        public void SetReportDefinition(bool IsJoinField, bool isAggregate)
        {
            if (IsJoinField)
                ReportDefinition = ReportDefinition.Join;
            else if (isAggregate)
                ReportDefinition = ReportDefinition.Aggregate;
            else if (IsQueryColumn)
                ReportDefinition = ReportDefinition.QueryColumn;
            else
                ReportDefinition = ReportDefinition.None;
        }

        public void SetFieldDefinition(BackendDataType BackendDataTypeId, bool isLookup)
        {
            switch (BackendDataTypeId)
            {
                case BackendDataType.DT_TabList:
                    FieldDefinition = FieldDefinition.ChildrenList;
                    break;
                case BackendDataType.DT_TabMatrix:
                    FieldDefinition = FieldDefinition.ChildrenMatrix;
                    break;
                case BackendDataType.DT_CustomFieldList:
                    FieldDefinition = FieldDefinition.CustomFieldList;
                    break;
                case BackendDataType.DT_FieldValidation:
                case BackendDataType.DT_ToolTipInfo:
                case BackendDataType.DT_FormInfo:
                case BackendDataType.DT_ReportInfo:
                case BackendDataType.DT_MatrixInfo:
                    FieldDefinition = FieldDefinition.Object;
                    break;
                default:
                    FieldDefinition = isLookup ? FieldDefinition.Lookup : FieldDefinition.Primitive;
                    break;
            }
        }

        public string GetUIFormControl(BackendDataType BackendDataTypeId, FormControlType FormControlTypeId)
        {
            switch ((FormControlType)FormControlTypeId)
            {
                case FormControlType.Default:
                    // set UI Form Control based on BackendDataTypeId
                    switch ((BackendDataType)BackendDataTypeId)
                    {
                        case BackendDataType.DT_string:
                            return "Text";
                        case BackendDataType.DT_decimal:
                            return "TextNumber";
                        case BackendDataType.DT_long:
                            return isLookup ? "Select" : "TextNumber";
                        case BackendDataType.DT_int:
                            return isLookup ? "Select" : "TextNumber";
                        case BackendDataType.DT_DateTime:
                            return "Date";
                        case BackendDataType.DT_bool:
                            return "Checkbox";
                        case BackendDataType.DT_TabList:
                            return "ChildrenList";
                        case BackendDataType.DT_TabMatrix:
                            return "ChildrenMatrix";
                        case BackendDataType.DT_FieldValidation:
                        case BackendDataType.DT_ToolTipInfo:
                        case BackendDataType.DT_FormInfo:
                        case BackendDataType.DT_ReportInfo:
                        case BackendDataType.DT_MatrixInfo:
                            return BackendDataTypeId.ToString().Replace("DT_","");
                        default:
                            return "Text";
                    }

                // set UI Form Control based on FormControlTypeId that is set explicitly by the user.
                case FormControlType.Text:
                    return "Text";
                case FormControlType.TextNumber:
                    return "TextNumber";
                case FormControlType.Select:
                    return "Select";
                case FormControlType.CheckBox:
                    return "Checkbox";
                case FormControlType.Date:
                    return "Date";
                case FormControlType.FormTab:
                    return "Tab";
                default:
                    return "Text";
            }
        }

        public static string MakeFirstLetterSmall(string? input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return input[0].ToString().ToLower() + input.Substring(1);
            }

            return input;
        }
    }
}
