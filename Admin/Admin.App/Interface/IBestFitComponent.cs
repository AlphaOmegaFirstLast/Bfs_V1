
using Admin.App.Constants;

namespace Admin.App
{
    public interface IBestFitComponent
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
        public string QueryBaseTable { get; set; }
    }
}