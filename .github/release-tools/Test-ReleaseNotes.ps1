#requires -Version 7.0
[CmdletBinding(DefaultParameterSetName = 'Directory')]
param(
    [Parameter(ParameterSetName = 'Directory')]
    [string] $ReleaseNotesDir = (Join-Path $PSScriptRoot 'release-notes'),

    [Parameter(Mandatory, ParameterSetName = 'File')]
    [string] $ReleaseNotesPath
)

. (Join-Path $PSScriptRoot 'Release.Common.ps1')

$notesFiles = if ($PSCmdlet.ParameterSetName -eq 'File') {
    $path = Resolve-ReleasePath $ReleaseNotesPath
    if (-not (Test-Path -LiteralPath $path -PathType Leaf)) {
        throw "Release notes file was not found: $path"
    }

    @(Get-Item -LiteralPath $path)
}
else {
    $dir = Resolve-ReleasePath $ReleaseNotesDir
    if (-not (Test-Path -LiteralPath $dir -PathType Container)) {
        throw "Release notes directory was not found: $dir"
    }

    @(Get-ChildItem -LiteralPath $dir -Filter '*.md' -File | Sort-Object Name)
}

$errors = New-Object System.Collections.Generic.List[string]
foreach ($file in $notesFiles) {
    $lines = @(Get-Content -LiteralPath $file.FullName -Encoding UTF8)
    if ($lines.Count -eq 0 -or [string]::IsNullOrWhiteSpace(($lines -join ''))) {
        $errors.Add("Release notes file is empty: $($file.FullName)")
        continue
    }

    for ($i = 0; $i -lt $lines.Count; $i++) {
        if ($lines[$i] -match '^#\s+') {
            $errors.Add("Release notes body must not contain a top-level H1 because GitHub renders the release title separately: $($file.FullName):$($i + 1)")
        }
    }
}

if ($errors.Count -gt 0) {
    $errors | ForEach-Object { Write-Error $_ }
    throw "Release notes validation failed with $($errors.Count) error(s)."
}

Write-Host "Release notes body files do not contain top-level H1 headings."
