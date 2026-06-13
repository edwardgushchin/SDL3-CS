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

namespace Audio_Simple_Playback_Callback;

internal static class Program
{
    private const int WindowWidth = 640;
    private const int WindowHeight = 480;
    private const int SampleRate = 8000;
    private static readonly SDL.AudioStreamCallback FeedAudioCallback = FeedTheAudioStreamMore;
    private static IntPtr _stream;
    private static int _currentSineSample;

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Example Simple Audio Playback Callback",
            "com.example.audio-simple-playback-callback",
            "examples/audio/simple-playback-callback",
            WindowWidth,
            WindowHeight,
            RenderFrame,
            Configure,
            Cleanup,
            initFlags: SDL.InitFlags.Video | SDL.InitFlags.Audio);
    }

    private static void Configure(RendererExampleContext context)
    {
        var spec = new SDL.AudioSpec
        {
            Channels = 1,
            Format = AudioExampleData.NativeAudioF32,
            Freq = SampleRate
        };

        _stream = SDL.OpenAudioDeviceStream(SDL.AudioDeviceDefaultPlayback, in spec, FeedAudioCallback, IntPtr.Zero);
        if (_stream == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Couldn't create audio stream: {SDL.GetError()}");
        }

        SDL.ResumeAudioStreamDevice(_stream);
    }

    private static void FeedTheAudioStreamMore(IntPtr userdata, IntPtr audioStream, int additionalAmount, int totalAmount)
    {
        var additionalSamples = additionalAmount / sizeof(float);
        while (additionalSamples > 0)
        {
            var total = Math.Min(additionalSamples, 128);
            var samples = AudioExampleData.GenerateSineF32Bytes(total, 440, SampleRate, ref _currentSineSample);
            SDL.PutAudioStreamData(audioStream, samples, samples.Length);
            additionalSamples -= total;
        }
    }

    private static void RenderFrame(RendererExampleContext context, double now)
    {
        SDL.SetRenderDrawColor(context.Renderer, 0, 0, 0, 255);
        SDL.RenderClear(context.Renderer);
        SDL.RenderPresent(context.Renderer);
    }

    private static void Cleanup(RendererExampleContext context)
    {
        if (_stream == IntPtr.Zero)
        {
            return;
        }

        SDL.DestroyAudioStream(_stream);
        _stream = IntPtr.Zero;
    }
}
