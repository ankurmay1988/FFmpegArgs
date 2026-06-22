function RunCommand
{
    $numOfArgs = $args.Length
    for ($i=0; $i -lt $numOfArgs; $i++)
    {
        iex $args[$i]
        if($LASTEXITCODE -eq 0) {
            Write-Host "$($args[$i]) success"
        }
        else{
            Write-Host "$($args[$i]) failed"
            return 0
        }
    }
    return 1
}

# NuGet build/pack is only allowed on the 'master' branch.
function Assert-MasterBranch
{
    $branch = (git rev-parse --abbrev-ref HEAD)
    if ($LASTEXITCODE -ne 0) { Write-Host "Not a git repository."; return $false }
    $branch = "$branch".Trim()
    if ($branch -ne 'master')
    {
        Write-Host "Current branch '$branch' is not 'master'. NuGet build/pack is only allowed on master."
        return $false
    }
    Write-Host "Branch 'master' OK."
    return $true
}

function NugetPack
{
    for ($i=0; $i -lt $args.Length; $i++)
    {
        $id = $args[$i]

        Write-Host "NugetPack $id"
        $proj = "$PSScriptRoot\$id\$id.csproj"
        $out  = "$PSScriptRoot\$id\bin\Release"
        # Version is resolved by GitVersion (SetVersionFromGitVersion target in ProjectBuildProperties.targets).
        $result = RunCommand "Remove-Item -Recurse -Force `"$out\**`" -ErrorAction SilentlyContinue; cmd /c exit 0" `
            "dotnet pack `"$proj`" -c Release -o `"$out`""

        if($result) {
            Write-Host "$id success"
        }
        else{
            Write-Host "$id failed"
            return 0
        }
    }
    return 1
}

function NugetPush
{
    $nugetKey=$env:nugetKey
    if (-not $nugetKey) {
        Write-Host "Nuget key not found. Please set the environment variable 'nugetKey'."
        return
    }
    for ($i=0; $i -lt $args.Length; $i++)
    {
        $id = $args[$i]

        Write-Host "NugetPush $id"

        $files = [System.IO.Directory]::GetFiles("$PSScriptRoot\$id\bin\Release","*.nupkg")
        iex "nuget push $($files[0]) -ApiKey $nugetKey -Source https://api.nuget.org/v3/index.json"
    }
}
