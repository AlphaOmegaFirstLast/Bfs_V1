using System.Diagnostics;

namespace Bfs.Core.Services.Deployment
{
    public class AzureApiDeployment
    {
        public string ScriptPath { get; set; } = string.Empty;

        public string AzureAccountName { get; set; } = string.Empty;
        public string AzureAccountPassword { get; set; } = string.Empty;
        public string TenantId { get; set; } = string.Empty;
        public string SubscriptionId { get; set; } = string.Empty;

        public string SourceProject { get; set; } = string.Empty;
        public string SourcePath { get; set; } = string.Empty;
        public string PublishPath { get; set; } = string.Empty;
        public string Config { get; set; } = string.Empty;
        public string EnvironmentValue { get; set; } = string.Empty;
        public string PublishProfilePath { get; set; } = string.Empty;
        public string AppService { get; set; } = string.Empty;
        public string ResourceGroup { get; set; } = string.Empty;
        public string TargetDeployVirtualFolder { get; set; } = string.Empty;

        public AzureApiDeployment (dynamic azureDeploymentEntity)  //todo instead of dynamic use DeploymentAzureEntity

        {

            //should read from azureDeploymentEntity
            SourceProject = @"C:\Bfs\Backend\Bfs.StockEx\Bfs.StockEx.Api.csproj";
           SourcePath = @"C:\Bfs\Backend\Bfs.StockEx\Bfs.StockEx.Api";
           PublishPath = @"D:\publish";
           Config = "Debug";
           EnvironmentValue = "/p:EnvironmentName=Staging";

           PublishProfilePath = @"C:\Bfs\Backend\Bfs.StockEx\Bfs.StockEx.Api\Properties\PublishProfiles\StagingStockExBackendApi.PublishSettings";
           AppService = "StagingStockExBackendApi";
           ResourceGroup = "StockEx_ResourceGroup";
           TargetDeployVirtualFolder = "/";

           ScriptPath = @"C:\Bfs\Admin\Scripts\PublishAzure.ps1";
        }

        public  void DoDeploy()
        {
            AzureAcount constant = new AzureAcount(); // should read from Azure key value secret

            AzureAccountName = constant.azureAccountName;
            AzureAccountPassword = constant.azureAccountPassword;
            TenantId = constant.tenantId;
            SubscriptionId = constant.subscriptionId;

            var args = $" -ExecutionPolicy Bypass -File \"{ScriptPath}\" ";

            args += $" -AzureAccountName \"{AzureAccountName.Trim()}\" ";
            args += $" -AzureAccountPassword \"{AzureAccountPassword.Trim()}\" ";
            args += $" -TenantId \"{TenantId.Trim()}\" ";
            args += $" -SubscriptionId \"{SubscriptionId.Trim()}\" ";

            args += $" -SourceProject  \"{SourceProject}\" ";
            args += $" -SourcePath \"{SourcePath}\" ";
            args += $" -PublishPath \"{PublishPath}\" ";
            args += $" -Config \"{Config}\" ";
            args += $" -EnvironmentValue \"{EnvironmentValue}\" ";
            args += $" -PublishProfilePath \"{PublishProfilePath}\" ";
            args += $" -TargetDeployVirtualFolder \"{TargetDeployVirtualFolder}\" ";
            args += $" -AppService \"{AppService}\" ";
            args += $" -ResourceGroup \"{ResourceGroup}\" ";

            var psi = new ProcessStartInfo
            {
                FileName = @"C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe ",

                Arguments = args,
                UseShellExecute = true,
                RedirectStandardOutput = false,
                RedirectStandardError = false,
                CreateNoWindow = false,
                Verb = "runas",
                WindowStyle = ProcessWindowStyle.Normal
            };
            Process.Start(psi);
        }
    }
}