using System.Reflection;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Audio.Audio;

internal static class MacroTests
{
    public static void DefineAudioFormat_ComposesFormatBits()
    {
        AssertMacro(nameof(SDL3.SDL.DefineAudioFormat));

        TestAssert.Equal((ushort)SDL3.SDL.AudioFormat.AudioU8, SDL3.SDL.DefineAudioFormat(signed: false, bigendian: false, flt: false, size: 8), "SDL.DefineAudioFormat must compose AudioU8.");
        TestAssert.Equal((ushort)SDL3.SDL.AudioFormat.AudioS8, SDL3.SDL.DefineAudioFormat(signed: true, bigendian: false, flt: false, size: 8), "SDL.DefineAudioFormat must compose AudioS8.");
        TestAssert.Equal((ushort)SDL3.SDL.AudioFormat.AudioS16LE, SDL3.SDL.DefineAudioFormat(signed: true, bigendian: false, flt: false, size: 16), "SDL.DefineAudioFormat must compose AudioS16LE.");
        TestAssert.Equal((ushort)SDL3.SDL.AudioFormat.AudioS16BE, SDL3.SDL.DefineAudioFormat(signed: true, bigendian: true, flt: false, size: 16), "SDL.DefineAudioFormat must compose AudioS16BE.");
        TestAssert.Equal((ushort)SDL3.SDL.AudioFormat.AudioF32LE, SDL3.SDL.DefineAudioFormat(signed: true, bigendian: false, flt: true, size: 32), "SDL.DefineAudioFormat must compose AudioF32LE.");
        TestAssert.Equal((ushort)SDL3.SDL.AudioFormat.AudioF32BE, SDL3.SDL.DefineAudioFormat(signed: true, bigendian: true, flt: true, size: 32), "SDL.DefineAudioFormat must compose AudioF32BE.");
    }

    public static void AudioBitSize_ReturnsLowByte()
    {
        AssertMacro(nameof(SDL3.SDL.AudioBitSize));

        TestAssert.Equal(16u, SDL3.SDL.AudioBitSize((uint)SDL3.SDL.AudioFormat.AudioS16LE), "SDL.AudioBitSize must read 16-bit format size.");
        TestAssert.Equal(32u, SDL3.SDL.AudioBitSize((uint)SDL3.SDL.AudioFormat.AudioF32BE), "SDL.AudioBitSize must read 32-bit format size.");
        TestAssert.Equal(0xCDu, SDL3.SDL.AudioBitSize(0xABCDu), "SDL.AudioBitSize must mask the low byte.");
    }

    public static void AudioByteSize_ReturnsBitSizeDividedByEight()
    {
        AssertMacro(nameof(SDL3.SDL.AudioByteSize));

        TestAssert.Equal(1u, SDL3.SDL.AudioByteSize((uint)SDL3.SDL.AudioFormat.AudioU8), "SDL.AudioByteSize must report 1 byte for U8.");
        TestAssert.Equal(2u, SDL3.SDL.AudioByteSize((uint)SDL3.SDL.AudioFormat.AudioS16LE), "SDL.AudioByteSize must report 2 bytes for S16.");
        TestAssert.Equal(4u, SDL3.SDL.AudioByteSize((uint)SDL3.SDL.AudioFormat.AudioF32LE), "SDL.AudioByteSize must report 4 bytes for F32.");
    }

    public static void AudioIsFloat_ReportsFloatFlag()
    {
        AssertMacro(nameof(SDL3.SDL.AudioIsFloat));

        TestAssert.Equal(true, SDL3.SDL.AudioIsFloat((uint)SDL3.SDL.AudioFormat.AudioF32LE), "SDL.AudioIsFloat must be true for F32.");
        TestAssert.Equal(false, SDL3.SDL.AudioIsFloat((uint)SDL3.SDL.AudioFormat.AudioS16LE), "SDL.AudioIsFloat must be false for integer formats.");
    }

