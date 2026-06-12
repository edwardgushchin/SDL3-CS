using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Video.Camera;

internal static class PInvokeTests
{
    private static readonly List<IntPtr> allocatedSpecPointers = [];
    private static IntPtr nextPointer;
    private static IntPtr capturedCamera;
    private static IntPtr capturedFrame;
    private static IntPtr capturedSpecPointer;
    private static IntPtr capturedFreePointer;
    private static SDL3.SDL.CameraSpec nextSpec;
    private static SDL3.SDL.CameraSpec capturedSpec;
    private static SDL3.SDL.CameraPosition nextPosition;
    private static SDL3.SDL.CameraPermissionState nextPermissionState;
    private static ulong nextTimestamp;
    private static uint nextUInt;
    private static uint capturedInstanceId;
    private static int nextInt;
    private static int nextCount;
    private static int capturedIndex;
    private static int capturedCallCount;
    private static int capturedFreeCallCount;

    public static void RunAll()
    {
        NativeEntryPoints_KeepExpectedLibraryImportMetadata();
        GetNumCameraDrivers_ReturnsNativeValue();
        GetCameraDriver_ReturnsUtf8StringAndForwardsIndex();
        GetCameraDriver_ReturnsNullForNativeNull();
        GetCurrentCameraDriver_ReturnsUtf8String();
        GetCurrentCameraDriver_ReturnsNullForNativeNull();
        GetCameras_ReturnsIdsAndFreesNativeArray();
        GetCameras_ReturnsNullWithoutFreeForNativeNull();
        GetCameraSupportedFormats_ReturnsFormatsAndFreesNativeArray();
        GetCameraSupportedFormats_ReturnsNullWithoutFreeForNativeNull();
        GetCameraName_ReturnsUtf8StringAndForwardsInstanceId();
        GetCameraName_ReturnsNullForNativeNull();
        GetCameraPosition_ForwardsInstanceIdAndReturnsNativeValue();
        OpenCamera_WithPointerSpecForwardsArgumentsAndReturnsNativeValue();
        OpenCamera_WithTypedSpecForwardsArgumentsAndReturnsNativeValue();
        GetCameraPermissionState_ForwardsCameraAndReturnsNativeValue();
        GetCameraID_ForwardsCameraAndReturnsNativeValue();
        GetCameraProperties_ForwardsCameraAndReturnsNativeValue();
        GetCameraFormat_ForwardsCameraReturnsSpecAndNativeValue();
        AcquireCameraFrame_ForwardsCameraReturnsTimestampAndFrame();
        ReleaseCameraFrame_ForwardsCameraAndFrame();
        CloseCamera_ForwardsCamera();
    }

    public static void NativeEntryPoints_KeepExpectedLibraryImportMetadata()
    {
        AssertSdlLibraryImport(GetNativeMethod("SDL_GetNumCameraDrivers"), "SDL_GetNumCameraDrivers");
        AssertSdlLibraryImport(GetNativeMethod("SDL_GetCameraDriver"), "SDL_GetCameraDriver");
        AssertSdlLibraryImport(GetNativeMethod("SDL_GetCurrentCameraDriver"), "SDL_GetCurrentCameraDriver");
        AssertSdlLibraryImport(GetNativeMethod("SDL_GetCameras"), "SDL_GetCameras");
        AssertSdlLibraryImport(GetNativeMethod("SDL_GetCameraSupportedFormats"), "SDL_GetCameraSupportedFormats");
        AssertSdlLibraryImport(GetNativeMethod("SDL_GetCameraName"), "SDL_GetCameraName");
        AssertSdlLibraryImport(GetNativeMethod("SDL_GetCameraPosition"), "SDL_GetCameraPosition");
        AssertSdlLibraryImport(GetNativeMethod("SDL_OpenCamera", [typeof(uint), typeof(IntPtr)]), "SDL_OpenCamera");
        AssertSdlLibraryImport(GetNativeMethod("SDL_OpenCamera", [typeof(uint), typeof(SDL3.SDL.CameraSpec).MakeByRefType()]), "SDL_OpenCamera");
        AssertSdlLibraryImport(GetNativeMethod("SDL_GetCameraPermissionState"), "SDL_GetCameraPermissionState");
        AssertSdlLibraryImport(GetNativeMethod("SDL_GetCameraID"), "SDL_GetCameraID");
        AssertSdlLibraryImport(GetNativeMethod("SDL_GetCameraProperties"), "SDL_GetCameraProperties");
        AssertSdlLibraryImport(GetNativeMethod("SDL_GetCameraFormat"), "SDL_GetCameraFormat");
        AssertSdlLibraryImport(GetNativeMethod("SDL_AcquireCameraFrame"), "SDL_AcquireCameraFrame");
        AssertSdlLibraryImport(GetNativeMethod("SDL_ReleaseCameraFrame"), "SDL_ReleaseCameraFrame");
        AssertSdlLibraryImport(GetNativeMethod("SDL_CloseCamera"), "SDL_CloseCamera");
    }

