#requires -Version 7.0
[CmdletBinding()]
param(
    [string] $SourceRoot = (Join-Path (Split-Path $PSScriptRoot -Parent) 'SDL3-CS')
)

if (-not (Test-Path -LiteralPath $SourceRoot -PathType Container)) {
    throw "SourceRoot was not found: $SourceRoot"
}

$resolvedSourceRoot = (Resolve-Path -LiteralPath $SourceRoot).Path
$privateDocViolations = New-Object System.Collections.Generic.List[object]
$missingPublicDocViolations = New-Object System.Collections.Generic.List[object]

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
        [AllowEmptyString()]
        [string] $Line
    )

    return $Line -match '^\s*private\b.*\(' -and $Line -notmatch '^\s*private\b.*\b(class|record|struct)\b'
}

function Test-PublicMethodDeclaration {
    param(
        [Parameter(Mandatory)]
        [AllowEmptyString()]
        [string] $Line
    )

    if ($Line -notmatch '^\s*public\b') {
        return $false
    }

    if ($Line -match '^\s*public\s+(?:(?:static|partial|abstract|sealed|readonly|unsafe|ref)\s+)*(class|record|struct|interface|enum|delegate)\b') {
        return $false
    }

    return $Line -match '^\s*public\s+(?:(?:static|partial|unsafe|extern|override|virtual|new|readonly|sealed|async)\s+)*(?:[\w@][\w@\.<>[\],?*]*\s+)+[\w@]+\s*\('
}

function Test-IgnorableBetweenDocAndDeclaration {
    param(
        [Parameter(Mandatory)]
        [AllowEmptyString()]
        [string] $Line
    )

    return $Line -match '^\s*$' -or
        $Line -match '^\s*\[' -or
        $Line -match '^\s*//(?!/)' -or
        $Line -match '^\s*#pragma\b'
}

function Get-DeclarationLineIndex {
    param(
        [Parameter(Mandatory)]
        [AllowEmptyString()]
        [string[]] $Lines,

        [Parameter(Mandatory)]
        [int] $StartIndex
    )

    $targetLine = $StartIndex
    while ($targetLine -lt $Lines.Count) {
        if (Test-IgnorableBetweenDocAndDeclaration -Line $Lines[$targetLine]) {
            $targetLine++
            continue
        }

        break
    }

    return $targetLine
}

function Test-HasXmlDocumentation {
    param(
        [Parameter(Mandatory)]
        [AllowEmptyString()]
        [string[]] $Lines,

        [Parameter(Mandatory)]
        [int] $DeclarationIndex
    )

    $lineIndex = $DeclarationIndex - 1
    while ($lineIndex -ge 0) {
        $line = $Lines[$lineIndex]

        if ($line -match '^\s*$' -or $line -match '^\s*//(?!/)' -or $line -match '^\s*#pragma\b') {
            $lineIndex--
            continue
        }

        if ($line -match '^\s*\]') {
            while ($lineIndex -ge 0 -and $Lines[$lineIndex] -notmatch '^\s*\[') {
                $lineIndex--
            }

            if ($lineIndex -ge 0) {
                $lineIndex--
            }

            continue
        }

        if ($line -match '^\s*\[') {
            $lineIndex--
            continue
        }

        break
    }

    return $lineIndex -ge 0 -and $Lines[$lineIndex] -match '^\s*///'
}

Get-ChildItem -LiteralPath $resolvedSourceRoot -Recurse -Filter '*.cs' | ForEach-Object {
    $path = $_.FullName
    $lines = @(Get-Content -LiteralPath $path)

    for ($i = 0; $i -lt $lines.Count; $i++) {
        if ($lines[$i] -notmatch '^\s*///') {
            continue
        }

        $docStart = $i
        while ($i -lt $lines.Count -and $lines[$i] -match '^\s*///') {
            $i++
        }
        $docEnd = $i - 1
        $targetLine = Get-DeclarationLineIndex -Lines $lines -StartIndex $i

        if ($targetLine -lt $lines.Count -and (Test-PrivateMethodDeclaration -Line $lines[$targetLine])) {
            $docLine = $lines[$docStart].Trim()
            $privateDocViolations.Add([pscustomobject]@{
                File = Get-RelativePath -Path $path -Root $resolvedSourceRoot
                DocLine = $docStart + 1
                DeclarationLine = $targetLine + 1
                Declaration = $lines[$targetLine].Trim()
                FirstDocLine = $docLine
            })
        }

        $i = $docEnd
    }

    for ($i = 0; $i -lt $lines.Count; $i++) {
        if (-not (Test-PublicMethodDeclaration -Line $lines[$i])) {
            continue
        }

        if (Test-HasXmlDocumentation -Lines $lines -DeclarationIndex $i) {
            continue
        }

        $missingPublicDocViolations.Add([pscustomobject]@{
            File = Get-RelativePath -Path $path -Root $resolvedSourceRoot
            DeclarationLine = $i + 1
            Declaration = $lines[$i].Trim()
        })
    }
}

if ($privateDocViolations.Count -gt 0) {
    $privateDocViolations |
        Sort-Object File, DocLine |
        Format-Table File, DocLine, DeclarationLine, Declaration -AutoSize

    throw "Found $($privateDocViolations.Count) XML documentation block(s) attached to private methods. Move user-facing documentation to the public wrapper API."
}

if ($missingPublicDocViolations.Count -gt 0) {
    $missingPublicDocViolations |
        Sort-Object File, DeclarationLine |
        Format-Table File, DeclarationLine, Declaration -AutoSize

    throw "Found $($missingPublicDocViolations.Count) public method declaration(s) without XML documentation."
}

Write-Host 'Public wrapper XML documentation placement is valid.'
Write-Host 'Public method XML documentation completeness is valid.'
