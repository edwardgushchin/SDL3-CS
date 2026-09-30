using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3.Tests.SDL.Video.OpenXR;

internal static class PInvokeTests
{
    private static IntPtr device, createInfo, images, formatsPointer;
    private static ulong session, swapchain;
    private static SDL3.SDL.GPUTextureFormat format;
    private static int result, count, freed, unloaded;
    private static bool success;

    public static void RunAll()
    {
        Metadata_PreservesNativeAbi();
        Handles_ForwardInputsResultsAndOwnershipPointers();
        Formats_CopyContiguousEnumsAndFreeNativeAllocation();
        Loader_ForwardsSuccessFailureAndProcedurePointer();
        if (NativeLibraryProbe.SupportsSDL3Export("SDL_CreateGPUXRSession"))
        {
            Native_InvalidDevicesReturnErrors();
            Native_LoaderFailureIsReversible();
        }
    }

    public static void Metadata_PreservesNativeAbi()
    {
        string[] names = ["CreateGPUXRSession", "GetGPUXRSwapchainFormats", "CreateGPUXRSwapchain", "DestroyGPUXRSwapchain", "OpenXR_LoadLibrary", "OpenXR_UnloadLibrary", "OpenXR_GetXrGetInstanceProcAddr"];
        foreach (string name in names)
        {
            MethodInfo method = typeof(SDL3.SDL).GetMethod("SDL_" + name, BindingFlags.NonPublic | BindingFlags.Static)!;
            LibraryImportAttribute import = method.GetCustomAttribute<LibraryImportAttribute>()!;
            TestAssert.Equal("SDL3", import.LibraryName, "OpenXR imports must use SDL3.");
            TestAssert.Equal("SDL_" + name, import.EntryPoint, "OpenXR imports must preserve exact entry point.");
            TestAssert.Equal(typeof(CallConvCdecl), method.GetCustomAttribute<UnmanagedCallConvAttribute>()!.CallConvs![0], "OpenXR imports must use cdecl.");
        }
        MethodInfo create = typeof(SDL3.SDL).GetMethod("SDL_CreateGPUXRSession", BindingFlags.NonPublic | BindingFlags.Static)!;
        TestAssert.Equal(typeof(int), create.ReturnType, "XrResult ABI must be int32.");
        TestAssert.Equal(typeof(ulong).MakeByRefType(), create.GetParameters()[2].ParameterType, "XR output handle ABI must be 64-bit.");
        MethodInfo chain = typeof(SDL3.SDL).GetMethod("SDL_CreateGPUXRSwapchain", BindingFlags.NonPublic | BindingFlags.Static)!;
        TestAssert.Equal(typeof(ulong), chain.GetParameters()[1].ParameterType, "XR session handle ABI must be 64-bit.");
        TestAssert.Equal(typeof(IntPtr).MakeByRefType(), chain.GetParameters()[5].ParameterType, "XR image array output must be a native pointer.");
        MethodInfo load = typeof(SDL3.SDL).GetMethod("SDL_OpenXR_LoadLibrary", BindingFlags.NonPublic | BindingFlags.Static)!;
        TestAssert.Equal(UnmanagedType.I1, load.ReturnParameter.GetCustomAttribute<MarshalAsAttribute>()!.Value, "OpenXR loader result must use native bool ABI.");
    }

