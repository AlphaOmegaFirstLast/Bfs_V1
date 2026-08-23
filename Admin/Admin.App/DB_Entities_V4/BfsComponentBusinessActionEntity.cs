using Admin.App.Constants;

namespace Admin.App
{
    public class BfsComponentBusinessActionEntity
    {
        public long TenantId { get; set; }
        public bool IsDeleted { get; set; }
        public long Id { get; set; }
        public long BfsComponentId { get; set; }
        public long BusinessActionId { get; set; }
        public ActionLocation ActionLocationId { get; set; }
    }
}