#requires -Version 7.0
[CmdletBinding()]
param(
    [string] $ExamplesRoot = (Join-Path $PSScriptRoot '../../SDL3-CS.Examples'),
    [string] $SolutionPath = (Join-Path $PSScriptRoot '../../SDL3-CS.sln'),
    [ValidateSet('All', 'Desktop', 'Android')]
    [string] $Scope = 'All',
    [ValidateSet('Debug', 'Release', 'Prerelease')]
    [string] $Configuration = 'Release',
    [switch] $ValidateOnly
)

function Get-NormalizedFullPath {
    param([Parameter(Mandatory)][string] $Path)

    return [System.IO.Path]::GetFullPath($Path).TrimEnd(
        [System.IO.Path]::DirectorySeparatorChar,
        [System.IO.Path]::AltDirectorySeparatorChar
    )
}

function Test-PathInsideRoot {
    param(
        [Parameter(Mandatory)][string] $Path,
        [Parameter(Mandatory)][string] $Root
    )

    $rootPrefix = $Root + [System.IO.Path]::DirectorySeparatorChar
    return $Path.StartsWith($rootPrefix, [System.StringComparison]::OrdinalIgnoreCase)
}

function Convert-SolutionProjectPath {
    param(
        [Parameter(Mandatory)][string] $Path,
        [Parameter(Mandatory)][string] $SolutionRoot
    )

    $directorySeparator = [System.IO.Path]::DirectorySeparatorChar
    $platformPath = $Path.Replace([char]92, $directorySeparator).Replace([char]47, $directorySeparator)
    return [System.IO.Path]::GetFullPath((Join-Path $SolutionRoot $platformPath))
}

function Test-AndroidProject {
    param([Parameter(Mandatory)][string] $ProjectPath)

    [xml] $projectXml = Get-Content -LiteralPath $ProjectPath -Raw -Encoding UTF8
    $targetFrameworks = @($projectXml.SelectNodes('//TargetFramework | //TargetFrameworks') |
        ForEach-Object { $_.InnerText -split ';' })
    return @($targetFrameworks | Where-Object { $_ -like '*-android*' }).Count -gt 0
}

function Invoke-ExampleBuild {
    param(
        [Parameter(Mandatory)][string] $ProjectPath,
        [Parameter(Mandatory)][string] $BuildConfiguration
    )

    & dotnet build $ProjectPath `
        --configuration $BuildConfiguration `
        --nologo `
        /p:GeneratePackageOnBuild=false

    if ($LASTEXITCODE -ne 0) {
        throw "Example project build failed with exit code $LASTEXITCODE`: $ProjectPath"
    }
}

$resolvedExamplesRoot = Get-NormalizedFullPath -Path $ExamplesRoot
$resolvedSolutionPath = [System.IO.Path]::GetFullPath($SolutionPath)
if (-not (Test-Path -LiteralPath $resolvedExamplesRoot -PathType Container)) {
    throw "Examples root was not found: $resolvedExamplesRoot"
}
if (-not (Test-Path -LiteralPath $resolvedSolutionPath -PathType Leaf)) {
    throw "Solution was not found: $resolvedSolutionPath"
}

$projectFiles = @(Get-ChildItem -LiteralPath $resolvedExamplesRoot -Recurse -File -Filter '*.csproj' |
    Sort-Object FullName)
if ($projectFiles.Count -eq 0) {
    throw "No example projects were found under $resolvedExamplesRoot"
}

$projects = @($projectFiles | ForEach-Object {
    $fullPath = [System.IO.Path]::GetFullPath($_.FullName)
    [pscustomobject]@{
        FullPath = $fullPath
        RelativePath = [System.IO.Path]::GetRelativePath($resolvedExamplesRoot, $fullPath).Replace('\', '/')
        IsAndroid = Test-AndroidProject -ProjectPath $fullPath
    }
})

$solutionRoot = Split-Path -Parent $resolvedSolutionPath
$solutionProjectPaths = @(Get-Content -LiteralPath $resolvedSolutionPath -Encoding UTF8 | ForEach-Object {
    if ($_ -match '^Project\("[^"]+"\)\s*=\s*"[^"]+",\s*"([^"]+\.csproj)"') {
        Convert-SolutionProjectPath -Path $Matches[1] -SolutionRoot $solutionRoot
    }
} | Where-Object { $_ -and (Test-PathInsideRoot -Path $_ -Root $resolvedExamplesRoot) })

$solutionPathCounts = @{}
foreach ($path in $solutionProjectPaths) {
    $key = $path.ToUpperInvariant()
    if (-not $solutionPathCounts.ContainsKey($key)) {
        $solutionPathCounts[$key] = 0
    }
    $solutionPathCounts[$key]++
}

$missingProjects = @($projects | Where-Object { -not $solutionPathCounts.ContainsKey($_.FullPath.ToUpperInvariant()) })
$duplicateProjects = @($projects | Where-Object { $solutionPathCounts[$_.FullPath.ToUpperInvariant()] -gt 1 })
$diskProjectKeys = @{}
foreach ($project in $projects) {
    $diskProjectKeys[$project.FullPath.ToUpperInvariant()] = $true
}
$extraProjects = @($solutionProjectPaths | Where-Object { -not $diskProjectKeys.ContainsKey($_.ToUpperInvariant()) })

$inventoryErrors = New-Object System.Collections.Generic.List[string]
foreach ($project in $missingProjects) {
    $inventoryErrors.Add("Example project is missing from SDL3-CS.sln: $($project.RelativePath)")
}
foreach ($project in $duplicateProjects) {
    $inventoryErrors.Add("Example project appears more than once in SDL3-CS.sln: $($project.RelativePath)")
}
foreach ($path in $extraProjects) {
    $inventoryErrors.Add("SDL3-CS.sln references an unknown example project: $path")
}
if ($inventoryErrors.Count -gt 0) {
    $inventoryErrors | ForEach-Object { Write-Error $_ }
    throw "Example project inventory validation failed with $($inventoryErrors.Count) error(s)."
}

$desktopProjects = @($projects | Where-Object { -not $_.IsAndroid })
$androidProjects = @($projects | Where-Object IsAndroid)
Write-Host "Example project inventory is valid: $($projects.Count) total, $($desktopProjects.Count) desktop, $($androidProjects.Count) Android."

if ($ValidateOnly) {
    return
}

$selectedProjects = switch ($Scope) {
    'Desktop' { $desktopProjects }
    'Android' { $androidProjects }
    default { $projects }
}
if ($selectedProjects.Count -eq 0) {
    throw "No example projects matched scope '$Scope'."
}

for ($index = 0; $index -lt $selectedProjects.Count; $index++) {
    $project = $selectedProjects[$index]
    Write-Host "[$($index + 1)/$($selectedProjects.Count)] Building $($project.RelativePath) ($Configuration)"
    Invoke-ExampleBuild -ProjectPath $project.FullPath -BuildConfiguration $Configuration
}

Write-Host "All $($selectedProjects.Count) example project(s) in scope '$Scope' built successfully."
