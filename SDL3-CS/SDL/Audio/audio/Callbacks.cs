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

using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    /// <code>typedef void (SDLCALL *SDL_AudioStreamCallback)(void *userdata, SDL_AudioStream *stream, int additional_amount, int total_amount);</code>
    /// <summary>
    /// <para>A callback that fires when data passes through an SDL_AudioStream.</para>
    /// <para>Apps can (optionally) register a callback with an audio stream that is
    /// called when data is added with <see cref="PutAudioStreamData(nint, byte[], int)"/>, or requested with
    /// <see cref="GetAudioStreamData(nint, byte[], int)"/>.</para>
    /// <para>Two values are offered here: one is the amount of additional data needed to
    /// satisfy the immediate request (which might be zero if the stream already
    /// has enough data queued) and the other is the total amount being requested.
    /// In a Get call triggering a Put callback, these values can be different. In
    /// a Put call triggering a Get callback, these values are always the same.</para>
    /// <para>Byte counts might be slightly overestimated due to buffering or resampling,
    /// and may change from call to call.</para>
    /// <para>This callback is not required to do anything. Generally this is useful for
    /// adding/reading data on demand, and the app will often put/get data as
    /// appropriate, but the system goes on with the data currently available to it
    /// if this callback does nothing.</para>
    /// </summary>
    /// <param name="userdata">an opaque pointer provided by the app for their personal
    /// use.</param>
    /// <param name="stream">the SDL audio stream associated with this callback.</param>
    /// <param name="additionalAmount">the amount of data, in bytes, that is needed right
    /// now.</param>
    /// <param name="totalAmount">the total amount of data requested, in bytes, that is
    /// requested or available.</param>
    /// <threadsafety>This callbacks may run from any thread, so if you need to
    /// protect shared data, you should use <see cref="LockAudioStream"/> to
    /// serialize access; this lock will be held before your callback
    /// is called, so your callback does not need to manage the lock
    /// explicitly.</threadsafety>
    /// <since>This datatype is available since SDL 3.2.0</since>
    /// <seealso cref="SetAudioStreamGetCallback"/>
    /// <seealso cref="SetAudioStreamPutCallback"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void AudioStreamCallback(IntPtr userdata, IntPtr stream, int additionalAmount, int totalAmount);
    
    
    /// <code>typedef void (SDLCALL *SDL_AudioPostmixCallback)(void *userdata, const SDL_AudioSpec *spec, float *buffer, int buflen);</code>
    /// <summary>
    /// <para>A callback that fires when data is about to be fed to an audio device.</para>
    /// <para>This is useful for accessing the final mix, perhaps for writing a
    /// visualizer or applying a final effect to the audio data before playback.</para>
    /// <para>This callback should run as quickly as possible and not block for any
    /// significant time, as this callback delays submission of data to the audio
    /// device, which can cause audio playback problems.</para>
    /// <para>The postmix callback _must_ be able to handle any audio data format
    /// specified in <c>spec</c>, which can change between callbacks if the audio device
    /// changed. However, this only covers frequency and channel count; data is
    /// always provided here in SDL_AUDIO_F32 format.</para>
    /// <para>The postmix callback runs _after_ logical device gain and audiostream gain
    /// have been applied, which is to say you can make the output data louder at
    /// this point than the gain settings would suggest.</para>
    /// </summary>
    /// <param name="userdata">a pointer provided by the app through
    /// <see cref="AudioPostmixCallback"/>, for its own use.</param>
    /// <param name="spec">the current format of audio that is to be submitted to the
    /// audio device.</param>
    /// <param name="buffer">the buffer of audio samples to be submitted. The callback can
    /// inspect and/or modify this data.</param>
    /// <param name="buflen">the size of <c>buffer</c> in bytes.</param>
    /// <threadsafety>This will run from a background thread owned by SDL. The
    /// application is responsible for locking resources the callback
    /// touches that need to be protected.</threadsafety>
    /// <since>This datatype is available since SDL 3.2.0</since>
    /// <seealso cref="SetAudioPostmixCallback"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void AudioPostmixCallback(IntPtr userdata, in AudioSpec spec, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] float[] buffer, int buflen);
    
    
    /// <code>typedef void (SDLCALL *SDL_AudioStreamDataCompleteCallback)(void *userdata, const void *buf, int buflen);</code>
    /// <summary>
    /// <para>A callback that fires for completed <see cref="PutAudioStreamDataNoCopy"/> data.</para>
    /// <para>When using <see cref="PutAudioStreamDataNoCopy"/> to provide data to an
    /// SDL_AudioStream, it's not safe to dispose of the data until the stream has
    /// completely consumed it. Often times it's difficult to know exactly when
    /// this has happened.</para>
    /// <para>This callback fires once when the stream no longer needs the buffer,
    /// allowing the app to easily free or reuse it.</para>
    /// </summary>
    /// <param name="userdata">an opaque pointer provided by the app for their personal
    /// use.</param>
    /// <param name="buflen">the size of buffer, in bytes, provided to
    /// <see cref="PutAudioStreamDataNoCopy"/>.</param>
    /// <param name="buf">the pointer provided to <see cref="PutAudioStreamDataNoCopy"/>.</param>
    /// <threadsafety>This callbacks may run from any thread, so if you need to
    /// protect shared data, you should use SDL_LockAudioStream to
    /// serialize access; this lock will be held before your callback
    /// is called, so your callback does not need to manage the lock
    /// explicitly.</threadsafety>
    /// <since>This datatype is available since SDL 3.4.0.</since>
    /// <seealso cref="SetAudioStreamGetCallback"/>
    /// <seealso cref="SetAudioStreamPutCallback"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void AudioStreamDataCompleteCallback(IntPtr userdata, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buf, int buflen);
}
