using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Video.Vulkan;

internal static class PInvokeTests
{
    private static IntPtr nextPointer;
    private static IntPtr nextSurface;
    private static IntPtr capturedWindow;
    private static IntPtr capturedInstance;
    private static IntPtr capturedAllocator;
    private static IntPtr capturedSurface;
    private static IntPtr capturedPhysicalDevice;
    private static uint nextCount;
    private static uint capturedQueueFamilyIndex;
    private static bool nextBool;
    private static int capturedCallCount;
    private static string? capturedPath;

    public static void RunAll()
    {
        NativeEntryPoints_KeepExpectedLibraryImportMetadata();
        VulkanLoaderFunctions_ForwardInputsAndReturnNativeValues();
        VulkanGetInstanceExtensions_ConvertsNativePointerArrayAndHandlesNull();
        VulkanSurfaceFunctions_ForwardInputsOutputsAndReturnNativeValues();
    }

    public static void NativeEntryPoints_KeepExpectedLibraryImportMetadata()
    {
        MethodInfo loadLibrary = GetNativeMethod("SDL_Vulkan_LoadLibrary");
        AssertNativeBoolImport(loadLibrary, "SDL_Vulkan_LoadLibrary");
        AssertStringParameterMarshal(loadLibrary, 0);
        AssertNativeImport(GetNativeMethod("SDL_Vulkan_GetVkGetInstanceProcAddr"), "SDL_Vulkan_GetVkGetInstanceProcAddr");
        AssertNativeImport(GetNativeMethod("SDL_Vulkan_UnloadLibrary"), "SDL_Vulkan_UnloadLibrary");
        AssertNativeImport(GetNativeMethod("SDL_Vulkan_GetInstanceExtensions"), "SDL_Vulkan_GetInstanceExtensions");
        AssertNativeBoolImport(GetNativeMethod("SDL_Vulkan_CreateSurface"), "SDL_Vulkan_CreateSurface");
        AssertNativeImport(GetNativeMethod("SDL_Vulkan_DestroySurface"), "SDL_Vulkan_DestroySurface");
        AssertNativeBoolImport(GetNativeMethod("SDL_Vulkan_GetPresentationSupport"), "SDL_Vulkan_GetPresentationSupport");
    }