    public static void GetNumCameraDrivers_ReturnsNativeValue()
    {
        ResetCaptureState();
        nextInt = 7;

        using NativeHookScope _ = NativeHookScope.Install("GetNumCameraDriversNativeFunction", nameof(CaptureIntNoArgs));
        int result = SDL3.SDL.GetNumCameraDrivers();

        TestAssert.Equal(7, result, "SDL.GetNumCameraDrivers must return the native hook value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetNumCameraDrivers must call the native hook once.");
    }

    public static void GetCameraDriver_ReturnsUtf8StringAndForwardsIndex()
    {
        ResetCaptureState();

        string? value = CaptureUtf8String(() => SDL3.SDL.GetCameraDriver(3),
            "v4l2",
            "GetCameraDriverNativeFunction",
            nameof(CaptureGetCameraDriver));

        TestAssert.Equal("v4l2", value, "SDL.GetCameraDriver must convert UTF-8 native strings.");
        TestAssert.Equal(3, capturedIndex, "SDL.GetCameraDriver must forward index.");
    }

    public static void GetCameraDriver_ReturnsNullForNativeNull()
    {
        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install("GetCameraDriverNativeFunction", nameof(CaptureGetCameraDriver));
        string? value = SDL3.SDL.GetCameraDriver(-1);

        TestAssert.Equal<string?>(null, value, "SDL.GetCameraDriver must return null for native null.");
        TestAssert.Equal(-1, capturedIndex, "SDL.GetCameraDriver must forward invalid index values to native code.");
    }

    public static void GetCurrentCameraDriver_ReturnsUtf8String()
    {
        ResetCaptureState();

        string? value = CaptureUtf8String(SDL3.SDL.GetCurrentCameraDriver,
            "coremedia",
            "GetCurrentCameraDriverNativeFunction",
            nameof(CapturePointerNoArgs));

        TestAssert.Equal("coremedia", value, "SDL.GetCurrentCameraDriver must convert UTF-8 native strings.");
    }

    public static void GetCurrentCameraDriver_ReturnsNullForNativeNull()
    {
        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install("GetCurrentCameraDriverNativeFunction", nameof(CapturePointerNoArgs));
        string? value = SDL3.SDL.GetCurrentCameraDriver();

        TestAssert.Equal<string?>(null, value, "SDL.GetCurrentCameraDriver must return null for native null.");
    }

