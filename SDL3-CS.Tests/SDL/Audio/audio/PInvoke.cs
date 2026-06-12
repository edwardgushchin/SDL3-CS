using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Audio.Audio;

internal static class PInvokeTests
{
    private static IntPtr nextPointer;
    private static IntPtr capturedStream;
    private static IntPtr[]? capturedStreams;
    private static IntPtr[]? capturedChannelBuffers;
    private static IntPtr capturedSrcSpecPointer;
    private static IntPtr capturedDstSpecPointer;
    private static IntPtr capturedBufferPointer;
    private static IntPtr capturedSourcePointer;
    private static IntPtr capturedDstBufferPointer;
    private static IntPtr capturedSrcBufferPointer;
    private static IntPtr capturedUserdata;
    private static string? capturedPath;
    private static int nextCount;
    private static int capturedNumStreams;
    private static int capturedLen;
    private static int capturedCount;
    private static int capturedNumChannels;
    private static int capturedNumSamples;
    private static int capturedIndex;
    private static int nextInt;
    private static uint capturedDevice;
    private static uint nextDevice;
    private static uint nextAudioLen;
    private static uint capturedBufferLength;
    private static IntPtr capturedSpecPointer;
    private static SDL3.SDL.AudioSpec capturedSpec;
    private static SDL3.SDL.AudioSpec capturedSrcSpec;
    private static SDL3.SDL.AudioSpec capturedDstSpec;
    private static SDL3.SDL.AudioSpec nextSpec;
    private static SDL3.SDL.AudioSpec nextDstSpec;
    private static int nextSampleFrames;
    private static bool nextBool;
    private static float nextFloat;
    private static float capturedGain;
    private static float capturedRatio;
    private static float capturedVolume;
    private static int capturedCallCount;
    private static uint nextProperties;
    private static int[]? capturedChannelMap;
    private static byte[]? capturedByteBuffer;
    private static SDL3.SDL.AudioStreamDataCompleteCallback? capturedDataCompleteCallback;
    private static SDL3.SDL.AudioStreamCallback? capturedAudioStreamCallback;
    private static SDL3.SDL.AudioPostmixCallback? capturedAudioPostmixCallback;
    private static bool capturedCloseIO;
    private static SDL3.SDL.AudioFormat capturedFormat;

