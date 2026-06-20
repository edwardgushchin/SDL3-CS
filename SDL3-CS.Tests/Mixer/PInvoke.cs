using System.Reflection;
using System.Buffers.Binary;
using System.Runtime.InteropServices;
using System.Text;

namespace SDL3.Tests.Mixer;

internal static class PInvokeTests
{
    public static void Version_ReturnsLinkedSdlMixerVersion()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.Version), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "Mixer.Version method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_Version");

        int version = SDL3.Mixer.Version();
        int major = version / 1_000_000;
        int minor = version / 1_000 % 1_000;
        int patch = version % 1_000;

        TestAssert.Equal(3, major, "Mixer.Version must return an SDL_mixer 3.x version.");
        TestAssert.True(minor >= 0, "Mixer.Version minor component must be non-negative.");
        TestAssert.True(patch >= 0, "Mixer.Version patch component must be non-negative.");
    }

    public static void Init_ReturnsTrueAndCanBeBalancedWithQuit()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.Init), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "Mixer.Init method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_Init");

        bool initialized = SDL3.Mixer.Init();
        TestAssert.True(initialized, "Mixer.Init() must initialize SDL_mixer successfully.");
        SDL3.Mixer.Quit();
    }

    public static void Quit_AllowsUninitializedAndBalancesInit()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.Quit), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "Mixer.Quit method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_Quit");

        SDL3.Mixer.Quit();
        TestAssert.True(SDL3.Mixer.Init(), "Mixer.Init() must succeed before the balanced Quit call.");
        SDL3.Mixer.Quit();
    }

    public static void GetNumAudioDecoders_ReturnsNonNegativeAfterInit()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.GetNumAudioDecoders), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "Mixer.GetNumAudioDecoders method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_GetNumAudioDecoders");

        using MixerInitScope _ = MixerInitScope.Create();
        int count = SDL3.Mixer.GetNumAudioDecoders();
        TestAssert.True(count >= 0, "Mixer.GetNumAudioDecoders() must return a non-negative decoder count after Mixer.Init().");
    }

    public static void MIX_GetAudioDecoder_ReturnsNullForInvalidIndex()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod("MIX_GetAudioDecoder", BindingFlags.NonPublic | BindingFlags.Static, [typeof(int)]);
        TestAssert.NotNull(method, "Mixer.MIX_GetAudioDecoder(int) method must be private static.");
        AssertMixerLibraryImport(method!, "MIX_GetAudioDecoder");

        using MixerInitScope _ = MixerInitScope.Create();
        IntPtr decoder = (IntPtr)method!.Invoke(null, [-1])!;
        TestAssert.Equal(IntPtr.Zero, decoder, "Mixer.MIX_GetAudioDecoder(int) must return IntPtr.Zero for an invalid index.");
    }

    public static void GetAudioDecoder_ReturnsNullForInvalidIndex()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.GetAudioDecoder), BindingFlags.Public | BindingFlags.Static, [typeof(int)]);
        TestAssert.NotNull(method, "Mixer.GetAudioDecoder(int) method must be public static.");

        using MixerInitScope _ = MixerInitScope.Create();
        string? decoder = SDL3.Mixer.GetAudioDecoder(-1);
        TestAssert.Equal<string?>(null, decoder, "Mixer.GetAudioDecoder(int) must return null for an invalid index.");
    }

    public static void CreateMixerDevice_ReturnsNullWhenMixerIsNotInitialized()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.CreateMixerDevice), BindingFlags.Public | BindingFlags.Static, [typeof(uint), typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.CreateMixerDevice(uint, IntPtr) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_CreateMixerDevice");

        SDL3.Mixer.Quit();
        IntPtr mixer = SDL3.Mixer.CreateMixerDevice(0, IntPtr.Zero);
        TestAssert.Equal(IntPtr.Zero, mixer, "Mixer.CreateMixerDevice(uint, IntPtr) must fail safely when SDL_mixer is not initialized.");
    }

    public static void CreateMixer_ReturnsMixerForMemorySpec()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.CreateMixer), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.CreateMixer(IntPtr) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_CreateMixer");

        using MixerInitScope _ = MixerInitScope.Create();
        using AudioSpecScope spec = AudioSpecScope.Create();
        IntPtr mixer = SDL3.Mixer.CreateMixer(spec.Handle);

        try
        {
            TestAssert.True(mixer != IntPtr.Zero, "Mixer.CreateMixer(IntPtr) must create a memory mixer for a valid AudioSpec.");
        }
        finally
        {
            DestroyMixerIfNeeded(mixer);
        }
    }

    public static void DestroyMixer_DestroysMemoryMixer()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.DestroyMixer), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.DestroyMixer(IntPtr) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_DestroyMixer");

        using MixerInitScope _ = MixerInitScope.Create();
        using AudioSpecScope spec = AudioSpecScope.Create();
        IntPtr mixer = SDL3.Mixer.CreateMixer(spec.Handle);
        TestAssert.True(mixer != IntPtr.Zero, "Mixer.CreateMixer(IntPtr) must create a mixer for the DestroyMixer test.");

        SDL3.Mixer.DestroyMixer(mixer);
    }

    public static void GetMixerProperties_ReturnsPropertiesForMemoryMixer()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.GetMixerProperties), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.GetMixerProperties(IntPtr) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_GetMixerProperties");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        uint properties = SDL3.Mixer.GetMixerProperties(mixer.Handle);
        TestAssert.True(properties != 0, "Mixer.GetMixerProperties(IntPtr) must return a properties ID for a valid memory mixer.");
    }

    public static void GetMixerFormat_ReturnsFormatForMemoryMixer()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.GetMixerFormat), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.GetMixerFormat(IntPtr, IntPtr) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_GetMixerFormat");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using AudioSpecScope spec = AudioSpecScope.CreateZeroed();
        bool result = SDL3.Mixer.GetMixerFormat(mixer.Handle, spec.Handle);
        SDL3.SDL.AudioSpec value = spec.Read();

        TestAssert.True(result, "Mixer.GetMixerFormat(IntPtr, IntPtr) must succeed for a valid memory mixer.");
        TestAssert.Equal(SDL3.SDL.AudioFormat.AudioF32LE, value.Format, "Mixer.GetMixerFormat(IntPtr, IntPtr) must report the requested memory mixer format.");
        TestAssert.Equal(2, value.Channels, "Mixer.GetMixerFormat(IntPtr, IntPtr) must report the requested channel count.");
        TestAssert.Equal(48_000, value.Freq, "Mixer.GetMixerFormat(IntPtr, IntPtr) must report the requested frequency.");
    }

    public static void LockMixer_AndUnlockMixer_AcceptValidAndNullMixer()
    {
        MethodInfo? lockMethod = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.LockMixer), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(lockMethod, "Mixer.LockMixer(IntPtr) method must be public static.");
        AssertMixerLibraryImport(lockMethod!, "MIX_LockMixer");

        MethodInfo? unlockMethod = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.UnlockMixer), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(unlockMethod, "Mixer.UnlockMixer(IntPtr) method must be public static.");
        AssertMixerLibraryImport(unlockMethod!, "MIX_UnlockMixer");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        SDL3.Mixer.LockMixer(mixer.Handle);
        SDL3.Mixer.UnlockMixer(mixer.Handle);
        SDL3.Mixer.LockMixer(IntPtr.Zero);
        SDL3.Mixer.UnlockMixer(IntPtr.Zero);
    }

    public static void LoadAudioIO_ReturnsAudioForWavStream()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.LoadAudioIO), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(IntPtr), typeof(bool), typeof(bool)]);
        TestAssert.NotNull(method, "Mixer.LoadAudioIO(IntPtr, IntPtr, bool, bool) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_LoadAudio_IO");
        AssertBooleanParameterMarshal(method!, "predecode");
        AssertBooleanParameterMarshal(method!, "closeio");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using ConstMemIOStreamScope stream = ConstMemIOStreamScope.Create(CreateSilentWavData());
        IntPtr audio = SDL3.Mixer.LoadAudioIO(mixer.Handle, stream.Handle, predecode: false, closeio: false);

        try
        {
            TestAssert.True(audio != IntPtr.Zero, "Mixer.LoadAudioIO(IntPtr, IntPtr, bool, bool) must load a valid WAV stream.");
        }
        finally
        {
            DestroyAudioIfNeeded(audio);
        }
    }

    public static void LoadAudio_ReturnsAudioForWavFile()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.LoadAudio), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(string), typeof(bool)]);
        TestAssert.NotNull(method, "Mixer.LoadAudio(IntPtr, string, bool) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_LoadAudio");
        AssertStringParameterMarshal(method!, "path");
        AssertBooleanParameterMarshal(method!, "predecode");

        string path = CreateTempAudioPath(".wav");
        File.WriteAllBytes(path, CreateSilentWavData());

        try
        {
            using MixerScope mixer = MixerScope.CreateMemoryMixer();
            IntPtr audio = SDL3.Mixer.LoadAudio(mixer.Handle, path, predecode: false);

            try
            {
                TestAssert.True(audio != IntPtr.Zero, "Mixer.LoadAudio(IntPtr, string, bool) must load a valid WAV file.");
            }
            finally
            {
                DestroyAudioIfNeeded(audio);
            }
        }
        finally
        {
            File.Delete(path);
        }
    }

    public static void LoadAudioWithProperties_ReturnsAudioForWavStreamProperties()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.LoadAudioWithProperties), BindingFlags.Public | BindingFlags.Static, [typeof(uint)]);
        TestAssert.NotNull(method, "Mixer.LoadAudioWithProperties(uint) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_LoadAudioWithProperties");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using ConstMemIOStreamScope stream = ConstMemIOStreamScope.Create(CreateSilentWavData());
        using PropertiesScope properties = PropertiesScope.Create();
        TestAssert.True(SDL3.SDL.SetPointerProperty(properties.Handle, SDL3.Mixer.Props.AudioLoadIOStreamPointer, stream.Handle), "SDL.SetPointerProperty must set the LoadAudio IOStream property.");
        TestAssert.True(SDL3.SDL.SetBooleanProperty(properties.Handle, SDL3.Mixer.Props.AudioLoadCloseIOBoolean, false), "SDL.SetBooleanProperty must keep LoadAudio IO ownership with the test.");
        TestAssert.True(SDL3.SDL.SetBooleanProperty(properties.Handle, SDL3.Mixer.Props.AudioLoadPreDecodeBoolean, false), "SDL.SetBooleanProperty must set LoadAudio predecode to false.");
        TestAssert.True(SDL3.SDL.SetPointerProperty(properties.Handle, SDL3.Mixer.Props.AudioLoadPreferredMixerPointer, mixer.Handle), "SDL.SetPointerProperty must set the preferred mixer property.");
        IntPtr audio = SDL3.Mixer.LoadAudioWithProperties(properties.Handle);

        try
        {
            TestAssert.True(audio != IntPtr.Zero, "Mixer.LoadAudioWithProperties(uint) must load a valid WAV stream from properties.");
        }
        finally
        {
            DestroyAudioIfNeeded(audio);
        }
    }

    public static void Props_MatchSdlMixer324PropertyNames()
    {
        TestAssert.Equal("SDL_mixer.audio.load.iostream", SDL3.Mixer.Props.AudioLoadIOStreamPointer, "Mixer.Props.AudioLoadIOStreamPointer must match MIX_PROP_AUDIO_LOAD_IOSTREAM_POINTER.");
        TestAssert.Equal("SDL_mixer.audio.load.closeio", SDL3.Mixer.Props.AudioLoadCloseIOBoolean, "Mixer.Props.AudioLoadCloseIOBoolean must match MIX_PROP_AUDIO_LOAD_CLOSEIO_BOOLEAN.");
        TestAssert.Equal("SDL_mixer.audio.load.predecode", SDL3.Mixer.Props.AudioLoadPreDecodeBoolean, "Mixer.Props.AudioLoadPreDecodeBoolean must match MIX_PROP_AUDIO_LOAD_PREDECODE_BOOLEAN.");
        TestAssert.Equal("SDL_mixer.audio.load.preferred_mixer", SDL3.Mixer.Props.AudioLoadPreferredMixerPointer, "Mixer.Props.AudioLoadPreferredMixerPointer must match MIX_PROP_AUDIO_LOAD_PREFERRED_MIXER_POINTER.");
        TestAssert.Equal("SDL_mixer.audio.load.skip_metadata_tags", SDL3.Mixer.Props.AudioLoadSkipMetadataTagsBoolean, "Mixer.Props.AudioLoadSkipMetadataTagsBoolean must match MIX_PROP_AUDIO_LOAD_SKIP_METADATA_TAGS_BOOLEAN.");
        TestAssert.Equal("SDL_mixer.audio.load.ignore_loops", SDL3.Mixer.Props.AudioLoadIgnoreLoopsBoolean, "Mixer.Props.AudioLoadIgnoreLoopsBoolean must match MIX_PROP_AUDIO_LOAD_IGNORE_LOOPS_BOOLEAN.");
        TestAssert.Equal("SDL_mixer.audio.decoder", SDL3.Mixer.Props.AudioDecoderString, "Mixer.Props.AudioDecoderString must match MIX_PROP_AUDIO_DECODER_STRING.");

        TestAssert.Equal("SDL_mixer.play.loop_start_frame", SDL3.Mixer.Props.PlayLoopStartFrameNumber, "Mixer.Props.PlayLoopStartFrameNumber must match MIX_PROP_PLAY_LOOP_START_FRAME_NUMBER.");
        TestAssert.Equal("SDL_mixer.play.loop_start_millisecond", SDL3.Mixer.Props.PlayLoopStartMillisecondNumber, "Mixer.Props.PlayLoopStartMillisecondNumber must match MIX_PROP_PLAY_LOOP_START_MILLISECOND_NUMBER.");
        TestAssert.Equal("SDL_mixer.play.halt_when_exhausted", SDL3.Mixer.Props.PlayHaltWhenExhaustedBoolean, "Mixer.Props.PlayHaltWhenExhaustedBoolean must match MIX_PROP_PLAY_HALT_WHEN_EXHAUSTED_BOOLEAN.");
    }

    public static void LoadAudioNoCopy_ReturnsAudioForPinnedWavData()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.LoadAudioNoCopy), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(IntPtr), typeof(nuint), typeof(bool)]);
        TestAssert.NotNull(method, "Mixer.LoadAudioNoCopy(IntPtr, IntPtr, nuint, bool) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_LoadAudioNoCopy");
        AssertBooleanParameterMarshal(method!, "freeWhenDone");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using PinnedBytesScope data = PinnedBytesScope.Create(CreateSilentWavData());
        IntPtr audio = SDL3.Mixer.LoadAudioNoCopy(mixer.Handle, data.Handle, (nuint)data.Length, freeWhenDone: false);

        try
        {
            TestAssert.True(audio != IntPtr.Zero, "Mixer.LoadAudioNoCopy(IntPtr, IntPtr, nuint, bool) must load pinned WAV data when the test retains ownership.");
        }
        finally
        {
            DestroyAudioIfNeeded(audio);
        }
    }

    public static void LoadRawAudioIO_ReturnsNullForNullInputs()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.LoadRawAudioIO), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(IntPtr), typeof(IntPtr).MakeByRefType(), typeof(bool)]);
        TestAssert.NotNull(method, "Mixer.LoadRawAudioIO(IntPtr, IntPtr, in IntPtr, bool) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_LoadRawAudio_IO");
        AssertByRefIntPtrParameter(method!, "spec");
        AssertBooleanParameterMarshal(method!, "closeio");

        IntPtr spec = IntPtr.Zero;
        IntPtr audio = SDL3.Mixer.LoadRawAudioIO(IntPtr.Zero, IntPtr.Zero, in spec, closeio: false);
        TestAssert.Equal(IntPtr.Zero, audio, "Mixer.LoadRawAudioIO(IntPtr, IntPtr, in IntPtr, bool) must fail safely for null inputs.");
    }

    public static void LoadRawAudio_ReturnsNullForNullData()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.LoadRawAudio), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(IntPtr), typeof(UIntPtr), typeof(IntPtr).MakeByRefType()]);
        TestAssert.NotNull(method, "Mixer.LoadRawAudio(IntPtr, IntPtr, UIntPtr, in IntPtr) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_LoadRawAudio");
        AssertByRefIntPtrParameter(method!, "spec");

        IntPtr spec = IntPtr.Zero;
        IntPtr audio = SDL3.Mixer.LoadRawAudio(IntPtr.Zero, IntPtr.Zero, UIntPtr.Zero, in spec);
        TestAssert.Equal(IntPtr.Zero, audio, "Mixer.LoadRawAudio(IntPtr, IntPtr, UIntPtr, in IntPtr) must fail safely for null data.");
    }

    public static void LoadRawAudioNoCopy_ReturnsNullForNullData()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.LoadRawAudioNoCopy), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(IntPtr), typeof(UIntPtr), typeof(IntPtr).MakeByRefType(), typeof(bool)]);
        TestAssert.NotNull(method, "Mixer.LoadRawAudioNoCopy(IntPtr, IntPtr, UIntPtr, in IntPtr, bool) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_LoadRawAudioNoCopy");
        AssertByRefIntPtrParameter(method!, "spec");
        AssertBooleanParameterMarshal(method!, "freeWhenDone");

        IntPtr spec = IntPtr.Zero;
        IntPtr audio = SDL3.Mixer.LoadRawAudioNoCopy(IntPtr.Zero, IntPtr.Zero, UIntPtr.Zero, in spec, freeWhenDone: false);
        TestAssert.Equal(IntPtr.Zero, audio, "Mixer.LoadRawAudioNoCopy(IntPtr, IntPtr, UIntPtr, in IntPtr, bool) must fail safely for null data.");
    }

    public static void CreateSineWaveAudio_ReturnsAudioForMemoryMixer()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.CreateSineWaveAudio), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(int), typeof(float)]);
        TestAssert.NotNull(method, "Mixer.CreateSineWaveAudio(IntPtr, int, float) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_CreateSineWaveAudio");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        IntPtr audio = SDL3.Mixer.CreateSineWaveAudio(mixer.Handle, 440, 0.125f);

        try
        {
            TestAssert.True(audio != IntPtr.Zero, "Mixer.CreateSineWaveAudio(IntPtr, int, float) must create audio for a valid memory mixer.");
        }
        finally
        {
            DestroyAudioIfNeeded(audio);
        }
    }

    public static void GetAudioProperties_ReturnsPropertiesForLoadedAudio()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.GetAudioProperties), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.GetAudioProperties(IntPtr) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_GetAudioProperties");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using AudioScope audio = AudioScope.LoadWav(mixer.Handle);
        uint properties = SDL3.Mixer.GetAudioProperties(audio.Handle);
        TestAssert.True(properties != 0, "Mixer.GetAudioProperties(IntPtr) must return a properties ID for a valid audio object.");
    }

    public static void GetAudioDuration_ReturnsKnownDurationForLoadedWav()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.GetAudioDuration), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.GetAudioDuration(IntPtr) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_GetAudioDuration");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using AudioScope audio = AudioScope.LoadWav(mixer.Handle);
        long duration = SDL3.Mixer.GetAudioDuration(audio.Handle);
        TestAssert.True(duration >= 0, "Mixer.GetAudioDuration(IntPtr) must return a known non-negative frame count for the WAV fixture.");
    }

    public static void GetAudioFormat_ReturnsFormatForLoadedWav()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.GetAudioFormat), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.GetAudioFormat(IntPtr, IntPtr) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_GetAudioFormat");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using AudioScope audio = AudioScope.LoadWav(mixer.Handle);
        using AudioSpecScope spec = AudioSpecScope.CreateZeroed();
        bool result = SDL3.Mixer.GetAudioFormat(audio.Handle, spec.Handle);
        SDL3.SDL.AudioSpec value = spec.Read();

        TestAssert.True(result, "Mixer.GetAudioFormat(IntPtr, IntPtr) must succeed for a valid audio object.");
        TestAssert.Equal(SDL3.SDL.AudioFormat.AudioS16LE, value.Format, "Mixer.GetAudioFormat(IntPtr, IntPtr) must report the WAV fixture sample format.");
        TestAssert.Equal(2, value.Channels, "Mixer.GetAudioFormat(IntPtr, IntPtr) must report the WAV fixture channel count.");
        TestAssert.Equal(48_000, value.Freq, "Mixer.GetAudioFormat(IntPtr, IntPtr) must report the WAV fixture frequency.");
    }

    public static void DestroyAudio_DestroysLoadedAudioAndAllowsNull()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.DestroyAudio), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.DestroyAudio(IntPtr) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_DestroyAudio");

        SDL3.Mixer.DestroyAudio(IntPtr.Zero);
        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        IntPtr audio = AudioScope.LoadWavHandle(mixer.Handle);
        TestAssert.True(audio != IntPtr.Zero, "AudioScope.LoadWavHandle must create an audio object for DestroyAudio.");
        SDL3.Mixer.DestroyAudio(audio);
    }

    public static void CreateTrack_ReturnsTrackForMemoryMixer()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.CreateTrack), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.CreateTrack(IntPtr) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_CreateTrack");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        IntPtr track = SDL3.Mixer.CreateTrack(mixer.Handle);

        try
        {
            TestAssert.True(track != IntPtr.Zero, "Mixer.CreateTrack(IntPtr) must create a track for a valid memory mixer.");
        }
        finally
        {
            DestroyTrackIfNeeded(track);
        }
    }

    public static void DestroyTrack_DestroysTrackAndAllowsNull()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.DestroyTrack), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.DestroyTrack(IntPtr) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_DestroyTrack");

        SDL3.Mixer.DestroyTrack(IntPtr.Zero);
        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        IntPtr track = SDL3.Mixer.CreateTrack(mixer.Handle);
        TestAssert.True(track != IntPtr.Zero, "Mixer.CreateTrack(IntPtr) must create a track for DestroyTrack.");
        SDL3.Mixer.DestroyTrack(track);
    }

    public static void GetTrackProperties_ReturnsPropertiesForTrack()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.GetTrackProperties), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.GetTrackProperties(IntPtr) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_GetTrackProperties");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        uint properties = SDL3.Mixer.GetTrackProperties(track.Handle);
        TestAssert.True(properties != 0, "Mixer.GetTrackProperties(IntPtr) must return a properties ID for a valid track.");
    }

    public static void GetTrackMixer_ReturnsOwningMixer()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.GetTrackMixer), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.GetTrackMixer(IntPtr) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_GetTrackMixer");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        IntPtr owner = SDL3.Mixer.GetTrackMixer(track.Handle);
        TestAssert.Equal(mixer.Handle, owner, "Mixer.GetTrackMixer(IntPtr) must return the mixer used to create the track.");
    }

    public static void SetTrackAudio_AssignsAndClearsAudio()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.SetTrackAudio), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.SetTrackAudio(IntPtr, IntPtr) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_SetTrackAudio");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        using AudioScope audio = AudioScope.LoadWav(mixer.Handle);
        TestAssert.True(SDL3.Mixer.SetTrackAudio(track.Handle, audio.Handle), "Mixer.SetTrackAudio(IntPtr, IntPtr) must assign a valid audio object.");
        TestAssert.Equal(audio.Handle, SDL3.Mixer.GetTrackAudio(track.Handle), "Mixer.GetTrackAudio(IntPtr) must return the assigned audio object.");
        TestAssert.True(SDL3.Mixer.SetTrackAudio(track.Handle, IntPtr.Zero), "Mixer.SetTrackAudio(IntPtr, IntPtr.Zero) must clear the track input.");
    }

    public static void SetTrackAudioStream_AssignsAndClearsStream()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.SetTrackAudioStream), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.SetTrackAudioStream(IntPtr, IntPtr) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_SetTrackAudioStream");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        using AudioStreamScope stream = AudioStreamScope.Create();
        TestAssert.True(SDL3.Mixer.SetTrackAudioStream(track.Handle, stream.Handle), "Mixer.SetTrackAudioStream(IntPtr, IntPtr) must assign a valid audio stream.");
        TestAssert.Equal(stream.Handle, SDL3.Mixer.GetTrackAudioStream(track.Handle), "Mixer.GetTrackAudioStream(IntPtr) must return the assigned audio stream.");
        TestAssert.True(SDL3.Mixer.SetTrackAudioStream(track.Handle, IntPtr.Zero), "Mixer.SetTrackAudioStream(IntPtr, IntPtr.Zero) must clear the track input.");
    }

    public static void SetTrackIOStream_AssignsAndClearsWavStream()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.SetTrackIOStream), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(IntPtr), typeof(bool)]);
        TestAssert.NotNull(method, "Mixer.SetTrackIOStream(IntPtr, IntPtr, bool) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_SetTrackIOStream");
        AssertBooleanParameterMarshal(method!, "closeio");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        using ConstMemIOStreamScope stream = ConstMemIOStreamScope.Create(CreateSilentWavData());
        TestAssert.True(SDL3.Mixer.SetTrackIOStream(track.Handle, stream.Handle, closeio: false), "Mixer.SetTrackIOStream(IntPtr, IntPtr, bool) must assign a seekable WAV stream.");
        TestAssert.True(SDL3.Mixer.SetTrackIOStream(track.Handle, IntPtr.Zero, closeio: false), "Mixer.SetTrackIOStream(IntPtr, IntPtr.Zero, bool) must clear the track input.");
    }

    public static void SetTrackRawIOStream_ReturnsFalseForNullInputs()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.SetTrackRawIOStream), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(IntPtr), typeof(IntPtr).MakeByRefType(), typeof(bool)]);
        TestAssert.NotNull(method, "Mixer.SetTrackRawIOStream(IntPtr, IntPtr, in IntPtr, bool) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_SetTrackRawIOStream");
        AssertByRefIntPtrParameter(method!, "spec");
        AssertBooleanParameterMarshal(method!, "closeio");

        IntPtr spec = IntPtr.Zero;
        bool result = SDL3.Mixer.SetTrackRawIOStream(IntPtr.Zero, IntPtr.Zero, in spec, closeio: false);
        TestAssert.Equal(false, result, "Mixer.SetTrackRawIOStream(IntPtr, IntPtr, in IntPtr, bool) must fail safely for null inputs.");
    }

    public static void TagTrack_AddsUtf8Tag()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.TagTrack), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(string)]);
        TestAssert.NotNull(method, "Mixer.TagTrack(IntPtr, string) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_TagTrack");
        AssertStringParameterMarshal(method!, "tag");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        TestAssert.True(SDL3.Mixer.TagTrack(track.Handle, "codex-track"), "Mixer.TagTrack(IntPtr, string) must add a UTF-8 tag to a valid track.");
    }

    public static void UntagTrack_RemovesTagAndAllowsMissingTag()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.UntagTrack), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(string)]);
        TestAssert.NotNull(method, "Mixer.UntagTrack(IntPtr, string) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_UntagTrack");
        AssertStringParameterMarshal(method!, "tag");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        TestAssert.True(SDL3.Mixer.TagTrack(track.Handle, "codex-track"), "Mixer.TagTrack must add a tag before UntagTrack.");
        SDL3.Mixer.UntagTrack(track.Handle, "codex-track");
        SDL3.Mixer.UntagTrack(track.Handle, "missing-track");
    }

    public static void MIX_GetTrackTags_ReturnsNativeTagArray()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod("MIX_GetTrackTags", BindingFlags.NonPublic | BindingFlags.Static, [typeof(IntPtr), typeof(int).MakeByRefType()]);
        TestAssert.NotNull(method, "Mixer.MIX_GetTrackTags(IntPtr, out int) method must be private static.");
        AssertMixerLibraryImport(method!, "MIX_GetTrackTags");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        TestAssert.True(SDL3.Mixer.TagTrack(track.Handle, "codex-track"), "Mixer.TagTrack must add a tag before MIX_GetTrackTags.");
        object?[] args = [track.Handle, 0];
        IntPtr tags = (IntPtr)method!.Invoke(null, args)!;

        try
        {
            TestAssert.True(tags != IntPtr.Zero, "Mixer.MIX_GetTrackTags(IntPtr, out int) must return a native tag array for a tagged track.");
            TestAssert.Equal(1, (int)args[1]!, "Mixer.MIX_GetTrackTags(IntPtr, out int) must report the tag count.");
        }
        finally
        {
            SDL3.SDL.Free(tags);
        }
    }

    public static void GetTrackTags_ReturnsManagedTagsAndCount()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.GetTrackTags), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(int).MakeByRefType()]);
        TestAssert.NotNull(method, "Mixer.GetTrackTags(IntPtr, out int) method must be public static.");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        TestAssert.True(SDL3.Mixer.TagTrack(track.Handle, "codex-track"), "Mixer.TagTrack must add a tag before GetTrackTags.");
        string[]? tags = SDL3.Mixer.GetTrackTags(track.Handle, out int count);

        TestAssert.NotNull(tags, "Mixer.GetTrackTags(IntPtr, out int) must return managed tags for a tagged track.");
        TestAssert.Equal(count, tags!.Length, "Mixer.GetTrackTags(IntPtr, out int) must report the managed tag count.");
        TestAssert.True(Array.IndexOf(tags, "codex-track") >= 0, "Mixer.GetTrackTags(IntPtr, out int) must include the assigned tag.");
    }

    public static void MIX_GetTaggedTracks_ReturnsNativeTrackArray()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod("MIX_GetTaggedTracks", BindingFlags.NonPublic | BindingFlags.Static, [typeof(IntPtr), typeof(string), typeof(int).MakeByRefType()]);
        TestAssert.NotNull(method, "Mixer.MIX_GetTaggedTracks(IntPtr, string, out int) method must be private static.");
        AssertMixerLibraryImport(method!, "MIX_GetTaggedTracks");
        AssertStringParameterMarshal(method!, "tag");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        TestAssert.True(SDL3.Mixer.TagTrack(track.Handle, "codex-track"), "Mixer.TagTrack must add a tag before MIX_GetTaggedTracks.");
        object?[] args = [mixer.Handle, "codex-track", 0];
        IntPtr tracks = (IntPtr)method!.Invoke(null, args)!;

        try
        {
            TestAssert.True(tracks != IntPtr.Zero, "Mixer.MIX_GetTaggedTracks(IntPtr, string, out int) must return a native track array for an existing tag.");
            TestAssert.Equal(1, (int)args[2]!, "Mixer.MIX_GetTaggedTracks(IntPtr, string, out int) must report the tagged track count.");
        }
        finally
        {
            SDL3.SDL.Free(tracks);
        }
    }

    public static void GetTaggedTracks_ReturnsManagedTrackPointersAndCount()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.GetTaggedTracks), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(string), typeof(int).MakeByRefType()]);
        TestAssert.NotNull(method, "Mixer.GetTaggedTracks(IntPtr, string, out int) method must be public static.");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        TestAssert.True(SDL3.Mixer.TagTrack(track.Handle, "codex-track"), "Mixer.TagTrack must add a tag before GetTaggedTracks.");
        IntPtr[]? tracks = SDL3.Mixer.GetTaggedTracks(mixer.Handle, "codex-track", out int count);

        TestAssert.NotNull(tracks, "Mixer.GetTaggedTracks(IntPtr, string, out int) must return managed track pointers for an existing tag.");
        TestAssert.Equal(count, tracks!.Length, "Mixer.GetTaggedTracks(IntPtr, string, out int) must report the managed track count.");
        TestAssert.True(Array.IndexOf(tracks, track.Handle) >= 0, "Mixer.GetTaggedTracks(IntPtr, string, out int) must include the tagged track.");
    }

    public static void SetTrackPlaybackPosition_SeeksAssignedStoppedTrack()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.SetTrackPlaybackPosition), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(long)]);
        TestAssert.NotNull(method, "Mixer.SetTrackPlaybackPosition(IntPtr, long) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_SetTrackPlaybackPosition");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        using AudioScope audio = AudioScope.LoadWav(mixer.Handle);
        TestAssert.True(SDL3.Mixer.SetTrackAudio(track.Handle, audio.Handle), "Mixer.SetTrackAudio must assign audio before SetTrackPlaybackPosition.");
        TestAssert.True(SDL3.Mixer.SetTrackPlaybackPosition(track.Handle, 0), "Mixer.SetTrackPlaybackPosition(IntPtr, long) must seek a stopped track with seekable audio.");
    }

    public static void GetTrackFadeFrames_ReturnsZeroForStoppedTrack()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.GetTrackFadeFrames), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.GetTrackFadeFrames(IntPtr) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_GetTrackFadeFrames");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        long frames = SDL3.Mixer.GetTrackFadeFrames(track.Handle);
        TestAssert.Equal(0L, frames, "Mixer.GetTrackFadeFrames(IntPtr) must report zero for a stopped track.");
    }

    public static void GetTrackPlaybackPosition_ReturnsPositionForAssignedStoppedTrack()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.GetTrackPlaybackPosition), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.GetTrackPlaybackPosition(IntPtr) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_GetTrackPlaybackPosition");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        using AudioScope audio = AudioScope.LoadWav(mixer.Handle);
        TestAssert.True(SDL3.Mixer.SetTrackAudio(track.Handle, audio.Handle), "Mixer.SetTrackAudio must assign audio before GetTrackPlaybackPosition.");
        long position = SDL3.Mixer.GetTrackPlaybackPosition(track.Handle);
        TestAssert.True(position >= 0, "Mixer.GetTrackPlaybackPosition(IntPtr) must report a non-negative position for an assigned stopped track.");
    }

    public static void GetTrackLoops_ReturnsZeroForStoppedTrack()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.GetTrackLoops), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.GetTrackLoops(IntPtr) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_GetTrackLoops");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        int loops = SDL3.Mixer.GetTrackLoops(track.Handle);
        TestAssert.Equal(0, loops, "Mixer.GetTrackLoops(IntPtr) must report zero for a stopped track.");
    }

    public static void SetTrackLoops_ConfiguresAssignedTrack()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.SetTrackLoops), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(int)]);
        TestAssert.NotNull(method, "Mixer.SetTrackLoops(IntPtr, int) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_SetTrackLoops");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        using AudioScope audio = AudioScope.LoadWav(mixer.Handle);
        TestAssert.True(SDL3.Mixer.SetTrackAudio(track.Handle, audio.Handle), "Mixer.SetTrackAudio must assign audio before SetTrackLoops.");
        TestAssert.True(SDL3.Mixer.SetTrackLoops(track.Handle, 0), "Mixer.SetTrackLoops(IntPtr, int) must accept a zero loop count.");
    }

    public static void GetTrackAudio_ReturnsAssignedAudio()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.GetTrackAudio), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.GetTrackAudio(IntPtr) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_GetTrackAudio");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        using AudioScope audio = AudioScope.LoadWav(mixer.Handle);
        TestAssert.True(SDL3.Mixer.SetTrackAudio(track.Handle, audio.Handle), "Mixer.SetTrackAudio must assign audio before GetTrackAudio.");
        IntPtr assigned = SDL3.Mixer.GetTrackAudio(track.Handle);
        TestAssert.Equal(audio.Handle, assigned, "Mixer.GetTrackAudio(IntPtr) must return the assigned audio object.");
    }

    public static void GetTrackAudioStream_ReturnsAssignedStream()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.GetTrackAudioStream), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.GetTrackAudioStream(IntPtr) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_GetTrackAudioStream");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        using AudioStreamScope stream = AudioStreamScope.Create();
        TestAssert.True(SDL3.Mixer.SetTrackAudioStream(track.Handle, stream.Handle), "Mixer.SetTrackAudioStream must assign a stream before GetTrackAudioStream.");
        IntPtr assigned = SDL3.Mixer.GetTrackAudioStream(track.Handle);
        TestAssert.Equal(stream.Handle, assigned, "Mixer.GetTrackAudioStream(IntPtr) must return the assigned audio stream.");
        TestAssert.True(SDL3.Mixer.SetTrackAudioStream(track.Handle, IntPtr.Zero), "Mixer.SetTrackAudioStream(IntPtr, IntPtr.Zero) must clear the track input before stream disposal.");
    }

    public static void GetTrackRemaining_ReturnsZeroForAssignedStoppedTrack()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.GetTrackRemaining), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.GetTrackRemaining(IntPtr) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_GetTrackRemaining");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        using AudioScope audio = AudioScope.LoadWav(mixer.Handle);
        TestAssert.True(SDL3.Mixer.SetTrackAudio(track.Handle, audio.Handle), "Mixer.SetTrackAudio must assign audio before GetTrackRemaining.");
        long remaining = SDL3.Mixer.GetTrackRemaining(track.Handle);
        TestAssert.Equal(0L, remaining, "Mixer.GetTrackRemaining(IntPtr) must report zero for an assigned stopped track.");
    }

    public static void TrackMSToFrames_ConvertsMillisecondsForAssignedTrack()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.TrackMSToFrames), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(long)]);
        TestAssert.NotNull(method, "Mixer.TrackMSToFrames(IntPtr, long) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_TrackMSToFrames");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        using AudioScope audio = AudioScope.LoadWav(mixer.Handle);
        TestAssert.True(SDL3.Mixer.SetTrackAudio(track.Handle, audio.Handle), "Mixer.SetTrackAudio must assign audio before TrackMSToFrames.");
        long frames = SDL3.Mixer.TrackMSToFrames(track.Handle, 1);
        TestAssert.Equal(48L, frames, "Mixer.TrackMSToFrames(IntPtr, long) must convert 1 ms at 48000 Hz to 48 frames.");
    }

    public static void TrackFramesToMS_ConvertsFramesForAssignedTrack()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.TrackFramesToMS), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(long)]);
        TestAssert.NotNull(method, "Mixer.TrackFramesToMS(IntPtr, long) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_TrackFramesToMS");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        using AudioScope audio = AudioScope.LoadWav(mixer.Handle);
        TestAssert.True(SDL3.Mixer.SetTrackAudio(track.Handle, audio.Handle), "Mixer.SetTrackAudio must assign audio before TrackFramesToMS.");
        long milliseconds = SDL3.Mixer.TrackFramesToMS(track.Handle, 48);
        TestAssert.Equal(1L, milliseconds, "Mixer.TrackFramesToMS(IntPtr, long) must convert 48 frames at 48000 Hz to 1 ms.");
    }

    public static void AudioMSToFrames_ConvertsMillisecondsForAudio()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.AudioMSToFrames), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(long)]);
        TestAssert.NotNull(method, "Mixer.AudioMSToFrames(IntPtr, long) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_AudioMSToFrames");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using AudioScope audio = AudioScope.LoadWav(mixer.Handle);
        long frames = SDL3.Mixer.AudioMSToFrames(audio.Handle, 1);
        TestAssert.Equal(48L, frames, "Mixer.AudioMSToFrames(IntPtr, long) must convert 1 ms at 48000 Hz to 48 frames.");
    }

    public static void AudioFramesToMS_ConvertsFramesForAudio()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.AudioFramesToMS), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(long)]);
        TestAssert.NotNull(method, "Mixer.AudioFramesToMS(IntPtr, long) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_AudioFramesToMS");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using AudioScope audio = AudioScope.LoadWav(mixer.Handle);
        long milliseconds = SDL3.Mixer.AudioFramesToMS(audio.Handle, 48);
        TestAssert.Equal(1L, milliseconds, "Mixer.AudioFramesToMS(IntPtr, long) must convert 48 frames at 48000 Hz to 1 ms.");
    }

    public static void MSToFrames_ConvertsMillisecondsAtSampleRate()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.MSToFrames), BindingFlags.Public | BindingFlags.Static, [typeof(int), typeof(long)]);
        TestAssert.NotNull(method, "Mixer.MSToFrames(int, long) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_MSToFrames");

        long frames = SDL3.Mixer.MSToFrames(48_000, 1);
        TestAssert.Equal(48L, frames, "Mixer.MSToFrames(int, long) must convert 1 ms at 48000 Hz to 48 frames.");
    }

    public static void FramesToMS_ConvertsFramesAtSampleRate()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.FramesToMS), BindingFlags.Public | BindingFlags.Static, [typeof(int), typeof(long)]);
        TestAssert.NotNull(method, "Mixer.FramesToMS(int, long) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_FramesToMS");

        long milliseconds = SDL3.Mixer.FramesToMS(48_000, 48);
        TestAssert.Equal(1L, milliseconds, "Mixer.FramesToMS(int, long) must convert 48 frames at 48000 Hz to 1 ms.");
    }

    public static void PlayTrack_StartsAssignedTrack()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.PlayTrack), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(uint)]);
        TestAssert.NotNull(method, "Mixer.PlayTrack(IntPtr, uint) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_PlayTrack");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        using AudioScope audio = AudioScope.LoadWav(mixer.Handle);
        TestAssert.True(SDL3.Mixer.SetTrackAudio(track.Handle, audio.Handle), "Mixer.SetTrackAudio must assign audio before PlayTrack.");
        TestAssert.True(SDL3.Mixer.PlayTrack(track.Handle, 0), "Mixer.PlayTrack(IntPtr, uint) must start an assigned track.");
        TestAssert.True(SDL3.Mixer.TrackPlaying(track.Handle), "Mixer.TrackPlaying(IntPtr) must report true after PlayTrack.");
    }

    public static void PlayTag_StartsTaggedAssignedTrack()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.PlayTag), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(string), typeof(uint)]);
        TestAssert.NotNull(method, "Mixer.PlayTag(IntPtr, string, uint) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_PlayTag");
        AssertStringParameterMarshal(method!, "tag");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        using AudioScope audio = AudioScope.LoadWav(mixer.Handle);
        TestAssert.True(SDL3.Mixer.SetTrackAudio(track.Handle, audio.Handle), "Mixer.SetTrackAudio must assign audio before PlayTag.");
        TestAssert.True(SDL3.Mixer.TagTrack(track.Handle, "codex-play"), "Mixer.TagTrack must tag the track before PlayTag.");
        TestAssert.True(SDL3.Mixer.PlayTag(mixer.Handle, "codex-play", 0), "Mixer.PlayTag(IntPtr, string, uint) must start tagged tracks.");
        TestAssert.True(SDL3.Mixer.TrackPlaying(track.Handle), "Mixer.TrackPlaying(IntPtr) must report true after PlayTag.");
    }

    public static void PlayAudio_StartsFireAndForgetAudio()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.PlayAudio), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.PlayAudio(IntPtr, IntPtr) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_PlayAudio");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using AudioScope audio = AudioScope.LoadWav(mixer.Handle);
        TestAssert.True(SDL3.Mixer.PlayAudio(mixer.Handle, audio.Handle), "Mixer.PlayAudio(IntPtr, IntPtr) must start fire-and-forget playback for valid audio.");
    }

    public static void StopTrack_StopsPlayingTrack()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.StopTrack), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(long)]);
        TestAssert.NotNull(method, "Mixer.StopTrack(IntPtr, long) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_StopTrack");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        using AudioScope audio = AudioScope.LoadWav(mixer.Handle);
        TestAssert.True(SDL3.Mixer.SetTrackAudio(track.Handle, audio.Handle), "Mixer.SetTrackAudio must assign audio before StopTrack.");
        TestAssert.True(SDL3.Mixer.PlayTrack(track.Handle, 0), "Mixer.PlayTrack must start the track before StopTrack.");
        TestAssert.True(SDL3.Mixer.StopTrack(track.Handle, 0), "Mixer.StopTrack(IntPtr, long) must stop a playing track immediately.");
        TestAssert.Equal(false, SDL3.Mixer.TrackPlaying(track.Handle), "Mixer.TrackPlaying(IntPtr) must report false after StopTrack.");
    }

    public static void StopAllTracks_StopsPlayingTracks()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.StopAllTracks), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(long)]);
        TestAssert.NotNull(method, "Mixer.StopAllTracks(IntPtr, long) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_StopAllTracks");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        using AudioScope audio = AudioScope.LoadWav(mixer.Handle);
        TestAssert.True(SDL3.Mixer.SetTrackAudio(track.Handle, audio.Handle), "Mixer.SetTrackAudio must assign audio before StopAllTracks.");
        TestAssert.True(SDL3.Mixer.PlayTrack(track.Handle, 0), "Mixer.PlayTrack must start the track before StopAllTracks.");
        TestAssert.True(SDL3.Mixer.StopAllTracks(mixer.Handle, 0), "Mixer.StopAllTracks(IntPtr, long) must stop all playing tracks immediately.");
        TestAssert.Equal(false, SDL3.Mixer.TrackPlaying(track.Handle), "Mixer.TrackPlaying(IntPtr) must report false after StopAllTracks.");
    }

    public static void StopTag_StopsTaggedPlayingTrack()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.StopTag), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(string), typeof(long)]);
        TestAssert.NotNull(method, "Mixer.StopTag(IntPtr, string, long) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_StopTag");
        AssertStringParameterMarshal(method!, "tag");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        using AudioScope audio = AudioScope.LoadWav(mixer.Handle);
        TestAssert.True(SDL3.Mixer.SetTrackAudio(track.Handle, audio.Handle), "Mixer.SetTrackAudio must assign audio before StopTag.");
        TestAssert.True(SDL3.Mixer.TagTrack(track.Handle, "codex-stop"), "Mixer.TagTrack must tag the track before StopTag.");
        TestAssert.True(SDL3.Mixer.PlayTrack(track.Handle, 0), "Mixer.PlayTrack must start the track before StopTag.");
        TestAssert.True(SDL3.Mixer.StopTag(mixer.Handle, "codex-stop", 0), "Mixer.StopTag(IntPtr, string, long) must stop tagged playing tracks immediately.");
        TestAssert.Equal(false, SDL3.Mixer.TrackPlaying(track.Handle), "Mixer.TrackPlaying(IntPtr) must report false after StopTag.");
    }

    public static void PauseTrack_PausesPlayingTrack()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.PauseTrack), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.PauseTrack(IntPtr) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_PauseTrack");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        using AudioScope audio = AudioScope.LoadWav(mixer.Handle);
        TestAssert.True(SDL3.Mixer.SetTrackAudio(track.Handle, audio.Handle), "Mixer.SetTrackAudio must assign audio before PauseTrack.");
        TestAssert.True(SDL3.Mixer.PlayTrack(track.Handle, 0), "Mixer.PlayTrack must start the track before PauseTrack.");
        TestAssert.True(SDL3.Mixer.PauseTrack(track.Handle), "Mixer.PauseTrack(IntPtr) must pause a playing track.");
        TestAssert.True(SDL3.Mixer.TrackPaused(track.Handle), "Mixer.TrackPaused(IntPtr) must report true after PauseTrack.");
    }

    public static void PauseAllTracks_PausesPlayingTracks()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.PauseAllTracks), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.PauseAllTracks(IntPtr) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_PauseAllTracks");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        using AudioScope audio = AudioScope.LoadWav(mixer.Handle);
        TestAssert.True(SDL3.Mixer.SetTrackAudio(track.Handle, audio.Handle), "Mixer.SetTrackAudio must assign audio before PauseAllTracks.");
        TestAssert.True(SDL3.Mixer.PlayTrack(track.Handle, 0), "Mixer.PlayTrack must start the track before PauseAllTracks.");
        TestAssert.True(SDL3.Mixer.PauseAllTracks(mixer.Handle), "Mixer.PauseAllTracks(IntPtr) must pause all playing tracks.");
        TestAssert.True(SDL3.Mixer.TrackPaused(track.Handle), "Mixer.TrackPaused(IntPtr) must report true after PauseAllTracks.");
    }

    public static void PauseTag_PausesTaggedPlayingTrack()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.PauseTag), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(string)]);
        TestAssert.NotNull(method, "Mixer.PauseTag(IntPtr, string) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_PauseTag");
        AssertStringParameterMarshal(method!, "tag");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        using AudioScope audio = AudioScope.LoadWav(mixer.Handle);
        TestAssert.True(SDL3.Mixer.SetTrackAudio(track.Handle, audio.Handle), "Mixer.SetTrackAudio must assign audio before PauseTag.");
        TestAssert.True(SDL3.Mixer.TagTrack(track.Handle, "codex-pause"), "Mixer.TagTrack must tag the track before PauseTag.");
        TestAssert.True(SDL3.Mixer.PlayTrack(track.Handle, 0), "Mixer.PlayTrack must start the track before PauseTag.");
        TestAssert.True(SDL3.Mixer.PauseTag(mixer.Handle, "codex-pause"), "Mixer.PauseTag(IntPtr, string) must pause tagged playing tracks.");
        TestAssert.True(SDL3.Mixer.TrackPaused(track.Handle), "Mixer.TrackPaused(IntPtr) must report true after PauseTag.");
    }

    public static void ResumeTrack_ResumesPausedTrack()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.ResumeTrack), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.ResumeTrack(IntPtr) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_ResumeTrack");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        using AudioScope audio = AudioScope.LoadWav(mixer.Handle);
        TestAssert.True(SDL3.Mixer.SetTrackAudio(track.Handle, audio.Handle), "Mixer.SetTrackAudio must assign audio before ResumeTrack.");
        TestAssert.True(SDL3.Mixer.PlayTrack(track.Handle, 0), "Mixer.PlayTrack must start the track before ResumeTrack.");
        TestAssert.True(SDL3.Mixer.PauseTrack(track.Handle), "Mixer.PauseTrack must pause the track before ResumeTrack.");
        TestAssert.True(SDL3.Mixer.ResumeTrack(track.Handle), "Mixer.ResumeTrack(IntPtr) must resume a paused track.");
        TestAssert.True(SDL3.Mixer.TrackPlaying(track.Handle), "Mixer.TrackPlaying(IntPtr) must report true after ResumeTrack.");
    }

    public static void ResumeAllTracks_ResumesPausedTracks()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.ResumeAllTracks), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.ResumeAllTracks(IntPtr) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_ResumeAllTracks");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        using AudioScope audio = AudioScope.LoadWav(mixer.Handle);
        TestAssert.True(SDL3.Mixer.SetTrackAudio(track.Handle, audio.Handle), "Mixer.SetTrackAudio must assign audio before ResumeAllTracks.");
        TestAssert.True(SDL3.Mixer.PlayTrack(track.Handle, 0), "Mixer.PlayTrack must start the track before ResumeAllTracks.");
        TestAssert.True(SDL3.Mixer.PauseAllTracks(mixer.Handle), "Mixer.PauseAllTracks must pause tracks before ResumeAllTracks.");
        TestAssert.True(SDL3.Mixer.ResumeAllTracks(mixer.Handle), "Mixer.ResumeAllTracks(IntPtr) must resume paused tracks.");
        TestAssert.True(SDL3.Mixer.TrackPlaying(track.Handle), "Mixer.TrackPlaying(IntPtr) must report true after ResumeAllTracks.");
    }

    public static void ResumeTag_ResumesTaggedPausedTrack()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.ResumeTag), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(string)]);
        TestAssert.NotNull(method, "Mixer.ResumeTag(IntPtr, string) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_ResumeTag");
        AssertStringParameterMarshal(method!, "tag");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        using AudioScope audio = AudioScope.LoadWav(mixer.Handle);
        TestAssert.True(SDL3.Mixer.SetTrackAudio(track.Handle, audio.Handle), "Mixer.SetTrackAudio must assign audio before ResumeTag.");
        TestAssert.True(SDL3.Mixer.TagTrack(track.Handle, "codex-resume"), "Mixer.TagTrack must tag the track before ResumeTag.");
        TestAssert.True(SDL3.Mixer.PlayTrack(track.Handle, 0), "Mixer.PlayTrack must start the track before ResumeTag.");
        TestAssert.True(SDL3.Mixer.PauseTag(mixer.Handle, "codex-resume"), "Mixer.PauseTag must pause tagged tracks before ResumeTag.");
        TestAssert.True(SDL3.Mixer.ResumeTag(mixer.Handle, "codex-resume"), "Mixer.ResumeTag(IntPtr, string) must resume tagged paused tracks.");
        TestAssert.True(SDL3.Mixer.TrackPlaying(track.Handle), "Mixer.TrackPlaying(IntPtr) must report true after ResumeTag.");
    }

    public static void TrackPlaying_ReportsPlayingState()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.TrackPlaying), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.TrackPlaying(IntPtr) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_TrackPlaying");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        using AudioScope audio = AudioScope.LoadWav(mixer.Handle);
        TestAssert.Equal(false, SDL3.Mixer.TrackPlaying(track.Handle), "Mixer.TrackPlaying(IntPtr) must report false before playback.");
        TestAssert.True(SDL3.Mixer.SetTrackAudio(track.Handle, audio.Handle), "Mixer.SetTrackAudio must assign audio before TrackPlaying.");
        TestAssert.True(SDL3.Mixer.PlayTrack(track.Handle, 0), "Mixer.PlayTrack must start the track before TrackPlaying.");
        TestAssert.True(SDL3.Mixer.TrackPlaying(track.Handle), "Mixer.TrackPlaying(IntPtr) must report true during playback.");
    }

    public static void TrackPaused_ReportsPausedState()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.TrackPaused), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.TrackPaused(IntPtr) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_TrackPaused");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        using AudioScope audio = AudioScope.LoadWav(mixer.Handle);
        TestAssert.Equal(false, SDL3.Mixer.TrackPaused(track.Handle), "Mixer.TrackPaused(IntPtr) must report false before playback.");
        TestAssert.True(SDL3.Mixer.SetTrackAudio(track.Handle, audio.Handle), "Mixer.SetTrackAudio must assign audio before TrackPaused.");
        TestAssert.True(SDL3.Mixer.PlayTrack(track.Handle, 0), "Mixer.PlayTrack must start the track before TrackPaused.");
        TestAssert.True(SDL3.Mixer.PauseTrack(track.Handle), "Mixer.PauseTrack must pause the track before TrackPaused.");
        TestAssert.True(SDL3.Mixer.TrackPaused(track.Handle), "Mixer.TrackPaused(IntPtr) must report true while paused.");
    }

    public static void SetMixerGain_SetsGainForMemoryMixer()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.SetMixerGain), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(float)]);
        TestAssert.NotNull(method, "Mixer.SetMixerGain(IntPtr, float) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_SetMixerGain");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        TestAssert.True(SDL3.Mixer.SetMixerGain(mixer.Handle, 0.5f), "Mixer.SetMixerGain(IntPtr, float) must set gain for a valid memory mixer.");
        TestAssert.Equal(false, SDL3.Mixer.SetMixerGain(mixer.Handle, -1.0f), "Mixer.SetMixerGain(IntPtr, float) must reject negative gain.");
    }

    public static void GetMixerGain_ReturnsLastMixerGain()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.GetMixerGain), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.GetMixerGain(IntPtr) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_GetMixerGain");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        TestAssert.Equal(1.0f, SDL3.Mixer.GetMixerGain(mixer.Handle), "Mixer.GetMixerGain(IntPtr) must return the default memory mixer gain.");
        TestAssert.True(SDL3.Mixer.SetMixerGain(mixer.Handle, 0.5f), "Mixer.SetMixerGain must set gain before GetMixerGain.");
        TestAssert.Equal(0.5f, SDL3.Mixer.GetMixerGain(mixer.Handle), "Mixer.GetMixerGain(IntPtr) must return the last mixer gain.");
    }

    public static void SetTrackGain_SetsGainForTrack()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.SetTrackGain), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(float)]);
        TestAssert.NotNull(method, "Mixer.SetTrackGain(IntPtr, float) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_SetTrackGain");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        TestAssert.True(SDL3.Mixer.SetTrackGain(track.Handle, 0.25f), "Mixer.SetTrackGain(IntPtr, float) must set gain for a valid track.");
        TestAssert.Equal(false, SDL3.Mixer.SetTrackGain(IntPtr.Zero, 0.25f), "Mixer.SetTrackGain(IntPtr.Zero, float) must fail safely for a null track.");
    }

    public static void GetTrackGain_ReturnsLastTrackGain()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.GetTrackGain), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.GetTrackGain(IntPtr) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_GetTrackGain");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        TestAssert.Equal(1.0f, SDL3.Mixer.GetTrackGain(track.Handle), "Mixer.GetTrackGain(IntPtr) must return the default track gain.");
        TestAssert.True(SDL3.Mixer.SetTrackGain(track.Handle, 0.25f), "Mixer.SetTrackGain must set gain before GetTrackGain.");
        TestAssert.Equal(0.25f, SDL3.Mixer.GetTrackGain(track.Handle), "Mixer.GetTrackGain(IntPtr) must return the last track gain.");
    }

    public static void SetTagGain_SetsGainForTaggedTrack()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.SetTagGain), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(string), typeof(float)]);
        TestAssert.NotNull(method, "Mixer.SetTagGain(IntPtr, string, float) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_SetTagGain");
        AssertStringParameterMarshal(method!, "tag");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        TestAssert.True(SDL3.Mixer.TagTrack(track.Handle, "codex-gain"), "Mixer.TagTrack must tag the track before SetTagGain.");
        TestAssert.True(SDL3.Mixer.SetTagGain(mixer.Handle, "codex-gain", 0.125f), "Mixer.SetTagGain(IntPtr, string, float) must set gain for tagged tracks.");
        TestAssert.Equal(0.125f, SDL3.Mixer.GetTrackGain(track.Handle), "Mixer.SetTagGain(IntPtr, string, float) must update the tagged track gain.");
        TestAssert.Equal(false, SDL3.Mixer.SetTagGain(IntPtr.Zero, "codex-gain", 0.125f), "Mixer.SetTagGain(IntPtr.Zero, string, float) must fail safely for a null mixer.");
    }

    public static void SetMixerFrequencyRatio_SetsRatioForMemoryMixer()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.SetMixerFrequencyRatio), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(float)]);
        TestAssert.NotNull(method, "Mixer.SetMixerFrequencyRatio(IntPtr, float) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_SetMixerFrequencyRatio");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        TestAssert.True(SDL3.Mixer.SetMixerFrequencyRatio(mixer.Handle, 1.25f), "Mixer.SetMixerFrequencyRatio(IntPtr, float) must set a valid memory mixer frequency ratio.");
        TestAssert.Equal(false, SDL3.Mixer.SetMixerFrequencyRatio(IntPtr.Zero, 1.25f), "Mixer.SetMixerFrequencyRatio(IntPtr.Zero, float) must fail safely for a null mixer.");
    }

    public static void GetMixerFrequencyRatio_ReturnsLastMixerRatio()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.GetMixerFrequencyRatio), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.GetMixerFrequencyRatio(IntPtr) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_GetMixerFrequencyRatio");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        TestAssert.Equal(1.0f, SDL3.Mixer.GetMixerFrequencyRatio(mixer.Handle), "Mixer.GetMixerFrequencyRatio(IntPtr) must return the default memory mixer frequency ratio.");
        TestAssert.True(SDL3.Mixer.SetMixerFrequencyRatio(mixer.Handle, 1.25f), "Mixer.SetMixerFrequencyRatio must set a ratio before GetMixerFrequencyRatio.");
        TestAssert.Equal(1.25f, SDL3.Mixer.GetMixerFrequencyRatio(mixer.Handle), "Mixer.GetMixerFrequencyRatio(IntPtr) must return the last memory mixer frequency ratio.");
    }

    public static void SetTrackFrequencyRatio_SetsRatioForTrack()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.SetTrackFrequencyRatio), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(float)]);
        TestAssert.NotNull(method, "Mixer.SetTrackFrequencyRatio(IntPtr, float) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_SetTrackFrequencyRatio");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        TestAssert.True(SDL3.Mixer.SetTrackFrequencyRatio(track.Handle, 0.75f), "Mixer.SetTrackFrequencyRatio(IntPtr, float) must set a valid track frequency ratio.");
        TestAssert.Equal(false, SDL3.Mixer.SetTrackFrequencyRatio(IntPtr.Zero, 0.75f), "Mixer.SetTrackFrequencyRatio(IntPtr.Zero, float) must fail safely for a null track.");
    }

    public static void GetTrackFrequencyRatio_ReturnsLastTrackRatio()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.GetTrackFrequencyRatio), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.GetTrackFrequencyRatio(IntPtr) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_GetTrackFrequencyRatio");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        TestAssert.Equal(1.0f, SDL3.Mixer.GetTrackFrequencyRatio(track.Handle), "Mixer.GetTrackFrequencyRatio(IntPtr) must return the default track frequency ratio.");
        TestAssert.True(SDL3.Mixer.SetTrackFrequencyRatio(track.Handle, 0.75f), "Mixer.SetTrackFrequencyRatio must set a ratio before GetTrackFrequencyRatio.");
        TestAssert.Equal(0.75f, SDL3.Mixer.GetTrackFrequencyRatio(track.Handle), "Mixer.GetTrackFrequencyRatio(IntPtr) must return the last track frequency ratio.");
    }

    public static void SetTrackOutputChannelMap_SetsStereoSwapMap()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.SetTrackOutputChannelMap), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(IntPtr), typeof(int)]);
        TestAssert.NotNull(method, "Mixer.SetTrackOutputChannelMap(IntPtr, IntPtr, int) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_SetTrackOutputChannelMap");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        IntPtr channelMap = Marshal.AllocHGlobal(sizeof(int) * 2);

        try
        {
            Marshal.WriteInt32(channelMap, 0, 1);
            Marshal.WriteInt32(channelMap, sizeof(int), 0);
            TestAssert.True(SDL3.Mixer.SetTrackOutputChannelMap(track.Handle, channelMap, 2), "Mixer.SetTrackOutputChannelMap(IntPtr, IntPtr, int) must accept a valid stereo map.");
            TestAssert.Equal(false, SDL3.Mixer.SetTrackOutputChannelMap(IntPtr.Zero, channelMap, 2), "Mixer.SetTrackOutputChannelMap(IntPtr.Zero, IntPtr, int) must fail safely for a null track.");
        }
        finally
        {
            Marshal.FreeHGlobal(channelMap);
        }
    }

    public static void SetTrackStereo_SetsAndClearsStereoGains()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.SetTrackStereo), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.SetTrackStereo(IntPtr, IntPtr) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_SetTrackStereo");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        SDL3.Mixer.StereoGains gains = new()
        {
            Left = 0.25f,
            Right = 0.75f
        };
        IntPtr gainsHandle = Marshal.AllocHGlobal(Marshal.SizeOf<SDL3.Mixer.StereoGains>());

        try
        {
            Marshal.StructureToPtr(gains, gainsHandle, fDeleteOld: false);
            TestAssert.True(SDL3.Mixer.SetTrackStereo(track.Handle, gainsHandle), "Mixer.SetTrackStereo(IntPtr, IntPtr) must accept valid stereo gains.");
            TestAssert.True(SDL3.Mixer.SetTrackStereo(track.Handle, IntPtr.Zero), "Mixer.SetTrackStereo(IntPtr, IntPtr.Zero) must clear spatialization.");
        }
        finally
        {
            Marshal.FreeHGlobal(gainsHandle);
        }
    }

    public static void SetTrack3DPosition_SetsTrackPosition()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.SetTrack3DPosition), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.SetTrack3DPosition(IntPtr, IntPtr) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_SetTrack3DPosition");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        SDL3.Mixer.Point3D point = new()
        {
            X = 1.0f,
            Y = 2.0f,
            Z = 3.0f
        };
        IntPtr pointHandle = Marshal.AllocHGlobal(Marshal.SizeOf<SDL3.Mixer.Point3D>());

        try
        {
            Marshal.StructureToPtr(point, pointHandle, fDeleteOld: false);
            TestAssert.True(SDL3.Mixer.SetTrack3DPosition(track.Handle, pointHandle), "Mixer.SetTrack3DPosition(IntPtr, IntPtr) must accept a valid 3D point.");
            TestAssert.True(SDL3.Mixer.SetTrack3DPosition(track.Handle, IntPtr.Zero), "Mixer.SetTrack3DPosition(IntPtr, IntPtr.Zero) must clear spatialization.");
        }
        finally
        {
            Marshal.FreeHGlobal(pointHandle);
        }
    }

    public static void GetTrack3DPosition_ReturnsLastTrackPosition()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.GetTrack3DPosition), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.GetTrack3DPosition(IntPtr, IntPtr) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_GetTrack3DPosition");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        SDL3.Mixer.Point3D point = new()
        {
            X = 1.0f,
            Y = 2.0f,
            Z = 3.0f
        };
        IntPtr pointHandle = Marshal.AllocHGlobal(Marshal.SizeOf<SDL3.Mixer.Point3D>());
        IntPtr resultHandle = Marshal.AllocHGlobal(Marshal.SizeOf<SDL3.Mixer.Point3D>());

        try
        {
            Marshal.StructureToPtr(point, pointHandle, fDeleteOld: false);
            TestAssert.True(SDL3.Mixer.SetTrack3DPosition(track.Handle, pointHandle), "Mixer.SetTrack3DPosition must set a point before GetTrack3DPosition.");
            TestAssert.True(SDL3.Mixer.GetTrack3DPosition(track.Handle, resultHandle), "Mixer.GetTrack3DPosition(IntPtr, IntPtr) must read a valid track position.");
            SDL3.Mixer.Point3D result = Marshal.PtrToStructure<SDL3.Mixer.Point3D>(resultHandle);

            TestAssert.Equal(1.0f, result.X, "Mixer.GetTrack3DPosition(IntPtr, IntPtr) must return the last X coordinate.");
            TestAssert.Equal(2.0f, result.Y, "Mixer.GetTrack3DPosition(IntPtr, IntPtr) must return the last Y coordinate.");
            TestAssert.Equal(3.0f, result.Z, "Mixer.GetTrack3DPosition(IntPtr, IntPtr) must return the last Z coordinate.");
        }
        finally
        {
            Marshal.FreeHGlobal(resultHandle);
            Marshal.FreeHGlobal(pointHandle);
        }
    }

    public static void CreateGroup_ReturnsGroupForMemoryMixer()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.CreateGroup), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.CreateGroup(IntPtr) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_CreateGroup");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        IntPtr group = SDL3.Mixer.CreateGroup(mixer.Handle);

        try
        {
            TestAssert.True(group != IntPtr.Zero, "Mixer.CreateGroup(IntPtr) must create a group for a valid memory mixer.");
        }
        finally
        {
            DestroyGroupIfNeeded(group);
        }
    }

    public static void DestroyGroup_DestroysGroupAndAllowsNull()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.DestroyGroup), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.DestroyGroup(IntPtr) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_DestroyGroup");

        SDL3.Mixer.DestroyGroup(IntPtr.Zero);
        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        IntPtr group = SDL3.Mixer.CreateGroup(mixer.Handle);
        TestAssert.True(group != IntPtr.Zero, "Mixer.CreateGroup(IntPtr) must create a group for DestroyGroup.");
        SDL3.Mixer.DestroyGroup(group);
    }

    public static void GetGroupProperties_ReturnsPropertiesForGroup()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.GetGroupProperties), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.GetGroupProperties(IntPtr) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_GetGroupProperties");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using GroupScope group = GroupScope.Create(mixer);
        uint properties = SDL3.Mixer.GetGroupProperties(group.Handle);
        TestAssert.True(properties != 0, "Mixer.GetGroupProperties(IntPtr) must return a properties ID for a valid group.");
    }

    public static void GetGroupMixer_ReturnsOwningMixer()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.GetGroupMixer), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.GetGroupMixer(IntPtr) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_GetGroupMixer");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using GroupScope group = GroupScope.Create(mixer);
        IntPtr owner = SDL3.Mixer.GetGroupMixer(group.Handle);
        TestAssert.Equal(mixer.Handle, owner, "Mixer.GetGroupMixer(IntPtr) must return the mixer used to create the group.");
    }

    public static void SetTrackGroup_AssignsAndClearsGroup()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.SetTrackGroup), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.SetTrackGroup(IntPtr, IntPtr) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_SetTrackGroup");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        using GroupScope group = GroupScope.Create(mixer);
        TestAssert.True(SDL3.Mixer.SetTrackGroup(track.Handle, group.Handle), "Mixer.SetTrackGroup(IntPtr, IntPtr) must assign a track to a group on the same mixer.");
        TestAssert.True(SDL3.Mixer.SetTrackGroup(track.Handle, IntPtr.Zero), "Mixer.SetTrackGroup(IntPtr, IntPtr.Zero) must clear group assignment.");
    }

    public static void SetTrackStoppedCallback_SetsAndClearsCallback()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.SetTrackStoppedCallback), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(SDL3.Mixer.TrackStoppedCallback), typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.SetTrackStoppedCallback(IntPtr, TrackStoppedCallback, IntPtr) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_SetTrackStoppedCallback");
        AssertCallbackParameter(method!, "cb", typeof(SDL3.Mixer.TrackStoppedCallback));

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        SDL3.Mixer.TrackStoppedCallback callback = (_, _) => { };
        callback(IntPtr.Zero, track.Handle);
        TestAssert.True(SDL3.Mixer.SetTrackStoppedCallback(track.Handle, callback, IntPtr.Zero), "Mixer.SetTrackStoppedCallback(IntPtr, TrackStoppedCallback, IntPtr) must set a callback for a valid track.");
        TestAssert.True(SDL3.Mixer.SetTrackStoppedCallback(track.Handle, null!, IntPtr.Zero), "Mixer.SetTrackStoppedCallback(IntPtr, null, IntPtr) must clear the track callback.");
        GC.KeepAlive(callback);
    }

    public static void SetTrackRawCallback_SetsAndClearsCallback()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.SetTrackRawCallback), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(SDL3.Mixer.TrackMixCallback), typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.SetTrackRawCallback(IntPtr, TrackMixCallback, IntPtr) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_SetTrackRawCallback");
        AssertCallbackParameter(method!, "cb", typeof(SDL3.Mixer.TrackMixCallback));

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        SDL3.Mixer.TrackMixCallback callback = (_, _, _, _, _) => { };
        callback(IntPtr.Zero, track.Handle, IntPtr.Zero, IntPtr.Zero, 0);
        TestAssert.True(SDL3.Mixer.SetTrackRawCallback(track.Handle, callback, IntPtr.Zero), "Mixer.SetTrackRawCallback(IntPtr, TrackMixCallback, IntPtr) must set a callback for a valid track.");
        TestAssert.True(SDL3.Mixer.SetTrackRawCallback(track.Handle, null!, IntPtr.Zero), "Mixer.SetTrackRawCallback(IntPtr, null, IntPtr) must clear the track callback.");
        GC.KeepAlive(callback);
    }

    public static void SetTrackCookedCallback_SetsAndClearsCallback()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.SetTrackCookedCallback), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(SDL3.Mixer.TrackMixCallback), typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.SetTrackCookedCallback(IntPtr, TrackMixCallback, IntPtr) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_SetTrackCookedCallback");
        AssertCallbackParameter(method!, "cb", typeof(SDL3.Mixer.TrackMixCallback));

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        SDL3.Mixer.TrackMixCallback callback = (_, _, _, _, _) => { };
        callback(IntPtr.Zero, track.Handle, IntPtr.Zero, IntPtr.Zero, 0);
        TestAssert.True(SDL3.Mixer.SetTrackCookedCallback(track.Handle, callback, IntPtr.Zero), "Mixer.SetTrackCookedCallback(IntPtr, TrackMixCallback, IntPtr) must set a callback for a valid track.");
        TestAssert.True(SDL3.Mixer.SetTrackCookedCallback(track.Handle, null!, IntPtr.Zero), "Mixer.SetTrackCookedCallback(IntPtr, null, IntPtr) must clear the track callback.");
        GC.KeepAlive(callback);
    }

    public static void SetGroupPostMixCallback_SetsAndClearsCallback()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.SetGroupPostMixCallback), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(SDL3.Mixer.GroupMixCallback), typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.SetGroupPostMixCallback(IntPtr, GroupMixCallback, IntPtr) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_SetGroupPostMixCallback");
        AssertCallbackParameter(method!, "cb", typeof(SDL3.Mixer.GroupMixCallback));

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using GroupScope group = GroupScope.Create(mixer);
        SDL3.Mixer.GroupMixCallback callback = (_, _, _, _, _) => { };
        callback(IntPtr.Zero, group.Handle, IntPtr.Zero, IntPtr.Zero, 0);
        TestAssert.True(SDL3.Mixer.SetGroupPostMixCallback(group.Handle, callback, IntPtr.Zero), "Mixer.SetGroupPostMixCallback(IntPtr, GroupMixCallback, IntPtr) must set a callback for a valid group.");
        TestAssert.True(SDL3.Mixer.SetGroupPostMixCallback(group.Handle, null!, IntPtr.Zero), "Mixer.SetGroupPostMixCallback(IntPtr, null, IntPtr) must clear the group callback.");
        GC.KeepAlive(callback);
    }

    public static void SetPostMixCallback_SetsAndClearsCallback()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.SetPostMixCallback), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(SDL3.Mixer.PostMixCallback), typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.SetPostMixCallback(IntPtr, PostMixCallback, IntPtr) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_SetPostMixCallback");
        AssertCallbackParameter(method!, "cb", typeof(SDL3.Mixer.PostMixCallback));

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        SDL3.Mixer.PostMixCallback callback = (_, _, _, _, _) => { };
        callback(IntPtr.Zero, mixer.Handle, IntPtr.Zero, IntPtr.Zero, 0);
        TestAssert.True(SDL3.Mixer.SetPostMixCallback(mixer.Handle, callback, IntPtr.Zero), "Mixer.SetPostMixCallback(IntPtr, PostMixCallback, IntPtr) must set a callback for a valid mixer.");
        TestAssert.True(SDL3.Mixer.SetPostMixCallback(mixer.Handle, null!, IntPtr.Zero), "Mixer.SetPostMixCallback(IntPtr, null, IntPtr) must clear the mixer callback.");
        GC.KeepAlive(callback);
    }

    public static void Generate_ReturnsMixedBytesForMemoryMixer()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.Generate), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(IntPtr), typeof(int)]);
        TestAssert.NotNull(method, "Mixer.Generate(IntPtr, IntPtr, int) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_Generate");

        using MixerScope mixer = MixerScope.CreateMemoryMixer();
        using TrackScope track = TrackScope.Create(mixer);
        using AudioScope audio = AudioScope.LoadWav(mixer.Handle);
        using PinnedBytesScope buffer = PinnedBytesScope.Create(new byte[512]);
        TestAssert.True(SDL3.Mixer.SetTrackAudio(track.Handle, audio.Handle), "Mixer.SetTrackAudio must assign audio before Generate.");
        TestAssert.True(SDL3.Mixer.PlayTrack(track.Handle, 0), "Mixer.PlayTrack must start playback before Generate.");
        int generated = SDL3.Mixer.Generate(mixer.Handle, buffer.Handle, buffer.Length);
        TestAssert.True(generated >= 0, "Mixer.Generate(IntPtr, IntPtr, int) must generate memory mixer output for a playing track.");
        TestAssert.Equal(-1, SDL3.Mixer.Generate(IntPtr.Zero, buffer.Handle, buffer.Length), "Mixer.Generate(IntPtr.Zero, IntPtr, int) must fail safely for a null mixer.");
    }

    public static void CreateAudioDecoder_ReturnsDecoderForWavFile()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.CreateAudioDecoder), BindingFlags.Public | BindingFlags.Static, [typeof(string), typeof(uint)]);
        TestAssert.NotNull(method, "Mixer.CreateAudioDecoder(string, uint) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_CreateAudioDecoder");
        AssertStringParameterMarshal(method!, "path");

        string path = CreateTempAudioPath(".wav");
        File.WriteAllBytes(path, CreateSilentWavData());

        try
        {
            using MixerInitScope _ = MixerInitScope.Create();
            IntPtr decoder = SDL3.Mixer.CreateAudioDecoder(path, 0);

            try
            {
                TestAssert.True(decoder != IntPtr.Zero, "Mixer.CreateAudioDecoder(string, uint) must create a decoder for a valid WAV file.");
            }
            finally
            {
                DestroyAudioDecoderIfNeeded(decoder);
            }
        }
        finally
        {
            File.Delete(path);
        }
    }

    public static void CreateAudioDecoderIO_ReturnsDecoderForWavStream()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.CreateAudioDecoderIO), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(bool), typeof(uint)]);
        TestAssert.NotNull(method, "Mixer.CreateAudioDecoderIO(IntPtr, bool, uint) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_CreateAudioDecoder_IO");
        AssertBooleanParameterMarshal(method!, "closeio");

        using MixerInitScope _ = MixerInitScope.Create();
        using ConstMemIOStreamScope stream = ConstMemIOStreamScope.Create(CreateSilentWavData());
        IntPtr decoder = SDL3.Mixer.CreateAudioDecoderIO(stream.Handle, closeio: false, props: 0);

        try
        {
            TestAssert.True(decoder != IntPtr.Zero, "Mixer.CreateAudioDecoderIO(IntPtr, bool, uint) must create a decoder for a valid WAV stream.");
        }
        finally
        {
            DestroyAudioDecoderIfNeeded(decoder);
        }
    }

    public static void DestroyAudioDecoder_DestroysDecoderAndAllowsNull()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.DestroyAudioDecoder), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.DestroyAudioDecoder(IntPtr) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_DestroyAudioDecoder");

        SDL3.Mixer.DestroyAudioDecoder(IntPtr.Zero);
        using MixerInitScope _ = MixerInitScope.Create();
        using ConstMemIOStreamScope stream = ConstMemIOStreamScope.Create(CreateSilentWavData());
        IntPtr decoder = SDL3.Mixer.CreateAudioDecoderIO(stream.Handle, closeio: false, props: 0);
        TestAssert.True(decoder != IntPtr.Zero, "Mixer.CreateAudioDecoderIO must create a decoder for DestroyAudioDecoder.");
        SDL3.Mixer.DestroyAudioDecoder(decoder);
    }

    public static void GetAudioDecoderProperties_ReturnsPropertiesForDecoder()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.GetAudioDecoderProperties), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.GetAudioDecoderProperties(IntPtr) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_GetAudioDecoderProperties");

        using MixerInitScope _ = MixerInitScope.Create();
        using ConstMemIOStreamScope stream = ConstMemIOStreamScope.Create(CreateSilentWavData());
        IntPtr decoder = SDL3.Mixer.CreateAudioDecoderIO(stream.Handle, closeio: false, props: 0);

        try
        {
            TestAssert.True(decoder != IntPtr.Zero, "Mixer.CreateAudioDecoderIO must create a decoder before GetAudioDecoderProperties.");
            uint properties = SDL3.Mixer.GetAudioDecoderProperties(decoder);
            TestAssert.True(properties != 0, "Mixer.GetAudioDecoderProperties(IntPtr) must return a properties ID for a valid decoder.");
        }
        finally
        {
            DestroyAudioDecoderIfNeeded(decoder);
        }
    }

    public static void GetAudioDecoderFormat_ReturnsFormatForDecoder()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.GetAudioDecoderFormat), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.GetAudioDecoderFormat(IntPtr, IntPtr) method must be public static.");
        AssertMixerBoolReturnMethodMetadata(method!, "MIX_GetAudioDecoderFormat");

        using MixerInitScope _ = MixerInitScope.Create();
        using ConstMemIOStreamScope stream = ConstMemIOStreamScope.Create(CreateSilentWavData());
        using AudioSpecScope spec = AudioSpecScope.CreateZeroed();
        IntPtr decoder = SDL3.Mixer.CreateAudioDecoderIO(stream.Handle, closeio: false, props: 0);

        try
        {
            TestAssert.True(decoder != IntPtr.Zero, "Mixer.CreateAudioDecoderIO must create a decoder before GetAudioDecoderFormat.");
            TestAssert.True(SDL3.Mixer.GetAudioDecoderFormat(decoder, spec.Handle), "Mixer.GetAudioDecoderFormat(IntPtr, IntPtr) must read the decoder format.");
            SDL3.SDL.AudioSpec value = spec.Read();
            TestAssert.Equal(SDL3.SDL.AudioFormat.AudioS16LE, value.Format, "Mixer.GetAudioDecoderFormat(IntPtr, IntPtr) must report the WAV fixture sample format.");
            TestAssert.Equal(2, value.Channels, "Mixer.GetAudioDecoderFormat(IntPtr, IntPtr) must report the WAV fixture channel count.");
            TestAssert.Equal(48_000, value.Freq, "Mixer.GetAudioDecoderFormat(IntPtr, IntPtr) must report the WAV fixture frequency.");
        }
        finally
        {
            DestroyAudioDecoderIfNeeded(decoder);
        }
    }

    public static void DecodeAudio_DecodesWavData()
    {
        MethodInfo? method = typeof(SDL3.Mixer).GetMethod(nameof(SDL3.Mixer.DecodeAudio), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(IntPtr), typeof(int), typeof(IntPtr)]);
        TestAssert.NotNull(method, "Mixer.DecodeAudio(IntPtr, IntPtr, int, IntPtr) method must be public static.");
        AssertMixerLibraryImport(method!, "MIX_DecodeAudio");

        using MixerInitScope _ = MixerInitScope.Create();
        using ConstMemIOStreamScope stream = ConstMemIOStreamScope.Create(CreateSilentWavData());
        using AudioSpecScope spec = AudioSpecScope.Create();
        using PinnedBytesScope buffer = PinnedBytesScope.Create(new byte[512]);
        IntPtr decoder = SDL3.Mixer.CreateAudioDecoderIO(stream.Handle, closeio: false, props: 0);

        try
        {
            TestAssert.True(decoder != IntPtr.Zero, "Mixer.CreateAudioDecoderIO must create a decoder before DecodeAudio.");
            int decoded = SDL3.Mixer.DecodeAudio(decoder, buffer.Handle, buffer.Length, spec.Handle);
            TestAssert.True(decoded >= 0, "Mixer.DecodeAudio(IntPtr, IntPtr, int, IntPtr) must decode or reach EOF without an unrecoverable error.");
            TestAssert.Equal(-1, SDL3.Mixer.DecodeAudio(IntPtr.Zero, buffer.Handle, buffer.Length, spec.Handle), "Mixer.DecodeAudio(IntPtr.Zero, IntPtr, int, IntPtr) must fail safely for a null decoder.");
        }
        finally
        {
            DestroyAudioDecoderIfNeeded(decoder);
        }
    }

    private static void AssertMixerBoolReturnMethodMetadata(MethodInfo method, string entryPoint)
    {
        AssertMixerLibraryImport(method, entryPoint);

        MarshalAsAttribute? returnMarshalAs = method.ReturnParameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(returnMarshalAs, $"Mixer.{method.Name} return value must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.I1, returnMarshalAs!.Value, $"Mixer.{method.Name} return value must use I1 marshalling.");
    }

    private static void AssertMixerLibraryImport(MethodInfo method, string entryPoint)
    {
        LibraryImportAttribute? libraryImport = method.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, $"Mixer.{method.Name} must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3_mixer", libraryImport!.LibraryName, $"Mixer.{method.Name} must import from SDL3_mixer.");
        TestAssert.Equal(entryPoint, libraryImport.EntryPoint, $"Mixer.{method.Name} must bind {entryPoint}.");
    }

    private static void AssertBooleanParameterMarshal(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"Mixer.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.I1, marshalAs!.Value, $"Mixer.{method.Name} parameter {parameterName} must use I1 marshalling.");
    }

    private static void AssertStringParameterMarshal(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"Mixer.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.LPUTF8Str, marshalAs!.Value, $"Mixer.{method.Name} parameter {parameterName} must use UTF-8 string marshalling.");
    }

    private static void AssertByRefIntPtrParameter(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        TestAssert.True(parameter.ParameterType.IsByRef, $"Mixer.{method.Name} parameter {parameterName} must remain by-reference.");
        TestAssert.Equal(typeof(IntPtr), parameter.ParameterType.GetElementType(), $"Mixer.{method.Name} parameter {parameterName} must be based on IntPtr.");
    }

    private static void AssertCallbackParameter(MethodInfo method, string parameterName, Type expectedType)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        TestAssert.Equal(expectedType, parameter.ParameterType, $"Mixer.{method.Name} parameter {parameterName} must use the expected callback delegate type.");
        UnmanagedFunctionPointerAttribute? unmanagedFunctionPointer = expectedType.GetCustomAttribute<UnmanagedFunctionPointerAttribute>();
        TestAssert.NotNull(unmanagedFunctionPointer, $"Mixer.{method.Name} callback delegate must keep unmanaged function pointer metadata.");
        TestAssert.Equal(CallingConvention.Cdecl, unmanagedFunctionPointer!.CallingConvention, $"Mixer.{method.Name} callback delegate must use cdecl calling convention.");
    }

    private static void DestroyMixerIfNeeded(IntPtr mixer)
    {
        if (mixer != IntPtr.Zero)
        {
            SDL3.Mixer.DestroyMixer(mixer);
        }
    }

    private static void DestroyAudioIfNeeded(IntPtr audio)
    {
        if (audio != IntPtr.Zero)
        {
            SDL3.Mixer.DestroyAudio(audio);
        }
    }

    private static void DestroyTrackIfNeeded(IntPtr track)
    {
        if (track != IntPtr.Zero)
        {
            SDL3.Mixer.DestroyTrack(track);
        }
    }

    private static void DestroyGroupIfNeeded(IntPtr group)
    {
        if (group != IntPtr.Zero)
        {
            SDL3.Mixer.DestroyGroup(group);
        }
    }

    private static void DestroyAudioDecoderIfNeeded(IntPtr decoder)
    {
        if (decoder != IntPtr.Zero)
        {
            SDL3.Mixer.DestroyAudioDecoder(decoder);
        }
    }

    private static string CreateTempAudioPath(string extension)
    {
        return Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}{extension}");
    }

    private static byte[] CreateSilentWavData()
    {
        const int channels = 2;
        const int sampleRate = 48_000;
        const int bitsPerSample = 16;
        const int frameCount = 8;
        const int blockAlign = channels * bitsPerSample / 8;
        const int dataSize = frameCount * blockAlign;
        byte[] data = new byte[44 + dataSize];

        Encoding.ASCII.GetBytes("RIFF").CopyTo(data, 0);
        BinaryPrimitives.WriteUInt32LittleEndian(data.AsSpan(4), (uint)(36 + dataSize));
        Encoding.ASCII.GetBytes("WAVE").CopyTo(data, 8);
        Encoding.ASCII.GetBytes("fmt ").CopyTo(data, 12);
        BinaryPrimitives.WriteUInt32LittleEndian(data.AsSpan(16), 16u);
        BinaryPrimitives.WriteUInt16LittleEndian(data.AsSpan(20), 1);
        BinaryPrimitives.WriteUInt16LittleEndian(data.AsSpan(22), channels);
        BinaryPrimitives.WriteUInt32LittleEndian(data.AsSpan(24), sampleRate);
        BinaryPrimitives.WriteUInt32LittleEndian(data.AsSpan(28), sampleRate * blockAlign);
        BinaryPrimitives.WriteUInt16LittleEndian(data.AsSpan(32), blockAlign);
        BinaryPrimitives.WriteUInt16LittleEndian(data.AsSpan(34), bitsPerSample);
        Encoding.ASCII.GetBytes("data").CopyTo(data, 36);
        BinaryPrimitives.WriteUInt32LittleEndian(data.AsSpan(40), dataSize);

        return data;
    }

    private sealed class MixerInitScope : IDisposable
    {
        private MixerInitScope()
        {
        }

        public static MixerInitScope Create()
        {
            TestAssert.True(SDL3.Mixer.Init(), "Mixer.Init() must succeed for mixer tests.");
            return new MixerInitScope();
        }

        public void Dispose()
        {
            SDL3.Mixer.Quit();
        }
    }

    private sealed class AudioSpecScope : IDisposable
    {
        private AudioSpecScope(IntPtr handle)
        {
            Handle = handle;
        }

        public IntPtr Handle { get; private set; }

        public static AudioSpecScope Create()
        {
            SDL3.SDL.AudioSpec spec = CreateMixerAudioSpec();
            IntPtr handle = Marshal.AllocHGlobal(Marshal.SizeOf<SDL3.SDL.AudioSpec>());
            Marshal.StructureToPtr(spec, handle, fDeleteOld: false);
            return new AudioSpecScope(handle);
        }

        public static AudioSpecScope CreateZeroed()
        {
            IntPtr handle = Marshal.AllocHGlobal(Marshal.SizeOf<SDL3.SDL.AudioSpec>());
            Marshal.StructureToPtr(default(SDL3.SDL.AudioSpec), handle, fDeleteOld: false);
            return new AudioSpecScope(handle);
        }

        public SDL3.SDL.AudioSpec Read()
        {
            return Marshal.PtrToStructure<SDL3.SDL.AudioSpec>(Handle);
        }

        public void Dispose()
        {
            if (Handle != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(Handle);
                Handle = IntPtr.Zero;
            }
        }
    }

    private static SDL3.SDL.AudioSpec CreateMixerAudioSpec()
    {
        return new SDL3.SDL.AudioSpec
        {
            Format = SDL3.SDL.AudioFormat.AudioF32LE,
            Channels = 2,
            Freq = 48_000
        };
    }

    private sealed class MixerScope : IDisposable
    {
        private readonly MixerInitScope init;
        private readonly AudioSpecScope spec;

        private MixerScope(MixerInitScope init, AudioSpecScope spec, IntPtr mixer)
        {
            this.init = init;
            this.spec = spec;
            Handle = mixer;
        }

        public IntPtr Handle { get; private set; }

        public static MixerScope CreateMemoryMixer()
        {
            MixerInitScope init = MixerInitScope.Create();
            AudioSpecScope spec = AudioSpecScope.Create();
            IntPtr mixer = SDL3.Mixer.CreateMixer(spec.Handle);

            if (mixer == IntPtr.Zero)
            {
                spec.Dispose();
                init.Dispose();
                throw new InvalidOperationException("Mixer.CreateMixer(IntPtr) must create a memory mixer.");
            }

            return new MixerScope(init, spec, mixer);
        }

        public void Dispose()
        {
            DestroyMixerIfNeeded(Handle);
            Handle = IntPtr.Zero;
            spec.Dispose();
            init.Dispose();
        }
    }

    private sealed class AudioScope : IDisposable
    {
        private AudioScope(IntPtr audio)
        {
            Handle = audio;
        }

        public IntPtr Handle { get; private set; }

        public static AudioScope LoadWav(IntPtr mixer)
        {
            return new AudioScope(LoadWavHandle(mixer));
        }

        public static IntPtr LoadWavHandle(IntPtr mixer)
        {
            using ConstMemIOStreamScope stream = ConstMemIOStreamScope.Create(CreateSilentWavData());
            IntPtr audio = SDL3.Mixer.LoadAudioIO(mixer, stream.Handle, predecode: false, closeio: false);
            TestAssert.True(audio != IntPtr.Zero, "Mixer.LoadAudioIO must load the WAV fixture for audio-object tests.");
            return audio;
        }

        public void Dispose()
        {
            DestroyAudioIfNeeded(Handle);
            Handle = IntPtr.Zero;
        }
    }

    private sealed class TrackScope : IDisposable
    {
        private TrackScope(IntPtr track)
        {
            Handle = track;
        }

        public IntPtr Handle { get; private set; }

        public static TrackScope Create(MixerScope mixer)
        {
            IntPtr track = SDL3.Mixer.CreateTrack(mixer.Handle);
            TestAssert.True(track != IntPtr.Zero, "Mixer.CreateTrack(IntPtr) must create a track for mixer track tests.");
            return new TrackScope(track);
        }

        public void Dispose()
        {
            DestroyTrackIfNeeded(Handle);
            Handle = IntPtr.Zero;
        }
    }

    private sealed class GroupScope : IDisposable
    {
        private GroupScope(IntPtr group)
        {
            Handle = group;
        }

        public IntPtr Handle { get; private set; }

        public static GroupScope Create(MixerScope mixer)
        {
            IntPtr group = SDL3.Mixer.CreateGroup(mixer.Handle);
            TestAssert.True(group != IntPtr.Zero, "Mixer.CreateGroup(IntPtr) must create a group for mixer group tests.");
            return new GroupScope(group);
        }

        public void Dispose()
        {
            DestroyGroupIfNeeded(Handle);
            Handle = IntPtr.Zero;
        }
    }

    private sealed class AudioStreamScope : IDisposable
    {
        private AudioStreamScope(IntPtr stream)
        {
            Handle = stream;
        }

        public IntPtr Handle { get; private set; }

        public static AudioStreamScope Create()
        {
            SDL3.SDL.AudioSpec spec = CreateMixerAudioSpec();
            IntPtr stream = SDL3.SDL.CreateAudioStream(in spec, in spec);
            TestAssert.True(stream != IntPtr.Zero, "SDL.CreateAudioStream must create a stream for mixer track tests.");
            return new AudioStreamScope(stream);
        }

        public void Dispose()
        {
            if (Handle != IntPtr.Zero)
            {
                SDL3.SDL.DestroyAudioStream(Handle);
                Handle = IntPtr.Zero;
            }
        }
    }

    private sealed class PropertiesScope : IDisposable
    {
        private PropertiesScope(uint handle)
        {
            Handle = handle;
        }

        public uint Handle { get; private set; }

        public static PropertiesScope Create()
        {
            uint properties = SDL3.SDL.CreateProperties();
            TestAssert.True(properties != 0, "SDL.CreateProperties must create a properties object.");
            return new PropertiesScope(properties);
        }

        public void Dispose()
        {
            if (Handle != 0)
            {
                SDL3.SDL.DestroyProperties(Handle);
                Handle = 0;
            }
        }
    }

    private sealed class PinnedBytesScope : IDisposable
    {
        private readonly GCHandle pinnedData;

        private PinnedBytesScope(byte[] data, GCHandle pinnedData)
        {
            Data = data;
            this.pinnedData = pinnedData;
        }

        public byte[] Data { get; }

        public IntPtr Handle => pinnedData.AddrOfPinnedObject();

        public int Length => Data.Length;

        public static PinnedBytesScope Create(byte[] data)
        {
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            return new PinnedBytesScope(data, handle);
        }

        public void Dispose()
        {
            pinnedData.Free();
        }
    }

    private sealed class ConstMemIOStreamScope : IDisposable
    {
        private readonly PinnedBytesScope data;

        private ConstMemIOStreamScope(PinnedBytesScope data, IntPtr stream)
        {
            this.data = data;
            Handle = stream;
        }

        public IntPtr Handle { get; private set; }

        public static ConstMemIOStreamScope Create(byte[] bytes)
        {
            PinnedBytesScope data = PinnedBytesScope.Create(bytes);
            IntPtr stream = SDL3.SDL.IOFromConstMem(data.Handle, (UIntPtr)data.Length);

            if (stream == IntPtr.Zero)
            {
                data.Dispose();
                throw new InvalidOperationException("SDL.IOFromConstMem must create a stream for mixer tests.");
            }

            return new ConstMemIOStreamScope(data, stream);
        }

        public void Dispose()
        {
            if (Handle != IntPtr.Zero)
            {
                SDL3.SDL.CloseIO(Handle);
                Handle = IntPtr.Zero;
            }

            data.Dispose();
        }
    }
}
