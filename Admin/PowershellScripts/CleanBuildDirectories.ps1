# Define the parent directory and the folders to target
$parentDir = "C:\Bfs_V1\V1\Backend"
$targets = @('bin', 'obj', '.vs')

# Check if the parent directory exists before proceeding
if (Test-Path $parentDir) {
    Write-Host "Starting cleanup in: $parentDir" -ForegroundColor Cyan
    
    # Get all directories matching the target names within the tree
    Get-ChildItem -Path $parentDir -Recurse -Directory | Where-Object { $targets -contains $_.Name } | ForEach-Object {
        Write-Host "Deleting: $($_.FullName)" -ForegroundColor Yellow
        Remove-Item -Path $_.FullName -Recurse -Force
    }
    
    Write-Host "Cleanup complete." -ForegroundColor Green
} else {
    Write-Error "The directory '$parentDir' does not exist."
}