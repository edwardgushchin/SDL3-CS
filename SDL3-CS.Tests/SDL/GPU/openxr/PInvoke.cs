using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.GPU.OpenXR;

internal static class PInvokeTests
{
    private static IntPtr capturedDevice;
    private static IntPtr capturedCreateInfo;
    private static IntPtr capturedSession;
    private static SDL3.SDL.GPUTextureFormat capturedFormat;
    private static IntPtr capturedSwapchain;
    private static IntPtr capturedSwapchainImages;
    private static IntPtr capturedTextures;
    private static IntPtr nextPointer;
    private static IntPtr nextSession;
    private static IntPtr nextSwapchain;
    private static IntPtr nextTextures;
    private static int nextInt;
    private static SDL3.XrResult nextXrResult;
    private static bool nextBool;
    private static int capturedCallCount;

    public static void RunAll()
    {
        CreateGPUXRSession_ForwardsArgumentsOutSessionAndReturnsNativeValue();
        GetGPUXRSwapchainFormats_ForwardsArgumentsOutCountAndReturnsNativePointer();
        CreateGPUXRSwapchain_ForwardsArgumentsOutValuesAndReturnsNativeValue();
        DestroyGPUXRSwapchain_ForwardsArgumentsAndReturnsNativeValue();
        OpenXRLoadLibrary_ReturnsNativeBoolValue();
        OpenXRUnloadLibrary_CallsNativeHook();
        OpenXRGetXrGetInstanceProcAddr_ReturnsNativePointer();
    }

    public static void CreateGPUXRSession_ForwardsArgumentsOutSessionAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CreateGPUXRSession");
        AssertSdlLibraryImport(nativeMethod, "SDL_CreateGPUXRSession");
        AssertByRefParameter(nativeMethod, "session");

        ResetCaptureState();
        nextXrResult = SDL3.XrResult.ErrorHandleInvalid;
        nextSession = (IntPtr)1001;
        using NativeHookScope _ = NativeHookScope.Install("CreateGPUXRSessionNativeFunction", nameof(CaptureCreateGPUXRSession));

        SDL3.XrResult result = SDL3.SDL.CreateGPUXRSession((IntPtr)1002, (IntPtr)1003, out IntPtr session);

