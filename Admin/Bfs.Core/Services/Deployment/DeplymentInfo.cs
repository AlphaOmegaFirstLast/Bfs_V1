using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace Bfs.Core.Services.Deployment
{
    internal class DeplymentInfo
    {
        public long TenantId { get; set; }
        public bool IsDeleted { get; set; }
        public long Id { get; set; }
        public long SystemInfoId { get; set; }
        public string SystemName { get; set; }

        public string ScriptFile { get; set; }

        public string Project { get; set; }
        public string ProjectType { get; set; }
        public string SourceProject { get; set; }
        public string SourcePath { get; set; }
        public string PublishPath { get; set; }

        public string Config { get; set; }
        public string EnvironmentValue { get; set; }
        public string TargetDeployApiVirtualFolder { get; set; }

        // Azure specific
        public string PublishProfilePath { get; set; }
        public string AppService { get; set; }
        public string ResourceGroup { get; set; }

        // Local Specific
        public string WebSite { get; set; }
        public string AppPoolName { get; set; }
        public string Port { get; set; }
        public string IsHttpsRequired { get; set; }

        public void SetDelpoyAzureApiInfo()
        {
            SourceProject = @"D:\Bfs\Backend\Bfs.StockEx\Bfs.StockEx.Api.csproj";
            SourcePath = @"D:\Bfs\Backend\Bfs.StockEx\Bfs.StockEx.Api";
            PublishPath = @"D:\publish";
            Config = "Debug";
            EnvironmentValue = "/p:EnvironmentName=Staging";

            PublishProfilePath = @"D:\Bfs\Backend\Bfs.StockEx\Bfs.StockEx.Api\Properties\PublishProfiles\StagingStockExBackendApi.PublishSettings";
            AppService = "StagingStockExBackendApi";
            ResourceGroup = "StockEx_ResourceGroup";
            TargetDeployApiVirtualFolder = "/";

            ScriptFile = @"D:\Bfs\Admin\Scripts\PublishAzure.ps1";

        }
        public void SetDelpoyAzureAuthInfo()
        {
            SourceProject = @"D:\Bfs\Frontend\Auth\StockEx.Auth.App\StockEx.Auth.App.csproj";
            SourcePath = @"D:\Bfs\Frontend\Auth\StockEx.Auth.App";
            PublishPath = @"D:\publishAuth";
            Config = "Debug";
            EnvironmentValue = "/p:EnvironmentName=Staging";

            PublishProfilePath = @"D:\Bfs\Frontend\Auth\StockEx.Auth.App\Properties\PublishProfiles\StagingStockExFrontendApi.PublishSettings";
            AppService = "StagingStockExFrontendApi";
            ResourceGroup = "StockEx_ResourceGroup";
            TargetDeployApiVirtualFolder = "auth";

            ScriptFile = @"D:\Bfs\Admin\Scripts\PublishAzure.ps1";

        }
        public void SetDelpoyAzureMainInfo()
        {
            SourceProject = @"D:\Bfs\Frontend\Main";
            SourcePath = @"D:\Bfs\Frontend\Main";
            PublishPath = @"D:\publishFront";
            Config = "Debug";
            EnvironmentValue = "/p:EnvironmentName=Staging";

            PublishProfilePath = @"D:\Bfs\Frontend\Auth\StockEx.Auth.App\Properties\PublishProfiles\StagingStockExFrontendApi.PublishSettings";
            AppService = "StagingStockExFrontendApi";
            ResourceGroup = "StockEx_ResourceGroup";
            TargetDeployApiVirtualFolder = "main";

            ScriptFile = @"D:\Bfs\Admin\Scripts\PublishAzure.ps1";

        }

        public void SetDelpoyLocalApiInfo()
        {
            SystemName = "StockEx.Api ";
            WebSite = "stockexapi.localhost";
            SourceProject = @"D:\Bfs\Backend\Bfs.StockEx\Bfs.StockEx.Api.csproj";
            SourcePath = @"D:\Bfs\Backend\Bfs.StockEx\Bfs.StockEx.Api";
            PublishPath = @"D:\publish\";
            IsHttpsRequired = "false";
            Config = "Debug";
            EnvironmentValue = "/p:EnvironmentName=Development";
            AppPoolName = "stockexapi.localhost";
            TargetDeployApiVirtualFolder = "";
            ScriptFile = @"D:\Bfs\Admin\Scripts\PublishLocal.ps1";
            Port = "80";
        }

        public void SetDelpoyLocalAuthInfo()
        {
            SystemName = "StockEx.Auth.App";
            WebSite = "stockexfront.localhost";
            SourceProject = @"D:\Bfs\Frontend\Auth\StockEx.Auth.App\StockEx.Auth.App.csproj";
            SourcePath = @"D:\Bfs\Frontend\Auth\StockEx.Auth.App";
            PublishPath = @"D:\publishFront\";
            IsHttpsRequired = "false";
            Config = "Debug";
            EnvironmentValue = "/p:EnvironmentName=Development";
            AppPoolName = "stockexfront.localhost";
            TargetDeployApiVirtualFolder = "auth";
            ScriptFile = @"D:\Bfs\Admin\Scripts\PublishLocal.ps1";
            Port = "80";
        }

        public void SetDelpoyLocalMainInfo()
        {
            SystemName = "StockEx.Angular";
            WebSite = "stockexfront.localhost";
            SourceProject = @"D:\Bfs\Frontend\Main";
            SourcePath = @"D:\Bfs\Frontend\Main";
            PublishPath = @"D:\publishFront\";
            IsHttpsRequired = "false";
            Config = "Debug";
            EnvironmentValue = "/p:EnvironmentName=Development";
            AppPoolName = "stockexfront.localhost";
            TargetDeployApiVirtualFolder = "main";
            ScriptFile = @"D:\Bfs\Admin\Scripts\PublishLocal.ps1";
            Port = "80";
        }
    }
}
