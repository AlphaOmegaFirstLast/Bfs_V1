using System.Diagnostics;
using Bfs.Core.Interfaces;

namespace Bfs.Core.Services.Deployment
{
    public class DeploymentManager
    {
        public static void PublishToLocal(IDeploymantBase info)
        {
            var args = $" -ExecutionPolicy Bypass -File \"{info.ScriptFile}\" ";

            args += $" -SourceProject  \"{info.SourceProject}\" ";
            args += $" -SourcePath \"{info.SourcePath}\" ";
            args += $" -PublishPath \"{info.PublishPath}\" ";
            args += $" -TargetVirtualDir \"{info.TargetVirtualDir}\" ";  // required in publishing angular

            args += $" -Config \"{info.Config}\" ";
            args += $" -EnvironmentValue \"{info.EnvironmentValue}\" ";

            RunPowershell(args);
        }

        public static void DeployToLocal(IDeploymantLocal info)
        {
            var args = $" -ExecutionPolicy Bypass -File \"{info.ScriptFile}\" ";

            args += $" -PublishPath \"{info.PublishPath}\" ";
            args += $" -IsHttpsRequired  \"{info.IsHttpsRequired}\" ";
            args += $" -WebSite \"{info.WebSite}\" ";
            args += $" -AppPoolName \"{info.AppPoolName}\" ";
            args += $" -TargetDeployVirtualFolder \"{info.TargetVirtualDir}\" ";

            RunPowershell(args);
        }

        public static void DeployToAzure(IDeploymantAzure info)
        {
            var args = $" -ExecutionPolicy Bypass -File \"{info.ScriptFile}\" ";

            args += $" -PublishPath \"{info.PublishPath}\" ";
            args += $" -PublishProfilePath \"{info.PublishProfilePath}\" ";
            args += $" -ResourceGroup \"{info.ResourceGroup}\" ";
            args += $" -AppService \"{info.AppService}\" ";
            args += $" -TargetDeployVirtualFolder \"{info.TargetVirtualDir}\" ";

            args = SetAzureKeys(args, new AzureAcount());

            RunPowershell(args);
        }

        public static string SetAzureKeys(string args, AzureAcount azureAcount)
        {
            args += $" -AzureAccountName \"{azureAcount.azureAccountName.Trim()}\" ";
            args += $" -AzureAccountPassword \"{azureAcount.azureAccountPassword.Trim()}\" ";
            args += $" -TenantId \"{azureAcount.tenantId.Trim()}\" ";
            args += $" -SubscriptionId \"{azureAcount.subscriptionId.Trim()}\" ";

            return args;
        }

        public static void RunPowershell(string args)
        {
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