    public static void GetCameras_ReturnsIdsAndFreesNativeArray()
    {
        ResetCaptureState();
        uint[] expected = [11, 22, 33];
        nextCount = expected.Length;
        nextPointer = AllocateUIntArray(expected);

        using NativeHookScope cameraHook = NativeHookScope.Install("GetCamerasNativeFunction", nameof(CaptureCameras));
        using NativeHookScope freeHook = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree));

        try
        {
            uint[]? actual = SDL3.SDL.GetCameras(out int count);

            TestAssert.Equal(expected.Length, count, "SDL.GetCameras must return native count.");
            TestAssert.NotNull(actual, "SDL.GetCameras must copy native arrays.");
            TestAssert.Equal(expected.Length, actual!.Length, "SDL.GetCameras must preserve array length.");
            for (int i = 0; i < expected.Length; i++)
            {
                TestAssert.Equal(expected[i], actual[i], $"SDL.GetCameras must copy device id {i}.");
            }

            TestAssert.Equal(nextPointer, capturedFreePointer, "SDL.GetCameras must free the native array.");
            TestAssert.Equal(1, capturedFreeCallCount, "SDL.GetCameras must free the native array once.");
        }
        finally
        {
            FreeAllocatedCameraSpecArray(nextPointer);
            nextPointer = IntPtr.Zero;
        }
    }

    public static void GetCameras_ReturnsNullWithoutFreeForNativeNull()
    {
        ResetCaptureState();
        nextCount = 2;

        using NativeHookScope cameraHook = NativeHookScope.Install("GetCamerasNativeFunction", nameof(CaptureCameras));
        using NativeHookScope freeHook = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree));
        uint[]? actual = SDL3.SDL.GetCameras(out int count);

        TestAssert.Equal(2, count, "SDL.GetCameras must still expose native count for null pointers.");
        TestAssert.Equal<uint[]?>(null, actual, "SDL.GetCameras must return null for native null.");
        TestAssert.Equal(0, capturedFreeCallCount, "SDL.GetCameras must not free a native null pointer.");
    }

    public static void GetCameraSupportedFormats_ReturnsFormatsAndFreesNativeArray()
    {
        ResetCaptureState();
        SDL3.SDL.CameraSpec[] expected =
        [
            CreateCameraSpec((SDL3.SDL.PixelFormat)1, (SDL3.SDL.Colorspace)2, 640, 480, 30, 1),
            CreateCameraSpec((SDL3.SDL.PixelFormat)3, (SDL3.SDL.Colorspace)4, 1280, 720, 60, 1)
        ];
        nextCount = expected.Length;
        nextPointer = AllocateCameraSpecArray(expected);

        using NativeHookScope formatsHook = NativeHookScope.Install("GetCameraSupportedFormatsNativeFunction", nameof(CaptureCameraSupportedFormats));
        using NativeHookScope freeHook = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree));

        try
        {
            SDL3.SDL.CameraSpec[]? actual = SDL3.SDL.GetCameraSupportedFormats(44, out int count);

            TestAssert.Equal(44u, capturedInstanceId, "SDL.GetCameraSupportedFormats must forward instance id.");
            TestAssert.Equal(expected.Length, count, "SDL.GetCameraSupportedFormats must return native count.");
            TestAssert.NotNull(actual, "SDL.GetCameraSupportedFormats must copy native arrays.");
            TestAssert.Equal(expected.Length, actual!.Length, "SDL.GetCameraSupportedFormats must preserve array length.");
            for (int i = 0; i < expected.Length; i++)
            {
                AssertCameraSpec(expected[i], actual[i], $"SDL.GetCameraSupportedFormats must copy spec {i}.");
            }

            TestAssert.Equal(nextPointer, capturedFreePointer, "SDL.GetCameraSupportedFormats must free the native array.");
            TestAssert.Equal(1, capturedFreeCallCount, "SDL.GetCameraSupportedFormats must free the native array once.");
        }
        finally
        {
            Marshal.FreeHGlobal(nextPointer);
            nextPointer = IntPtr.Zero;
        }
    }

    public static void GetCameraSupportedFormats_ReturnsNullWithoutFreeForNativeNull()
    {
        ResetCaptureState();
        nextCount = 1;

        using NativeHookScope formatsHook = NativeHookScope.Install("GetCameraSupportedFormatsNativeFunction", nameof(CaptureCameraSupportedFormats));
        using NativeHookScope freeHook = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree));
        SDL3.SDL.CameraSpec[]? actual = SDL3.SDL.GetCameraSupportedFormats(55, out int count);

        TestAssert.Equal(55u, capturedInstanceId, "SDL.GetCameraSupportedFormats must forward instance id for null pointers.");
        TestAssert.Equal(1, count, "SDL.GetCameraSupportedFormats must still expose native count for null pointers.");
        TestAssert.Equal<SDL3.SDL.CameraSpec[]?>(null, actual, "SDL.GetCameraSupportedFormats must return null for native null.");
        TestAssert.Equal(0, capturedFreeCallCount, "SDL.GetCameraSupportedFormats must not free a native null pointer.");
    }

    public static void GetCameraName_ReturnsUtf8StringAndForwardsInstanceId()
    {
        ResetCaptureState();

        string? value = CaptureUtf8String(() => SDL3.SDL.GetCameraName(66),
            "Integrated Camera",
            "GetCameraNameNativeFunction",
            nameof(CaptureGetCameraName));

        TestAssert.Equal("Integrated Camera", value, "SDL.GetCameraName must convert UTF-8 native strings.");
        TestAssert.Equal(66u, capturedInstanceId, "SDL.GetCameraName must forward instance id.");
    }

    public static void GetCameraName_ReturnsNullForNativeNull()
    {
        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install("GetCameraNameNativeFunction", nameof(CaptureGetCameraName));
        string? value = SDL3.SDL.GetCameraName(77);

        TestAssert.Equal<string?>(null, value, "SDL.GetCameraName must return null for native null.");
        TestAssert.Equal(77u, capturedInstanceId, "SDL.GetCameraName must forward instance id for native null.");
    }

    public static void GetCameraPosition_ForwardsInstanceIdAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextPosition = SDL3.SDL.CameraPosition.BackFacing;

        using NativeHookScope _ = NativeHookScope.Install("GetCameraPositionNativeFunction", nameof(CaptureCameraPosition));
        SDL3.SDL.CameraPosition result = SDL3.SDL.GetCameraPosition(88);

        TestAssert.Equal(SDL3.SDL.CameraPosition.BackFacing, result, "SDL.GetCameraPosition must return the native hook value.");
        TestAssert.Equal(88u, capturedInstanceId, "SDL.GetCameraPosition must forward instance id.");
    }

    public static void OpenCamera_WithPointerSpecForwardsArgumentsAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextPointer = (IntPtr)0x1234;

        using NativeHookScope _ = NativeHookScope.Install("OpenCameraWithPointerNativeFunction", nameof(CaptureOpenCameraWithPointer));
        IntPtr result = SDL3.SDL.OpenCamera(99, (IntPtr)0x5678);

        TestAssert.Equal((IntPtr)0x1234, result, "SDL.OpenCamera(IntPtr) must return the native hook value.");
        TestAssert.Equal(99u, capturedInstanceId, "SDL.OpenCamera(IntPtr) must forward instance id.");
        TestAssert.Equal((IntPtr)0x5678, capturedSpecPointer, "SDL.OpenCamera(IntPtr) must forward spec pointer.");
    }

    public static void OpenCamera_WithTypedSpecForwardsArgumentsAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextPointer = (IntPtr)0x2345;
        SDL3.SDL.CameraSpec spec = CreateCameraSpec((SDL3.SDL.PixelFormat)5, (SDL3.SDL.Colorspace)6, 1920, 1080, 24, 1);

        using NativeHookScope _ = NativeHookScope.Install("OpenCameraWithSpecNativeFunction", nameof(CaptureOpenCameraWithSpec));
        IntPtr result = SDL3.SDL.OpenCamera(100, in spec);

        TestAssert.Equal((IntPtr)0x2345, result, "SDL.OpenCamera(in CameraSpec) must return the native hook value.");
        TestAssert.Equal(100u, capturedInstanceId, "SDL.OpenCamera(in CameraSpec) must forward instance id.");
        AssertCameraSpec(spec, capturedSpec, "SDL.OpenCamera(in CameraSpec) must forward spec.");
    }

    public static void GetCameraPermissionState_ForwardsCameraAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextPermissionState = SDL3.SDL.CameraPermissionState.Approved;

        using NativeHookScope _ = NativeHookScope.Install("GetCameraPermissionStateNativeFunction", nameof(CaptureCameraPermissionState));
        SDL3.SDL.CameraPermissionState result = SDL3.SDL.GetCameraPermissionState((IntPtr)0x3456);

        TestAssert.Equal(SDL3.SDL.CameraPermissionState.Approved, result, "SDL.GetCameraPermissionState must return the native hook value.");
        TestAssert.Equal((IntPtr)0x3456, capturedCamera, "SDL.GetCameraPermissionState must forward camera.");
    }

    public static void GetCameraID_ForwardsCameraAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextUInt = 123;

        using NativeHookScope _ = NativeHookScope.Install("GetCameraIDNativeFunction", nameof(CaptureUIntFromCamera));
        uint result = SDL3.SDL.GetCameraID((IntPtr)0x4567);

        TestAssert.Equal(123u, result, "SDL.GetCameraID must return the native hook value.");
        TestAssert.Equal((IntPtr)0x4567, capturedCamera, "SDL.GetCameraID must forward camera.");
    }

    public static void GetCameraProperties_ForwardsCameraAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextUInt = 234;

        using NativeHookScope _ = NativeHookScope.Install("GetCameraPropertiesNativeFunction", nameof(CaptureUIntFromCamera));
        uint result = SDL3.SDL.GetCameraProperties((IntPtr)0x5678);

        TestAssert.Equal(234u, result, "SDL.GetCameraProperties must return the native hook value.");
        TestAssert.Equal((IntPtr)0x5678, capturedCamera, "SDL.GetCameraProperties must forward camera.");
    }

    public static void GetCameraFormat_ForwardsCameraReturnsSpecAndNativeValue()
    {
        ResetCaptureState();
        nextInt = 1;
        nextSpec = CreateCameraSpec((SDL3.SDL.PixelFormat)7, (SDL3.SDL.Colorspace)8, 320, 240, 15, 1);

        using NativeHookScope _ = NativeHookScope.Install("GetCameraFormatNativeFunction", nameof(CaptureGetCameraFormat));
        int result = SDL3.SDL.GetCameraFormat((IntPtr)0x6789, out SDL3.SDL.CameraSpec actualSpec);

        TestAssert.Equal(1, result, "SDL.GetCameraFormat must return the native hook value.");
        TestAssert.Equal((IntPtr)0x6789, capturedCamera, "SDL.GetCameraFormat must forward camera.");
        AssertCameraSpec(nextSpec, actualSpec, "SDL.GetCameraFormat must return native spec.");
    }

    public static void AcquireCameraFrame_ForwardsCameraReturnsTimestampAndFrame()
    {
        ResetCaptureState();
        nextPointer = (IntPtr)0x789A;
        nextTimestamp = 123456789;

        using NativeHookScope _ = NativeHookScope.Install("AcquireCameraFrameNativeFunction", nameof(CaptureAcquireCameraFrame));
        IntPtr result = SDL3.SDL.AcquireCameraFrame((IntPtr)0x7890, out ulong timestampNS);

        TestAssert.Equal((IntPtr)0x789A, result, "SDL.AcquireCameraFrame must return the native hook frame.");
        TestAssert.Equal(123456789ul, timestampNS, "SDL.AcquireCameraFrame must return native timestamp.");
        TestAssert.Equal((IntPtr)0x7890, capturedCamera, "SDL.AcquireCameraFrame must forward camera.");
    }

    public static void ReleaseCameraFrame_ForwardsCameraAndFrame()
    {
        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install("ReleaseCameraFrameNativeFunction", nameof(CaptureReleaseCameraFrame));
        SDL3.SDL.ReleaseCameraFrame((IntPtr)0x8901, (IntPtr)0x9012);

        TestAssert.Equal((IntPtr)0x8901, capturedCamera, "SDL.ReleaseCameraFrame must forward camera.");
        TestAssert.Equal((IntPtr)0x9012, capturedFrame, "SDL.ReleaseCameraFrame must forward frame.");
    }

    public static void CloseCamera_ForwardsCamera()
    {
        ResetCaptureState();

        using NativeHookScope _ = NativeHookScope.Install("CloseCameraNativeFunction", nameof(CaptureCloseCamera));
        SDL3.SDL.CloseCamera((IntPtr)0xA123);

        TestAssert.Equal((IntPtr)0xA123, capturedCamera, "SDL.CloseCamera must forward camera.");
    }

    private static int CaptureIntNoArgs()
    {
        capturedCallCount++;
        return nextInt;
    }

    private static IntPtr CaptureGetCameraDriver(int index)
    {
        capturedIndex = index;
        return nextPointer;
    }

    private static IntPtr CapturePointerNoArgs()
    {
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureCameras(out int count)
    {
        count = nextCount;
        return nextPointer;
    }

    private static IntPtr CaptureCameraSupportedFormats(uint instanceId, out int count)
    {
        capturedInstanceId = instanceId;
        count = nextCount;
        return nextPointer;
    }

    private static IntPtr CaptureGetCameraName(uint instanceId)
    {
        capturedInstanceId = instanceId;
        return nextPointer;
    }

    private static SDL3.SDL.CameraPosition CaptureCameraPosition(uint instanceId)
    {
        capturedInstanceId = instanceId;
        return nextPosition;
    }

    private static IntPtr CaptureOpenCameraWithPointer(uint instanceId, IntPtr spec)
    {
        capturedInstanceId = instanceId;
        capturedSpecPointer = spec;
        return nextPointer;
    }

    private static IntPtr CaptureOpenCameraWithSpec(uint instanceId, in SDL3.SDL.CameraSpec spec)
    {
        capturedInstanceId = instanceId;
        capturedSpec = spec;
        return nextPointer;
    }

    private static SDL3.SDL.CameraPermissionState CaptureCameraPermissionState(IntPtr camera)
    {
        capturedCamera = camera;
        return nextPermissionState;
    }

    private static uint CaptureUIntFromCamera(IntPtr camera)
    {
        capturedCamera = camera;
        return nextUInt;
    }

    private static int CaptureGetCameraFormat(IntPtr camera, out SDL3.SDL.CameraSpec spec)
    {
        capturedCamera = camera;
        spec = nextSpec;
        return nextInt;
    }

    private static IntPtr CaptureAcquireCameraFrame(IntPtr camera, out ulong timestampNS)
    {
        capturedCamera = camera;
        timestampNS = nextTimestamp;
        return nextPointer;
    }

    private static void CaptureReleaseCameraFrame(IntPtr camera, IntPtr frame)
    {
        capturedCamera = camera;
        capturedFrame = frame;
    }

    private static void CaptureCloseCamera(IntPtr camera)
    {
        capturedCamera = camera;
    }

    private static void CaptureFree(IntPtr pointer)
    {
        capturedFreePointer = pointer;
        capturedFreeCallCount++;
    }

    private static string? CaptureUtf8String(Func<string?> action, string value, string fieldName, string hookMethodName)
    {
        nextPointer = Marshal.StringToCoTaskMemUTF8(value);

        using NativeHookScope _ = NativeHookScope.Install(fieldName, hookMethodName);
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

    private static SDL3.SDL.CameraSpec CreateCameraSpec(
        SDL3.SDL.PixelFormat pixelFormat,
        SDL3.SDL.Colorspace colorspace,
        int width,
        int height,
        int framerateNumerator,
        int framerateDenominator)
    {
        return new SDL3.SDL.CameraSpec
        {
            PixelFormat = pixelFormat,
            Colorspace = colorspace,
            Width = width,
            Height = height,
            FramerateNumerator = framerateNumerator,
            FramerateDenominator = framerateDenominator
        };
    }

    private static IntPtr AllocateUIntArray(uint[] values)
    {
        int elementSize = sizeof(uint);
        IntPtr pointer = Marshal.AllocHGlobal(elementSize * values.Length);
        for (int i = 0; i < values.Length; i++)
        {
            Marshal.WriteInt32(IntPtr.Add(pointer, i * elementSize), unchecked((int)values[i]));
        }

        return pointer;
    }

    private static IntPtr AllocateCameraSpecArray(SDL3.SDL.CameraSpec[] values)
    {
        int pointerSize = IntPtr.Size;
        IntPtr pointer = Marshal.AllocHGlobal(pointerSize * values.Length);
        for (int i = 0; i < values.Length; i++)
        {
            IntPtr elementPointer = Marshal.AllocHGlobal(Marshal.SizeOf<SDL3.SDL.CameraSpec>());
            Marshal.StructureToPtr(values[i], elementPointer, false);
            allocatedSpecPointers.Add(elementPointer);
            Marshal.WriteIntPtr(pointer, i * pointerSize, elementPointer);
        }

        return pointer;
    }

    private static void FreeAllocatedCameraSpecArray(IntPtr pointer)
    {
        foreach (IntPtr elementPointer in allocatedSpecPointers)
        {
            Marshal.FreeHGlobal(elementPointer);
        }

        allocatedSpecPointers.Clear();
        Marshal.FreeHGlobal(pointer);
    }

    private static void AssertCameraSpec(SDL3.SDL.CameraSpec expected, SDL3.SDL.CameraSpec actual, string message)
    {
        TestAssert.Equal(expected.PixelFormat, actual.PixelFormat, $"{message} PixelFormat.");
        TestAssert.Equal(expected.Colorspace, actual.Colorspace, $"{message} Colorspace.");
        TestAssert.Equal(expected.Width, actual.Width, $"{message} Width.");
        TestAssert.Equal(expected.Height, actual.Height, $"{message} Height.");
        TestAssert.Equal(expected.FramerateNumerator, actual.FramerateNumerator, $"{message} FramerateNumerator.");
        TestAssert.Equal(expected.FramerateDenominator, actual.FramerateDenominator, $"{message} FramerateDenominator.");
    }

    private static void ResetCaptureState()
    {
        nextPointer = IntPtr.Zero;
        capturedCamera = IntPtr.Zero;
        capturedFrame = IntPtr.Zero;
        capturedSpecPointer = IntPtr.Zero;
        capturedFreePointer = IntPtr.Zero;
        nextSpec = default;
        capturedSpec = default;
        nextPosition = default;
        nextPermissionState = default;
        nextTimestamp = 0;
        nextUInt = 0;
        capturedInstanceId = 0;
        nextInt = 0;
        nextCount = 0;
        capturedIndex = 0;
        capturedCallCount = 0;
        capturedFreeCallCount = 0;
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

    private sealed class NativeHookScope : IDisposable
    {
        private readonly FieldInfo field;
        private readonly object? previousValue;

        private NativeHookScope(FieldInfo field, object? hook)
        {
            this.field = field;
            previousValue = field.GetValue(null);
            field.SetValue(null, hook);
        }

        public static NativeHookScope Install(string fieldName, string methodName)
        {
            FieldInfo? field = typeof(SDL3.SDL).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
            TestAssert.NotNull(field, $"SDL private hook field {fieldName} must exist.");

            MethodInfo? method = typeof(PInvokeTests).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
            TestAssert.NotNull(method, $"Test hook method {methodName} must exist.");

            Delegate hook = Delegate.CreateDelegate(field!.FieldType, method!);

            return new NativeHookScope(field, hook);
        }

        public void Dispose()
        {
            field.SetValue(null, previousValue);
        }
    }
}