    public static void VulkanLoaderFunctions_ForwardInputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("VulkanLoadLibraryNativeFunction", nameof(CaptureVulkanLoadLibrary)))
        {
            bool actual = SDL3.SDL.VulkanLoadLibrary("vulkan-1.dll");

            TestAssert.Equal(true, actual, "SDL.VulkanLoadLibrary must return native success value.");
            TestAssert.Equal("vulkan-1.dll", capturedPath, "SDL.VulkanLoadLibrary must forward path.");
            TestAssert.Equal(1, capturedCallCount, "SDL.VulkanLoadLibrary must call native hook once.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x1101;
        using (NativeHookScope _ = NativeHookScope.Install("VulkanGetVkGetInstanceProcAddrNativeFunction", nameof(ReturnNextPointer)))
        {
            IntPtr actual = SDL3.SDL.VulkanGetVkGetInstanceProcAddr();

            TestAssert.Equal((IntPtr)0x1101, actual, "SDL.VulkanGetVkGetInstanceProcAddr must return native function pointer.");
            TestAssert.Equal(1, capturedCallCount, "SDL.VulkanGetVkGetInstanceProcAddr must call native hook once.");
        }

        ResetCaptureState();
        using (NativeHookScope _ = NativeHookScope.Install("VulkanUnloadLibraryNativeFunction", nameof(CaptureNoArgVoid)))
        {
            SDL3.SDL.VulkanUnloadLibrary();

            TestAssert.Equal(1, capturedCallCount, "SDL.VulkanUnloadLibrary must call native hook once.");
        }
    }

    public static void VulkanGetInstanceExtensions_ConvertsNativePointerArrayAndHandlesNull()
    {
        ResetCaptureState();
        nextCount = 2;
        IntPtr first = Marshal.StringToCoTaskMemUTF8("VK_KHR_surface");
        IntPtr second = Marshal.StringToCoTaskMemUTF8("VK_KHR_win32_surface");
        nextPointer = Marshal.AllocHGlobal(IntPtr.Size * 2);
        Marshal.WriteIntPtr(nextPointer, 0, first);
        Marshal.WriteIntPtr(nextPointer, IntPtr.Size, second);

        try
        {
            using (NativeHookScope _ = NativeHookScope.Install("VulkanGetInstanceExtensionsNativeFunction", nameof(CaptureVulkanGetInstanceExtensions)))
            {
                string[]? actual = SDL3.SDL.VulkanGetInstanceExtensions(out uint count);

                TestAssert.Equal(2u, count, "SDL.VulkanGetInstanceExtensions must return native count.");
                TestAssert.NotNull(actual, "SDL.VulkanGetInstanceExtensions must convert non-null pointer arrays.");
                TestAssert.Equal(2, actual!.Length, "SDL.VulkanGetInstanceExtensions must preserve extension count.");
                TestAssert.Equal("VK_KHR_surface", actual[0], "SDL.VulkanGetInstanceExtensions must convert first extension.");
                TestAssert.Equal("VK_KHR_win32_surface", actual[1], "SDL.VulkanGetInstanceExtensions must convert second extension.");
                TestAssert.Equal(1, capturedCallCount, "SDL.VulkanGetInstanceExtensions must call native hook once.");
            }
        }
        finally
        {
            Marshal.FreeHGlobal(nextPointer);
            Marshal.FreeCoTaskMem(first);
            Marshal.FreeCoTaskMem(second);
            nextPointer = IntPtr.Zero;
        }

        ResetCaptureState();
        using (NativeHookScope _ = NativeHookScope.Install("VulkanGetInstanceExtensionsNativeFunction", nameof(CaptureVulkanGetInstanceExtensions)))
        {
            string[]? actual = SDL3.SDL.VulkanGetInstanceExtensions(out uint count);

            TestAssert.Equal(0u, count, "SDL.VulkanGetInstanceExtensions null path must return native count.");
            TestAssert.Equal<string[]?>(null, actual, "SDL.VulkanGetInstanceExtensions must return null for native null.");
            TestAssert.Equal(1, capturedCallCount, "SDL.VulkanGetInstanceExtensions null path must call native hook once.");
        }
    }

    public static void VulkanSurfaceFunctions_ForwardInputsOutputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextBool = true;
        nextSurface = (IntPtr)0x2201;
        using (NativeHookScope _ = NativeHookScope.Install("VulkanCreateSurfaceNativeFunction", nameof(CaptureVulkanCreateSurface)))
        {
            bool actual = SDL3.SDL.VulkanCreateSurface((IntPtr)0x2202, (IntPtr)0x2203, (IntPtr)0x2204, out IntPtr surface);

            TestAssert.Equal(true, actual, "SDL.VulkanCreateSurface must return native success value.");
            TestAssert.Equal((IntPtr)0x2202, capturedWindow, "SDL.VulkanCreateSurface must forward window.");
            TestAssert.Equal((IntPtr)0x2203, capturedInstance, "SDL.VulkanCreateSurface must forward instance.");
            TestAssert.Equal((IntPtr)0x2204, capturedAllocator, "SDL.VulkanCreateSurface must forward allocator.");
            TestAssert.Equal((IntPtr)0x2201, surface, "SDL.VulkanCreateSurface must return native surface.");
            TestAssert.Equal(1, capturedCallCount, "SDL.VulkanCreateSurface must call native hook once.");
        }

        ResetCaptureState();
        using (NativeHookScope _ = NativeHookScope.Install("VulkanDestroySurfaceNativeFunction", nameof(CaptureVulkanDestroySurface)))
        {
            SDL3.SDL.VulkanDestroySurface((IntPtr)0x3301, (IntPtr)0x3302, (IntPtr)0x3303);

            TestAssert.Equal((IntPtr)0x3301, capturedInstance, "SDL.VulkanDestroySurface must forward instance.");
            TestAssert.Equal((IntPtr)0x3302, capturedSurface, "SDL.VulkanDestroySurface must forward surface.");
            TestAssert.Equal((IntPtr)0x3303, capturedAllocator, "SDL.VulkanDestroySurface must forward allocator.");
            TestAssert.Equal(1, capturedCallCount, "SDL.VulkanDestroySurface must call native hook once.");
        }

        ResetCaptureState();
        nextBool = false;
        using (NativeHookScope _ = NativeHookScope.Install("VulkanGetPresentationSupportNativeFunction", nameof(CaptureVulkanGetPresentationSupport)))
        {
            bool actual = SDL3.SDL.VulkanGetPresentationSupport((IntPtr)0x4401, (IntPtr)0x4402, 17);

            TestAssert.Equal(false, actual, "SDL.VulkanGetPresentationSupport must return native support value.");
            TestAssert.Equal((IntPtr)0x4401, capturedInstance, "SDL.VulkanGetPresentationSupport must forward instance.");
            TestAssert.Equal((IntPtr)0x4402, capturedPhysicalDevice, "SDL.VulkanGetPresentationSupport must forward physical device.");
            TestAssert.Equal(17u, capturedQueueFamilyIndex, "SDL.VulkanGetPresentationSupport must forward queue family index.");
            TestAssert.Equal(1, capturedCallCount, "SDL.VulkanGetPresentationSupport must call native hook once.");
        }
    }

    private static bool CaptureVulkanLoadLibrary(string? path)
    {
        capturedPath = path;
        capturedCallCount++;
        return nextBool;
    }

    private static IntPtr ReturnNextPointer()
    {
        capturedCallCount++;
        return nextPointer;
    }

    private static void CaptureNoArgVoid()
    {
        capturedCallCount++;
    }

    private static IntPtr CaptureVulkanGetInstanceExtensions(out uint count)
    {
        count = nextCount;
        capturedCallCount++;
        return nextPointer;
    }

    private static bool CaptureVulkanCreateSurface(IntPtr window, IntPtr instance, IntPtr allocator, out IntPtr surface)
    {
        capturedWindow = window;
        capturedInstance = instance;
        capturedAllocator = allocator;
        surface = nextSurface;
        capturedCallCount++;
        return nextBool;
    }

    private static void CaptureVulkanDestroySurface(IntPtr instance, IntPtr surface, IntPtr allocator)
    {
        capturedInstance = instance;
        capturedSurface = surface;
        capturedAllocator = allocator;
        capturedCallCount++;
    }

    private static bool CaptureVulkanGetPresentationSupport(IntPtr instance, IntPtr physicalDevice, uint queueFamilyIndex)
    {
        capturedInstance = instance;
        capturedPhysicalDevice = physicalDevice;
        capturedQueueFamilyIndex = queueFamilyIndex;
        capturedCallCount++;
        return nextBool;
    }

    private static void ResetCaptureState()
    {
        nextPointer = IntPtr.Zero;
        nextSurface = IntPtr.Zero;
        capturedWindow = IntPtr.Zero;
        capturedInstance = IntPtr.Zero;
        capturedAllocator = IntPtr.Zero;
        capturedSurface = IntPtr.Zero;
        capturedPhysicalDevice = IntPtr.Zero;
        nextCount = 0;
        capturedQueueFamilyIndex = 0;
        nextBool = false;
        capturedCallCount = 0;
        capturedPath = null;
    }

    private static MethodInfo GetNativeMethod(string methodName)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(method, $"SDL.{methodName} method must be private static.");
        return method!;
    }

    private static void AssertNativeImport(MethodInfo method, string entryPoint)
    {
        AssertSdlLibraryImport(method, entryPoint);
        AssertExcludedFromCoverage(method);
    }

    private static void AssertNativeBoolImport(MethodInfo method, string entryPoint)
    {
        AssertSdlLibraryImport(method, entryPoint);
        AssertBoolReturnMarshal(method);
        AssertExcludedFromCoverage(method);
    }

    private static void AssertSdlLibraryImport(MethodInfo method, string entryPoint)
    {
        LibraryImportAttribute? libraryImport = method.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, $"SDL.{method.Name} must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3", libraryImport!.LibraryName, $"SDL.{method.Name} must import from SDL3.");
        TestAssert.Equal(entryPoint, libraryImport.EntryPoint, $"SDL.{method.Name} must bind {entryPoint}.");
    }

    private static void AssertBoolReturnMarshal(MethodInfo method)
    {
        MarshalAsAttribute? marshalAs = method.ReturnParameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} return value must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.I1, marshalAs!.Value, $"SDL.{method.Name} return value must use I1 marshalling.");
    }

    private static void AssertStringParameterMarshal(MethodInfo method, int index)
    {
        MarshalAsAttribute? marshalAs = method.GetParameters()[index].GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} string parameter must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.LPUTF8Str, marshalAs!.Value, $"SDL.{method.Name} string parameter must use UTF-8 marshalling.");
    }

    private static void AssertExcludedFromCoverage(MethodInfo method)
    {
        ExcludeFromCodeCoverageAttribute? attribute = method.GetCustomAttribute<ExcludeFromCodeCoverageAttribute>();
        TestAssert.NotNull(attribute, $"SDL.{method.Name} native stub must be excluded from code coverage.");
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
