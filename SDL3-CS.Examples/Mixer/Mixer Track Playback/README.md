# Mixer Track Playback

<p align="center">
  <strong>Mixer Track Playback</strong>
</p>

<p align="center">
  <img alt="SDL3-CS example" src="https://img.shields.io/badge/SDL3-CS%20example-555?style=flat-square">
  <img alt="Mixer" src="https://img.shields.io/badge/Mixer-555?style=flat-square">
  <img alt="net10.0" src="https://img.shields.io/badge/net10.0-555?style=flat-square">
  <img alt="C# 14" src="https://img.shields.io/badge/C%23%2014-555?style=flat-square">
</p>

Mixer Track Playback demonstrates SDL_mixer track playback through SDL3-CS.

## What This Project Demonstrates

- Using the SDL_mixer native package from an example project.
- Loading `test.mp3` as an example asset.
- Opening mixer output and playing a track.
- Stopping playback and freeing mixer resources cleanly.

## Expected Result

The bundled MP3 track plays through SDL_mixer.

## Project Model

This project is part of the SDL3-CS examples tree. The common example configuration lives in [`../../Directory.Build.props`](../../Directory.Build.props) and [`../../Directory.Build.targets`](../../Directory.Build.targets):

- examples default to `net10.0`, `Exe`, nullable reference types, implicit usings, and C# 14;
- local source builds reference [`SDL3-CS`](../../../SDL3-CS/SDL3-CS.csproj) directly;
- desktop examples receive the matching native runtime package for Windows, Linux, or macOS;
- optional SDL companion runtimes are added only when an example opts into them.

Runtime dependency for this example: matching `SDL3-CS.<Platform>.Mixer` native package.

## Source Files

- `Program.cs`

## Assets

- `test.mp3`

## Requirements

- .NET 10 SDK.
- A platform supported by the SDL3-CS native packages.
- For desktop examples: Windows, Linux, or macOS with a graphics environment available.
- For device-specific examples: the matching device or host capability, such as camera, gamepad, joystick, pen, or audio output.

## Run

From the repository root:

```powershell
dotnet run --project ".\SDL3-CS.Examples\Mixer\Mixer Track Playback\Mixer Track Playback.csproj" -c Release
```

## Notes

- The example opts into `SDL3CSExampleUseMixer`, so `Directory.Build.targets` adds the platform-specific SDL_mixer package when it is run from this repository.

## Related Documentation

- [Repository README](../../../README.md)
- [Examples inventory](../../UPSTREAM-SDL3-EXAMPLES.md)
- [SDL3-CS Wiki](https://github.com/edwardgushchin/SDL3-CS/wiki)
- [Official SDL examples](https://examples.libsdl.org/SDL3/)
