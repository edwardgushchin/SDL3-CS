# SDL3-CS Tests

<p align="center">
  <img alt="test project" src="https://img.shields.io/badge/project-tests-555?style=flat-square">
  <img alt=".NET 8" src="https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square">
  <img alt="C# 14" src="https://img.shields.io/badge/C%23-14-239120?style=flat-square">
</p>

`SDL3-CS.Tests` is the focused verification project for the managed wrapper. It is intentionally separate from the shipped package and mirrors the wrapper source tree so each wrapper function can be tested close to its public API location.

## What This Project Covers

- Metadata and ABI checks for generated or native entry-point bindings.
- Managed wrapper behavior for success, failure, null, invalid input, marshalling, and ownership paths.
- Struct layout, enum values, callback lifetimes, and helper APIs that must match SDL ABI expectations.
- Regression tests for GitHub issues and release audits.

## Project Model

| Setting | Value |
|---------|-------|
| Target framework | `net8.0` |
| Output type | `Exe` |
| Language | C# 14 |
| Unsafe code | enabled |
| Main dependency | [`SDL3-CS`](../SDL3-CS/README.md) project reference |

The test layout should mirror wrapper source paths. For example, tests for `SDL3-CS/Mixer/PInvoke.cs` belong in `SDL3-CS.Tests/Mixer/PInvoke.cs`.

## Run

From the repository root:

```powershell
dotnet build .\SDL3-CS.Tests\SDL3-CS.Tests.csproj -c Release
dotnet run --project .\SDL3-CS.Tests\SDL3-CS.Tests.csproj -c Release --no-build
```

For wrapper documentation work, also run:

```powershell
pwsh .\.github\release-tools\Test-PublicWrapperXmlDocs.ps1 -SourceRoot .\SDL3-CS
```

## Coverage Expectations

Repository policy requires every C# method declaration in the wrapper project to have meaningful automated coverage. Tests should prove behavior, ABI shape, or native metadata rather than only assigning delegates or checking that a method exists.

When a direct native call is unsafe on a developer workstation, prefer metadata, signature, marshalling, and controlled failure-path assertions that still prove the wrapper contract.

## Native Runtime Notes

Some tests can execute without native SDL libraries because they inspect metadata or managed behavior. Runtime tests that call SDL need compatible native binaries available to the test output for the current host platform.

## Related Documentation

- [Repository README](../README.md)
- [Managed wrapper project](../SDL3-CS/README.md)
- [SDL3-CS Wiki](https://github.com/edwardgushchin/SDL3-CS/wiki)
