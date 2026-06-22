Import-Module .\FunctionModule.psm1 -Force

# NuGet pack/push is only allowed on the 'release' branch.
if (-not (Assert-ReleaseBranch)) { pause; return }

# Package list is shared with the release CI workflow (single source of truth).
$names = Get-PackageIds

$result = NugetPack @names
if($result)
{
    # Publish .nupkg to GitHub Releases (grouped by minor: assets attached to vM.m.0).
    Write-Host "enter to publish .nupkg to GitHub Releases"
    pause
    $pub = PublishGithubReleases @names
    if (-not $pub) { Write-Host "GitHub release publish had errors" }

    if([string]::IsNullOrEmpty($env:nugetKey))
    {
        Write-Host "Build & pack success"
    }
    else
    {
        Write-Host "enter to push nuget"
        pause
        Write-Host "enter to confirm"
        pause

        NugetPush @names
    }
}
else
{
    echo "Build & pack error"
}
pause
