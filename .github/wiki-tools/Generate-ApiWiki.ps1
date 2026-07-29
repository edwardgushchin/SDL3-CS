#requires -Version 7.0
[CmdletBinding()]
param(
    [string] $ProjectRoot = (Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path,
    [string] $AssemblyPath,
    [string] $XmlDocPath,
    [string] $OutputPath,
    [string] $RepositoryUrl = 'https://github.com/edwardgushchin/SDL3-CS',
    [string] $WikiUrl = 'https://github.com/edwardgushchin/SDL3-CS/wiki',
    [string] $HomePageName = ('SDL3' + [char]0x2010 + 'CS-Wiki'),
    [ValidatePattern('^[0-9a-fA-F]{40}$')]
    [string] $SourceCommit,
    [string] $GeneratedAtUtc,
    [string] $ManagedVersion
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$script:WikiMetadataLine = $null

function Resolve-RepoPath {
    param(
        [Parameter(Mandatory)] [string] $Path,
        [Parameter(Mandatory)] [string] $BasePath
    )

    if ([System.IO.Path]::IsPathRooted($Path)) {
        return [System.IO.Path]::GetFullPath($Path)
    }

    return [System.IO.Path]::GetFullPath((Join-Path $BasePath $Path))
}

function Assert-ChildPath {
    param(
        [Parameter(Mandatory)] [string] $ParentPath,
        [Parameter(Mandatory)] [string] $ChildPath
    )

    $parentFull = [System.IO.Path]::GetFullPath($ParentPath).TrimEnd([System.IO.Path]::DirectorySeparatorChar, [System.IO.Path]::AltDirectorySeparatorChar)
    $childFull = [System.IO.Path]::GetFullPath($ChildPath).TrimEnd([System.IO.Path]::DirectorySeparatorChar, [System.IO.Path]::AltDirectorySeparatorChar)

    if ($childFull.Equals($parentFull, [System.StringComparison]::OrdinalIgnoreCase)) {
        throw "OutputPath must not be the repository root: $childFull"
    }

    $relative = [System.IO.Path]::GetRelativePath($parentFull, $childFull)
    if ($relative.StartsWith('..') -or [System.IO.Path]::IsPathRooted($relative)) {
        throw "OutputPath must stay inside ProjectRoot. ProjectRoot=$parentFull OutputPath=$childFull"
    }
}

function Get-XmlMemberMap {
    param([Parameter(Mandatory)] [string] $Path)

    [xml] $document = Get-Content -LiteralPath $Path -Raw
    $members = @{}
    foreach ($member in $document.doc.members.member) {
        $name = $member.GetAttribute('name')
        if (-not [string]::IsNullOrWhiteSpace($name)) {
            $members[$name] = $member
        }
    }

    return $members
}

function Get-DocTypeName {
    param([Parameter(Mandatory)] [System.Type] $Type)

    if ($Type.IsByRef) {
        return "$(Get-DocTypeName $Type.GetElementType())@"
    }

    if ($Type.IsPointer) {
        return "$(Get-DocTypeName $Type.GetElementType())*"
    }

    if ($Type.IsArray) {
        $rank = $Type.GetArrayRank()
        $suffix = if ($rank -eq 1) { '[]' } else { '[' + (',' * ($rank - 1)) + ']' }
        return "$(Get-DocTypeName $Type.GetElementType())$suffix"
    }

    if ($Type.IsGenericParameter) {
        if ($null -ne $Type.DeclaringMethod) {
            return '``' + $Type.GenericParameterPosition
        }

        return '`' + $Type.GenericParameterPosition
    }

    if ($Type.IsGenericType) {
        $definition = $Type.GetGenericTypeDefinition()
        $typeName = $definition.FullName.Replace('+', '.')
        $typeName = $typeName -replace '`[0-9]+$', ''
        $arguments = @($Type.GetGenericArguments() | ForEach-Object { Get-DocTypeName $_ })
        return "$typeName{$($arguments -join ',')}"
    }

    return $Type.FullName.Replace('+', '.')
}

function Get-MethodXmlId {
    param([Parameter(Mandatory)] [System.Reflection.MethodInfo] $Method)

    $declaringType = $Method.DeclaringType.FullName.Replace('+', '.')
    $methodName = $Method.Name

    if ($Method.IsGenericMethod) {
        $methodName += '``' + $Method.GetGenericArguments().Length
    }

    $parameters = @($Method.GetParameters() | ForEach-Object { Get-DocTypeName $_.ParameterType })
    if ($parameters.Count -eq 0) {
        return "M:$declaringType.$methodName"
    }

    return "M:$declaringType.$methodName($($parameters -join ','))"
}

$script:TypeAliases = @{
    'System.Void' = 'void'
    'System.Boolean' = 'bool'
    'System.Byte' = 'byte'
    'System.SByte' = 'sbyte'
    'System.Char' = 'char'
    'System.Int16' = 'short'
    'System.UInt16' = 'ushort'
    'System.Int32' = 'int'
    'System.UInt32' = 'uint'
    'System.Int64' = 'long'
    'System.UInt64' = 'ulong'
    'System.Single' = 'float'
    'System.Double' = 'double'
    'System.Decimal' = 'decimal'
    'System.String' = 'string'
    'System.Object' = 'object'
    'System.IntPtr' = 'IntPtr'
    'System.UIntPtr' = 'UIntPtr'
}

function Get-FriendlyTypeName {
    param([Parameter(Mandatory)] [System.Type] $Type)

    if ($Type.IsByRef) {
        return Get-FriendlyTypeName $Type.GetElementType()
    }

    if ($Type.IsPointer) {
        return "$(Get-FriendlyTypeName $Type.GetElementType())*"
    }

    if ($Type.IsArray) {
        $rank = $Type.GetArrayRank()
        $suffix = if ($rank -eq 1) { '[]' } else { '[' + (',' * ($rank - 1)) + ']' }
        return "$(Get-FriendlyTypeName $Type.GetElementType())$suffix"
    }

    if ($Type.IsGenericParameter) {
        return $Type.Name
    }

    if ($null -ne $Type.FullName -and $script:TypeAliases.ContainsKey($Type.FullName)) {
        return $script:TypeAliases[$Type.FullName]
    }

    if ($Type.IsGenericType) {
        $definition = $Type.GetGenericTypeDefinition()
        $arguments = @($Type.GetGenericArguments() | ForEach-Object { Get-FriendlyTypeName $_ })

        if ($definition.FullName -eq 'System.Nullable`1') {
            return "$($arguments[0])?"
        }

        $name = $definition.Name -replace '`[0-9]+$', ''
        if ($null -ne $definition.DeclaringType) {
            $name = "$(Get-FriendlyTypeName $definition.DeclaringType).$name"
        }

        return "$name<$($arguments -join ', ')>"
    }

    if ($null -ne $Type.DeclaringType) {
        return "$(Get-FriendlyTypeName $Type.DeclaringType).$($Type.Name)"
    }

    if ($Type.Namespace -eq 'SDL3') {
        return $Type.Name
    }

    if ($Type.Namespace -eq 'System') {
        return $Type.Name
    }

    return $Type.FullName.Replace('+', '.')
}

function Get-WikiIdentifier {
    param([string] $Name)

    if ($null -eq $Name) {
        return ''
    }

    return $Name.Replace([string][char]0x0421, 'C').Replace([string][char]0x0441, 'c')
}

function Get-ParameterSignature {
    param([Parameter(Mandatory)] [System.Reflection.ParameterInfo] $Parameter)

    $type = $Parameter.ParameterType
    $prefix = ''

    if ($type.IsByRef) {
        if ($Parameter.IsOut) {
            $prefix = 'out '
        } elseif ($Parameter.IsIn) {
            $prefix = 'in '
        } else {
            $prefix = 'ref '
        }
        $type = $type.GetElementType()
    }

    if ($null -ne $Parameter.GetCustomAttributes([System.ParamArrayAttribute], $false) -and $Parameter.GetCustomAttributes([System.ParamArrayAttribute], $false).Count -gt 0) {
        $prefix = 'params '
    }

    return "$prefix$(Get-FriendlyTypeName $type) $(Get-WikiIdentifier $Parameter.Name)"
}

function Get-MethodSignature {
    param([Parameter(Mandatory)] [System.Reflection.MethodInfo] $Method)

    $static = if ($Method.IsStatic) { 'static ' } else { '' }
    $genericArguments = ''
    if ($Method.IsGenericMethod) {
        $genericArguments = '<' + (($Method.GetGenericArguments() | ForEach-Object { $_.Name }) -join ', ') + '>'
    }

    $parameters = @($Method.GetParameters() | ForEach-Object { Get-ParameterSignature $_ })
    return "public $static$(Get-FriendlyTypeName $Method.ReturnType) $($Method.Name)$genericArguments($($parameters -join ', '));"
}

function Convert-CrefToText {
    param([string] $Cref)

    if ([string]::IsNullOrWhiteSpace($Cref)) {
        return ''
    }

    $text = $Cref -replace '^[A-Z]:', ''
    $text = $text -replace '\(.*$', ''
    $text = $text -replace '``[0-9]+', ''
    $text = $text -replace '^SDL3\.', ''
    return $text
}

function Convert-SdlRelativeLinks {
    param([string] $Text)

    if ([string]::IsNullOrWhiteSpace($Text)) {
        return $Text
    }

    return [regex]::Replace(
        $Text,
        '\]\((?![a-z][a-z0-9+.-]*:|#|mailto:)([^)#]+)(#[^)]+)?\)',
        {
            param($match)
            $target = $match.Groups[1].Value
            $anchor = $match.Groups[2].Value
            "](https://wiki.libsdl.org/SDL3/$target$anchor)"
        })
}

function Convert-XmlChildToMarkdown {
    param([Parameter(Mandatory)] [System.Xml.XmlNode] $Node)

    switch ($Node.NodeType) {
        ([System.Xml.XmlNodeType]::Text) {
            return ([regex]::Replace($Node.Value, '\s+', ' '))
        }
        ([System.Xml.XmlNodeType]::CData) {
            return ([regex]::Replace($Node.Value, '\s+', ' '))
        }
        ([System.Xml.XmlNodeType]::Element) {
            switch ($Node.Name) {
                'c' {
                    return '`' + $Node.InnerText.Trim() + '`'
                }
                'code' {
                    $code = $Node.InnerText.Trim()
                    if ([string]::IsNullOrWhiteSpace($code)) {
                        return ''
                    }
                    return "`n```c`n$code`n```n"
                }
                'para' {
                    $text = Convert-XmlNodeToMarkdown $Node
                    if ([string]::IsNullOrWhiteSpace($text)) {
                        return ''
                    }
                    return "$text`n`n"
                }
                'see' {
                    $cref = $Node.GetAttribute('cref')
                    if (-not [string]::IsNullOrWhiteSpace($cref)) {
                        return '`' + (Convert-CrefToText $cref) + '`'
                    }

                    $href = $Node.GetAttribute('href')
                    if (-not [string]::IsNullOrWhiteSpace($href)) {
                        $label = if ([string]::IsNullOrWhiteSpace($Node.InnerText)) { $href } else { $Node.InnerText.Trim() }
                        return "[$label]($href)"
                    }

                    return $Node.InnerText.Trim()
                }
                'seealso' {
                    $cref = $Node.GetAttribute('cref')
                    if (-not [string]::IsNullOrWhiteSpace($cref)) {
                        return '`' + (Convert-CrefToText $cref) + '`'
                    }

                    return $Node.InnerText.Trim()
                }
                'a' {
                    $href = $Node.GetAttribute('href')
                    $label = if ([string]::IsNullOrWhiteSpace($Node.InnerText)) { $href } else { $Node.InnerText.Trim() }
                    if ([string]::IsNullOrWhiteSpace($href)) {
                        return $label
                    }
                    if ($href -notmatch '^[a-z][a-z0-9+.-]*:' -and -not $href.StartsWith('#')) {
                        $href = "https://wiki.libsdl.org/SDL3/$href"
                    }
                    return "[$label]($href)"
                }
                'paramref' {
                    return '`' + $Node.GetAttribute('name') + '`'
                }
                'typeparamref' {
                    return '`' + $Node.GetAttribute('name') + '`'
                }
                'list' {
                    $items = @()
                    foreach ($item in $Node.SelectNodes('item')) {
                        $items += '- ' + (Convert-XmlNodeToMarkdown $item).Trim()
                    }
                    return "`n$($items -join "`n")`n"
                }
                default {
                    return Convert-XmlNodeToMarkdown $Node
                }
            }
        }
        default {
            return ''
        }
    }
}

function Convert-XmlNodeToMarkdown {
    param([System.Xml.XmlNode] $Node)

    if ($null -eq $Node) {
        return ''
    }

    $parts = @()
    foreach ($child in $Node.ChildNodes) {
        $parts += Convert-XmlChildToMarkdown $child
    }

    $text = ($parts -join '')
    $text = $text -replace "[ \t]+`n", "`n"
    $text = $text -replace "`n[ \t]+", "`n"
    $text = $text -replace ' {2,}', ' '
    $text = $text -replace "(`n){3,}", "`n`n"
    return (Convert-SdlRelativeLinks $text.Trim())
}

function Escape-MarkdownTableCell {
    param([string] $Text)

    if ($null -eq $Text) {
        return ''
    }

    return ($Text -replace '\|', '\|' -replace "`r?`n", '<br>')
}

function Get-SurfaceName {
    param([Parameter(Mandatory)] [System.Type] $Type)

    $docTypeName = $Type.FullName.Replace('+', '.')
    if (-not $docTypeName.StartsWith('SDL3.')) {
        return $null
    }

    $parts = $docTypeName.Substring(5).Split('.')
    return $parts[0]
}

function Get-TypeDisplayName {
    param([Parameter(Mandatory)] [System.Type] $Type)

    $name = $Type.FullName.Replace('+', '.')
    if ($name.StartsWith('SDL3.')) {
        return $name.Substring(5)
    }

    return $name
}

function Write-WikiFile {
    param(
        [Parameter(Mandatory)] [string] $Name,
        [Parameter(Mandatory)] [AllowEmptyString()] [string[]] $Content,
        [Parameter(Mandatory)] [string] $Directory
    )

    $lines = [System.Collections.Generic.List[string]]::new()
    if (-not [string]::IsNullOrWhiteSpace($script:WikiMetadataLine)) {
        $lines.Add($script:WikiMetadataLine)
        $lines.Add('')
    }
    foreach ($line in $Content) {
        $lines.Add($line)
    }
    while ($lines.Count -gt 0 -and $lines[$lines.Count - 1] -eq '') {
        $lines.RemoveAt($lines.Count - 1)
    }

    $path = Join-Path $Directory "$Name.md"
    Set-Content -LiteralPath $path -Value ($lines -join "`n") -Encoding utf8NoBOM
}

function Get-WikiContentHash {
    param([Parameter(Mandatory)][string] $Path)

    $manifest = [System.Text.StringBuilder]::new()
    foreach ($file in Get-ChildItem -LiteralPath $Path -Filter '*.md' -File | Sort-Object Name) {
        $canonicalContent = [System.IO.File]::ReadAllText($file.FullName).Replace("`r`n", "`n").Replace("`r", "`n")
        $fileHash = [Convert]::ToHexString([System.Security.Cryptography.SHA256]::HashData([System.Text.Encoding]::UTF8.GetBytes($canonicalContent))).ToLowerInvariant()
        [void] $manifest.Append($file.Name).Append(':').Append($fileHash).Append("`n")
    }

    $manifestBytes = [System.Text.Encoding]::UTF8.GetBytes($manifest.ToString())
    return [Convert]::ToHexString([System.Security.Cryptography.SHA256]::HashData($manifestBytes)).ToLowerInvariant()
}

$ProjectRoot = [System.IO.Path]::GetFullPath($ProjectRoot)
if ([string]::IsNullOrWhiteSpace($AssemblyPath)) {
    $AssemblyPath = Join-Path $ProjectRoot 'SDL3-CS\bin\Release\net8.0\SDL3-CS.dll'
}
if ([string]::IsNullOrWhiteSpace($XmlDocPath)) {
    $XmlDocPath = Join-Path $ProjectRoot 'SDL3-CS\bin\Release\SDL3-CS.xml'
}
if ([string]::IsNullOrWhiteSpace($OutputPath)) {
    $OutputPath = Join-Path $ProjectRoot 'artifacts\wiki\SDL3-CS'
}

$AssemblyPath = Resolve-RepoPath $AssemblyPath $ProjectRoot
$XmlDocPath = Resolve-RepoPath $XmlDocPath $ProjectRoot
$OutputPath = Resolve-RepoPath $OutputPath $ProjectRoot

if (-not (Test-Path -LiteralPath $AssemblyPath)) {
    throw "Assembly not found: $AssemblyPath. Run: dotnet build .\SDL3-CS\SDL3-CS.csproj -c Release --no-restore"
}
if (-not (Test-Path -LiteralPath $XmlDocPath)) {
    throw "XML documentation not found: $XmlDocPath. Run: dotnet build .\SDL3-CS\SDL3-CS.csproj -c Release --no-restore"
}

Assert-ChildPath -ParentPath $ProjectRoot -ChildPath $OutputPath

if (Test-Path -LiteralPath $OutputPath) {
    Get-ChildItem -LiteralPath $OutputPath -Force | Remove-Item -Recurse -Force
} else {
    New-Item -ItemType Directory -Path $OutputPath -Force | Out-Null
}

$memberMap = Get-XmlMemberMap $XmlDocPath
$assembly = [System.Reflection.Assembly]::LoadFrom($AssemblyPath)
$assemblyVersion = $assembly.GetName().Version.ToString()
if ([string]::IsNullOrWhiteSpace($ManagedVersion)) {
    $ManagedVersion = $assemblyVersion
}
if ($ManagedVersion -notmatch '^\d+\.\d+\.\d+\.\d+(?:[-+][0-9A-Za-z.-]+)?$') {
    throw "ManagedVersion must be a four-part package version: $ManagedVersion"
}
if ([string]::IsNullOrWhiteSpace($SourceCommit)) {
    $SourceCommit = ((& git -C $ProjectRoot rev-parse HEAD 2>$null) | Out-String).Trim()
    if ($LASTEXITCODE -ne 0 -or $SourceCommit -notmatch '^[0-9a-fA-F]{40}$') {
        throw 'SourceCommit was not provided and could not be resolved from ProjectRoot.'
    }
}
$SourceCommit = $SourceCommit.ToLowerInvariant()

if ([string]::IsNullOrWhiteSpace($GeneratedAtUtc)) {
    $commitTimestamp = ((& git -C $ProjectRoot show -s --format=%cI $SourceCommit 2>$null) | Out-String).Trim()
    if ($LASTEXITCODE -ne 0 -or [string]::IsNullOrWhiteSpace($commitTimestamp)) {
        throw 'GeneratedAtUtc was not provided and could not be resolved from SourceCommit.'
    }
    $GeneratedAtUtc = $commitTimestamp
}

$parsedGeneratedAt = [DateTimeOffset]::MinValue
if (-not [DateTimeOffset]::TryParse($GeneratedAtUtc, [System.Globalization.CultureInfo]::InvariantCulture, [System.Globalization.DateTimeStyles]::AllowWhiteSpaces, [ref] $parsedGeneratedAt)) {
    throw "GeneratedAtUtc is not a valid timestamp: $GeneratedAtUtc"
}
$GeneratedAtUtc = $parsedGeneratedAt.UtcDateTime.ToString('yyyy-MM-ddTHH:mm:ssZ', [System.Globalization.CultureInfo]::InvariantCulture)
$script:WikiMetadataLine = "<!-- sdl3-cs-wiki managed-version=`"$ManagedVersion`" source-commit=`"$SourceCommit`" generated-at=`"$GeneratedAtUtc`" -->"

$surfaces = [ordered]@{
    SDL = @{
        Title = 'SDL core API'
        Page = 'API-SDL'
        Description = 'Core SDL3 wrapper functions and nested helper surfaces.'
    }
    Image = @{
        Title = 'SDL_image API'
        Page = 'API-Image'
        Description = 'Image loading, saving, animation, and metadata functions.'
    }
    Mixer = @{
        Title = 'SDL_mixer API'
        Page = 'API-Mixer'
        Description = 'Audio mixer, track, group, and decoder functions.'
    }
    TTF = @{
        Title = 'SDL_ttf API'
        Page = 'API-TTF'
        Description = 'Font loading, text shaping, measurement, and rendering functions.'
    }
    ShaderCross = @{
        Title = 'SDL_shadercross API'
        Page = 'API-ShaderCross'
        Description = 'Shader translation, compilation, and reflection functions.'
    }
}

$publicMethods = @()
foreach ($type in $assembly.GetTypes()) {
    if (-not ($type.IsPublic -or $type.IsNestedPublic)) {
        continue
    }
    if ([System.Delegate].IsAssignableFrom($type)) {
        continue
    }

    $surface = Get-SurfaceName $type
    if ([string]::IsNullOrWhiteSpace($surface) -or -not $surfaces.Contains($surface)) {
        continue
    }

    foreach ($method in $type.GetMethods([System.Reflection.BindingFlags]'Public, Static, Instance, DeclaredOnly')) {
        if ($method.IsSpecialName) {
            continue
        }

        $xmlId = Get-MethodXmlId $method
        $doc = $null
        if ($memberMap.ContainsKey($xmlId)) {
            $doc = $memberMap[$xmlId]
        }

        $publicMethods += [pscustomobject]@{
            Surface = $surface
            DeclaringType = Get-TypeDisplayName $method.DeclaringType
            MethodName = $method.Name
            Signature = Get-MethodSignature $method
            XmlId = $xmlId
            Method = $method
            Documentation = $doc
            HasDocumentation = ($null -ne $doc)
        }
    }
}

$publicMethods = @($publicMethods | Sort-Object Surface, DeclaringType, MethodName, Signature)

$homePage = @(
    'This Wiki contains a generated reference for the public SDL3-CS function API.',
    '',
    "- Repository: [$RepositoryUrl]($RepositoryUrl)",
    "- Wiki: [$WikiUrl]($WikiUrl)",
    ('- Managed version: `{0}`' -f $ManagedVersion),
    ('- Source commit: `{0}`' -f $SourceCommit),
    ('- Generated at (UTC): `{0}`' -f $GeneratedAtUtc),
    '',
    '## Sections',
    '',
    '- [API Reference](API-Reference)',
    '- [SDL core API](API-SDL)',
    '- [SDL_image API](API-Image)',
    '- [SDL_mixer API](API-Mixer)',
    '- [SDL_ttf API](API-TTF)',
    '- [SDL_shadercross API](API-ShaderCross)'
)
Write-WikiFile -Name $HomePageName -Content $homePage -Directory $OutputPath

$sidebar = @(
    "* [SDL3-CS Wiki]($HomePageName)",
    '* [API Reference](API-Reference)',
    '  * [SDL core API](API-SDL)',
    '  * [SDL_image API](API-Image)',
    '  * [SDL_mixer API](API-Mixer)',
    '  * [SDL_ttf API](API-TTF)',
    '  * [SDL_shadercross API](API-ShaderCross)'
)
Write-WikiFile -Name '_Sidebar' -Content $sidebar -Directory $OutputPath

$reference = @(
    '# API Reference',
    '',
    'Index of public SDL3-CS functions grouped by the main API surfaces.',
    '',
    '| Surface | Page | Functions | With XML docs | Description |',
    '|---|---:|---:|---:|---|'
)
foreach ($surfaceName in $surfaces.Keys) {
    $surfaceMethods = @($publicMethods | Where-Object Surface -eq $surfaceName)
    $documented = @($surfaceMethods | Where-Object HasDocumentation).Count
    $reference += ('| `{0}` | [{1}]({2}) | {3} | {4} | {5} |' -f $surfaceName, $surfaces[$surfaceName].Title, $surfaces[$surfaceName].Page, $surfaceMethods.Count, $documented, $surfaces[$surfaceName].Description)
}
Write-WikiFile -Name 'API-Reference' -Content $reference -Directory $OutputPath

foreach ($surfaceName in $surfaces.Keys) {
    $surface = $surfaces[$surfaceName]
    $surfaceMethods = @($publicMethods | Where-Object Surface -eq $surfaceName)
    $documented = @($surfaceMethods | Where-Object HasDocumentation).Count
    $page = @(
        "# $($surface.Title)",
        '',
        '[Back to API Reference](API-Reference)',
        '',
        $surface.Description,
        '',
        "- Functions: $($surfaceMethods.Count)",
        "- Functions with XML docs: $documented",
        ''
    )

    foreach ($typeGroup in ($surfaceMethods | Group-Object DeclaringType | Sort-Object Name)) {
        $page += ('## `{0}`' -f $typeGroup.Name)
        $page += ''

        foreach ($entry in ($typeGroup.Group | Sort-Object MethodName, Signature)) {
            $method = [System.Reflection.MethodInfo] $entry.Method
            $doc = [System.Xml.XmlElement] $entry.Documentation

            $page += ('### `{0}.{1}`' -f $entry.DeclaringType, $entry.MethodName)
            $page += ''
            $page += '```csharp'
            $page += $entry.Signature
            $page += '```'
            $page += ''

            if ($null -ne $doc) {
                $nativeDeclaration = Convert-XmlNodeToMarkdown ($doc.SelectSingleNode('code'))
                if (-not [string]::IsNullOrWhiteSpace($nativeDeclaration)) {
                    $page += '**SDL declaration**'
                    $page += ''
                    $page += $nativeDeclaration
                    $page += ''
                }

                $summary = Convert-XmlNodeToMarkdown ($doc.SelectSingleNode('summary'))
                if (-not [string]::IsNullOrWhiteSpace($summary)) {
                    $page += $summary
                    $page += ''
                }

                $parameters = @($method.GetParameters())
                if ($parameters.Count -gt 0) {
                    $page += '**Parameters**'
                    $page += ''
                    $page += '| Name | Type | Description |'
                    $page += '|---|---|---|'
                    foreach ($parameter in $parameters) {
                        $parameterType = $parameter.ParameterType
                        $direction = ''
                        if ($parameterType.IsByRef) {
                            if ($parameter.IsOut) {
                                $direction = 'out '
                            } elseif ($parameter.IsIn) {
                                $direction = 'in '
                            } else {
                                $direction = 'ref '
                            }
                            $parameterType = $parameterType.GetElementType()
                        }

                        $descriptionNode = $doc.SelectSingleNode("param[@name='$($parameter.Name)']")
                        $description = Convert-XmlNodeToMarkdown $descriptionNode
                        $page += ('| `{0}` | `{1}{2}` | {3} |' -f (Get-WikiIdentifier $parameter.Name), $direction, (Get-FriendlyTypeName $parameterType), (Escape-MarkdownTableCell $description))
                    }
                    $page += ''
                }

                $returns = Convert-XmlNodeToMarkdown ($doc.SelectSingleNode('returns'))
                if (-not [string]::IsNullOrWhiteSpace($returns)) {
                    $page += '**Returns**'
                    $page += ''
                    $page += $returns
                    $page += ''
                }

                $remarks = Convert-XmlNodeToMarkdown ($doc.SelectSingleNode('remarks'))
                if (-not [string]::IsNullOrWhiteSpace($remarks)) {
                    $page += '**Remarks**'
                    $page += ''
                    $page += $remarks
                    $page += ''
                }

                $threadSafety = Convert-XmlNodeToMarkdown ($doc.SelectSingleNode('threadsafety'))
                if (-not [string]::IsNullOrWhiteSpace($threadSafety)) {
                    $page += '**Thread safety**'
                    $page += ''
                    $page += $threadSafety
                    $page += ''
                }

                $since = Convert-XmlNodeToMarkdown ($doc.SelectSingleNode('since'))
                if (-not [string]::IsNullOrWhiteSpace($since)) {
                    $page += "**Since:** $since"
                    $page += ''
                }

                $seeAlsoNodes = @($doc.SelectNodes('seealso'))
                if ($seeAlsoNodes.Count -gt 0) {
                    $seeAlso = @($seeAlsoNodes | ForEach-Object { Convert-XmlNodeToMarkdown $_ } | Where-Object { -not [string]::IsNullOrWhiteSpace($_) })
                    if ($seeAlso.Count -gt 0) {
                        $page += '**See also:** ' + ($seeAlso -join ', ')
                        $page += ''
                    }
                }
            } else {
                $page += '_XML documentation is not available for this function._'
                $page += ''
            }

            $page += ('**XML member id:** `{0}`' -f $entry.XmlId)
            $page += ''
        }
    }

    Write-WikiFile -Name $surface.Page -Content $page -Directory $OutputPath
}

$summary = [pscustomobject]@{
    OutputPath = $OutputPath
    Pages = @(Get-ChildItem -LiteralPath $OutputPath -Filter '*.md' | Select-Object -ExpandProperty Name)
    ManagedVersion = $ManagedVersion
    AssemblyVersion = $assemblyVersion
    SourceCommit = $SourceCommit
    GeneratedAtUtc = $GeneratedAtUtc
    ContentHash = Get-WikiContentHash -Path $OutputPath
    FunctionCount = $publicMethods.Count
    DocumentedFunctionCount = @($publicMethods | Where-Object HasDocumentation).Count
    MissingXmlDocCount = @($publicMethods | Where-Object { -not $_.HasDocumentation }).Count
}

$summary | ConvertTo-Json -Depth 4 -Compress
