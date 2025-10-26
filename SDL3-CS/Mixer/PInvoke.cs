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
    /// <code>extern SDL_DECLSPEC int SDLCALL MIX_Version(void);</code>
    /// <summary>
    /// Get the version of SDL_mixer that is linked against your program.
    /// <para>If you are linking to SDL_mixer dynamically, then it is possible that the
    /// current version will be different than the version you compiled against.
    /// This function returns the current version, while SDL_MIXER_VERSION is the
    /// version you compiled with.</para>
    /// </summary>
    /// <remarks>This function may be called safely at any time, even before <see cref="Init"/>.</remarks>
    /// <returns>the version of the linked library.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_Version"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int Version();
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_Init(void);</code>
    /// <summary>
    /// <para>Initialize the SDL_mixer library.</para>
    /// <para>This must be successfully called once before (almost) any other SDL_mixer
    /// function can be used.</para>
    /// <para>It is safe to call this multiple times; the library will only initialize
    /// once, and won't deinitialize until <see cref="Quit"/> has been called a matching
    /// number of times. Extra attempts to init report success.</para>
    /// </summary>
    /// <returns>true on success, false on error; call <see cref="SDL.GetError"/> for details.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="Quit"/>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_Init"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool Init();
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL MIX_Quit(void);</code>
    /// <summary>
    /// <para>Deinitialize the SDL_mixer library.</para>
    /// <para>This must be called when done with the library, probably at the end of your
    /// program.</para>
    /// <para>It is safe to call this multiple times; the library will only deinitialize
    /// once, when this function is called the same number of times as MIX_Init was
    /// successfully called.</para>
    /// <para>Once you have successfully deinitialized the library, it is safe to call
    /// MIX_Init to reinitialize it for further use.</para>
    /// <para>On successful deinitialization, SDL_mixer will destroy almost all created
    /// objects, including objects of type:</para>
    /// <list type="bullet">
    /// <item>MIX_Mixer</item>
    /// <item>MIX_Track</item>
    /// <item>MIX_Audio</item>
    /// <item>MIX_Group</item>
    /// <item>MIX_AudioDecoder</item>
    /// </list>
    /// <para>...which is to say: it's possible a single call to this function will clean
    /// up anything it allocated, stop all audio output, close audio devices, etc.
    /// Don't attempt to destroy objects after this call. The app is still
    /// encouraged to manage their resources carefully and clean up first, treating
    /// this function as a safety net against memory leaks.</para>
    /// </summary>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="Init"/>
    [LibraryImport(MixerLibrary, EntryPoint = "Mix_Quit"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Quit();
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL MIX_GetNumAudioDecoders(void);</code>
    /// <summary>
    /// Report the number of audio decoders available for use.
    /// <para>An audio decoder is what turns specific audio file formats into usable PCM
    /// data. For example, there might be an MP3 decoder, or a WAV decoder, etc.
    /// SDL_mixer probably has several decoders built in.</para>
    /// <para>The return value can be used to call <see cref="GetAudioDecoder"/> in a loop.</para>
    /// <para>The number of decoders available is decided during <see cref="Init"/> and does not
    /// change until the library is deinitialized.</para>
    /// </summary>
    /// <returns>the number of decoders available.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="GetAudioDecoder"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_GetNumAudioDecoders"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumAudioDecoders();
    
    
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_GetAudioDecoder"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr MIX_GetAudioDecoder(int index);

    /// <code>extern SDL_DECLSPEC const char * SDLCALL MIX_GetAudioDecoder(int index);</code>
    /// <summary>
    /// <para>Report the name of a specific audio decoders.</para>
    /// <para>An audio decoder is what turns specific audio file formats into usable PCM
    /// data. For example, there might be an MP3 decoder, or a WAV decoder, etc.
    /// SDL_mixer probably has several decoders built in.</para>
    /// <para>The names are capital English letters and numbers, low-ASCII. They don't
    /// necessarily map to a specific file format; Some decoders, like "XMP"
    /// operate on multiple file types, and more than one decoder might handle the
    /// same file type, like "DRMP3" vs "MPG123". Note that in that last example,
    /// neither decoder is called "MP3".</para>
    /// <para>The index of a specific decoder is decided during <see cref="Init"/> and does not
    /// change until the library is deinitialized. Valid indices are between zero
    /// and the return value of <see cref="GetNumAudioDecoders"/>.</para>
    /// <para>The returned pointer is const memory owned by SDL_mixer; do not free it.</para>
    /// </summary>
    /// <param name="index">the index of the decoder to query.</param>
    /// <returns>a UTF-8 (really, ASCII) string of the decoder's name, or <c>null</c> if
    /// <c>index</c> is invalid.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="GetNumAudioDecoders"/>
    public static string? GetAudioDecoder(int index) => Marshal.PtrToStringUTF8(MIX_GetAudioDecoder(index));
    
    
    /// <code>extern SDL_DECLSPEC MIX_Mixer * SDLCALL MIX_CreateMixerDevice(SDL_AudioDeviceID devid, const SDL_AudioSpec *spec);</code>
    /// <summary>
    /// Create a mixer that plays sound directly to an audio device.
    /// <para>This is usually the function you want, vs <see cref="CreateMixer"/>.</para>
    /// <para>You can choose a specific device ID to open, following SDL's usual rules,
    /// but often the correct choice is to specify
    /// <see cref="SDL.AudioDeviceDefaultPlayback"/> and let SDL figure out what device to use
    /// (and seamlessly transition you to new hardware if the default changes).</para>
    /// <para>Only playback devices make sense here. Attempting to open a recording
    /// device will fail.</para>
    /// <para>This will call SDL.Init(InitFlags.Audio) internally; it's safe to call
    /// <see cref="Init"/> before this call, too, if you intend to enumerate audio devices
    /// to choose one to open here.</para>
    /// <para>An audio format can be requested, and the system will try to set the
    /// hardware to those specifications, or as close as possible, but this is just
    /// a hint. SDL_mixer will handle all data conversion behind the scenes in any
    /// case, and specifying a <c>null</c> spec is a reasonable choice. The best reason to
    /// specify a format is because you know all your data is in that format and it
    /// might save some unnecessary CPU time on conversion.</para>
    /// <para>The actual device format chosen is available through <see cref="GetMixerFormat"/>.</para>
    /// <para> Once a mixer is created, next steps are usually to load audio (through
    /// <see cref="LoadAudio"/> and friends), create a track (<see cref="CreateTrack"/>), and play
    /// that audio through that track.</para>
    /// <para>When done with the mixer, it can be destroyed with <see cref="DestroyMixer"/>.</para>
    /// </summary>
    /// <param name="devid">the device to open for playback, or
    /// <see cref="SDL.AudioDeviceDefaultPlayback"/> for the default.</param>
    /// <param name="spec">the audio format request from the device. May be <c>null</c>.</param>
    /// <returns>a mixer that can be used to play audio, or <c>null</c> on failure; call
    /// <see cref="SDL.GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="CreateMixer"/>
    /// <seealso cref="DestroyMixer"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_CreateMixerDevice"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateMixerDevice(uint devid, IntPtr spec);
    
    
    /// <code>extern SDL_DECLSPEC MIX_Mixer * SDLCALL MIX_CreateMixer(const SDL_AudioSpec *spec);</code>
    /// <summary>
    /// Create a mixer that generates audio to a memory buffer.
    /// <para>Usually you want <see cref="CreateMixerDevice"/> instead of this function. The
    /// mixer created here can be used with <see cref="Generate"/> to produce more data on
    /// demand, as fast as desired.</para>
    /// <para>An audio format must be specified. This is the format it will output in.
    /// This cannot be <c>null</c>.</para>
    /// <para>Once a mixer is created, next steps are usually to load audio (through
    /// <see cref="LoadAudio"/> and friends), create a track (<see cref="CreateTrack"/>), and play
    /// that audio through that track.</para>
    /// <para>When done with the mixer, it can be destroyed with <see cref="DestroyMixer"/>.</para>
    /// </summary>
    /// <param name="spec">the audio format that mixer will generate.</param>
    /// <returns>a mixer that can be used to generate audio, or <c>null</c> on failure;
    /// call <see cref="SDL.GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="CreateMixerDevice"/>
    /// <seealso cref="DestroyMixer"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_CreateMixer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateMixer(IntPtr spec);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL MIX_DestroyMixer(MIX_Mixer *mixer);</code>
    /// <summary>
    /// Free a mixer.
    /// <para>If this mixer was created with <see cref="CreateMixerDevice"/>, this function will
    /// also close the audio device and call SDL.QuitSubSystem(InitFlags.Audio).</para>
    /// <para>Any MIX_Group or MIX_Track created for this mixer will also be destroyed.
    /// Do not access them again or attempt to destroy them after the device is
    /// destroyed. MIX_Audio objects will not be destroyed, since they can be
    /// shared between mixers (but those will all be destroyed during <see cref="Quit"/>).</para>
    /// </summary>
    /// <param name="mixer">the mixer to destroy.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="CreateMixerDevice"/>
    /// <seealso cref="CreateMixer"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_DestroyMixer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyMixer(IntPtr mixer);
    
    
    /// <code>extern SDL_DECLSPEC SDL_PropertiesID SDLCALL MIX_GetMixerProperties(MIX_Mixer *mixer);</code>
    /// <summary>
    /// Get the properties associated with a mixer.
    /// <para>Currently SDL_mixer assigns no properties of its own to a mixer, but this
    /// can be a convenient place to store app-specific data.</para>
    /// <para> A SDL_PropertiesID is created the first time this function is called for a
    /// given mixer.</para>
    /// </summary>
    /// <param name="mixer">the mixer to query.</param>
    /// <returns>a valid property ID on success or 0 on failure; call
    /// <see cref="SDL.GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_GetMixerProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint GetMixerProperties(IntPtr mixer);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_GetMixerFormat(MIX_Mixer *mixer, SDL_AudioSpec *spec);</code>
    /// <summary>
    /// Get the audio format a mixer is generating.
    /// <para>Generally you don't need this information, as SDL_mixer will convert data
    /// as necessary between inputs you provide and its output format, but it might
    /// be useful if trying to match your inputs to reduce conversion and
    /// resampling costs.</para>
    /// <para>For mixers created with <see cref="CreateMixerDevice"/>, this is the format of the
    /// audio device (and may change later if the device itself changes; SDL_mixer
    /// will seamlessly handle this change internally, though).</para>
    /// <para>For mixers created with <see cref="CreateMixer"/>, this is the format that
    /// <see cref="Generate"/> will produce, as requested at create time, and does not
    /// change.</para>
    /// <para>Note that internally, SDL_mixer will work in SDL.AudioFormat.AudioF32 format before
    /// outputting the format specified here, so it would be more efficient to
    /// match input data to that, not the final output format.</para>
    /// </summary>
    /// <param name="mixer">the mixer to query.</param>
    /// <param name="spec">where to store the mixer audio format.</param>
    /// <returns>true on success or false on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_GetMixerFormat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetMixerFormat(IntPtr mixer, IntPtr spec);
    
    
    /// <code>extern SDL_DECLSPEC MIX_Audio * SDLCALL MIX_LoadAudio_IO(MIX_Mixer *mixer, SDL_IOStream *io, bool predecode, bool closeio);</code>
    /// <summary>
    /// Load audio for playback from an SDL_IOStream.
    /// <para>In normal usage, apps should load audio once, maybe at startup, then play
    /// it multiple times.</para>
    /// <para>When loading audio, it will be cached fully in RAM in its original data
    /// format. Each time it plays, the data will be decoded. For example, an MP3
    /// will be stored in memory in MP3 format and be decompressed on the fly
    /// during playback. This is a tradeoff between i/o overhead and memory usage.</para>
    /// <para>If <c>predecode</c> is true, the data will be decompressed during load and
    /// stored as raw PCM data. This might dramatically increase loading time and
    /// memory usage, but there will be no need to decompress data during playback.</para>
    /// <para>(One could also use <see cref="SetTrackIOStream"/> to bypass loading the data into
    /// RAM upfront at all, but this offers still different tradeoffs. The correct
    /// approach depends on the app's needs and employing different approaches in
    /// different situations can make sense.)</para>
    /// <para>MIX_Audio objects can be shared between mixers. This function takes a
    /// MIX_Mixer, to imply this is the most likely place it will be used and
    /// loading should try to match its audio format, but the resulting audio can
    /// be used elsewhere. If <c>mixer</c> is <c>null</c>, SDL_mixer will set reasonable
    /// defaults.</para>
    /// <para>Once a MIX_Audio is created, it can be assigned to a MIX_Track with
    /// <see cref="SetTrackAudio"/>, or played without any management with <see cref="PlayAudio"/>.</para>
    /// <para>When done with a MIX_Audio, it can be freed with <see cref="DestroyAudio"/>.</para>
    /// <para>This function loads data from an SDL_IOStream. There is also a version that
    /// loads from a path on the filesystem (<see cref="LoadAudio"/>), and one that accepts
    /// properties for ultimate control (<see cref="LoadAudioWithProperties"/>).</para>
    /// <para>The SDL_IOStream provided must be able to seek, or loading will fail. If
    /// the stream can't seek (data is coming from an HTTP connection, etc),
    /// consider caching the data to memory or disk first and creating a new stream
    /// to read from there.</para>
    /// </summary>
    /// <param name="mixer">a mixer this audio is intended to be used with. May be <c>null</c>.</param>
    /// <param name="io">the SDL_IOStream to load data from.</param>
    /// <param name="predecode">if true, data will be fully uncompressed before returning.</param>
    /// <param name="closeio">true if SDL_mixer should close <c>io</c> before returning
    /// (success or failure).</param>
    /// <returns>an audio object that can be used to make sound on a mixer, or <c>null</c>
    /// on failure; call <see cref="SDL.GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="DestroyAudio"/>
    /// <seealso cref="SetTrackAudio"/>
    /// <seealso cref="LoadAudio"/>
    /// <seealso cref="LoadAudioWithProperties"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_LoadAudio_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadAudioIO(IntPtr mixer, IntPtr io, [MarshalAs(UnmanagedType.I1)] bool predecode, [MarshalAs(UnmanagedType.I1)] bool closeio);
    
    
    /// <code>extern SDL_DECLSPEC MIX_Audio * SDLCALL MIX_LoadAudio(MIX_Mixer *mixer, const char *path, bool predecode);</code>
    /// <summary>
    /// Load audio for playback from a file.
    /// <para>This is equivalent to calling:</para>
    /// <code>Mixer.LoadAudioIO(mixer, SDL.IOFromFile(path, "rb"), predecode, true);</code>
    /// <para>This function loads data from a path on the filesystem. There is also a
    /// version that loads from an SDL_IOStream (<see cref="LoadAudioIO"/>), and one that
    /// accepts properties for ultimate control (<see cref="LoadAudioWithProperties"/>).</para>
    /// </summary>
    /// <param name="mixer">a mixer this audio is intended to be used with. May be <c>null</c>.</param>
    /// <param name="path">the path on the filesystem to load data from.</param>
    /// <param name="predecode">if true, data will be fully uncompressed before returning.</param>
    /// <returns>an audio object that can be used to make sound on a mixer, or <c>null</c>
    /// on failure; call <see cref="SDL.GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="DestroyAudio"/>
    /// <seealso cref="SetTrackAudio"/>
    /// <seealso cref="LoadAudioIO"/>
    /// <seealso cref="LoadAudioWithProperties"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_LoadAudio"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadAudio(IntPtr mixer, [MarshalAs(UnmanagedType.LPUTF8Str)] string path, [MarshalAs(UnmanagedType.I1)] bool predecode);
    
    
    /// <code>extern SDL_DECLSPEC MIX_Audio * SDLCALL MIX_LoadAudioWithProperties(SDL_PropertiesID props);</code>
    /// <summary>
    /// Load audio for playback through a collection of properties.
    /// <para> Please see <see cref="LoadAudioIO"/> for a description of what the various
    /// LoadAudio functions do. This function uses properties to dictate how it
    /// operates, and exposes functionality the other functions don't provide.</para>
    /// <para>These are the supported properties:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.AudioLoadIOStreamPointer"/>: a pointer to an SDL_IOStream to
    /// be used to load audio data. Required. This stream must be able to seek!</item>
    /// <item><see cref="Props.AudioLoadCloseIOBoolean"/>: true if SDL_mixer should close the
    /// SDL_IOStream before returning (success or failure).</item>
    /// <item><see cref="Props.AudioLoadPreDecodeBoolean"/>: true if SDL_mixer should fully
    /// decode and decompress the data before returning. Otherwise it will be
    /// stored in its original state and decompressed on demand.</item>
    /// <item><see cref="Props.AudioLoadPreferredMixerPointer"/>: a pointer to a MIX_Mixer,
    /// in case steps can be made to match its format when decoding. Optional.</item>
    /// <item><see cref="Props.AudioLoadSkipMetadataTagsBoolean"/>: true to skip parsing
    /// metadata tags, like ID3 and APE tags. This can be used to speed up
    /// loading _if the data definitely doesn't have these tags_. Some decoders
    /// will fail if these tags are present when this property is true.</item>
    /// <item><see cref="Props.AudioDecoderString"/>: the name of the decoder to use for this
    /// data. Optional. If not specified, SDL_mixer will examine the data and
    /// choose the best decoder. These names are the same returned from
    /// <see cref="GetAudioDecoder"/>.</item>
    /// </list>
    /// <para>Specific decoders might accept additional custom properties, such as where
    /// to find soundfonts for MIDI playback, etc.</para>
    /// </summary>
    /// <param name="props">a set of properties on how to load audio.</param>
    /// <returns>an audio object that can be used to make sound on a mixer, or <c>null</c>
    /// on failure; call <see cref="SDL.GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="DestroyAudio"/>
    /// <seealso cref="SetTrackAudio"/>
    /// <seealso cref="LoadAudio"/>
    /// <seealso cref="LoadAudioIO"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_LoadAudioWithProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadAudioWithProperties(uint props);
    
    
    /// <code>extern SDL_DECLSPEC MIX_Audio * SDLCALL MIX_LoadRawAudio_IO(MIX_Mixer *mixer, SDL_IOStream *io, const SDL_AudioSpec *spec, bool closeio);</code>
    /// <summary>
    /// Load raw PCM data from an SDL_IOStream.
    /// <para>There are other options for _streaming_ raw PCM: an SDL_AudioStream can be
    /// connected to a track, as can an SDL_IOStream, and will read from those
    /// sources on-demand when it is time to mix the audio. This function is useful
    /// for loading static audio data that is meant to be played multiple times.</para>
    /// <para>This function will load the raw data in its entirety and cache it in RAM.</para>
    /// <para> MIX_Audio objects can be shared between multiple mixers. The `mixer`
    /// parameter just suggests the most likely mixer to use this audio, in case
    /// some optimization might be applied, but this is not required, and a NULL
    /// mixer may be specified.</para>
    /// </summary>
    /// <param name="mixer">a mixer this audio is intended to be used with. May be <c>null</c>.</param>
    /// <param name="io">the SDL_IOStream to load data from.</param>
    /// <param name="spec">what format the raw data is in.</param>
    /// <param name="closeio"> true if SDL_mixer should close <c>io</c> before returning
    /// (success or failure).</param>
    /// <returns>an audio object that can be used to make sound on a mixer, or <c>null</c>
    /// on failure; call <see cref="SDL.GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="DestroyAudio"/>
    /// <seealso cref="SetTrackAudio"/>
    /// <seealso cref="LoadRawAudio"/>
    /// <seealso cref="LoadRawAudioNoCopy"/>
    /// <seealso cref="LoadAudioIO"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_LoadRawAudio_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadRawAudioIO(IntPtr mixer, IntPtr io, in IntPtr spec, [MarshalAs(UnmanagedType.I1)] bool closeio);
    
    
    /// <code>extern SDL_DECLSPEC MIX_Audio * SDLCALL MIX_LoadRawAudio(MIX_Mixer *mixer, const void *data, size_t datalen, const SDL_AudioSpec *spec);</code>
    /// <summary>
    /// Load raw PCM data from a memory buffer.
    /// <para>There are other options for _streaming_ raw PCM: an SDL_AudioStream can be
    /// connected to a track, as can an SDL_IOStream, and will read from those
    /// sources on-demand when it is time to mix the audio. This function is useful
    /// for loading static audio data that is meant to be played multiple times.</para>
    /// <para>This function will load the raw data in its entirety and cache it in RAM,
    /// allocating a copy. If the original data will outlive the created MIX_Audio,
    /// you can use <see cref="LoadRawAudioNoCopy"/> to avoid extra allocations and copies.</para>
    /// <para>MIX_Audio objects can be shared between multiple mixers. The <c>mixer</c>
    /// parameter just suggests the most likely mixer to use this audio, in case
    /// some optimization might be applied, but this is not required, and a <c>null</c>
    /// mixer may be specified.</para>
    /// </summary>
    /// <param name="mixer">a mixer this audio is intended to be used with. May be <c>null</c>.</param>
    /// <param name="data">the raw PCM data to load.</param>
    /// <param name="datalen">the size, in bytes, of the raw PCM data.</param>
    /// <param name="spec">what format the raw data is in.</param>
    /// <returns>an audio object that can be used to make sound on a mixer, or <c>null</c>
    /// on failure; call <see cref="SDL.GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="DestroyAudio"/>
    /// <seealso cref="SetTrackAudio"/>
    /// <seealso cref="LoadRawAudioIO"/>
    /// <seealso cref="LoadRawAudioNoCopy"/>
    /// <seealso cref="LoadAudioIO"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_LoadRawAudio"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadRawAudio(IntPtr mixer, IntPtr data, UIntPtr datalen, in IntPtr spec);
    
    
    /// <code>extern SDL_DECLSPEC MIX_Audio * SDLCALL MIX_LoadRawAudioNoCopy(MIX_Mixer *mixer, const void *data, size_t datalen, const SDL_AudioSpec *spec, bool free_when_done);</code>
    /// <summary>
    /// Load raw PCM data from a memory buffer without making a copy.
    /// <para>This buffer must live for the entire time the returned MIX_Audio lives, as
    /// it will access it whenever it needs to mix more data.</para>
    /// <para>This function is meant to maximize efficiency: if the data is already in
    /// memory and can remain there, don't copy it. But it can also lead to some
    /// interesting tricks, like changing the buffer's contents to alter multiple
    /// playing tracks at once. (But, of course, be careful when being too clever.)</para>
    /// <para> MIX_Audio objects can be shared between multiple mixers. The <c>mixer</c>
    /// parameter just suggests the most likely mixer to use this audio, in case
    /// some optimization might be applied, but this is not required, and a NULL
    /// mixer may be specified.</para>
    /// <para>If <c>freeWhenDone</c> is true, SDL_mixer will call <c>SDL.Free(data)</c> when the
    /// returned MIX_Audio is eventually destroyed. This can be useful when the
    /// data is not static, but rather composed dynamically for this specific
    /// MIX_Audio and simply wants to avoid the extra copy.</para>
    /// </summary>
    /// <param name="mixer">a mixer this audio is intended to be used with. May be <c>null</c>.</param>
    /// <param name="data">the buffer where the raw PCM data lives.</param>
    /// <param name="datalen">the size, in bytes, of the buffer.</param>
    /// <param name="spec">what format the raw data is in.</param>
    /// <param name="freeWhenDone">if true, <c>data</c> will be given to <see cref="SDL.Free"/> when the
    /// MIX_Audio is destroyed.</param>
    /// <returns>an audio object that can be used to make sound on a mixer, or <c>null</c>
    /// on failure; call <see cref="SDL.GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="DestroyAudio"/>
    /// <seealso cref="SetTrackAudio"/>
    /// <seealso cref="LoadRawAudio"/>
    /// <seealso cref="LoadRawAudioIO"/>
    /// <seealso cref="LoadAudioIO"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_LoadRawAudioNoCopy"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr LoadRawAudioNoCopy(IntPtr mixer, IntPtr data, UIntPtr datalen, in IntPtr spec, [MarshalAs(UnmanagedType.I1)] bool freeWhenDone);
    
    
    /// <code>extern SDL_DECLSPEC MIX_Audio * SDLCALL MIX_CreateSineWaveAudio(MIX_Mixer *mixer, int hz, float amplitude);</code>
    /// <summary>
    /// Create a MIX_Audio that generates a sinewave.
    /// <para>This is useful just to have _something_ to play, perhaps for testing or
    /// debugging purposes.</para>
    /// <para>The resulting MIX_Audio will generate infinite audio when assigned to a
    /// track.</para>
    /// <para>You specify its frequency in Hz (determines the pitch of the sinewave's
    /// audio) and amplitude (determines the volume of the sinewave: 1.0f is very
    /// loud, 0.0f is silent).</para>
    /// <para>MIX_Audio objects can be shared between multiple mixers. The <c>mixer</c>
    /// parameter just suggests the most likely mixer to use this audio, in case
    /// some optimization might be applied, but this is not required, and a NULL
    /// mixer may be specified.</para>
    /// </summary>
    /// <param name="mixer">a mixer this audio is intended to be used with. May be <c>null</c>.</param>
    /// <param name="hz">the sinewave's frequency in Hz.</param>
    /// <param name="amplitude">the sinewave's amplitude from 0.0f to 1.0f.</param>
    /// <returns>an audio object that can be used to make sound on a mixer, or <c>null</c>
    /// on failure; call <see cref="SDL.GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="DestroyAudio"/>
    /// <seealso cref="SetTrackAudio"/>
    /// <seealso cref="LoadAudioIO"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_CreateSineWaveAudio"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateSineWaveAudio(IntPtr mixer, int hz, float amplitude);
    
    
    /// <code>extern SDL_DECLSPEC SDL_PropertiesID SDLCALL MIX_GetAudioProperties(MIX_Audio *audio);</code>
    /// <summary>
    /// <para>The following read-only properties are provided by SDL_mixer:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.MetadataTitleString"/>: the audio's title ("Smells Like Teen
    /// Spirit").</item>
    /// <item><see cref="Props.MetadataArtistString"/>: the audio's artist name ("Nirvana").</item>
    /// <item><see cref="Props.MetadataAlbumString"/>: the audio's album name ("Nevermind").</item>
    /// <item><see cref="Props.MetadataCopyrightString"/>: the audio's copyright info
    /// ("Copyright (c) 1991")</item>
    /// <item><see cref="Props.MetadataTrackNumber"/>: the audio's track number on the album
    /// (1)</item>
    /// <item><see cref="Props.MetadataTotalTrackSNumber"/>: the total tracks on the album
    /// (13)</item>
    /// <item><see cref="Props.MetadataYearNumber"/>: the year the audio was released (1991)</item>
    /// <item><see cref="Props.MetadataDurationFramesNumber"/>: The sample frames worth of
    /// PCM data that comprise this audio. It might be off by a little if the
    /// decoder only knows the duration as a unit of time.</item>
    /// <item><see cref="Props.MetadataDurationInfiniteBoolean"/>: if true, audio never runs
    /// out of sound to generate. This isn't necessarily always known to
    /// SDL_mixer, though.</item>
    /// </list>
    /// <para>Other properties, documented with <see cref="LoadAudioWithProperties"/>, may also
    /// be present.</para>
    /// <para>Note that the metadata properties are whatever SDL_mixer finds in things
    /// like ID3 tags, and they often have very little standardized formatting, may
    /// be missing, and can be completely wrong if the original data is
    /// untrustworthy (like an MP3 from a P2P file sharing service).</para>
    /// </summary>
    /// <param name="audio">the audio to query.</param>
    /// <returns>a valid property ID on success or 0 on failure; call
    /// <see cref="SDL.GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_GetAudioProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint GetAudioProperties(IntPtr audio);
    
    
    /// <code>extern SDL_DECLSPEC Sint64 SDLCALL MIX_GetAudioDuration(MIX_Audio *audio);</code>
    /// <summary>
    /// Get the length of a MIX_Audio's playback in sample frames.
    /// <para>This information is also available via the
    /// <see cref="Props.MetadataDurationFramesNumber"/> property, but it's common enough
    /// to provide a simple accessor function.</para>
    /// <para>This reports the length of the data in _sample frames_, so sample-perfect
    /// mixing can be possible. Sample frames are only meaningful as a measure of
    /// time if the sample rate (frequency) is also known. To convert from sample
    /// frames to milliseconds, use <see cref="AudioFramesToMS"/>.</para>
    /// <para>Not all audio file formats can report the complete length of the data they
    /// will produce through decoding: some can't calculate it, some might produce
    /// infinite audio.</para>
    /// <para>Also, some file formats can only report duration as a unit of time, which
    /// means SDL_mixer might have to estimate sample frames from that information.
    /// With less precision, the reported duration might be off by a few sample
    /// frames in either direction.</para>
    /// <para>This will return a value >= 0 if a duration is known. It might also return
    /// <see cref="DurationUnknown"/> or <see cref="DurationInfinite"/>.</para>
    /// </summary>
    /// <param name="audio">the audio to query.</param>
    /// <returns>the length of the audio in sample frames, or <see cref="DurationUnknown"/>
    /// or <see cref="DurationInfinite"/>.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_GetAudioDuration"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long GetAudioDuration(IntPtr audio);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_GetAudioFormat(MIX_Audio *audio, SDL_AudioSpec *spec);</code>
    /// <summary>
    /// Query the initial audio format of a MIX_Audio.
    /// <para>Note that some audio files can change format in the middle; some explicitly
    /// support this, but a more common example is two MP3 files concatenated
    /// together. In many cases, SDL_mixer will correctly handle these sort of
    /// files, but this function will only report the initial format a file uses.</para>
    /// </summary>
    /// <param name="audio">the audio to query.</param>
    /// <param name="spec">on success, audio format details will be stored here.</param>
    /// <returns>true on success or false on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_GetAudioFormat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetAudioFormat(IntPtr audio, IntPtr spec);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL MIX_DestroyAudio(MIX_Audio *audio);</code>
    /// <summary>
    /// Destroy the specified audio.
    /// <para>MIX_Audio is reference-counted internally, so this function only unrefs it.
    /// If doing so causes the reference count to drop to zero, the MIX_Audio will
    /// be deallocated. This allows the system to safely operate if the audio is
    /// still assigned to a MIX_Track at the time of destruction. The actual
    /// destroying will happen when the track stops using it.</para>
    /// <para>But from the caller's perspective, once this function is called, it should
    /// assume the <c>audio</c> pointer has become invalid.</para>
    /// <para>Destroying a <c>null</c> MIX_Audio is a legal no-op.</para>
    /// </summary>
    /// <param name="audio">the audio to destroy.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_DestroyAudio"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyAudio(IntPtr audio);
    
    
    /// <code>extern SDL_DECLSPEC MIX_Track * SDLCALL MIX_CreateTrack(MIX_Mixer *mixer);</code>
    /// <summary>
    /// Create a new track on a mixer.
    /// <para>A track provides a single source of audio. All currently-playing tracks
    /// will be processed and mixed together to form the final output from the
    /// mixer.</para>
    /// <para>There are no limits to the number of tracks on may create, beyond running
    /// out of memory, but in normal practice there are a small number of tracks
    /// that are reused between all loaded audio as appropriate.</para>
    /// <para>Tracks are unique to a specific MIX_Mixer and can't be transferred between
    /// them.</para>
    /// </summary>
    /// <param name="mixer">the mixer on which to create this track.</param>
    /// <returns>a new MIX_Track on success, <c>null</c> on error; call <see cref="SDL.GetError"/> for
    /// more informations.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="DestroyTrack"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_CreateTrack"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateTrack(IntPtr mixer);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL MIX_DestroyTrack(MIX_Track *track);</code>
    /// <summary>
    /// Destroy the specified track.
    /// <para>If the track is currently playing, it will be stopped immediately, without
    /// any fadeout. If there is a callback set through
    /// <see cref="SetTrackStoppedCallback"/>, it will _not_ be called.</para>
    /// <para> If the mixer is currently mixing in another thread, this will block until
    /// it finishes.</para>
    /// <para>Destroying a <c>null</c> MIX_Track is a legal no-op.</para>
    /// </summary>
    /// <param name="track">the track to destroy.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_DestroyTrack"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyTrack(IntPtr track);
    
    
    /// <code>extern SDL_DECLSPEC SDL_PropertiesID SDLCALL MIX_GetTrackProperties(MIX_Track *track);</code>
    /// <summary>
    /// Get the properties associated with a track.
    /// <para>Currently SDL_mixer assigns no properties of its own to a track, but this
    /// can be a convenient place to store app-specific data.</para>
    /// <para>A SDL_PropertiesID is created the first time this function is called for a
    /// given track.</para>
    /// </summary>
    /// <param name="track">the track to query.</param>
    /// <returns>a valid property ID on success or 0 on failure; call
    /// <see cref="SDL.GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_GetTrackProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint GetTrackProperties(IntPtr track);
    
    
    /// <code>extern SDL_DECLSPEC MIX_Mixer * SDLCALL MIX_GetTrackMixer(MIX_Track *track);</code>
    /// <summary>
    /// Get the MIX_Mixer that owns a MIX_Track.
    /// <para>This is the mixer pointer that was passed to <see cref="CreateTrack"/>.</para>
    /// </summary>
    /// <param name="track">the track to query.</param>
    /// <returns>the mixer associated with the track, or <c>null</c> on error; call
    /// <see cref="SDL.GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_GetTrackMixer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetTrackMixer(IntPtr track);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_SetTrackAudio(MIX_Track *track, MIX_Audio *audio);</code>
    /// <summary>
    /// Set a MIX_Track's input to a MIX_Audio.
    /// <para>A MIX_Audio is audio data stored in RAM (possibly still in a compressed
    /// form). One MIX_Audio can be assigned to multiple tracks at once.</para>
    /// <para>Once a track has a valid input, it can start mixing sound by calling
    /// <see cref="PlayTrack"/>, or possibly <seealso cref="PlayTag"/>.</para>
    /// <para>Calling this function with a NULL audio input is legal, and removes any
    /// input from the track. If the track was currently playing, the next time the
    /// mixer runs, it'll notice this and mark the track as stopped, calling any
    /// assigned <see cref="TrackStoppedCallback"/>.</para>
    /// <para>It is legal to change the input of a track while it's playing, however some
    /// states, like loop points, may cease to make sense with the new audio. In
    /// such a case, one can call <see cref="PlayTrack"/> again to adjust parameters.</para>
    /// <para>The track will hold a reference to the provided MIX_Audio, so it is safe to
    /// call <see cref="DestroyAudio"/> on it while the track is still using it. The track
    /// will drop its reference (and possibly free the resources) once it is no
    /// longer using the MIX_Audio.</para>
    /// </summary>
    /// <param name="track">the track on which to set a new audio input.</param>
    /// <param name="audio">the new audio input to set. May be <c>null</c>.</param>
    /// <returns>on success, false on error; call <see cref="SDL.GetError"/> for details.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_SetTrackAudio"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetTrackAudio(IntPtr track, IntPtr audio);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_SetTrackAudioStream(MIX_Track *track, SDL_AudioStream *stream);</code>
    /// <summary>
    /// Set a MIX_Track's input to an SDL_AudioStream.
    /// <para>Using an audio stream allows the application to generate any type of audio,
    /// in any format, possibly procedurally or on-demand, and mix in with all
    /// other tracks.</para>
    /// <para>When a track uses an audio stream, it will call SDL_GetAudioStreamData as
    /// it needs more audio to mix. The app can either buffer data to the stream
    /// ahead of time, or set a callback on the stream to provide data as needed.
    /// Please refer to SDL's documentation for details.</para>
    /// <para>A given audio stream may only be assigned to a single track at a time;
    /// duplicate assignments won't return an error, but assigning a stream to
    /// multiple tracks will cause each track to read from the stream arbitrarily,
    /// causing confusion and incorrect mixing.</para>
    /// <para>Once a track has a valid input, it can start mixing sound by calling
    /// <see cref="PlayTrack"/>, or possibly <see cref="PlayTag"/>.</para>
    /// <para>Calling this function with a <c>null</c> audio stream is legal, and removes any
    /// input from the track. If the track was currently playing, the next time the
    /// mixer runs, it'll notice this and mark the track as stopped, calling any
    /// assigned <see cref="TrackStoppedCallback"/>.</para>
    /// <para>It is legal to change the input of a track while it's playing, however some
    /// states, like loop points, may cease to make sense with the new audio. In
    /// such a case, one can call <see cref="PlayTrack"/> again to adjust parameters.</para>
    /// <para>The provided audio stream must remain valid until the track no longer needs
    /// it (either by changing the track's input or destroying the track).</para>
    /// </summary>
    /// <param name="track">the track on which to set a new audio input.</param>
    /// <param name="stream">the audio stream to use as the track's input.</param>
    /// <returns>true on success, false on error; call <see cref="SDL.GetError"/> for details.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_SetTrackAudioStream"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetTrackAudioStream(IntPtr track, IntPtr stream);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_SetTrackIOStream(MIX_Track *track, SDL_IOStream *io, bool closeio);</code>
    /// <summary>
    /// Set a MIX_Track's input to an SDL_IOStream.
    /// <para>This is not the recommended way to set a track's input, but this can be
    /// useful for a very specific scenario: a large file, to be played once, that
    /// must be read from disk in small chunks as needed. In most cases, however,
    /// it is preferable to create a MIX_Audio ahead of time and use
    /// <see cref="SetTrackAudio"/> instead.</para>
    /// <para>The stream supplied here should provide an audio file in a supported
    /// format. SDL_mixer will parse it during this call to make sure it's valid,
    /// and then will read file data from the stream as it needs to decode more
    /// during mixing.</para>
    /// <para>The stream must be able to seek through the complete set of data, or this
    /// function will fail.</para>
    /// <para>A given IOStream may only be assigned to a single track at a time;
    /// duplicate assignments won't return an error, but assigning a stream to
    /// multiple tracks will cause each track to read from the stream arbitrarily,
    /// causing confusion, incorrect mixing, or failure to decode.</para>
    /// <para>Once a track has a valid input, it can start mixing sound by calling
    /// <see cref="PlayTrack"/>, or possibly <see cref="PlayTag"/>.</para>
    /// <para>Calling this function with a <c>null</c> stream is legal, and removes any input
    /// from the track. If the track was currently playing, the next time the mixer
    /// runs, it'll notice this and mark the track as stopped, calling any assigned
    /// <see cref="TrackStoppedCallback"/>.</para>
    /// <para>It is legal to change the input of a track while it's playing, however some
    /// states, like loop points, may cease to make sense with the new audio. In
    /// such a case, one can call <see cref="PlayTrack"/> again to adjust parameters.</para>
    /// <para>The provided stream must remain valid until the track no longer needs it
    /// (either by changing the track's input or destroying the track).</para>
    /// </summary>
    /// <param name="track">the track on which to set a new audio input.</param>
    /// <param name="io">the new i/o stream to use as the track's input.</param>
    /// <param name="closeio">if true, close the stream when done with it.</param>
    /// <returns>true on success, false on error; call <see cref="SDL.GetError"/> for details.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="SetTrackRawIOStream"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_SetTrackIOStream"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetTrackIOStream(IntPtr track, IntPtr io, [MarshalAs(UnmanagedType.I1)] bool closeio);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_SetTrackRawIOStream(MIX_Track *track, SDL_IOStream *io, const SDL_AudioSpec *spec, bool closeio);</code>
    /// <summary>
    /// Set a MIX_Track's input to an SDL_IOStream providing raw PCM data.
    /// <para>This is not the recommended way to set a track's input, but this can be
    /// useful for a very specific scenario: a large file, to be played once, that
    /// must be read from disk in small chunks as needed. In most cases, however,
    /// it is preferable to create a MIX_Audio ahead of time and use
    /// <see cref="SetTrackAudio"/> instead.</para>
    /// <para>Also, an <see cref="SetTrackAudioStream"/> can _also_ provide raw PCM audio to a
    /// track, via an SDL_AudioStream, which might be preferable unless the data is
    /// already coming directly from an SDL_IOStream.</para>
    /// <para>The stream supplied here should provide an audio in raw PCM format.</para>
    /// <para>A given IOStream may only be assigned to a single track at a time;
    /// duplicate assignments won't return an error, but assigning a stream to
    /// multiple tracks will cause each track to read from the stream arbitrarily,
    /// causing confusion and incorrect mixing.</para>
    /// <para>Once a track has a valid input, it can start mixing sound by calling
    /// <see cref="PlayTrack"/>, or possibly <see cref="PlayTag"/>.</para>
    /// <para>Calling this function with a <c>null</c> stream is legal, and removes any input
    /// from the track. If the track was currently playing, the next time the mixer
    /// runs, it'll notice this and mark the track as stopped, calling any assigned
    /// <see cref="TrackStoppedCallback"/>.</para>
    /// <para>It is legal to change the input of a track while it's playing, however some
    /// states, like loop points, may cease to make sense with the new audio. In
    /// such a case, one can call <see cref="PlayTrack"/> again to adjust parameters.</para>
    /// <para>The provided stream must remain valid until the track no longer needs it
    /// (either by changing the track's input or destroying the track).</para>
    /// </summary>
    /// <param name="track">the track on which to set a new audio input.</param>
    /// <param name="io">the new i/o stream to use as the track's input.</param>
    /// <param name="spec">the format of the PCM data that the SDL_IOStream will provide</param>
    /// <param name="closeio">if true, close the stream when done with it.</param>
    /// <returns>true on success, false on error; call <see cref="SDL.GetError"/> for details.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="SetTrackAudioStream"/>
    /// <seealso cref="SetTrackIOStream"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_SetTrackRawIOStream"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetTrackRawIOStream(IntPtr track, IntPtr io, in IntPtr spec, [MarshalAs(UnmanagedType.I1)] bool closeio);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_TagTrack(MIX_Track *track, const char *tag);</code>
    /// <summary>
    /// Assign an arbitrary tag to a track.
    /// <para>A tag can be any valid C string in UTF-8 encoding. It can be useful to
    /// group tracks in various ways. For example, everything in-game might be
    /// marked as "game", so when the user brings up the settings menu, the app can
    /// pause all tracks involved in gameplay at once, but keep background music
    /// and menu sound effects running.</para>
    /// <para>A track can have as many tags as desired, until the machine runs out of
    /// memory.</para>
    /// <para>It's legal to add the same tag to a track more than once; the extra
    /// attempts will report success but not change anything.</para>
    /// <para>Tags can later be removed with <see cref="UntagTrack"/>.</para>
    /// </summary>
    /// <param name="track">the track to add a tag to.</param>
    /// <param name="tag">the tag to add.</param>
    /// <returns>true on success, false on error; call <see cref="SDL.GetError"/> for details.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="UntagTrack"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_TagTrack"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool TagTrack(IntPtr track, [MarshalAs(UnmanagedType.LPUTF8Str)] string tag);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL MIX_UntagTrack(MIX_Track *track, const char *tag);</code>
    /// <summary>
    /// Remove an arbitrary tag from a track.
    /// <para>A tag can be any valid C string in UTF-8 encoding. It can be useful to
    /// group tracks in various ways. For example, everything in-game might be
    /// marked as "game", so when the user brings up the settings menu, the app can
    /// pause all tracks involved in gameplay at once, but keep background music
    /// and menu sound effects running.</para>
    /// <para>It's legal to remove a tag that the track doesn't have; this function
    /// doesn't report errors, so this simply does nothing.</para>
    /// </summary>
    /// <param name="track">the track from which to remove a tag.</param>
    /// <param name="tag">the tag to remove.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <seealso cref="TagTrack"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_UntagTrack"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void UntagTrack(IntPtr track, [MarshalAs(UnmanagedType.LPUTF8Str)] string tag);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_SetTrackPlaybackPosition(MIX_Track *track, Sint64 frames);</code>
    /// <summary>
    /// Seek a playing track to a new position in its input.
    /// <para>(Not to be confused with <see cref="SetTrack3DPosition"/>, which is positioning of
    /// the track in 3D space, not the playback position of its audio data.)</para>
    /// <para>On a playing track, the next time the mixer runs, it will start mixing from
    /// the new position.</para>
    /// <para>Position is defined in _sample frames_ of decoded audio, not units of time,
    /// so that sample-perfect mixing can be achieved. To instead operate in units
    /// of time, use <see cref="TrackMSToFrames"/> to get the approximate sample frames for
    /// a given tick.</para>
    /// <para>This function requires an input that can seek (so it can not be used if the
    /// input was set with <see cref="SetTrackAudioStream"/>), and a audio file format that
    /// allows seeking. SDL_mixer's decoders for some file formats do not offer
    /// seeking, or can only seek to times, not exact sample frames, in which case
    /// the final position may be off by some amount of sample frames. Please check
    /// your audio data and file bug reports if appropriate.</para>
    /// <para>It's legal to call this function on a track that is stopped, but a future
    /// call to <see cref="PlayTrack"/> will reset the start position anyhow. Paused tracks
    /// will resume at the new input position.</para>
    /// </summary>
    /// <param name="track">the track to change.</param>
    /// <param name="frames">the sample frame position to seek to.</param>
    /// <returns>true on success, false on error; call <see cref="SDL.GetError"/> for details.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <see cref="GetTrackPlaybackPosition"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_SetTrackPlaybackPosition"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetTrackPlaybackPosition(IntPtr track, long frames);
    
    
    /// <code>extern SDL_DECLSPEC Sint64 SDLCALL MIX_GetTrackPlaybackPosition(MIX_Track *track);</code>
    /// <summary>
    /// Get the current input position of a playing track.
    /// <para>(Not to be confused with <see cref="GetTrack3DPosition"/>, which is positioning of
    /// the track in 3D space, not the playback position of its audio data.)</para>
    /// <para>Position is defined in _sample frames_ of decoded audio, not units of time,
    /// so that sample-perfect mixing can be achieved. To instead operate in units
    /// of time, use <see cref="TrackFramesToMS"/> to convert the return value to
    /// milliseconds.</para>
    /// <para>Stopped and paused tracks will report the position when they halted.
    /// Playing tracks will report the current position, which will change over
    /// time.</para>
    /// </summary>
    /// <param name="track">the track to change.</param>
    /// <returns>the track's current sample frame position, or -1 on error; call
    /// <see cref="SDL.GetError"/> for details.</returns>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_GetTrackPlaybackPosition"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long GetTrackPlaybackPosition(IntPtr track);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_TrackLooping(MIX_Track *track);</code>
    /// <summary>
    /// Query whether a given track is looping.
    /// <para>This specifically checks if the track is _not stopped_ (paused or playing),
    /// and there is at least one loop remaining. If a track _was_ looping but is
    /// on its final iteration of the loop, this will return false.</para>
    /// <para>On various errors (<see cref="Init"/> was not called, the track is <c>null</c>), this
    /// returns false, but there is no mechanism to distinguish errors from
    /// non-looping tracks.</para>
    /// </summary>
    /// <param name="track">the track to query.</param>
    /// <returns>true if looping, false otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_TrackLooping"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool TrackLooping(IntPtr track);
    
    
    /// <code>extern SDL_DECLSPEC MIX_Audio * SDLCALL MIX_GetTrackAudio(MIX_Track *track);</code>
    /// <summary>
    /// Query the MIX_Audio assigned to a track.
    /// <para>This returns the MIX_Audio object currently assigned to <c>null</c> through a
    /// call to <see cref="SetTrackAudio"/>. If there is none assigned, or the track has an
    /// input that isn't a MIX_Audio (such as an SDL_AudioStream or SDL_IOStream),
    /// this will return <c>null</c>.</para>
    /// <para>On various errors (<see cref="Init"/> was not called, the track is <c>null</c>), this
    /// returns <c>null</c>, but there is no mechanism to distinguish errors from tracks
    /// without a valid input.</para>
    /// </summary>
    /// <param name="track">the track to query.</param>
    /// <returns>a MIX_Audio if available, <see cref="Init"/> if not.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="GetTrackAudioStream"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_GetTrackAudio"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetTrackAudio(IntPtr track);
    
    
    /// <code>extern SDL_DECLSPEC SDL_AudioStream * SDLCALL MIX_GetTrackAudioStream(MIX_Track *track);</code>
    /// <summary>
    /// Query the SDL_AudioStream assigned to a track.
    /// <para>This returns the SDL_AudioStream object currently assigned to <c>track</c>
    /// through a call to <see cref="SetTrackAudioStream"/>. If there is none assigned, or
    /// the track has an input that isn't an SDL_AudioStream (such as a MIX_Audio
    /// or SDL_IOStream), this will return <c>null</c>.</para>
    /// <para>On various errors (<see cref="Init"/> was not called, the track is NULL), this
    /// returns <c>null</c>, but there is no mechanism to distinguish errors from tracks
    /// without a valid input.</para>
    /// </summary>
    /// <param name="track">the track to query.</param>
    /// <returns>an SDL_AudioStream if available, <see cref="Init"/> if not.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="GetTrackAudio"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_GetTrackAudioStream"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetTrackAudioStream(IntPtr track);
    
    
    /// <code>extern SDL_DECLSPEC Sint64 SDLCALL MIX_GetTrackRemaining(MIX_Track *track);</code>
    /// <summary>
    /// Return the number of sample frames remaining to be mixed in a track.
    /// <para>If the track is playing or paused, and its total duration is known, this
    /// will report how much audio is left to mix. If the track is playing, future
    /// calls to this function will report different values.</para>
    /// <para>Remaining audio is defined in _sample frames_ of decoded audio, not units
    /// of time, so that sample-perfect mixing can be achieved. To instead operate
    /// in units of time, use <see cref="TrackFramesToMS"/> to convert the return value to
    /// milliseconds.</para>
    /// <para>This function does not take into account fade-outs or looping, just the
    /// current mixing position vs the duration of the track.</para>
    /// <para>If the duration of the track isn't known, or <c>track</c> is <c>null</c>, this function
    /// returns -1. A stopped track reports 0.</para>
    /// </summary>
    /// <param name="track">the track to query.</param>
    /// <returns>the total sample frames still to be mixed, or -1 if unknown.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_GetTrackRemaining"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long GetTrackRemaining(IntPtr track);
    
    
    /// <code>extern SDL_DECLSPEC Sint64 SDLCALL MIX_TrackMSToFrames(MIX_Track *track, Sint64 ms);</code>
    /// <summary>
    /// Convert milliseconds to sample frames for a track's current format.
    /// <para>This calculates time based on the track's current input format, which can
    /// change when its input does, and also if that input changes formats
    /// mid-stream (for example, if decoding a file that is two MP3s concatenated
    /// together).</para>
    /// <para>On various errors (<see cref="Init"/> was not called, the track is <c>null</c>), this
    /// returns -1. If the track has no input, this returns -1. If `ms` is &lt; 0,
    /// this returns -1.</para>
    /// </summary>
    /// <param name="track">the track to query.</param>
    /// <param name="ms">the milliseconds to convert to track-specific sample frames.</param>
    /// <returns>Converted number of sample frames, or -1 for errors/no input; call
    /// <see cref="SDL.GetError"/> for details.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="TrackFramesToMS"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_TrackMSToFrames"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long TrackMSToFrames(IntPtr track, long ms);
    
    
    /// <code>extern SDL_DECLSPEC Sint64 SDLCALL MIX_TrackFramesToMS(MIX_Track *track, Sint64 frames);</code>
    /// <summary>
    /// Convert sample frames for a track's current format to milliseconds.
    /// <para>This calculates time based on the track's current input format, which can
    /// change when its input does, and also if that input changes formats
    /// mid-stream (for example, if decoding a file that is two MP3s concatenated
    /// together).</para>
    /// <para>Sample frames are more precise than milliseconds, so out of necessity, this
    /// function will approximate by rounding down to the closest full millisecond.</para>
    /// <para>On various errors (<see cref="Init"/> was not called, the track is <c>null</c>), this
    /// returns -1. If the track has no input, this returns -1. If <c>frames</c> is &lt; 0,
    /// this returns -1.</para>
    /// </summary>
    /// <param name="track">the track to query.</param>
    /// <param name="frames">the track-specific sample frames to convert to milliseconds.</param>
    /// <returns>Converted number of milliseconds, or -1 for errors/no input; call
    /// <see cref="SDL.GetError"/> for details.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="TrackMSToFrames"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_TrackFramesToMS"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long TrackFramesToMS(IntPtr track, long frames);
    
    
    /// <code>extern SDL_DECLSPEC Sint64 SDLCALL MIX_AudioMSToFrames(MIX_Audio *audio, Sint64 ms);</code>
    /// <summary>
    /// Convert milliseconds to sample frames for a MIX_Audio's format.
    /// <para>This calculates time based on the audio's initial format, even if the
    /// format would change mid-stream.</para>
    /// <para>If <c>ms</c> is &lt; 0, this returns -1.</para>
    /// </summary>
    /// <param name="audio">the audio to query.</param>
    /// <param name="ms">the milliseconds to convert to audio-specific sample frames.</param>
    /// <returns>Converted number of sample frames, or -1 for errors/no input; call
    /// <see cref="SDL.GetError"/> for details.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="AudioFramesToMS"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_AudioMSToFrames"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long AudioMSToFrames(IntPtr audio, long ms);
    
    
    /// <code>extern SDL_DECLSPEC Sint64 SDLCALL MIX_AudioFramesToMS(MIX_Audio *audio, Sint64 frames);</code>
    /// <summary>
    /// Convert sample frames for a MIX_Audio's format to milliseconds.
    /// <para>This calculates time based on the audio's initial format, even if the
    /// format would change mid-stream.</para>
    /// <para>Sample frames are more precise than milliseconds, so out of necessity, this
    /// function will approximate by rounding down to the closest full millisecond.</para>
    /// <para>If <c>frames</c> is &lt; 0, this returns -1.</para>
    /// </summary>
    /// <param name="audio">the audio to query.</param>
    /// <param name="frames">the audio-specific sample frames to convert to milliseconds.</param>
    /// <returns>Converted number of milliseconds, or -1 for errors/no input; call
    /// <see cref="SDL.GetError"/> for details.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="AudioMSToFrames"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_AudioFramesToMS"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long AudioFramesToMS(IntPtr audio, long frames);
    
    
    /// <code>extern SDL_DECLSPEC Sint64 SDLCALL MIX_MSToFrames(int sample_rate, Sint64 ms);</code>
    /// <summary>
    /// Convert milliseconds to sample frames at a specific sample rate.
    /// <para>If <c>sampleRate</c> is &lt;= 0, this returns -1. If <c>ms</c> is &lt; 0, this returns -1.</para>
    /// </summary>
    /// <param name="sampleRate">the sample rate to use for conversion.</param>
    /// <param name="ms">the milliseconds to convert to rate-specific sample frames.</param>
    /// <returns>Converted number of sample frames, or -1 for errors; call
    /// <see cref="SDL.GetError"/> for details.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="FramesToMS"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_MSToFrames"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long MSToFrames(int sampleRate, long ms);
    
    
    /// <code>extern SDL_DECLSPEC Sint64 SDLCALL MIX_FramesToMS(int sample_rate, Sint64 frames);</code>
    /// <summary>
    /// Convert sample frames, at a specific sample rate, to milliseconds.
    /// <para>Sample frames are more precise than milliseconds, so out of necessity, this
    /// function will approximate by rounding down to the closest full millisecond.</para>
    /// <para>If <c>sampleRate</c> is &lt;= 0, this returns -1. If <c>frames</c> is &lt; 0, this returns
    /// -1.</para>
    /// </summary>
    /// <param name="sampleRate">the sample rate to use for conversion.</param>
    /// <param name="frames">the rate-specific sample frames to convert to milliseconds.</param>
    /// <returns>Converted number of milliseconds, or -1 for errors; call
    /// <see cref="SDL.GetError"/> for details.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="MSToFrames"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_FramesToMS"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long FramesToMS(int sampleRate, long frames);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_PlayTrack(MIX_Track *track, SDL_PropertiesID options);</code>
    /// <summary>
    /// Start (or restart) mixing a track for playback.
    /// <para>The track will use whatever input was last assigned to it when playing; an
    /// input must be assigned to this track or this function will fail. Inputs are
    /// assigned with calls to <see cref="SetTrackAudio"/>, <see cref="SetTrackAudioStream"/>, or
    /// <see cref="SetTrackIOStream"/>.</para>
    /// <para>If the track is already playing, or paused, this will restart the track
    /// with the newly-specified parameters.</para>
    /// <para>As there are several parameters, and more may be added in the future, they
    /// are specified with an SDL_PropertiesID. The parameters have reasonable
    /// defaults, and specifying a 0 for <c>options</c> will choose defaults for
    /// everything.</para>
    /// <para>These are the supported properties:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.PlayLoopsNumber"/>: The number of times to loop the track when
    /// it reaches the end. A value of 1 will loop to the start one time. Zero
    /// will not loop at all. A value of -1 requests infinite loops. If the input
    /// is not seekable and this value isn't zero, this function will report
    /// success but the track will stop at the point it should loop. Default 0.</item>
    /// <item><see cref="Props.PlayMaxFrameNumber"/>: Mix at most to this sample frame
    /// position in the track. This will be treated as if the input reach EOF at
    /// this point in the audio file. If -1, mix all available audio without a
    /// limit. Default -1.</item>
    /// <item><see cref="Props.PlayMaxMillisecondsNumber"/>: The same as using the
    /// <see cref="Props.PlayMaxFrameNumber"/> property, but the value is specified in
    /// milliseconds instead of sample frames. If both properties are specified,
    /// the sample frames value is favored. Default -1.</item>
    /// <item><see cref="Props.PlayStartFrameNumber"/>: Start mixing from this sample frame
    /// position in the track's input. A value &lt;= 0 will begin from the start of
    /// the track's input. If the input is not seekable and this value is > 0,
    /// this function will report failure. Default 0.</item>
    /// <item><see cref="Props.PlayStartMillisecondNumber"/>: The same as using the
    /// <see cref="Props.PlayMaxFrameNumber"/> property, but the value is specified in
    /// milliseconds instead of sample frames. If both properties are specified,
    /// the sample frames value is favored. Default 0.</item>
    /// <item>`<see cref="Props.PlayLoopStartFrameNumber"/>`: If the track is looping, this is
    /// the sample frame position that the track will loop back to; this lets one
    /// play an intro at the start of a track on the first iteration, but have a
    /// loop point somewhere in the middle thereafter. A value &lt;= 0 will begin
    /// the loop from the start of the track's input. Default 0.</item>
    /// <item>`MIX_PROP_PLAY_LOOP_START_MILLISECOND_NUMBER`: The same as using the
    /// <see cref="Props.PlayLoopStartFrameNumber"/> property, but the value is
    /// specified in milliseconds instead of sample frames. If both properties
    /// are specified, the sample frames value is favored. Default 0.</item>
    /// <item><see cref="Props.PlayFadeInFramesNumber"/>: The number of sample frames over
    /// which to fade in the newly-started track. The track will begin mixing
    /// silence and reach full volume smoothly over this many sample frames. If
    /// the track loops before the fade-in is complete, it will continue to fade
    /// correctly from the loop point. A value &lt;= 0 will disable fade-in, so the
    /// track starts mixing at full volume. Default 0.</item>
    /// <item><see cref="Props.PlayStartMillisecondNumber"/>: The same as using the
    /// <see cref="Props.PlayFadeInFramesNumber"/> property, but the value is specified
    /// in milliseconds instead of sample frames. If both properties are
    /// specified, the sample frames value is favored. Default 0.</item>
    /// <item><see cref="Props.PlayAppendSilenceFramesNumber"/>: At the end of mixing this
    /// track, after all loops are complete, append this many sample frames of
    /// silence as if it were part of the audio file. This allows for apps to
    /// implement effects in callbacks, like reverb, that need to generate
    /// samples past the end of the stream's audio, or perhaps introduce a delay
    /// before starting a new sound on the track without having to manage it
    /// directly. A value &lt;= 0 generates no silence before stopping the track.
    /// Default 0.</item>
    /// <item>`<see cref="Props.PlayAppendSilenceMillisecondsNumber"/>`: The same as using the
    /// <see cref="Props.PlayAppendSilenceFramesNumber"/> property, but the value is
    /// specified in milliseconds instead of sample frames. If both properties
    /// are specified, the sample frames value is favored. Default 0.</item>
    /// </list>
    /// <para>If this function fails, mixing of this track will not start (or restart, if
    /// it was already started).</para>
    /// </summary>
    /// <param name="track">the track to start (or restart) mixing.</param>
    /// <param name="options">a set of properties that control playback. May be zero.</param>
    /// <returns>true on success, false on error; call <see cref="SDL.GetError"/> for details.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="PlayTag"/>
    /// <seealso cref="PlayAudio"/>
    /// <seealso cref="StopTrack"/>
    /// <seealso cref="PauseTrack"/>
    /// <seealso cref="TrackPlaying"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_PlayTrack"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool PlayTrack(IntPtr track, uint options);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_PlayTag(MIX_Mixer *mixer, const char *tag, SDL_PropertiesID options);</code>
    /// <summary>
    /// Start (or restart) mixing all tracks with a specific tag for playback.
    /// <para>This function follows all the same rules as <see cref="PlayTrack"/>; please refer
    /// to its documentation for the details. Unlike that function, <see cref="PlayTag"/>
    /// operates on multiple tracks at once that have the specified tag applied,
    /// via <see cref="TagTrack"/>.</para>
    /// <para>If all of your tagged tracks have different sample rates, it would make
    /// sense to use the <c>*MillisecondsNumber</c> properties in your <c>options</c>,
    /// instead of <c>*FramesNumber</c>, and let SDL_mixer figure out how to apply it
    /// to each track.</para>
    /// <para>This function returns true if all tagged tracks are started (or restarted).
    /// If any track fails, this function returns false, but all tracks that could
    /// start will still be started even when this function reports failure.</para>
    /// <para>From the point of view of the mixing process, all tracks that successfully
    /// (re)start will do so at the exact same moment.</para>
    /// </summary>
    /// <param name="mixer">the mixer on which to look for tagged tracks.</param>
    /// <param name="tag">the tag to use when searching for tracks.</param>
    /// <param name="options">the set of options that will be applied to each track.</param>
    /// <returns>true on success, false on error; call <see cref="SDL.GetError"/> for details.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="PlayTrack"/>
    /// <seealso cref="TagTrack"/>
    /// <seealso cref="StopTrack"/>
    /// <seealso cref="PauseTrack"/>
    /// <seealso cref="TrackPlaying"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_PlayTag"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool PlayTag(IntPtr mixer, [MarshalAs(UnmanagedType.LPUTF8Str)] string tag, uint options);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_PlayAudio(MIX_Mixer *mixer, MIX_Audio *audio);</code>
    /// <summary>
    /// Play a MIX_Audio from start to finish without any management.
    /// <para>This is what we term a "fire-and-forget" sound. Internally, SDL_mixer will
    /// manage a temporary track to mix the specified MIX_Audio, cleaning it up
    /// when complete. No options can be provided for how to do the mixing, like
    /// <see cref="PlayTrack"/> offers, and since the track is not available to the caller,
    /// no adjustments can be made to mixing over time.</para>
    /// <para>This is not the function to build an entire game of any complexity around,
    /// but it can be convenient to play simple, one-off sounds that can't be
    /// stopped early. An example would be a voice saying "GAME OVER" during an
    /// unpausable endgame sequence.</para>
    /// <para>There are no limits to the number of fire-and-forget sounds that can mix at
    /// once (short of running out of memory), and SDL_mixer keeps an internal pool
    /// of temporary tracks it creates as needed and reuses when available.</para>
    /// </summary>
    /// <param name="mixer">the mixer on which to play this audio.</param>
    /// <param name="audio">the audio input to play.</param>
    /// <returns>true if the track has begun mixing, false on error; call
    /// <see cref="SDL.GetError"/> for details.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="PlayTrack"/>
    /// <seealso cref="LoadAudio"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_PlayAudio"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool PlayAudio(IntPtr mixer, IntPtr audio);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_StopTrack(MIX_Track *track, Sint64 fade_out_frames);</code>
    /// <summary>
    /// Halt a currently-playing track, possibly fading out over time.
    /// <para>If <c>fadeOutFrames</c> is &gt; 0, the track does not stop mixing immediately,
    /// but rather fades to silence over that many sample frames before stopping.
    /// Sample frames are specific to the input assigned to the track, to allow for
    /// sample-perfect mixing. <see cref="TrackMSToFrames"/> can be used to convert
    /// milliseconds to an appropriate value here.</para>
    /// <para>If the track ends normally while the fade-out is still in progress, the
    /// audio stops there; the fade is not adjusted to be shorter if it will last
    /// longer than the audio remaining.</para>
    /// <para>Once a track has completed any fadeout and come to a stop, it will call its
    /// <see cref="TrackStoppedCallback"/>, if any. It is legal to assign the track a new
    /// input and/or restart it during this callback.</para>
    /// <para>It is legal to halt a track that's already stopped. It does nothing, and
    /// returns true.</para>
    /// </summary>
    /// <param name="track">the track to halt.</param>
    /// <param name="fadeOutFrames">the number of sample frames to spend fading out to
    /// silence before halting. 0 to stop immediately.</param>
    /// <returns>true if the track has stopped, false on error; call <see cref="SDL.GetError"/>
    /// for details.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="PlayTrack"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_StopTrack"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool StopTrack(IntPtr track, long fadeOutFrames);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_StopAllTracks(MIX_Mixer *mixer, Sint64 fade_out_ms);</code>
    /// <summary>
    /// Halt all currently-playing tracks, possibly fading out over time.
    /// <para>If <c>fadeOutMs</c> is &gt; 0, the tracks do not stop mixing immediately, but
    /// rather fades to silence over that many milliseconds before stopping. Note
    /// that this is different than <see cref="StopTrack"/>, which wants sample frames;
    /// this function takes milliseconds because different tracks might have
    /// different sample rates.</para>
    /// <para>If a track ends normally while the fade-out is still in progress, the audio
    /// stops there; the fade is not adjusted to be shorter if it will last longer
    /// than the audio remaining.</para>
    /// <para>Once a track has completed any fadeout and come to a stop, it will call its
    /// <see cref="TrackStoppedCallback"/>, if any. It is legal to assign the track a new
    /// input and/or restart it during this callback. This function does not
    /// prevent new play requests from being made.</para>
    /// </summary>
    /// <param name="mixer">the mixer on which to stop all tracks.</param>
    /// <param name="fadeOutMs">the number of milliseconds to spend fading out to
    /// silence before halting. 0 to stop immediately.</param>
    /// <returns>true on success, false on error; call <see cref="SDL.GetError"/> for details.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="StopTrack"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_StopAllTracks"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool StopAllTracks(IntPtr mixer, long fadeOutMs);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_StopTag(MIX_Mixer *mixer, const char *tag, Sint64 fade_out_ms);</code>
    /// <summary>
    /// Halt all tracks with a specific tag, possibly fading out over time.
    /// <para>If <c>fadeOutMs</c> is > 0, the tracks do not stop mixing immediately, but
    /// rather fades to silence over that many milliseconds before stopping. Note
    /// that this is different than <see cref="StopTrack"/>, which wants sample frames;
    /// this function takes milliseconds because different tracks might have
    /// different sample rates.</para>
    /// <para>If a track ends normally while the fade-out is still in progress, the audio
    /// stops there; the fade is not adjusted to be shorter if it will last longer
    /// than the audio remaining.</para>
    /// <para>Once a track has completed any fadeout and come to a stop, it will call its
    /// <see cref="TrackStoppedCallback"/>, if any. It is legal to assign the track a new
    /// input and/or restart it during this callback. This function does not
    /// prevent new play requests from being made.</para>
    /// </summary>
    /// <param name="mixer">the mixer on which to stop tracks.</param>
    /// <param name="tag">the tag to use when searching for tracks.</param>
    /// <param name="fadeOutMs">the number of milliseconds to spend fading out to
    /// silence before halting. 0 to stop immediately.</param>
    /// <returns>true on success, false on error; call <see cref="SDL.GetError"/> for details.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="StopTrack"/>
    /// <seealso cref="TagTrack"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_StopTag"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool StopTag(IntPtr mixer, [MarshalAs(UnmanagedType.LPUTF8Str)] string tag, long fadeOutMs);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_PauseTrack(MIX_Track *track);</code>
    /// <summary>
    /// Pause a currently-playing track.
    /// <para>A paused track is not considered "stopped," so its MIX_TrackStoppedCallback
    /// will not fire if paused, but it won't change state by default, generate
    /// audio, or generally make progress, until it is resumed.</para>
    /// <para>It is legal to pause a track that's in any state (playing, already paused,
    /// or stopped). Unless the track is currently playing, pausing does nothing,
    /// and returns true. A false return is only used to signal errors here (such
    /// as <see cref="Init"/> not being called or <c>track</c> being <c>null</c>).</para>
    /// </summary>
    /// <param name="track">the track to pause.</param>
    /// <returns>true if the track has paused, false on error; call <see cref="SDL.GetError"/>
    /// for details.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="ResumeTrack"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_PauseTrack"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool PauseTrack(IntPtr track);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_PauseAllTracks(MIX_Mixer *mixer);</code>
    /// <summary>
    /// Pause all currently-playing tracks.
    /// <para>A paused track is not considered "stopped," so its <see cref="TrackStoppedCallback"/>
    /// will not fire if paused, but it won't change state by default, generate
    /// audio, or generally make progress, until it is resumed.</para>
    /// <para>This function makes all tracks on the specified mixer that are currently
    /// playing move to a paused state. They can later be resumed.</para>
    /// </summary>
    /// <param name="mixer">the mixer on which to pause all tracks.</param>
    /// <returns>true on success, false on error; call <see cref="SDL.GetError"/> for details.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="ResumeTrack"/>
    /// <seealso cref="ResumeAllTracks"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_PauseAllTracks"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool PauseAllTracks(IntPtr mixer);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_PauseTag(MIX_Mixer *mixer, const char *tag);</code>
    /// <summary>
    /// Pause all tracks with a specific tag.
    /// <para>A paused track is not considered "stopped," so its <see cref="TrackStoppedCallback"/>
    /// will not fire if paused, but it won't change state by default, generate
    /// audio, or generally make progress, until it is resumed.</para>
    /// <para>This function makes all currently-playing tracks on the specified mixer,
    /// with a specific tag, move to a paused state. They can later be resumed.</para>
    /// <para>Tracks that match the specified tag that aren't currently playing are
    /// ignored.</para>
    /// </summary>
    /// <param name="mixer">the mixer on which to pause tracks.</param>
    /// <param name="tag">the tag to use when searching for tracks.</param>
    /// <returns>true on success, false on error; call <see cref="SDL.GetError"/> for details.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="PauseTrack"/>
    /// <seealso cref="ResumeTrack"/>
    /// <seealso cref="ResumeTag"/>
    /// <seealso cref="TagTrack"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_PauseTag"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool PauseTag(IntPtr mixer, [MarshalAs(UnmanagedType.LPUTF8Str)] string tag);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_ResumeTrack(MIX_Track *track);</code>
    /// <summary>
    /// Resume a currently-paused track.
    /// <para>A paused track is not considered "stopped," so its <see cref="TrackStoppedCallback"/>
    /// will not fire if paused, but it won't change state by default, generate
    /// audio, or generally make progress, until it is resumed.</para>
    /// <para>It is legal to resume a track that's in any state (playing, paused, or
    /// stopped). Unless the track is currently paused, resuming does nothing, and
    /// returns true. A false return is only used to signal errors here (such as
    /// <see cref="Init"/> not being called or <c>track</c> being <c>null</c>).</para>
    /// </summary>
    /// <param name="track">the track to resume.</param>
    /// <returns>true if the track has resumed, false on error; call <see cref="SDL.GetError"/>
    /// for details.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="PauseTrack"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_ResumeTrack"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ResumeTrack(IntPtr track);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_ResumeAllTracks(MIX_Mixer *mixer);</code>
    /// <summary>
    /// Resume all currently-paused tracks.
    /// <para>A paused track is not considered "stopped," so its <see cref="TrackStoppedCallback"/>
    /// will not fire if paused, but it won't change state by default, generate
    /// audio, or generally make progress, until it is resumed.</para>
    /// <para>This function makes all tracks on the specified mixer that are currently
    /// paused move to a playing state.</para>
    /// </summary>
    /// <param name="mixer">the mixer on which to resume all tracks.</param>
    /// <returns>true on success, false on error; call <see cref="SDL.GetError"/> for details.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="PauseTrack"/>
    /// <seealso cref="PauseAllTracks"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_ResumeAllTracks"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ResumeAllTracks(IntPtr mixer);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_ResumeTag(MIX_Mixer *mixer, const char *tag);</code>
    /// <summary>
    /// Resume all tracks with a specific tag.
    /// <para>A paused track is not considered "stopped," so its <see cref="TrackStoppedCallback"/>
    /// will not fire if paused, but it won't change state by default, generate
    /// audio, or generally make progress, until it is resumed.</para>
    /// <para>This function makes all currently-paused tracks on the specified mixer,
    /// with a specific tag, move to a playing state.</para>
    /// <para>Tracks that match the specified tag that aren't currently paused are
    /// ignored.</para>
    /// </summary>
    /// <param name="mixer">the mixer on which to resume tracks.</param>
    /// <param name="tag">the tag to use when searching for tracks.</param>
    /// <returns>true on success, false on error; call <see cref="SDL.GetError"/> for details.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="ResumeTrack"/>
    /// <seealso cref="PauseTrack"/>
    /// <seealso cref="PauseTag"/>
    /// <seealso cref="TagTrack"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_ResumeTag"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ResumeTag(IntPtr mixer, [MarshalAs(UnmanagedType.LPUTF8Str)] string tag);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_TrackPlaying(MIX_Track *track);</code>
    /// <summary>
    /// Query if a track is currently playing.
    /// <para>If this returns true, the track is currently contributing to the mixer's
    /// output (it's "playing"). It is not stopped nor paused.</para>
    /// <para>On various errors (MIX_Init() was not called, the track is NULL), this
    /// returns false, but there is no mechanism to distinguish errors from
    /// non-playing tracks.</para>
    /// </summary>
    /// <param name="track">the track to query.</param>
    /// <returns>true if playing, false otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="PlayTrack"/>
    /// <seealso cref="PauseTrack"/>
    /// <seealso cref="ResumeTrack"/>
    /// <seealso cref="StopTrack"/>
    /// <seealso cref="TrackPaused"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_TrackPlaying"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool TrackPlaying(IntPtr track);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_TrackPaused(MIX_Track *track);</code>
    /// <summary>
    /// Query if a track is currently paused.
    /// <para>If this returns true, the track is not currently contributing to the
    /// mixer's output but will when resumed (it's "paused"). It is not playing nor
    /// stopped.</para>
    /// <para>On various errors (<see cref="Init"/> was not called, the track is <c>null</c>), this
    /// returns false, but there is no mechanism to distinguish errors from
    /// non-playing tracks.</para>
    /// </summary>
    /// <param name="track">the track to query.</param>
    /// <returns>true if paused, false otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="PlayTrack"/>
    /// <seealso cref="PauseTrack"/>
    /// <seealso cref="ResumeTrack"/>
    /// <seealso cref="StopTrack"/>
    /// <seealso cref="TrackPlaying"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_TrackPaused"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool TrackPaused(IntPtr track);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_SetMasterGain(MIX_Mixer *mixer, float gain);</code>
    /// <summary>
    /// Set a mixer's master gain control.
    /// <para>Each mixer has a master gain, to adjust the volume of the entire mix. Each
    /// sample passing through the pipeline is modulated by this gain value. A gain
    /// of zero will generate silence, 1.0f will not change the mixed volume, and
    /// larger than 1.0f will increase the volume. Negative values are illegal.
    /// There is no maximum gain specified, but this can quickly get extremely
    /// loud, so please be careful with this setting.</para>
    /// <para>A mixer's master gain defaults to 1.0f.</para>
    /// <para>This value can be changed at any time to adjust the future mix.</para>
    /// </summary>
    /// <param name="mixer">the mixer to adjust.</param>
    /// <param name="gain">the new gain value.</param>
    /// <returns>true on success or false on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="GetMasterGain"/>
    /// <seealso cref="SetTrackGain"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_SetMasterGain"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetMasterGain(IntPtr mixer, float gain);
    
    
    /// <code>extern SDL_DECLSPEC float SDLCALL MIX_GetMasterGain(MIX_Mixer *mixer);</code>
    /// <summary>
    /// Get a mixer's master gain control.
    /// <para>This returns the last value set through <see cref="SetMasterGain"/>, or 1.0f if no
    /// value has ever been explicitly set.</para>
    /// </summary>
    /// <param name="mixer">the mixer to query.</param>
    /// <returns>the mixer's current master gain.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="SetMasterGain"/>
    /// <seealso cref="GetTrackGain"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_SetMasterGain"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float GetMasterGain(IntPtr mixer);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_SetTrackGain(MIX_Track *track, float gain);</code>
    /// <summary>
    /// Set a track's gain control.
    /// <para>Each track has its own gain, to adjust its overall volume. Each sample from
    /// this track is modulated by this gain value. A gain of zero will generate
    /// silence, 1.0f will not change the mixed volume, and larger than 1.0f will
    /// increase the volume. Negative values are illegal. There is no maximum gain
    /// specified, but this can quickly get extremely loud, so please be careful
    /// with this setting.</para>
    /// <para>A track's gain defaults to 1.0f.</para>
    /// <para>This value can be changed at any time to adjust the future mix.</para>
    /// </summary>
    /// <param name="track">the track to adjust.</param>
    /// <param name="gain">the new gain value.</param>
    /// <returns>true on success or false on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="GetTrackGain"/>
    /// <seealso cref="SetMasterGain"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_SetTrackGain"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetTrackGain(IntPtr track, float gain);
    
    
    /// <code>extern SDL_DECLSPEC float SDLCALL MIX_GetTrackGain(MIX_Track *track);</code>
    /// <summary>
    /// Get a track's gain control.
    /// <para>This returns the last value set through <see cref="SetTrackGain"/>, or 1.0f if no
    /// value has ever been explicitly set.</para>
    /// </summary>
    /// <param name="track">the track to query.</param>
    /// <returns>the track's current gain.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="SetTrackGain"/>
    /// <seealso cref="GetMasterGain"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_GetTrackGain"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float GetTrackGain(IntPtr track);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_SetTagGain(MIX_Mixer *mixer, const char *tag, float gain);</code>
    /// <summary>
    /// Set the gain control of all tracks with a specific tag.
    /// <para>Each track has its own gain, to adjust its overall volume. Each sample from
    /// this track is modulated by this gain value. A gain of zero will generate
    /// silence, 1.0f will not change the mixed volume, and larger than 1.0f will
    /// increase the volume. Negative values are illegal. There is no maximum gain
    /// specified, but this can quickly get extremely loud, so please be careful
    /// with this setting.</para>
    /// <para>A track's gain defaults to 1.0f.</para>
    /// <para>This will change the gain control on tracks on the specified mixer that
    /// have the specified tag.</para>
    /// <para>From the point of view of the mixing process, all tracks that successfully
    /// change gain values will do so at the exact same moment.</para>
    /// <para>This value can be changed at any time to adjust the future mix.</para>
    /// </summary>
    /// <param name="mixer">the mixer on which to look for tagged tracks.</param>
    /// <param name="tag">the tag to use when searching for tracks.</param>
    /// <param name="gain">the new gain value.</param>
    /// <returns>true on success or false on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="GetTrackGain"/>
    /// <seealso cref="SetTrackGain"/>
    /// <seealso cref="SetMasterGain"/>
    /// <seealso cref="TagTrack"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_SetTagGain"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetTagGain(IntPtr mixer, [MarshalAs(UnmanagedType.LPUTF8Str)] string tag, float gain);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_SetTrackFrequencyRatio(MIX_Track *track, float ratio);</code>
    /// <summary>
    /// Change the frequency ratio of a track.
    /// <para>The frequency ratio is used to adjust the rate at which audio data is
    /// consumed. Changing this effectively modifies the speed and pitch of the
    /// track's audio. A value greater than 1.0f will play the audio faster, and at
    /// a higher pitch. A value less than 1.0f will play the audio slower, and at a
    /// lower pitch. 1.0f is normal speed.</para>
    /// <para>The default value is 1.0f.</para>
    /// <para>This value can be changed at any time to adjust the future mix.</para>
    /// </summary>
    /// <param name="track">the track on which to change the frequency ratio.</param>
    /// <param name="ratio">the frequency ratio. Must be between 0.01f and 100.0f.</param>
    /// <returns>true on success or false on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="GetTrackFrequencyRatio"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_SetTrackFrequencyRatio"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetTrackFrequencyRatio(IntPtr track, float ratio);
    
    
    /// <code>extern SDL_DECLSPEC float SDLCALL MIX_GetTrackFrequencyRatio(MIX_Track *track);</code>
    /// <summary>
    /// Query the frequency ratio of a track.
    /// <para>The frequency ratio is used to adjust the rate at which audio data is
    /// consumed. Changing this effectively modifies the speed and pitch of the
    /// track's audio. A value greater than 1.0f will play the audio faster, and at
    /// a higher pitch. A value less than 1.0f will play the audio slower, and at a
    /// lower pitch. 1.0f is normal speed.</para>
    /// <para>The default value is 1.0f.</para>
    /// <para>On various errors (<see cref="Init"/> was not called, the track is <c>null</c>), this
    /// returns 0.0f. Since this is not a valid value to set, this can be seen as
    /// an error state.</para>
    /// </summary>
    /// <param name="track">the track on which to query the frequency ratio.</param>
    /// <returns>the current frequency ratio, or 0.0f on failure; call
    /// <see cref="SDL.GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="GetTrackFrequencyRatio"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_GetTrackFrequencyRatio"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float GetTrackFrequencyRatio(IntPtr track);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_SetTrackOutputChannelMap(MIX_Track *track, const int *chmap, int count);</code>
    /// <summary>
    /// Set the current output channel map of a track.
    /// <para>Channel maps are optional; most things do not need them, instead passing
    /// data in the order that SDL expects.</para>
    /// <para>The output channel map reorders track data after transformations and before
    /// it is mixed into a mixer group. This can be useful for reversing stereo
    /// channels, for example.</para>
    /// <para>Each item in the array represents an input channel, and its value is the
    /// channel that it should be remapped to. To reverse a stereo signal's left
    /// and right values, you'd have an array of <c>{ 1, 0 }</c>. It is legal to remap
    /// multiple channels to the same thing, so <c>{ 1, 1 }</c> would duplicate the
    /// right channel to both channels of a stereo signal. An element in the
    /// channel map set to -1 instead of a valid channel will mute that channel,
    /// setting it to a silence value.</para>
    /// <para>You cannot change the number of channels through a channel map, just
    /// reorder/mute them.</para>
    /// <para>Tracks default to no remapping applied. Passing a <c>null</c> channel map is
    /// legal, and turns off remapping.</para>
    /// <para>SDL_mixer will copy the channel map; the caller does not have to save this
    /// array after this call.</para>
    /// </summary>
    /// <param name="track">the track to change.</param>
    /// <param name="chmap">the new channel map, <c>null</c> to reset to default.</param>
    /// <param name="count">The number of channels in the map.</param>
    /// <returns>true on success or false on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_SetTrackOutputChannelMap"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetTrackOutputChannelMap(IntPtr track, IntPtr chmap, int count);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_SetTrackStereo(MIX_Track *track, const MIX_StereoGains *gains);</code>
    /// <summary>
    /// Force a track to stereo output, with optionally left/right panning.
    /// <para>This will cause the output of the track to convert to stereo, and then mix
    /// it only onto the Front Left and Front Right speakers, regardless of the
    /// speaker configuration. The left and right channels are modulated by
    /// <c>gains</c>, which can be used to produce panning effects. This function may be
    /// called to adjust the gains at any time.</para>
    /// <para>If <c>gains</c> is not <c>null</c>, this track will be switched into forced-stereo
    /// mode. If <c>gains</c> is <c>null</c>, this will disable spatialization (both the
    /// forced-stereo mode of this function and full 3D spatialization of
    /// <see cref="SetTrack3DPosition"/>).</para>
    /// <para>Negative gains are clamped to zero; there is no clamp for maximum, so one
    /// could set the value > 1.0f to make a channel louder.</para>
    /// <para>The track's 3D position, reported by <see cref="GetTrack3DPosition"/>, will be
    /// reset to (0, 0, 0).</para>
    /// </summary>
    /// <param name="track">the track to adjust.</param>
    /// <param name="gains">the per-channel gains, or <c>null</c> to disable spatialization.</param>
    /// <returns>true on success or false on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="SetTrack3DPosition"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_SetTrackStereo"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetTrackStereo(IntPtr track, IntPtr gains);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_SetTrack3DPosition(MIX_Track *track, const MIX_Point3D *position);</code>
    /// <summary>
    /// Set a track's position in 3D space.
    /// <para>(Please note that SDL_mixer is not intended to be a extremely powerful 3D
    /// API. It lacks 3D features that other APIs like OpenAL offer: there's no
    /// doppler effect, distance models, rolloff, etc. This is meant to be Good
    /// Enough for games that can use some positional sounds and can even take
    /// advantage of surround-sound configurations.)</para>
    /// <para> If <c>position</c> is not <c>null</c>, this track will be switched into 3D positional
    /// mode. If <c>position</c> is <c>null</c>, this will disable positional mixing (both the
    /// full 3D spatialization of this function and forced-stereo mode of
    /// <see cref="SetTrackStereo"/>).</para>
    /// <para>In 3D positional mode, SDL_mixer will mix this track as if it were
    /// positioned in 3D space, including distance attenuation (quieter as it gets
    /// further from the listener) and spatialization (positioned on the correct
    /// speakers to suggest direction, either with stereo outputs or full surround
    /// sound).</para>
    /// <para>For a mono speaker output, spatialization is effectively disabled but
    /// distance attenuation will still work, which is all you can really do with a
    /// single speaker.</para>
    /// <para>The coordinate system operates like OpenGL or OpenAL: a "right-handed"
    /// coordinate system. See <see cref="Point3D"/> for the details.</para>
    /// <para>The listener is always at coordinate (0,0,0) and can't be changed.</para>
    /// <para>The track's input will be converted to mono (1 channel) so it can be
    /// rendered across the correct speakers.</para>
    /// </summary>
    /// <param name="track">the track for which to set 3D position.</param>
    /// <param name="position">the new 3D position for the track. May be <c>null</c>.</param>
    /// <returns>true on success or false on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="GetTrack3DPosition"/>
    /// <seealso cref="SetTrackStereo"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_SetTrack3DPosition"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetTrack3DPosition(IntPtr track, IntPtr position);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_GetTrack3DPosition(MIX_Track *track, MIX_Point3D *position);</code>
    /// <summary>
    /// Get a track's current position in 3D space.
    /// <para>If 3D positioning isn't enabled for this track, through a call to
    /// <see cref="SetTrack3DPosition"/>, this will return (0,0,0).</para>
    /// </summary>
    /// <param name="track">the track to query.</param>
    /// <param name="position">on successful return, will contain the track's position.</param>
    /// <returns>true on success or false on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="SetTrack3DPosition"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_GetTrack3DPosition"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetTrack3DPosition(IntPtr track, IntPtr position);
    
    
    /// <code>extern SDL_DECLSPEC MIX_Group * SDLCALL MIX_CreateGroup(MIX_Mixer *mixer);</code>
    /// <summary>
    /// <para>Create a mixing group.</para>
    /// <para>Tracks are assigned to a mixing group (or if unassigned, they live in a
    /// mixer's internal default group). All tracks in a group are mixed together
    /// and the app can access this mixed data before it is mixed with all other
    /// groups to produce the final output.</para>
    /// <para>This can be a useful feature, but is completely optional; apps can ignore
    /// mixing groups entirely and still have a full experience with SDL_mixer.</para>
    /// <para>After creating a group, assign tracks to it with <see cref="SetTrackGroup"/>. Use
    /// <see cref="SetGroupPostMixCallback"/> to access the group's mixed data.</para>
    /// <para>A mixing group can be destroyed with <see cref="DestroyGroup"/> when no longer
    /// needed. Destroying the mixer will also destroy all its still-existing
    /// mixing groups.</para>
    /// </summary>
    /// <param name="mixer">the mixer on which to create a mixing group.</param>
    /// <returns>a newly-created mixing group, or <c>null</c> on error; call
    /// <see cref="SDL.GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="DestroyGroup"/>
    /// <seealso cref="SetTrackGroup"/>
    /// <seealso cref="SetGroupPostMixCallback"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_CreateGroup"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateGroup(IntPtr mixer);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL MIX_DestroyGroup(MIX_Group *group);</code>
    /// <summary>
    /// Destroy a mixing group.
    /// <para>Any tracks currently assigned to this group will be reassigned to the
    /// mixer's internal default group.</para>
    /// </summary>
    /// <param name="group">the mixing group to destroy.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="CreateGroup"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_DestroyGroup"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyGroup(IntPtr group);
    
    
    /// <code>extern SDL_DECLSPEC SDL_PropertiesID SDLCALL MIX_GetGroupProperties(MIX_Group *group);</code>
    /// <summary>
    /// Get the properties associated with a group.
    /// <para>Currently SDL_mixer assigns no properties of its own to a group, but this
    /// can be a convenient place to store app-specific data.</para>
    /// <para>A SDL_PropertiesID is created the first time this function is called for a
    /// given group.</para>
    /// </summary>
    /// <param name="group">the group to query.</param>
    /// <returns>a valid property ID on success or 0 on failure; call
    /// <see cref="SDL.GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_GetGroupProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint GetGroupProperties(IntPtr group);
    
    
    /// <code>extern SDL_DECLSPEC MIX_Mixer * SDLCALL MIX_GetGroupMixer(MIX_Group *group);</code>
    /// <summary>
    /// Get the MIX_Mixer that owns a MIX_Group.
    /// <para>This is the mixer pointer that was passed to <see cref="CreateGroup"/>.</para>
    /// </summary>
    /// <param name="group">the group to query.</param>
    /// <returns>the mixer associated with the group, or <c>null</c> on error; call
    /// <see cref="SDL.GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_GetGroupMixer"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetGroupMixer(IntPtr group);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_SetTrackGroup(MIX_Track *track, MIX_Group *group);</code>
    /// <summary>
    /// Assign a track to a mixing group.
    /// <para>All tracks in a group are mixed together, and that output is made available
    /// to the app before it is mixed into the final output.</para>
    /// <para>Tracks can only be in one group at a time, and the track and group must
    /// have been created on the same MIX_Mixer.</para>
    /// <para>Setting a track to a <c>null</c> group will remove it from any app-created groups,
    /// and reassign it to the mixer's internal default group.</para>
    /// </summary>
    /// <param name="track">the track to set mixing group assignment.</param>
    /// <param name="group">the new mixing group to assign to. May be <c>null</c></param>
    /// <returns>true on success or false on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="CreateGroup"/>
    /// <seealso cref="SetGroupPostMixCallback"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_SetTrackGroup"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetTrackGroup(IntPtr track, IntPtr group);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_SetTrackStoppedCallback(MIX_Track *track, MIX_TrackStoppedCallback cb, void *userdata);</code>
    /// <summary>
    /// Set a callback that fires when a MIX_Track is stopped.
    /// <para>When a track completes playback, either because it ran out of data to mix
    /// (and all loops were completed as well), or it was explicitly stopped by the
    /// app, it will fire the callback specified here.</para>
    /// <para>Each track has its own unique callback.</para>
    /// <para>Passing a <c>null</c> callback here is legal; it disables this track's callback.</para>
    /// <para>Pausing a track will not fire the callback, nor will the callback fire on a
    /// playing track that is being destroyed.</para>
    /// <para>It is legal to adjust the track, including changing its input and
    /// restarting it. If this is done because it ran out of data in the middle of
    /// mixing, the mixer will start mixing the new track state in its current run
    /// without any gap in the audio.</para>
    /// </summary>
    /// <param name="track">the track to assign this callback to.</param>
    /// <param name="cb">the function to call when the track stops. May be <c>null</c>.</param>
    /// <param name="userdata">an opaque pointer provided to the callback for its own
    /// personal use.</param>
    /// <returns>true on success or false on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="TrackStoppedCallback"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_SetTrackStoppedCallback"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetTrackStoppedCallback(IntPtr track, TrackStoppedCallback cb, IntPtr userdata);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_SetTrackCookedCallback(MIX_Track *track, MIX_TrackMixCallback cb, void *userdata);</code>
    /// <summary>
    /// <para>Set a callback that fires when the mixer has transformed a track's audio.</para>
    /// <para>As a track needs to mix more data, it pulls from its input (a MIX_Audio, an
    /// SDL_AudioStream, etc). This input might be a compressed file format, like
    /// MP3, so a little more data is uncompressed from it.</para>
    /// <para>Once the track has PCM data to start operating on, and its raw callback has
    /// completed, it will begin to transform the audio: gain, fading, frequency
    /// ratio, 3D positioning, etc.</para>
    /// <para>A callback can be fired after all these transformations, but before the
    /// transformed data is mixed into other tracks. This lets an app view the data
    /// at the last moment that it is still a part of this track. It can also
    /// change the data in any way it pleases during this callback, and the mixer
    /// will continue as if this data came directly from the input.</para>
    /// <para>Each track has its own unique cooked callback.</para>
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
    /// <seealso cref="SetTrackRawCallback"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_SetTrackCookedCallback"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetTrackCookedCallback(IntPtr track, TrackStoppedCallback cb, IntPtr userdata);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_SetGroupPostMixCallback(MIX_Group *group, MIX_GroupMixCallback cb, void *userdata);</code>
    /// <summary>
    /// Set a callback that fires when a mixer group has completed mixing.
    /// <para>After all playing tracks in a mixer group have pulled in more data from
    /// their inputs, transformed it, and mixed together into a single buffer, a
    /// callback can be fired. This lets an app view the data at the last moment
    /// that it is still a part of this group. It can also change the data in any
    /// way it pleases during this callback, and the mixer will continue as if this
    /// data came directly from the group's mix buffer.</para>
    /// <para>Each group has its own unique callback. Tracks that aren't in an explicit
    /// MIX_Group are mixed in an internal grouping that is not available to the
    /// app.</para>
    /// <para>Passing a <c>null</c> callback here is legal; it disables this group's callback.</para>
    /// </summary>
    /// <param name="group">the mixing group to assign this callback to.</param>
    /// <param name="cb">the function to call when the group mixes. May be <c>null</c>.</param>
    /// <param name="userdata">an opaque pointer provided to the callback for its own
    /// personal use.</param>
    /// <returns>true on success or false on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="GroupMixCallback"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_SetGroupPostMixCallback"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetGroupPostMixCallback(IntPtr group, GroupMixCallback cb, IntPtr userdata);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_SetPostMixCallback(MIX_Mixer *mixer, MIX_PostMixCallback cb, void *userdata);</code>
    /// <summary>
    /// <para>Set a callback that fires when all mixing has completed.</para>
    /// <para>After all mixer groups have processed, their buffers are mixed together
    /// into a single buffer for the final output, at which point a callback can be
    /// fired. This lets an app view the data at the last moment before mixing
    /// completes. It can also change the data in any way it pleases during this
    /// callback, and the mixer will continue as if this data is the final output.</para>
    /// <para>Each mixer has its own unique callback.</para>
    /// <para>Passing a <c>null</c> callback here is legal; it disables this mixer's callback.</para>
    /// </summary>
    /// <param name="mixer">the mixer to assign this callback to.</param>
    /// <param name="cb">the function to call when the mixer mixes. May be <c>null</c>.</param>
    /// <param name="userdata">an opaque pointer provided to the callback for its own
    /// personal use.</param>
    /// <returns>true on success or false on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="PostMixCallback"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_SetPostMixCallback"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetPostMixCallback(IntPtr mixer, PostMixCallback cb, IntPtr userdata);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_Generate(MIX_Mixer *mixer, void *buffer, int buflen);</code>
    /// <summary>
    /// <para>Generate mixer output when not driving an audio device.</para>
    /// <para>SDL_mixer allows the creation of MIX_Mixer objects that are not connected
    /// to an audio device, by calling <see cref="CreateMixer"/> instead of
    /// <see cref="CreateMixerDevice"/>. Such mixers will not generate output until
    /// explicitly requested through this function.</para>
    /// <para>The caller may request as much audio as desired, so long as <c>buflen</c> is a
    /// multiple of the sample frame size specified when creating the mixer (for
    /// example, if requesting stereo Sint16 audio, buflen must be a multiple of 4:
    /// 2 bytes-per-channel times 2 channels).</para>
    /// <para>The mixer will mix as quickly as possible; since it works in sample frames
    /// instead of time, it can potentially generate enormous amounts of audio in a
    /// small amount of time.</para>
    /// <para>On success, this always fills <c>buffer</c> with <c>buflen</c> bytes of audio; if all
    /// playing tracks finish mixing, it will fill the remaining buffer with
    /// silence.</para>
    /// <para>Each call to this function will pick up where it left off, playing tracks
    /// will continue to mix from the point the previous call completed, etc. The
    /// mixer state can be changed between each call in any way desired: tracks can
    /// be added, played, stopped, changed, removed, etc. Effectively this function
    /// does the same thing SDL_mixer does internally when the audio device needs
    /// more audio to play.</para>
    /// <para>This function can not be used with mixers from <see cref="CreateMixerDevice"/>;
    /// those generate audio as needed internally.</para>
    /// </summary>
    /// <param name="mixer">the mixer for which to generate more audio.</param>
    /// <param name="buffer">a pointer to a buffer to store audio in.</param>
    /// <param name="buflen">the number of bytes to store in buffer.</param>
    /// <returns>true on success or false on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="CreateMixer"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_Generate"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool Generate(IntPtr mixer, IntPtr buffer, int buflen);
    
    
    /// <code>extern SDL_DECLSPEC MIX_AudioDecoder * SDLCALL MIX_CreateAudioDecoder(const char *path, SDL_PropertiesID props);</code>
    /// <summary>
    /// Create a MIX_AudioDecoder from a path on the filesystem.
    /// <para>Most apps won't need this, as SDL_mixer's usual interfaces will decode
    /// audio as needed. However, if one wants to decode an audio file into a
    /// memory buffer without playing it, this interface offers that.</para>
    /// <para>This function allows properties to be specified. This is intended to supply
    /// file-specific settings, such as where to find SoundFonts for a MIDI file,
    /// etc. In most cases, the caller should pass a zero to specify no extra
    /// properties.</para>
    /// <para>When done with the audio decoder, it can be destroyed with
    /// <see cref="DestroyAudioDecoder"/>.</para>
    /// <para>This function requires SDL_mixer to have been initialized with a successful
    /// call to <see cref="Init"/>, but does not need an actual MIX_Mixer to have been
    /// created.</para>
    /// </summary>
    /// <param name="path">the path on the filesystem from which to load data.</param>
    /// <param name="props">decoder-specific properties. May be zero.</param>
    /// <returns>an audio decoder, ready to decode.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="CreateAudioDecoderIO"/>
    /// <seealso cref="DecodeAudio"/>
    /// <seealso cref="DestroyAudioDecoder"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_CreateAudioDecoder"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateAudioDecoder([MarshalAs(UnmanagedType.LPUTF8Str)] string path, uint props);
    
    
    /// <code>extern SDL_DECLSPEC MIX_AudioDecoder * SDLCALL MIX_CreateAudioDecoder_IO(SDL_IOStream *io, bool closeio, SDL_PropertiesID props);</code>
    /// <summary>
    /// Create a MIX_AudioDecoder from an SDL_IOStream.
    /// <para>Most apps won't need this, as SDL_mixer's usual interfaces will decode
    /// audio as needed. However, if one wants to decode an audio file into a
    /// memory buffer without playing it, this interface offers that.</para>
    /// <para>This function allows properties to be specified. This is intended to supply
    /// file-specific settings, such as where to find SoundFonts for a MIDI file,
    /// etc. In most cases, the caller should pass a zero to specify no extra
    /// properties.</para>
    /// <para>If <c>closeio</c> is true, then <c>io</c> will be closed when this decoder is done
    /// with it. If this function fails and <c>closeio</c> is true, then <c>io</c> will be
    /// closed before this function returns.</para>
    /// <para>When done with the audio decoder, it can be destroyed with
    /// <see cref="DestroyAudioDecoder"/>.</para>
    /// <para>This function requires SDL_mixer to have been initialized with a successful
    /// call to <see cref="Init"/>, but does not need an actual MIX_Mixer to have been
    /// created.</para>
    /// </summary>
    /// <param name="io">the i/o stream from which to load data.</param>
    /// <param name="closeio">if true, close the i/o stream when done with it.</param>
    /// <param name="props">decoder-specific properties. May be zero.</param>
    /// <returns>an audio decoder, ready to decode.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="CreateAudioDecoderIO"/>
    /// <seealso cref="DecodeAudio"/>
    /// <seealso cref="DestroyAudioDecoder"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_CreateAudioDecoder_IO"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateAudioDecoderIO(IntPtr io, [MarshalAs(UnmanagedType.I1)] bool closeio, uint props);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL MIX_DestroyAudioDecoder(MIX_AudioDecoder *audiodecoder);</code>
    /// <summary>
    /// Destroy the specified audio decoder.
    /// <para>Destroying a <c>null</c> MIX_AudioDecoder is a legal no-op.</para>
    /// </summary>
    /// <param name="audiodecoder">the audio to destroy.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_DestroyAudioDecoder"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyAudioDecoder(IntPtr audiodecoder);
    
    
    /// <code>extern SDL_DECLSPEC SDL_PropertiesID SDLCALL MIX_GetAudioDecoderProperties(MIX_AudioDecoder *audiodecoder);</code>
    /// <summary>
    /// <para>Get the properties associated with a MIX_AudioDecoder.</para>
    /// <para>SDL_mixer offers some properties of its own, but this can also be a
    /// convenient place to store app-specific data.</para>
    /// <para>A SDL_PropertiesID is created the first time this function is called for a
    /// given MIX_AudioDecoder, if necessary.</para>
    /// <para>The file-specific metadata exposed through this function is identical to
    /// those available through <see cref="GetAudioProperties"/>. Please refer to that
    /// function's documentation for details.</para>
    /// </summary>
    /// <param name="audiodecoder">the audio decoder to query.</param>
    /// <returns>a valid property ID on success or 0 on failure; call
    /// <see cref="SDL.GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    /// <seealso cref="GetAudioProperties"/>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_GetAudioDecoderProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint GetAudioDecoderProperties(IntPtr audiodecoder);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL MIX_GetAudioDecoderFormat(MIX_AudioDecoder *audiodecoder, SDL_AudioSpec *spec);</code>
    /// <summary>
    /// Query the initial audio format of a MIX_AudioDecoder.
    /// <para>Note that some audio files can change format in the middle; some explicitly
    /// support this, but a more common example is two MP3 files concatenated
    /// together. In many cases, SDL_mixer will correctly handle these sort of
    /// files, but this function will only report the initial format a file uses.</para>
    /// </summary>
    /// <param name="audiodecoder">the audio decoder to query.</param>
    /// <param name="spec">on success, audio format details will be stored here.</param>
    /// <returns>true on success or false on failure; call <see cref="SDL.GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0.</since>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_GetAudioDecoderFormat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetAudioDecoderFormat(IntPtr audiodecoder, IntPtr spec);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL MIX_DecodeAudio(MIX_AudioDecoder *audiodecoder, void *buffer, int buflen, const SDL_AudioSpec *spec);</code>
    /// <summary>
    /// Decode more audio from a MIX_AudioDecoder.
    /// <para>Data is decoded on demand in whatever format is requested. The format is
    /// permitted to change between calls.</para>
    /// <para>This function will return the number of bytes decoded, which may be less
    /// than requested if there was an error or end-of-file. A return value of zero
    /// means the entire file was decoded, -1 means an unrecoverable error
    /// happened.</para>
    /// </summary>
    /// <param name="audiodecoder">the decoder from which to retrieve more data.</param>
    /// <param name="buffer">the memory buffer to store decoded audio.</param>
    /// <param name="buflen">the maximum number of bytes to store to <c>buffer</c>.</param>
    /// <param name="spec">the format that audio data will be stored to <c>buffer</c>.</param>
    /// <returns>number of bytes decoded, or -1 on error; call <see cref="SDL.GetError"/> for
    /// more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL_mixer 3.0.0</since>
    [LibraryImport(MixerLibrary, EntryPoint = "MIX_DecodeAudio"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int DecodeAudio(IntPtr audiodecoder, IntPtr buffer, int buflen, IntPtr spec);
}