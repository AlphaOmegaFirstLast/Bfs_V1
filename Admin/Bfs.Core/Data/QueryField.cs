using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bfs.Core.Data
{
    public class QueryField
    {
        public string ComponentName { get; set; }
        public string FieldName { get; set; }
        public string DbName { get; set; }
        public string QueryName { get; set; }
        public bool IsAggregare { get; set; } = false;
    }
}
