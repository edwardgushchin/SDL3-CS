#requires -Version 7.0
[CmdletBinding()]
param(
    [string] $PublishScriptPath = (Join-Path $PSScriptRoot 'Publish-Release.ps1')
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

function Add-PublishPlanError {
    param(
        [Parameter(Mandatory)]
        [string] $Message
    )

    $script:errors.Add($Message)
}

function Assert-PublishScriptContains {
    param(
        [Parameter(Mandatory)]
        [string] $Text,

        [Parameter(Mandatory)]
        [string] $Expected,

        [Parameter(Mandatory)]
        [string] $Description
    )

    if (-not $Text.Contains($Expected, [System.StringComparison]::Ordinal)) {
        Add-PublishPlanError "$Description is missing expected text: $Expected"
    }
}

$errors = New-Object System.Collections.Generic.List[string]
$publishScript = Resolve-ReleasePath $PublishScriptPath
if (-not (Test-Path -LiteralPath $publishScript -PathType Leaf)) {
    throw "Publish script was not found: $publishScript"
}

$text = Get-Content -LiteralPath $publishScript -Raw -Encoding UTF8

Assert-PublishScriptContains -Text $text -Expected '[switch] $KeepGitHubReleaseDraft' -Description 'keep-draft publish option'
Assert-PublishScriptContains -Text $text -Expected '[switch] $RequireUpstreamCurrent' -Description 'upstream-current publish option'
Assert-PublishScriptContains -Text $text -Expected '$ReleaseNotesDir' -Description 'release notes directory option'
Assert-PublishScriptContains -Text $text -Expected "'release', 'create'" -Description 'GitHub release create command'
Assert-PublishScriptContains -Text $text -Expected "'--draft'" -Description 'GitHub release draft flag'
Assert-PublishScriptContains -Text $text -Expected "'--draft=false'" -Description 'GitHub release publish command'
Assert-PublishScriptContains -Text $text -Expected 'if ($KeepGitHubReleaseDraft)' -Description 'keep-draft branch'
Assert-PublishScriptContains -Text $text -Expected 'if ($NuGetPush)' -Description 'NuGet push branch'
Assert-PublishScriptContains -Text $text -Expected '-RequireUpstreamCurrent:$RequireUpstreamCurrent' -Description 'conditional upstream-current readiness flag'

$draftCreateIndex = $text.IndexOf("'release', 'create'", [System.StringComparison]::Ordinal)
$draftFlagIndex = if ($draftCreateIndex -ge 0) {
    $text.IndexOf("'--draft'", $draftCreateIndex, [System.StringComparison]::Ordinal)
}
else {
    -1
}
$nugetPushIndex = $text.IndexOf('if ($NuGetPush)', [System.StringComparison]::Ordinal)
$keepDraftIndex = $text.IndexOf('if ($KeepGitHubReleaseDraft)', [System.StringComparison]::Ordinal)
$publishDraftIndex = $text.IndexOf("'--draft=false'", [System.StringComparison]::Ordinal)

if ($draftCreateIndex -ge 0 -and $draftFlagIndex -lt $draftCreateIndex) {
    Add-PublishPlanError "GitHub release create command must include '--draft'."
}

if ($draftCreateIndex -ge 0 -and $nugetPushIndex -ge 0 -and $draftCreateIndex -gt $nugetPushIndex) {
    Add-PublishPlanError "GitHub release draft must be created before NuGet push so assets are attached before public release."
}

if ($nugetPushIndex -ge 0 -and $publishDraftIndex -ge 0 -and $publishDraftIndex -lt $nugetPushIndex) {
    Add-PublishPlanError "GitHub release must not be made public before NuGet push completes."
}

if ($keepDraftIndex -ge 0 -and $publishDraftIndex -ge 0 -and $keepDraftIndex -gt $publishDraftIndex) {
    Add-PublishPlanError "-KeepGitHubReleaseDraft branch must wrap the final '--draft=false' publish command."
}

if ($errors.Count -gt 0) {
    $errors | ForEach-Object { Write-Error $_ }
    throw "Release publish plan validation failed with $($errors.Count) error(s)."
}

Write-Host "Release publish plan is safe: GitHub release is created as draft, NuGet push runs before public release publish, and -KeepGitHubReleaseDraft is available."