    public static void Handles_ForwardInputsResultsAndOwnershipPointers()
    {
        result = 0;
        using (Hook hook = Hook.Install("CreateGPUXRSessionNativeFunction", nameof(CreateSession)))
        {
            TestAssert.Equal(0, SDL3.SDL.CreateGPUXRSession((IntPtr)101, (IntPtr)102, out ulong actual), "Session creation must forward native result.");
            TestAssert.Equal((IntPtr)101, device, "Session creation must forward device.");
            TestAssert.Equal((IntPtr)102, createInfo, "Session creation must forward complete external create-info pointer.");
            TestAssert.Equal(0x123456789ABCDEF0UL, actual, "Session creation must preserve all handle bits.");
            result = -12;
            TestAssert.Equal(-12, SDL3.SDL.CreateGPUXRSession(IntPtr.Zero, IntPtr.Zero, out _), "Session creation must forward errors.");
        }
        using (Hook _ = Hook.Install("CreateGPUXRSwapchainNativeFunction", nameof(CreateSwapchain)))
        {
            result = 0;
            TestAssert.Equal(0, SDL3.SDL.CreateGPUXRSwapchain((IntPtr)103, 0x123456789ABCDEF0UL, (IntPtr)104, SDL3.SDL.GPUTextureFormat.R8G8B8A8Unorm, out ulong actual, out IntPtr actualImages), "Swapchain creation must forward result.");
            TestAssert.Equal((IntPtr)103, device, "Swapchain creation must forward device.");
            TestAssert.Equal(0x123456789ABCDEF0UL, session, "Swapchain creation must preserve session handle.");
            TestAssert.Equal((IntPtr)104, createInfo, "Swapchain creation must forward external create-info pointer.");
            TestAssert.Equal(SDL3.SDL.GPUTextureFormat.R8G8B8A8Unorm, format, "Swapchain creation must forward GPU format.");
            TestAssert.Equal(0xFEDCBA9876543210UL, actual, "Swapchain output must preserve all handle bits.");
            TestAssert.Equal((IntPtr)105, actualImages, "Image array must retain native ownership pointer.");
        }
        using (Hook _ = Hook.Install("DestroyGPUXRSwapchainNativeFunction", nameof(DestroySwapchain)))
        {
            result = -12;
            TestAssert.Equal(-12, SDL3.SDL.DestroyGPUXRSwapchain((IntPtr)106, 0xFEDCBA9876543210UL, (IntPtr)105), "Swapchain destruction must forward result.");
            TestAssert.Equal((IntPtr)106, device, "Swapchain destruction must forward device.");
            TestAssert.Equal(0xFEDCBA9876543210UL, swapchain, "Swapchain destruction must preserve handle width.");
            TestAssert.Equal((IntPtr)105, images, "Swapchain destruction must return original image allocation to native owner.");
        }
    }

    public static void Formats_CopyContiguousEnumsAndFreeNativeAllocation()
    {
        using Hook query = Hook.Install("GetGPUXRSwapchainFormatsNativeFunction", nameof(GetFormats));
        using Hook free = Hook.Install("FreeNativeFunction", nameof(FreeFormats));
        freed = 0;
        formatsPointer = Marshal.AllocHGlobal(8);
        Marshal.WriteInt32(formatsPointer, 0, (int)SDL3.SDL.GPUTextureFormat.R8G8B8A8Unorm);
        Marshal.WriteInt32(formatsPointer, 4, (int)SDL3.SDL.GPUTextureFormat.B8G8R8A8Unorm);
        count = 2;
        SDL3.SDL.GPUTextureFormat[]? actual = SDL3.SDL.GetGPUXRSwapchainFormats((IntPtr)107, 0x123456789ABCDEF0UL, out int actualCount);
        TestAssert.Equal(2, actualCount, "XR format count must be preserved.");
        TestAssert.Equal((IntPtr)107, device, "XR format query must forward device.");
        TestAssert.Equal(0x123456789ABCDEF0UL, session, "XR format query must preserve session width.");
        TestAssert.Equal(SDL3.SDL.GPUTextureFormat.R8G8B8A8Unorm, actual![0], "XR formats must be read as contiguous enums.");
        TestAssert.Equal(SDL3.SDL.GPUTextureFormat.B8G8R8A8Unorm, actual[1], "XR format stride must be int32.");
        TestAssert.Equal(1, freed, "XR format allocation must be freed exactly once.");
        formatsPointer = IntPtr.Zero;
        count = 0;
        TestAssert.Equal<SDL3.SDL.GPUTextureFormat[]?>(null, SDL3.SDL.GetGPUXRSwapchainFormats(IntPtr.Zero, 0, out _), "Native null formats must return null.");
        TestAssert.Equal(1, freed, "Native null must not be freed.");
        formatsPointer = Marshal.AllocHGlobal(4);
        TestAssert.Equal(0, SDL3.SDL.GetGPUXRSwapchainFormats(IntPtr.Zero, 0, out _)!.Length, "Non-null empty allocation must return empty array.");
        TestAssert.Equal(2, freed, "Empty native allocation must be freed.");
        formatsPointer = Marshal.AllocHGlobal(4);
        count = -1;
        bool threw = false;
        try { SDL3.SDL.GetGPUXRSwapchainFormats(IntPtr.Zero, 0, out _); }
        catch (ArgumentOutOfRangeException) { threw = true; }
        TestAssert.True(threw, "Invalid native count must be rejected.");
        TestAssert.Equal(3, freed, "Native allocation must be freed even if conversion fails.");
    }

