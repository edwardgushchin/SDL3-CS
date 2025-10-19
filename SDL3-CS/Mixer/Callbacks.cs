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

public partial class Mixer
{
    /// <code>typedef void (SDLCALL *MIX_TrackStoppedCallback)(void *userdata, MIX_Track *track);</code>
    /// <summary>
    /// A callback that fires when a MIX_Track is stopped.
    /// <para>This callback is fired when a track completes playback, either because it
    /// ran out of data to mix (and all loops were completed as well), or it was
    /// explicitly stopped by the app. Pausing a track will not fire this callback.</para>
    /// <para>It is legal to adjust the track, including changing its input and
    /// restarting it. If this is done because it ran out of data in the middle of
    /// mixing, the mixer will start mixing the new track state in its current run
    /// without any gap in the audio.</para>
    /// <para>This callback will not fire when a playing track is destroyed.</para>
    /// </summary>
    /// <param name="userdata">an opaque pointer provided by the app for its personal use.</param>
    /// <param name="track">the track that has stopped.</param>
    /// <since>This datatype is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="SetTrackStoppedCallback"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TrackStoppedCallback(IntPtr userdata, IntPtr track);
    
    /// <code>typedef void (SDLCALL *MIX_TrackMixCallback)(void *userdata, MIX_Track *track, const SDL_AudioSpec *spec, float *pcm, int samples);</code>
    /// <summary>
    /// <para>A callback that fires when a MIX_Track is mixing at various stages.</para>
    /// <para>This callback is fired for different parts of the mixing pipeline, and
    /// gives the app visbility into the audio data that is being generated at
    /// various stages.</para>
    /// <para>The audio data passed through here is _not_ const data; the app is
    /// permitted to change it in any way it likes, and those changes will
    /// propagate through the mixing pipeline.</para>
    /// <para>An audiospec is provided. Different tracks might be in different formats,
    /// and an app needs to be able to handle that, but SDL_mixer always does its
    /// mixing work in 32-bit float samples, even if the inputs or final output are
    /// not floating point. As such, <c>spec->format</c> will always be <c>SDL.AudioFormat.AudioF32</c>
    /// and <c>pcm</c> hardcoded to be a float pointer.</para>
    /// <para><c>samples</c> is the number of float values pointed to by <c>pcm</c>: samples, not
    /// sample frames! There are no promises how many samples will be provided
    /// per-callback, and this number can vary wildly from call to call, depending
    /// on many factors.</para>
    /// <para>Making changes to the track during this callback is undefined behavior.
    /// Change the data in <c>pcm</c> but not the track itself.</para>
    /// </summary>
    /// <param name="userdata">an opaque pointer provided by the app for its personal use.</param>
    /// <param name="track">the track that is being mixed.</param>
    /// <param name="spec">the format of the data in <c>pcm</c>.</param>
    /// <param name="pcm">the raw PCM data in float32 format.</param>
    /// <param name="samples">the number of float values pointed to by <c>pcm</c>.</param>
    /// <since>This datatype is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="SetTrackRawCallback"/>
    /// <seealso cref="SetTrackCookedCallback"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TrackMixCallback(IntPtr userdata, IntPtr track, IntPtr spec, IntPtr pcm, int samples);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_SetTrackRawCallback(MIX_Track *track, MIX_TrackMixCallback cb, void *userdata);</code>
    /// <summary>
    /// <para>Set a callback that fires when a MIX_Track has initial decoded audio.</para>
    /// <para>As a track needs to mix more data, it pulls from its input (a MIX_Audio, an
    /// SDL_AudioStream, etc). This input might be a compressed file format, like
    /// MP3, so a little more data is uncompressed from it.</para>
    /// <para>Once the track has PCM data to start operating on, it can fire a callback
    /// before _any_ changes to the raw PCM input have happened. This lets an app
    /// view the data before it has gone through transformations such as gain, 3D
    /// positioning, fading, etc. It can also change the data in any way it pleases
    /// during this callback, and the mixer will continue as if this data came
    /// directly from the input.</para>
    /// <para>Each track has its own unique raw callback.</para>
    /// <para>Passing a <c>null</c> callback here is legal; it disables this track's callback.</para>
    /// </summary>
    /// <param name="track">the track to assign this callback to.</param>
    /// <param name="cb">the function to call when the track mixes. May be <c>null</c>.</param>
    /// <param name="userdata">an opaque pointer provided to the callback for its own
    /// personal use.</param>
    /// <returns>true on success or false on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="TrackMixCallback"/>
    /// <seealso cref="SetTrackCookedCallback"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool SetTrackRawCallback(IntPtr track, TrackMixCallback cb, IntPtr userdata);
    
    
    /// <code>typedef void (SDLCALL *MIX_GroupMixCallback)(void *userdata, MIX_Group *group, const SDL_AudioSpec *spec, float *pcm, int samples);</code>
    /// <summary>
    /// <para>A callback that fires when a MIX_Group has completed mixing.</para>
    /// <para>This callback is fired when a mixing group has finished mixing: all tracks
    /// in the group have mixed into a single buffer and are prepared to be mixed
    /// into all other groups for the final mix output.</para>
    /// <para>The audio data passed through here is _not_ const data; the app is
    /// permitted to change it in any way it likes, and those changes will
    /// propagate through the mixing pipeline.</para>
    /// <para>An audiospec is provided. Different groups might be in different formats,
    /// and an app needs to be able to handle that, but SDL_mixer always does its
    /// mixing work in 32-bit float samples, even if the inputs or final output are
    /// not floating point. As such, <c>spec->format</c> will always be <c>SDL.AudioFormat.AudioF32</c>
    /// and <c>pcm</c> hardcoded to be a float pointer.</para>
    /// <para><c>samples</c> is the number of float values pointed to by <c>pcm</c>: samples, not
    /// sample frames! There are no promises how many samples will be provided
    /// per-callback, and this number can vary wildly from call to call, depending
    /// on many factors.</para>
    /// </summary>
    /// <param name="userdata">an opaque pointer provided by the app for its personal use.</param>
    /// <param name="group">the group that is being mixed.</param>
    /// <param name="spec">the format of the data in <c>pcm</c>.</param>
    /// <param name="pcm">the raw PCM data in float32 format.</param>
    /// <param name="samples">the number of float values pointed to by <c>pcm</c>.</param>
    /// <since>This datatype is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="SetGroupPostMixCallback"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void GroupMixCallback(IntPtr userdata, IntPtr group, IntPtr spec, IntPtr pcm, int samples);
    
    
    /// <code>typedef void (SDLCALL *MIX_PostMixCallback)(void *userdata, MIX_Mixer *mixer, const SDL_AudioSpec *spec, float *pcm, int samples);</code>
    /// <summary>
    /// <para>A callback that fires when all mixing has completed.</para>
    /// <para>This callback is fired when the mixer has completed all its work. If this
    /// mixer was created with <see cref="CreateMixerDevice"/>, the data provided by this
    /// callback is what is being sent to the audio hardware, minus last
    /// conversions for format requirements. If this mixer was created with
    /// <see cref="CreateMixer"/>, this is what is being output from <see cref="Generate"/>, after
    /// final conversions.</para>
    /// <para>The audio data passed through here is _not_ const data; the app is
    /// permitted to change it in any way it likes, and those changes will replace
    /// the final mixer pipeline output.</para>
    /// <para>An audiospec is provided. SDL_mixer always does its mixing work in 32-bit
    /// float samples, even if the inputs or final output are not floating point.
    /// As such, <c>spec->format</c> will always be <c>SDL.AudioFormat.AudioF32</c> and <c>pcm</c> hardcoded
    /// to be a float pointer.</para>
    /// <para><c>samples</c> is the number of float values pointed to by <c>pcm</c>: samples, not
    /// sample frames! There are no promises how many samples will be provided
    /// per-callback, and this number can vary wildly from call to call, depending
    /// on many factors.</para>
    /// </summary>
    /// <param name="userdata">an opaque pointer provided by the app for its personal use.</param>
    /// <param name="mixer">the mixer that is generating audio.</param>
    /// <param name="spec">the format of the data in <c>pcm</c>.</param>
    /// <param name="pcm">the raw PCM data in float32 format.</param>
    /// <param name="samples">the number of float values pointed to by <c>pcm</c>.</param>
    /// <since>This datatype is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="SetPostMixCallback"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void PostMixCallback(IntPtr userdata, IntPtr mixer, IntPtr spec, IntPtr pcm, int samples);
}