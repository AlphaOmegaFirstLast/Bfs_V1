using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bfs.Core.Services.Deployment
{
    public interface IDeploymantInfoLocal : IDeploymantInfoBase
    {
        // Local Specific
        public bool IsHttpsRequired { get; set; }
        public string WebSite { get; set; }
        public string AppPoolName { get; set; }
        public string Port { get; set; }
    }
}
