using Admin.App.Constants;

namespace Admin.App
{
    public interface IComponentActionEntity : IActionEntity
    {
        public long BfsComponentId { get; set; }
        public ActionLocation ActionLocationId { get; set; }
        public ActionSource ActionSourceId { get; set; }
    }
}