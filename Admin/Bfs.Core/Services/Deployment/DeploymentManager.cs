using System.Diagnostics;

namespace Bfs.Core.Services.Deployment
{
    public class DeploymentManager
    {
        public static void DeployLocalApi(DeploymentLocalEntity vars)
        {

            var args = $" -ExecutionPolicy Bypass -File \"{vars.ScriptFile.Trim()}\" ";

            args += $" -ProjectType {vars.ProjectType} ";
            args += $" -SourceProject {vars.SourceProject.Trim()} ";
            args += $" -SourcePath {vars.SourcePath.Trim()} ";
            args += $" -PublishPath {vars.PublishPath.Trim()} ";

            args += $" -Config {vars.Config.Trim()} ";
            args += $" -EnvironmentValue {vars.EnvironmentValue.Trim()} ";
            args += $" -TargetDeployVirtualFolder {vars.TargetVirtualFolder.Trim()} ";

            args += $" -Project {vars.Project.Trim()} ";
            args += $" -WebSite {vars.WebSite.Trim()} ";
            args += $" -AppPoolName {vars.AppPoolName.Trim()} ";
            args += $" -Port {vars.Port} ";
            args += $" -isHttpsRequired {vars.IsHttpsRequired.Trim()} ";

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

        public static void DeployAzureApi(AzureAcount azureAcount, DeploymentAzureEntity vars)
        {
            var args = $" -ExecutionPolicy Bypass -File \"{vars.ScriptFile}\" ";

            args += $" -AzureAccountName \"{azureAcount.azureAccountName.Trim()}\" ";
            args += $" -AzureAccountPassword \"{azureAcount.azureAccountPassword.Trim()}\" ";
            args += $" -TenantId \"{azureAcount.tenantId.Trim()}\" ";
            args += $" -SubscriptionId \"{azureAcount.subscriptionId.Trim()}\" ";

            args += $" -ProjectType {vars.ProjectType} ";
            args += $" -SourceProject  \"{vars.SourceProject}\" ";
            args += $" -SourcePath \"{vars.SourcePath}\" ";
            args += $" -PublishPath \"{vars.PublishPath}\" ";

            args += $" -Config \"{vars.Config}\" ";
            args += $" -EnvironmentValue \"{vars.EnvironmentValue}\" ";
            args += $" -TargetDeployVirtualFolder \"{vars.TargetDeployApiVirtualFolder}\" ";

            args += $" -PublishProfilePath \"{vars.PublishProfilePath}\" ";
            args += $" -AppService \"{vars.AppService}\" ";
            args += $" -ResourceGroup \"{vars.ResourceGroup}\" ";

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