        TestAssert.Equal(SDL3.XrResult.ErrorHandleInvalid, result, "SDL.CreateGPUXRSession must return native XrResult.");
        TestAssert.Equal((IntPtr)1002, capturedDevice, "SDL.CreateGPUXRSession must forward device.");
        TestAssert.Equal((IntPtr)1003, capturedCreateInfo, "SDL.CreateGPUXRSession must forward createInfo.");
        TestAssert.Equal((IntPtr)1001, session, "SDL.CreateGPUXRSession must forward native session out value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.CreateGPUXRSession must call native hook once.");
    }

    public static void GetGPUXRSwapchainFormats_ForwardsArgumentsOutCountAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGPUXRSwapchainFormats");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetGPUXRSwapchainFormats");
        AssertByRefParameter(nativeMethod, "numFormats");

        ResetCaptureState();
        nextPointer = (IntPtr)1101;
        nextInt = 3;
        using NativeHookScope _ = NativeHookScope.Install("GetGPUXRSwapchainFormatsNativeFunction", nameof(CaptureGetGPUXRSwapchainFormats));

        IntPtr result = SDL3.SDL.GetGPUXRSwapchainFormats((IntPtr)1102, (IntPtr)1103, out int numFormats);

        TestAssert.Equal((IntPtr)1101, result, "SDL.GetGPUXRSwapchainFormats must return native format array pointer.");
        TestAssert.Equal((IntPtr)1102, capturedDevice, "SDL.GetGPUXRSwapchainFormats must forward device.");
        TestAssert.Equal((IntPtr)1103, capturedSession, "SDL.GetGPUXRSwapchainFormats must forward session.");
        TestAssert.Equal(3, numFormats, "SDL.GetGPUXRSwapchainFormats must forward native count out value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetGPUXRSwapchainFormats must call native hook once.");
    }

    public static void CreateGPUXRSwapchain_ForwardsArgumentsOutValuesAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CreateGPUXRSwapchain");
        AssertSdlLibraryImport(nativeMethod, "SDL_CreateGPUXRSwapchain");
        AssertByRefParameter(nativeMethod, "swapchain");
        AssertByRefParameter(nativeMethod, "textures");

        ResetCaptureState();
        nextXrResult = SDL3.XrResult.ErrorFunctionUnsupported;
        nextSwapchain = (IntPtr)1201;
        nextTextures = (IntPtr)1202;
        using NativeHookScope _ = NativeHookScope.Install("CreateGPUXRSwapchainNativeFunction", nameof(CaptureCreateGPUXRSwapchain));

        SDL3.XrResult result = SDL3.SDL.CreateGPUXRSwapchain((IntPtr)1203, (IntPtr)1204, (IntPtr)1205, SDL3.SDL.GPUTextureFormat.R8G8B8A8Unorm, out IntPtr swapchain, out IntPtr textures);

        TestAssert.Equal(SDL3.XrResult.ErrorFunctionUnsupported, result, "SDL.CreateGPUXRSwapchain must return native XrResult.");
        TestAssert.Equal((IntPtr)1203, capturedDevice, "SDL.CreateGPUXRSwapchain must forward device.");
        TestAssert.Equal((IntPtr)1204, capturedSession, "SDL.CreateGPUXRSwapchain must forward session.");
        TestAssert.Equal((IntPtr)1205, capturedCreateInfo, "SDL.CreateGPUXRSwapchain must forward createinfo.");
        TestAssert.Equal(SDL3.SDL.GPUTextureFormat.R8G8B8A8Unorm, capturedFormat, "SDL.CreateGPUXRSwapchain must forward format.");
        TestAssert.Equal((IntPtr)1201, swapchain, "SDL.CreateGPUXRSwapchain must forward native swapchain out value.");
        TestAssert.Equal((IntPtr)1202, textures, "SDL.CreateGPUXRSwapchain must forward native textures out value.");
        TestAssert.Equal((IntPtr)1202, capturedTextures, "SDL.CreateGPUXRSwapchain hook must capture native textures out value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.CreateGPUXRSwapchain must call native hook once.");
    }

    public static void DestroyGPUXRSwapchain_ForwardsArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_DestroyGPUXRSwapchain");
        AssertSdlLibraryImport(nativeMethod, "SDL_DestroyGPUXRSwapchain");

        ResetCaptureState();
        nextXrResult = SDL3.XrResult.ErrorHandleInvalid;
        using NativeHookScope _ = NativeHookScope.Install("DestroyGPUXRSwapchainNativeFunction", nameof(CaptureDestroyGPUXRSwapchain));

        SDL3.XrResult result = SDL3.SDL.DestroyGPUXRSwapchain((IntPtr)1301, (IntPtr)1302, (IntPtr)1303);

        TestAssert.Equal(SDL3.XrResult.ErrorHandleInvalid, result, "SDL.DestroyGPUXRSwapchain must return native XrResult.");
        TestAssert.Equal((IntPtr)1301, capturedDevice, "SDL.DestroyGPUXRSwapchain must forward device.");
        TestAssert.Equal((IntPtr)1302, capturedSwapchain, "SDL.DestroyGPUXRSwapchain must forward swapchain.");
        TestAssert.Equal((IntPtr)1303, capturedSwapchainImages, "SDL.DestroyGPUXRSwapchain must forward swapchainImages.");
        TestAssert.Equal(1, capturedCallCount, "SDL.DestroyGPUXRSwapchain must call native hook once.");
    }

    public static void OpenXRLoadLibrary_ReturnsNativeBoolValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_OpenXR_LoadLibrary");
        AssertSdlLibraryImport(nativeMethod, "SDL_OpenXR_LoadLibrary");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("OpenXRLoadLibraryNativeFunction", nameof(CaptureNoArgumentBool));

        bool result = SDL3.SDL.OpenXRLoadLibrary();

        TestAssert.Equal(true, result, "SDL.OpenXRLoadLibrary must return native bool value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.OpenXRLoadLibrary must call native hook once.");
    }

    public static void OpenXRUnloadLibrary_CallsNativeHook()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_OpenXR_UnloadLibrary");
        AssertSdlLibraryImport(nativeMethod, "SDL_OpenXR_UnloadLibrary");

        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("OpenXRUnloadLibraryNativeFunction", nameof(CaptureNoArgumentVoid));

        SDL3.SDL.OpenXRUnloadLibrary();

        TestAssert.Equal(1, capturedCallCount, "SDL.OpenXRUnloadLibrary must call native hook once.");
    }

    public static void OpenXRGetXrGetInstanceProcAddr_ReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_OpenXR_GetXrGetInstanceProcAddr");
        AssertSdlLibraryImport(nativeMethod, "SDL_OpenXR_GetXrGetInstanceProcAddr");

        ResetCaptureState();
        nextPointer = (IntPtr)1401;
        using NativeHookScope _ = NativeHookScope.Install("OpenXRGetXrGetInstanceProcAddrNativeFunction", nameof(CaptureNoArgumentPointer));

        IntPtr result = SDL3.SDL.OpenXRGetXrGetInstanceProcAddr();

        TestAssert.Equal((IntPtr)1401, result, "SDL.OpenXRGetXrGetInstanceProcAddr must return native pointer.");
        TestAssert.Equal(1, capturedCallCount, "SDL.OpenXRGetXrGetInstanceProcAddr must call native hook once.");
    }

    private static void ResetCaptureState()
    {
        capturedDevice = IntPtr.Zero;
        capturedCreateInfo = IntPtr.Zero;
        capturedSession = IntPtr.Zero;
        capturedFormat = default;
        capturedSwapchain = IntPtr.Zero;
        capturedSwapchainImages = IntPtr.Zero;
        capturedTextures = IntPtr.Zero;
        nextPointer = IntPtr.Zero;
        nextSession = IntPtr.Zero;
        nextSwapchain = IntPtr.Zero;
        nextTextures = IntPtr.Zero;
        nextInt = 0;
        nextXrResult = default;
        nextBool = false;
        capturedCallCount = 0;
    }

    private static SDL3.XrResult CaptureCreateGPUXRSession(IntPtr device, IntPtr createInfo, out IntPtr session)
    {
        capturedDevice = device;
        capturedCreateInfo = createInfo;
        session = nextSession;
        capturedCallCount++;
        return nextXrResult;
    }

    private static IntPtr CaptureGetGPUXRSwapchainFormats(IntPtr device, IntPtr session, out int numFormats)
    {
        capturedDevice = device;
        capturedSession = session;
        numFormats = nextInt;
        capturedCallCount++;
        return nextPointer;
    }

    private static SDL3.XrResult CaptureCreateGPUXRSwapchain(IntPtr device, IntPtr session, IntPtr createinfo, SDL3.SDL.GPUTextureFormat format, out IntPtr swapchain, out IntPtr textures)
    {
        capturedDevice = device;
        capturedSession = session;
        capturedCreateInfo = createinfo;
        capturedFormat = format;
        swapchain = nextSwapchain;
        textures = nextTextures;
        capturedTextures = nextTextures;
        capturedCallCount++;
        return nextXrResult;
    }

    private static SDL3.XrResult CaptureDestroyGPUXRSwapchain(IntPtr device, IntPtr swapchain, IntPtr swapchainImages)
    {
        capturedDevice = device;
        capturedSwapchain = swapchain;
        capturedSwapchainImages = swapchainImages;
        capturedCallCount++;
        return nextXrResult;
    }

    private static bool CaptureNoArgumentBool()
    {
        capturedCallCount++;
        return nextBool;
    }

    private static void CaptureNoArgumentVoid()
    {
        capturedCallCount++;
    }

    private static IntPtr CaptureNoArgumentPointer()
    {
        capturedCallCount++;
        return nextPointer;
    }

    private static MethodInfo GetNativeMethod(string methodName)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(method, $"SDL.{methodName} method must be private static.");
        return method!;
    }

    private static void AssertSdlLibraryImport(MethodInfo method, string entryPoint)
    {
        LibraryImportAttribute? libraryImport = method.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, $"SDL.{method.Name} must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3", libraryImport!.LibraryName, $"SDL.{method.Name} must import from SDL3.");
        TestAssert.Equal(entryPoint, libraryImport.EntryPoint, $"SDL.{method.Name} must bind {entryPoint}.");
        AssertCdecl(method, $"SDL.{method.Name}");
    }

    private static void AssertCdecl(MethodInfo method, string apiName)
    {
        UnmanagedCallConvAttribute? callConv = method.GetCustomAttribute<UnmanagedCallConvAttribute>();
        TestAssert.NotNull(callConv, $"{apiName} must keep unmanaged calling convention metadata.");
        Type[] callConvs = callConv!.CallConvs ?? Array.Empty<Type>();
        TestAssert.Equal(1, callConvs.Length, $"{apiName} must declare one unmanaged calling convention.");
        TestAssert.Equal(typeof(CallConvCdecl), callConvs[0], $"{apiName} must use cdecl calling convention.");
    }

    private static void AssertBoolReturnMarshal(MethodInfo method, UnmanagedType unmanagedType)
    {
        MarshalAsAttribute? marshalAs = method.ReturnParameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} return value must keep MarshalAs metadata.");
        TestAssert.Equal(unmanagedType, marshalAs!.Value, $"SDL.{method.Name} return value must use expected bool marshalling.");
    }

    private static void AssertByRefParameter(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        TestAssert.True(parameter.ParameterType.IsByRef, $"SDL.{method.Name} parameter {parameterName} must stay by reference.");
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
