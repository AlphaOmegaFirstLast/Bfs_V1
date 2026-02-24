using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bfs.Core.Services.Deployment
{
    public interface IDeploymantInfoBase
    {
        public string ScriptFile { get; set; }
        public string SourceProject { get; set; }
        public string SourcePath { get; set; }
        public string PublishPath { get; set; }
        public string TargetVirtualDir { get; set; }  // required in publishing angular

        public string Config { get; set; }
        public string EnvironmentValue { get; set; }
    }
}
