#region License
/* Copyright (c) 2024-2025 Eduard Gushchin.
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

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// <para>Define an <see cref="AudioFormat"/> value.</para>
    /// <para>SDL does not support custom audio formats, so this macro is not of much
    /// use externally, but it can be illustrative as to what the various bits of
    /// an <see cref="AudioFormat"/> mean.</para>
    /// <para>For example, <see cref="AudioFormat.AudioS32LE"/> looks like this:</para>
    /// <code>DefineAudioFormat(1, 0, 0, 32)</code>
    /// </summary>
    /// <param name="signed">1 for signed data, 0 for unsigned data.</param>
    /// <param name="bigendian">1 for bigendian data, 0 for littleendian data.</param>
    /// <param name="flt">1 for floating point data, 0 for integer data.</param>
    /// <param name="size">number of bits per sample.</param>
    /// <returns>a format value in the style of <see cref="AudioFormat"/>.</returns>
    /// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
    /// <since>This macro is available since SDL 3.2.0</since>
    [Macro]
    public static ushort DefineAudioFormat(bool signed, bool bigendian, bool flt, int size)
    {
        ushort format = 0;

        if (signed)
            format |= (1 << 15);

        if (bigendian)
            format |= (1 << 12);

        if (flt)
            format |= (1 << 8);

        format |= (ushort)(size & AudioMaskBitSize);

        return format;
    }

    
    /// <summary>
    /// <para>Retrieve the size, in bits, from an <see cref="AudioFormat"/>.</para>
    /// <para>For example, <c>AudioBitSize(AudioFormat.AudioS16)</c> returns 16.</para>
    /// </summary>
    /// <param name="x">an <see cref="AudioFormat"/> value.</param>
    /// <returns>data size in bits.</returns>
    /// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
    /// <since>This macro is available since SDL 3.2.0</since>
    [Macro]
    public static uint AudioBitSize(uint x) => x & AudioMaskBitSize;

    
    /// <summary>
    /// <para>Retrieve the size, in bytes, from an <see cref="AudioFormat"/>.</para>
    /// <para>For example, <c>AudioByteSize(AudioFormat.AudioS16)</c> returns 2.</para>
    /// </summary>
    /// <param name="x">an <see cref="AudioFormat"/> value.</param>
    /// <returns>data size in bytes.</returns>
    /// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
    /// <since>This macro is available since SDL 3.2.0</since>
    [Macro]
    public static uint AudioByteSize(uint x) => AudioBitSize(x) / 8;
    
    
    /// <summary>
    /// <para>Determine if an <see cref="AudioFormat"/> represents floating point data.</para>
    /// <para>For example, <c>AudioIsFloat(AudioFormat.AudioS16)</c> returns 0.</para>
    /// </summary>
    /// <param name="x">an <see cref="AudioFormat"/> value.</param>
    /// <returns>non-zero if format is floating point, zero otherwise.</returns>
    /// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
    /// <since>This macro is available since SDL 3.2.0</since>
    [Macro]
    public static bool AudioIsFloat(uint x) => (x & AudioMaskFloat) != 0;
    
    
    /// <summary>
    /// <para>Determine if an <see cref="AudioFormat"/> represents bigendian data.</para>
    /// <para>For example, <c>AudioIsBitEndian(AudioFormat.AudioS16LE)</c> returns 0.</para>
    /// </summary>
    /// <param name="x">an <see cref="AudioFormat"/> value.</param>
    /// <returns>non-zero if format is bigendian, zero otherwise.</returns>
    /// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
    /// <since>This macro is available since SDL 3.2.0</since>
    [Macro]
    public static bool AudioIsBigEndian(uint x) => (x & AudioMaskBigEndian) != 0;
    
    
    /// <summary>
    /// <para>Determine if an <see cref="AudioFormat"/> represents littleendian data.</para>
    /// <para>For example, <c>AudioIsLittleEndian(AudioFormat.AudioS16BE)</c> returns 0.</para>
    /// </summary>
    /// <param name="x">an <see cref="AudioFormat"/> value.</param>
    /// <returns>non-zero if format is littleendian, zero otherwise.</returns>
    /// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
    /// <since>This macro is available since SDL 3.2.0</since>
    [Macro]
    public static bool AudioIsLittleEndian(uint x) => !AudioIsBigEndian(x);
    
    
    /// <summary>
    /// <para>Determine if an <see cref="AudioFormat"/> represents signed data.</para>
    /// <para>For example, <c>AudioIsSigned(AudioFormat.AudioU8)</c> returns 0.</para>
    /// </summary>
    /// <param name="x">an <see cref="AudioFormat"/> value.</param>
    /// <returns>non-zero if format is signed, zero otherwise.</returns>
    /// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
    /// <since>This macro is available since SDL 3.2.0</since>
    [Macro]
    public static bool AudioIsSigned(uint x) => (x & AudioMaskSigned) != 0;
    
    
    /// <summary>
    /// <para>Determine if an <see cref="AudioFormat"/> represents integer data.</para>
    /// <para>For example, <c>AudioIsInt(AudioFormat.AudioF32)</c> returns 0.</para>
    /// </summary>
    /// <param name="x">an <see cref="AudioFormat"/> value.</param>
    /// <returns>non-zero if format is integer, zero otherwise.</returns>
    /// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
    /// <since>This macro is available since SDL 3.2.0</since>
    [Macro]
    public static bool AudioIsInt(uint x) => !AudioIsFloat(x);
    
    
    /// <summary>
    /// <para>Determine if an <see cref="AudioFormat"/> represents unsigned data.</para>
    /// <para>For example, <c>AudioIsUnsigned(AudioFormat.AudioS16)</c> returns 0.</para>
    /// </summary>
    /// <param name="x">an <see cref="AudioFormat"/> value.</param>
    /// <returns>non-zero if format is unsigned, zero otherwise.</returns>
    /// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
    /// <since>This macro is available since SDL 3.2.0</since>
    [Macro]
    public static bool AudioIsUnsigned(uint x) => !AudioIsSigned(x);
    
    
    /// <summary>
    /// <para>Calculate the size of each audio frame (in bytes) from an <see cref="AudioSpec"/>.</para>
    /// <para>This reports on the size of an audio sample frame: stereo Sint16 data (2
    /// channels of 2 bytes each) would be 4 bytes per frame, for example.</para>
    /// </summary>
    /// <param name="x">an <see cref="AudioSpec"/> to query.</param>
    /// <returns>the number of bytes used per sample frame.</returns>
    /// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
    /// <since>This macro is available since SDL 3.2.0</since>
    [Macro]
    public static uint AudioFrameSize(AudioSpec x) => (uint) (AudioByteSize((uint) x.Format) * x.Channels);
}