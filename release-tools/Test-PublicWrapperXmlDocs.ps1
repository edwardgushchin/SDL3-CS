#requires -Version 7.0
[CmdletBinding()]
param(
    [string] $SourceRoot = (Join-Path (Split-Path $PSScriptRoot -Parent) 'SDL3-CS')
)

if (-not (Test-Path -LiteralPath $SourceRoot -PathType Container)) {
    throw "SourceRoot was not found: $SourceRoot"
}

$resolvedSourceRoot = (Resolve-Path -LiteralPath $SourceRoot).Path
$violations = New-Object System.Collections.Generic.List[object]

function Get-RelativePath {
    param(
        [Parameter(Mandatory)]
        [string] $Path,

        [Parameter(Mandatory)]
        [string] $Root
    )

    $relative = [System.IO.Path]::GetRelativePath($Root, $Path)
    return $relative.Replace([System.IO.Path]::DirectorySeparatorChar, '/')
}

function Test-PrivateMethodDeclaration {
    param(
        [Parameter(Mandatory)]
        [string] $Line
    )

    return $Line -match '^\s*private\b.*\(' -and $Line -notmatch '^\s*private\b.*\b(class|record|struct)\b'
}

Get-ChildItem -LiteralPath $resolvedSourceRoot -Recurse -Filter '*.cs' | ForEach-Object {
    $path = $_.FullName
    $lines = Get-Content -LiteralPath $path

    for ($i = 0; $i -lt $lines.Count; $i++) {
        if ($lines[$i] -notmatch '^\s*///') {
            continue
        }

        $docStart = $i
        while ($i -lt $lines.Count -and $lines[$i] -match '^\s*///') {
            $i++
        }
        $docEnd = $i - 1

        $targetLine = $i
        while ($targetLine -lt $lines.Count) {
            if ($lines[$targetLine] -match '^\s*$') {
                $targetLine++
                continue
            }

            if ($lines[$targetLine] -match '^\s*\[') {
                $targetLine++
                continue
            }

            break
        }

        if ($targetLine -lt $lines.Count -and (Test-PrivateMethodDeclaration -Line $lines[$targetLine])) {
            $docLine = $lines[$docStart].Trim()
            $violations.Add([pscustomobject]@{
                File = Get-RelativePath -Path $path -Root $resolvedSourceRoot
                DocLine = $docStart + 1
                DeclarationLine = $targetLine + 1
                Declaration = $lines[$targetLine].Trim()
                FirstDocLine = $docLine
            })
        }

        $i = $docEnd
    }
}

if ($violations.Count -gt 0) {
    $violations |
        Sort-Object File, DocLine |
        Format-Table File, DocLine, DeclarationLine, Declaration -AutoSize

    throw "Found $($violations.Count) XML documentation block(s) attached to private methods. Move user-facing documentation to the public wrapper API."
}

Write-Host 'Public wrapper XML documentation placement is valid.'
