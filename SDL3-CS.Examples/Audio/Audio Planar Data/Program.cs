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

namespace Audio_Planar_Data;

internal static class Program
{
    private const int WindowWidth = 640;
    private const int WindowHeight = 480;
    private static readonly SDL.FRect LeftButtonRect = new() { X = 100, Y = 170, W = 100, H = 100 };
    private static readonly SDL.FRect RightButtonRect = new() { X = 440, Y = 170, W = 100, H = 100 };
    private static readonly byte[] Left = AudioExampleData.GenerateSineU8(1870, 440, 4000);
    private static readonly byte[] Right = AudioExampleData.GenerateSineU8(1777, 660, 4000);
    private static IntPtr _renderer;
    private static IntPtr _stream;
    private static int _playingSound;

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Example Audio Planar Data",
            "com.example.audio-planar-data",
            "examples/audio/planar-data",
            WindowWidth,
            WindowHeight,
            RenderFrame,
            Configure,
            Cleanup,
            HandleEvent,
            SDL.InitFlags.Video | SDL.InitFlags.Audio);
    }

    private static void Configure(RendererExampleContext context)
    {
        _renderer = context.Renderer;
        var spec = new SDL.AudioSpec
        {
            Format = SDL.AudioFormat.AudioU8,
            Channels = 2,
            Freq = 4000
        };

        _stream = SDL.OpenAudioDeviceStream(SDL.AudioDeviceDefaultPlayback, in spec, null, IntPtr.Zero);
        if (_stream == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Couldn't open audio device stream: {SDL.GetError()}");
        }

        SDL.ResumeAudioStreamDevice(_stream);
    }

    private static bool HandleEvent(SDL.Event sdlEvent)
    {
        if (_renderer != IntPtr.Zero)
        {
            SDL.ConvertEventToRenderCoordinates(_renderer, ref sdlEvent);
        }

        if ((SDL.EventType)sdlEvent.Type != SDL.EventType.MouseButtonDown || _playingSound != 0)
        {
            return true;
        }

        var point = new SDL.FPoint { X = sdlEvent.Button.X, Y = sdlEvent.Button.Y };
        if (PointInRect(point, LeftButtonRect))
        {
            PlayPlanar(left: Left, right: null, playingSound: -1);
        }
        else if (PointInRect(point, RightButtonRect))
        {
            PlayPlanar(left: null, right: Right, playingSound: 1);
        }

        return true;
    }

    private static void RenderFrame(RendererExampleContext context, double now)
    {
        if (_playingSound != 0 && SDL.GetAudioStreamQueued(_stream) == 0)
        {
            _playingSound = 0;
        }

        SDL.SetRenderDrawColor(context.Renderer, 0, 0, 0, 255);
        SDL.RenderClear(context.Renderer);
        RenderButton(context.Renderer, LeftButtonRect, "LEFT", -1);
        RenderButton(context.Renderer, RightButtonRect, "RIGHT", 1);
        SDL.RenderPresent(context.Renderer);
    }

    private static void PlayPlanar(byte[]? left, byte[]? right, int playingSound)
    {
        GCHandle leftHandle = default;
        GCHandle rightHandle = default;

        try
        {
            var sampleCount = left?.Length ?? right?.Length ?? 0;
            leftHandle = left is null ? default : GCHandle.Alloc(left, GCHandleType.Pinned);
            rightHandle = right is null ? default : GCHandle.Alloc(right, GCHandleType.Pinned);

            var planes = new[]
            {
                leftHandle.IsAllocated ? leftHandle.AddrOfPinnedObject() : IntPtr.Zero,
                rightHandle.IsAllocated ? rightHandle.AddrOfPinnedObject() : IntPtr.Zero
            };

            SDL.PutAudioStreamPlanarData(_stream, planes, -1, sampleCount);
            SDL.FlushAudioStream(_stream);
            _playingSound = playingSound;
        }
        finally
        {
            if (leftHandle.IsAllocated)
            {
                leftHandle.Free();
            }

            if (rightHandle.IsAllocated)
            {
                rightHandle.Free();
            }
        }
    }

    private static void RenderButton(IntPtr renderer, SDL.FRect rect, string text, int buttonValue)
    {
        if (_playingSound == buttonValue)
        {
            SDL.SetRenderDrawColor(renderer, 0, 255, 0, 255);
        }
        else
        {
            SDL.SetRenderDrawColor(renderer, 0, 0, 255, 255);
        }

        SDL.RenderFillRect(renderer, in rect);
        SDL.SetRenderDrawColor(renderer, 255, 255, 255, 255);

        var x = rect.X + ((rect.W - (SDL.DebugTextFontCharacterSize * text.Length)) / 2.0f);
        var y = rect.Y + ((rect.H - SDL.DebugTextFontCharacterSize) / 2.0f);
        SDL.RenderDebugText(renderer, x, y, text);
    }

    private static bool PointInRect(SDL.FPoint point, SDL.FRect rect)
    {
        return point.X >= rect.X && point.X <= rect.X + rect.W &&
               point.Y >= rect.Y && point.Y <= rect.Y + rect.H;
    }

    private static void Cleanup(RendererExampleContext context)
    {
        _renderer = IntPtr.Zero;

        if (_stream == IntPtr.Zero)
        {
            return;
        }

        SDL.DestroyAudioStream(_stream);
        _stream = IntPtr.Zero;
    }
}
