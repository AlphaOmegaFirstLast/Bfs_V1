using Admin.App.Constants;
using System;

namespace Admin.App
{
    public class BfsSystemActionEntity: IActionEntity
    {
        public long TenantId { get; set; }
        public bool IsDeleted { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        public string ActionTemplate { get; set; }

        public ActionType ActionTypeId { get; set; }
        public WriterType WriterTypeId { get; set; }
        public string MatchProperty { get; set; }
        public string MatchValues { get; set; }
        public string Notes { get; set; }
    }
}