# Camera

<p align="center">
  <strong>Camera</strong>
</p>

<p align="center">
  <img alt="SDL3-CS example" src="https://img.shields.io/badge/SDL3-CS%20example-555?style=flat-square">
  <img alt="Camera" src="https://img.shields.io/badge/Camera-555?style=flat-square">
  <img alt="net10.0" src="https://img.shields.io/badge/net10.0-555?style=flat-square">
  <img alt="C# 14" src="https://img.shields.io/badge/C%23%2014-555?style=flat-square">
</p>

Camera lists and opens SDL camera devices from managed code.

## What This Project Demonstrates

- Initializing the SDL camera subsystem.
- Enumerating available cameras.
- Opening a selected camera safely.
- Reporting device availability and shutdown results.

## Expected Result

The console output reflects the camera devices available on the host.

## Project Model

This project is part of the SDL3-CS examples tree. The common example configuration lives in [`../../Directory.Build.props`](../../Directory.Build.props) and [`../../Directory.Build.targets`](../../Directory.Build.targets):

- examples default to `net10.0`, `Exe`, nullable reference types, implicit usings, and C# 14;
- local source builds reference [`SDL3-CS`](../../../SDL3-CS/SDL3-CS.csproj) directly;
- desktop examples receive the matching native runtime package for Windows, Linux, or macOS;
- optional SDL companion runtimes are added only when an example opts into them.

Runtime dependency for this example: base `SDL3-CS.<Platform>` native runtime package.

## Source Files

- `Program.cs`

## Assets

- No project-specific data assets are required.

## Requirements

- .NET 10 SDK.
- A platform supported by the SDL3-CS native packages.
- For desktop examples: Windows, Linux, or macOS with a graphics environment available.
- For device-specific examples: the matching device or host capability, such as camera, gamepad, joystick, pen, or audio output.

## Run

From the repository root:

```powershell
dotnet run --project ".\SDL3-CS.Examples\Camera\Camera\Camera.csproj" -c Release
```

## Notes

- The example follows the shared SDL3-CS examples build model and keeps native runtime selection in common MSBuild targets.

## Related Documentation

- [Repository README](../../../README.md)
- [Examples inventory](../../UPSTREAM-SDL3-EXAMPLES.md)
- [SDL3-CS Wiki](https://github.com/edwardgushchin/SDL3-CS/wiki)
- [Official SDL examples](https://examples.libsdl.org/SDL3/)
