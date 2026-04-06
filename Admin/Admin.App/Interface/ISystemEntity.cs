using Admin.App.Constants;

namespace Admin.App
{
    public interface ISystemEntity
    {
        public long TenantId { get; set; }
        public bool IsDeleted { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsMaster { get; set; }
        public SystemTemplateType SystemTemplateId { get; set; }
        public string DbPrefix { get; set; }
        public string BasePortNumber { get; set; }
        public string Notes { get; set; }
    }
}