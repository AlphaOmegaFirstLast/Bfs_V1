param(
    [string]$PublishPath,      
    [string]$isHttpsRequired  ,
    [string]$WebSite          ,
    [string]$AppPoolName      ,
    [string]$TargetVirtualDir,
    [int]$Port
	)  

$ArgumentArray = @(
    $PublishPath,
    $isHttpsRequired,
    $WebSite,
    $AppPoolName,
    $TargetVirtualDir,
    $Port
)
#--------------------------------------------

Import-Module -Name WebAdministration
#--------------------------------------------

if ($PSHOME -like "*SysWOW64*")
{
  Write-Warning "Restarting this script under 64-bit Windows PowerShell."

  # Restart this script under 64-bit Windows PowerShell.
  #   (\SysNative\ redirects to \System32\ for 64-bit mode)

  & (Join-Path ($PSHOME -replace "SysWOW64", "SysNative") powershell.exe) -File `
    (Join-Path $PSScriptRoot $MyInvocation.MyCommand)  @ArgumentArray

  # Exit 32-bit script.

  Exit $LastExitCode
}
#--------------------------------------------

function writeMsg([String] $msg)
{
    Write-Host ' '
    Write-Host '-------------------------------------------- '  $msg
    Write-Host ' '
}
#--------------------------------------------

function EnsureAppPool($AppPoolName) 
{
    if (-not (Get-WebAppPoolState -Name $AppPoolName -ErrorAction SilentlyContinue)) {

        writeMsg "Creating App Pool: $AppPoolName"
        New-WebAppPool -Name $AppPoolName
        Set-ItemProperty "IIS:\AppPools\$AppPoolName" -Name "managedRuntimeVersion" -Value ""
        Set-ItemProperty "IIS:\AppPools\$AppPoolName" -Name "processModel.identityType" -Value "ApplicationPoolIdentity"

    } 
    else 
    {
        writeMsg "App Pool '$AppPoolName' already exists"
    }
}
#--------------------------------------------

function EnsureSite($PublishPath,$WebSite, $AppPoolName, $port) 
{
    if (-not (Get-Website -Name $WebSite -ErrorAction SilentlyContinue))
    {
        writeMsg "Creating IIS Site: $WebSite"
        New-WebAppPool -Name $WebSite
        New-Website -Name $WebSite -Port $port -PhysicalPath $PublishPath -ApplicationPool $AppPoolName -HostHeader $WebSite
    } 
    else 
    {
        writeMsg "Site '$WebSite' already exists. Updating path and app pool."
        Set-ItemProperty "IIS:\Sites\$WebSite" -Name "physicalPath" -Value $PublishPath
        Set-ItemProperty "IIS:\Sites\$WebSite" -Name "applicationPool" -Value $AppPoolName
    }
}
#--------------------------------------------

function EnsureVirtualDir($PublishPath,$WebSite, $AppPoolName, $port, $TargetVirtualDir) 
{
    # Add virtual directory if it doesn't exist
    $var = -not [string]::IsNullOrEmpty($TargetVirtualDir)
    if ($var)
    {
        $vdirPath = "IIS:\Sites\$WebSite\$TargetVirtualDir"

        if (-not (Test-Path $TargetVirtualDir)) {
            writeMsg "Creating virtual directory '$TargetVirtualDir' under site '$WebSite'"

           read-host

            $SubFolder = "$TargetVirtualDir"  # Or use a timestamp, branch name, etc.
            $PublishPathWithSub = Join-Path -Path $PublishPath -ChildPath $SubFolder

            #New-WebVirtualDirectory  -Path "IIS:\Sites\$WebSite\" -Site "$WebSite/$TargetVirtualDir" -Name $TargetVirtualDir  -PhysicalPath $PublishPathWithSub
            ConvertTo-WebApplication   "IIS:\Sites\$WebSite\$TargetVirtualDir" 
            Set-ItemProperty -Path "IIS:\Sites\$WebSite\$TargetVirtualDir" -Name "applicationPool" -Value $AppPoolName 
            Read-Host
        } 
        else
        {
            writeMsg "Virtual directory '$TargetVirtualDir' already exists. Skipping creation."
        }
    }
}
#--------------------------------------------

function DeployLocal()
{
     EnsureAppPool $AppPoolName
     EnsureSite $PublishPath $WebSite $AppPoolName $Port 
     EnsureVirtualDir $PublishPath $WebSite $AppPoolName $Port $TargetVirtualDir

     writeMsg "Deployment Complete. Site $WebSite' is live on http://localhost:$Port"
}
#--------------------------------------------

DeployLocal
#C:\Bfs_V1\Admin\PowershellScripts\DeployLocal.ps1 -PublishPath 'c:\publish\frontend\main' -AppPoolName 'bfsFrontend'  -WebSite 'bfsFrontend.localhost' -TargetVirtualDir 'main'  -Port '80'







