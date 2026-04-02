using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bfs.Core.Interfaces
{
    public interface IDeploymentLocal : IDeploymentBase
    {
        // Local Specific
        public bool IsHttpsRequired { get; set; }
        public string WebSite { get; set; }
        public string AppPoolName { get; set; }
        public string Port { get; set; }
    }
}
