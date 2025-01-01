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
    /// <code>typedef void (SDLCALL *Mix_MixCallback)(void *udata, Uint8 *stream, int len);</code>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MixCallback(IntPtr udata, IntPtr stream, int len);
    
    
    /// <code>typedef void (SDLCALL *Mix_MusicFinishedCallback)(void);</code>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MusicFinishedCallback();
    
    
    /// <code>typedef void (SDLCALL *Mix_ChannelFinishedCallback)(int channel);</code>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ChannelFinishedCallback(int channel);
    
    
    /// <code>typedef void (SDLCALL *Mix_EffectFunc_t)(int chan, void *stream, int len, void *udata);</code>
    /// <summary>
    /// <para>This is the format of a special effect callback:</para>
    /// <code>myeffect(int chan, void *stream, int len, void *udata);</code>
    /// <para>chan) is the channel number that your effect is affecting. (stream) is the
    /// buffer of data to work upon. (len) is the size of (stream), and (udata) is
    /// a user-defined bit of data, which you pass as the last arg of
    /// <see cref="RegisterEffect"/>, and is passed back unmolested to your callback. Your
    /// effect changes the contents of (stream) based on whatever parameters are
    /// significant, or just leaves it be, if you prefer. You can do whatever you
    /// like to the buffer, though, and it will continue in its changed state down
    /// the mixing pipeline, through any other effect functions, then finally to be
    /// mixed with the rest of the channels and music for the final output stream.</para>
    /// </summary>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void EffectFuncT(int chan, IntPtr stream, int len, IntPtr udata);
    
    
    /// <code>typedef void (SDLCALL *Mix_EffectDone_t)(int chan, void *udata);</code>
    /// <summary>
    /// <para>This is a callback that signifies that a channel has finished all its loops
    /// and has completed playback.</para>
    /// <para>This gets called if the buffer plays out normally, or if you call
    /// <see cref="HaltChannel"/>, implicitly stop a channel via <see cref="AllocateChannels"/>, or
    /// unregister a callback while it's still playing.</para>
    /// </summary>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void EffectDoneT(int chan, IntPtr udata);
    
    
    /// <code>typedef bool (SDLCALL *Mix_EachSoundFontCallback)(const char*, void*);</code>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool EachSoundFontCallback(string s, IntPtr d);
}