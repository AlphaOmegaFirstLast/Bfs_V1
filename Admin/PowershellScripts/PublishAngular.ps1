param(
    [string]$SourcePath       ,
    [string]$PublishPath      , #publish\frontend\main
    [string]$TargetVirtualDir ,
    [string]$Config           ,
    [string]$EnvironmentValue 
    )  

$ArgumentArray = @(
    $SourcePath,
    $PublishPath,
    $TargetVirtualDir,
    $Config,
    $EnvironmentValue
)
#-------------------------------------------------------

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
#-------------------------------------------------------

function CleanDirectory ([String] $dir)
{   
    if (Test-Path -Path $PublishPath )
    {
       Get-ChildItem -Path $PublishPath -Recurse | Remove-Item -force -recurse
       Remove-Item -Path $PublishPath  -Force  -Recurse
    }
    New-Item -ItemType directory -Path $PublishPath
}
#-------------------------------------------------------

function PublishAngular()
{
     CleanDirectory ($PublishPath)
     cd $SourcePath

     # Run ng build with configuration and output path
     $Base = "/" + $TargetVirtualDir  +"/"
     ng build --configuration=$Config   --base-href=$Base   --output-path=$PublishPath 

     $angularBrowserPath = $PublishPath + "/browser\*"
     Copy-Item -Path $angularBrowserPath -Destination $PublishPath -Recurse -Force

     writeMsg "Publish Angular Complete. "
}
#-------------------------------------------------------
function writeMsg([String] $msg)
{
    Write-Host ' '
    Write-Host '-------------------------------------------- '  $msg
    Write-Host ' '
}

PublishAngular 

#C:\Bfs_V1\Admin\PowershellScripts\PublishAngular.ps1 -SourcePath 'C:\Bfs_V1\V1\Frontend\main' -PublishPath 'c:\publish\frontend\main' -Config 'development'