    public static void GetNumAudioDrivers_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetNumAudioDrivers");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetNumAudioDrivers");

        using NativeHookScope _ = NativeHookScope.Install("GetNumAudioDriversNativeFunction", nameof(CaptureGetNumAudioDrivers));
        int result = SDL3.SDL.GetNumAudioDrivers();

        TestAssert.Equal(7, result, "SDL.GetNumAudioDrivers must return the native hook value.");
    }

    public static void SDL_GetAudioDriver_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetAudioDriver");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetAudioDriver");
    }

    public static void GetAudioDriver_ReturnsStringAndNull()
    {
        using NativeHookScope _ = NativeHookScope.Install("GetAudioDriverNativeFunction", nameof(CaptureGetAudioDriver));

        string? value = CaptureUtf8String(() => SDL3.SDL.GetAudioDriver(3), "wasapi");
        TestAssert.Equal("wasapi", value, "SDL.GetAudioDriver must convert UTF-8 native driver name.");
        TestAssert.Equal(3, capturedIndex, "SDL.GetAudioDriver must forward index.");

        nextPointer = IntPtr.Zero;
        TestAssert.Equal<string?>(null, SDL3.SDL.GetAudioDriver(4), "SDL.GetAudioDriver must return null for native null.");
        TestAssert.Equal(4, capturedIndex, "SDL.GetAudioDriver must forward index for null result.");
    }

    public static void SDL_GetCurrentAudioDriver_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetCurrentAudioDriver");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetCurrentAudioDriver");
    }

    public static void GetCurrentAudioDriver_ReturnsStringAndNull()
    {
        using NativeHookScope _ = NativeHookScope.Install("GetCurrentAudioDriverNativeFunction", nameof(CapturePointer));

        string? value = CaptureUtf8String(SDL3.SDL.GetCurrentAudioDriver, "directsound");
        TestAssert.Equal("directsound", value, "SDL.GetCurrentAudioDriver must convert UTF-8 native driver name.");

        nextPointer = IntPtr.Zero;
        TestAssert.Equal<string?>(null, SDL3.SDL.GetCurrentAudioDriver(), "SDL.GetCurrentAudioDriver must return null for native null.");
    }

    public static void SDL_GetAudioPlaybackDevices_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetAudioPlaybackDevices");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetAudioPlaybackDevices");
    }

    public static void GetAudioPlaybackDevices_ReturnsDeviceArrayAndNull()
    {
        using NativeHookScope _ = NativeHookScope.Install("GetAudioPlaybackDevicesNativeFunction", nameof(CaptureDeviceArray));

        nextPointer = CreateNativeUIntArray(10, 20);
        nextCount = 2;
        uint[]? devices = SDL3.SDL.GetAudioPlaybackDevices(out int count);

        TestAssert.NotNull(devices, "SDL.GetAudioPlaybackDevices must return native device IDs.");
        TestAssert.Equal(2, count, "SDL.GetAudioPlaybackDevices must forward native count.");
        TestAssert.Equal(10u, devices![0], "SDL.GetAudioPlaybackDevices must copy device 0.");
        TestAssert.Equal(20u, devices[1], "SDL.GetAudioPlaybackDevices must copy device 1.");

        nextPointer = IntPtr.Zero;
        nextCount = 0;
        TestAssert.Equal<uint[]?>(null, SDL3.SDL.GetAudioPlaybackDevices(out count), "SDL.GetAudioPlaybackDevices must return null for native null.");
        TestAssert.Equal(0, count, "SDL.GetAudioPlaybackDevices must return native count for null pointer.");
    }

    public static void SDL_GetAudioRecordingDevices_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetAudioRecordingDevices");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetAudioRecordingDevices");
    }

    public static void GetAudioRecordingDevices_ReturnsDeviceArrayAndNull()
    {
        using NativeHookScope _ = NativeHookScope.Install("GetAudioRecordingDevicesNativeFunction", nameof(CaptureDeviceArray));

        nextPointer = CreateNativeUIntArray(30, 40, 50);
        nextCount = 3;
        uint[]? devices = SDL3.SDL.GetAudioRecordingDevices(out int count);

        TestAssert.NotNull(devices, "SDL.GetAudioRecordingDevices must return native device IDs.");
        TestAssert.Equal(3, count, "SDL.GetAudioRecordingDevices must forward native count.");
        TestAssert.Equal(30u, devices![0], "SDL.GetAudioRecordingDevices must copy device 0.");
        TestAssert.Equal(40u, devices[1], "SDL.GetAudioRecordingDevices must copy device 1.");
        TestAssert.Equal(50u, devices[2], "SDL.GetAudioRecordingDevices must copy device 2.");

        nextPointer = IntPtr.Zero;
        nextCount = 0;
        TestAssert.Equal<uint[]?>(null, SDL3.SDL.GetAudioRecordingDevices(out count), "SDL.GetAudioRecordingDevices must return null for native null.");
        TestAssert.Equal(0, count, "SDL.GetAudioRecordingDevices must return native count for null pointer.");
    }

    public static void SDL_GetAudioDeviceName_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetAudioDeviceName");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetAudioDeviceName");
    }

    public static void GetAudioDeviceName_ReturnsStringAndNull()
    {
        using NativeHookScope _ = NativeHookScope.Install("GetAudioDeviceNameNativeFunction", nameof(CaptureGetAudioDeviceName));

        string? value = CaptureUtf8String(() => SDL3.SDL.GetAudioDeviceName(77), "Speakers");
        TestAssert.Equal("Speakers", value, "SDL.GetAudioDeviceName must convert UTF-8 native device name.");
        TestAssert.Equal(77u, capturedDevice, "SDL.GetAudioDeviceName must forward device ID.");

        nextPointer = IntPtr.Zero;
        TestAssert.Equal<string?>(null, SDL3.SDL.GetAudioDeviceName(78), "SDL.GetAudioDeviceName must return null for native null.");
        TestAssert.Equal(78u, capturedDevice, "SDL.GetAudioDeviceName must forward device ID for null result.");
    }

    public static void GetAudioDeviceFormat_ForwardsDeviceAndOutputs()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetAudioDeviceFormat");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetAudioDeviceFormat");
        AssertBoolReturnMarshal(nativeMethod);

        nextBool = true;
        nextSampleFrames = 512;
        nextSpec = new SDL3.SDL.AudioSpec
        {
            Format = SDL3.SDL.AudioFormat.AudioF32LE,
            Channels = 2,
            Freq = 48000
        };

        using NativeHookScope _ = NativeHookScope.Install("GetAudioDeviceFormatNativeFunction", nameof(CaptureGetAudioDeviceFormat));
        bool result = SDL3.SDL.GetAudioDeviceFormat(88, out SDL3.SDL.AudioSpec spec, out int sampleFrames);

        TestAssert.Equal(true, result, "SDL.GetAudioDeviceFormat must return the native hook value.");
        TestAssert.Equal(88u, capturedDevice, "SDL.GetAudioDeviceFormat must forward device ID.");
        TestAssert.Equal(SDL3.SDL.AudioFormat.AudioF32LE, spec.Format, "SDL.GetAudioDeviceFormat must forward output format.");
        TestAssert.Equal(2, spec.Channels, "SDL.GetAudioDeviceFormat must forward output channels.");
        TestAssert.Equal(48000, spec.Freq, "SDL.GetAudioDeviceFormat must forward output frequency.");
        TestAssert.Equal(512, sampleFrames, "SDL.GetAudioDeviceFormat must forward output sample frames.");
    }

    public static void SDL_GetAudioDeviceChannelMap_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetAudioDeviceChannelMap");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetAudioDeviceChannelMap");
    }

    public static void GetAudioDeviceChannelMap_ReturnsChannelArrayAndNull()
    {
        using NativeHookScope _ = NativeHookScope.Install("GetAudioDeviceChannelMapNativeFunction", nameof(CaptureChannelMap));

        nextPointer = CreateNativeIntArray(1, 0, 2);
        nextCount = 3;
        int[]? channels = SDL3.SDL.GetAudioDeviceChannelMap(99, out int count);

        TestAssert.NotNull(channels, "SDL.GetAudioDeviceChannelMap must return native channel map.");
        TestAssert.Equal(3, count, "SDL.GetAudioDeviceChannelMap must forward native count.");
        TestAssert.Equal(1, channels![0], "SDL.GetAudioDeviceChannelMap must copy channel 0.");
        TestAssert.Equal(0, channels[1], "SDL.GetAudioDeviceChannelMap must copy channel 1.");
        TestAssert.Equal(2, channels[2], "SDL.GetAudioDeviceChannelMap must copy channel 2.");
        TestAssert.Equal(99u, capturedDevice, "SDL.GetAudioDeviceChannelMap must forward device ID.");

        nextPointer = IntPtr.Zero;
        nextCount = 0;
        TestAssert.Equal<int[]?>(null, SDL3.SDL.GetAudioDeviceChannelMap(100, out count), "SDL.GetAudioDeviceChannelMap must return null for native null.");
        TestAssert.Equal(0, count, "SDL.GetAudioDeviceChannelMap must return native count for null pointer.");
        TestAssert.Equal(100u, capturedDevice, "SDL.GetAudioDeviceChannelMap must forward device ID for null result.");
    }

    public static void OpenAudioDevice_WithSpec_ForwardsDeviceAndSpec()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_OpenAudioDevice", [typeof(uint), typeof(SDL3.SDL.AudioSpec).MakeByRefType()]);
        AssertSdlLibraryImport(nativeMethod, "SDL_OpenAudioDevice");

        nextDevice = 123;
        SDL3.SDL.AudioSpec spec = new()
        {
            Format = SDL3.SDL.AudioFormat.AudioS16LE,
            Channels = 2,
            Freq = 44100
        };

        using NativeHookScope _ = NativeHookScope.Install("OpenAudioDeviceWithSpecNativeFunction", nameof(CaptureOpenAudioDeviceWithSpec));
        uint result = SDL3.SDL.OpenAudioDevice(101, in spec);

        TestAssert.Equal(123u, result, "SDL.OpenAudioDevice(in AudioSpec) must return the native hook device ID.");
        TestAssert.Equal(101u, capturedDevice, "SDL.OpenAudioDevice(in AudioSpec) must forward device ID.");
        TestAssert.Equal(SDL3.SDL.AudioFormat.AudioS16LE, capturedSpec.Format, "SDL.OpenAudioDevice(in AudioSpec) must forward format.");
        TestAssert.Equal(2, capturedSpec.Channels, "SDL.OpenAudioDevice(in AudioSpec) must forward channels.");
        TestAssert.Equal(44100, capturedSpec.Freq, "SDL.OpenAudioDevice(in AudioSpec) must forward frequency.");
    }

    public static void OpenAudioDevice_WithPointer_ForwardsDeviceAndPointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_OpenAudioDevice", [typeof(uint), typeof(IntPtr)]);
        AssertSdlLibraryImport(nativeMethod, "SDL_OpenAudioDevice");

        nextDevice = 124;
        using NativeHookScope _ = NativeHookScope.Install("OpenAudioDeviceWithPointerNativeFunction", nameof(CaptureOpenAudioDeviceWithPointer));
        uint result = SDL3.SDL.OpenAudioDevice(102, (IntPtr)103);

        TestAssert.Equal(124u, result, "SDL.OpenAudioDevice(IntPtr) must return the native hook device ID.");
        TestAssert.Equal(102u, capturedDevice, "SDL.OpenAudioDevice(IntPtr) must forward device ID.");
        TestAssert.Equal((IntPtr)103, capturedSpecPointer, "SDL.OpenAudioDevice(IntPtr) must forward spec pointer.");
    }

    public static void IsAudioDevicePhysical_ReturnsNativeValue()
    {
        AssertBoolDeviceForwarder("SDL_IsAudioDevicePhysical", "SDL_IsAudioDevicePhysical", "IsAudioDevicePhysicalNativeFunction", SDL3.SDL.IsAudioDevicePhysical);
    }

    public static void IsAudioDevicePlayback_ReturnsNativeValue()
    {
        AssertBoolDeviceForwarder("SDL_IsAudioDevicePlayback", "SDL_IsAudioDevicePlayback", "IsAudioDevicePlaybackNativeFunction", SDL3.SDL.IsAudioDevicePlayback);
    }

    public static void PauseAudioDevice_ReturnsNativeValue()
    {
        AssertBoolDeviceForwarder("SDL_PauseAudioDevice", "SDL_PauseAudioDevice", "PauseAudioDeviceNativeFunction", SDL3.SDL.PauseAudioDevice);
    }

    public static void ResumeAudioDevice_ReturnsNativeValue()
    {
        AssertBoolDeviceForwarder("SDL_ResumeAudioDevice", "SDL_ResumeAudioDevice", "ResumeAudioDeviceNativeFunction", SDL3.SDL.ResumeAudioDevice);
    }

    public static void AudioDevicePaused_ReturnsNativeValue()
    {
        AssertBoolDeviceForwarder("SDL_AudioDevicePaused", "SDL_AudioDevicePaused", "AudioDevicePausedNativeFunction", SDL3.SDL.AudioDevicePaused);
    }

    public static void GetAudioDeviceGain_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetAudioDeviceGain");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetAudioDeviceGain");

        nextFloat = 0.75f;
        using NativeHookScope _ = NativeHookScope.Install("GetAudioDeviceGainNativeFunction", nameof(CaptureGetAudioDeviceGain));
        float result = SDL3.SDL.GetAudioDeviceGain(104);

        TestAssert.Equal(0.75f, result, "SDL.GetAudioDeviceGain must return the native hook gain.");
        TestAssert.Equal(104u, capturedDevice, "SDL.GetAudioDeviceGain must forward device ID.");
    }

    public static void SetAudioDeviceGain_ForwardsDeviceAndGain()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetAudioDeviceGain");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetAudioDeviceGain");
        AssertBoolReturnMarshal(nativeMethod);

        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("SetAudioDeviceGainNativeFunction", nameof(CaptureSetAudioDeviceGain));
        bool result = SDL3.SDL.SetAudioDeviceGain(105, 0.5f);

        TestAssert.Equal(true, result, "SDL.SetAudioDeviceGain must return the native hook value.");
        TestAssert.Equal(105u, capturedDevice, "SDL.SetAudioDeviceGain must forward device ID.");
        TestAssert.Equal(0.5f, capturedGain, "SDL.SetAudioDeviceGain must forward gain.");
    }

    public static void CloseAudioDevice_ForwardsDevice()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CloseAudioDevice");
        AssertSdlLibraryImport(nativeMethod, "SDL_CloseAudioDevice");

        capturedCallCount = 0;
        using NativeHookScope _ = NativeHookScope.Install("CloseAudioDeviceNativeFunction", nameof(CaptureCloseAudioDevice));
        SDL3.SDL.CloseAudioDevice(106);

        TestAssert.Equal(106u, capturedDevice, "SDL.CloseAudioDevice must forward device ID.");
        TestAssert.Equal(1, capturedCallCount, "SDL.CloseAudioDevice must call native hook once.");
    }

    public static void BindAudioStreams_ForwardsDeviceStreamsAndCount()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_BindAudioStreams");
        AssertSdlLibraryImport(nativeMethod, "SDL_BindAudioStreams");
        AssertBoolReturnMarshal(nativeMethod);
        AssertArrayParameterMarshal(nativeMethod, "streams", 2);

        nextBool = true;
        IntPtr[] streams = [(IntPtr)201, (IntPtr)202];
        using NativeHookScope _ = NativeHookScope.Install("BindAudioStreamsNativeFunction", nameof(CaptureBindAudioStreams));
        bool result = SDL3.SDL.BindAudioStreams(200, streams, streams.Length);

        TestAssert.Equal(true, result, "SDL.BindAudioStreams must return the native hook value.");
        TestAssert.Equal(200u, capturedDevice, "SDL.BindAudioStreams must forward device ID.");
        TestAssert.Equal(2, capturedNumStreams, "SDL.BindAudioStreams must forward stream count.");
        TestAssert.NotNull(capturedStreams, "SDL.BindAudioStreams must forward streams.");
        TestAssert.Equal((IntPtr)201, capturedStreams![0], "SDL.BindAudioStreams must forward stream 0.");
        TestAssert.Equal((IntPtr)202, capturedStreams[1], "SDL.BindAudioStreams must forward stream 1.");
    }

    public static void BindAudioStream_ForwardsDeviceAndStream()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_BindAudioStream");
        AssertSdlLibraryImport(nativeMethod, "SDL_BindAudioStream");
        AssertBoolReturnMarshal(nativeMethod);

        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("BindAudioStreamNativeFunction", nameof(CaptureBindAudioStream));
        bool result = SDL3.SDL.BindAudioStream(203, (IntPtr)204);

        TestAssert.Equal(true, result, "SDL.BindAudioStream must return the native hook value.");
        TestAssert.Equal(203u, capturedDevice, "SDL.BindAudioStream must forward device ID.");
        TestAssert.Equal((IntPtr)204, capturedStream, "SDL.BindAudioStream must forward stream.");
    }

    public static void UnbindAudioStreams_ForwardsStreamsAndCount()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_UnbindAudioStreams");
        AssertSdlLibraryImport(nativeMethod, "SDL_UnbindAudioStreams");
        AssertArrayParameterMarshal(nativeMethod, "streams", 1);

        IntPtr[] streams = [(IntPtr)205, (IntPtr)206];
        capturedCallCount = 0;
        using NativeHookScope _ = NativeHookScope.Install("UnbindAudioStreamsNativeFunction", nameof(CaptureUnbindAudioStreams));
        SDL3.SDL.UnbindAudioStreams(streams, streams.Length);

        TestAssert.Equal(1, capturedCallCount, "SDL.UnbindAudioStreams must call native hook once.");
        TestAssert.Equal(2, capturedNumStreams, "SDL.UnbindAudioStreams must forward stream count.");
        TestAssert.NotNull(capturedStreams, "SDL.UnbindAudioStreams must forward streams.");
        TestAssert.Equal((IntPtr)205, capturedStreams![0], "SDL.UnbindAudioStreams must forward stream 0.");
        TestAssert.Equal((IntPtr)206, capturedStreams[1], "SDL.UnbindAudioStreams must forward stream 1.");

        SDL3.SDL.UnbindAudioStreams(null, 0);
        TestAssert.Equal(2, capturedCallCount, "SDL.UnbindAudioStreams must also forward null stream arrays.");
        TestAssert.Equal<IntPtr[]?>(null, capturedStreams, "SDL.UnbindAudioStreams must forward null streams.");
        TestAssert.Equal(0, capturedNumStreams, "SDL.UnbindAudioStreams must forward zero count for null streams.");
    }

    public static void UnbindAudioStream_ForwardsStream()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_UnbindAudioStream");
        AssertSdlLibraryImport(nativeMethod, "SDL_UnbindAudioStream");

        capturedCallCount = 0;
        using NativeHookScope _ = NativeHookScope.Install("UnbindAudioStreamNativeFunction", nameof(CaptureUnbindAudioStream));
        SDL3.SDL.UnbindAudioStream((IntPtr)207);

        TestAssert.Equal(1, capturedCallCount, "SDL.UnbindAudioStream must call native hook once.");
        TestAssert.Equal((IntPtr)207, capturedStream, "SDL.UnbindAudioStream must forward stream.");
    }

    public static void GetAudioStreamDevice_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetAudioStreamDevice");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetAudioStreamDevice");

        nextDevice = 208;
        using NativeHookScope _ = NativeHookScope.Install("GetAudioStreamDeviceNativeFunction", nameof(CaptureGetAudioStreamDevice));
        uint result = SDL3.SDL.GetAudioStreamDevice((IntPtr)209);

        TestAssert.Equal(208u, result, "SDL.GetAudioStreamDevice must return the native hook device ID.");
        TestAssert.Equal((IntPtr)209, capturedStream, "SDL.GetAudioStreamDevice must forward stream.");
    }

    public static void CreateAudioStream_WithPointers_ForwardsSpecs()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CreateAudioStream", [typeof(IntPtr), typeof(IntPtr)]);
        AssertSdlLibraryImport(nativeMethod, "SDL_CreateAudioStream");

        nextPointer = (IntPtr)300;
        using NativeHookScope _ = NativeHookScope.Install("CreateAudioStreamWithPointersNativeFunction", nameof(CaptureCreateAudioStreamWithPointers));
        IntPtr result = SDL3.SDL.CreateAudioStream((IntPtr)301, (IntPtr)302);

        TestAssert.Equal((IntPtr)300, result, "SDL.CreateAudioStream(IntPtr, IntPtr) must return the native hook stream.");
        TestAssert.Equal((IntPtr)301, capturedSrcSpecPointer, "SDL.CreateAudioStream(IntPtr, IntPtr) must forward src spec pointer.");
        TestAssert.Equal((IntPtr)302, capturedDstSpecPointer, "SDL.CreateAudioStream(IntPtr, IntPtr) must forward dst spec pointer.");
    }

    public static void CreateAudioStream_WithSpecs_ForwardsSpecs()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CreateAudioStream", [typeof(SDL3.SDL.AudioSpec).MakeByRefType(), typeof(SDL3.SDL.AudioSpec).MakeByRefType()]);
        AssertSdlLibraryImport(nativeMethod, "SDL_CreateAudioStream");

        nextPointer = (IntPtr)303;
        SDL3.SDL.AudioSpec srcSpec = CreateAudioSpec(SDL3.SDL.AudioFormat.AudioS16LE, 1, 22050);
        SDL3.SDL.AudioSpec dstSpec = CreateAudioSpec(SDL3.SDL.AudioFormat.AudioF32LE, 2, 48000);

        using NativeHookScope _ = NativeHookScope.Install("CreateAudioStreamWithSpecsNativeFunction", nameof(CaptureCreateAudioStreamWithSpecs));
        IntPtr result = SDL3.SDL.CreateAudioStream(in srcSpec, in dstSpec);

        TestAssert.Equal((IntPtr)303, result, "SDL.CreateAudioStream(in AudioSpec, in AudioSpec) must return the native hook stream.");
        AssertAudioSpec(srcSpec, capturedSrcSpec, "SDL.CreateAudioStream(in AudioSpec, in AudioSpec) must forward src spec.");
        AssertAudioSpec(dstSpec, capturedDstSpec, "SDL.CreateAudioStream(in AudioSpec, in AudioSpec) must forward dst spec.");
    }

    public static void GetAudioStreamProperties_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetAudioStreamProperties");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetAudioStreamProperties");

        nextProperties = 304;
        using NativeHookScope _ = NativeHookScope.Install("GetAudioStreamPropertiesNativeFunction", nameof(CaptureGetAudioStreamProperties));
        uint result = SDL3.SDL.GetAudioStreamProperties((IntPtr)305);

        TestAssert.Equal(304u, result, "SDL.GetAudioStreamProperties must return the native hook properties ID.");
        TestAssert.Equal((IntPtr)305, capturedStream, "SDL.GetAudioStreamProperties must forward stream.");
    }

    public static void GetAudioStreamFormat_ForwardsStreamAndOutputs()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetAudioStreamFormat");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetAudioStreamFormat");
        AssertBoolReturnMarshal(nativeMethod);

        nextBool = true;
        nextSpec = CreateAudioSpec(SDL3.SDL.AudioFormat.AudioS8, 1, 11025);
        nextDstSpec = CreateAudioSpec(SDL3.SDL.AudioFormat.AudioS32LE, 2, 44100);

        using NativeHookScope _ = NativeHookScope.Install("GetAudioStreamFormatNativeFunction", nameof(CaptureGetAudioStreamFormat));
        bool result = SDL3.SDL.GetAudioStreamFormat((IntPtr)306, out SDL3.SDL.AudioSpec srcSpec, out SDL3.SDL.AudioSpec dstSpec);

        TestAssert.Equal(true, result, "SDL.GetAudioStreamFormat must return the native hook value.");
        TestAssert.Equal((IntPtr)306, capturedStream, "SDL.GetAudioStreamFormat must forward stream.");
        AssertAudioSpec(nextSpec, srcSpec, "SDL.GetAudioStreamFormat must forward src output spec.");
        AssertAudioSpec(nextDstSpec, dstSpec, "SDL.GetAudioStreamFormat must forward dst output spec.");
    }

    public static void SetAudioStreamFormat_WithPointers_ForwardsStreamAndSpecs()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetAudioStreamFormat", [typeof(IntPtr), typeof(IntPtr), typeof(IntPtr)]);
        AssertSdlLibraryImport(nativeMethod, "SDL_SetAudioStreamFormat");
        AssertBoolReturnMarshal(nativeMethod);

        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("SetAudioStreamFormatWithPointersNativeFunction", nameof(CaptureSetAudioStreamFormatWithPointers));
        bool result = SDL3.SDL.SetAudioStreamFormat((IntPtr)307, (IntPtr)308, (IntPtr)309);

        TestAssert.Equal(true, result, "SDL.SetAudioStreamFormat(IntPtr, IntPtr, IntPtr) must return the native hook value.");
        TestAssert.Equal((IntPtr)307, capturedStream, "SDL.SetAudioStreamFormat(IntPtr, IntPtr, IntPtr) must forward stream.");
        TestAssert.Equal((IntPtr)308, capturedSrcSpecPointer, "SDL.SetAudioStreamFormat(IntPtr, IntPtr, IntPtr) must forward src spec pointer.");
        TestAssert.Equal((IntPtr)309, capturedDstSpecPointer, "SDL.SetAudioStreamFormat(IntPtr, IntPtr, IntPtr) must forward dst spec pointer.");
    }

    public static void SetAudioStreamFormat_WithSpecs_ForwardsStreamAndSpecs()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetAudioStreamFormat", [typeof(IntPtr), typeof(SDL3.SDL.AudioSpec).MakeByRefType(), typeof(SDL3.SDL.AudioSpec).MakeByRefType()]);
        AssertSdlLibraryImport(nativeMethod, "SDL_SetAudioStreamFormat");
        AssertBoolReturnMarshal(nativeMethod);

        nextBool = true;
        SDL3.SDL.AudioSpec srcSpec = CreateAudioSpec(SDL3.SDL.AudioFormat.AudioS16BE, 4, 32000);
        SDL3.SDL.AudioSpec dstSpec = CreateAudioSpec(SDL3.SDL.AudioFormat.AudioF32BE, 6, 96000);

        using NativeHookScope _ = NativeHookScope.Install("SetAudioStreamFormatWithSpecsNativeFunction", nameof(CaptureSetAudioStreamFormatWithSpecs));
        bool result = SDL3.SDL.SetAudioStreamFormat((IntPtr)310, in srcSpec, in dstSpec);

        TestAssert.Equal(true, result, "SDL.SetAudioStreamFormat(IntPtr, in AudioSpec, in AudioSpec) must return the native hook value.");
        TestAssert.Equal((IntPtr)310, capturedStream, "SDL.SetAudioStreamFormat(IntPtr, in AudioSpec, in AudioSpec) must forward stream.");
        AssertAudioSpec(srcSpec, capturedSrcSpec, "SDL.SetAudioStreamFormat(IntPtr, in AudioSpec, in AudioSpec) must forward src spec.");
        AssertAudioSpec(dstSpec, capturedDstSpec, "SDL.SetAudioStreamFormat(IntPtr, in AudioSpec, in AudioSpec) must forward dst spec.");
    }

    public static void GetAudioStreamFrequencyRatio_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetAudioStreamFrequencyRatio");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetAudioStreamFrequencyRatio");

        nextFloat = 1.25f;
        using NativeHookScope _ = NativeHookScope.Install("GetAudioStreamFrequencyRatioNativeFunction", nameof(CaptureGetAudioStreamFloat));
        float result = SDL3.SDL.GetAudioStreamFrequencyRatio((IntPtr)311);

        TestAssert.Equal(1.25f, result, "SDL.GetAudioStreamFrequencyRatio must return the native hook ratio.");
        TestAssert.Equal((IntPtr)311, capturedStream, "SDL.GetAudioStreamFrequencyRatio must forward stream.");
    }

    public static void SetAudioStreamFrequencyRatio_ForwardsStreamAndRatio()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetAudioStreamFrequencyRatio");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetAudioStreamFrequencyRatio");
        AssertBoolReturnMarshal(nativeMethod);

        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("SetAudioStreamFrequencyRatioNativeFunction", nameof(CaptureSetAudioStreamFrequencyRatio));
        bool result = SDL3.SDL.SetAudioStreamFrequencyRatio((IntPtr)312, 0.75f);

        TestAssert.Equal(true, result, "SDL.SetAudioStreamFrequencyRatio must return the native hook value.");
        TestAssert.Equal((IntPtr)312, capturedStream, "SDL.SetAudioStreamFrequencyRatio must forward stream.");
        TestAssert.Equal(0.75f, capturedRatio, "SDL.SetAudioStreamFrequencyRatio must forward ratio.");
    }

    public static void GetAudioStreamGain_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetAudioStreamGain");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetAudioStreamGain");

        nextFloat = 0.25f;
        using NativeHookScope _ = NativeHookScope.Install("GetAudioStreamGainNativeFunction", nameof(CaptureGetAudioStreamFloat));
        float result = SDL3.SDL.GetAudioStreamGain((IntPtr)313);

        TestAssert.Equal(0.25f, result, "SDL.GetAudioStreamGain must return the native hook gain.");
        TestAssert.Equal((IntPtr)313, capturedStream, "SDL.GetAudioStreamGain must forward stream.");
    }

    public static void SetAudioStreamGain_ForwardsStreamAndGain()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetAudioStreamGain");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetAudioStreamGain");
        AssertBoolReturnMarshal(nativeMethod);

        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("SetAudioStreamGainNativeFunction", nameof(CaptureSetAudioStreamGain));
        bool result = SDL3.SDL.SetAudioStreamGain((IntPtr)314, 0.6f);

        TestAssert.Equal(true, result, "SDL.SetAudioStreamGain must return the native hook value.");
        TestAssert.Equal((IntPtr)314, capturedStream, "SDL.SetAudioStreamGain must forward stream.");
        TestAssert.Equal(0.6f, capturedGain, "SDL.SetAudioStreamGain must forward gain.");
    }

    public static void SDL_GetAudioStreamInputChannelMap_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetAudioStreamInputChannelMap");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetAudioStreamInputChannelMap");
    }

    public static void GetAudioStreamInputChannelMap_ReturnsChannelArrayAndNull()
    {
        using NativeHookScope _ = NativeHookScope.Install("GetAudioStreamInputChannelMapNativeFunction", nameof(CaptureStreamChannelMap));

        nextPointer = CreateNativeIntArray(0, 1);
        nextCount = 2;
        int[]? channels = SDL3.SDL.GetAudioStreamInputChannelMap((IntPtr)315, out int count);

        TestAssert.NotNull(channels, "SDL.GetAudioStreamInputChannelMap must return native channel map.");
        TestAssert.Equal(2, count, "SDL.GetAudioStreamInputChannelMap must forward native count.");
        TestAssert.Equal(0, channels![0], "SDL.GetAudioStreamInputChannelMap must copy channel 0.");
        TestAssert.Equal(1, channels[1], "SDL.GetAudioStreamInputChannelMap must copy channel 1.");
        TestAssert.Equal((IntPtr)315, capturedStream, "SDL.GetAudioStreamInputChannelMap must forward stream.");

        nextPointer = IntPtr.Zero;
        nextCount = 0;
        TestAssert.Equal<int[]?>(null, SDL3.SDL.GetAudioStreamInputChannelMap((IntPtr)316, out count), "SDL.GetAudioStreamInputChannelMap must return null for native null.");
        TestAssert.Equal(0, count, "SDL.GetAudioStreamInputChannelMap must return native count for null pointer.");
        TestAssert.Equal((IntPtr)316, capturedStream, "SDL.GetAudioStreamInputChannelMap must forward stream for null result.");
    }

    public static void SDL_GetAudioStreamOutputChannelMap_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetAudioStreamOutputChannelMap");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetAudioStreamOutputChannelMap");
    }

    public static void GetAudioStreamOutputChannelMap_ReturnsChannelArrayAndNull()
    {
        using NativeHookScope _ = NativeHookScope.Install("GetAudioStreamOutputChannelMapNativeFunction", nameof(CaptureStreamChannelMap));

        nextPointer = CreateNativeIntArray(2, 1, 0);
        nextCount = 3;
        int[]? channels = SDL3.SDL.GetAudioStreamOutputChannelMap((IntPtr)317, out int count);

        TestAssert.NotNull(channels, "SDL.GetAudioStreamOutputChannelMap must return native channel map.");
        TestAssert.Equal(3, count, "SDL.GetAudioStreamOutputChannelMap must forward native count.");
        TestAssert.Equal(2, channels![0], "SDL.GetAudioStreamOutputChannelMap must copy channel 0.");
        TestAssert.Equal(1, channels[1], "SDL.GetAudioStreamOutputChannelMap must copy channel 1.");
        TestAssert.Equal(0, channels[2], "SDL.GetAudioStreamOutputChannelMap must copy channel 2.");
        TestAssert.Equal((IntPtr)317, capturedStream, "SDL.GetAudioStreamOutputChannelMap must forward stream.");

        nextPointer = IntPtr.Zero;
        nextCount = 0;
        TestAssert.Equal<int[]?>(null, SDL3.SDL.GetAudioStreamOutputChannelMap((IntPtr)318, out count), "SDL.GetAudioStreamOutputChannelMap must return null for native null.");
        TestAssert.Equal(0, count, "SDL.GetAudioStreamOutputChannelMap must return native count for null pointer.");
        TestAssert.Equal((IntPtr)318, capturedStream, "SDL.GetAudioStreamOutputChannelMap must forward stream for null result.");
    }

    public static void SetAudioStreamInputChannelMap_ForwardsStreamMapAndCount()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetAudioStreamInputChannelMap");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetAudioStreamInputChannelMap");
        AssertBoolReturnMarshal(nativeMethod);
        AssertArrayParameterMarshal(nativeMethod, "chmap", 2);

        nextBool = true;
        int[] chmap = [1, 0];
        using NativeHookScope _ = NativeHookScope.Install("SetAudioStreamInputChannelMapNativeFunction", nameof(CaptureSetAudioStreamChannelMap));
        bool result = SDL3.SDL.SetAudioStreamInputChannelMap((IntPtr)401, chmap, chmap.Length);

        TestAssert.Equal(true, result, "SDL.SetAudioStreamInputChannelMap must return the native hook value.");
        TestAssert.Equal((IntPtr)401, capturedStream, "SDL.SetAudioStreamInputChannelMap must forward stream.");
        TestAssert.Equal(2, capturedCount, "SDL.SetAudioStreamInputChannelMap must forward count.");
        TestAssert.NotNull(capturedChannelMap, "SDL.SetAudioStreamInputChannelMap must forward channel map.");
        TestAssert.Equal(1, capturedChannelMap![0], "SDL.SetAudioStreamInputChannelMap must forward channel map item 0.");
        TestAssert.Equal(0, capturedChannelMap[1], "SDL.SetAudioStreamInputChannelMap must forward channel map item 1.");

        SDL3.SDL.SetAudioStreamInputChannelMap((IntPtr)402, null, 0);
        TestAssert.Equal((IntPtr)402, capturedStream, "SDL.SetAudioStreamInputChannelMap must forward stream for null map.");
        TestAssert.Equal<int[]?>(null, capturedChannelMap, "SDL.SetAudioStreamInputChannelMap must forward null channel map.");
        TestAssert.Equal(0, capturedCount, "SDL.SetAudioStreamInputChannelMap must forward zero count for null map.");
    }

    public static void SetAudioStreamOutputChannelMap_ForwardsStreamMapAndCount()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetAudioStreamOutputChannelMap");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetAudioStreamOutputChannelMap");
        AssertBoolReturnMarshal(nativeMethod);
        AssertArrayParameterMarshal(nativeMethod, "chmap", 2);

        nextBool = true;
        int[] chmap = [2, 1, 0];
        using NativeHookScope _ = NativeHookScope.Install("SetAudioStreamOutputChannelMapNativeFunction", nameof(CaptureSetAudioStreamChannelMap));
        bool result = SDL3.SDL.SetAudioStreamOutputChannelMap((IntPtr)403, chmap, chmap.Length);

        TestAssert.Equal(true, result, "SDL.SetAudioStreamOutputChannelMap must return the native hook value.");
        TestAssert.Equal((IntPtr)403, capturedStream, "SDL.SetAudioStreamOutputChannelMap must forward stream.");
        TestAssert.Equal(3, capturedCount, "SDL.SetAudioStreamOutputChannelMap must forward count.");
        TestAssert.NotNull(capturedChannelMap, "SDL.SetAudioStreamOutputChannelMap must forward channel map.");
        TestAssert.Equal(2, capturedChannelMap![0], "SDL.SetAudioStreamOutputChannelMap must forward channel map item 0.");
        TestAssert.Equal(1, capturedChannelMap[1], "SDL.SetAudioStreamOutputChannelMap must forward channel map item 1.");
        TestAssert.Equal(0, capturedChannelMap[2], "SDL.SetAudioStreamOutputChannelMap must forward channel map item 2.");
    }

    public static void PutAudioStreamData_WithPointer_ForwardsBufferAndLength()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_PutAudioStreamData", [typeof(IntPtr), typeof(IntPtr), typeof(int)]);
        AssertSdlLibraryImport(nativeMethod, "SDL_PutAudioStreamData");
        AssertBoolReturnMarshal(nativeMethod);

        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("PutAudioStreamDataWithPointerNativeFunction", nameof(CapturePutAudioStreamDataWithPointer));
        bool result = SDL3.SDL.PutAudioStreamData((IntPtr)404, (IntPtr)405, 6);

        TestAssert.Equal(true, result, "SDL.PutAudioStreamData(IntPtr) must return the native hook value.");
        TestAssert.Equal((IntPtr)404, capturedStream, "SDL.PutAudioStreamData(IntPtr) must forward stream.");
        TestAssert.Equal((IntPtr)405, capturedBufferPointer, "SDL.PutAudioStreamData(IntPtr) must forward buffer pointer.");
        TestAssert.Equal(6, capturedLen, "SDL.PutAudioStreamData(IntPtr) must forward length.");
    }

    public static void PutAudioStreamDataNoCopy_ForwardsBufferCallbackAndUserdata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_PutAudioStreamDataNoCopy");
        AssertSdlLibraryImport(nativeMethod, "SDL_PutAudioStreamDataNoCopy");
        AssertBoolReturnMarshal(nativeMethod);

        nextBool = true;
        SDL3.SDL.AudioStreamDataCompleteCallback callback = TestAudioStreamDataCompleteCallback;
        using NativeHookScope _ = NativeHookScope.Install("PutAudioStreamDataNoCopyNativeFunction", nameof(CapturePutAudioStreamDataNoCopy));
        bool result = SDL3.SDL.PutAudioStreamDataNoCopy((IntPtr)406, (IntPtr)407, 8, callback, (IntPtr)408);

        TestAssert.Equal(true, result, "SDL.PutAudioStreamDataNoCopy must return the native hook value.");
        TestAssert.Equal((IntPtr)406, capturedStream, "SDL.PutAudioStreamDataNoCopy must forward stream.");
        TestAssert.Equal((IntPtr)407, capturedBufferPointer, "SDL.PutAudioStreamDataNoCopy must forward buffer pointer.");
        TestAssert.Equal(8, capturedLen, "SDL.PutAudioStreamDataNoCopy must forward length.");
        TestAssert.NotNull(capturedDataCompleteCallback, "SDL.PutAudioStreamDataNoCopy must forward callback.");
        TestAssert.Equal((IntPtr)408, capturedUserdata, "SDL.PutAudioStreamDataNoCopy must forward userdata.");
    }

    public static void PutAudioStreamData_WithBytes_ForwardsBufferAndLength()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_PutAudioStreamData", [typeof(IntPtr), typeof(byte[]), typeof(int)]);
        AssertSdlLibraryImport(nativeMethod, "SDL_PutAudioStreamData");
        AssertBoolReturnMarshal(nativeMethod);
        AssertArrayParameterMarshal(nativeMethod, "buf", 2);

        nextBool = true;
        byte[] buffer = [1, 2, 3, 4];
        using NativeHookScope _ = NativeHookScope.Install("PutAudioStreamDataWithBytesNativeFunction", nameof(CapturePutAudioStreamDataWithBytes));
        bool result = SDL3.SDL.PutAudioStreamData((IntPtr)409, buffer, buffer.Length);

        TestAssert.Equal(true, result, "SDL.PutAudioStreamData(byte[]) must return the native hook value.");
        TestAssert.Equal((IntPtr)409, capturedStream, "SDL.PutAudioStreamData(byte[]) must forward stream.");
        TestAssert.Equal(4, capturedLen, "SDL.PutAudioStreamData(byte[]) must forward length.");
        TestAssert.NotNull(capturedByteBuffer, "SDL.PutAudioStreamData(byte[]) must forward buffer.");
        TestAssert.Equal((byte)1, capturedByteBuffer![0], "SDL.PutAudioStreamData(byte[]) must forward buffer item 0.");
        TestAssert.Equal((byte)4, capturedByteBuffer[3], "SDL.PutAudioStreamData(byte[]) must forward buffer item 3.");
    }

    public static void PutAudioStreamPlanarData_ForwardsChannelBuffersAndCounts()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_PutAudioStreamPlanarData");
        AssertSdlDllImport(nativeMethod, "SDL_PutAudioStreamPlanarData");
        AssertBoolReturnMarshal(nativeMethod);
        AssertPointerArrayParameterMarshal(nativeMethod, "channelBuffers");

        nextBool = true;
        IntPtr[] channelBuffers = [(IntPtr)410, IntPtr.Zero, (IntPtr)411];
        using NativeHookScope _ = NativeHookScope.Install("PutAudioStreamPlanarDataNativeFunction", nameof(CapturePutAudioStreamPlanarData));
        bool result = SDL3.SDL.PutAudioStreamPlanarData((IntPtr)412, channelBuffers, 3, 128);

        TestAssert.Equal(true, result, "SDL.PutAudioStreamPlanarData must return the native hook value.");
        TestAssert.Equal((IntPtr)412, capturedStream, "SDL.PutAudioStreamPlanarData must forward stream.");
        TestAssert.Equal(3, capturedNumChannels, "SDL.PutAudioStreamPlanarData must forward channel count.");
        TestAssert.Equal(128, capturedNumSamples, "SDL.PutAudioStreamPlanarData must forward sample count.");
        TestAssert.NotNull(capturedChannelBuffers, "SDL.PutAudioStreamPlanarData must forward channel buffers.");
        TestAssert.Equal((IntPtr)410, capturedChannelBuffers![0], "SDL.PutAudioStreamPlanarData must forward channel buffer 0.");
        TestAssert.Equal(IntPtr.Zero, capturedChannelBuffers[1], "SDL.PutAudioStreamPlanarData must forward null channel buffer.");
        TestAssert.Equal((IntPtr)411, capturedChannelBuffers[2], "SDL.PutAudioStreamPlanarData must forward channel buffer 2.");
    }

    public static void GetAudioStreamData_WithPointer_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetAudioStreamData", [typeof(IntPtr), typeof(IntPtr), typeof(int)]);
        AssertSdlLibraryImport(nativeMethod, "SDL_GetAudioStreamData");

        nextInt = 12;
        using NativeHookScope _ = NativeHookScope.Install("GetAudioStreamDataWithPointerNativeFunction", nameof(CaptureGetAudioStreamDataWithPointer));
        int result = SDL3.SDL.GetAudioStreamData((IntPtr)413, (IntPtr)414, 16);

        TestAssert.Equal(12, result, "SDL.GetAudioStreamData(IntPtr) must return the native hook value.");
        TestAssert.Equal((IntPtr)413, capturedStream, "SDL.GetAudioStreamData(IntPtr) must forward stream.");
        TestAssert.Equal((IntPtr)414, capturedBufferPointer, "SDL.GetAudioStreamData(IntPtr) must forward buffer pointer.");
        TestAssert.Equal(16, capturedLen, "SDL.GetAudioStreamData(IntPtr) must forward length.");
    }

    public static void GetAudioStreamData_WithBytes_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetAudioStreamData", [typeof(IntPtr), typeof(byte[]), typeof(int)]);
        AssertSdlLibraryImport(nativeMethod, "SDL_GetAudioStreamData");
        AssertArrayParameterMarshal(nativeMethod, "buf", 2);

        nextInt = 3;
        byte[] buffer = [0, 0, 0];
        using NativeHookScope _ = NativeHookScope.Install("GetAudioStreamDataWithBytesNativeFunction", nameof(CaptureGetAudioStreamDataWithBytes));
        int result = SDL3.SDL.GetAudioStreamData((IntPtr)415, buffer, buffer.Length);

        TestAssert.Equal(3, result, "SDL.GetAudioStreamData(byte[]) must return the native hook value.");
        TestAssert.Equal((IntPtr)415, capturedStream, "SDL.GetAudioStreamData(byte[]) must forward stream.");
        TestAssert.Equal(3, capturedLen, "SDL.GetAudioStreamData(byte[]) must forward length.");
        TestAssert.NotNull(capturedByteBuffer, "SDL.GetAudioStreamData(byte[]) must forward buffer.");
        TestAssert.Equal((byte)0, capturedByteBuffer![0], "SDL.GetAudioStreamData(byte[]) must forward buffer item 0.");
    }

    public static void GetAudioStreamAvailable_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetAudioStreamAvailable");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetAudioStreamAvailable");

        nextInt = 64;
        using NativeHookScope _ = NativeHookScope.Install("GetAudioStreamAvailableNativeFunction", nameof(CaptureGetAudioStreamInt));
        int result = SDL3.SDL.GetAudioStreamAvailable((IntPtr)416);

        TestAssert.Equal(64, result, "SDL.GetAudioStreamAvailable must return the native hook value.");
        TestAssert.Equal((IntPtr)416, capturedStream, "SDL.GetAudioStreamAvailable must forward stream.");
    }

    public static void GetAudioStreamQueued_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetAudioStreamQueued");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetAudioStreamQueued");

        nextInt = 32;
        using NativeHookScope _ = NativeHookScope.Install("GetAudioStreamQueuedNativeFunction", nameof(CaptureGetAudioStreamInt));
        int result = SDL3.SDL.GetAudioStreamQueued((IntPtr)417);

        TestAssert.Equal(32, result, "SDL.GetAudioStreamQueued must return the native hook value.");
        TestAssert.Equal((IntPtr)417, capturedStream, "SDL.GetAudioStreamQueued must forward stream.");
    }

    public static void FlushAudioStream_ReturnsNativeValue()
    {
        AssertBoolStreamForwarder("SDL_FlushAudioStream", "SDL_FlushAudioStream", "FlushAudioStreamNativeFunction", SDL3.SDL.FlushAudioStream);
    }

    public static void ClearAudioStream_ReturnsNativeValue()
    {
        AssertBoolStreamForwarder("SDL_ClearAudioStream", "SDL_ClearAudioStream", "ClearAudioStreamNativeFunction", SDL3.SDL.ClearAudioStream);
    }

    public static void PauseAudioStreamDevice_ReturnsNativeValue()
    {
        AssertBoolStreamForwarder("SDL_PauseAudioStreamDevice", "SDL_PauseAudioStreamDevice", "PauseAudioStreamDeviceNativeFunction", SDL3.SDL.PauseAudioStreamDevice);
    }

    public static void ResumeAudioStreamDevice_ReturnsNativeValue()
    {
        AssertBoolStreamForwarder("SDL_ResumeAudioStreamDevice", "SDL_ResumeAudioStreamDevice", "ResumeAudioStreamDeviceNativeFunction", SDL3.SDL.ResumeAudioStreamDevice);
    }

    public static void AudioStreamDevicePaused_ReturnsNativeValue()
    {
        AssertBoolStreamForwarder("SDL_AudioStreamDevicePaused", "SDL_AudioStreamDevicePaused", "AudioStreamDevicePausedNativeFunction", SDL3.SDL.AudioStreamDevicePaused);
    }

    public static void LockAudioStream_ReturnsNativeValue()
    {
        AssertBoolStreamForwarder("SDL_LockAudioStream", "SDL_LockAudioStream", "LockAudioStreamNativeFunction", SDL3.SDL.LockAudioStream);
    }

    public static void UnlockAudioStream_ReturnsNativeValue()
    {
        AssertBoolStreamForwarder("SDL_UnlockAudioStream", "SDL_UnlockAudioStream", "UnlockAudioStreamNativeFunction", SDL3.SDL.UnlockAudioStream);
    }

    public static void SetAudioStreamGetCallback_ForwardsCallbackAndUserdata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetAudioStreamGetCallback");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetAudioStreamGetCallback");
        AssertBoolReturnMarshal(nativeMethod);

        nextBool = true;
        SDL3.SDL.AudioStreamCallback callback = TestAudioStreamCallback;
        using NativeHookScope _ = NativeHookScope.Install("SetAudioStreamGetCallbackNativeFunction", nameof(CaptureSetAudioStreamCallback));
        bool result = SDL3.SDL.SetAudioStreamGetCallback((IntPtr)419, callback, (IntPtr)420);

        TestAssert.Equal(true, result, "SDL.SetAudioStreamGetCallback must return the native hook value.");
        TestAssert.Equal((IntPtr)419, capturedStream, "SDL.SetAudioStreamGetCallback must forward stream.");
        TestAssert.NotNull(capturedAudioStreamCallback, "SDL.SetAudioStreamGetCallback must forward callback.");
        TestAssert.Equal((IntPtr)420, capturedUserdata, "SDL.SetAudioStreamGetCallback must forward userdata.");

        SDL3.SDL.SetAudioStreamGetCallback((IntPtr)421, null, (IntPtr)422);
        TestAssert.Equal((IntPtr)421, capturedStream, "SDL.SetAudioStreamGetCallback must forward stream for null callback.");
        TestAssert.Equal<SDL3.SDL.AudioStreamCallback?>(null, capturedAudioStreamCallback, "SDL.SetAudioStreamGetCallback must forward null callback.");
        TestAssert.Equal((IntPtr)422, capturedUserdata, "SDL.SetAudioStreamGetCallback must forward userdata for null callback.");
    }

    public static void SetAudioStreamPutCallback_ForwardsCallbackAndUserdata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetAudioStreamPutCallback");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetAudioStreamPutCallback");
        AssertBoolReturnMarshal(nativeMethod);

        nextBool = true;
        SDL3.SDL.AudioStreamCallback callback = TestAudioStreamCallback;
        using NativeHookScope _ = NativeHookScope.Install("SetAudioStreamPutCallbackNativeFunction", nameof(CaptureSetAudioStreamCallback));
        bool result = SDL3.SDL.SetAudioStreamPutCallback((IntPtr)423, callback, (IntPtr)424);

        TestAssert.Equal(true, result, "SDL.SetAudioStreamPutCallback must return the native hook value.");
        TestAssert.Equal((IntPtr)423, capturedStream, "SDL.SetAudioStreamPutCallback must forward stream.");
        TestAssert.NotNull(capturedAudioStreamCallback, "SDL.SetAudioStreamPutCallback must forward callback.");
        TestAssert.Equal((IntPtr)424, capturedUserdata, "SDL.SetAudioStreamPutCallback must forward userdata.");
    }

    public static void DestroyAudioStream_ForwardsStream()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_DestroyAudioStream");
        AssertSdlLibraryImport(nativeMethod, "SDL_DestroyAudioStream");

        capturedCallCount = 0;
        using NativeHookScope _ = NativeHookScope.Install("DestroyAudioStreamNativeFunction", nameof(CaptureDestroyAudioStream));
        SDL3.SDL.DestroyAudioStream((IntPtr)425);

        TestAssert.Equal((IntPtr)425, capturedStream, "SDL.DestroyAudioStream must forward stream.");
        TestAssert.Equal(1, capturedCallCount, "SDL.DestroyAudioStream must call the native hook once.");
    }

    public static void OpenAudioDeviceStream_WithSpec_ForwardsDeviceSpecCallbackAndUserdata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_OpenAudioDeviceStream", [typeof(uint), typeof(SDL3.SDL.AudioSpec).MakeByRefType(), typeof(SDL3.SDL.AudioStreamCallback), typeof(IntPtr)]);
        AssertSdlLibraryImport(nativeMethod, "SDL_OpenAudioDeviceStream");

        nextPointer = (IntPtr)426;
        SDL3.SDL.AudioSpec spec = CreateAudioSpec(SDL3.SDL.AudioFormat.AudioF32LE, 2, 48000);
        SDL3.SDL.AudioStreamCallback callback = TestAudioStreamCallback;
        using NativeHookScope _ = NativeHookScope.Install("OpenAudioDeviceStreamWithSpecNativeFunction", nameof(CaptureOpenAudioDeviceStreamWithSpec));
        IntPtr result = SDL3.SDL.OpenAudioDeviceStream(27u, in spec, callback, (IntPtr)427);

        TestAssert.Equal((IntPtr)426, result, "SDL.OpenAudioDeviceStream(in AudioSpec) must return the native hook stream.");
        TestAssert.Equal(27u, capturedDevice, "SDL.OpenAudioDeviceStream(in AudioSpec) must forward device.");
        AssertAudioSpec(spec, capturedSpec, "SDL.OpenAudioDeviceStream(in AudioSpec) must forward spec.");
        TestAssert.NotNull(capturedAudioStreamCallback, "SDL.OpenAudioDeviceStream(in AudioSpec) must forward callback.");
        TestAssert.Equal((IntPtr)427, capturedUserdata, "SDL.OpenAudioDeviceStream(in AudioSpec) must forward userdata.");
    }

    public static void OpenAudioDeviceStream_WithPointer_ForwardsDeviceSpecPointerCallbackAndUserdata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_OpenAudioDeviceStream", [typeof(uint), typeof(IntPtr), typeof(SDL3.SDL.AudioStreamCallback), typeof(IntPtr)]);
        AssertSdlLibraryImport(nativeMethod, "SDL_OpenAudioDeviceStream");

        nextPointer = (IntPtr)428;
        using NativeHookScope _ = NativeHookScope.Install("OpenAudioDeviceStreamWithPointerNativeFunction", nameof(CaptureOpenAudioDeviceStreamWithPointer));
        IntPtr result = SDL3.SDL.OpenAudioDeviceStream(28u, (IntPtr)429, null, (IntPtr)430);

        TestAssert.Equal((IntPtr)428, result, "SDL.OpenAudioDeviceStream(IntPtr) must return the native hook stream.");
        TestAssert.Equal(28u, capturedDevice, "SDL.OpenAudioDeviceStream(IntPtr) must forward device.");
        TestAssert.Equal((IntPtr)429, capturedSpecPointer, "SDL.OpenAudioDeviceStream(IntPtr) must forward spec pointer.");
        TestAssert.Equal<SDL3.SDL.AudioStreamCallback?>(null, capturedAudioStreamCallback, "SDL.OpenAudioDeviceStream(IntPtr) must forward null callback.");
        TestAssert.Equal((IntPtr)430, capturedUserdata, "SDL.OpenAudioDeviceStream(IntPtr) must forward userdata.");
    }

    public static void SetAudioPostmixCallback_ForwardsDeviceCallbackAndUserdata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetAudioPostmixCallback");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetAudioPostmixCallback");
        AssertBoolReturnMarshal(nativeMethod);

        nextBool = true;
        SDL3.SDL.AudioPostmixCallback callback = TestAudioPostmixCallback;
        using NativeHookScope _ = NativeHookScope.Install("SetAudioPostmixCallbackNativeFunction", nameof(CaptureSetAudioPostmixCallback));
        bool result = SDL3.SDL.SetAudioPostmixCallback(29u, callback, (IntPtr)431);

        TestAssert.Equal(true, result, "SDL.SetAudioPostmixCallback must return the native hook value.");
        TestAssert.Equal(29u, capturedDevice, "SDL.SetAudioPostmixCallback must forward device.");
        TestAssert.NotNull(capturedAudioPostmixCallback, "SDL.SetAudioPostmixCallback must forward callback.");
        TestAssert.Equal((IntPtr)431, capturedUserdata, "SDL.SetAudioPostmixCallback must forward userdata.");

        SDL3.SDL.SetAudioPostmixCallback(30u, null, (IntPtr)432);
        TestAssert.Equal(30u, capturedDevice, "SDL.SetAudioPostmixCallback must forward device for null callback.");
        TestAssert.Equal<SDL3.SDL.AudioPostmixCallback?>(null, capturedAudioPostmixCallback, "SDL.SetAudioPostmixCallback must forward null callback.");
        TestAssert.Equal((IntPtr)432, capturedUserdata, "SDL.SetAudioPostmixCallback must forward userdata for null callback.");
    }

    public static void LoadWAVIO_ForwardsSourceCloseFlagAndOutputs()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_LoadWAVIO");
        AssertSdlLibraryImport(nativeMethod, "SDL_LoadWAV_IO");
        AssertBoolReturnMarshal(nativeMethod);
        AssertBoolParameterMarshal(nativeMethod, "closeio");

        nextBool = true;
        nextSpec = CreateAudioSpec(SDL3.SDL.AudioFormat.AudioS16LE, 2, 44100);
        nextPointer = (IntPtr)433;
        nextAudioLen = 2048;
        using NativeHookScope _ = NativeHookScope.Install("LoadWAVIONativeFunction", nameof(CaptureLoadWAVIO));
        bool result = SDL3.SDL.LoadWAVIO((IntPtr)434, true, out SDL3.SDL.AudioSpec spec, out IntPtr audioBuf, out uint audioLen);

        TestAssert.Equal(true, result, "SDL.LoadWAVIO must return the native hook value.");
        TestAssert.Equal((IntPtr)434, capturedSourcePointer, "SDL.LoadWAVIO must forward source pointer.");
        TestAssert.Equal(true, capturedCloseIO, "SDL.LoadWAVIO must forward closeio.");
        AssertAudioSpec(nextSpec, spec, "SDL.LoadWAVIO must forward output spec.");
        TestAssert.Equal((IntPtr)433, audioBuf, "SDL.LoadWAVIO must forward output audio buffer.");
        TestAssert.Equal(2048u, audioLen, "SDL.LoadWAVIO must forward output audio length.");
    }

    public static void LoadWAV_ForwardsPathAndOutputs()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_LoadWAV");
        AssertSdlLibraryImport(nativeMethod, "SDL_LoadWAV");
        AssertBoolReturnMarshal(nativeMethod);
        AssertStringParameterMarshal(nativeMethod, "path", UnmanagedType.LPUTF8Str);

        nextBool = true;
        nextSpec = CreateAudioSpec(SDL3.SDL.AudioFormat.AudioF32LE, 1, 22050);
        nextPointer = (IntPtr)435;
        nextAudioLen = 4096;
        using NativeHookScope _ = NativeHookScope.Install("LoadWAVNativeFunction", nameof(CaptureLoadWAV));
        bool result = SDL3.SDL.LoadWAV("sample.wav", out SDL3.SDL.AudioSpec spec, out IntPtr audioBuf, out uint audioLen);

        TestAssert.Equal(true, result, "SDL.LoadWAV must return the native hook value.");
        TestAssert.Equal("sample.wav", capturedPath, "SDL.LoadWAV must forward path.");
        AssertAudioSpec(nextSpec, spec, "SDL.LoadWAV must forward output spec.");
        TestAssert.Equal((IntPtr)435, audioBuf, "SDL.LoadWAV must forward output audio buffer.");
        TestAssert.Equal(4096u, audioLen, "SDL.LoadWAV must forward output audio length.");
    }

    public static void MixAudio_ForwardsPointersFormatLengthAndVolume()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_MixAudio");
        AssertSdlLibraryImport(nativeMethod, "SDL_MixAudio");
        AssertBoolReturnMarshal(nativeMethod);

        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("MixAudioNativeFunction", nameof(CaptureMixAudio));
        bool result = SDL3.SDL.MixAudio((IntPtr)436, (IntPtr)437, SDL3.SDL.AudioFormat.AudioS16LE, 512, 0.75f);

        TestAssert.Equal(true, result, "SDL.MixAudio must return the native hook value.");
        TestAssert.Equal((IntPtr)436, capturedDstBufferPointer, "SDL.MixAudio must forward destination pointer.");
        TestAssert.Equal((IntPtr)437, capturedSrcBufferPointer, "SDL.MixAudio must forward source pointer.");
        TestAssert.Equal(SDL3.SDL.AudioFormat.AudioS16LE, capturedFormat, "SDL.MixAudio must forward format.");
        TestAssert.Equal(512u, capturedBufferLength, "SDL.MixAudio must forward length.");
        TestAssert.Equal(0.75f, capturedVolume, "SDL.MixAudio must forward volume.");
    }

    public static void ConvertAudioSamples_ForwardsSpecsDataAndOutputs()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_ConvertAudioSamples");
        AssertSdlLibraryImport(nativeMethod, "SDL_ConvertAudioSamples");
        AssertBoolReturnMarshal(nativeMethod);

        nextBool = true;
        nextPointer = (IntPtr)438;
        nextInt = 8192;
        SDL3.SDL.AudioSpec srcSpec = CreateAudioSpec(SDL3.SDL.AudioFormat.AudioS16LE, 2, 44100);
        SDL3.SDL.AudioSpec dstSpec = CreateAudioSpec(SDL3.SDL.AudioFormat.AudioF32LE, 2, 48000);
        using NativeHookScope _ = NativeHookScope.Install("ConvertAudioSamplesNativeFunction", nameof(CaptureConvertAudioSamples));
        bool result = SDL3.SDL.ConvertAudioSamples(in srcSpec, (IntPtr)439, 1024, in dstSpec, out IntPtr dstData, out int dstLen);

        TestAssert.Equal(true, result, "SDL.ConvertAudioSamples must return the native hook value.");
        AssertAudioSpec(srcSpec, capturedSrcSpec, "SDL.ConvertAudioSamples must forward src spec.");
        TestAssert.Equal((IntPtr)439, capturedSrcBufferPointer, "SDL.ConvertAudioSamples must forward source data pointer.");
        TestAssert.Equal(1024, capturedLen, "SDL.ConvertAudioSamples must forward source length.");
        AssertAudioSpec(dstSpec, capturedDstSpec, "SDL.ConvertAudioSamples must forward dst spec.");
        TestAssert.Equal((IntPtr)438, dstData, "SDL.ConvertAudioSamples must forward output data pointer.");
        TestAssert.Equal(8192, dstLen, "SDL.ConvertAudioSamples must forward output data length.");
    }

    public static void SDL_GetAudioFormatName_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetAudioFormatName");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetAudioFormatName");
    }

    public static void GetAudioFormatName_ReturnsStringAndEmpty()
    {
        using NativeHookScope _ = NativeHookScope.Install("GetAudioFormatNameNativeFunction", nameof(CaptureGetAudioFormatName));

        string value = CaptureUtf8String(() => SDL3.SDL.GetAudioFormatName(SDL3.SDL.AudioFormat.AudioF32LE), "SDL_AUDIO_F32LE")!;
        TestAssert.Equal("SDL_AUDIO_F32LE", value, "SDL.GetAudioFormatName must convert UTF-8 native name.");
        TestAssert.Equal(SDL3.SDL.AudioFormat.AudioF32LE, capturedFormat, "SDL.GetAudioFormatName must forward format.");

        nextPointer = IntPtr.Zero;
        TestAssert.Equal(string.Empty, SDL3.SDL.GetAudioFormatName(SDL3.SDL.AudioFormat.Unknown), "SDL.GetAudioFormatName must return empty string for native null.");
        TestAssert.Equal(SDL3.SDL.AudioFormat.Unknown, capturedFormat, "SDL.GetAudioFormatName must forward format for native null.");
    }

    public static void GetSilenceValueForFormat_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetSilenceValueForFormat");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetSilenceValueForFormat");

        nextInt = 128;
        using NativeHookScope _ = NativeHookScope.Install("GetSilenceValueForFormatNativeFunction", nameof(CaptureGetSilenceValueForFormat));
        int result = SDL3.SDL.GetSilenceValueForFormat(SDL3.SDL.AudioFormat.AudioU8);

        TestAssert.Equal(128, result, "SDL.GetSilenceValueForFormat must return the native hook value.");
        TestAssert.Equal(SDL3.SDL.AudioFormat.AudioU8, capturedFormat, "SDL.GetSilenceValueForFormat must forward format.");
    }

    private static int CaptureGetNumAudioDrivers()
    {
        return 7;
    }

    private static IntPtr CaptureGetAudioDriver(int index)
    {
        capturedIndex = index;
        return nextPointer;
    }

    private static IntPtr CapturePointer()
    {
        return nextPointer;
    }

    private static IntPtr CaptureDeviceArray(out int count)
    {
        count = nextCount;
        return nextPointer;
    }

    private static IntPtr CaptureGetAudioDeviceName(uint devid)
    {
        capturedDevice = devid;
        return nextPointer;
    }

    private static bool CaptureGetAudioDeviceFormat(uint devid, out SDL3.SDL.AudioSpec spec, out int sampleFrames)
    {
        capturedDevice = devid;
        spec = nextSpec;
        sampleFrames = nextSampleFrames;
        return nextBool;
    }

    private static IntPtr CaptureChannelMap(uint devid, out int count)
    {
        capturedDevice = devid;
        count = nextCount;
        return nextPointer;
    }

    private static uint CaptureOpenAudioDeviceWithSpec(uint devid, in SDL3.SDL.AudioSpec spec)
    {
        capturedDevice = devid;
        capturedSpec = spec;
        return nextDevice;
    }

    private static uint CaptureOpenAudioDeviceWithPointer(uint devid, IntPtr spec)
    {
        capturedDevice = devid;
        capturedSpecPointer = spec;
        return nextDevice;
    }

    private static bool CaptureBoolDevice(uint devid)
    {
        capturedDevice = devid;
        return nextBool;
    }

    private static float CaptureGetAudioDeviceGain(uint devid)
    {
        capturedDevice = devid;
        return nextFloat;
    }

    private static bool CaptureSetAudioDeviceGain(uint devid, float gain)
    {
        capturedDevice = devid;
        capturedGain = gain;
        return nextBool;
    }

    private static void CaptureCloseAudioDevice(uint devid)
    {
        capturedDevice = devid;
        capturedCallCount++;
    }

    private static bool CaptureBindAudioStreams(uint devid, IntPtr[] streams, int numStream)
    {
        capturedDevice = devid;
        capturedStreams = streams;
        capturedNumStreams = numStream;
        return nextBool;
    }

    private static bool CaptureBindAudioStream(uint devid, IntPtr stream)
    {
        capturedDevice = devid;
        capturedStream = stream;
        return nextBool;
    }

    private static void CaptureUnbindAudioStreams(IntPtr[]? streams, int numStreams)
    {
        capturedStreams = streams;
        capturedNumStreams = numStreams;
        capturedCallCount++;
    }

    private static void CaptureUnbindAudioStream(IntPtr stream)
    {
        capturedStream = stream;
        capturedCallCount++;
    }

    private static uint CaptureGetAudioStreamDevice(IntPtr stream)
    {
        capturedStream = stream;
        return nextDevice;
    }

    private static IntPtr CaptureCreateAudioStreamWithPointers(IntPtr srcSpec, IntPtr dstSpec)
    {
        capturedSrcSpecPointer = srcSpec;
        capturedDstSpecPointer = dstSpec;
        return nextPointer;
    }

    private static IntPtr CaptureCreateAudioStreamWithSpecs(in SDL3.SDL.AudioSpec srcSpec, in SDL3.SDL.AudioSpec dstSpec)
    {
        capturedSrcSpec = srcSpec;
        capturedDstSpec = dstSpec;
        return nextPointer;
    }

    private static uint CaptureGetAudioStreamProperties(IntPtr stream)
    {
        capturedStream = stream;
        return nextProperties;
    }

    private static bool CaptureGetAudioStreamFormat(IntPtr stream, out SDL3.SDL.AudioSpec srcSpec, out SDL3.SDL.AudioSpec dstSpec)
    {
        capturedStream = stream;
        srcSpec = nextSpec;
        dstSpec = nextDstSpec;
        return nextBool;
    }

    private static bool CaptureSetAudioStreamFormatWithPointers(IntPtr stream, IntPtr srcSpec, IntPtr dstSpec)
    {
        capturedStream = stream;
        capturedSrcSpecPointer = srcSpec;
        capturedDstSpecPointer = dstSpec;
        return nextBool;
    }

    private static bool CaptureSetAudioStreamFormatWithSpecs(IntPtr stream, in SDL3.SDL.AudioSpec srcSpec, in SDL3.SDL.AudioSpec dstSpec)
    {
        capturedStream = stream;
        capturedSrcSpec = srcSpec;
        capturedDstSpec = dstSpec;
        return nextBool;
    }

    private static float CaptureGetAudioStreamFloat(IntPtr stream)
    {
        capturedStream = stream;
        return nextFloat;
    }

    private static bool CaptureSetAudioStreamFrequencyRatio(IntPtr stream, float ratio)
    {
        capturedStream = stream;
        capturedRatio = ratio;
        return nextBool;
    }

    private static bool CaptureSetAudioStreamGain(IntPtr stream, float gain)
    {
        capturedStream = stream;
        capturedGain = gain;
        return nextBool;
    }

    private static IntPtr CaptureStreamChannelMap(IntPtr stream, out int count)
    {
        capturedStream = stream;
        count = nextCount;
        return nextPointer;
    }

    private static bool CaptureSetAudioStreamChannelMap(IntPtr stream, int[]? chmap, int count)
    {
        capturedStream = stream;
        capturedChannelMap = chmap;
        capturedCount = count;
        return nextBool;
    }

    private static bool CapturePutAudioStreamDataWithPointer(IntPtr stream, IntPtr buf, int len)
    {
        capturedStream = stream;
        capturedBufferPointer = buf;
        capturedLen = len;
        return nextBool;
    }

    private static bool CapturePutAudioStreamDataNoCopy(IntPtr stream, IntPtr buf, int len, SDL3.SDL.AudioStreamDataCompleteCallback? callback, IntPtr userdata)
    {
        capturedStream = stream;
        capturedBufferPointer = buf;
        capturedLen = len;
        capturedDataCompleteCallback = callback;
        capturedUserdata = userdata;
        return nextBool;
    }

    private static bool CapturePutAudioStreamDataWithBytes(IntPtr stream, byte[] buf, int len)
    {
        capturedStream = stream;
        capturedByteBuffer = buf;
        capturedLen = len;
        return nextBool;
    }

    private static bool CapturePutAudioStreamPlanarData(IntPtr stream, IntPtr[] channelBuffers, int numChannels, int numSamples)
    {
        capturedStream = stream;
        capturedChannelBuffers = channelBuffers;
        capturedNumChannels = numChannels;
        capturedNumSamples = numSamples;
        return nextBool;
    }

    private static int CaptureGetAudioStreamDataWithPointer(IntPtr stream, IntPtr buf, int len)
    {
        capturedStream = stream;
        capturedBufferPointer = buf;
        capturedLen = len;
        return nextInt;
    }

    private static int CaptureGetAudioStreamDataWithBytes(IntPtr stream, byte[] buf, int len)
    {
        capturedStream = stream;
        capturedByteBuffer = buf;
        capturedLen = len;
        return nextInt;
    }

    private static int CaptureGetAudioStreamInt(IntPtr stream)
    {
        capturedStream = stream;
        return nextInt;
    }

    private static bool CaptureBoolStream(IntPtr stream)
    {
        capturedStream = stream;
        return nextBool;
    }

    private static bool CaptureSetAudioStreamCallback(IntPtr stream, SDL3.SDL.AudioStreamCallback? callback, IntPtr userdata)
    {
        capturedStream = stream;
        capturedAudioStreamCallback = callback;
        capturedUserdata = userdata;
        return nextBool;
    }

    private static void CaptureDestroyAudioStream(IntPtr stream)
    {
        capturedStream = stream;
        capturedCallCount++;
    }

    private static IntPtr CaptureOpenAudioDeviceStreamWithSpec(uint devid, in SDL3.SDL.AudioSpec spec, SDL3.SDL.AudioStreamCallback? callback, IntPtr userdata)
    {
        capturedDevice = devid;
        capturedSpec = spec;
        capturedAudioStreamCallback = callback;
        capturedUserdata = userdata;
        return nextPointer;
    }

    private static IntPtr CaptureOpenAudioDeviceStreamWithPointer(uint devid, IntPtr spec, SDL3.SDL.AudioStreamCallback? callback, IntPtr userdata)
    {
        capturedDevice = devid;
        capturedSpecPointer = spec;
        capturedAudioStreamCallback = callback;
        capturedUserdata = userdata;
        return nextPointer;
    }

    private static bool CaptureSetAudioPostmixCallback(uint devid, SDL3.SDL.AudioPostmixCallback? callback, IntPtr userdata)
    {
        capturedDevice = devid;
        capturedAudioPostmixCallback = callback;
        capturedUserdata = userdata;
        return nextBool;
    }

    private static bool CaptureLoadWAVIO(IntPtr src, bool closeio, out SDL3.SDL.AudioSpec spec, out IntPtr audioBuf, out uint audioLen)
    {
        capturedSourcePointer = src;
        capturedCloseIO = closeio;
        spec = nextSpec;
        audioBuf = nextPointer;
        audioLen = nextAudioLen;
        return nextBool;
    }

    private static bool CaptureLoadWAV(string path, out SDL3.SDL.AudioSpec spec, out IntPtr audioBuf, out uint audioLen)
    {
        capturedPath = path;
        spec = nextSpec;
        audioBuf = nextPointer;
        audioLen = nextAudioLen;
        return nextBool;
    }

    private static bool CaptureMixAudio(IntPtr dst, IntPtr src, SDL3.SDL.AudioFormat format, uint len, float volume)
    {
        capturedDstBufferPointer = dst;
        capturedSrcBufferPointer = src;
        capturedFormat = format;
        capturedBufferLength = len;
        capturedVolume = volume;
        return nextBool;
    }

    private static bool CaptureConvertAudioSamples(in SDL3.SDL.AudioSpec srcSpec, IntPtr srcData, int srcLen, in SDL3.SDL.AudioSpec dstSpec, out IntPtr dstData, out int dstLen)
    {
        capturedSrcSpec = srcSpec;
        capturedSrcBufferPointer = srcData;
        capturedLen = srcLen;
        capturedDstSpec = dstSpec;
        dstData = nextPointer;
        dstLen = nextInt;
        return nextBool;
    }

    private static IntPtr CaptureGetAudioFormatName(SDL3.SDL.AudioFormat format)
    {
        capturedFormat = format;
        return nextPointer;
    }

    private static int CaptureGetSilenceValueForFormat(SDL3.SDL.AudioFormat format)
    {
        capturedFormat = format;
        return nextInt;
    }

    private static void TestAudioStreamDataCompleteCallback(IntPtr userdata, byte[] buf, int buflen)
    {
    }

    private static void TestAudioStreamCallback(IntPtr userdata, IntPtr stream, int additionalAmount, int totalAmount)
    {
    }

    private static void TestAudioPostmixCallback(IntPtr userdata, in SDL3.SDL.AudioSpec spec, float[] buffer, int buflen)
    {
    }

    private static SDL3.SDL.AudioSpec CreateAudioSpec(SDL3.SDL.AudioFormat format, int channels, int freq)
    {
        return new SDL3.SDL.AudioSpec
        {
            Format = format,
            Channels = channels,
            Freq = freq
        };
    }

    private static void AssertAudioSpec(SDL3.SDL.AudioSpec expected, SDL3.SDL.AudioSpec actual, string message)
    {
        TestAssert.Equal(expected.Format, actual.Format, $"{message} Format mismatch.");
        TestAssert.Equal(expected.Channels, actual.Channels, $"{message} Channels mismatch.");
        TestAssert.Equal(expected.Freq, actual.Freq, $"{message} Freq mismatch.");
    }

    private static IntPtr CreateNativeUIntArray(params uint[] values)
    {
        IntPtr pointer = SDL3.SDL.Malloc((UIntPtr)(sizeof(uint) * values.Length));
        TestAssert.True(pointer != IntPtr.Zero, "Test uint array allocation must succeed.");

        for (int i = 0; i < values.Length; i++)
        {
            Marshal.WriteInt32(pointer, i * sizeof(uint), unchecked((int)values[i]));
        }

        return pointer;
    }

    private static IntPtr CreateNativeIntArray(params int[] values)
    {
        IntPtr pointer = SDL3.SDL.Malloc((UIntPtr)(sizeof(int) * values.Length));
        TestAssert.True(pointer != IntPtr.Zero, "Test int array allocation must succeed.");
        Marshal.Copy(values, 0, pointer, values.Length);
        return pointer;
    }

    private static string? CaptureUtf8String(Func<string?> action, string value)
    {
        nextPointer = Marshal.StringToCoTaskMemUTF8(value);

        try
        {
            return action();
        }
        finally
        {
            Marshal.FreeCoTaskMem(nextPointer);
            nextPointer = IntPtr.Zero;
        }
    }

    private static MethodInfo GetNativeMethod(string methodName)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(method, $"SDL.{methodName} method must be private static.");
        return method!;
    }

    private static MethodInfo GetNativeMethod(string methodName, Type[] parameterTypes)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static, parameterTypes);
        TestAssert.NotNull(method, $"SDL.{methodName} overload must be private static.");
        return method!;
    }

    private static void AssertSdlLibraryImport(MethodInfo method, string entryPoint)
    {
        LibraryImportAttribute? libraryImport = method.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, $"SDL.{method.Name} must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3", libraryImport!.LibraryName, $"SDL.{method.Name} must import from SDL3.");
        TestAssert.Equal(entryPoint, libraryImport.EntryPoint, $"SDL.{method.Name} must bind {entryPoint}.");
    }

    private static void AssertSdlDllImport(MethodInfo method, string entryPoint)
    {
        DllImportAttribute? dllImport = method.GetCustomAttribute<DllImportAttribute>();
        TestAssert.NotNull(dllImport, $"SDL.{method.Name} must keep DllImport metadata.");
        TestAssert.Equal("SDL3", dllImport!.Value, $"SDL.{method.Name} must import from SDL3.");
        TestAssert.Equal(entryPoint, dllImport.EntryPoint, $"SDL.{method.Name} must bind {entryPoint}.");
    }

    private static void AssertBoolReturnMarshal(MethodInfo method)
    {
        MarshalAsAttribute? marshalAs = method.ReturnParameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} return value must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.I1, marshalAs!.Value, $"SDL.{method.Name} return value must use I1 marshalling.");
    }

    private static void AssertArrayParameterMarshal(MethodInfo method, string parameterName, short sizeParamIndex)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.LPArray, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use LPArray marshalling.");
        TestAssert.Equal(sizeParamIndex, marshalAs.SizeParamIndex, $"SDL.{method.Name} parameter {parameterName} must keep SizeParamIndex.");
    }

    private static void AssertBoolParameterMarshal(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.I1, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use I1 marshalling.");
    }

    private static void AssertStringParameterMarshal(MethodInfo method, string parameterName, UnmanagedType unmanagedType)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(unmanagedType, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use expected string marshalling.");
    }

    private static void AssertPointerArrayParameterMarshal(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.LPArray, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use LPArray marshalling.");
        TestAssert.Equal(UnmanagedType.LPArray, marshalAs.ArraySubType, $"SDL.{method.Name} parameter {parameterName} must keep ArraySubType.");
    }

    private static void AssertBoolDeviceForwarder(string methodName, string entryPoint, string hookFieldName, Func<uint, bool> action)
    {
        MethodInfo nativeMethod = GetNativeMethod(methodName);
        AssertSdlLibraryImport(nativeMethod, entryPoint);
        AssertBoolReturnMarshal(nativeMethod);

        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureBoolDevice));
        bool result = action(107);

        TestAssert.Equal(true, result, $"SDL.{methodName} public wrapper must return the native hook value.");
        TestAssert.Equal(107u, capturedDevice, $"SDL.{methodName} public wrapper must forward device ID.");
    }

    private static void AssertBoolStreamForwarder(string methodName, string entryPoint, string hookFieldName, Func<IntPtr, bool> action)
    {
        MethodInfo nativeMethod = GetNativeMethod(methodName);
        AssertSdlLibraryImport(nativeMethod, entryPoint);
        AssertBoolReturnMarshal(nativeMethod);

        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureBoolStream));
        bool result = action((IntPtr)418);

        TestAssert.Equal(true, result, $"SDL.{methodName} public wrapper must return the native hook value.");
        TestAssert.Equal((IntPtr)418, capturedStream, $"SDL.{methodName} public wrapper must forward stream.");
    }

    private sealed class NativeHookScope : IDisposable
    {
        private readonly FieldInfo field;
        private readonly object? originalValue;

        private NativeHookScope(FieldInfo field, object? originalValue)
        {
            this.field = field;
            this.originalValue = originalValue;
        }

        public static NativeHookScope Install(string fieldName, string methodName)
        {
            FieldInfo? field = typeof(SDL3.SDL).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
            TestAssert.NotNull(field, $"SDL.{fieldName} native hook field must exist.");
            MethodInfo? method = typeof(PInvokeTests).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
            TestAssert.NotNull(method, $"{methodName} hook method must exist.");
            object? originalValue = field!.GetValue(null);
            Delegate hook = Delegate.CreateDelegate(field.FieldType, method!);
            field.SetValue(null, hook);
            return new NativeHookScope(field, originalValue);
        }

        public void Dispose()
        {
            field.SetValue(null, originalValue);
        }
    }
}
