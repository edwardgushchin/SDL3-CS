#region License
/* Copyright (c) 2024-2026 Eduard Gushchin.
 *
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from
 * the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you must not
 * claim that you wrote the original software. If you use this software in a
 * product, an acknowledgment in the product documentation would be
 * appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not be
 * misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source distribution.
 */
#endregion

using System.Runtime.InteropServices;
using SDL3;
using SDL3.Examples.Common;

namespace Demo_Bytepusher;

internal static class Program
{
    private const int ScreenWidth = 256;
    private const int ScreenHeight = 256;
    private const int RamSize = 0x1000000;
    private const int FramesPerSecond = 60;
    private const int SamplesPerFrame = 256;
    private const ulong NsPerSecond = 1_000_000_000UL;
    private const int MaxAudioLatencyFrames = 5;
    private const int IoKeyboard = 0;
    private const int IoPc = 2;
    private const int IoScreenPage = 5;
    private const int IoAudioBank = 6;
    private static readonly byte[] Ram = new byte[RamSize + 8];
    private static readonly byte[] Screen = new byte[ScreenWidth * ScreenHeight];
    private static IntPtr _texture;
    private static IntPtr _palette;
    private static IntPtr _audioStream;
    private static ulong _lastTick;
    private static ulong _tickAccumulator;
    private static ushort _keyState;
    private static int _statusTicks;
    private static string _status = "";
    private static bool _displayHelp = true;
    private static bool _positionalInput;

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "SDL 3 BytePusher",
            "com.example.SDL3BytePusher",
            "SDL 3 BytePusher",
            ScreenWidth * 2,
            ScreenHeight * 2,
            RenderFrame,
            Configure,
            Cleanup,
            HandleEvent,
            SDL.InitFlags.Video | SDL.InitFlags.Audio,
            SDL.RendererLogicalPresentation.IntegerScale);
    }

    private static void Configure(RendererExampleContext context)
    {
        _texture = SDL.CreateTexture(context.Renderer, SDL.PixelFormat.Index8, SDL.TextureAccess.Streaming, ScreenWidth, ScreenHeight);
        if (_texture == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Couldn't create BytePusher texture: {SDL.GetError()}");
        }

        _palette = SDL.CreatePalette(256);
        if (_palette == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Couldn't create BytePusher palette: {SDL.GetError()}");
        }

        var colors = new SDL.Color[256];
        var index = 0;
        for (var r = 0; r < 6; r++)
        {
            for (var g = 0; g < 6; g++)
            {
                for (var b = 0; b < 6; b++, index++)
                {
                    colors[index] = new SDL.Color { R = (byte)(r * 0x33), G = (byte)(g * 0x33), B = (byte)(b * 0x33), A = 255 };
                }
            }
        }

        if (!SDL.SetPaletteColors(_palette, colors, 0, colors.Length) || !SDL.SetTexturePalette(_texture, _palette))
        {
            throw new InvalidOperationException($"Couldn't configure BytePusher palette: {SDL.GetError()}");
        }

        SDL.SetTextureScaleMode(_texture, SDL.ScaleMode.Nearest);

        var spec = new SDL.AudioSpec
        {
            Format = SDL.AudioFormat.AudioS8,
            Channels = 1,
            Freq = SamplesPerFrame * FramesPerSecond
        };
        _audioStream = SDL.OpenAudioDeviceStream(SDL.AudioDeviceDefaultPlayback, in spec, null, IntPtr.Zero);
        if (_audioStream == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Couldn't create BytePusher audio stream: {SDL.GetError()}");
        }

        SDL.SetAudioStreamGain(_audioStream, 0.1f);
        SDL.ResumeAudioStreamDevice(_audioStream);
        SetStatus($"renderer: {SDL.GetRendererName(context.Renderer) ?? "unknown"}");
        _lastTick = SDL.GetTicksNS();
        _tickAccumulator = NsPerSecond;
    }

    private static bool HandleEvent(SDL.Event sdlEvent)
    {
        switch ((SDL.EventType)sdlEvent.Type)
        {
            case SDL.EventType.DropFile:
                LoadDroppedFile(sdlEvent.Drop.Data);
                break;

            case SDL.EventType.KeyDown:
                if (sdlEvent.Key.Key == SDL.Keycode.Escape)
                {
                    return false;
                }

                if (sdlEvent.Key.Key == SDL.Keycode.Return)
                {
                    _positionalInput = !_positionalInput;
                    _keyState = 0;
                    SetStatus(_positionalInput ? "switched to positional input" : "switched to symbolic input");
                }

                _keyState |= _positionalInput ? ScancodeMask(sdlEvent.Key.Scancode) : KeycodeMask(sdlEvent.Key.Key);
                break;

            case SDL.EventType.KeyUp:
                _keyState = (ushort)(_keyState & ~(_positionalInput ? ScancodeMask(sdlEvent.Key.Scancode) : KeycodeMask(sdlEvent.Key.Key)));
                break;
        }

        return true;
    }

    private static void RenderFrame(RendererExampleContext context, double now)
    {
        var tick = SDL.GetTicksNS();
        var delta = tick - _lastTick;
        _lastTick = tick;
        _tickAccumulator += delta * FramesPerSecond;
        var updated = _tickAccumulator >= NsPerSecond;
        var skipAudio = _tickAccumulator >= MaxAudioLatencyFrames * NsPerSecond;

        if (skipAudio)
        {
            SDL.ClearAudioStream(_audioStream);
        }

        while (_tickAccumulator >= NsPerSecond)
        {
            _tickAccumulator -= NsPerSecond;
            Ram[IoKeyboard] = (byte)(_keyState >> 8);
            Ram[IoKeyboard + 1] = (byte)_keyState;

            var pc = ReadU24(IoPc);
            for (var i = 0; i < ScreenWidth * ScreenHeight; i++)
            {
                var src = ReadU24(pc);
                var dst = ReadU24(pc + 3);
                Ram[dst] = Ram[src];
                pc = ReadU24(pc + 6);
            }

            if (!skipAudio || _tickAccumulator < NsPerSecond)
            {
                var audioOffset = ReadU16(IoAudioBank) << 8;
                SDL.PutAudioStreamData(_audioStream, Ram.AsSpan(audioOffset, SamplesPerFrame), SamplesPerFrame);
            }
        }

        if (updated && !_displayHelp)
        {
            var screenOffset = Ram[IoScreenPage] << 16;
            Buffer.BlockCopy(Ram, screenOffset, Screen, 0, Screen.Length);
            SDL.UpdateTexture(_texture, IntPtr.Zero, Screen, ScreenWidth);
        }

        SDL.SetRenderDrawColor(context.Renderer, 0, 0, 0, 255);
        SDL.RenderClear(context.Renderer);

        if (_displayHelp)
        {
            Print(context.Renderer, 4, 4, "Drop a BytePusher file in this");
            Print(context.Renderer, 8, 12, "window to load and run it!");
            Print(context.Renderer, 4, 28, "Press ENTER to switch between");
            Print(context.Renderer, 8, 36, "positional and symbolic input.");
        }
        else
        {
            SDL.RenderTexture(context.Renderer, _texture, IntPtr.Zero, IntPtr.Zero);
        }

        if (_statusTicks > 0)
        {
            if (updated)
            {
                _statusTicks--;
            }

            Print(context.Renderer, 4, ScreenHeight - 12, _status);
        }

        SDL.RenderPresent(context.Renderer);
    }

    private static void LoadDroppedFile(IntPtr data)
    {
        if (data == IntPtr.Zero)
        {
            return;
        }

        var path = Marshal.PtrToStringUTF8(data);
        SDL.Free(data);
        if (string.IsNullOrWhiteSpace(path))
        {
            return;
        }

        try
        {
            var bytes = File.ReadAllBytes(path);
            Array.Clear(Ram);
            Array.Copy(bytes, Ram, Math.Min(bytes.Length, RamSize));
            SDL.ClearAudioStream(_audioStream);
            _displayHelp = false;
            SetStatus($"loaded {Path.GetFileName(path)}");
        }
        catch (Exception)
        {
            _displayHelp = true;
            SetStatus($"load failed: {Path.GetFileName(path)}");
        }
    }

    private static ushort ReadU16(int address)
    {
        return (ushort)((Ram[address] << 8) | Ram[address + 1]);
    }

    private static int ReadU24(int address)
    {
        return (Ram[address] << 16) | (Ram[address + 1] << 8) | Ram[address + 2];
    }

    private static ushort KeycodeMask(SDL.Keycode key)
    {
        return key switch
        {
            >= SDL.Keycode.Alpha0 and <= SDL.Keycode.Alpha9 => (ushort)(1 << ((int)key - (int)SDL.Keycode.Alpha0)),
            >= SDL.Keycode.A and <= SDL.Keycode.F => (ushort)(1 << ((int)key - (int)SDL.Keycode.A + 10)),
            _ => 0
        };
    }

    private static ushort ScancodeMask(SDL.Scancode scancode)
    {
        var index = scancode switch
        {
            SDL.Scancode.Alpha1 => 0x1,
            SDL.Scancode.Alpha2 => 0x2,
            SDL.Scancode.Alpha3 => 0x3,
            SDL.Scancode.Alpha4 => 0xC,
            SDL.Scancode.Q => 0x4,
            SDL.Scancode.W => 0x5,
            SDL.Scancode.E => 0x6,
            SDL.Scancode.R => 0xD,
            SDL.Scancode.A => 0x7,
            SDL.Scancode.S => 0x8,
            SDL.Scancode.D => 0x9,
            SDL.Scancode.F => 0xE,
            SDL.Scancode.Z => 0xA,
            SDL.Scancode.X => 0x0,
            SDL.Scancode.C => 0xB,
            SDL.Scancode.V => 0xF,
            _ => -1
        };

        return index < 0 ? (ushort)0 : (ushort)(1 << index);
    }

    private static void Print(IntPtr renderer, int x, int y, string text)
    {
        SDL.SetRenderDrawColor(renderer, 0, 0, 0, 255);
        SDL.RenderDebugText(renderer, x + 1, y + 1, text);
        SDL.SetRenderDrawColor(renderer, 255, 255, 255, 255);
        SDL.RenderDebugText(renderer, x, y, text);
        SDL.SetRenderDrawColor(renderer, 0, 0, 0, 255);
    }

    private static void SetStatus(string text)
    {
        _status = text;
        _statusTicks = FramesPerSecond * 3;
    }

    private static void Cleanup(RendererExampleContext context)
    {
        if (_audioStream != IntPtr.Zero)
        {
            SDL.DestroyAudioStream(_audioStream);
            _audioStream = IntPtr.Zero;
        }

        if (_texture != IntPtr.Zero)
        {
            SDL.DestroyTexture(_texture);
            _texture = IntPtr.Zero;
        }

        if (_palette != IntPtr.Zero)
        {
            SDL.DestroyPalette(_palette);
            _palette = IntPtr.Zero;
        }
    }
}
