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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetNumAudioDrivers(void);</code>
    /// <summary>
    /// <para>Use this function to get the number of built-in audio drivers.</para>
    /// <para>This function returns a hardcoded number. This never returns a negative
    /// value; if there are no drivers compiled into this build of SDL, this
    /// function returns zero. The presence of a driver in this list does not mean
    /// it will function, it just means SDL is capable of interacting with that
    /// interface. For example, a build of SDL might have esound support, but if
    /// there's no esound server available, SDL's esound driver would fail if used.</para>
    /// <para>By default, SDL tries all drivers, in its preferred order, until one is
    /// found to be usable.</para>
    /// </summary>
    /// <returns>the number of built-in audio drivers.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetAudioDriver"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetNumAudioDrivers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumAudioDrivers();
   
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAudioDriver"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetAudioDriver(int index);
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetAudioDriver(int index);</code>
    /// <summary>
    /// <para>Use this function to get the name of a built in audio driver.</para>
    /// <para>The list of audio drivers is given in the order that they are normally
    /// initialized by default; the drivers that seem more reasonable to choose
    /// first (as far as the SDL developers believe) are earlier in the list.</para>
    /// <para>The names of drivers are all simple, low-ASCII identifiers, like "alsa",
    /// "coreaudio" or "wasapi". These never have Unicode characters, and are not
    /// meant to be proper names.</para>
    /// </summary>
    /// <param name="index">the index of the audio driver; the value ranges from 0 to
    /// <see cref="GetNumAudioDrivers"/> - 1.</param>
    /// <returns>the name of the audio driver at the requested index, or <c>null</c> if an
    /// invalid index was specified.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetNumAudioDrivers"/>
    public static string? GetAudioDriver(int index)
    {
        var value = SDL_GetAudioDriver(index); 
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetCurrentAudioDriver"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetCurrentAudioDriver();
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetCurrentAudioDriver(void);</code>
    /// <summary>
    /// <para>Get the name of the current audio driver.</para>
    /// <para>The names of drivers are all simple, low-ASCII identifiers, like "alsa",
    /// "coreaudio" or "wasapi". These never have Unicode characters, and are not
    /// meant to be proper names.</para>
    /// </summary>
    /// <returns>the name of the current audio driver or <c>null</c> if no driver has been
    /// initialized.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static string? GetCurrentAudioDriver()
    {
        var value = SDL_GetCurrentAudioDriver(); 
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAudioPlaybackDevices"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetAudioPlaybackDevices(out int count);
    /// <code>extern SDL_DECLSPEC SDL_AudioDeviceID * SDLCALL SDL_GetAudioPlaybackDevices(int *count);</code>
    /// <summary>
    /// <para>Get a list of currently-connected audio playback devices.</para>
    /// <para>This returns of list of available devices that play sound, perhaps to
    /// speakers or headphones ("playback" devices). If you want devices that
    /// record audio, like a microphone ("recording" devices), use
    /// <see cref="GetAudioRecordingDevices"/> instead.</para>
    /// <para>This only returns a list of physical devices; it will not have any device
    /// IDs returned by <see cref="OpenAudioDevice(uint, nint)"/>.</para>
    /// <para>If this function returns <c>null</c>, to signify an error, <c>count</c> will be set to
    /// zero.</para>
    /// </summary>
    /// <param name="count">a pointer filled in with the number of devices returned, may
    /// be <c>null</c>.</param>
    /// <returns>a 0 terminated array of device instance IDs or <c>null</c> on error; call
    /// <see cref="GetError"/> for more information. This should be freed with
    /// <see cref="Free"/> when it is no longer needed.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="OpenAudioDevice(uint, nint)"/>
    /// <seealso cref="GetAudioRecordingDevices"/>
    public static uint[]? GetAudioPlaybackDevices(out int count)
    {
        var ptr = SDL_GetAudioPlaybackDevices(out count);

        try
        {
            return PointerToStructureArray<uint>(ptr, count);
        }
        finally
        {
            Free(ptr);
        }
    }
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAudioRecordingDevices"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetAudioRecordingDevices(out int count);
    /// <code>extern SDL_DECLSPEC SDL_AudioDeviceID * SDLCALL SDL_GetAudioRecordingDevices(int *count);</code>
    /// <summary>
    /// <para>Get a list of currently-connected audio recording devices.</para>
    /// <para>This returns of list of available devices that record audio, like a
    /// microphone ("recording" devices). If you want devices that play sound,
    /// perhaps to speakers or headphones ("playback" devices), use
    /// <see cref="GetAudioPlaybackDevices"/> instead.</para>
    /// <para>This only returns a list of physical devices; it will not have any device
    /// IDs returned by <see cref="OpenAudioDevice(uint, nint)"/>.</para>
    /// <para>If this function returns <c>null</c>, to signify an error, <c>count</c> will be set to
    /// zero.</para>
    /// </summary>
    /// <param name="count">a pointer filled in with the number of devices returned, may
    /// be <c>null</c>.</param>
    /// <returns>a 0 terminated array of device instance IDs, or <c>null</c> on failure;
    /// call <see cref="GetError"/> for more information. This should be freed
    /// with <see cref="Free"/> when it is no longer needed.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="OpenAudioDevice(uint, nint)"/>
    /// <seealso cref="GetAudioPlaybackDevices"/>
    public static uint[]? GetAudioRecordingDevices(out int count)
    {
        var ptr = SDL_GetAudioRecordingDevices(out count);
        
        try
        {
            return PointerToStructureArray<uint>(ptr, count);
        }
        finally
        {
            if(ptr != IntPtr.Zero) Free(ptr);
        }
    }

    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAudioDeviceName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetAudioDeviceName(uint devid);
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetAudioDeviceName(SDL_AudioDeviceID devid);</code>
    /// <summary>
    /// Get the human-readable name of a specific audio device.
    /// </summary>
    /// <param name="devid">the instance ID of the device to query.</param>
    /// <returns>the name of the audio device, or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetAudioPlaybackDevices"/>
    /// <seealso cref="GetAudioRecordingDevices"/>
    public static string? GetAudioDeviceName(uint devid)
    {
        var value = SDL_GetAudioDeviceName(devid); 
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetAudioDeviceFormat(SDL_AudioDeviceID devid, SDL_AudioSpec *spec, int *sample_frames);</code>
    /// <summary>
    /// <para>Get the current audio format of a specific audio device.</para>
    /// <para>For an opened device, this will report the format the device is currently
    /// using. If the device isn't yet opened, this will report the device's
    /// preferred format (or a reasonable default if this can't be determined).</para>
    /// <para>You may also specify <see cref="AudioDeviceDefaultPlayback"/> or
    /// <see cref="AudioDeviceDefaultRecording"/> here, which is useful for getting a
    /// reasonable recommendation before opening the system-recommended default
    /// device.</para>
    /// <para>You can also use this to request the current device buffer size. This is
    /// specified in sample frames and represents the amount of data SDL will feed
    /// to the physical hardware in each chunk. This can be converted to
    /// milliseconds of audio with the following equation:</para>
    /// <para><c>ms = (int) ((((Sint64) frames) * 1000) / spec.freq);</c></para>
    /// <para>Buffer size is only important if you need low-level control over the audio
    /// playback timing. Most apps do not need this.</para>
    /// </summary>
    /// <param name="devid">the instance ID of the device to query.</param>
    /// <param name="spec">on return, will be filled with device details.</param>
    /// <param name="sampleFrames">pointer to store device buffer size, in sample frames.
    /// Can be <c>null</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAudioDeviceFormat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetAudioDeviceFormat(uint devid, out AudioSpec spec, out int sampleFrames);

    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAudioDeviceChannelMap"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetAudioDeviceChannelMap(uint devid, out int count);
    /// <c>extern SDL_DECLSPEC int * SDLCALL SDL_GetAudioDeviceChannelMap(SDL_AudioDeviceID devid, int *count);</c>
    /// <summary>
    /// <para>Get the current channel map of an audio device.</para>
    /// <para>Channel maps are optional; most things do not need them, instead passing
    /// data in the [order that SDL expects](CategoryAudio#channel-layouts).</para>
    /// <para>Audio devices usually have no remapping applied. This is represented by
    /// returning <c>null</c>, and does not signify an error.</para>
    /// </summary>
    /// <param name="devid">the instance ID of the device to query.</param>
    /// <param name="count">On output, set to number of channels in the map. Can be <c>null</c>.</param>
    /// <returns>an array of the current channel mapping, with as many elements as
    /// the current output spec's channels, or <c>null</c> if default. This
    /// should be freed with <see cref="Free"/> when it is no longer needed.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetAudioStreamInputChannelMap"/>
    public static int[]? GetAudioDeviceChannelMap(uint devid, out int count)
    {
        var ptr = SDL_GetAudioDeviceChannelMap(devid, out count);

        try
        {
            return PointerToStructureArray<int>(ptr, count);
        }
        finally
        {
            if (ptr != IntPtr.Zero) Free(ptr);
        }
    }


    /// <code>extern SDL_DECLSPEC SDL_AudioDeviceID SDLCALL SDL_OpenAudioDevice(SDL_AudioDeviceID devid, const SDL_AudioSpec *spec);</code>
    /// <summary>
    /// <para>Open a specific audio device.</para>
    /// <para>You can open both playback and recording devices through this function.
    /// Playback devices will take data from bound audio streams, mix it, and send
    /// it to the hardware. Recording devices will feed any bound audio streams
    /// with a copy of any incoming data.</para>
    /// <para>An opened audio device starts out with no audio streams bound. To start
    /// audio playing, bind a stream and supply audio data to it. Unlike SDL2,
    /// there is no audio callback; you only bind audio streams and make sure they
    /// have data flowing into them (however, you can simulate SDL2's semantics
    /// fairly closely by using <see cref="OpenAudioDeviceStream(uint, in nint, AudioStreamCallback, nint)"/> instead of this
    /// function).</para>
    /// <para>If you don't care about opening a specific device, pass a `devid` of either
    /// <see cref="AudioDeviceDefaultPlayback"/> or
    /// <see cref="AudioDeviceDefaultRecording"/>. In this case, SDL will try to pick
    /// the most reasonable default, and may also switch between physical devices
    /// seamlessly later, if the most reasonable default changes during the
    /// lifetime of this opened device (user changed the default in the OS's system
    /// preferences, the default got unplugged so the system jumped to a new
    /// default, the user plugged in headphones on a mobile device, etc). Unless
    /// you have a good reason to choose a specific device, this is probably what
    /// you want.</para>
    /// <para>You may request a specific format for the audio device, but there is no
    /// promise the device will honor that request for several reasons. As such,
    /// it's only meant to be a hint as to what data your app will provide. Audio
    /// streams will accept data in whatever format you specify and manage
    /// conversion for you as appropriate. <see cref="GetAudioDeviceFormat"/> can tell you
    /// the preferred format for the device before opening and the actual format
    /// the device is using after opening.</para>
    /// <para>It's legal to open the same device ID more than once; each successful open
    /// will generate a new logical SDL_AudioDeviceID that is managed separately
    /// from others on the same physical device. This allows libraries to open a
    /// device separately from the main app and bind its own streams without
    /// conflicting.</para>
    /// <para>It is also legal to open a device ID returned by a previous call to thi
    /// function; doing so just creates another logical device on the same physical
    /// device. This may be useful for making logical groupings of audio streams.</para>
    /// <para>This function returns the opened device ID on success. This is a new,
    /// unique SDL_AudioDeviceID that represents a logical device.</para>
    /// <para>Some backends might offer arbitrary devices (for example, a networked audio
    /// protocol that can connect to an arbitrary server). For these, as a change
    /// from SDL2, you should open a default device ID and use an SDL hint to
    /// specify the target if you care, or otherwise let the backend figure out a
    /// reasonable default. Most backends don't offer anything like this, and often
    /// this would be an end user setting an environment variable for their custom
    /// need, and not something an application should specifically manage.</para>
    /// <para>When done with an audio device, possibly at the end of the app's life, one
    /// should call <see cref="CloseAudioDevice"/> on the returned device id.</para>
    /// </summary>
    /// <param name="devid">the device instance id to open, or
    /// <see cref="AudioDeviceDefaultPlayback"/> or
    /// <see cref="AudioDeviceDefaultRecording"/> for the most reasonable
    /// default device.</param>
    /// <param name="spec">the requested device configuration. Can be <c>null</c> to use
    /// reasonable defaults.</param>
    /// <returns>the device ID on success or 0 on failure; call <see cref="GetError"/> for
    /// more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CloseAudioDevice"/>
    /// <seealso cref="GetAudioDeviceFormat"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_OpenAudioDevice"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint OpenAudioDevice(uint devid, in AudioSpec spec);
    
    
    /// <code>extern SDL_DECLSPEC SDL_AudioDeviceID SDLCALL SDL_OpenAudioDevice(SDL_AudioDeviceID devid, const SDL_AudioSpec *spec);</code>
    /// <summary>
    /// <para>Open a specific audio device.</para>
    /// <para>You can open both playback and recording devices through this function.
    /// Playback devices will take data from bound audio streams, mix it, and send
    /// it to the hardware. Recording devices will feed any bound audio streams
    /// with a copy of any incoming data.</para>
    /// <para>An opened audio device starts out with no audio streams bound. To start
    /// audio playing, bind a stream and supply audio data to it. Unlike SDL2,
    /// there is no audio callback; you only bind audio streams and make sure they
    /// have data flowing into them (however, you can simulate SDL2's semantics
    /// fairly closely by using <see cref="OpenAudioDeviceStream(uint, in nint, AudioStreamCallback?, nint)"/> instead of this
    /// function).</para>
    /// <para>If you don't care about opening a specific device, pass a `devid` of either
    /// <see cref="AudioDeviceDefaultPlayback"/> or
    /// <see cref="AudioDeviceDefaultRecording"/>. In this case, SDL will try to pick
    /// the most reasonable default, and may also switch between physical devices
    /// seamlessly later, if the most reasonable default changes during the
    /// lifetime of this opened device (user changed the default in the OS's system
    /// preferences, the default got unplugged so the system jumped to a new
    /// default, the user plugged in headphones on a mobile device, etc). Unless
    /// you have a good reason to choose a specific device, this is probably what
    /// you want.</para>
    /// <para>You may request a specific format for the audio device, but there is no
    /// promise the device will honor that request for several reasons. As such,
    /// it's only meant to be a hint as to what data your app will provide. Audio
    /// streams will accept data in whatever format you specify and manage
    /// conversion for you as appropriate. <see cref="GetAudioDeviceFormat"/> can tell you
    /// the preferred format for the device before opening and the actual format
    /// the device is using after opening.</para>
    /// <para>It's legal to open the same device ID more than once; each successful open
    /// will generate a new logical SDL_AudioDeviceID that is managed separately
    /// from others on the same physical device. This allows libraries to open a
    /// device separately from the main app and bind its own streams without
    /// conflicting.</para>
    /// <para>It is also legal to open a device ID returned by a previous call to thi
    /// function; doing so just creates another logical device on the same physical
    /// device. This may be useful for making logical groupings of audio streams.</para>
    /// <para>This function returns the opened device ID on success. This is a new,
    /// unique SDL_AudioDeviceID that represents a logical device.</para>
    /// <para>Some backends might offer arbitrary devices (for example, a networked audio
    /// protocol that can connect to an arbitrary server). For these, as a change
    /// from SDL2, you should open a default device ID and use an SDL hint to
    /// specify the target if you care, or otherwise let the backend figure out a
    /// reasonable default. Most backends don't offer anything like this, and often
    /// this would be an end user setting an environment variable for their custom
    /// need, and not something an application should specifically manage.</para>
    /// <para>When done with an audio device, possibly at the end of the app's life, one
    /// should call <see cref="CloseAudioDevice"/> on the returned device id.</para>
    /// </summary>
    /// <param name="devid">the device instance id to open, or
    /// <see cref="AudioDeviceDefaultPlayback"/> or
    /// <see cref="AudioDeviceDefaultRecording"/> for the most reasonable
    /// default device.</param>
    /// <param name="spec">the requested device configuration. Can be <c>null</c> to use
    /// reasonable defaults.</param>
    /// <returns>the device ID on success or 0 on failure; call <see cref="GetError"/> for
    /// more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CloseAudioDevice"/>
    /// <seealso cref="GetAudioDeviceFormat"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_OpenAudioDevice"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint OpenAudioDevice(uint devid, IntPtr spec);


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_IsAudioDevicePhysical(SDL_AudioDeviceID devid);</code>
    /// <summary>
    /// <para>Determine if an audio device is physical (instead of logical).</para>
    /// <para>An SDL_AudioDeviceID that represents physical hardware is a physica
    /// device; there is one for each piece of hardware that SDL can see. Logical
    /// devices are created by calling <see cref="OpenAudioDevice(uint, nint)"/> or
    /// <see cref="OpenAudioDeviceStream(uint, in nint, AudioStreamCallback, nint)"/>, and while each is associated with a physical
    /// device, there can be any number of logical devices on one physical device.</para>
    /// <para>For the most part, logical and physical IDs are interchangeable--if you try
    /// to open a logical device, SDL understands to assign that effort to the
    /// underlying physical device, etc. However, it might be useful to know if an
    /// arbitrary device ID is physical or logical. This function reports which.</para>
    /// <para>This function may return either true or false for invalid device IDs.</para>
    /// </summary>
    /// <param name="devid">the device ID to query.</param>
    /// <returns><c>true</c> if devid is a physical device, <c>false</c> if it is logical.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_IsAudioDevicePhysical"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsAudioDevicePhysical(uint devid);


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_IsAudioDevicePlayback(SDL_AudioDeviceID devid);</code>
    /// <summary>
    /// <para>Determine if an audio device is a playback device (instead of recording).</para>
    /// <para>This function may return either true or false for invalid device IDs.</para>
    /// </summary>
    /// <param name="devid">the device ID to query.</param>
    /// <returns><c>true</c> if devid is a playback device, <c>false</c> if it is recording.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_IsAudioDevicePlayback"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsAudioDevicePlayback(uint devid);


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_PauseAudioDevice(SDL_AudioDeviceID dev);</code>
    /// <summary>
    /// <para>Use this function to pause audio playback on a specified device.</para>
    /// <para>This function pauses audio processing for a given device. Any bound audio
    /// streams will not progress, and no audio will be generated. Pausing one
    /// device does not prevent other unpaused devices from running.</para>
    /// <para>Unlike in SDL2, audio devices start in an _unpaused_ state, since an app
    /// has to bind a stream before any audio will flow. Pausing a paused device is
    /// a legal no-op.</para>
    /// <para>Pausing a device can be useful to halt all audio without unbinding all the
    /// audio streams. This might be useful while a game is paused, or a level is
    /// loading, etc.</para>
    /// <para>Physical devices can not be paused or unpaused, only logical devices
    /// created through <see cref="OpenAudioDevice(uint, nint)"/> can be.</para>
    /// </summary>
    /// <param name="devid">a device opened by <see cref="OpenAudioDevice(uint, nint)"/>.</param>
    /// <returns>true on success or false on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ResumeAudioDevice"/>
    /// <seealso cref="AudioDevicePaused"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_PauseAudioDevice"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool PauseAudioDevice(uint devid);


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ResumeAudioDevice(SDL_AudioDeviceID dev);</code>
    /// <summary>
    /// <para>Use this function to unpause audio playback on a specified device.</para>
    /// <para>This function unpauses audio processing for a given device that has
    /// previously been paused with <see cref="PauseAudioDevice"/>. Once unpaused, any
    /// bound audio streams will begin to progress again, and audio can be
    /// generated.</para>
    /// <para>Unlike in SDL2, audio devices start in an _unpaused_ state, since an app
    /// has to bind a stream before any audio will flow. Unpausing an unpaused
    /// device is a legal no-op.</para>
    /// <para>Physical devices can not be paused or unpaused, only logical devices
    /// created through <see cref="OpenAudioDevice(uint, nint)"/> can be.</para>
    /// </summary>
    /// <param name="devid">a device opened by <see cref="OpenAudioDevice(uint, nint)"/>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="AudioDevicePaused"/>
    /// <seealso cref="PauseAudioDevice"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ResumeAudioDevice"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ResumeAudioDevice(uint devid);


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_AudioDevicePaused(SDL_AudioDeviceID dev);</code>
    /// <summary>
    /// <para>Use this function to query if an audio device is paused.</para>
    /// <para>Unlike in SDL2, audio devices start in an _unpaused_ state, since an app
    /// has to bind a stream before any audio will flow.</para>
    /// <para>Physical devices can not be paused or unpaused, only logical devices
    /// created through <see cref="OpenAudioDevice(uint, nint)"/> can be. Physical and invalid device
    /// IDs will report themselves as unpaused here.</para>
    /// </summary>
    /// <param name="devid">a device opened by <see cref="OpenAudioDevice(uint, nint)"/>.</param>
    /// <returns><c>true</c> if device is valid and paused, <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="PauseAudioDevice"/>
    /// <seealso cref="ResumeAudioDevice"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_AudioDevicePaused"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool AudioDevicePaused(uint devid);


    /// <code>extern SDL_DECLSPEC float SDLCALL SDL_GetAudioDeviceGain(SDL_AudioDeviceID devid);</code>
    /// <summary>
    /// <para>Get the gain of an audio device.</para>
    /// <para>The gain of a device is its volume; a larger gain means a louder output,
    /// with a gain of zero being silence.</para>
    /// <para>Audio devices default to a gain of 1.0f (no change in output).</para>
    /// <para>Physical devices may not have their gain changed, only logical devices, and
    /// this function will always return -1.0f when used on physical devices.</para>
    /// </summary>
    /// <param name="devid">the audio device to query.</param>
    /// <returns>the gain of the device or -1.0f on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetAudioDeviceGain"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAudioDeviceGain"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float GetAudioDeviceGain(uint devid);


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetAudioDeviceGain(SDL_AudioDeviceID devid, float gain);</code>
    /// <summary>
    /// <para>Change the gain of an audio device.</para>
    /// <para>The gain of a device is its volume; a larger gain means a louder output,
    /// with a gain of zero being silence.</para>
    /// <para>Audio devices default to a gain of 1.0f (no change in output).</para>
    /// <para>Physical devices may not have their gain changed, only logical devices, and
    /// this function will always return false when used on physical devices. While
    /// it might seem attractive to adjust several logical devices at once in this
    /// way, it would allow an app or library to interfere with another portion of
    /// the program's otherwise-isolated devices.</para>
    /// <para>This is applied, along with any per-audiostream gain, during playback to
    /// the hardware, and can be continuously changed to create various effects. On
    /// recording devices, this will adjust the gain before passing the data into
    /// an audiostream; that recording audiostream can then adjust its gain further
    /// when outputting the data elsewhere, if it likes, but that second gain is
    /// not applied until the data leaves the audiostream again.</para>
    /// </summary>
    /// <param name="devid">the audio device on which to change gain.</param>
    /// <param name="gain">the gain. 1.0f is no change, 0.0f is silence.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread, as it holds
    /// a stream-specific mutex while running.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetAudioDeviceGain"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetAudioDeviceGain"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetAudioDeviceGain(uint devid, float gain);


    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_CloseAudioDevice(SDL_AudioDeviceID devid);</code>
    /// <summary>
    /// <para>Close a previously-opened audio device.</para>
    /// <para>The application should close open audio devices once they are no longer
    /// needed.</para>
    /// <para>This function may block briefly while pending audio data is played by the
    /// hardware, so that applications don't drop the last buffer of data they
    /// supplied if terminating immediately afterwards.</para>
    /// </summary>
    /// <param name="devid">an audio device id previously returned by
    /// <see cref="OpenAudioDevice(uint, nint)"/>.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="OpenAudioDevice(uint, nint)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CloseAudioDevice"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CloseAudioDevice(uint devid);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_BindAudioStreams(SDL_AudioDeviceID devid, SDL_AudioStream * const *streams, int num_streams);</code>
    /// <summary>
    /// <para>Bind a list of audio streams to an audio device.</para>
    /// <para>Audio data will flow through any bound streams. For a playback device, data
    /// for all bound streams will be mixed together and fed to the device. For a
    /// recording device, a copy of recorded data will be provided to each bound
    /// stream.</para>
    /// <para>Audio streams can only be bound to an open device. This operation is
    /// atomic--all streams bound in the same call will start processing at the
    /// same time, so they can stay in sync. Also: either all streams will be bound
    /// or none of them will be.</para>
    /// <para>It is an error to bind an already-bound stream; it must be explicitly
    /// unbound first.</para>
    /// <para>Binding a stream to a device will set its output format for playback
    /// devices, and its input format for recording devices, so they match the
    /// device's settings. The caller is welcome to change the other end of the
    /// stream's format at any time with <see cref="SetAudioStreamFormat"/>. If the other
    /// end of the stream's format has never been set (the audio stream was created
    /// with a NULL audio spec), this function will set it to match the device
    /// end's format.</para>
    /// </summary>
    /// <param name="devid">an audio device to bind a stream to.</param>
    /// <param name="streams">an array of audio streams to bind.</param>
    /// <param name="numStream">number streams listed in the <c>streams</c> array.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BindAudioStreams"/>
    /// <seealso cref="UnbindAudioStream"/>
    /// <seealso cref="GetAudioStreamDevice"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindAudioStreams"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool BindAudioStreams(uint devid, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] IntPtr[] streams, int numStream);
    

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_BindAudioStream(SDL_AudioDeviceID devid, SDL_AudioStream *stream);</code>
    /// <summary>
    /// <para>Bind a single audio stream to an audio device.</para>
    /// <para>This is a convenience function, equivalent to calling
    /// <c>BindAudioStreams(devid, stream, 1)</c>.</para>
    /// </summary>
    /// <param name="devid">an audio device to bind a stream to.</param>
    /// <param name="stream">an audio stream to bind to a device.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BindAudioStreams"/>
    /// <seealso cref="UnbindAudioStream"/>
    /// <seealso cref="GetAudioStreamDevice"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_BindAudioStream"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool BindAudioStream(uint devid, IntPtr stream);


    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_UnbindAudioStreams(SDL_AudioStream * const *streams, int num_streams);</code>
    /// <summary>
    /// <para>Unbind a list of audio streams from their audio devices.</para>
    /// <para>The streams being unbound do not all have to be on the same device. All
    /// streams on the same device will be unbound atomically (data will stop
    /// flowing through all unbound streams on the same device at the same time).</para>
    /// <para>Unbinding a stream that isn't bound to a device is a legal no-op.</para>
    /// </summary>
    /// <param name="streams">an array of audio streams to unbind. Can be <c>null</c> or contain
    /// <c>null</c>.</param>
    /// <param name="numStreams">number streams listed in the <c>streams</c> array.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BindAudioStreams"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_UnbindAudioStreams"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial void UnbindAudioStreams([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] IntPtr[]? streams, int numStreams);


    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_UnbindAudioStream(SDL_AudioStream *stream);</code>
    /// <summary>
    /// <para>Unbind a single audio stream from its audio device.</para>
    /// <para>This is a convenience function, equivalent to calling
    /// <c>UnbindAudioStreams(stream, 1)</c>.</para>
    /// </summary>
    /// <param name="stream">an audio stream to unbind from a device.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BindAudioStream"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_UnbindAudioStream"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial void UnbindAudioStream(IntPtr stream);


    /// <code>extern SDL_DECLSPEC SDL_AudioDeviceID SDLCALL SDL_GetAudioStreamDevice(SDL_AudioStream *stream);</code>
    /// <summary>
    /// <para>Query an audio stream for its currently-bound device.</para>
    /// <para>This reports the logical audio device that an audio stream is currently
    /// bound to.</para>
    /// <para>If not bound, or invalid, this returns zero, which is not a valid device
    /// ID.</para>
    /// </summary>
    /// <param name="stream">the audio stream to query.</param>
    /// <returns>the bound audio device, or 0 if not bound or invalid.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="BindAudioStream"/>
    /// <seealso cref="BindAudioStreams"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAudioStreamDevice"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint GetAudioStreamDevice(IntPtr stream);


    /// <code>extern SDL_DECLSPEC SDL_AudioStream * SDLCALL SDL_CreateAudioStream(const SDL_AudioSpec *src_spec, const SDL_AudioSpec *dst_spec);</code>
    /// <summary>
    /// Create a new audio stream.
    /// </summary>
    /// <param name="srcSpec">the format details of the input audio.</param>
    /// <param name="dstSpec">the format details of the output audio.</param>
    /// <returns>a new audio stream on success or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="PutAudioStreamData(nint, byte[], int)"/>
    /// <seealso cref="GetAudioStreamData(nint, byte[], int)"/>
    /// <seealso cref="GetAudioStreamAvailable"/>
    /// <seealso cref="FlushAudioStream"/>
    /// <seealso cref="ClearAudioStream"/>
    /// <seealso cref="SetAudioStreamFormat"/>
    /// <seealso cref="DestroyAudioStream"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateAudioStream"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateAudioStream(in AudioSpec srcSpec, in AudioSpec dstSpec);


    /// <code>extern SDL_DECLSPEC SDL_PropertiesID SDLCALL SDL_GetAudioStreamProperties(SDL_AudioStream *stream);</code>
    /// <summary>
    /// <para>Get the properties associated with an audio stream.</para>
    /// <para>The application can hang any data it wants here, but the following
    /// properties are understood by SDL:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.AudioStreamAutoCleanupBoolean"/>: if true (the default), the
    /// stream be automatically cleaned up when the audio subsystem quits. If set
    /// to false, the streams will persist beyond that. This property is ignored
    /// for streams created through <see cref="OpenAudioDeviceStream(uint, in AudioSpec, AudioStreamCallback?, nint)"/>, and will always
    /// be cleaned up. Streams that are not cleaned up will still be unbound from
    /// devices when the audio subsystem quits. This property was added in SDL
    /// 3.4.0.</item>
    /// </list>
    /// </summary>
    /// <param name="stream">the SDL_AudioStream to query.</param>
    /// <returns>a valid property ID on success or 0 on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAudioStreamProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint GetAudioStreamProperties(IntPtr stream);


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetAudioStreamFormat(SDL_AudioStream *stream, SDL_AudioSpec *src_spec, SDL_AudioSpec *dst_spec);</code>
    /// <summary>
    /// Query the current format of an audio stream.
    /// </summary>
    /// <param name="stream">the SDL_AudioStream to query.</param>
    /// <param name="srcSpec">where to store the input audio format; ignored if <c>null</c>.</param>
    /// <param name="dstSpec">where to store the output audio format; ignored if <c>null</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread, as it holds
    /// a stream-specific mutex while running.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetAudioStreamFormat"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAudioStreamFormat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetAudioStreamFormat(IntPtr stream, out AudioSpec srcSpec, out AudioSpec dstSpec);


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetAudioStreamFormat(SDL_AudioStream *stream, const SDL_AudioSpec *src_spec, const SDL_AudioSpec *dst_spec);</code>
    /// <summary>
    /// <para>Change the input and output formats of an audio stream.</para>
    /// <para>Future calls to and <see cref="GetAudioStreamAvailable"/> and <see cref="GetAudioStreamData(nint, byte[], int)"/>
    /// will reflect the new format, and future calls to <see cref="PutAudioStreamData(nint, byte[], int)"/>
    /// must provide data in the new input formats.</para>
    /// <para>Data that was previously queued in the stream will still be operated on i
    /// the format that was current when it was added, which is to say you can put
    /// the end of a sound file in one format to a stream, change formats for the
    /// next sound file, and start putting that new data while the previous sound
    /// file is still queued, and everything will still play back correctly.</para>
    /// <para>If a stream is bound to a device, then the format of the side of the stream
    /// bound to a device cannot be changed (src_spec for recording devices,
    /// dst_spec for playback devices). Attempts to make a change to this side
    /// will be ignored, but this will not report an error. The other side's format
    /// can be changed.</para>
    /// </summary>
    /// <param name="stream">the stream the format is being changed.</param>
    /// <param name="srcSpec">the new format of the audio input; if <c>null</c>, it is not
    /// changed.</param>
    /// <param name="dstSpec">the new format of the audio output; if <c>null</c>, it is not
    /// changed.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread, as it holds
    /// a stream-specific mutex while running.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetAudioStreamFormat"/>
    /// <seealso cref="SetAudioStreamFrequencyRatio"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetAudioStreamFormat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetAudioStreamFormat(IntPtr stream, in AudioSpec srcSpec, in AudioSpec dstSpec);


    /// <code>extern SDL_DECLSPEC float SDLCALL SDL_GetAudioStreamFrequencyRatio(SDL_AudioStream *stream);</code>
    /// <summary>
    /// Get the frequency ratio of an audio stream.
    /// </summary>
    /// <param name="stream">the SDL_AudioStream to query.</param>
    /// <returns>the frequency ratio of the stream or 0.0 on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread, as it holds
    /// a stream-specific mutex while running.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetAudioStreamFrequencyRatio"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAudioStreamFrequencyRatio"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float GetAudioStreamFrequencyRatio(IntPtr stream);


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetAudioStreamFrequencyRatio(SDL_AudioStream *stream, float ratio);</code>
    /// <summary>
    /// <para>Change the frequency ratio of an audio stream.</para>
    /// <para>The frequency ratio is used to adjust the rate at which input data is
    /// consumed. Changing this effectively modifies the speed and pitch of the
    /// audio. A value greater than 1.0f will play the audio faster, and at a
    /// higher pitch. A value less than 1.0f will play the audio slower, and at a
    /// lower pitch. 1.0f means play at normal speed.</para>
    /// <para>This is applied during <see cref="GetAudioStreamData(nint, byte[], int)"/>, and can be continuously
    /// changed to create various effects.</para>
    /// </summary>
    /// <param name="stream">the stream on which the frequency ratio is being changed.</param>
    /// <param name="ratio">the frequency ratio. 1.0 is normal speed. Must be between 0.01
    /// and 100.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread, as it holds
    /// a stream-specific mutex while running.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetAudioStreamFrequencyRatio"/>
    /// <seealso cref="SetAudioStreamFormat"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetAudioStreamFrequencyRatio"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetAudioStreamFrequencyRatio(IntPtr stream, float ratio);


    /// <code>extern SDL_DECLSPEC float SDLCALL SDL_GetAudioStreamGain(SDL_AudioStream *stream);</code>
    /// <summary>
    /// <para>Get the gain of an audio stream.</para>
    /// <para>The gain of a stream is its volume; a larger gain means a louder output,
    /// with a gain of zero being silence.</para>
    /// <para>Audio streams default to a gain of 1.0f (no change in output).</para>
    /// </summary>
    /// <param name="stream">the SDL_AudioStream to query.</param>
    /// <returns>the gain of the stream or -1.0f on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread, as it holds
    /// a stream-specific mutex while running.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetAudioStreamGain"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAudioStreamGain"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float GetAudioStreamGain(IntPtr stream);


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetAudioStreamGain(SDL_AudioStream *stream, float gain);</code>
    /// <summary>
    /// <para>Change the gain of an audio stream.</para>
    /// <para>The gain of a stream is its volume; a larger gain means a louder output,
    /// with a gain of zero being silence.</para>
    /// <para>Audio streams default to a gain of 1.0f (no change in output).</para>
    /// <para>This is applied during <see cref="GetAudioStreamData(nint, byte[], int)"/>, and can be continuously
    /// changed to create various effects.</para>
    /// </summary>
    /// <param name="stream">the stream on which the gain is being changed.</param>
    /// <param name="gain">the gain. 1.0f is no change, 0.0f is silence.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread, as it holds
    /// a stream-specific mutex while running.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetAudioStreamGain"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetAudioStreamGain"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetAudioStreamGain(IntPtr stream, float gain);

    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAudioStreamInputChannelMap"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetAudioStreamInputChannelMap(IntPtr stream, out int count);
    /// <code>extern SDL_DECLSPEC int * SDLCALL SDL_GetAudioStreamInputChannelMap(SDL_AudioStream *stream, int *count);</code>
    /// <summary>
    /// <para>Get the current input channel map of an audio stream.</para>
    /// <para>Channel maps are optional; most things do not need them, instead passing
    /// data in the [order that SDL expects](CategoryAudio#channel-layouts).</para>
    /// <para>Audio streams default to no remapping applied. This is represented by
    /// returning <c>null</c>, and does not signify an error.</para>
    /// </summary>
    /// <param name="stream">the SDL_AudioStream to query.</param>
    /// <param name="count">On output, set to number of channels in the map. Can be <c>null</c>.</param>
    /// <returns>an array of the current channel mapping, with as many elements as
    /// the current output spec's channels, or <c>null</c> if default. This
    /// should be freed with <see cref="Free"/> when it is no longer needed.</returns>
    /// <threadsafety>It is safe to call this function from any thread, as it holds
    /// a stream-specific mutex while running.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetAudioStreamInputChannelMap"/>
    public static int[]? GetAudioStreamInputChannelMap(IntPtr stream, out int count)
    {
        var ptr = SDL_GetAudioStreamInputChannelMap(stream, out count);

        try
        {
            return PointerToStructureArray<int>(ptr, count);
        }
        finally
        {
            if (ptr != IntPtr.Zero) Free(ptr);
        }
    }


    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAudioStreamOutputChannelMap"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetAudioStreamOutputChannelMap(IntPtr stream, out int count);
    /// <code>extern SDL_DECLSPEC int * SDLCALL SDL_GetAudioStreamOutputChannelMap(SDL_AudioStream *stream, int *count);</code>
    /// <summary>
    /// <para>Get the current output channel map of an audio stream.</para>
    /// <para>Channel maps are optional; most things do not need them, instead passing
    /// data in the [order that SDL expects](CategoryAudio#channel-layouts).</para>
    /// <para>Audio streams default to no remapping applied. This is represented by
    /// returning <c>null</c>, and does not signify an error.</para>
    /// </summary>
    /// <param name="stream">the SDL_AudioStream to query.</param>
    /// <param name="count">On output, set to number of channels in the map. Can be <c>null</c>.</param>
    /// <returns>an array of the current channel mapping, with as many elements as
    /// the current output spec's channels, or <c>null</c> if default. This
    /// should be freed with <see cref="Free"/> when it is no longer needed.</returns>
    /// <threadsafety>It is safe to call this function from any thread, as it holds
    /// a stream-specific mutex while running.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetAudioStreamInputChannelMap"/>
    public static int[]? GetAudioStreamOutputChannelMap(IntPtr stream, out int count)
    {
        var ptr = SDL_GetAudioStreamOutputChannelMap(stream, out count);

        try
        {
            return PointerToStructureArray<int>(ptr, count);
        }
        finally
        {
            if (ptr != IntPtr.Zero) Free(ptr);
        }
    }


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetAudioStreamInputChannelMap(SDL_AudioStream *stream, const int *chmap, int count);</code>
    /// <summary>
    /// <para>Set the current input channel map of an audio stream.</para>
    /// <para>Channel maps are optional; most things do not need them, instead passing
    /// data in the [order that SDL expects](CategoryAudio#channel-layouts).</para>
    /// <para>The input channel map reorders data that is added to a stream via
    /// <see cref="PutAudioStreamData(nint, byte[], int)"/>. Future calls to <see cref="PutAudioStreamData(nint, byte[], int)"/> must provide
    /// data in the new channel order.</para>
    /// <para>Each item in the array represents an input channel, and its value is the
    /// channel that it should be remapped to. To reverse a stereo signal's left
    /// and right values, you'd have an array of <c>{ 1, 0 }</c>. It is legal to remap
    /// multiple channels to the same thing, so <c>{ 1, 1 }</c> would duplicate the
    /// right channel to both channels of a stereo signal. An element in the
    /// channel map set to -1 instead of a valid channel will mute that channel,
    /// setting it to a silence value.</para>
    /// <para>You cannot change the number of channels through a channel map, just
    /// reorder/mute them.</para>
    /// <para>Data that was previously queued in the stream will still be operated on in
    /// the order that was current when it was added, which is to say you can put
    /// the end of a sound file in one order to a stream, change orders for the
    /// next sound file, and start putting that new data while the previous sound
    /// file is still queued, and everything will still play back correctly.</para>
    /// <para>Audio streams default to no remapping applied. Passing a <c>null</c> channel map
    /// is legal, and turns off remapping.</para>
    /// <para>SDL will copy the channel map; the caller does not have to save this array
    /// after this call.</para>
    /// <para>If <c>count</c> is not equal to the current number of channels in the audio
    /// stream's format, this will fail. This is a safety measure to make sure a
    /// race condition hasn't changed the format while this call is setting the
    /// channel map.</para>
    /// <para>Unlike attempting to change the stream's format, the input channel map on a
    /// stream bound to a recording device is permitted to change at any time; any
    /// data added to the stream from the device after this call will have the new
    /// mapping, but previously-added data will still have the prior mapping.</para>
    /// </summary>
    /// <param name="stream">the SDL_AudioStream to change.</param>
    /// <param name="chmap">the new channel map, <c>null</c> to reset to default.</param>
    /// <param name="count">The number of channels in the map.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread, as it holds
    /// a stream-specific mutex while running. Don't change the
    /// stream's format to have a different number of channels from a
    /// a different thread at the same time, though!</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetAudioStreamInputChannelMap"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetAudioStreamInputChannelMap"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr SetAudioStreamInputChannelMap(IntPtr stream, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] int[]? chmap, int count);


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetAudioStreamOutputChannelMap(SDL_AudioStream *stream, const int *chmap, int count);</code>
    /// <summary>
    /// <para>Set the current output channel map of an audio stream.</para>
    /// <para>Channel maps are optional; most things do not need them, instead passing
    /// data in the [order that SDL expects](CategoryAudio#channel-layouts).</para>
    /// <para>The output channel map reorders data that is leaving a stream via
    /// <see cref="GetAudioStreamData(nint, byte[], int)"/>.</para>
    /// <para>Each item in the array represents an input channel, and its value is th
    /// channel that it should be remapped to. To reverse a stereo signal's left
    /// and right values, you'd have an array of <c>{ 1, 0 }</c>. It is legal to remap
    /// multiple channels to the same thing, so <c>{ 1, 1 }</c> would duplicate the
    /// right channel to both channels of a stereo signal. An element in the
    /// channel map set to -1 instead of a valid channel will mute that channel,
    /// setting it to a silence value.</para>
    /// <para>You cannot change the number of channels through a channel map, just
    /// reorder/mute them.</para>
    /// <para>The output channel map can be changed at any time, as output remapping is
    /// applied during <see cref="GetAudioStreamData(nint, byte[], int)"/>.</para>
    /// <para>Audio streams default to no remapping applied. Passing a <c>null</c> channel map
    /// is legal, and turns off remapping.</para>
    /// <para>SDL will copy the channel map; the caller does not have to save this array
    /// after this call.</para>
    /// <para>If <c>count</c> is not equal to the current number of channels in the audio
    /// stream's format, this will fail. This is a safety measure to make sure a
    /// race condition hasn't changed the format while this call is setting the
    /// channel map.</para>
    /// <para>Unlike attempting to change the stream's format, the output channel map on
    /// a stream bound to a recording device is permitted to change at any time;
    /// any data added to the stream after this call will have the new mapping, but
    /// previously-added data will still have the prior mapping. When the channel
    /// map doesn't match the hardware's channel layout, SDL will convert the data
    /// before feeding it to the device for playback.</para>
    /// </summary>
    /// <param name="stream">the SDL_AudioStream to change.</param>
    /// <param name="chmap">the new channel map, <c>null</c> to reset to default.</param>
    /// <param name="count">The number of channels in the map.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread, as it holds
    /// a stream-specific mutex while running. Don't change the
    /// stream's format to have a different number of channels from a
    /// a different thread at the same time, though!</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetAudioStreamInputChannelMap"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetAudioStreamOutputChannelMap"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr SetAudioStreamOutputChannelMap(IntPtr stream, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] int[]? chmap, int count);


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_PutAudioStreamData(SDL_AudioStream *stream, const void *buf, int len);</code>
    /// <summary>
    /// <para>Add data to the stream.</para>
    /// <para>This data must match the format/channels/samplerate specified in the latest
    /// call to <see cref="SetAudioStreamFormat"/>, or the format specified when creating the
    /// stream if it hasn't been changed.</para>
    /// <para>Note that this call simply copies the unconverted data for later. This is
    /// different than SDL2, where data was converted during the Put call and the
    /// Get call would just dequeue the previously-converted data.</para>
    /// </summary>
    /// <param name="stream">the stream the audio data is being added to.</param>
    /// <param name="buf">a pointer to the audio data to add.</param>
    /// <param name="len">the number of bytes to write to the stream.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread, but if the
    /// stream has a callback set, the caller might need to manage
    /// extra locking.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ClearAudioStream"/>
    /// <seealso cref="FlushAudioStream"/>
    /// <seealso cref="GetAudioStreamData(nint, byte[], int)"/>
    /// <seealso cref="GetAudioStreamQueued"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_PutAudioStreamData"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool PutAudioStreamData(IntPtr stream, IntPtr buf, int len);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_PutAudioStreamDataNoCopy(SDL_AudioStream *stream, const void *buf, int len, SDL_AudioStreamDataCompleteCallback callback, void *userdata);</code>
    /// <summary>
    /// <para>Add external data to an audio stream without copying it.</para>
    /// <para>Unlike <see cref="PutAudioStreamData(nint, nint, int)"/>, this function does not make a copy of the
    /// provided data, instead storing the provided pointer. This means that the
    /// put operation does not need to allocate and copy the data, but the original
    /// data must remain available until the stream is done with it, either by
    /// being read from the stream in its entirety, or a call to
    /// <seealso cref="ClearAudioStream"/> or <see cref="DestroyAudioStream"/>.</para>
    /// <para>The data must match the format/channels/samplerate specified in the latest
    /// call to <see cref="SetAudioStreamFormat"/>, or the format specified when creating the
    /// stream if it hasn't been changed.</para>
    /// <para>An optional callback may be provided, which is called when the stream no
    /// longer needs the data. Once this callback fires, the stream will not access
    /// the data again. This callback will fire for any reason the data is no
    /// longer needed, including clearing or destroying the stream.</para>
    /// <para>Note that there is still an allocation to store tracking information, so
    /// this function is more efficient for larger blocks of data. If you're
    /// planning to put a few samples at a time, it will be more efficient to use
    /// <see cref="PutAudioStreamData(nint, nint, int)"/>, which allocates and buffers in blocks.</para>
    /// </summary>
    /// <param name="stream">the stream the audio data is being added to.</param>
    /// <param name="buf">a pointer to the audio data to add.</param>
    /// <param name="len">the number of bytes to add to the stream.</param>
    /// <param name="callback">the callback function to call when the data is no longer
    /// needed by the stream. May be NULL.</param>
    /// <param name="userdata">an opaque pointer provided to the callback for its own
    /// personal use.</param>
    /// <returns>true on success or false on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread, but if the
    /// stream has a callback set, the caller might need to manage
    /// extra locking.</threadsafety>
    /// <since>This function is available since SDL 3.4.0.</since>
    /// <seealso cref="ClearAudioStream"/>
    /// <seealso cref="FlushAudioStream"/>
    /// <seealso cref="GetAudioStreamData(nint, nint, int)"/>
    /// <seealso cref="GetAudioStreamQueued"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_PutAudioStreamDataNoCopy"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool PutAudioStreamDataNoCopy(IntPtr stream, IntPtr buf, int len, AudioStreamDataCompleteCallback? callback, IntPtr userdata);
    
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_PutAudioStreamData(SDL_AudioStream *stream, const void *buf, int len);</code>
    /// <summary>
    /// <para>Add data to the stream.</para>
    /// <para>This data must match the format/channels/samplerate specified in the latest
    /// call to <see cref="SetAudioStreamFormat"/>, or the format specified when creating the
    /// stream if it hasn't been changed.</para>
    /// <para>Note that this call simply copies the unconverted data for later. This is
    /// different than SDL2, where data was converted during the Put call and the
    /// Get call would just dequeue the previously-converted data.</para>
    /// </summary>
    /// <param name="stream">the stream the audio data is being added to.</param>
    /// <param name="buf">a pointer to the audio data to add.</param>
    /// <param name="len">the number of bytes to write to the stream.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread, but if the
    /// stream has a callback set, the caller might need to manage
    /// extra locking.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ClearAudioStream"/>
    /// <seealso cref="FlushAudioStream"/>
    /// <seealso cref="GetAudioStreamData(nint, byte[], int)"/>
    /// <seealso cref="GetAudioStreamQueued"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_PutAudioStreamData"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool PutAudioStreamData(IntPtr stream, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buf, int len);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_PutAudioStreamPlanarData(SDL_AudioStream *stream, const void * const *channel_buffers, int num_channels, int num_samples);</code>
    /// <summary>
    /// <para>Add data to the stream with each channel in a separate array.</para>
    /// <para>This data must match the format/channels/samplerate specified in the latest
    /// call to SDL_SetAudioStreamFormat, or the format specified when creating the
    /// stream if it hasn't been changed.</para>
    /// <para>The data will be interleaved and queued. Note that AudioStream only
    /// operates on interleaved data, so this is simply a convenience function for
    /// easily queueing data from sources that provide separate arrays. There is no
    /// equivalent function to retrieve planar data.</para>
    /// <para>The arrays in <c>channelBuffers</c> are ordered as they are to be interleaved;
    /// the first array will be the first sample in the interleaved data. Any
    /// individual array may be <c>null</c>; in this case, silence will be interleaved for
    /// that channel.</para>
    /// <para><c>numChannels</c> specifies how many arrays are in <c>channelBuffers</c>. This can
    /// be used as a safety to prevent overflow, in case the stream format has
    /// changed elsewhere. If more channels are specified than the current input
    /// spec, they are ignored. If less channels are specified, the missing arrays
    /// are treated as if they are <c>null</c> (silence is written to those channels). If
    /// the count is -1, SDL will assume the array count matches the current input
    /// spec.</para>
    /// <para>Note that <c>numSamples</c> is the number of _samples per array_. This can also
    /// be thought of as the number of _sample frames_ to be queued. A value of 1
    /// with stereo arrays will queue two samples to the stream. This is different
    /// than <code>PutAudioStreamData</code>, which wants the size of a single array in
    /// bytes.</para>
    /// </summary>
    /// <param name="stream">the stream the audio data is being added to.</param>
    /// <param name="channelBuffers">a pointer to an array of arrays, one array per
    /// channel.</param>
    /// <param name="numChannels">the number of arrays in <c>channelBuffers</c> or -1.</param>
    /// <param name="numSamples">the number of _samples_ per array to write to the
    /// stream.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread, but if the
    /// stream has a callback set, the caller might need to manage
    /// extra locking.</threadsafety>
    /// <since>This function is available since SDL 3.4.0.</since>
    /// <seealso cref="ClearAudioStream"/>
    /// <seealso cref="FlushAudioStream"/>
    /// <seealso cref="GetAudioStreamData(nint, byte[], int)"/>
    /// <seealso cref="GetAudioStreamQueued"/>
    [DllImport(SDLLibrary, EntryPoint = "SDL_PutAudioStreamPlanarData"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool PutAudioStreamPlanarData(IntPtr stream, 
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPArray)] IntPtr[] channelBuffers, 
        int numChannels, int numSamples);


    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetAudioStreamData(SDL_AudioStream *stream, void *buf, int len);</code>
    /// <summary>
    /// <para>Get converted/resampled data from the stream.</para>
    /// <para>The input/output data format/channels/samplerate is specified when creating
    /// the stream, and can be changed after creation by calling
    /// <see cref="SetAudioStreamFormat"/>.</para>
    /// <para>Note that any conversion and resampling necessary is done during this call,
    /// and SDL_PutAudioStreamData simply queues unconverted data for later. This
    /// is different than SDL2, where that work was done while inputting new data
    /// to the stream and requesting the output just copied the converted data.</para>
    /// </summary>
    /// <param name="stream">the stream the audio is being requested from.</param>
    /// <param name="buf">a buffer to fill with audio data.</param>
    /// <param name="len">the maximum number of bytes to fill.</param>
    /// <returns>the number of bytes read from the stream or -1 on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread, but if the
    /// stream has a callback set, the caller might need to manage
    /// extra locking.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ClearAudioStream"/>
    /// <seealso cref="GetAudioStreamAvailable"/>
    /// <seealso cref="PutAudioStreamData(nint, byte[], int)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAudioStreamData"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetAudioStreamData(IntPtr stream, IntPtr buf, int len);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetAudioStreamData(SDL_AudioStream *stream, void *buf, int len);</code>
    /// <summary>
    /// <para>Get converted/resampled data from the stream.</para>
    /// <para>The input/output data format/channels/samplerate is specified when creating
    /// the stream, and can be changed after creation by calling
    /// <see cref="SetAudioStreamFormat"/>.</para>
    /// <para>Note that any conversion and resampling necessary is done during this call,
    /// and SDL_PutAudioStreamData simply queues unconverted data for later. This
    /// is different than SDL2, where that work was done while inputting new data
    /// to the stream and requesting the output just copied the converted data.</para>
    /// </summary>
    /// <param name="stream">the stream the audio is being requested from.</param>
    /// <param name="buf">a buffer to fill with audio data.</param>
    /// <param name="len">the maximum number of bytes to fill.</param>
    /// <returns>the number of bytes read from the stream or -1 on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread, but if the
    /// stream has a callback set, the caller might need to manage
    /// extra locking.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ClearAudioStream"/>
    /// <seealso cref="GetAudioStreamAvailable"/>
    /// <seealso cref="PutAudioStreamData(nint, byte[], int)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAudioStreamData"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetAudioStreamData(IntPtr stream, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buf, int len);


    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetAudioStreamAvailable(SDL_AudioStream *stream);</code>
    /// <summary>
    /// <para>Get the number of converted/resampled bytes available.</para>
    /// <para>The stream may be buffering data behind the scenes until it has enough to
    /// resample correctly, so this number might be lower than what you expect, or
    /// even be zero. Add more data or flush the stream if you need the data now.</para>
    /// <para>If the stream has so much data that it would overflow an int, the return
    /// value is clamped to a maximum value, but no queued data is lost; if there
    /// are gigabytes of data queued, the app might need to read some of it with
    /// <see cref="GetAudioStreamData(nint, byte[], int)"/> before this function's return value is no longer
    /// clamped.</para>
    /// </summary>
    /// <param name="stream">the audio stream to query.</param>
    /// <returns>the number of converted/resampled bytes available or -1 on
    /// failure; call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetAudioStreamData(nint, byte[], int)"/>
    /// <seealso cref="PutAudioStreamData(nint, byte[], int)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAudioStreamAvailable"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetAudioStreamAvailable(IntPtr stream);


    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetAudioStreamQueued(SDL_AudioStream *stream);</code>
    /// <summary>
    /// <para>Get the number of bytes currently queued.</para>
    /// <para>This is the number of bytes put into a stream as input, not the number that
    /// can be retrieved as output. Because of several details, it's not possible
    /// to calculate one number directly from the other. If you need to know how
    /// much usable data can be retrieved right now, you should use
    /// <see cref="GetAudioStreamAvailable"/> and not this function.</para>
    /// <para>Note that audio streams can change their input format at any time, even if
    /// there is still data queued in a different format, so the returned byte
    /// count will not necessarily match the number of _sample frames_ available.
    /// Users of this API should be aware of format changes they make when feeding
    /// a stream and plan accordingly.</para>
    /// <para>Queued data is not converted until it is consumed by
    /// <see cref="GetAudioStreamData(nint, byte[], int)"/>, so this value should be representative of the exact
    /// data that was put into the stream.</para>
    /// <para>If the stream has so much data that it would overflow an int, the return
    /// value is clamped to a maximum value, but no queued data is lost; if there
    /// are gigabytes of data queued, the app might need to read some of it with
    /// <see cref="GetAudioStreamData(nint, byte[], int)"/> before this function's return value is no longer
    /// clamped.</para>
    /// </summary>
    /// <param name="stream">the audio stream to query.</param>
    /// <returns>the number of bytes queued or -1 on failure; call <see cref="GetError"/>
    /// for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="PutAudioStreamData(nint, byte[], int)"/>
    /// <seealso cref="ClearAudioStream"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAudioStreamQueued"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetAudioStreamQueued(IntPtr stream);


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_FlushAudioStream(SDL_AudioStream *stream);</code>
    /// <summary>
    /// <para>Tell the stream that you're done sending data, and anything being buffered
    /// should be converted/resampled and made available immediately.</para>
    /// <para>It is legal to add more data to a stream after flushing, but there may be
    /// audio gaps in the output. Generally this is intended to signal the end of
    /// input, so the complete output becomes available.</para>
    /// </summary>
    /// <param name="stream">the audio stream to flush.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="PutAudioStreamData(nint, byte[], int)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_FlushAudioStream"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool FlushAudioStream(IntPtr stream);


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ClearAudioStream(SDL_AudioStream *stream);</code>
    /// <summary>
    /// <para>Clear any pending data in the stream.</para>
    /// <para>This drops any queued data, so there will be nothing to read from the
    /// stream until more is added.</para>
    /// </summary>
    /// <param name="stream">the audio stream to clear.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetAudioStreamAvailable"/>
    /// <seealso cref="GetAudioStreamData(nint, byte[], int)"/>
    /// <seealso cref="GetAudioStreamQueued"/>
    /// <seealso cref="PutAudioStreamData(nint, byte[], int)"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ClearAudioStream"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ClearAudioStream(IntPtr stream);


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_PauseAudioStreamDevice(SDL_AudioStream *stream);</code>
    /// <summary>
    /// <para>Use this function to pause audio playback on the audio device associated
    /// with an audio stream.</para>
    /// <para>This function pauses audio processing for a given device. Any bound audio
    /// streams will not progress, and no audio will be generated. Pausing one
    /// device does not prevent other unpaused devices from running.</para>
    /// <para>Pausing a device can be useful to halt all audio without unbinding all the
    /// audio streams. This might be useful while a game is paused, or a level is
    /// loading, etc.</para>
    /// </summary>
    /// <param name="stream">the audio stream associated with the audio device to pause.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ResumeAudioStreamDevice"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_PauseAudioStreamDevice"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool PauseAudioStreamDevice(IntPtr stream);


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ResumeAudioStreamDevice(SDL_AudioStream *stream);</code>
    /// <summary>
    /// <para>Use this function to unpause audio playback on the audio device associated
    /// with an audio stream.</para>
    /// <para>This function unpauses audio processing for a given device that has
    /// previously been paused. Once unpaused, any bound audio streams will begin
    /// to progress again, and audio can be generated.</para>
    /// <para><see cref="OpenAudioDeviceStream(uint, in IntPtr, AudioStreamCallback, IntPtr)"/> opens audio devices in a paused state, so this
    /// function call is required for audio playback to begin on such devices.</para>
    /// </summary>
    /// <param name="stream">the audio stream associated with the audio device to resume.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="PauseAudioStreamDevice"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ResumeAudioStreamDevice"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ResumeAudioStreamDevice(IntPtr stream);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_AudioStreamDevicePaused(SDL_AudioStream *stream);</code>
    /// <summary>
    /// <para>Use this function to query if an audio device associated with a stream is
    /// paused.</para>
    /// <para>Unlike in SDL2, audio devices start in an _unpaused_ state, since an app
    /// has to bind a stream before any audio will flow.</para>
    /// </summary>
    /// <param name="stream">the audio stream associated with the audio device to query.</param>
    /// <returns><c>true</c> if device is valid and paused, <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.1.10.</since>
    /// <seealso cref="PauseAudioStreamDevice"/>
    /// <seealso cref="ResumeAudioStreamDevice"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_AudioStreamDevicePaused"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool AudioStreamDevicePaused(IntPtr stream);


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_LockAudioStream(SDL_AudioStream *stream);</code>
    /// <summary>
    /// <para>Lock an audio stream for serialized access.</para>
    /// <para>Each SDL_AudioStream has an internal mutex it uses to protect its data
    /// structures from threading conflicts. This function allows an app to lock
    /// that mutex, which could be useful if registering callbacks on this stream.</para>
    /// <para>One does not need to lock a stream to use in it most cases, as the stream
    /// manages this lock internally. However, this lock is held during callbacks,
    /// which may run from arbitrary threads at any time, so if an app needs to
    /// protect shared data during those callbacks, locking the stream guarantees
    /// that the callback is not running while the lock is held.</para>
    /// <para>As this is just a wrapper over SDL_LockMutex for an internal lock; it has
    /// all the same attributes (recursive locks are allowed, etc).</para>
    /// </summary>
    /// <param name="stream">the audio stream to lock.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="UnlockAudioStream"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_LockAudioStream"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool LockAudioStream(IntPtr stream);


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_UnlockAudioStream(SDL_AudioStream *stream);</code>
    /// <summary>
    /// <para>Unlock an audio stream for serialized access.</para>
    /// <para>This unlocks an audio stream after a call to <see cref="LockAudioStream"/>.</para>
    /// </summary>
    /// <param name="stream">the audio stream to unlock.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>You should only call this from the same thread that
    /// previously called <see cref="LockAudioStream"/>.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="LockAudioStream"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_UnlockAudioStream"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool UnlockAudioStream(IntPtr stream);


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetAudioStreamGetCallback(SDL_AudioStream *stream, SDL_AudioStreamCallback callback, void *userdata);</code>
    /// <summary>
    /// <para>Set a callback that runs when data is requested from an audio stream.</para>
    /// <para>This callback is called _before_ data is obtained from the stream, giving
    /// the callback the chance to add more on-demand.</para>
    /// <para>The callback can (optionally) call <see cref="PutAudioStreamData(nint, byte[], int)"/> to add more
    /// audio to the stream during this call; if needed, the request that triggered
    /// this callback will obtain the new data immediately.</para>
    /// <para>The callback's <c>additional_amount</c> argument is roughly how many bytes of
    /// _unconverted_ data (in the stream's input format) is needed by the caller,
    /// although this may overestimate a little for safety. This takes into account
    /// how much is already in the stream and only asks for any extra necessary to
    /// resolve the request, which means the callback may be asked for zero bytes,
    /// and a different amount on each call.</para>
    /// <para>The callback is not required to supply exact amounts; it is allowed to
    /// supply too much or too little or none at all. The caller will get what's
    /// available, up to the amount they requested, regardless of this callback's
    /// outcome.</para>
    /// <para>Clearing or flushing an audio stream does not call this callback.</para>
    /// <para>This function obtains the stream's lock, which means any existing callback
    /// (get or put) in progress will finish running before setting the new
    /// callback.</para>
    /// <para>Setting a <c>null</c> function turns off the callback.</para>
    /// </summary>
    /// <param name="stream">the audio stream to set the new callback on.</param>
    /// <param name="callback">the new callback function to call when data is requested
    /// from the stream.</param>
    /// <param name="userdata">an opaque pointer provided to the callback for its own
    /// personal use.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information. This only fails if <c>stream</c> is <c>null</c>.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetAudioStreamPutCallback"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetAudioStreamGetCallback"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetAudioStreamGetCallback(IntPtr stream, AudioStreamCallback? callback, IntPtr userdata);


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetAudioStreamPutCallback(SDL_AudioStream *stream, SDL_AudioStreamCallback callback, void *userdata);</code>
    /// <summary>
    /// <para>Set a callback that runs when data is added to an audio stream.</para>
    /// <para>This callback is called _after_ the data is added to the stream, giving the
    /// callback the chance to obtain it immediately.</para>
    /// <para>The callback can (optionally) call <see cref="GetAudioStreamData(nint, byte[], int)"/> to obtain audio
    /// from the stream during this call.</para>
    /// <para>TThe callback's <c>additional_amount</c> argument is how many bytes of
    /// _converted_ data (in the stream's output format) was provided by the
    /// caller, although this may underestimate a little for safety. This value
    /// might be less than what is currently available in the stream, if data was
    /// already there, and might be less than the caller provided if the stream
    /// needs to keep a buffer to aid in resampling. Which means the callback may
    /// be provided with zero bytes, and a different amount on each call.</para>
    /// <para>The callback may call <see cref="GetAudioStreamAvailable"/> to see the total amount
    /// currently available to read from the stream, instead of the total provided
    /// by the current call.</para>
    /// <para>The callback is not required to obtain all data. It is allowed to read less
    /// or none at all. Anything not read now simply remains in the stream for
    /// later access.</para>
    /// <para>Clearing or flushing an audio stream does not call this callback.</para>
    /// <para>This function obtains the stream's lock, which means any existing callback
    /// (get or put) in progress will finish running before setting the new
    /// callback.</para>
    /// <para>Setting a <c>null</c> function turns off the callback.</para>
    /// </summary>
    /// <param name="stream">the audio stream to set the new callback on.</param>
    /// <param name="callback">the new callback function to call when data is added to the
    /// stream.</param>
    /// <param name="userdata">an opaque pointer provided to the callback for its own
    /// personal use.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information. This only fails if <c>stream</c> is <c>null</c>.</returns>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetAudioStreamPutCallback"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetAudioStreamPutCallback(IntPtr stream, AudioStreamCallback? callback, IntPtr userdata);


    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DestroyAudioStream(SDL_AudioStream *stream);</code>
    /// <summary>
    /// <para>Free an audio stream.</para>
    /// <para>This will release all allocated data, including any audio that is still
    /// queued. You do not need to manually clear the stream first.</para>
    /// <para>If this stream was bound to an audio device, it is unbound during this
    /// call. If this stream was created with <see cref="OpenAudioDeviceStream(uint, in nint, AudioStreamCallback, nint)"/>, the audio
    /// device that was opened alongside this stream's creation will be closed,
    /// too.</para>
    /// </summary>
    /// <param name="stream">the audio stream to destroy.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateAudioStream"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DestroyAudioStream"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyAudioStream(IntPtr stream);


    /// <code>extern SDL_DECLSPEC SDL_AudioStream * SDLCALL SDL_OpenAudioDeviceStream(SDL_AudioDeviceID devid, const SDL_AudioSpec *spec, SDL_AudioStreamCallback callback, void *userdata);</code>
    /// <summary>
    /// <para>Convenience function for straightforward audio init for the common case.</para>
    /// <para>If all your app intends to do is provide a single source of PCM audio, this
    /// function allows you to do all your audio setup in a single call.</para>
    /// <para>This is also intended to be a clean means to migrate apps from SDL2.</para>
    /// <para>This function will open an audio device, create a stream and bind it.
    /// Unlike other methods of setup, the audio device will be closed when this
    /// stream is destroyed, so the app can treat the returned SDL_AudioStream as
    /// the only object needed to manage audio playback.</para>
    /// <para>Also unlike other functions, the audio device begins paused. This is to map
    /// more closely to SDL2-style behavior, since there is no extra step here to
    /// bind a stream to begin audio flowing. The audio device should be resumed
    /// with <c>ResumeAudioStreamDevice().</c></para>
    /// <para>This function works with both playback and recording devices.</para>
    /// <para>The <c>spec</c> parameter represents the app's side of the audio stream. That
    /// is, for recording audio, this will be the output format, and for playing
    /// audio, this will be the input format. If spec is <c>null</c>, the system will
    /// choose the format, and the app can use <see cref="GetAudioStreamFormat"/> to obtain
    /// this information later.</para>
    /// <para>If you don't care about opening a specific audio device, you can (and
    /// probably _should_), use <see cref="AudioDeviceDefaultPlayback"/> for playback and
    /// <see cref="AudioDeviceDefaultRecording"/> for recording.</para>
    /// <para>One can optionally provide a callback function; if <c>null</c>, the app is
    /// expected to queue audio data for playback (or unqueue audio data if
    /// capturing). Otherwise, the callback will begin to fire once the device is
    /// unpaused.</para>
    /// <para>Destroying the returned stream with <see cref="DestroyAudioStream"/> will also close
    /// the audio device associated with this stream.</para>
    /// </summary>
    /// <param name="devid">an audio device to open, or <see cref="AudioDeviceDefaultPlayback"/>
    /// or <see cref="AudioDeviceDefaultRecording"/>.</param>
    /// <param name="spec">the audio stream's data format. Can be <c>null</c>.</param>
    /// <param name="callback">a callback where the app will provide new data for
    /// playback, or receive new data for recording. Can be <c>null</c>,
    /// in which case the app will need to call
    /// <see cref="PutAudioStreamData(nint, byte[], int)"/> or <see cref="GetAudioStreamData(nint, byte[], int)"/> as
    /// necessary.</param>
    /// <param name="userdata">app-controlled pointer passed to callback. Can be <c>null</c>.
    /// Ignored if callback is <c>null</c>.</param>
    /// <returns>an audio stream on success, ready to use, or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information. When done with this stream,
    /// call <see cref="DestroyAudioStream"/> to free resources and close the
    /// device.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetAudioStreamDevice"/>
    /// <seealso cref="ResumeAudioStreamDevice"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_OpenAudioDeviceStream"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr OpenAudioDeviceStream(uint devid, in AudioSpec spec, AudioStreamCallback? callback, IntPtr userdata);
    
    
    /// <code>extern SDL_DECLSPEC SDL_AudioStream * SDLCALL SDL_OpenAudioDeviceStream(SDL_AudioDeviceID devid, const SDL_AudioSpec *spec, SDL_AudioStreamCallback callback, void *userdata);</code>
    /// <summary>
    /// <para>Convenience function for straightforward audio init for the common case.</para>
    /// <para>If all your app intends to do is provide a single source of PCM audio, this
    /// function allows you to do all your audio setup in a single call.</para>
    /// <para>This is also intended to be a clean means to migrate apps from SDL2.</para>
    /// <para>This function will open an audio device, create a stream and bind it.
    /// Unlike other methods of setup, the audio device will be closed when this
    /// stream is destroyed, so the app can treat the returned SDL_AudioStream as
    /// the only object needed to manage audio playback.</para>
    /// <para>Also unlike other functions, the audio device begins paused. This is to map
    /// more closely to SDL2-style behavior, since there is no extra step here to
    /// bind a stream to begin audio flowing. The audio device should be resumed
    /// with <c>ResumeAudioStreamDevice(stream);</c></para>
    /// <para>This function works with both playback and recording devices.</para>
    /// <para>The <c>spec</c> parameter represents the app's side of the audio stream. That
    /// is, for recording audio, this will be the output format, and for playing
    /// audio, this will be the input format. If spec is <c>null</c>, the system will
    /// choose the format, and the app can use <see cref="GetAudioStreamFormat"/> to obtain
    /// this information later.</para>
    /// <para>If you don't care about opening a specific audio device, you can (and
    /// probably _should_), use <see cref="AudioDeviceDefaultPlayback"/> for playback and
    /// <see cref="AudioDeviceDefaultRecording"/> for recording.</para>
    /// <para>One can optionally provide a callback function; if <c>null</c>, the app is
    /// expected to queue audio data for playback (or unqueue audio data if
    /// capturing). Otherwise, the callback will begin to fire once the device is
    /// unpaused.</para>
    /// <para>Destroying the returned stream with <see cref="DestroyAudioStream"/> will also close
    /// the audio device associated with this stream.</para>
    /// </summary>
    /// <param name="devid">an audio device to open, or <see cref="AudioDeviceDefaultPlayback"/>
    /// or <see cref="AudioDeviceDefaultRecording"/>.</param>
    /// <param name="spec">the audio stream's data format. Can be <c>null</c>.</param>
    /// <param name="callback">a callback where the app will provide new data for
    /// playback, or receive new data for recording. Can be <c>null</c>,
    /// in which case the app will need to call
    /// <see cref="PutAudioStreamData(nint, byte[], int)"/> or <see cref="GetAudioStreamData(nint, byte[], int)"/> as
    /// necessary.</param>
    /// <param name="userdata">app-controlled pointer passed to callback. Can be <c>null</c>.
    /// Ignored if callback is <c>null</c>.</param>
    /// <returns>an audio stream on success, ready to use, or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information. When done with this stream,
    /// call <see cref="DestroyAudioStream"/> to free resources and close the
    /// device.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetAudioStreamDevice"/>
    /// <seealso cref="ResumeAudioStreamDevice"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_OpenAudioDeviceStream"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr OpenAudioDeviceStream(uint devid, IntPtr spec, AudioStreamCallback? callback, IntPtr userdata);


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetAudioPostmixCallback(SDL_AudioDeviceID devid, SDL_AudioPostmixCallback callback, void *userdata);</code>
    /// <summary>
    /// <para>Set a callback that fires when data is about to be fed to an audio device.</para>
    /// <para>This is useful for accessing the final mix, perhaps for writing a
    /// visualizer or applying a final effect to the audio data before playback.</para>
    /// <para>The buffer is the final mix of all bound audio streams on an opened device;
    /// this callback will fire regularly for any device that is both opened and
    /// unpaused. If there is no new data to mix, either because no streams are
    /// bound to the device or all the streams are empty, this callback will still
    /// fire with the entire buffer set to silence.</para>
    /// <para>This callback is allowed to make changes to the data; the contents of the
    /// buffer after this call is what is ultimately passed along to the hardware.</para>
    /// <para>The callback is always provided the data in float format (values from -1.0f
    /// to 1.0f), but the number of channels or sample rate may be different than
    /// the format the app requested when opening the device; SDL might have had to
    /// manage a conversion behind the scenes, or the playback might have jumped to
    /// new physical hardware when a system default changed, etc. These details may
    /// change between calls. Accordingly, the size of the buffer might change
    /// between calls as well.</para>
    /// <para>This callback can run at any time, and from any thread; if you need to
    /// serialize access to your app's data, you should provide and use a mutex or
    /// other synchronization device.</para>
    /// <para>All of this to say: there are specific needs this callback can fulfill, but
    /// it is not the simplest interface. Apps should generally provide audio in
    /// their preferred format through an SDL_AudioStream and let SDL handle the
    /// difference.</para>
    /// <para>This function is extremely time-sensitive; the callback should do the least
    /// amount of work possible and return as quickly as it can. The longer the
    /// callback runs, the higher the risk of audio dropouts or other problems.</para>
    /// <para>This function will block until the audio device is in between iterations,
    /// so any existing callback that might be running will finish before this
    /// function sets the new callback and returns.</para>
    /// <para>Setting a <c>null</c> callback function disables any previously-set callback.</para>
    /// </summary>
    /// <param name="devid">the ID of an opened audio device.</param>
    /// <param name="callback">a callback function to be called. Can be <c>null</c>.</param>
    /// <param name="userdata">app-controlled pointer passed to callback. Can be <c>null</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetAudioPostmixCallback"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetAudioPostmixCallback(uint devid, AudioPostmixCallback? callback, IntPtr userdata);


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_LoadWAV_IO(SDL_IOStream *src, bool closeio, SDL_AudioSpec *spec, Uint8 **audio_buf, Uint32 *audio_len);</code>
    /// <summary>
    /// <para>Load the audio data of a WAVE file into memory.</para>
    /// <para>Loading a WAVE file requires <c>src</c>, <c>spec</c>, <c>audioBuf</c> and <c>audioLen</c> to
    /// be valid pointers. The entire data portion of the file is then loaded into
    /// memory and decoded if necessary.</para>
    /// <para>Supported formats are RIFF WAVE files with the formats PCM (8, 16, 24, and
    /// 32 bits), IEEE Float (32 bits), Microsoft ADPCM and IMA ADPCM (4 bits), and
    /// A-law and mu-law (8 bits). Other formats are currently unsupported and
    /// cause an error.</para>
    /// <para>If this function succeeds, the return value is zero and the pointer to the
    /// audio data allocated by the function is written to <c>audioBuf</c> and its
    /// length in bytes to <c>audioLen</c>. The <see cref="AudioSpec"/> members <c>freq</c>,
    /// <c>channels</c>, and <c>format</c> are set to the values of the audio data in the
    /// buffer.</para>
    /// <para>It's necessary to use <see cref="Free"/> to free the audio data returned in
    /// <c>audioBuf</c> when it is no longer used.</para>
    /// <para>Because of the underspecification of the .WAV format, there are many
    /// problematic files in the wild that cause issues with strict decoders. To
    /// provide compatibility with these files, this decoder is lenient in regards
    /// to the truncation of the file, the fact chunk, and the size of the RIFF
    /// chunk. The hints <see cref="Hints.WaveRiffChunkSize"/>,
    /// <see cref="Hints.WaveTruncation"/>, and <see cref="Hints.WaveFactChunk"/> can be used to
    /// tune the behavior of the loading process.</para>
    /// <para>Any file that is invalid (due to truncation, corruption, or wrong values in
    /// the headers), too big, or unsupported causes an error. Additionally, any
    /// critical I/O error from the data source will terminate the loading process
    /// with an error. The function returns <c>null</c> on error and in all cases (with
    /// the exception of <c>src</c> being <c>null</c>), an appropriate error message will be
    /// set.</para>
    /// <para>It is required that the data source supports seeking.</para>
    /// <para>Example:</para>
    /// <para><code>
    /// LoadWAVIO(IOFromFile("sample.wav", "rb"), true, out var spec, out var buf, var len);
    /// </code></para>
    /// <para>Note that the <see cref="LoadWAV"/> function does this same thing for you, but in a
    /// less messy way:</para>
    /// <para><code>
    /// LoadWAV("sample.wav", out var spec, out var buf, out var len);
    /// </code></para>
    /// </summary>
    /// <param name="src">the data source for the WAVE data.</param>
    /// <param name="closeio">if true, calls <see cref="CloseIO"/> on <c>src</c> before returning, even
    /// in the case of an error.</param>
    /// <param name="spec">a pointer to an <see cref="AudioSpec"/> that will be set to the WAVE
    /// data's format details on successful return.</param>
    /// <param name="audioBuf">a pointer filled with the audio data, allocated by the
    /// function.</param>
    /// <param name="audioLen">a pointer filled with the length of the audio data buffer
    /// in bytes.</param>
    /// <returns>
    /// <para><c>true</c> on success. <c>audioBuf</c> will be filled with a pointer to an
    /// allocated buffer containing the audio data, and <c>audioLen</c> is
    /// filled with the length of that audio buffer in bytes.</para>
    /// <para>This function returns false if the .WAV file cannot be opened,
    /// uses an unknown data format, or is corrupt; call <see cref="GetError"/>
    /// for more information.</para>
    /// <para>When the application is done with the data returned in
    /// <c>audioBuf</c>, it should call <see cref="Free"/> to dispose of it.</para>
    /// </returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="Free"/>
    /// <seealso cref="LoadWAV"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_LoadWAV_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool LoadWAVIO(IntPtr src, [MarshalAs(UnmanagedType.I1)] bool closeio, out AudioSpec spec, out IntPtr audioBuf, out uint audioLen);


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_LoadWAV(const char *path, SDL_AudioSpec *spec, Uint8 **audio_buf, Uint32 *audio_len);</code>
    /// <summary>
    /// <para>Loads a WAV from a file path.</para>
    /// <para>This is a convenience function that is effectively the same as:</para>
    /// <para><code>LoadWAVIO(IOFromFile(path, "rb"), true, spec, audio_buf, audio_len);</code></para>
    /// </summary>
    /// <param name="path">the file path of the WAV file to open.</param>
    /// <param name="spec">a pointer to an <see cref="AudioSpec"/> that will be set to the WAVE
    /// data's format details on successful return.</param>
    /// <param name="audioBuf">a pointer filled with the audio data, allocated by the
    /// function.</param>
    /// <param name="audioLen">a pointer filled with the length of the audio data buffer
    /// in bytes.</param>
    /// <returns>
    /// <para>true on success. <c>audioBuf</c> will be filled with a pointer to an
    /// allocated buffer containing the audio data, and <c>audioLen</c> is
    /// filled with the length of that audio buffer in bytes.</para>
    /// <para>This function returns false if the .WAV file cannot be opened,
    /// uses an unknown data format, or is corrupt; call <see cref="GetError"/>
    /// for more information.</para>
    /// <para>When the application is done with the data returned in
    /// <c>audioBuf</c>, it should call <see cref="Free"/> to dispose of it.</para>
    /// </returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="Free"/>
    /// <seealso cref="LoadWAVIO"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_LoadWAV"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool LoadWAV([MarshalAs(UnmanagedType.LPUTF8Str)] string path, out AudioSpec spec, out IntPtr audioBuf, out uint audioLen);


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_MixAudio(Uint8 *dst, const Uint8 *src, SDL_AudioFormat format, Uint32 len, float volume);</code>
    /// <summary>
    /// <para>Mix audio data in a specified format.</para>
    /// <para>This takes an audio buffer <c>src</c> of <c>len</c> bytes of <c>format</c> data and mixes
    /// it into <c>dst</c>, performing addition, volume adjustment, and overflow
    /// clipping. The buffer pointed to by <c>dst</c> must also be <c>len</c> bytes of
    /// <c>format</c> data.</para>
    /// <para>This is provided for convenience -- you can mix your own audio data.</para>
    /// <para>Do not use this function for mixing together more than two streams of
    /// sample data. The output from repeated application of this function may be
    /// distorted by clipping, because there is no accumulator with greater range
    /// than the input (not to mention this being an inefficient way of doing it).</para>
    /// <para>It is a common misconception that this function is required to write audio
    /// data to an output stream in an audio callback. While you can do that,
    /// <see cref="MixAudio"/> is really only needed when you're mixing a single audio
    /// stream with a volume adjustment.</para>
    /// </summary>
    /// <param name="dst">the destination for the mixed audio.</param>
    /// <param name="src">the source audio buffer to be mixed.</param>
    /// <param name="format">the <see cref="AudioFormat"/> structure representing the desired audio
    /// format.</param>
    /// <param name="len">the length of the audio buffer in bytes.</param>
    /// <param name="volume">ranges from 0.0 - 1.0, and should be set to 1.0 for full
    /// audio volume.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_MixAudio"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool MixAudio(IntPtr dst, IntPtr src, AudioFormat format, uint len, float volume);


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ConvertAudioSamples(const SDL_AudioSpec *src_spec, const Uint8 *src_data, int src_len, const SDL_AudioSpec *dst_spec, Uint8 **dst_data, int *dst_len);</code>
    /// <summary>
    /// <para>Convert some audio data of one format to another format.</para>
    /// <para>Please note that this function is for convenience, but should not be used
    /// to resample audio in blocks, as it will introduce audio artifacts on the
    /// boundaries. You should only use this function if you are converting audio
    /// data in its entirety in one call. If you want to convert audio in smaller
    /// chunks, use an SDL_AudioStream, which is designed for this situation.</para>
    /// <para>Internally, this function creates and destroys an SDL_AudioStream on each
    /// use, so it's also less efficient than using one directly, if you need to
    /// convert multiple times.</para>
    /// </summary>
    /// <param name="srcSpec">the format details of the input audio.</param>
    /// <param name="srcData">the audio data to be converted.</param>
    /// <param name="srcLen">the len of src_data.</param>
    /// <param name="dstSpec">the format details of the output audio.</param>
    /// <param name="dstData">will be filled with a pointer to converted audio data,
    /// which should be freed with <see cref="Free"/>. On error, it will be
    /// <c>null</c>.</param>
    /// <param name="dstLen">will be filled with the len of <c>dstData</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ConvertAudioSamples"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ConvertAudioSamples(in AudioSpec srcSpec, IntPtr srcData, int srcLen, in AudioSpec dstSpec, out IntPtr dstData, out int dstLen);

    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAudioFormatName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetAudioFormatName(AudioFormat format);
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetAudioFormatName(SDL_AudioFormat format);</code>
    /// <summary>
    /// Get the human readable name of an audio format.
    /// </summary>
    /// <param name="format">the audio format to query.</param>
    /// <returns>the human readable name of the specified audio format or
    /// <see cref="AudioFormat.Unknown"/> if the format isn't recognized.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static string GetAudioFormatName(AudioFormat format)
    {
        var value = SDL_GetAudioFormatName(format); 
        return value == IntPtr.Zero ? "" : Marshal.PtrToStringUTF8(value)!;
    }


    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetSilenceValueForFormat(SDL_AudioFormat format);</code>
    /// <summary>
    /// <para>Get the appropriate memset value for silencing an audio format.</para>
    /// <para>The value returned by this function can be used as the second argument to
    /// memset (or <see cref="Memset"/>) to set an audio buffer in a specific format to
    /// silence.</para>
    /// </summary>
    /// <param name="format">the audio data format to query.</param>
    /// <returns>a byte value that can be passed to memset.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSilenceValueForFormat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetSilenceValueForFormat(AudioFormat format);
}