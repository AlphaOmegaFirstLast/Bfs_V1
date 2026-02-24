 
 param(
    [int]$ProjectType,
    [string]$AzureAccountName,
    [string]$AzureAccountPassword,
    [string]$TenantId,
    [string]$SubscriptionId,
    [string]$SourceProject,
    [string]$SourcePath,
    [string]$PublishPath,
    [string]$Config,
    [string]$EnvironmentValue ,
    [string]$PublishProfilePath,
    [string]$AppService ,
    [string]$ResourceGroup,
    [string]$TargetDeployVirtualFolder)
    

$ArgumentArray = @(
    $ProjectType
    $AzureAccountName,
    $AzureAccountPassword,
    $TenantId,
    $SubscriptionId,
    $SourceProject,
    $SourcePath,
    $PublishPath ,
    $Config,
    $EnvironmentValue,
    $PublishProfilePath,
    $AppService,
    $ResourceGroup,
    $TargetDeployVirtualFolder
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
#--------------------------------------------

function RestoreNugetPackages()
{
    Write-Host "Restoring Nuget Packages:" + 'D:\StockEx\Backend\StockEx.Api.sln'

    $nugetRestore = '"' + 'D:\StockEx\Backend' + '\.nuget\nuget.exe" restore "' + 'D:\StockEx\Backend\StockEx.Api.sln' + '"'
    Write-Host $nugetRestore
    $nugetRestore = "cmd.exe /C " + $nugetRestore
    Invoke-Expression -Command:$nugetRestore

    dotnet restore 'D:\StockEx\Backend\StockEx.Api.sln' # -v detailed
}
#--------------------------------------------

function PublishLocallyApi()
{
    Write-Host 'Publish Path'  $PublishPath

    CleanDirectory $PublishPath
    writeMsg ('Publish Api:   ' + $SourceProject + '     to      ' + $PublishPath)
    dotnet publish $SourceProject -c $Config  -o $PublishPath  $EnvironmentValue
}
#--------------------------------------------

function PublishLocallyAuth()
{
    
    $SubFolder = "$TargetDeployVirtualFolder"  # Or use a timestamp, branch name, etc.
    $PublishPathWithSub = Join-Path -Path $PublishPath -ChildPath $SubFolder

     if (Test-Path -Path $PublishPathWithSub )
     {
       Get-ChildItem -Path $PublishPathWithSub -Recurse | Remove-Item -force -recurse
       Remove-Item -Path $PublishPathWithSub  -Force  -Recurse
     }
     New-Item -ItemType directory -Path $PublishPathWithSub

     dotnet publish $SourceProject -c $Config -o $PublishPathWithSub $EnvironmentValue
}
#--------------------------------------------

function PublishLocallyMain()
{   
    $SubFolder = "$TargetDeployVirtualFolder"  # Or use a timestamp, branch name, etc.
    $PublishPathWithSub = Join-Path -Path $PublishPath -ChildPath $SubFolder

     if (Test-Path -Path $PublishPathWithSub )
     {
       Get-ChildItem -Path $PublishPathWithSub -Recurse | Remove-Item -force -recurse
       Remove-Item -Path $PublishPathWithSub  -Force  -Recurse
     }
     New-Item -ItemType directory -Path $PublishPathWithSub

     cd $SourcePath

     # Run ng build with configuration and output path
     ng build --configuration=development --base-href=/main/
     Copy-Item -Path "dist/inspinia-ng/browser\*" -Destination $PublishPathWithSub -Recurse -Force 
}
#--------------------------------------------

function DeployProject () 
{
   az webapp stop --name $AppService --resource-group $ResourceGroup
  
   Write-Host 'Publish path'  $PublishPath  'profile' $PublishProfilePath
    $var = -not [string]::IsNullOrEmpty($TargetDeployVirtualFolder)
    if ($var)
    {
       $SubFolder = "$TargetDeployVirtualFolder"  # Or use a timestamp, branch name, etc.
       $PublishPathWithSub = Join-Path -Path $PublishPath -ChildPath $SubFolder
       $command = 'WAWSDeploy "' + $PublishPathWithSub + '" "' + $PublishProfilePath + '" /v /au /t '+ $TargetDeployVirtualFolder 
    }
    else
    {
       $command = 'WAWSDeploy "' + $PublishPath + '" "' + $PublishProfilePath + '" /v /au /t '+ $TargetDeployVirtualFolder
    }   
    
    Write-Output $command
    read-host
    read-host
    Invoke-Expression -Command:$command

    az webapp start --name $AppService --resource-group $ResourceGroup
 }
#--------------------------------------------

function CleanDirectory 
{  
    if (Test-Path -Path $PublishPath)
    {
       Get-ChildItem -Path $PublishPath -Recurse | Remove-Item -force -recurse
       Remove-Item -Path $PublishPath  -Force  -Recurse
    }
    New-Item -ItemType directory -Path $PublishPath
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
  PublishLocallyApi
  PublishLocallyAuth
  PublishLocallyMain
  LoginToAzure
  DeployProject 
}

Main





