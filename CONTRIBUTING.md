# Contributing to SDL3-CS

Thank you for helping improve SDL3-CS. Contributions are welcome for managed bindings, native packages, examples, documentation, tests, and release tooling.

## Before You Start

- Read and follow the [Code of Conduct](CODE_OF_CONDUCT.md).
- Search the [existing issues](https://github.com/edwardgushchin/SDL3-CS/issues) and [discussions](https://github.com/edwardgushchin/SDL3-CS/discussions) before opening a new report.
- Use [GitHub Discussions](https://github.com/edwardgushchin/SDL3-CS/discussions) for usage questions. The [support guide](SUPPORT.md) explains where each type of request belongs.
- Report suspected vulnerabilities privately as described in the [security policy](SECURITY.md).
- For a substantial API, ABI, packaging, or release change, open an issue first so the scope and upstream SDL baseline can be agreed on.

## Development Setup

The managed wrapper supports .NET 7, .NET 8, .NET 9, and .NET 10. Install the matching .NET SDKs and PowerShell 7. Native-package work can also require CMake and the platform toolchains used by the relevant workflow.

Clone your fork and restore the managed projects:

```powershell
git clone https://github.com/YOUR-USER/SDL3-CS.git
cd SDL3-CS
dotnet restore .\SDL3-CS\SDL3-CS.csproj
dotnet restore .\SDL3-CS.Tests\SDL3-CS.Tests.csproj
```

Create a focused branch from the active development branch. Unless an issue or maintainer says otherwise, pull requests should target the current `release-*` branch; release branches are subsequently integrated into `main`.

## Binding Changes

Keep bindings aligned with the exact upstream SDL, SDL_image, SDL_mixer, SDL_ttf, or SDL_shadercross declaration used by the active release line.

- Follow the structure, naming, marshalling, and XML documentation style of neighboring APIs.
- Preserve the original C declaration in the public wrapper XML documentation.
- Put upstream API documentation on the public managed member, not a private native entry point.
- Add meaningful mirrored tests in `SDL3-CS.Tests/` using the same relative path as the wrapper source.
- Cover success and failure behavior, marshalling, ownership, overloads, and native metadata where applicable.
- Avoid unrelated formatting or refactoring in the same pull request.

## Tests and Checks

Run the narrowest relevant test first. Before submitting a managed wrapper change, run at least:

```powershell
dotnet build .\SDL3-CS\SDL3-CS.csproj -c Release /p:GeneratePackageOnBuild=false
dotnet build .\SDL3-CS.Tests\SDL3-CS.Tests.csproj -c Release
pwsh .\.github\release-tools\Test-PublicWrapperXmlDocs.ps1 -SourceRoot .\SDL3-CS
```

The test runner needs matching native libraries. On Windows, restore the published runtime for the active release line, then run it:

```powershell
pwsh .\.github\release-tools\Restore-ManagedReleaseTestRuntime.ps1 `
  -NativePackageRevision 1 `
  -Rid win-x64 `
  -Destination SDL3-CS.Tests\bin\Release\net8.0

dotnet run --project .\SDL3-CS.Tests\SDL3-CS.Tests.csproj -c Release --no-build --no-restore
```

If the active release uses another native package revision, pass that published revision instead. Native packaging and release-tool changes require the focused checks documented alongside the affected scripts and workflows.

## Commits and Pull Requests

- Write commit subjects and bodies in English.
- Keep each commit reviewable and limited to one coherent concern.
- Link the related issue with `Fixes #123`, `Closes #123`, or `Refs #123` when appropriate.
- Complete the pull request template and list the exact commands you ran.
- Include tests for behavior changes and update public documentation when users need new instructions.
- Do not include generated build output, local IDE settings, credentials, tokens, or native build artifacts.

By contributing, you agree that your work will be distributed under the repository's [zlib license](LICENSE).
