param(
    [string]$SourceProject    , 
    [string]$SourcePath       ,
    [string]$PublishPath      , #publish\backend\projectName || publish\frontend\auth
    [string]$Config           ,
    [string]$EnvironmentValue 
    )  

$ArgumentArray = @(
    $SourceProject,
    $SourcePath,
    $PublishPath,
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

function writeMsg([String] $msg)
{
    Write-Host ' '
    Write-Host '-------------------------------------------- '  $msg
    Write-Host ' '
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

function PublishApi()
{
     CleanDirectory ($PublishPath)
     cd $SourcePath
     $SourceProjectFile = Join-Path -Path $SourcePath -ChildPath $SourceProject

     # Run ng build with configuration and output path
     dotnet publish $SourceProjectFile -c $Config -o $PublishPath /p:EnvironmentName=$EnvironmentValue

     writeMsg "Publish Api Complete. "
}
#--------------------------------------------

PublishApi 

#C:\Bfs_V1\Admin\PowershellScripts\PublishApi.ps1 -SourcePath 'C:\Bfs\V1\Backend\Bfs.XXX\Bfs.XXX.Api' -SourceProject 'Bfs.XXX.Api.csproj' -PublishPath 'c:\publish\backend\XXX' -Config 'debug'  -EnvironmentValue 'development'