    public static void AudioIsBigEndian_ReportsEndianFlag()
    {
        AssertMacro(nameof(SDL3.SDL.AudioIsBigEndian));

        TestAssert.Equal(true, SDL3.SDL.AudioIsBigEndian((uint)SDL3.SDL.AudioFormat.AudioS16BE), "SDL.AudioIsBigEndian must be true for BE formats.");
        TestAssert.Equal(false, SDL3.SDL.AudioIsBigEndian((uint)SDL3.SDL.AudioFormat.AudioS16LE), "SDL.AudioIsBigEndian must be false for LE formats.");
    }

    public static void AudioIsLittleEndian_InvertsBigEndianFlag()
    {
        AssertMacro(nameof(SDL3.SDL.AudioIsLittleEndian));

        TestAssert.Equal(true, SDL3.SDL.AudioIsLittleEndian((uint)SDL3.SDL.AudioFormat.AudioS16LE), "SDL.AudioIsLittleEndian must be true for LE formats.");
        TestAssert.Equal(false, SDL3.SDL.AudioIsLittleEndian((uint)SDL3.SDL.AudioFormat.AudioS16BE), "SDL.AudioIsLittleEndian must be false for BE formats.");
    }

    public static void AudioIsSigned_ReportsSignedFlag()
    {
        AssertMacro(nameof(SDL3.SDL.AudioIsSigned));

        TestAssert.Equal(true, SDL3.SDL.AudioIsSigned((uint)SDL3.SDL.AudioFormat.AudioS8), "SDL.AudioIsSigned must be true for signed formats.");
        TestAssert.Equal(false, SDL3.SDL.AudioIsSigned((uint)SDL3.SDL.AudioFormat.AudioU8), "SDL.AudioIsSigned must be false for unsigned formats.");
    }

    public static void AudioIsInt_InvertsFloatFlag()
    {
        AssertMacro(nameof(SDL3.SDL.AudioIsInt));

        TestAssert.Equal(true, SDL3.SDL.AudioIsInt((uint)SDL3.SDL.AudioFormat.AudioS32LE), "SDL.AudioIsInt must be true for integer formats.");
        TestAssert.Equal(false, SDL3.SDL.AudioIsInt((uint)SDL3.SDL.AudioFormat.AudioF32LE), "SDL.AudioIsInt must be false for float formats.");
    }

    public static void AudioIsUnsigned_InvertsSignedFlag()
    {
        AssertMacro(nameof(SDL3.SDL.AudioIsUnsigned));

        TestAssert.Equal(true, SDL3.SDL.AudioIsUnsigned((uint)SDL3.SDL.AudioFormat.AudioU8), "SDL.AudioIsUnsigned must be true for unsigned formats.");
        TestAssert.Equal(false, SDL3.SDL.AudioIsUnsigned((uint)SDL3.SDL.AudioFormat.AudioS8), "SDL.AudioIsUnsigned must be false for signed formats.");
    }

    public static void AudioFrameSize_MultipliesSampleBytesByChannels()
    {
        AssertMacro(nameof(SDL3.SDL.AudioFrameSize));

        SDL3.SDL.AudioSpec stereo16 = new()
        {
            Format = SDL3.SDL.AudioFormat.AudioS16LE,
            Channels = 2,
            Freq = 48000
        };

        SDL3.SDL.AudioSpec surroundFloat = new()
        {
            Format = SDL3.SDL.AudioFormat.AudioF32BE,
            Channels = 6,
            Freq = 48000
        };

        TestAssert.Equal(4u, SDL3.SDL.AudioFrameSize(stereo16), "SDL.AudioFrameSize must multiply 2-byte samples by 2 channels.");
        TestAssert.Equal(24u, SDL3.SDL.AudioFrameSize(surroundFloat), "SDL.AudioFrameSize must multiply 4-byte samples by 6 channels.");
    }

    private static void AssertMacro(string methodName)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, $"SDL.{methodName} method must be public static.");
        SDL3.SDL.MacroAttribute? macro = method!.GetCustomAttribute<SDL3.SDL.MacroAttribute>();
        TestAssert.NotNull(macro, $"SDL.{methodName} must keep Macro metadata.");
    }
}
