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

public partial class Mixer
{
    /// <code>extern SDL_DECLSPEC int SDLCALL Mix_Version(void);</code>
    /// <summary>
    /// This function gets the version of the dynamically linked SDL_mixer library.
    /// </summary>
    /// <returns>SDL_mixer version.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_Version"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Version();
    
    
    /// <code>extern SDL_DECLSPEC MIX_InitFlags SDLCALL Mix_Init(MIX_InitFlags flags);</code>
    /// <summary>
    /// <para>Initialize SDL_mixer.</para>
    /// <para>This function loads dynamic libraries that SDL_mixer needs, and prepares
    /// them for use.</para>
    /// <para>Note that, unlike other SDL libraries, this call is optional! If you load a
    /// music file, SDL_mixer will handle initialization on the fly. This function
    /// will let you know, up front, whether a specific format will be available
    /// for use.</para>
    /// <para>Flags should be one or more flags from <see cref="InitFlags"/> OR'd together. It
    /// returns the flags successfully initialized, or 0 on failure.</para>
    /// <para>Currently, these flags are:</para>
    /// <list type="bullet">
    /// <item><see cref="InitFlags.FLAC"/></item>
    /// <item><see cref="InitFlags.MOD"/></item>
    /// <item><see cref="InitFlags.MP3"/></item>
    /// <item><see cref="InitFlags.OGG"/></item>
    /// <item><see cref="InitFlags.MID"/></item>
    /// <item><see cref="InitFlags.OPUS"/></item>
    /// <item><see cref="InitFlags.WAVPACK"/></item>
    /// </list>
    /// <para>More flags may be added in a future SDL_mixer release.</para>
    /// <para>This function may need to load external shared libraries to support various
    /// codecs, which means this function can fail to initialize that support on an
    /// otherwise-reasonable system if the library isn't available; this is not
    /// just a question of exceptional circumstances like running out of memory at
    /// startup!</para>
    /// <para>Note that you may call this function more than once to initialize with
    /// additional flags. The return value will reflect both new flags that
    /// successfully initialized, and also include flags that had previously been
    /// initialized as well.</para>
    /// <para>As this will return previously-initialized flags, it's legal to call this
    /// with zero (no flags set). This is a safe no-op that can be used to query
    /// the current initialization state without changing it at all.</para>
    /// <para>Since this returns previously-initialized flags as well as new ones, and
    /// you can call this with zero, you should not check for a zero return value
    /// to determine an error condition. Instead, you should check to make sure all
    /// the flags you require are set in the return value. If you have a game with
    /// data in a specific format, this might be a fatal error. If you're a generic
    /// media player, perhaps you are fine with only having WAV and MP3 support and
    /// can live without Opus playback, even if you request support for everything.</para>
    /// <para>Unlike other SDL satellite libraries, calls to <see cref="Init"/> do not stack; a
    /// single call to <see cref="Quit"/> will deinitialize everything and does not have to
    /// be paired with a matching <see cref="Init"/> call. For that reason, it's considered
    /// best practices to have a single Mix_Init and <see cref="Quit"/> call in your program.
    /// While this isn't required, be aware of the risks of deviating from that
    /// behavior.</para>
    /// <para>After initializing SDL_mixer, the next step is to open an audio device to
    /// prepare to play sound (with <see cref="OpenAudio(uint, in SDL.AudioSpec)"/>), and load audio data to play
    /// with that device.</para>
    /// </summary>
    /// <param name="flags">initialization flags, OR'd together.</param>
    /// <returns>all currently initialized flags.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="Quit"/>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_Init"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial InitFlags Init(InitFlags flags);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL Mix_Quit(void);</code>
    /// <summary>
    /// <para>Deinitialize SDL_mixer.</para>
    /// <para>This should be the last function you call in SDL_mixer, after freeing all
    /// other resources and closing all audio devices. This will unload any shared
    /// libraries it is using for various codecs.</para>
    /// <para>After this call, a call to Init(0) will return 0 (no codecs loaded).</para>
    /// <para>You can safely call <see cref="Init"/> to reload various codec support after this
    /// call.</para>
    /// <para>Unlike other SDL satellite libraries, calls to <see cref="Init"/> do not stack; a
    /// single call to <see cref="Quit"/> will deinitialize everything and does not have to
    /// be paired with a matching <see cref="Init"/> call. For that reason, it's considered
    /// best practices to have a single Mix_Init and <see cref="Quit"/> call in your program.
    /// While this isn't required, be aware of the risks of deviating from that
    /// behavior.</para>
    /// </summary>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="Init"/>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_Quit"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Quit();
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL Mix_OpenAudio(SDL_AudioDeviceID devid, const SDL_AudioSpec *spec);</code>
    /// <summary>
    /// <para>Open an audio device for playback.</para>
    /// <para>An audio device is what generates sound, so the app must open one to make
    /// noise.</para>
    /// <para>This function will check if SDL's audio system is initialized, and if not,
    /// it will initialize it by calling <c>SDL.Init(SDL.InitFlags.Audio)</c> on your behalf.
    /// You are free to (and encouraged to!) initialize it yourself before calling
    /// this function, as this gives your program more control over the process.</para>
    /// <para>If you aren't particularly concerned with the specifics of the audio
    /// device, and your data isn't in a specific format, you can pass a <c>null</c> for
    /// the <c>spec</c> parameter and SDL_mixer will choose a reasonable default.
    /// SDL_mixer will convert audio data you feed it to the hardware's format
    /// behind the scenes.</para>
    /// <para>That being said, if you have control of your audio data and you know its
    /// format ahead of time, you may save CPU time by opening the audio device in
    /// that exact format so SDL_mixer does not have to spend time converting
    /// anything behind the scenes, and can just pass the data straight through to
    /// the hardware.</para>
    /// <para>The other reason to care about specific formats: if you plan to touch the
    /// mix buffer directly (with <see cref="SetPostMix"/>, a registered effect, or
    /// <see cref="HookMusic"/>), you might have code that expects it to be in a specific
    /// format, and you should specify that here.</para>
    /// <para>This function allows you to select specific audio hardware on the system
    /// with the <c>devid</c> parameter. If you specify 0, SDL_mixer will choose the
    /// best default it can on your behalf (which, in many cases, is exactly what
    /// you want anyhow). This is equivalent to specifying
    /// <see cref="SDL.AudioDeviceDefaultPlayback"/>, but is less wordy. SDL_mixer does not
    /// offer a mechanism to determine device IDs to open, but you can use
    /// <see cref="SDL.GetAudioPlaybackDevices"/> to get a list of available devices. If you do
    /// this, be sure to call <c>SDL.Init(SDL.InitFlags.Audio)</c> first to initialize SDL's
    /// audio system!</para>
    /// <para>If this function reports success, you are ready to start making noise! Load
    /// some audio data and start playing!</para>
    /// <para>When done with an audio device, probably at the end of the program, the app
    /// should close the audio with <see cref="CloseAudio"/>.</para>
    /// </summary>
    /// <param name="devid">the device name to open, or 0 for a reasonable default.</param>
    /// <param name="spec">the audio format you'd like SDL_mixer to work in.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="CloseAudio"/>
    /// <seealso cref="QuerySpec"/>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_OpenAudio"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool OpenAudio(uint devid, IntPtr spec);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL Mix_OpenAudio(SDL_AudioDeviceID devid, const SDL_AudioSpec *spec);</code>
    /// <summary>
    /// <para>Open an audio device for playback.</para>
    /// <para>An audio device is what generates sound, so the app must open one to make
    /// noise.</para>
    /// <para>This function will check if SDL's audio system is initialized, and if not,
    /// it will initialize it by calling <c>SDL.Init(SDL.InitFlags.Audio)</c> on your behalf.
    /// You are free to (and encouraged to!) initialize it yourself before calling
    /// this function, as this gives your program more control over the process.</para>
    /// <para>If you aren't particularly concerned with the specifics of the audio
    /// device, and your data isn't in a specific format, you can pass a <c>null</c> for
    /// the <c>spec</c> parameter and SDL_mixer will choose a reasonable default.
    /// SDL_mixer will convert audio data you feed it to the hardware's format
    /// behind the scenes.</para>
    /// <para>That being said, if you have control of your audio data and you know its
    /// format ahead of time, you may save CPU time by opening the audio device in
    /// that exact format so SDL_mixer does not have to spend time converting
    /// anything behind the scenes, and can just pass the data straight through to
    /// the hardware.</para>
    /// <para>The other reason to care about specific formats: if you plan to touch the
    /// mix buffer directly (with <see cref="SetPostMix"/>, a registered effect, or
    /// <see cref="HookMusic"/>), you might have code that expects it to be in a specific
    /// format, and you should specify that here.</para>
    /// <para>This function allows you to select specific audio hardware on the system
    /// with the <c>devid</c> parameter. If you specify 0, SDL_mixer will choose the
    /// best default it can on your behalf (which, in many cases, is exactly what
    /// you want anyhow). This is equivalent to specifying
    /// <see cref="SDL.AudioDeviceDefaultPlayback"/>, but is less wordy. SDL_mixer does not
    /// offer a mechanism to determine device IDs to open, but you can use
    /// <see cref="SDL.GetAudioPlaybackDevices"/> to get a list of available devices. If you do
    /// this, be sure to call <c>SDL.Init(SDL.InitFlags.Audio)</c> first to initialize SDL's
    /// audio system!</para>
    /// <para>If this function reports success, you are ready to start making noise! Load
    /// some audio data and start playing!</para>
    /// <para>When done with an audio device, probably at the end of the program, the app
    /// should close the audio with <see cref="CloseAudio"/>.</para>
    /// </summary>
    /// <param name="devid">the device name to open, or 0 for a reasonable default.</param>
    /// <param name="spec">the audio format you'd like SDL_mixer to work in.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="CloseAudio"/>
    /// <seealso cref="QuerySpec"/>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_OpenAudio"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool OpenAudio(uint devid, in SDL.AudioSpec spec);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL Mix_PauseAudio(int pause_on);</code>
    /// <summary>
    /// Suspend or resume the whole audio output.
    /// </summary>
    /// <param name="pauseOn">1 to pause audio output, or 0 to resume.</param>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_PauseAudio"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void PauseAudio(int pauseOn);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL Mix_QuerySpec(int *frequency, SDL_AudioFormat *format, int *channels);</code>
    /// <summary>
    /// <para>Find out what the actual audio device parameters are.</para>
    /// <para>Note this is only important if the app intends to touch the audio buffers
    /// being sent to the hardware directly. If an app just wants to play audio
    /// files and let SDL_mixer handle the low-level details, this function can
    /// probably be ignored.</para>
    /// <para>If the audio device is not opened, this function will return 0.</para>
    /// </summary>
    /// <param name="frequency">On return, will be filled with the audio device's
    /// frequency in Hz.</param>
    /// <param name="format">On return, will be filled with the audio device's format.</param>
    /// <param name="channels">On return, will be filled with the audio device's channel
    /// count.</param>
    /// <returns><c>true</c> if the audio device has been opened, <c>false</c> otherwise.</returns>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_QuerySpec"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool QuerySpec(out int frequency, out SDL.AudioFormat format, out int channels);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL Mix_AllocateChannels(int numchans);</code>
    /// <summary>
    /// <para>Dynamically change the number of channels managed by the mixer.</para>
    /// <para>SDL_mixer deals with "channels," which is not the same thing as the
    /// mono/stereo channels; they might be better described as "tracks," as each
    /// one corresponds to a separate source of audio data. Three different WAV
    /// files playing at the same time would be three separate SDL_mixer channels,
    /// for example.</para>
    /// <para>An app needs as many channels as it has audio data it wants to play
    /// simultaneously, mixing them into a single stream to send to the audio
    /// device.</para>
    /// <para>SDL_mixer allocates <seealso cref="Channels"/> (currently 8) channels when you open an
    /// audio device, which may be more than an app needs, but if the app needs
    /// more or wants less, this function can change it.</para>
    /// <para>If decreasing the number of channels, any upper channels currently playing
    /// are stopped. This will deregister all effects on those channels and call
    /// any callback specified by <see cref="ChannelFinished"/> for each removed channel.</para>
    /// <para>If <c>numchans</c> is less than zero, this will return the current number of
    /// channels without changing anything.</para>
    /// </summary>
    /// <param name="numchans">the new number of channels, or &lt; 0 to query current channel
    /// count.</param>
    /// <returns>the new number of allocated channels.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_AllocateChannels"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int AllocateChannels(int numchans);
    
    
    /// <code>extern SDL_DECLSPEC Mix_Chunk * SDLCALL Mix_LoadWAV_IO(SDL_IOStream *src, bool closeio);</code>
    /// <summary>
    /// <para>Load a supported audio format into a chunk.</para>
    /// <para>SDL_mixer has two separate data structures for audio data. One it calls a
    /// "chunk," which is meant to be a file completely decoded into memory up
    /// front, and the other it calls "music" which is a file intended to be
    /// decoded on demand. Originally, simple formats like uncompressed WAV files
    /// were meant to be chunks and compressed things, like MP3s, were meant to be
    /// music, and you would stream one thing for a game's music and make repeating
    /// sound effects with the chunks.</para>
    /// <para>In modern times, this isn't split by format anymore, and most are
    /// interchangeable, so the question is what the app thinks is worth
    /// predecoding or not. Chunks might take more memory, but once they are loaded
    /// won't need to decode again, whereas music always needs to be decoded on the
    /// fly. Also, crucially, there are as many channels for chunks as the app can
    /// allocate, but SDL_mixer only offers a single "music" channel.</para>
    /// <para>If <c>closeio</c> is true, the IOStream will be closed before returning, whether
    /// this function succeeds or not. SDL_mixer reads everything it needs from the
    /// IOStream during this call in any case.</para>
    /// <para>There is a separate function (a macro, before SDL_mixer 3.0.0) to read
    /// files from disk without having to deal with SDL_IOStream:
    /// <c>LoadWAV("filename.wav")</c> will call this function and manage those
    /// details for you.</para>
    /// <para>When done with a chunk, the app should dispose of it with a call to
    /// <see cref="FreeChunk"/>.</para>
    /// </summary>
    /// <param name="src">an SDL_IOStream that data will be read from.</param>
    /// <param name="closeio"><c>true</c> to close the SDL_IOStream before returning, <c>false</c> to
    /// leave it open.</param>
    /// <returns>a new chunk, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0</since>
    /// <seealso cref="LoadWAV"/>
    /// <seealso cref="FreeChunk"/>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_LoadWAV_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadWAVIO(IntPtr src, [MarshalAs(UnmanagedType.I1)] bool closeio);
    
    
    /// <code>extern SDL_DECLSPEC Mix_Chunk * SDLCALL Mix_LoadWAV(const char *file);</code>
    /// <summary>
    /// <para>Load a supported audio format into a chunk.</para>
    /// <para>SDL_mixer has two separate data structures for audio data. One it calls a
    /// "chunk," which is meant to be a file completely decoded into memory up
    /// front, and the other it calls "music" which is a file intended to be
    /// decoded on demand. Originally, simple formats like uncompressed WAV files
    /// were meant to be chunks and compressed things, like MP3s, were meant to be
    /// music, and you would stream one thing for a game's music and make repeating
    /// sound effects with the chunks.</para>
    /// <para>In modern times, this isn't split by format anymore, and most are
    /// interchangeable, so the question is what the app thinks is worth
    /// predecoding or not. Chunks might take more memory, but once they are loaded
    /// won't need to decode again, whereas music always needs to be decoded on the
    /// fly. Also, crucially, there are as many channels for chunks as the app can
    /// allocate, but SDL_mixer only offers a single "music" channel.</para>
    /// <para>If you would rather use the abstract SDL_IOStream interface to load data
    /// from somewhere other than the filesystem, you can use <see cref="LoadWAVIO"/>
    /// instead.</para>
    /// <para>When done with a chunk, the app should dispose of it with a call to
    /// <see cref="FreeChunk"/>.</para>
    /// <para>Note that before SDL_mixer 3.0.0, this function was a macro that called
    /// <see cref="LoadWAVIO"/>, creating a IOStream and setting <c>closeio</c> to true. This
    /// macro has since been promoted to a proper API function. Older binaries
    /// linked against a newer SDL_mixer will still call <see cref="LoadWAVIO"/> directly,
    /// as they are using the macro, which was available since the dawn of time.</para>
    /// </summary>
    /// <param name="file">the filesystem path to load data from.</param>
    /// <returns>a new chunk, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0</since>
    /// <seealso cref="LoadWAVIO"/>
    /// <seealso cref="FreeChunk"/>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_LoadWAV"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadWAV([MarshalAs(UnmanagedType.LPUTF8Str)] string file);
    
    
    /// <code>extern SDL_DECLSPEC Mix_Music * SDLCALL Mix_LoadMUS(const char *file);</code>
    /// <summary>
    /// <para>Load a supported audio format into a music object.</para>
    /// <para>SDL_mixer has two separate data structures for audio data. One it calls a
    /// "chunk," which is meant to be a file completely decoded into memory up
    /// front, and the other it calls "music" which is a file intended to be
    /// decoded on demand. Originally, simple formats like uncompressed WAV files
    /// were meant to be chunks and compressed things, like MP3s, were meant to be
    /// music, and you would stream one thing for a game's music and make repeating
    /// sound effects with the chunks.</para>
    /// <para>In modern times, this isn't split by format anymore, and most are
    /// interchangeable, so the question is what the app thinks is worth
    /// predecoding or not. Chunks might take more memory, but once they are loaded
    /// won't need to decode again, whereas music always needs to be decoded on the
    /// fly. Also, crucially, there are as many channels for chunks as the app can
    /// allocate, but SDL_mixer only offers a single "music" channel.</para>
    /// <para>When done with this music, the app should dispose of it with a call to
    /// <see cref="FreeMusic"/>.</para>
    /// </summary>
    /// <param name="file">a file path from where to load music data.</param>
    /// <returns>a new music object, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="FreeMusic"/>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_LoadMUS"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadMUS([MarshalAs(UnmanagedType.LPUTF8Str)] string file);
    
    
    /// <code>extern SDL_DECLSPEC Mix_Music * SDLCALL Mix_LoadMUS_IO(SDL_IOStream *src, bool closeio);</code>
    /// <summary>
    /// <para>Load a supported audio format into a music object.</para>
    /// <para>SDL_mixer has two separate data structures for audio data. One it calls a
    /// "chunk," which is meant to be a file completely decoded into memory up
    /// front, and the other it calls "music" which is a file intended to be
    /// decoded on demand. Originally, simple formats like uncompressed WAV files
    /// were meant to be chunks and compressed things, like MP3s, were meant to be
    /// music, and you would stream one thing for a game's music and make repeating
    /// sound effects with the chunks.</para>
    /// <para>In modern times, this isn't split by format anymore, and most are
    /// interchangeable, so the question is what the app thinks is worth
    /// predecoding or not. Chunks might take more memory, but once they are loaded
    /// won't need to decode again, whereas music always needs to be decoded on the
    /// fly. Also, crucially, there are as many channels for chunks as the app can
    /// allocate, but SDL_mixer only offers a single "music" channel.</para>
    /// <para>If <c>closeio</c> is true, the IOStream will be closed before returning, whether
    /// this function succeeds or not. SDL_mixer reads everything it needs from the
    /// IOStream during this call in any case.</para>
    /// <para>As a convenience, there is a function to read files from disk without
    /// having to deal with SDL_IOStream: <c>LoadMUS("filename.mp3")</c> will manage
    /// those details for you.</para>
    /// <para>This function attempts to guess the file format from incoming data. If the
    /// caller knows the format, or wants to force it, it should use
    /// <see cref="LoadMUSTypeIO"/> instead.</para>
    /// <para>When done with this music, the app should dispose of it with a call to
    /// <see cref="FreeMusic"/>.</para>
    /// </summary>
    /// <param name="src">an SDL_IOStream that data will be read from.</param>
    /// <param name="closeio"><c>true</c> to close the SDL_IOStream before returning, <c>false</c> to
    /// leave it open.</param>
    /// <returns>a new music object, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="FreeMusic"/>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_LoadMUS_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadMUSIO(IntPtr src, [MarshalAs(UnmanagedType.I1)] bool closeio);
    
    
    /// <code>extern SDL_DECLSPEC Mix_Music * SDLCALL Mix_LoadMUSType_IO(SDL_IOStream *src, Mix_MusicType type, bool closeio);</code>
    /// <summary>
    /// <para>Load an audio format into a music object, assuming a specific format.</para>
    /// <para>SDL_mixer has two separate data structures for audio data. One it calls a
    /// "chunk," which is meant to be a file completely decoded into memory up
    /// front, and the other it calls "music" which is a file intended to be
    /// decoded on demand. Originally, simple formats like uncompressed WAV files
    /// were meant to be chunks and compressed things, like MP3s, were meant to be
    /// music, and you would stream one thing for a game's music and make repeating
    /// sound effects with the chunks.</para>
    /// <para>In modern times, this isn't split by format anymore, and most are
    /// interchangeable, so the question is what the app thinks is worth
    /// predecoding or not. Chunks might take more memory, but once they are loaded
    /// won't need to decode again, whereas music always needs to be decoded on the
    /// fly. Also, crucially, there are as many channels for chunks as the app can
    /// allocate, but SDL_mixer only offers a single "music" channel.</para>
    /// <para>This function loads music data, and lets the application specify the type
    /// of music being loaded, which might be useful if SDL_mixer cannot figure it
    /// out from the data stream itself.</para>
    /// <para>Currently, the following types are supported:</para>
    /// <list type="bullet">
    /// <item><see cref="MusicType.None"/> (SDL_mixer should guess, based on the data)</item>
    /// <item><see cref="MusicType.WAV"/> (Microsoft WAV files)</item>
    /// <item><see cref="MusicType.MOD"/> (Various tracker formats)</item>
    /// <item><see cref="MusicType.MID"/> (MIDI files)</item>
    /// <item><see cref="MusicType.OGG"/> (Ogg Vorbis files)</item>
    /// <item><see cref="MusicType.MP3"/> (MP3 files)</item>
    /// <item><see cref="MusicType.FLAC"/> (FLAC files)</item>
    /// <item><see cref="MusicType.OPUS"/> (Opus files)</item>
    /// <item><see cref="MusicType.WAVPACK"/> (WavPack files)</item>
    /// </list>
    /// <para>If <c>closeio</c> is true, the IOStream will be closed before returning, whether
    /// this function succeeds or not. SDL_mixer reads everything it needs from the
    /// IOStream during this call in any case.</para>
    /// <para>As a convenience, there is a function to read files from disk without
    /// having to deal with SDL_IOStream: <c>LoadMUS("filename.mp3")</c> will manage
    /// those details for you (but not let you specify the music type explicitly)..</para>
    /// <para>When done with this music, the app should dispose of it with a call to
    /// <see cref="FreeMusic"/>.</para>
    /// </summary>
    /// <param name="src">an SDL_IOStream that data will be read from.</param>
    /// <param name="type">the type of audio data provided by <c>src</c>.</param>
    /// <param name="closeio"><c>true</c> to close the SDL_IOStream before returning, <c>false</c> to
    /// leave it open.</param>
    /// <returns>a new music object, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="FreeMusic"/>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_LoadMUSType_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadMUSTypeIO(IntPtr src, MusicType type, [MarshalAs(UnmanagedType.I1)] bool closeio);
    
    
    /// <code>extern SDL_DECLSPEC Mix_Chunk * SDLCALL Mix_QuickLoad_WAV(Uint8 *mem);</code>
    /// <summary>
    /// <para>Load a WAV file from memory as quickly as possible.</para>
    /// <para>Unlike <see cref="LoadWAVIO"/>, this function has several requirements, and unless
    /// you control all your audio data and know what you're doing, you should
    /// consider this function unsafe and not use it.</para>
    /// <list type="bullet">
    /// <item>The provided audio data MUST be in Microsoft WAV format.</item>
    /// <item>The provided audio data shouldn't use any strange WAV extensions.</item>
    /// <item>The audio data MUST be in the exact same format as the audio device. This
    /// function will not attempt to convert it, or even verify it's in the right
    /// format.</item>
    /// <item>The audio data must be valid; this function does not know the size of the
    /// memory buffer, so if the WAV data is corrupted, it can read past the end
    /// of the buffer, causing a crash.</item>
    /// <item>The audio data must live at least as long as the returned <see cref="Chunk"/>,
    /// because SDL_mixer will use that data directly and not make a copy of it.</item>
    /// </list>
    /// <para>This function will do NO error checking! Be extremely careful here!</para>
    /// <para>(Seriously, use <see cref="LoadWAVIO"/> instead.)</para>
    /// <para>If this function is successful, the provided memory buffer must remain
    /// available until <see cref="FreeChunk"/> is called on the returned chunk.</para>
    /// </summary>
    /// <param name="mem">memory buffer containing of a WAV file.</param>
    /// <returns>a new chunk, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="LoadWAVIO"/>
    /// <seealso cref="FreeChunk"/>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_QuickLoad_WAV"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr QuickLoadWAV(IntPtr mem);
    
    
    /// <code>extern SDL_DECLSPEC Mix_Chunk * SDLCALL Mix_QuickLoad_RAW(Uint8 *mem, Uint32 len);</code>
    /// <summary>
    /// <para>Load a raw audio data from memory as quickly as possible.</para>
    /// <para>The audio data MUST be in the exact same format as the audio device. This
    /// function will not attempt to convert it, or even verify it's in the right
    /// format.</para>
    /// <para>If this function is successful, the provided memory buffer must remain
    /// available until <see cref="FreeChunk"/> is called on the returned chunk.</para>
    /// </summary>
    /// <param name="mem">memory buffer containing raw PCM data.</param>
    /// <param name="len">length of buffer pointed to by `mem`, in bytes.</param>
    /// <returns>a new chunk, or <c>null</c> on error.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="FreeChunk"/>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_QuickLoad_RAW"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr QuickLoadRAW(IntPtr mem, uint len);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL Mix_FreeChunk(Mix_Chunk *chunk);</code>
    /// <summary>
    /// <para>Free an audio chunk.</para>
    /// <para>An app should call this function when it is done with a <see cref="Chunk"/> and wants
    /// to dispose of its resources.</para>
    /// <para>SDL_mixer will stop any channels this chunk is currently playing on. This
    /// will deregister all effects on those channels and call any callback
    /// specified by <see cref="ChannelFinished"/> for each removed channel.</para>
    /// </summary>
    /// <param name="chunk">the chunk to free.</param>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="LoadWAV"/>
    /// <seealso cref="LoadWAVIO"/>
    /// <seealso cref="QuickLoadWAV"/>
    /// <seealso cref="QuickLoadRAW"/>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_FreeChunk"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeChunk(IntPtr chunk);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL Mix_FreeMusic(Mix_Music *music);</code>
    /// <summary>
    /// <para>Free a music object.</para>
    /// <para>If this music is currently playing, it will be stopped.</para>
    /// <para>If this music is in the process of fading out (via <see cref="FadeOutMusic"/>),
    /// this function will <b>block</b> until the fade completes. If you need to avoid
    /// this, be sure to call <see cref="HaltMusic"/> before freeing the music.</para>
    /// </summary>
    /// <param name="music">the music object to free.</param>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="LoadMUS"/>
    /// <seealso cref="LoadMUSIO"/>
    /// <seealso cref="LoadMUSTypeIO"/>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_FreeMusic"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeMusic(IntPtr music);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL Mix_GetNumChunkDecoders(void);</code>
    /// <summary>
    /// <para>Get a list of chunk decoders that this build of SDL_mixer provides.</para>
    /// <para>This list can change between builds AND runs of the program, if external
    /// libraries that add functionality become available. You must successfully
    /// call <see cref="OpenAudio(uint, in SDL.AudioSpec)"/> before calling this function, as decoders are
    /// activated at device open time.</para>
    /// <para>Appearing in this list doesn't promise your specific audio file will
    /// decode...but it's handy to know if you have, say, a functioning Ogg Vorbis
    /// install.</para>
    /// <para>These return values are static, read-only data; do not modify or free it.
    /// The pointers remain valid until you call <see cref="CloseAudio"/>.</para>
    /// </summary>
    /// <returns>number of chunk decoders available.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="GetChunkDecoder"/>
    /// <seealso cref="HasChunkDecoder"/>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_GetNumChunkDecoders"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumChunkDecoders();
    
    
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_GetChunkDecoder"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr Mix_GetChunkDecoder(int index);
    /// <code>extern SDL_DECLSPEC const char * SDLCALL Mix_GetChunkDecoder(int index);</code>
    /// <summary>
    /// <para>Get a chunk decoder's name.</para>
    /// <para>The requested decoder's index must be between zero and
    /// <see cref="GetNumChunkDecoders"/>-1. It's safe to call this with an invalid index;
    /// this function will return <c>null</c> in that case.</para>
    /// <para>This list can change between builds AND runs of the program, if external
    /// libraries that add functionality become available. You must successfully
    /// call <see cref="OpenAudio(uint, in SDL.AudioSpec)"/> before calling this function, as decoders are
    /// activated at device open time.</para>
    /// </summary>
    /// <param name="index">index of the chunk decoder.</param>
    /// <returns>the chunk decoder's name.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="GetNumChunkDecoders"/>
    public static string GetChunkDecoder(int index)
    {
        var value = Mix_GetChunkDecoder(index); 
        return value == IntPtr.Zero ? "" : Marshal.PtrToStringUTF8(value)!;
    }
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL Mix_HasChunkDecoder(const char *name);</code>
    /// <summary>
    /// <para>Check if a chunk decoder is available by name.</para>
    /// <para>This result can change between builds AND runs of the program, if external
    /// libraries that add functionality become available. You must successfully
    /// call <see cref="OpenAudio(uint, in SDL.AudioSpec)"/> before calling this function, as decoders are
    /// activated at device open time.</para>
    /// <para>Decoder names are arbitrary but also obvious, so you have to know what
    /// you're looking for ahead of time, but usually it's the file extension in
    /// capital letters (some example names are "AIFF", "VOC", "WAV").</para>
    /// </summary>
    /// <param name="name">the decoder name to query.</param>
    /// <returns><c>true</c> if a decoder by that name is available, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="GetNumChunkDecoders"/>
    /// <seealso cref="GetChunkDecoder"/>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_HasChunkDecoder"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasChunkDecoder([MarshalAs(UnmanagedType.LPUTF8Str)] string name);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL Mix_GetNumMusicDecoders(void);</code>
    /// <summary>
    /// <para>Get a list of music decoders that this build of SDL_mixer provides.</para>
    /// <para>This list can change between builds AND runs of the program, if external
    /// libraries that add functionality become available. You must successfully
    /// call <see cref="OpenAudio(uint, in SDL.AudioSpec)"/> before calling this function, as decoders are
    /// activated at device open time.</para>
    /// <para>Appearing in this list doesn't promise your specific audio file will
    /// decode...but it's handy to know if you have, say, a functioning Ogg Vorbis
    /// install.</para>
    /// <para>These return values are static, read-only data; do not modify or free it.
    /// The pointers remain valid until you call <see cref="CloseAudio"/>.</para>
    /// </summary>
    /// <returns>number of music decoders available.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="GetMusicDecoder"/>
    /// <seealso cref="HasMusicDecoder"/>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_HasChunkDecoder"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumMusicDecoders();
    
    
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_GetMusicDecoder"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr Mix_GetMusicDecoder(int index);
    /// <code>extern SDL_DECLSPEC const char * SDLCALL Mix_GetMusicDecoder(int index);</code>
    /// <summary>
    /// <para>Get a music decoder's name.</para>
    /// <para>The requested decoder's index must be between zero and
    /// <see cref="GetNumMusicDecoders"/>-1. It's safe to call this with an invalid index;
    /// this function will return <c>null</c> in that case.</para>
    /// <para>This list can change between builds AND runs of the program, if external
    /// libraries that add functionality become available. You must successfully
    /// call <see cref="OpenAudio(uint, in SDL.AudioSpec)"/> before calling this function, as decoders are
    /// activated at device open time.</para>
    /// </summary>
    /// <param name="index">index of the music decoder.</param>
    /// <returns>the music decoder's name.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="GetNumMusicDecoders"/>
    public static string GetMusicDecoder(int index)
    {
        var value = Mix_GetMusicDecoder(index); 
        return value == IntPtr.Zero ? "" : Marshal.PtrToStringUTF8(value)!;
    }
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL Mix_HasMusicDecoder(const char *name);</code>
    /// <summary>
    /// <para>Check if a music decoder is available by name.</para>
    /// <para>This result can change between builds AND runs of the program, if external
    /// libraries that add functionality become available. You must successfully
    /// call <see cref="OpenAudio(uint, in SDL.AudioSpec)"/> before calling this function, as decoders are
    /// activated at device open time.</para>
    /// <para>Decoder names are arbitrary but also obvious, so you have to know what
    /// you're looking for ahead of time, but usually it's the file extension in
    /// capital letters (some example names are "MOD", "MP3", "FLAC").</para>
    /// </summary>
    /// <param name="name">the decoder name to query.</param>
    /// <returns><c>true</c> if a decoder by that name is available, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0</since>
    /// <seealso cref="GetNumMusicDecoders"/>
    /// <seealso cref="GetMusicDecoder"/>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_HasMusicDecoder"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasMusicDecoder([MarshalAs(UnmanagedType.LPUTF8Str)] string name);
    
    
    /// <code>extern SDL_DECLSPEC Mix_MusicType SDLCALL Mix_GetMusicType(const Mix_Music *music);</code>
    /// <summary>
    /// <para>Find out the format of a mixer music.</para>
    /// <para>If <c>music</c> is <c>null</c>, this will query the currently playing music (and return
    /// <see cref="MusicType.None"/> if nothing is currently playing).</para>
    /// </summary>
    /// <param name="music">the music object to query, or <c>null</c> for the currently-playing
    /// music.</param>
    /// <returns>the <see cref="MusicType"/> for the music object.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_GetMusicType"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial MusicType GetMusicType(IntPtr music);
    
    
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_GetMusicTitle"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr Mix_GetMusicTitle(IntPtr music);
    /// <code>extern SDL_DECLSPEC const char *SDLCALL Mix_GetMusicTitle(const Mix_Music *music);</code>
    /// <summary>
    /// <para>Get the title for a music object.</para>
    /// <para>This returns format-specific metadata. Not all file formats supply this!</para>
    /// <para>If <c>music</c> is <c>null</c>, this will query the currently-playing music.</para>
    /// <para>Unlike this function, <see cref="GetMusicTitle"/> produce a string with the music's
    /// filename if a title isn't available, which might be preferable for some
    /// applications.</para>
    /// <para>This function never returns <c>null</c>! If no data is available, it will return
    /// an empty string ("").</para>
    /// </summary>
    /// <param name="music">the music object to query, or <c>null</c> for the currently-playing
    /// music.</param>
    /// <returns>the music's title if available, or "".</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="GetMusicTitle"/>
    /// <seealso cref="GetMusicArtistTag"/>
    /// <seealso cref="GetMusicAlbumTag"/>
    /// <seealso cref="GetMusicCopyrightTag"/>
    public static string GetMusicTitle(IntPtr music)
    {
        var value = Mix_GetMusicTitle(music); 
        return value == IntPtr.Zero ? "" : Marshal.PtrToStringUTF8(value)!;
    }
    
    
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_GetMusicTitleTag"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr Mix_GetMusicTitleTag(IntPtr music);
    /// <code>extern SDL_DECLSPEC const char *SDLCALL Mix_GetMusicTitleTag(const Mix_Music *music);</code>
    /// <summary>
    /// <para>Get the title for a music object.</para>
    /// <para>This returns format-specific metadata. Not all file formats supply this!</para>
    /// <para>If <c>music</c> is <c>null</c>, this will query the currently-playing music.</para>
    /// <para>Unlike this function, <see cref="GetMusicTitle"/> produce a string with the music's
    /// filename if a title isn't available, which might be preferable for some
    /// applications.</para>
    /// <para>This function never returns <c>null</c>! If no data is available, it will return
    /// an empty string ("").</para>
    /// </summary>
    /// <param name="music">the music object to query, or <c>null</c> for the currently-playing
    /// music.</param>
    /// <returns>the music's title if available, or "".</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="GetMusicTitle"/>
    /// <seealso cref="GetMusicArtistTag"/>
    /// <seealso cref="GetMusicAlbumTag"/>
    /// <seealso cref="GetMusicCopyrightTag"/>
    public static string GetMusicTitleTag(IntPtr music)
    {
        var value = Mix_GetMusicTitleTag(music); 
        return value == IntPtr.Zero ? "" : Marshal.PtrToStringUTF8(value)!;
    }
    
    
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_GetMusicArtistTag"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr Mix_GetMusicArtistTag(IntPtr music);
    /// <code>extern SDL_DECLSPEC const char *SDLCALL Mix_GetMusicArtistTag(const Mix_Music *music);</code>
    /// <summary>
    /// <para>Get the artist name for a music object.</para>
    /// <para>This returns format-specific metadata. Not all file formats supply this!</para>
    /// <para>If <c>music</c> is <c>null</c>, this will query the currently-playing music.</para>
    /// <para>This function never returns <c>null</c>! If no data is available, it will return
    /// an empty string ("").</para>
    /// </summary>
    /// <param name="music">the music object to query, or <c>null</c> for the currently-playing
    /// music.</param>
    /// <returns>the music's artist name if available, or "".</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="GetMusicTitleTag"/>
    /// <seealso cref="GetMusicAlbumTag"/>
    /// <seealso cref="GetMusicCopyrightTag"/>
    public static string GetMusicArtistTag(IntPtr music)
    {
        var value = Mix_GetMusicArtistTag(music); 
        return value == IntPtr.Zero ? "" : Marshal.PtrToStringUTF8(value)!;
    }
    
    
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_GetMusicAlbumTag"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr Mix_GetMusicAlbumTag(IntPtr music);
    /// <code>extern SDL_DECLSPEC const char *SDLCALL Mix_GetMusicAlbumTag(const Mix_Music *music);</code>
    /// <summary>
    /// <para>Get the album name for a music object.</para>
    /// <para>This returns format-specific metadata. Not all file formats supply this!</para>
    /// <para>If <c>music</c> is <c>null</c>, this will query the currently-playing music.</para>
    /// <para>This function never returns <c>null</c>! If no data is available, it will return
    /// an empty string ("").</para>
    /// </summary>
    /// <param name="music">the music object to query, or <c>null</c> for the currently-playing
    /// music.</param>
    /// <returns>the music's album name if available, or "".</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="GetMusicTitleTag"/>
    /// <seealso cref="GetMusicArtistTag"/>
    /// <seealso cref="GetMusicCopyrightTag"/>
    public static string GetMusicAlbumTag(IntPtr music)
    {
        var value = Mix_GetMusicAlbumTag(music); 
        return value == IntPtr.Zero ? "" : Marshal.PtrToStringUTF8(value)!;
    }
    
    
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_GetMusicCopyrightTag"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr Mix_GetMusicCopyrightTag(IntPtr music);
    /// <code>extern SDL_DECLSPEC const char *SDLCALL Mix_GetMusicCopyrightTag(const Mix_Music *music);</code>
    /// <summary>
    /// <para>Get the copyright text for a music object.</para>
    /// <para>This returns format-specific metadata. Not all file formats supply this!</para>
    /// <para>If <c>music</c> is <c>null</c>, this will query the currently-playing music.</para>
    /// <para>This function never returns <c>null</c>! If no data is available, it will return
    /// an empty string ("").</para>
    /// </summary>
    /// <param name="music">the music object to query, or <c>null</c> for the currently-playing
    /// music.</param>
    /// <returns>the music's copyright text if available, or "".</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="GetMusicTitleTag"/>
    /// <seealso cref="GetMusicArtistTag"/>
    /// <seealso cref="GetMusicAlbumTag"/>
    public static string GetMusicCopyrightTag(IntPtr music)
    {
        var value = Mix_GetMusicCopyrightTag(music); 
        return value == IntPtr.Zero ? "" : Marshal.PtrToStringUTF8(value)!;
    }
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL Mix_SetPostMix(Mix_MixCallback mix_func, void *arg);</code>
    /// <summary>
    /// <para>Set a function that is called after all mixing is performed.</para>
    /// <para>This can be used to provide real-time visual display of the audio stream or
    /// add a custom mixer filter for the stream data.</para>
    /// <para>The callback will fire every time SDL_mixer is ready to supply more data to
    /// the audio device, after it has finished all its mixing work. This runs
    /// inside an SDL audio callback, so it's important that the callback return
    /// quickly, or there could be problems in the audio playback.</para>
    /// <para>The data provided to the callback is in the format that the audio device
    /// was opened in, and it represents the exact waveform SDL_mixer has mixed
    /// from all playing chunks and music for playback. You are allowed to modify
    /// the data, but it cannot be resized (so you can't add a reverb effect that
    /// goes past the end of the buffer without saving some state between runs to
    /// add it into the next callback, or resample the buffer to a smaller size to
    /// speed it up, etc).</para>
    /// <para>The <c>arg</c> pointer supplied here is passed to the callback as-is, for
    /// whatever the callback might want to do with it (keep track of some ongoing
    /// state, settings, etc).</para>
    /// <para>Passing a <c>null</c> callback disables the post-mix callback until such a time as
    /// a new one callback is set.</para>
    /// <para>There is only one callback available. If you need to mix multiple inputs,
    /// be prepared to handle them from a single function.</para>
    /// </summary>
    /// <param name="mixFunc">the callback function to become the new post-mix callback.</param>
    /// <param name="arg">a pointer that is passed, untouched, to the callback.</param>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="HookMusic"/>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_SetPostMix"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetPostMix(MixCallback mixFunc, IntPtr arg);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL Mix_HookMusic(Mix_MixCallback mix_func, void *arg);</code>
    /// <summary>
    /// <para>Add your own music player or additional mixer function.</para>
    /// <para>This works something like <see cref="SetPostMix"/>, but it has some crucial
    /// differences. Note that an app can use this _and_ <see cref="SetPostMix"/> at the
    /// same time. This allows an app to replace the built-in music playback,
    /// either with it's own music decoder or with some sort of
    /// procedurally-generated audio output.</para>
    /// <para>The supplied callback will fire every time SDL_mixer is preparing to supply
    /// more data to the audio device. This runs inside an SDL audio callback, so
    /// it's important that the callback return quickly, or there could be problems
    /// in the audio playback.</para>
    /// <para>Running this callback is the first thing SDL_mixer will do when starting to
    /// mix more audio. The buffer will contain silence upon entry, so the callback
    /// does not need to mix into existing data or initialize the buffer.</para>
    /// <para>Note that while a callback is set through this function, SDL_mixer will not
    /// mix any playing music; this callback is used instead. To disable this
    /// callback (and thus reenable built-in music playback) call this function
    /// with a <c>null</c> callback.</para>
    /// <para>The data written to by the callback is in the format that the audio device
    /// was opened in, and upon return from the callback, SDL_mixer will mix any
    /// playing chunks (but not music!) into the buffer. The callback cannot resize
    /// the buffer (so you must be prepared to provide exactly the amount of data
    /// demanded or leave it as silence).</para>
    /// <para>The <c>arg</c> pointer supplied here is passed to the callback as-is, for
    /// whatever the callback might want to do with it (keep track of some ongoing
    /// state, settings, etc).</para>
    /// <para>As there is only one music "channel" mixed, there is only one callback
    /// available. If you need to mix multiple inputs, be prepared to handle them
    /// from a single function.</para>
    /// </summary>
    /// <param name="mixFunc">the callback function to become the new post-mix callback.</param>
    /// <param name="arg">a pointer that is passed, untouched, to the callback.</param>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="SetPostMix"/>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_HookMusic"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void HookMusic(MixCallback mixFunc, IntPtr arg);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL Mix_HookMusicFinished(Mix_MusicFinishedCallback music_finished);</code>
    /// <summary>
    /// <para>Set a callback that runs when a music object has stopped playing.</para>
    /// <para>This callback will fire when the currently-playing music has completed, or
    /// when it has been explicitly stopped from a call to <see cref="HaltMusic"/>. As such,
    /// this callback might fire from an arbitrary background thread at almost any
    /// time; try to limit what you do here.</para>
    /// <para>It is legal to start a new music object playing in this callback (or
    /// restart the one that just stopped). If the music finished normally, this
    /// can be used to loop the music without a gap in the audio playback.</para>
    /// <para>A <c>null</c> pointer will disable the callback.</para>
    /// </summary>
    /// <param name="musicFinished">the callback function to become the new notification
    /// mechanism.</param>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_HookMusicFinished"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void HookMusicFinished(MusicFinishedCallback musicFinished);
    
    
    /// <code>extern SDL_DECLSPEC void * SDLCALL Mix_GetMusicHookData(void);</code>
    /// <summary>
    /// <para>Get a pointer to the user data for the current music hook.</para>
    /// <para>This returns the <c>arg</c> pointer last passed to <see cref="HookMusic"/>, or <c>null</c> if
    /// that function has never been called.</para>
    /// </summary>
    /// <returns>pointer to the user data previously passed to <see cref="HookMusic"/>.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_GetMusicHookData"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetMusicHookData();
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL Mix_ChannelFinished(Mix_ChannelFinishedCallback channel_finished);</code>
    /// <summary>
    /// <para>Set a callback that runs when a channel has finished playing.</para>
    /// <para>The callback may be called from the mixer's audio callback or it could be
    /// called as a result of <see cref="HaltChannel"/>, etc.</para>
    /// <para>The callback has a single parameter, <c>channel</c>, which says what mixer
    /// channel has just stopped.</para>
    /// <para>A <c>null</c> pointer will disable the callback.</para>
    /// </summary>
    /// <param name="channelFinished">the callback function to become the new
    /// notification mechanism.</param>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_ChannelFinished"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ChannelFinished(ChannelFinishedCallback channelFinished);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL Mix_RegisterEffect(int chan, Mix_EffectFunc_t f, Mix_EffectDone_t d, void *arg);</code>
    /// <summary>
    /// <para>Register a special effect function.</para>
    /// <para>At mixing time, the channel data is copied into a buffer and passed through
    /// each registered effect function. After it passes through all the functions,
    /// it is mixed into the final output stream. The copy to buffer is performed
    /// once, then each effect function performs on the output of the previous
    /// effect. Understand that this extra copy to a buffer is not performed if
    /// there are no effects registered for a given chunk, which saves CPU cycles,
    /// and any given effect will be extra cycles, too, so it is crucial that your
    /// code run fast. Also note that the data that your function is given is in
    /// the format of the sound device, and not the format you gave to
    /// <see cref="OpenAudio(uint, in SDL.AudioSpec)"/>, although they may in reality be the same. This is an
    /// unfortunate but necessary speed concern. Use <see cref="QuerySpec"/> to determine
    /// if you can handle the data before you register your effect, and take
    /// appropriate actions.</para>
    /// <para>You may also specify a callback (<see cref="EffectDoneT"/>) that is called when the
    /// channel finishes playing. This gives you a more fine-grained control than
    /// <see cref="ChannelFinished"/>, in case you need to free effect-specific resources,
    /// etc. If you don't need this, you can specify <c>null</c>.</para>
    /// <para>ou may set the callbacks before or after calling <see cref="PlayChannel"/>.</para>
    /// <para>Things like <see cref="SetPanning"/> are just internal special effect functions, so
    /// if you are using that, you've already incurred the overhead of a copy to a
    /// separate buffer, and that these effects will be in the queue with any
    /// functions you've registered. The list of registered effects for a channel
    /// is reset when a chunk finishes playing, so you need to explicitly set them
    /// with each call to PlayChannel*().</para>
    /// <para>You may also register a special effect function that is to be run after
    /// final mixing occurs. The rules for these callbacks are identical to those
    /// in <see cref="RegisterEffect"/>, but they are run after all the channels and the
    /// music have been mixed into a single stream, whereas channel-specific
    /// effects run on a given channel before any other mixing occurs. These global
    /// effect callbacks are call "posteffects". Posteffects only have their
    /// <see cref="EffectDoneT"/> function called when they are unregistered (since the main
    /// output stream is never "done" in the same sense as a channel). You must
    /// unregister them manually when you've had enough. Your callback will be told
    /// that the channel being mixed is <see cref="ChannelPost"/> if the processing is
    /// considered a posteffect.</para>
    /// <para>After all these effects have finished processing, the callback registered
    /// through <see cref="SetPostMix"/> runs, and then the stream goes to the audio
    /// device.</para>
    /// </summary>
    /// <param name="chan">the channel to register an effect to, or <see cref="ChannelPost"/>.</param>
    /// <param name="f">effect the callback to run when more of this channel is to be
    /// mixed.</param>
    /// <param name="d">effect done callback.</param>
    /// <param name="arg">argument.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_RegisterEffect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RegisterEffect(int chan, EffectFuncT f, EffectDoneT d, IntPtr arg);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL Mix_UnregisterEffect(int channel, Mix_EffectFunc_t f);</code>
    /// <summary>
    /// <para>Explicitly unregister a special effect function.</para>
    /// <para>You may not need to call this at all, unless you need to stop an effect
    /// from processing in the middle of a chunk's playback.</para>
    /// <para>Posteffects are never implicitly unregistered as they are for channels (as
    /// the output stream does not have an end), but they may be explicitly
    /// unregistered through this function by specifying <see cref="ChannelPost"/> for a
    /// channel.</para>
    /// </summary>
    /// <param name="channel">the channel to unregister an effect on, or <see cref="ChannelPost"/>.</param>
    /// <param name="f">effect the callback stop calling in future mixing iterations.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_UnregisterEffect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool UnregisterEffect(int channel, EffectFuncT f);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL Mix_UnregisterAllEffects(int channel);</code>
    /// <summary>
    /// <para>Explicitly unregister all special effect functions.</para>
    /// <para>You may not need to call this at all, unless you need to stop all effects
    /// from processing in the middle of a chunk's playback.</para>
    /// <para>Note that this will also shut off some internal effect processing, since
    /// <see cref="SetPanning"/> and others may use this API under the hood. This is called
    /// internally when a channel completes playback. Posteffects are never
    /// implicitly unregistered as they are for channels, but they may be
    /// explicitly unregistered through this function by specifying
    /// <see cref="ChannelPost"/> for a channel.</para>
    /// </summary>
    /// <param name="channel">the channel to unregister all effects on, or
    /// <see cref="ChannelPost"/>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_UnregisterAllEffects"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool UnregisterAllEffects(int channel);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL Mix_SetPanning(int channel, Uint8 left, Uint8 right);</code>
    /// <summary>
    /// <para>Set the panning of a channel.</para>
    /// <para>The left and right channels are specified as integers between 0 and 255,
    /// quietest to loudest, respectively.</para>
    /// <para>Technically, this is just individual volume control for a sample with two
    /// (stereo) channels, so it can be used for more than just panning. If you
    /// want real panning, call it like this:</para>
    /// <c>SetPanning(channel, left, 255 - left);</c>
    /// <para>Setting <c>channel</c> to <see cref="ChannelPost"/> registers this as a posteffect, and
    /// the panning will be done to the final mixed stream before passing it on to
    /// the audio device.</para>
    /// <para>This uses the <see cref="RegisterEffect"/> API internally, and returns without
    /// registering the effect function if the audio device is not configured for
    /// stereo output. Setting both <c>left</c> and <c>right</c> to 255 causes this effect to
    /// be unregistered, since that is the data's normal state.</para>
    /// <para>Note that an audio device in mono mode is a no-op, but this call will
    /// return successful in that case. Error messages can be retrieved from
    /// <see cref="SDL.GetError"/>.</para>
    /// </summary>
    /// <param name="channel">The mixer channel to pan or <see cref="ChannelPost"/>.</param>
    /// <param name="left">Volume of stereo left channel, 0 is silence, 255 is full
    /// volume.</param>
    /// <param name="right">Volume of stereo right channel, 0 is silence, 255 is full
    /// volume.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="SetPosition"/>
    /// <seealso cref="SetDistance"/>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_SetPanning"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetPanning(int channel, byte left, byte right);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL Mix_SetPosition(int channel, Sint16 angle, Uint8 distance);</code>
    /// <summary>
    /// <para>Set the position of a channel.</para>
    /// <para><c>angle</c> is an integer from 0 to 360, that specifies the location of the
    /// sound in relation to the listener. <c>angle</c> will be reduced as necessary
    /// (540 becomes 180 degrees, -100 becomes 260). Angle 0 is due north, and
    /// rotates clockwise as the value increases. For efficiency, the precision of
    /// this effect may be limited (angles 1 through 7 might all produce the same
    /// effect, 8 through 15 are equal, etc). <c>distance</c> is an integer between 0
    /// and 255 that specifies the space between the sound and the listener. The
    /// larger the number, the further away the sound is. Using 255 does not
    /// guarantee that the channel will be removed from the mixing process or be
    /// completely silent. For efficiency, the precision of this effect may be
    /// limited (distance 0 through 5 might all produce the same effect, 6 through
    /// 10 are equal, etc). Setting <c>angle</c> and <c>distance</c> to 0 unregisters this
    /// effect, since the data would be unchanged.</para>
    /// <para>If you need more precise positional audio, consider using OpenAL for
    /// spatialized effects instead of SDL_mixer. This is only meant to be a basic
    /// effect for simple "3D" games.</para>
    /// <para>If the audio device is configured for mono output, then you won't get any
    /// effectiveness from the angle; however, distance attenuation on the channel
    /// will still occur. While this effect will function with stereo voices, it
    /// makes more sense to use voices with only one channel of sound, so when they
    /// are mixed through this effect, the positioning will sound correct. You can
    /// convert them to mono through SDL before giving them to the mixer in the
    /// first place if you like.</para>
    /// <para>Setting the channel to <see cref="ChannelPost"/> registers this as a posteffect, and
    /// the positioning will be done to the final mixed stream before passing it on
    /// to the audio device.</para>
    /// <para>This is a convenience wrapper over <see cref="SetDistance"/> and <see cref="SetPanning"/>.</para>
    /// </summary>
    /// <param name="channel">The mixer channel to position, or <see cref="ChannelPost"/>.</param>
    /// <param name="angle">angle, in degrees. North is 0, and goes clockwise.</param>
    /// <param name="distance">distance; 0 is the listener, 255 is maxiumum distance away.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_SetPosition"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetPosition(int channel, short angle, byte distance);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL Mix_SetDistance(int channel, Uint8 distance);</code>
    /// <summary>
    /// <para>Set the "distance" of a channel.</para>
    /// <para><c>distance</c> is an integer from 0 to 255 that specifies the location of the
    /// sound in relation to the listener. Distance 0 is overlapping the listener,
    /// and 255 is as far away as possible. A distance of 255 does not guarantee
    /// silence; in such a case, you might want to try changing the chunk's volume,
    /// or just cull the sample from the mixing process with <see cref="HaltChannel"/>. For
    /// efficiency, the precision of this effect may be limited (distances 1
    /// through 7 might all produce the same effect, 8 through 15 are equal, etc).
    /// (distance) is an integer between 0 and 255 that specifies the space between
    /// the sound and the listener. The larger the number, the further away the
    /// sound is. Setting the distance to 0 unregisters this effect, since the data
    /// would be unchanged. If you need more precise positional audio, consider
    /// using OpenAL for spatialized effects instead of SDL_mixer. This is only
    /// meant to be a basic effect for simple "3D" games.</para>
    /// <para>Setting the channel to <see cref="ChannelPost"/> registers this as a posteffect, and
    /// the distance attenuation will be done to the final mixed stream before
    /// passing it on to the audio device.</para>
    /// <para>This uses the <see cref="RegisterEffect"/> API internally.</para>
    /// </summary>
    /// <param name="channel">The mixer channel to attenuate, or <see cref="RegisterEffect"/>.</param>
    /// <param name="distance">distance; 0 is the listener, 255 is maxiumum distance away.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_SetDistance"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetDistance(int channel, byte distance);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL Mix_SetReverseStereo(int channel, int flip);</code>
    /// <summary>
    /// <para>Cause a channel to reverse its stereo.</para>
    /// <para>This is handy if the user has his speakers hooked up backwards, or you
    /// would like to have a trippy sound effect.</para>
    /// <para>Calling this function with <c>flip</c> set to non-zero reverses the chunks's
    /// usual channels. If <c>flip</c> is zero, the effect is unregistered.</para>
    /// <para>This uses the <see cref="RegisterEffect"/> API internally, and thus is probably
    /// more CPU intensive than having the user just plug in his speakers
    /// correctly. <see cref="SetReverseStereo"/> returns without registering the effect
    /// function if the audio device is not configured for stereo output.</para>
    /// <para>If you specify <see cref="ChannelPost"/> for <c>channel</c>, then this effect is used on
    /// the final mixed stream before sending it on to the audio device (a
    /// posteffect).</para>
    /// </summary>
    /// <param name="channel">The mixer channel to reverse, or <see cref="ChannelPost"/>.</param>
    /// <param name="flip">non-zero to reverse stereo, zero to disable this effect.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information. Note that an audio device in mono mode is a no-op,
    /// but this call will return successful in that case.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_SetReverseStereo"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetReverseStereo(int channel, int flip);
    
    
    /// <c>extern SDL_DECLSPEC int SDLCALL Mix_ReserveChannels(int num);</c>
    /// <summary>
    /// <para>Reserve the first channels for the application.</para>
    /// <para>While SDL_mixer will use up to the number of channels allocated by
    /// <see cref="AllocateChannels"/>, this sets channels aside that will not be available
    /// when calling Mix_PlayChannel with a channel of -1 (play on the first unused
    /// channel). In this case, SDL_mixer will treat reserved channels as "used"
    /// whether anything is playing on them at the moment or not.</para>
    /// <para>This is useful if you've budgeted some channels for dedicated audio and the
    /// rest are just used as they are available.</para>
    /// <para>Calling this function will set channels 0 to <c>n - 1</c> to be reserved. This
    /// will not change channel allocations. The number of reserved channels will
    /// be clamped to the current number allocated.</para>
    /// <para>By default, no channels are reserved.</para>
    /// </summary>
    /// <param name="channel">number of channels to reserve, starting at index zero.</param>
    /// <param name="flip">the number of reserved channels.</param>
    /// <returns>This function is available since SDL_mixer 3.0.0.</returns>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_ReserveChannels"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int ReserveChannels(int channel, int flip);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL Mix_GroupChannel(int which, int tag);</code>
    /// <summary>
    /// <para>Assign a tag to a channel.</para>
    /// <para>A tag is an arbitrary number that can be assigned to several mixer
    /// channels, to form groups of channels.</para>
    /// <para>If <c>tag</c> is -1, the tag is removed (actually -1 is the tag used to
    /// represent the group of all the channels).</para>
    /// <para>This function replaces the requested channel's current tag; you may only
    /// have one tag per channel.</para>
    /// <para>You may not specify <see cref="ChannelPost"/> for a channel.</para>
    /// </summary>
    /// <param name="which">the channel to set the tag on.</param>
    /// <param name="tag">an arbitrary value to assign a channel.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_GroupChannel"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GroupChannel(int which, int tag);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL Mix_GroupChannels(int from, int to, int tag);</code>
    /// <summary>
    /// <para>Assign several consecutive channels to the same tag.</para>
    /// <para>A tag is an arbitrary number that can be assigned to several mixer
    /// channels, to form groups of channels.</para>
    /// <para>If <c>tag</c> is -1, the tag is removed (actually -1 is the tag used to
    /// represent the group of all the channels).</para>
    /// <para>This function replaces the requested channels' current tags; you may only
    /// have one tag per channel.</para>
    /// <para>You may not specify <see cref="ChannelPost"/> for a channel.</para>
    /// <para>Note that this returns success and failure in the _opposite_ way from
    /// <see cref="GroupChannel"/>. We regret the API design mistake.</para>
    /// </summary>
    /// <param name="from">the first channel to set the tag on.</param>
    /// <param name="to">the last channel to set the tag on, inclusive.</param>
    /// <param name="tag">an arbitrary value to assign a channel.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_GroupChannels"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GroupChannels(int from, int to, int tag);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL Mix_GroupAvailable(int tag);</code>
    /// <summary>
    /// <para>Finds the first available channel in a group of channels.</para>
    /// <para>A tag is an arbitrary number that can be assigned to several mixer
    /// channels, to form groups of channels.</para>
    /// <para>This function searches all channels with a specified tag, and returns the
    /// channel number of the first one it finds that is currently unused.</para>
    /// <para>If no channels with the specified tag are unused, this function returns -1.</para>
    /// </summary>
    /// <param name="tag">an arbitrary value, assigned to channels, to search for.</param>
    /// <returns>first available channel, or -1 if none are available.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_GroupAvailable"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GroupAvailable(int tag);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL Mix_GroupCount(int tag);</code>
    /// <summary>
    /// <para>Returns the number of channels in a group.</para>
    /// <para>If tag is -1, this will return the total number of channels allocated,
    /// regardless of what their tag might be.</para>
    /// </summary>
    /// <param name="tag">an arbitrary value, assigned to channels, to search for.</param>
    /// <returns>the number of channels assigned the specified tag.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_GroupCount"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GroupCount(int tag);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL Mix_GroupOldest(int tag);</code>
    /// <summary>
    /// <para>Find the "oldest" sample playing in a group of channels.</para>
    /// <para>Specifically, this function returns the channel number that is assigned the
    /// specified tag, is currently playing, and has the lowest start time, based
    /// on the value of <see cref="SDL.GetTicks"/> when the channel started playing.</para>
    /// <para>If no channel with this tag is currently playing, this function returns -1.</para>
    /// </summary>
    /// <param name="tag">an arbitrary value, assigned to channels, to search through.</param>
    /// <returns>the "oldest" sample playing in a group of channels.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="GroupNewer"/>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_GroupOldest"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GroupOldest(int tag);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL Mix_GroupNewer(int tag);</code>
    /// <summary>
    /// <para>Find the "most recent" sample playing in a group of channels.</para>
    /// <para>Specifically, this function returns the channel number that is assigned the
    /// specified tag, is currently playing, and has the highest start time, based
    /// on the value of <see cref="SDL.GetTicks"/> when the channel started playing.</para>
    /// <para>If no channel with this tag is currently playing, this function returns -1.</para>
    /// </summary>
    /// <param name="tag">an arbitrary value, assigned to channels, to search through.</param>
    /// <returns>the "most recent" sample playing in a group of channels.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="GroupOldest"/>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_GroupNewer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GroupNewer(int tag);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL Mix_PlayChannel(int channel, Mix_Chunk *chunk, int loops);</code>
    /// <summary>
    /// <para>Play an audio chunk on a specific channel.</para>
    /// <para>If the specified channel is -1, play on the first free channel (and return
    /// -1 without playing anything new if no free channel was available).</para>
    /// <para>If a specific channel was requested, and there is a chunk already playing
    /// there, that chunk will be halted and the new chunk will take its place.</para>
    /// <para>If <c>loops</c> is greater than zero, loop the sound that many times. If <c>loops</c>
    /// is -1, loop "infinitely" (~65000 times).</para>
    /// <para>Note that before SDL_mixer 3.0.0, this function was a macro that called
    /// <see cref="PlayChannelTimed"/> with a fourth parameter ("ticks") of -1. This
    /// function still does the same thing, but promotes it to a proper API
    /// function. Older binaries linked against a newer SDL_mixer will still call
    /// <see cref="PlayChannelTimed"/> directly, as they are using the macro, which was
    /// available since the dawn of time.</para>
    /// </summary>
    /// <param name="channel">the channel on which to play the new chunk.</param>
    /// <param name="chunk">the new chunk to play.</param>
    /// <param name="loops">the number of times the chunk should loop, -1 to loop (not
    /// actually) infinitely.</param>
    /// <returns>which channel was used to play the sound, or -1 if sound could not
    /// be played.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_PlayChannel"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int PlayChannel(int channel, IntPtr chunk, int loops);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL Mix_PlayChannelTimed(int channel, Mix_Chunk *chunk, int loops, int ticks);</code>
    /// <summary>
    /// <para>Play an audio chunk on a specific channel for a maximum time.</para>
    /// <para>If the specified channel is -1, play on the first free channel (and return
    /// -1 without playing anything new if no free channel was available).</para>
    /// <para>If a specific channel was requested, and there is a chunk already playing
    /// there, that chunk will be halted and the new chunk will take its place.</para>
    /// <para>If <c>loops</c> is greater than zero, loop the sound that many times. If `loops`
    /// is -1, loop "infinitely" (~65000 times).</para>
    /// <para><c>ticks</c> specifies the maximum number of milliseconds to play this chunk
    /// before halting it. If you want the chunk to play until all data has been
    /// mixed, specify -1.</para>
    /// <para>Note that this function does not block for the number of ticks requested;
    /// it just schedules the chunk to play and notes the maximum for the mixer to
    /// manage later, and returns immediately.</para>
    /// </summary>
    /// <param name="channel">the channel on which to play the new chunk.</param>
    /// <param name="chunk">the new chunk to play.</param>
    /// <param name="loops">the number of times the chunk should loop, -1 to loop (not
    /// actually) infinitely.</param>
    /// <param name="ticks">the maximum number of milliseconds of this chunk to mix for
    /// playback.</param>
    /// <returns>which channel was used to play the sound, or -1 if sound could not
    /// be played.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_PlayChannelTimed"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int PlayChannelTimed(int channel, IntPtr chunk, int loops, int ticks);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL Mix_PlayMusic(Mix_Music *music, int loops);</code>
    /// <summary>
    /// <para>Play a new music object.</para>
    /// <para>This will schedule the music object to begin mixing for playback.</para>
    /// <para>There is only ever one music object playing at a time; if this is called
    /// when another music object is playing, the currently-playing music is halted
    /// and the new music will replace it.</para>
    /// <para>Please note that if the currently-playing music is in the process of fading
    /// out (via <see cref="FadeOutMusic"/>), this function will *block* until the fade
    /// completes. If you need to avoid this, be sure to call <see cref="HaltMusic"/>
    /// before starting new music.</para>
    /// </summary>
    /// <param name="music">the new music object to schedule for mixing.</param>
    /// <param name="loops">the number of loops to play the music for (0 means "play once
    /// and stop").</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_PlayMusic"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool PlayMusic(IntPtr music, int loops);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL Mix_FadeInMusic(Mix_Music *music, int loops, int ms);</code>
    /// <summary>
    /// <para>Play a new music object, fading in the audio.</para>
    /// <para>This will start the new music playing, much like <see cref="PlayMusic"/> will, but
    /// will start the music playing at silence and fade in to its normal volume
    /// over the specified number of milliseconds.</para>
    /// <para>If there is already music playing, that music will be halted and the new
    /// music object will take its place.</para>
    /// <para>If <c>loops</c> is greater than zero, loop the music that many times. If <c>loops</c>
    /// is -1, loop "infinitely" (~65000 times).</para>
    /// <para>Fading music will change it's volume progressively, as if <see cref="VolumeMusic"/>
    /// was called on it (which is to say: you probably shouldn't call
    /// <see cref="VolumeMusic"/> on fading music).</para>
    /// </summary>
    /// <param name="music">the new music object to play.</param>
    /// <param name="loops">the number of times the chunk should loop, -1 to loop (not
    /// actually) infinitely.</param>
    /// <param name="ms">the number of milliseconds to spend fading in.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_FadeInMusic"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool FadeInMusic(IntPtr music, int loops, int ms);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL Mix_FadeInMusicPos(Mix_Music *music, int loops, int ms, double position);</code>
    /// <summary>
    /// <para>Play a new music object, fading in the audio, from a starting position.</para>
    /// <para>This will start the new music playing, much like <see cref="PlayMusic"/> will, but
    /// will start the music playing at silence and fade in to its normal volume
    /// over the specified number of milliseconds.</para>
    /// <para>If there is already music playing, that music will be halted and the new
    /// music object will take its place.</para>
    /// <para>If <c>loops</c> is greater than zero, loop the music that many times. If <c>loops</c>
    /// is -1, loop "infinitely" (~65000 times).</para>
    /// <para>Fading music will change it's volume progressively, as if <see cref="VolumeMusic"/>
    /// was called on it (which is to say: you probably shouldn't call
    /// <see cref="VolumeMusic"/> on fading music).</para>
    /// <para>This function allows the caller to start the music playback past the
    /// beginning of its audio data. You may specify a start position, in seconds,
    /// and the playback and fade-in will start there instead of with the first
    /// samples of the music.</para>
    /// <para>An app can specify a <c>position</c> of 0.0 to start at the beginning of the
    /// music (or just call <see cref="FadeInMusic"/> instead).</para>
    /// <para>To convert from milliseconds, divide by 1000.0.</para>
    /// </summary>
    /// <param name="music">the new music object to play.</param>
    /// <param name="loops">the number of times the chunk should loop, -1 to loop (not
    /// actually) infinitely.</param>
    /// <param name="ms">the number of milliseconds to spend fading in.</param>
    /// <param name="position">the start position within the music, in seconds, where
    /// playback should start.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_FadeInMusicPos"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool FadeInMusicPos(IntPtr music, int loops, int ms, double position);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL Mix_FadeInChannel(int channel, Mix_Chunk *chunk, int loops, int ms);</code>
    /// <summary>
    /// <para>Play an audio chunk on a specific channel, fading in the audio.</para>
    /// <para>This will start the new sound playing, much like <see cref="PlayChannel"/> will,
    /// but will start the sound playing at silence and fade in to its normal
    /// volume over the specified number of milliseconds.</para>
    /// <para>If the specified channel is -1, play on the first free channel (and return
    /// -1 without playing anything new if no free channel was available).</para>
    /// <para>If a specific channel was requested, and there is a chunk already playing
    /// there, that chunk will be halted and the new chunk will take its place.</para>
    /// <para>If <c>loops</c> is greater than zero, loop the sound that many times. If <c>loops</c>
    /// is -1, loop "infinitely" (~65000 times).</para>
    /// <para>A fading channel will change it's volume progressively, as if <see cref="Volume"/>
    /// was called on it (which is to say: you probably shouldn't call <see cref="Volume"/>
    /// on a fading channel).</para>
    /// <para>Note that before SDL_mixer 3.0.0, this function was a macro that called
    /// <see cref="FadeInChannelTimed"/> with a fourth parameter ("ticks") of -1. This
    /// function still does the same thing, but promotes it to a proper API
    /// function. Older binaries linked against a newer SDL_mixer will still call
    /// <see cref="FadeInChannelTimed"/> directly, as they are using the macro, which was
    /// available since the dawn of time.</para>
    /// </summary>
    /// <param name="channel">the channel on which to play the new chunk, or -1 to find
    /// any available.</param>
    /// <param name="chunk">the new chunk to play.</param>
    /// <param name="loops">the number of times the chunk should loop, -1 to loop (not
    /// actually) infinitely.</param>
    /// <param name="ms">the number of milliseconds to spend fading in.</param>
    /// <returns>which channel was used to play the sound, or -1 if sound could not
    /// be played.</returns>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_FadeInChannel"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int FadeInChannel(int channel, IntPtr chunk, int loops, int ms);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL Mix_FadeInChannelTimed(int channel, Mix_Chunk *chunk, int loops, int ms, int ticks);</code>
    /// <summary>
    /// <para>Play an audio chunk on a specific channel, fading in the audio, for a
    /// maximum time.</para>
    /// <para>This will start the new sound playing, much like <see cref="PlayChannel"/> will,
    /// but will start the sound playing at silence and fade in to its normal
    /// volume over the specified number of milliseconds.</para>
    /// <para>If the specified channel is -1, play on the first free channel (and return
    /// -1 without playing anything new if no free channel was available).</para>
    /// <para>If a specific channel was requested, and there is a chunk already playing
    /// there, that chunk will be halted and the new chunk will take its place.</para>
    /// <para>If <c>loops</c> is greater than zero, loop the sound that many times. If <c>loops</c>
    /// is -1, loop "infinitely" (~65000 times).</para>
    /// <para><c>ticks</c> specifies the maximum number of milliseconds to play this chunk
    /// before halting it. If you want the chunk to play until all data has been
    /// mixed, specify -1.</para>
    /// <para>Note that this function does not block for the number of ticks requested;
    /// it just schedules the chunk to play and notes the maximum for the mixer to
    /// manage later, and returns immediately.</para>
    /// <para>A fading channel will change it's volume progressively, as if <see cref="Volume"/>
    /// was called on it (which is to say: you probably shouldn't call <see cref="Volume"/>
    /// on a fading channel).</para>
    /// </summary>
    /// <param name="channel">the channel on which to play the new chunk, or -1 to find
    /// any available.</param>
    /// <param name="chunk">the new chunk to play.</param>
    /// <param name="loops">the number of times the chunk should loop, -1 to loop (not
    /// actually) infinitely.</param>
    /// <param name="ms">the number of milliseconds to spend fading in.</param>
    /// <param name="ticks">the maximum number of milliseconds of this chunk to mix for
    /// playback.</param>
    /// <returns>which channel was used to play the sound, or -1 if sound could not
    /// be played.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_FadeInChannelTimed"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int FadeInChannelTimed(int channel, IntPtr chunk, int loops, int ms, int ticks);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL Mix_Volume(int channel, int volume);</code>
    /// <summary>
    /// <para>Set the volume for a specific channel.</para>
    /// <para>The volume must be between 0 (silence) and <see cref="MaxVolume"/> (full volume).
    /// Note that <see cref="MaxVolume"/> is 128. Values greater than <see cref="MaxVolume"/> are
    /// clamped to <see cref="MaxVolume"/>.</para>
    /// <para>Specifying a negative volume will not change the current volume; as such,
    /// this can be used to query the current volume without making changes, as
    /// this function returns the previous (in this case, still-current) value.</para>
    /// <para>If the specified channel is -1, this function sets the volume for all
    /// channels, and returns _the average_ of all channels' volumes prior to this
    /// call.</para>
    /// <para>The default volume for a channel is <see cref="MaxVolume"/> (no attenuation).</para>
    /// </summary>
    /// <param name="channel">the channel on set/query the volume on, or -1 for all
    /// channels.</param>
    /// <param name="volume">the new volume, between 0 and <see cref="MaxVolume"/>, or -1 to query.</param>
    /// <returns>the previous volume. If the specified volume is -1, this returns
    /// the current volume. If `channel` is -1, this returns the average
    /// of all channels.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_Volume"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Volume(int channel, int volume);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL Mix_VolumeChunk(Mix_Chunk *chunk, int volume);</code>
    /// <summary>
    /// <para>Set the volume for a specific chunk.</para>
    /// <para>In addition to channels having a volume setting, individual chunks also
    /// maintain a separate volume. Both values are considered when mixing, so both
    /// affect the final attenuation of the sound. This lets an app adjust the
    /// volume for all instances of a sound in addition to specific instances of
    /// that sound.</para>
    /// <para>The volume must be between 0 (silence) and <see cref="MaxVolume"/> (full volume).
    /// Note that <see cref="MaxVolume"/> is 128. Values greater than <see cref="MaxVolume"/> are
    /// clamped to <see cref="MaxVolume"/>.</para>
    /// <para>Specifying a negative volume will not change the current volume; as such,
    /// this can be used to query the current volume without making changes, as
    /// this function returns the previous (in this case, still-current) value.</para>
    /// <para>The default volume for a chunk is <see cref="MaxVolume"/> (no attenuation).</para>
    /// </summary>
    /// <param name="chunk">the chunk whose volume to adjust.</param>
    /// <param name="volume">the new volume, between 0 and <see cref="MaxVolume"/>, or -1 to query.</param>
    /// <returns>the previous volume. If the specified volume is -1, this returns
    /// the current volume. If <c>chunk</c> is <c>null</c>, this returns -1.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_VolumeChunk"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int VolumeChunk(IntPtr chunk, int volume);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL Mix_VolumeMusic(int volume);</code>
    /// <summary>
    /// <para>Set the volume for the music channel.</para>
    /// <para>The volume must be between 0 (silence) and <see cref="MaxVolume"/> (full volume).
    /// Note that <see cref="MaxVolume"/> is 128. Values greater than <see cref="MaxVolume"/> are
    /// clamped to <see cref="MaxVolume"/>.</para>
    /// <para>Specifying a negative volume will not change the current volume; as such,
    /// this can be used to query the current volume without making changes, as
    /// this function returns the previous (in this case, still-current) value.</para>
    /// <para>The default volume for music is <see cref="MaxVolume"/> (no attenuation).</para>
    /// </summary>
    /// <param name="volume">the new volume, between 0 and <see cref="MaxVolume"/>, or -1 to query.</param>
    /// <returns>the previous volume. If the specified volume is -1, this returns
    /// the current volume.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_VolumeMusic"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int VolumeMusic(int volume);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL Mix_GetMusicVolume(Mix_Music *music);</code>
    /// <summary>
    /// Query the current volume value for a music object.
    /// </summary>
    /// <param name="music">the music object to query.</param>
    /// <returns>the music's current volume, between 0 and <see cref="MaxVolume"/> (128).</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_GetMusicVolume"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetMusicVolume(IntPtr music);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL Mix_MasterVolume(int volume);</code>
    /// <summary>
    /// <para>Set the master volume for all channels.</para>
    /// <para>SDL_mixer keeps a per-channel volume, a per-chunk volume, and a master
    /// volume, and considers all three when mixing audio. This function sets the
    /// master volume, which is applied to all playing channels when mixing.</para>
    /// <para>The volume must be between 0 (silence) and <see cref="MaxVolume"/> (full volume).
    /// Note that <see cref="MaxVolume"/> is 128. Values greater than <see cref="MaxVolume"/> are
    /// clamped to <see cref="MaxVolume"/>.</para>
    /// <para>Specifying a negative volume will not change the current volume; as such,
    /// this can be used to query the current volume without making changes, as
    /// this function returns the previous (in this case, still-current) value.</para>
    /// <para>Note that the master volume does not affect any playing music; it is only
    /// applied when mixing chunks. Use <see cref="VolumeMusic"/> for that.</para>
    /// </summary>
    /// <param name="volume">the new volume, between 0 and <see cref="MaxVolume"/>, or -1 to query.</param>
    /// <returns>the previous volume. If the specified volume is -1, this returns
    /// the current volume.</returns>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_MasterVolume"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int MasterVolume(int volume);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL Mix_HaltChannel(int channel);</code>
    /// <summary>
    /// <para>Halt playing of a particular channel.</para>
    /// <para>This will stop further playback on that channel until a new chunk is
    /// started there.</para>
    /// <para>Specifying a channel of -1 will halt _all_ channels, except for any playing
    /// music.</para>
    /// <para>Any halted channels will have any currently-registered effects
    /// deregistered, and will call any callback specified by <see cref="ChannelFinished"/>
    /// before this function returns.</para>
    /// <para>You may not specify <see cref="ChannelPost"/> for a channel.</para>
    /// </summary>
    /// <param name="channel">channel to halt, or -1 to halt all channels.</param>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_HaltChannel"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void HaltChannel(int channel);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL Mix_HaltGroup(int tag);</code>
    /// <summary>
    /// <para>Halt playing of a group of channels by arbitrary tag.</para>
    /// <para>This will stop further playback on all channels with a specific tag, until
    /// a new chunk is started there.</para>
    /// <para>A tag is an arbitrary number that can be assigned to several mixer
    /// channels, to form groups of channels.</para>
    /// <para>The default tag for a channel is -1.</para>
    /// <para>Any halted channels will have any currently-registered effects
    /// deregistered, and will call any callback specified by <see cref="ChannelFinished"/>
    /// before this function returns.</para>
    /// </summary>
    /// <param name="tag">an arbitrary value, assigned to channels, to search for.</param>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_HaltGroup"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void HaltGroup(int tag);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL Mix_HaltMusic(void);</code>
    /// <summary>
    /// <para>Halt playing of the music stream.</para>
    /// <para>This will stop further playback of music until a new music object is
    /// started there.</para>
    /// <para>Any halted music will call any callback specified by
    /// <see cref="HookMusicFinished"/> before this function returns.</para>
    /// </summary>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_HaltMusic"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void HaltMusic();
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL Mix_ExpireChannel(int channel, int ticks);</code>
    /// <summary>
    /// <para>Change the expiration delay for a particular channel.</para>
    /// <para>The channel will halt after the <c>ticks</c> milliseconds have elapsed, or
    /// remove the expiration if <c>ticks</c> is -1.</para>
    /// <para>This overrides the value passed to the fourth parameter of
    /// <see cref="PlayChannelTimed"/>.</para>
    /// <para>Specifying a channel of -1 will set an expiration for _all_ channels.</para>
    /// <para>Any halted channels will have any currently-registered effects
    /// deregistered, and will call any callback specified by <see cref="ChannelFinished"/>
    /// once the halt occurs.</para>
    /// <para>Note that this function does not block for the number of ticks requested;
    /// it just schedules the chunk to expire and notes the time for the mixer to
    /// manage later, and returns immediately.</para>
    /// </summary>
    /// <param name="channel">the channel to change the expiration time on.</param>
    /// <param name="ticks">number of milliseconds from now to let channel play before
    /// halting, -1 to not halt.</param>
    /// <returns>the number of channels that changed expirations.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_HaltMusic"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int ExpireChannel(int channel, int ticks);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL Mix_FadeOutChannel(int which, int ms);</code>
    /// <summary>
    /// <para>Halt a channel after fading it out for a specified time.</para>
    /// <para>This will begin a channel fading from its current volume to silence over
    /// <c>ms</c> milliseconds. After that time, the channel is halted.</para>
    /// <para>Any halted channels will have any currently-registered effects
    /// deregistered, and will call any callback specified by <see cref="ChannelFinished"/>
    /// once the halt occurs.</para>
    /// <para>A fading channel will change it's volume progressively, as if <see cref="Volume"/>
    /// was called on it (which is to say: you probably shouldn't call <see cref="Volume"/>
    /// on a fading channel).</para>
    /// <para>Note that this function does not block for the number of milliseconds
    /// requested; it just schedules the chunk to fade and notes the time for the
    /// mixer to manage later, and returns immediately.</para>
    /// </summary>
    /// <param name="which">the channel to fade out.</param>
    /// <param name="ms">number of milliseconds to fade before halting the channel.</param>
    /// <returns>the number of channels scheduled to fade.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_FadeOutChannel"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int FadeOutChannel(int which, int ms);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL Mix_FadeOutGroup(int tag, int ms);</code>
    /// <summary>
    /// <para>Halt a playing group of channels by arbitrary tag, after fading them out
    /// for a specified time.</para>
    /// <para>This will begin fading a group of channels with a specific tag from their
    /// current volumes to silence over <c>ms</c> milliseconds. After that time, those
    /// channels are halted.</para>
    /// <para>A tag is an arbitrary number that can be assigned to several mixer
    /// channels, to form groups of channels.</para>
    /// <para>The default tag for a channel is -1.</para>
    /// <para>Any halted channels will have any currently-registered effects
    /// deregistered, and will call any callback specified by <see cref="ChannelFinished"/>
    /// once the halt occurs.</para>
    /// <para>A fading channel will change it's volume progressively, as if <see cref="Volume"/>
    /// was called on it (which is to say: you probably shouldn't call <see cref="Volume"/>
    /// on a fading channel).</para>
    /// <para>Note that this function does not block for the number of milliseconds
    /// requested; it just schedules the group to fade and notes the time for the
    /// mixer to manage later, and returns immediately.</para>
    /// </summary>
    /// <param name="tag">an arbitrary value, assigned to channels, to search for.</param>
    /// <param name="ms">number of milliseconds to fade before halting the group.</param>
    /// <returns>the number of channels that were scheduled for fading.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_FadeOutGroup"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int FadeOutGroup(int tag, int ms);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL Mix_FadeOutMusic(int ms);</code>
    /// <summary>
    /// <para>Halt the music stream after fading it out for a specified time.</para>
    /// <para>This will begin the music fading from its current volume to silence over
    /// <c>ms</c> milliseconds. After that time, the music is halted.</para>
    /// <para>Any halted music will call any callback specified by
    /// <see cref="HookMusicFinished"/> once the halt occurs.</para>
    /// <para>Fading music will change it's volume progressively, as if <see cref="VolumeMusic"/>
    /// was called on it (which is to say: you probably shouldn't call
    /// <see cref="VolumeMusic"/> on a fading channel).</para>
    /// <para>Note that this function does not block for the number of milliseconds
    /// requested; it just schedules the music to fade and notes the time for the
    /// mixer to manage later, and returns immediately.</para>
    /// </summary>
    /// <param name="ms">number of milliseconds to fade before halting the channel.</param>
    /// <returns><c>true</c> if music was scheduled to fade, <c>false</c> otherwise. If no music
    /// is currently playing, this returns false.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_FadeOutMusic"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool FadeOutMusic(int ms);
    
    
    /// <code>extern SDL_DECLSPEC Mix_Fading SDLCALL Mix_FadingMusic(void);</code>
    /// <summary>
    /// <para>Query the fading status of the music stream.</para>
    /// <para>This reports one of three values:</para>
    /// <list type="bullet">
    /// <item><see cref="Fading.NoFading"/></item>
    /// <item><see cref="Fading.FadingOut"/></item>
    /// <item><see cref="Fading.FadingIn"/></item>
    /// </list>
    /// <para>If music is not currently playing, this returns <see cref="Fading.NoFading"/>.</para>
    /// </summary>
    /// <returns>the current fading status of the music stream.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_FadingMusic"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Fading FadingMusic();
    
    
    /// <code>extern SDL_DECLSPEC Mix_Fading SDLCALL Mix_FadingChannel(int which);</code>
    /// <summary>
    /// <para>Query the fading status of a channel.</para>
    /// <para>This reports one of three values:</para>
    /// <list type="bullet">
    /// <item><see cref="Fading.NoFading"/></item>
    /// <item><see cref="Fading.FadingOut"/></item>
    /// <item><see cref="Fading.FadingIn"/></item>
    /// </list>
    /// <para>If nothing is currently playing on the channel, or an invalid channel is
    /// specified, this returns <see cref="Fading.NoFading"/>.</para>
    /// <para>You may not specify <see cref="ChannelPost"/> for a channel.</para>
    /// <para>You may not specify -1 for all channels; only individual channels may be
    /// queried.</para>
    /// </summary>
    /// <param name="which">the channel to query.</param>
    /// <returns>the current fading status of the channel.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_FadingChannel"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Fading FadingChannel(int which);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL Mix_Pause(int channel);</code>
    /// <summary>
    /// <para>Pause a particular channel.</para>
    /// <para>Pausing a channel will prevent further playback of the assigned chunk but
    /// will maintain the chunk's current mixing position. When resumed, this
    /// channel will continue to mix the chunk where it left off.</para>
    /// <para>A paused channel can be resumed by calling <see cref="Resume"/>.</para>
    /// <para>A paused channel with an expiration will not expire while paused (the
    /// expiration countdown will be adjusted once resumed).</para>
    /// <para>It is legal to halt a paused channel. Playing a new chunk on a paused
    /// channel will replace the current chunk and unpause the channel.</para>
    /// <para>Specifying a channel of -1 will pause _all_ channels. Any music is
    /// unaffected.</para>
    /// <para>You may not specify <see cref="ChannelPost"/> for a channel.</para>
    /// </summary>
    /// <param name="channel">the channel to pause, or -1 to pause all channels.</param>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_Pause"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Pause(int channel);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL Mix_PauseGroup(int tag);</code>
    /// <summary>
    /// <para>Pause playing of a group of channels by arbitrary tag.</para>
    /// <para>Pausing a channel will prevent further playback of the assigned chunk but
    /// will maintain the chunk's current mixing position. When resumed, this
    /// channel will continue to mix the chunk where it left off.</para>
    /// <para>A paused channel can be resumed by calling <see cref="Resume"/> or
    /// <see cref="ResumeGroup"/>.</para>
    /// <para>A paused channel with an expiration will not expire while paused (the
    /// expiration countdown will be adjusted once resumed).</para>
    /// <para>A tag is an arbitrary number that can be assigned to several mixer
    /// channels, to form groups of channels.</para>
    /// <para>The default tag for a channel is -1.</para>
    /// </summary>
    /// <param name="tag">an arbitrary value, assigned to channels, to search for.</param>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_PauseGroup"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void PauseGroup(int tag);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL Mix_Resume(int channel);</code>
    /// <summary>
    /// <para>Resume a particular channel.</para>
    /// <para>It is legal to resume an unpaused or invalid channel; it causes no effect
    /// and reports no error.</para>
    /// <para>If the paused channel has an expiration, its expiration countdown resumes
    /// now, as well.</para>
    /// <para>Specifying a channel of -1 will resume _all_ paused channels. Any music is
    /// unaffected.</para>
    /// </summary>
    /// <param name="channel">the channel to resume, or -1 to resume all paused channels.</param>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_Resume"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Resume(int channel);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL Mix_ResumeGroup(int tag);</code>
    /// <summary>
    /// <para>Resume playing of a group of channels by arbitrary tag.</para>
    /// <para>It is legal to resume an unpaused or invalid channel; it causes no effect
    /// and reports no error.</para>
    /// <para>If the paused channel has an expiration, its expiration countdown resumes
    /// now, as well.</para>
    /// <para>A tag is an arbitrary number that can be assigned to several mixer
    /// channels, to form groups of channels.</para>
    /// <para>The default tag for a channel is -1.</para>
    /// </summary>
    /// <param name="tag">an arbitrary value, assigned to channels, to search for.</param>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_ResumeGroup"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResumeGroup(int tag);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL Mix_Paused(int channel);</code>
    /// <summary>
    /// <para>Query whether a particular channel is paused.</para>
    /// <para>If an invalid channel is specified, this function returns zero.</para>
    /// </summary>
    /// <param name="channel">the channel to query, or -1 to query all channels.</param>
    /// <returns>1 if channel paused, 0 otherwise. If <c>channel</c> is -1, returns the
    /// number of paused channels.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_Paused"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Paused(int channel);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL Mix_PauseMusic(void);</code>
    /// <summary>
    /// <para>Pause the music stream.</para>
    /// <para>Pausing the music stream will prevent further playback of the assigned
    /// music object, but will maintain the object's current mixing position. When
    /// resumed, this channel will continue to mix the music where it left off.</para>
    /// <para>Paused music can be resumed by calling <see cref="ResumeMusic"/>.</para>
    /// <para>It is legal to halt paused music. Playing a new music object when music is
    /// paused will replace the current music and unpause the music stream.</para>
    /// </summary>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_PauseMusic"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void PauseMusic();
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL Mix_ResumeMusic(void);</code>
    /// <summary>
    /// <para>Resume the music stream.</para>
    /// <para>It is legal to resume an unpaused music stream; it causes no effect and
    /// reports no error.</para>
    /// </summary>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_ResumeMusic"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResumeMusic();
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL Mix_RewindMusic(void);</code>
    /// <summary>
    /// <para>Rewind the music stream.</para>
    /// <para>This causes the currently-playing music to start mixing from the beginning
    /// of the music, as if it were just started.</para>
    /// <para>It's a legal no-op to rewind the music stream when not playing.</para>
    /// </summary>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_RewindMusic"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RewindMusic();
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL Mix_PausedMusic(void);</code>
    /// <summary>
    /// Query whether the music stream is paused.
    /// </summary>
    /// <returns><c>true</c> if music is paused, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="PauseMusic"/>
    /// <seealso cref="ResumeMusic"/>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_PausedMusic"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool PausedMusic();
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL Mix_ModMusicJumpToOrder(int order);</code>
    /// <summary>
    /// <para>Jump to a given order in mod music.</para>
    /// <para>This only applies to MOD music formats.</para>
    /// </summary>
    /// <param name="order">order.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_ModMusicJumpToOrder"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ModMusicJumpToOrder(int order);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL Mix_StartTrack(Mix_Music *music, int track);</code>
    /// <summary>
    /// <para>Start a track in music object.</para>
    /// <para>This only applies to GME music formats.</para>
    /// </summary>
    /// <param name="music">the music object.</param>
    /// <param name="track">the track number to play. 0 is the first track.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_StartTrack"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool StartTrack(IntPtr music, int track);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL Mix_GetNumTracks(Mix_Music *music);</code>
    /// <summary>
    /// <para>Get number of tracks in music object.</para>
    /// <para>This only applies to GME music formats.</para>
    /// </summary>
    /// <param name="music">the music object.</param>
    /// <returns>number of tracks if successful, or -1 if failed or isn't
    /// implemented.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_GetNumTracks"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumTracks(IntPtr music);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL Mix_SetMusicPosition(double position);</code>
    /// <summary>
    /// <para>Set the current position in the music stream, in seconds.</para>
    /// <para>To convert from milliseconds, divide by 1000.0.</para>
    /// <para>This function is only implemented for MOD music formats (set pattern order
    /// number) and for WAV, OGG, FLAC, MP3, and MOD music at the moment.</para>
    /// </summary>
    /// <param name="position">the new position, in seconds (as a double).</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_SetMusicPosition"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetMusicPosition(double position);
    
    
    /// <code>extern SDL_DECLSPEC double SDLCALL Mix_GetMusicPosition(Mix_Music *music);</code>
    /// <summary>
    /// <para>Get the time current position of music stream, in seconds.</para>
    /// <para>To convert to milliseconds, multiply by 1000.0.</para>
    /// </summary>
    /// <param name="music">the music object to query.</param>
    /// <returns>-1.0 if this feature is not supported for some codec.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_GetMusicPosition"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetMusicPosition(IntPtr music);
    
    
    /// <code>extern SDL_DECLSPEC double SDLCALL Mix_MusicDuration(Mix_Music *music);</code>
    /// <summary>
    /// <para>Get a music object's duration, in seconds.</para>
    /// <para>To convert to milliseconds, multiply by 1000.0.</para>
    /// <para>If <c>null</c> is passed, returns duration of current playing music.</para>
    /// </summary>
    /// <param name="music">the music object to query.</param>
    /// <returns>music duration in seconds, or -1.0 on error.</returns>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_MusicDuration"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double MusicDuration(IntPtr music);
    
    
    /// <code>extern SDL_DECLSPEC double SDLCALL Mix_GetMusicLoopStartTime(Mix_Music *music);</code>
    /// <summary>
    /// <para>Get the loop start time position of music stream, in seconds.</para>
    /// <para>To convert to milliseconds, multiply by 1000.0.</para>
    /// <para>If <c>null</c> is passed, returns duration of current playing music.</para>
    /// </summary>
    /// <param name="music">the music object to query.</param>
    /// <returns>-1.0 if this feature is not used for this music or not supported
    /// for some codec.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_GetMusicLoopStartTime"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetMusicLoopStartTime(IntPtr music);
    
    
    /// <code>extern SDL_DECLSPEC double SDLCALL Mix_GetMusicLoopEndTime(Mix_Music *music);</code>
    /// <summary>
    /// <para>Get the loop end time position of music stream, in seconds.</para>
    /// <para>To convert to milliseconds, multiply by 1000.0.</para>
    /// <para>If <c>null</c> is passed, returns duration of current playing music.</para>
    /// </summary>
    /// <param name="music">the music object to query.</param>
    /// <returns>-1.0 if this feature is not used for this music or not supported
    /// for some codec.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_GetMusicLoopEndTime"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetMusicLoopEndTime(IntPtr music);
    
    
    /// <code>extern SDL_DECLSPEC double SDLCALL Mix_GetMusicLoopLengthTime(Mix_Music *music);</code>
    /// <summary>
    /// <para>Get the loop time length of music stream, in seconds.</para>
    /// <para>To convert to milliseconds, multiply by 1000.0.</para>
    /// <para>If <c>null</c> is passed, returns duration of current playing music.</para>
    /// </summary>
    /// <param name="music">the music object to query.</param>
    /// <returns>-1.0 if this feature is not used for this music or not supported
    /// for some codec.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_GetMusicLoopLengthTime"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetMusicLoopLengthTime(IntPtr music);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL Mix_Playing(int channel);</code>
    /// <summary>
    /// <para>Check the playing status of a specific channel.</para>
    /// <para>If the channel is currently playing, this function returns 1. Otherwise it
    /// returns 0.</para>
    /// <para>If the specified channel is -1, all channels are checked, and this function
    /// returns the number of channels currently playing.</para>
    /// <para>You may not specify <see cref="ChannelPost"/> for a channel.</para>
    /// <para>Paused channels are treated as playing, even though they are not currently
    /// making forward progress in mixing.</para>
    /// </summary>
    /// <param name="channel">channel.</param>
    /// <returns>non-zero if channel is playing, zero otherwise. If <c>channel</c> is
    /// -1, return the total number of channel playings.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_Playing"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Playing(int channel);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL Mix_PlayingMusic(void);</code>
    /// <summary>
    /// <para>Check the playing status of the music stream.</para>
    /// <para>If music is currently playing, this function returns 1. Otherwise it
    /// returns 0.</para>
    /// <para>Paused music is treated as playing, even though it is not currently making
    /// forward progress in mixing.</para>
    /// </summary>
    /// <returns><c>true</c> if music is playing, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_PlayingMusic"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool PlayingMusic();
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL Mix_SetSoundFonts(const char *paths);</code>
    /// <summary>
    /// <para>Set SoundFonts paths to use by supported MIDI backends.</para>
    /// <para>You may specify multiple paths in a single string by separating them with
    /// semicolons; they will be searched in the order listed.</para>
    /// <para>This function replaces any previously-specified paths.</para>
    /// <para>Passing a <c>null</c> path will remove any previously-specified paths.</para>
    /// <para>Note that unlike most SDL and SDL_mixer functions, this function returns
    /// zero if there's an error, not on success. We apologize for the API design
    /// inconsistency here.</para>
    /// </summary>
    /// <param name="paths">Paths on the filesystem where SoundFonts are available,
    /// separated by semicolons.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_SetSoundFonts"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetSoundFonts([MarshalAs(UnmanagedType.LPUTF8Str)] string paths);
    
    
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_GetSoundFonts"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr Mix_GetSoundFonts();
    /// <code>extern SDL_DECLSPEC const char* SDLCALL Mix_GetSoundFonts(void);</code>
    /// <summary>
    /// <para>Get SoundFonts paths to use by supported MIDI backends.</para>
    /// <para>There are several factors that determine what will be reported by this
    /// function:</para>
    /// <list type="bullet">
    /// <item>If the boolean _SDL hint_ <c>"SDL_FORCE_SOUNDFONTS"</c> is set, AND the
    /// <c>"SDL_SOUNDFONTS"</c> _environment variable_ is also set, this function will
    /// return that environment variable regardless of whether
    /// <see cref="SetSoundFonts"/> was ever called.</item>
    /// <item>Otherwise, if <see cref="SetSoundFonts"/> was successfully called with a non-NULL
    /// path, this function will return the string passed to that function.</item>
    /// <item>Otherwise, if the <c>"SDL_SOUNDFONTS"</c> variable is set, this function will
    /// return that environment variable.</item>
    /// <item>Otherwise, this function will search some common locations on the
    /// filesystem, and if it finds a SoundFont there, it will return that.</item>
    /// <item>Failing everything else, this function returns <c>null</c>.</item>
    /// </list>
    /// <para>This returns a pointer to internal (possibly read-only) memory, and it
    /// should not be modified or free'd by the caller.</para>
    /// </summary>
    /// <returns>semicolon-separated list of sound font paths.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    public static string GetSoundFonts()
    {
        var value = Mix_GetSoundFonts(); 
        return value == IntPtr.Zero ? "" : Marshal.PtrToStringUTF8(value)!;
    }
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL Mix_EachSoundFont(Mix_EachSoundFontCallback function, void *data);</code>
    /// <summary>
    /// <para>Iterate SoundFonts paths to use by supported MIDI backends.</para>
    /// <para>This function will take the string reported by <see cref="GetSoundFonts"/>, split
    /// it up into separate paths, as delimited by semicolons in the string, and
    /// call a callback function for each separate path.</para>
    /// <para>If there are no paths available, this returns 0 without calling the
    /// callback at all.</para>
    /// <para>If the callback returns non-zero, this function stops iterating and returns
    /// non-zero. If the callback returns 0, this function will continue iterating,
    /// calling the callback again for further paths. If the callback never returns
    /// 1, this function returns 0, so this can be used to decide if an available
    /// soundfont is acceptable for use.</para>
    /// </summary>
    /// <param name="function">the callback function to call once per path.</param>
    /// <param name="data">a pointer to pass to the callback for its own personal use.</param>
    /// <returns><c>true</c> if callback ever returned <c>true</c>, <c>false</c> on error or if the
    /// callback never returned <c>true</c>.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_EachSoundFont"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool EachSoundFont(EachSoundFontCallback function, IntPtr data);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL Mix_SetTimidityCfg(const char *path);</code>
    /// <summary>
    /// <para>Set full path of the Timidity config file.</para>
    /// <para>For example, "/etc/timidity.cfg"</para>
    /// <para>This is obviously only useful if SDL_mixer is using Timidity internally to
    /// play MIDI files.</para>
    /// </summary>
    /// <param name="path">path to a Timidity config file.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_SetTimidityCfg"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetTimidityCfg([MarshalAs(UnmanagedType.LPUTF8Str)] string path);
    
    
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_GetTimidityCfg"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr Mix_GetTimidityCfg();
    /// <code>extern SDL_DECLSPEC const char* SDLCALL Mix_GetTimidityCfg(void);</code>
    /// <summary>
    /// <para>Get full path of a previously-specified Timidity config file.</para>
    /// <para>For example, "/etc/timidity.cfg"</para>
    /// <para>If a path has never been specified, this returns <c>null</c>.</para>
    /// <para>This returns a pointer to internal memory, and it should not be modified or
    /// free'd by the caller.</para>
    /// </summary>
    /// <returns>the previously-specified path, or <c>null</c> if not set.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="SetTimidityCfg"/>
    public static string? GetTimidityCfg()
    {
        var value = Mix_GetTimidityCfg(); 
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }
    
    
    /// <code>extern SDL_DECLSPEC Mix_Chunk * SDLCALL Mix_GetChunk(int channel);</code>
    /// <summary>
    /// <para>Get the Mix_Chunk currently associated with a mixer channel.</para>
    /// <para>You may not specify <see cref="ChannelPost"/> or -1 for a channel.</para>
    /// </summary>
    /// <param name="channel">the channel to query.</param>
    /// <returns>the associated chunk, if any, or <c>null</c> if it's an invalid channel.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_GetChunk"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetChunk(int channel);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL Mix_CloseAudio(void);</code>
    /// <summary>
    /// <para>Close the mixer, halting all playing audio.</para>
    /// <para>Any halted channels will have any currently-registered effects
    /// deregistered, and will call any callback specified by <see cref="ChannelFinished"/>
    /// before this function returns.</para>
    /// <para>Any halted music will call any callback specified by
    /// <see cref="HookMusicFinished"/> before this function returns.</para>
    /// <para>Do not start any new audio playing during callbacks in this function.</para>
    /// <para>This will close the audio device. Attempting to play new audio after this
    /// function returns will fail, until another successful call to
    /// <see cref="OpenAudio(uint, in SDL.AudioSpec)"/>.</para>
    /// <para>Note that (unlike Mix_OpenAudio optionally calling Init(InitFlags.Audio)
    /// on the app's behalf), this will _not_ deinitialize the SDL audio subsystem
    /// in any case. At some point after calling this function and <see cref="Quit"/>, some
    /// part of the application should be responsible for calling <see cref="SDL.Quit"/> to
    /// deinitialize all of SDL, including its audio subsystem.</para>
    /// <para>This function should be the last thing you call in SDL_mixer before
    /// <see cref="Quit"/>. However, the following notes apply if you don't follow this
    /// advice:</para>
    /// <para>Note that this will not free any loaded chunks or music; you should dispose
    /// of those resources separately. It is probably poor form to dispose of them
    /// _after_ this function, but it is safe to call <see cref="FreeChunk"/> and
    /// <see cref="FreeMusic"/> after closing the device.</para>
    /// <para>Note that any chunks or music you don't free may or may not work if you
    /// call <see cref="OpenAudio(uint, in SDL.AudioSpec)"/> again, as the audio device may be in a new format and
    /// the existing chunks will not be converted to match.</para>
    /// </summary>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="Quit"/>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_CloseAudio"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CloseAudio();
}