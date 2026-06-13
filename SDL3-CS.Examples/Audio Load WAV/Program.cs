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

using SDL3;
using SDL3.Examples.Common;

namespace Audio_Load_WAV;

internal static class Program
{
    private const int WindowWidth = 640;
    private const int WindowHeight = 480;
    private static IntPtr _stream;
    private static IntPtr _wavData;
    private static uint _wavDataLength;

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Example Audio Load Wave",
            "com.example.audio-load-wav",
            "examples/audio/load-wav",
            WindowWidth,
            WindowHeight,
            RenderFrame,
            Configure,
            Cleanup,
            initFlags: SDL.InitFlags.Video | SDL.InitFlags.Audio);
    }

    private static void Configure(RendererExampleContext context)
    {
        var wavPath = AudioExampleData.EnsureGeneratedWav("sample.wav", 330, 1000);
        if (!SDL.LoadWAV(wavPath, out var spec, out _wavData, out _wavDataLength))
        {
            throw new InvalidOperationException($"Couldn't load .wav file: {SDL.GetError()}");
        }

        _stream = SDL.OpenAudioDeviceStream(SDL.AudioDeviceDefaultPlayback, in spec, null, IntPtr.Zero);
        if (_stream == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Couldn't create audio stream: {SDL.GetError()}");
        }

        SDL.ResumeAudioStreamDevice(_stream);
    }

    private static void RenderFrame(RendererExampleContext context, double now)
    {
        if (SDL.GetAudioStreamQueued(_stream) < (int)_wavDataLength)
        {
            SDL.PutAudioStreamData(_stream, _wavData, (int)_wavDataLength);
        }

        SDL.SetRenderDrawColor(context.Renderer, 0, 0, 0, 255);
        SDL.RenderClear(context.Renderer);
        SDL.RenderPresent(context.Renderer);
    }

    private static void Cleanup(RendererExampleContext context)
    {
        if (_stream != IntPtr.Zero)
        {
            SDL.DestroyAudioStream(_stream);
            _stream = IntPtr.Zero;
        }

        if (_wavData != IntPtr.Zero)
        {
            SDL.Free(_wavData);
            _wavData = IntPtr.Zero;
            _wavDataLength = 0;
        }
    }
}
