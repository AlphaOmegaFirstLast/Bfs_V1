
namespace Admin.App
{
    public interface IManualCodeEntity
    {
        public long TenantId { get; set; }

        public bool IsDeleted { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string StartTemplate { get; set; }
        public string EndTemplate { get; set; }
        public string Code { get; set; }
        public string Notes { get; set; }

        public long BfsSystemId { get; set; }
      
        public long BfsComponentId { get; set; }
    }
}

