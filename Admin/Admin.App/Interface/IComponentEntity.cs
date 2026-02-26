
using Admin.App.Constants;

namespace Admin.App
{
    public interface IComponentEntity
    {
        public long TenantId { get; set; }
        public bool IsDeleted { get; set; }
        public long Id { get; set; }
        public long BfsSystemId { get; set; }
        public bool IsSoftDelete { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public DataType DataTypeId { get; set; }
        public string MenuName { get; set; }
        public string MenuPlaceHolder { get; set; }
        public string Notes { get; set; }
        public string InterfaceRequired { get; set; }
        public string QueryBaseTable { get; set; }
        //    public ReportType ReportTypeId { get; set; } = 0;  //ToDo add this property to the interface if needed, but for now it's only used in some components like reports, so we can keep it in the entity for now.
    }
}