using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bfs.Core.Services.Deployment
{
    public interface IDeploymantInfoAzure : IDeploymantInfoBase
    {
        // Azure specific
        public string PublishProfilePath { get; set; }
        public string AppService { get; set; }
        public string ResourceGroup { get; set; }
    }
}