    public static void Loader_ForwardsSuccessFailureAndProcedurePointer()
    {
        using Hook load = Hook.Install("OpenXRLoadLibraryNativeFunction", nameof(Load));
        using Hook unload = Hook.Install("OpenXRUnloadLibraryNativeFunction", nameof(Unload));
        using Hook pointer = Hook.Install("OpenXRGetXrGetInstanceProcAddrNativeFunction", nameof(GetProc));
        success = true;
        TestAssert.Equal(true, SDL3.SDL.OpenXRLoadLibrary(), "OpenXR loader must return success.");
        success = false;
        TestAssert.Equal(false, SDL3.SDL.OpenXRLoadLibrary(), "OpenXR loader must return failure.");
        images = (IntPtr)108;
        TestAssert.Equal(images, SDL3.SDL.OpenXRGetXrGetInstanceProcAddr(), "OpenXR procedure pointer must be preserved.");
        images = IntPtr.Zero;
        TestAssert.Equal(IntPtr.Zero, SDL3.SDL.OpenXRGetXrGetInstanceProcAddr(), "OpenXR procedure pointer must preserve null.");
        unloaded = 0;
        SDL3.SDL.OpenXRUnloadLibrary();
        TestAssert.Equal(1, unloaded, "OpenXR unload must invoke native once.");
    }

    public static void Native_InvalidDevicesReturnErrors()
    {
        TestAssert.Equal(-12, SDL3.SDL.CreateGPUXRSession(IntPtr.Zero, IntPtr.Zero, out _), "Native XR creation must reject invalid device.");
        TestAssert.Equal<SDL3.SDL.GPUTextureFormat[]?>(null, SDL3.SDL.GetGPUXRSwapchainFormats(IntPtr.Zero, 0, out _), "Native XR format query must reject invalid device.");
        TestAssert.Equal(-12, SDL3.SDL.CreateGPUXRSwapchain(IntPtr.Zero, 0, IntPtr.Zero, SDL3.SDL.GPUTextureFormat.Invalid, out _, out _), "Native XR swapchain creation must reject invalid device.");
        TestAssert.Equal(-12, SDL3.SDL.DestroyGPUXRSwapchain(IntPtr.Zero, 0, IntPtr.Zero), "Native XR destruction must reject invalid device.");
    }

    public static void Native_LoaderFailureIsReversible()
    {
        string? previous = SDL3.SDL.GetHint("SDL_OPENXR_LIBRARY");
        try
        {
            SDL3.SDL.SetHintWithPriority("SDL_OPENXR_LIBRARY", "/nonexistent/sdl3cs-openxr-loader", SDL3.SDL.HintPriority.Override);
            TestAssert.Equal(false, SDL3.SDL.OpenXRLoadLibrary(), "Missing OpenXR loader must fail safely.");
            TestAssert.Equal(IntPtr.Zero, SDL3.SDL.OpenXRGetXrGetInstanceProcAddr(), "Unloaded OpenXR loader must return null procedure pointer.");
            SDL3.SDL.OpenXRUnloadLibrary();
        }
        finally
        {
            if (previous is null) SDL3.SDL.ResetHint("SDL_OPENXR_LIBRARY");
            else SDL3.SDL.SetHintWithPriority("SDL_OPENXR_LIBRARY", previous, SDL3.SDL.HintPriority.Override);
        }
    }

    private static int CreateSession(IntPtr d, IntPtr info, out ulong output) { device = d; createInfo = info; output = 0x123456789ABCDEF0UL; return result; }
    private static IntPtr GetFormats(IntPtr d, ulong s, out int output) { device = d; session = s; output = count; return formatsPointer; }
    private static int CreateSwapchain(IntPtr d, ulong s, IntPtr info, SDL3.SDL.GPUTextureFormat f, out ulong output, out IntPtr textures) { device = d; session = s; createInfo = info; format = f; output = 0xFEDCBA9876543210UL; textures = (IntPtr)105; return result; }
    private static int DestroySwapchain(IntPtr d, ulong s, IntPtr textures) { device = d; swapchain = s; images = textures; return result; }
    private static void FreeFormats(IntPtr pointer) { freed++; Marshal.FreeHGlobal(pointer); }
    private static bool Load() => success;
    private static void Unload() => unloaded++;
    private static IntPtr GetProc() => images;

    private sealed class Hook : IDisposable
    {
        private readonly FieldInfo field;
        private readonly object? previous;
        private Hook(FieldInfo field, object? previous) { this.field = field; this.previous = previous; }
        public static Hook Install(string fieldName, string methodName)
        {
            FieldInfo field = typeof(SDL3.SDL).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static)!;
            object? previous = field.GetValue(null);
            field.SetValue(null, Delegate.CreateDelegate(field.FieldType, typeof(PInvokeTests).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static)!));
            return new Hook(field, previous);
        }
        public void Dispose() => field.SetValue(null, previous);
    }
}
