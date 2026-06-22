Import-Module $PSScriptRoot\FunctionModule.psm1 -Force

function NugetBuildSingle
{
	# NuGet pack/push is only allowed on the 'master' branch.
	if (-not (Assert-MasterBranch)) { pause; return }

	$projectName= $args[0];

	# Version is resolved by GitVersion (no manual build-date suffix anymore).
	$result = NugetPack $projectName
	if($result)
    {
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
