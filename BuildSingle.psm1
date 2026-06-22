Import-Module $PSScriptRoot\FunctionModule.psm1 -Force

function NugetBuildSingle
{
	# NuGet pack/push is only allowed on the 'release' branch.
	if (-not (Assert-ReleaseBranch)) { pause; return }

	$projectName= $args[0];

	# Version is resolved by GitVersion (no manual build-date suffix anymore).
	$result = NugetPack $projectName
	if($result)
    {
        # Publish .nupkg to GitHub Releases (grouped by minor: asset attached to vM.m.0).
        Write-Host "enter to publish .nupkg to GitHub Releases"
        pause
        $pub = PublishGithubReleases $projectName
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

            NugetPush $projectName
        }
    }
    else
    {
        echo "Build & pack error"
    }
    pause
}
