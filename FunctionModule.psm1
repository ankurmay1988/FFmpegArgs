# Single source of truth for the NuGet package ids to build/pack/publish.
# Shared by NugetAll.ps1 (local) and the release CI workflow.
function Get-PackageIds
{
    return @(
        "FFmpegArgs",
        "FFmpegArgs.Codec",
        "FFmpegArgs.Cores",
        "FFmpegArgs.Executes",
        "FFmpegArgs.Extensions",
        "FFmpegArgs.Filters",
        "FFmpegArgs.Filters.AudioFilters",
        #"FFmpegArgs.Filters.AudioSinks",
        "FFmpegArgs.Filters.AudioSources",
        "FFmpegArgs.Filters.Generated",
        "FFmpegArgs.Filters.Multimedia",
        #"FFmpegArgs.Filters.MultimediaSources",
        "FFmpegArgs.Filters.OpenCLVideoFilters",
        #"FFmpegArgs.Filters.VAAPIVideoFilters",
        "FFmpegArgs.Filters.VideoFilters",
        #"FFmpegArgs.Filters.VideoSinks",
        "FFmpegArgs.Filters.VideoSources",
        "FFmpegArgs.Inputs",
        "FFmpegArgs.Outputs"
    )
}

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

# NuGet build/pack/publish is only allowed on the 'release' branch.
function Assert-ReleaseBranch
{
    $branch = (git rev-parse --abbrev-ref HEAD)
    if ($LASTEXITCODE -ne 0) { Write-Host "Not a git repository."; return $false }
    $branch = "$branch".Trim()
    if ($branch -ne 'release')
    {
        Write-Host "Current branch '$branch' is not 'release'. NuGet build/pack/publish is only allowed on release."
        return $false
    }
    Write-Host "Branch 'release' OK."
    return $true
}

# Publish packed .nupkg to GitHub Releases, grouped by MAJOR.MINOR line:
# every package of version M.m.* is uploaded as an asset to the release for tag vM.m.0.
# The minor tag must already exist & be pushed; the release is created if missing, reused otherwise.
# Requires the GitHub CLI 'gh' (authenticated).
function PublishGithubReleases
{
    if (-not (Get-Command gh -ErrorAction SilentlyContinue)) {
        Write-Host "PublishGithubReleases: 'gh' (GitHub CLI) not found. Install: winget install GitHub.cli"
        return 0
    }

    # Group nupkg paths by MAJOR.MINOR, parsing the version from the filename (<id>.<version>.nupkg).
    $groups = @{}
    for ($i=0; $i -lt $args.Length; $i++)
    {
        $id = $args[$i]
        $binDir = "$PSScriptRoot\$id\bin\Release"
        if (-not (Test-Path $binDir)) { Write-Host "PublishGithubReleases: '$binDir' not found"; return 0 }

        $files = [System.IO.Directory]::GetFiles($binDir, "*.nupkg")
        if ($files.Length -eq 0) { Write-Host "PublishGithubReleases: no .nupkg for $id in $binDir"; return 0 }

        foreach ($file in $files)
        {
            $stem    = [System.IO.Path]::GetFileNameWithoutExtension($file)  # <id>.<version>
            $version = $stem.Substring($id.Length + 1)                       # <version>
            $parts   = $version.Split('.')
            if ($parts.Length -lt 2) { Write-Host "PublishGithubReleases: cannot parse minor from '$version' ($id)"; return 0 }

            $minor = "$($parts[0]).$($parts[1])"                             # MAJOR.MINOR
            if (-not $groups.ContainsKey($minor)) { $groups[$minor] = New-Object System.Collections.Generic.List[string] }
            $groups[$minor].Add($file)
        }
    }

    foreach ($minor in $groups.Keys)
    {
        $tag = "v$minor.0"

        # The minor tag anchors the M.m line; it must exist locally (and be pushed to origin).
        git rev-parse -q --verify "refs/tags/$tag" 1>$null 2>$null
        if ($LASTEXITCODE -ne 0) {
            Write-Host "PublishGithubReleases: tag '$tag' not found. Create it first: git tag $tag && git push origin $tag"
            return 0
        }

        # Create the GitHub Release for this minor if missing (reuse otherwise).
        gh release view $tag 1>$null 2>$null
        if ($LASTEXITCODE -ne 0) {
            Write-Host "Creating GitHub Release $tag"
            gh release create $tag --verify-tag --title $tag --generate-notes
            if ($LASTEXITCODE -ne 0) { Write-Host "gh release create $tag failed"; return 0 }
        }

        $assets = @($groups[$minor])
        Write-Host "Uploading $($assets.Count) package(s) to GitHub Release $tag"
        gh release upload $tag $assets --clobber
        if ($LASTEXITCODE -ne 0) { Write-Host "gh release upload $tag failed"; return 0 }
    }
    return 1
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
        # dotnet nuget push is portable (Windows local + CI runner); --skip-duplicate avoids errors on re-push.
        & dotnet nuget push $files[0] --api-key $nugetKey --source https://api.nuget.org/v3/index.json --skip-duplicate
    }
}
