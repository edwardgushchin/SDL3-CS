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

using System.Buffers.Binary;
using System.Runtime.InteropServices;
using System.Text;
using SDL3;

namespace SDL3.Examples.Common;

internal static class AudioExampleData
{
    public static SDL.AudioFormat NativeAudioF32 => BitConverter.IsLittleEndian
        ? SDL.AudioFormat.AudioF32LE
        : SDL.AudioFormat.AudioF32BE;

    public static byte[] GenerateSineF32Bytes(int sampleCount, int frequency, int sampleRate, ref int currentSample)
    {
        var samples = new float[sampleCount];
        for (var i = 0; i < samples.Length; i++)
        {
            var phase = currentSample * frequency / (float)sampleRate;
            samples[i] = MathF.Sin(phase * 2.0f * MathF.PI);
            currentSample++;
        }

        currentSample %= sampleRate;

        var bytes = new byte[samples.Length * sizeof(float)];
        Buffer.BlockCopy(samples, 0, bytes, 0, bytes.Length);
        return bytes;
    }

    public static byte[] GenerateSineU8(int sampleCount, int frequency, int sampleRate)
    {
        var data = new byte[sampleCount];
        for (var i = 0; i < data.Length; i++)
        {
            var phase = i * frequency / (float)sampleRate;
            data[i] = (byte)Math.Clamp(128.0f + (MathF.Sin(phase * 2.0f * MathF.PI) * 80.0f), 0.0f, 255.0f);
        }

        return data;
    }

    public static string EnsureGeneratedWav(string fileName, int frequency, int durationMs)
    {
        var path = Path.Combine(AppContext.BaseDirectory, fileName);
        if (!File.Exists(path))
        {
            const int sampleRate = 8000;
            var sampleCount = sampleRate * durationMs / 1000;
            File.WriteAllBytes(path, CreateU8MonoWav(GenerateSineU8(sampleCount, frequency, sampleRate), sampleRate));
        }

        return path;
    }

    public static IntPtr CreateAudioStreamWithSource(in SDL.AudioSpec sourceSpec)
    {
        var sourceSpecPtr = Marshal.AllocHGlobal(Marshal.SizeOf<SDL.AudioSpec>());
        try
        {
            Marshal.StructureToPtr(sourceSpec, sourceSpecPtr, false);
            return SDL.CreateAudioStream(sourceSpecPtr, IntPtr.Zero);
        }
        finally
        {
            Marshal.FreeHGlobal(sourceSpecPtr);
        }
    }

    private static byte[] CreateU8MonoWav(byte[] pcm, int sampleRate)
    {
        const short channels = 1;
        const short bitsPerSample = 8;
        const short blockAlign = channels * bitsPerSample / 8;
        var byteRate = sampleRate * blockAlign;
        var wav = new byte[44 + pcm.Length];

        WriteAscii(wav.AsSpan(0, 4), "RIFF");
        BinaryPrimitives.WriteInt32LittleEndian(wav.AsSpan(4, 4), 36 + pcm.Length);
        WriteAscii(wav.AsSpan(8, 4), "WAVE");
        WriteAscii(wav.AsSpan(12, 4), "fmt ");
        BinaryPrimitives.WriteInt32LittleEndian(wav.AsSpan(16, 4), 16);
        BinaryPrimitives.WriteInt16LittleEndian(wav.AsSpan(20, 2), 1);
        BinaryPrimitives.WriteInt16LittleEndian(wav.AsSpan(22, 2), channels);
        BinaryPrimitives.WriteInt32LittleEndian(wav.AsSpan(24, 4), sampleRate);
        BinaryPrimitives.WriteInt32LittleEndian(wav.AsSpan(28, 4), byteRate);
        BinaryPrimitives.WriteInt16LittleEndian(wav.AsSpan(32, 2), blockAlign);
        BinaryPrimitives.WriteInt16LittleEndian(wav.AsSpan(34, 2), bitsPerSample);
        WriteAscii(wav.AsSpan(36, 4), "data");
        BinaryPrimitives.WriteInt32LittleEndian(wav.AsSpan(40, 4), pcm.Length);
        pcm.CopyTo(wav.AsSpan(44));

        return wav;
    }

    private static void WriteAscii(Span<byte> destination, string text)
    {
        Encoding.ASCII.GetBytes(text, destination);
    }
}
