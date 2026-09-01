using System.Buffers.Binary;

namespace AndroidAvaloniaMixerAudio;

internal static class WaveFactory
{
    public static byte[] CreateSineWaveWav(int sampleRate, double frequency, TimeSpan duration)
    {
        const short channels = 1;
        const short bitsPerSample = 16;
        int sampleCount = checked((int)(sampleRate * duration.TotalSeconds));
        int dataLength = checked(sampleCount * channels * (bitsPerSample / 8));
        byte[] wave = new byte[44 + dataLength];
        Span<byte> bytes = wave;

        "RIFF"u8.CopyTo(bytes);
        BinaryPrimitives.WriteInt32LittleEndian(bytes[4..], 36 + dataLength);
        "WAVEfmt "u8.CopyTo(bytes[8..]);
        BinaryPrimitives.WriteInt32LittleEndian(bytes[16..], 16);
        BinaryPrimitives.WriteInt16LittleEndian(bytes[20..], 1);
        BinaryPrimitives.WriteInt16LittleEndian(bytes[22..], channels);
        BinaryPrimitives.WriteInt32LittleEndian(bytes[24..], sampleRate);
        BinaryPrimitives.WriteInt32LittleEndian(bytes[28..], sampleRate * channels * (bitsPerSample / 8));
        BinaryPrimitives.WriteInt16LittleEndian(bytes[32..], (short)(channels * (bitsPerSample / 8)));
        BinaryPrimitives.WriteInt16LittleEndian(bytes[34..], bitsPerSample);
        "data"u8.CopyTo(bytes[36..]);
        BinaryPrimitives.WriteInt32LittleEndian(bytes[40..], dataLength);

        for (int index = 0; index < sampleCount; index++)
        {
            double phase = 2.0 * Math.PI * frequency * index / sampleRate;
            short sample = (short)(Math.Sin(phase) * short.MaxValue * 0.20);
            BinaryPrimitives.WriteInt16LittleEndian(bytes[(44 + index * 2)..], sample);
        }

        return wave;
    }
}
