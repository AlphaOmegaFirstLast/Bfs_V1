namespace Admin.App
{
    public class BfsManualCodeEntity : IManualCodeEntity
    {
        public long TenantId { get; set; }

        public bool IsDeleted { get; set; } = false;
        public long Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string StartTemplate { get; set; } = string.Empty;
        public string EndTemplate { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;

        public long BfsSystemId { get; set; } = 0;
       
        public long BfsComponentId { get; set; } = 0;
    }
}