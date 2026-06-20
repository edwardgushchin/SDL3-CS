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

namespace Audio_Multiple_Streams;

internal static class Program
{
    private const int WindowWidth = 640;
    private const int WindowHeight = 480;
    private static readonly Sound[] Sounds = [new("sample.wav", 330), new("sword.wav", 660)];
    private static uint _audioDevice;

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Example Audio Multiple Streams",
            "com.example.audio-multiple-streams",
            "examples/audio/multiple-streams",
            WindowWidth,
            WindowHeight,
            RenderFrame,
            Configure,
            Cleanup,
            initFlags: SDL.InitFlags.Video | SDL.InitFlags.Audio);
    }

    private static void Configure(RendererExampleContext context)
    {
        _audioDevice = SDL.OpenAudioDevice(SDL.AudioDeviceDefaultPlayback, IntPtr.Zero);
        if (_audioDevice == 0)
        {
            throw new InvalidOperationException($"Couldn't open audio device: {SDL.GetError()}");
        }

        foreach (var sound in Sounds)
        {
            sound.Init(_audioDevice);
        }
    }

    private static void RenderFrame(RendererExampleContext context, double now)
    {
        foreach (var sound in Sounds)
        {
            sound.QueueLoopChunk();
        }

        SDL.SetRenderDrawColor(context.Renderer, 0, 0, 0, 255);
        SDL.RenderClear(context.Renderer);
        SDL.RenderPresent(context.Renderer);
    }

    private static void Cleanup(RendererExampleContext context)
    {
        if (_audioDevice != 0)
        {
            SDL.CloseAudioDevice(_audioDevice);
            _audioDevice = 0;
        }

        foreach (var sound in Sounds)
        {
            sound.Dispose();
        }
    }

    private sealed class Sound : IDisposable
    {
        private readonly string _fileName;
        private readonly int _frequency;
        private IntPtr _wavData;
        private uint _wavDataLength;
        private IntPtr _stream;

        public Sound(string fileName, int frequency)
        {
            _fileName = fileName;
            _frequency = frequency;
        }

        public void Init(uint audioDevice)
        {
            var wavPath = AudioExampleData.EnsureGeneratedWav(_fileName, _frequency, 700);
            if (!SDL.LoadWAV(wavPath, out var spec, out _wavData, out _wavDataLength))
            {
                throw new InvalidOperationException($"Couldn't load .wav file: {SDL.GetError()}");
            }

            _stream = AudioExampleData.CreateAudioStreamWithSource(in spec);
            if (_stream == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Couldn't create audio stream: {SDL.GetError()}");
            }

            if (!SDL.BindAudioStream(audioDevice, _stream))
            {
                throw new InvalidOperationException($"Failed to bind '{_fileName}' stream to device: {SDL.GetError()}");
            }
        }

        public void QueueLoopChunk()
        {
            if (_stream != IntPtr.Zero && SDL.GetAudioStreamQueued(_stream) < (int)_wavDataLength)
            {
                SDL.PutAudioStreamData(_stream, _wavData, (int)_wavDataLength);
            }
        }

        public void Dispose()
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
}
