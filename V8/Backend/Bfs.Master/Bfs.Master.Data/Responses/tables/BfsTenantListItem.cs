using Bfs.Core.ObjectFields;

namespace Bfs.Master.Data
{
    public class BfsTenantListItem
    {
        public string AutoCompleteName { get; set; }
        public string LookupName { get; set; }

        public string DbConnection { get; set; }
        public long Id { get; set; }
        public string Notes { get; set; }
        //public List<CustomField> CustomFields { get; set; }
        public string CustomFields { get; set; }

        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Logo { get; set; }
        public string Theme { get; set; }

    }
}