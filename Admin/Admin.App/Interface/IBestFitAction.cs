using Admin.App.Constants;

namespace Admin.App
{
    public interface IBestFitAction
    {
        public long TenantId { get; set; }
        public bool IsDeleted { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public long BfsComponentId { get; set; }
        public string ActionTemplate { get; set; }

        public ActionType ActionType { get; set; }
        public ActionLocation ActionLocation { get; set; }
        public ActionSource ActionSource { get; set; }
        public WriterType WriterType { get; set; }
        public string MatchProperty { get; set; }
        public string[] MatchValues { get; set; }
        public string Notes { get; set; }
    }
}