 
 param(
    [string]$AzureAccountName,
    [string]$AzureAccountPassword,
    [string]$TenantId,
    [string]$SubscriptionId,
	
    [string]$PublishPath,
    [string]$TargetDeployVirtualFolder
	
    [string]$PublishProfilePath,
    [string]$AppService ,
    [string]$ResourceGroup
	)
    
$ArgumentArray = @(
    $AzureAccountName,
    $AzureAccountPassword,
    $TenantId,
    $SubscriptionId,
	
    $PublishPath ,
    [string]$TargetDeployVirtualFolder
	
    $PublishProfilePath,
    $AppService,
    $ResourceGroup
)
#--------------------------------------------

foreach ($arg in $ArgumentArray) {
    Write-Host "Deploying with argument: $arg"
}
#--------------------------------------------

if ($PSHOME -like "*SysWOW64*")
{
  Write-Warning "Restarting this script under 64-bit Windows PowerShell."

  # Restart this script under 64-bit Windows PowerShell.
  #   (\SysNative\ redirects to \System32\ for 64-bit mode)

  & (Join-Path ($PSHOME -replace "SysWOW64", "SysNative") powershell.exe) -File `
    (Join-Path $PSScriptRoot $MyInvocation.MyCommand)@ArgumentArray

  # Exit 32-bit script.

  Exit $LastExitCode
}
#--------------------------------------------

foreach ($arg in $ArgumentArray) {
    Write-Host "Deploying with argument: $arg"
}
#--------------------------------------------

import-module -name WebAdministration
#--------------------------------------------

function LoginToAzure()
{
    Write-Host 'password' $azureAccountPassword 'user' $azureAccountName 'tenant' $tenantId 
    $azurePassword = ConvertTo-SecureString $azureAccountPassword -AsPlainText -Force   
    $psCred = New-Object System.Management.Automation.PSCredential($azureAccountName, $azurePassword)
              Connect-AzAccount -ServicePrincipal -Credential $psCred -Tenant $tenantId 
}
#'--------------------------------------------

function DeployProject () {

   az webapp stop --name $AppService --resource-group $ResourceGroup
  
    Write-Host 'Publish path'  $PublishPath  'profile' $PublishProfilePath
    $command = 'WAWSDeploy "' + $PublishPath + '" "' + $PublishProfilePath + '" /v /au /t '+ $TargetDeployVirtualFolder 
    Write-Output $command
    read-host
    Invoke-Expression -Command:$command
   
    az webapp start --name $AppService --resource-group $ResourceGroup
 }
#--------------------------------------------

function writeMsg([String] $msg)
{
    Write-Host ' '
    Write-Host '-------------------------------------------- '  $msg
    Write-Host ' '
}
#--------------------------------------------

function Main()
{
  LoginToAzure
  DeployProject 
}

Main





