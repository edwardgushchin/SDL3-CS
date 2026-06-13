using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Video.Video;

internal static class PInvokeTests
{
    private static IntPtr nextPointer;
    private static IntPtr capturedFreePointer;
    private static IntPtr capturedWindow;
    private static IntPtr capturedIcon;
    private static IntPtr capturedParent;
    private static IntPtr capturedShape;
    private static IntPtr capturedModePointer;
    private static IntPtr capturedRectPointer;
    private static IntPtr capturedCallbackData;
    private static IntPtr capturedContext;
    private static IntPtr capturedUserData;
    private static UIntPtr nextSize;
    private static int nextInt;
    private static int capturedIndex;
    private static int nextCount;
    private static int capturedFreeCallCount;
    private static int nextVSync;
    private static int capturedVSync;
    private static int capturedNumRects;
    private static uint nextUInt;
    private static uint capturedDisplayID;
    private static int capturedWidth;
    private static int capturedHeight;
    private static int nextX;
    private static int nextY;
    private static int nextWidth;
    private static int nextHeight;
    private static int capturedX;
    private static int capturedY;
    private static int nextTop;
    private static int nextLeft;
    private static int nextBottom;
    private static int nextRight;
    private static bool nextBool;
    private static bool capturedBool;
    private static bool capturedIncludeHighDensityModes;
    private static float nextFloat;
    private static float capturedRefreshRate;
    private static float nextMinAspect;
    private static float nextMaxAspect;
    private static float capturedMinAspect;
    private static float capturedMaxAspect;
    private static float capturedOpacity;
    private static SDL3.SDL.Rect nextRect;
    private static SDL3.SDL.Rect[]? capturedRects;
    private static SDL3.SDL.Point capturedPoint;
    private static SDL3.SDL.Rect capturedRect;
    private static SDL3.SDL.DisplayMode nextDisplayMode;
    private static SDL3.SDL.DisplayMode capturedDisplayMode;
    private static SDL3.SDL.PixelFormat nextPixelFormat;
    private static SDL3.SDL.SystemTheme nextSystemTheme;
    private static SDL3.SDL.DisplayOrientation nextDisplayOrientation;
    private static SDL3.SDL.WindowFlags nextWindowFlags;
    private static SDL3.SDL.WindowFlags capturedWindowFlags;
    private static SDL3.SDL.FlashOperation capturedFlashOperation;
    private static SDL3.SDL.ProgressState nextProgressState;
    private static SDL3.SDL.ProgressState capturedProgressState;
    private static SDL3.SDL.HitTest? capturedHitTest;
    private static string? capturedTitle;
    private static uint capturedProperties;
    private static uint capturedWindowID;
    private static int capturedOffsetX;
    private static int capturedOffsetY;
    private static int capturedValue;
    private static int capturedInterval;
    private static int capturedNoArgCallCount;
    private static float capturedProgressValue;
    private static SDL3.SDL.GLAttr capturedGLAttr;
    private static SDL3.SDL.EGLAttribArrayCallback? capturedPlatformAttribCallback;
    private static SDL3.SDL.EGLIntArrayCallback? capturedSurfaceAttribCallback;
    private static SDL3.SDL.EGLIntArrayCallback? capturedContextAttribCallback;
    private static string? capturedPath;
    private static string? capturedProc;
    private static string? capturedExtension;

    public static void RunAll()
    {
        NativeEntryPoints_KeepExpectedLibraryImportMetadata();
        VideoDriverFunctions_ForwardAndConvertStrings();
        DisplayEnumerationAndScalarFunctions_ForwardInputsAndReturnNativeValues();
        DisplayBoundsOrientationAndScaleFunctions_ForwardInputsOutputsAndReturnNativeValues();
        DisplayModeFunctions_ForwardInputsOutputsAndManageNativePointers();
        DisplayLookupAndWindowScaleFunctions_ForwardInputsAndReturnNativeValues();
        WindowFullscreenProfileAndPixelFormatFunctions_ForwardInputsOutputsAndReturnNativeValues();
        WindowListFunctions_ManageNativePointerArrays();
        WindowCreationAndIdentityFunctions_ForwardInputsAndReturnNativeValues();
        WindowTitleFunctions_ForwardAndConvertStrings();
        WindowGeometryFunctions_ForwardInputsOutputsAndReturnNativeValues();
        WindowStateFunctions_ForwardInputsAndReturnNativeValues();
        WindowSurfaceFunctions_ForwardInputsOutputsAndReturnNativeValues();
        WindowGrabMouseFocusAndHitTestFunctions_ForwardInputsOutputsAndReturnNativeValues();
        WindowShapeProgressAndDestroyFunctions_ForwardInputsAndReturnNativeValues();
        ScreenSaverFunctions_ReturnNativeValues();
        OpenGLFunctions_ForwardInputsOutputsAndReturnNativeValues();
    }

    public static void NativeEntryPoints_KeepExpectedLibraryImportMetadata()
    {
        AssertNativeImport(GetNativeMethod("SDL_GetNumVideoDrivers"), "SDL_GetNumVideoDrivers");
        AssertNativeImport(GetNativeMethod("SDL_GetVideoDriver"), "SDL_GetVideoDriver");
        AssertNativeImport(GetNativeMethod("SDL_GetCurrentVideoDriver"), "SDL_GetCurrentVideoDriver");
        AssertNativeImport(GetNativeMethod("SDL_GetSystemTheme"), "SDL_GetSystemTheme");
        AssertNativeImport(GetNativeMethod("SDL_GetDisplays"), "SDL_GetDisplays");
        AssertNativeImport(GetNativeMethod("SDL_GetPrimaryDisplay"), "SDL_GetPrimaryDisplay");
        AssertNativeImport(GetNativeMethod("SDL_GetDisplayProperties"), "SDL_GetDisplayProperties");
        AssertNativeImport(GetNativeMethod("SDL_GetDisplayName"), "SDL_GetDisplayName");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetDisplayBounds"), "SDL_GetDisplayBounds");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetDisplayUsableBounds"), "SDL_GetDisplayUsableBounds");
        AssertNativeImport(GetNativeMethod("SDL_GetNaturalDisplayOrientation"), "SDL_GetNaturalDisplayOrientation");
        AssertNativeImport(GetNativeMethod("SDL_GetCurrentDisplayOrientation"), "SDL_GetCurrentDisplayOrientation");
        AssertNativeImport(GetNativeMethod("SDL_GetDisplayContentScale"), "SDL_GetDisplayContentScale");
        AssertNativeImport(GetNativeMethod("SDL_GetFullscreenDisplayModes"), "SDL_GetFullscreenDisplayModes");
        MethodInfo closestFullscreenDisplayMode = GetNativeMethod("SDL_GetClosestFullscreenDisplayMode");
        AssertNativeBoolImport(closestFullscreenDisplayMode, "SDL_GetClosestFullscreenDisplayMode");
        AssertBoolParameterMarshal(closestFullscreenDisplayMode, 4);
        AssertNativeImport(GetNativeMethod("SDL_GetDesktopDisplayMode"), "SDL_GetDesktopDisplayMode");
        AssertNativeImport(GetNativeMethod("SDL_GetCurrentDisplayMode"), "SDL_GetCurrentDisplayMode");
        AssertNativeImport(GetNativeMethod("SDL_GetDisplayForPoint"), "SDL_GetDisplayForPoint");
        AssertNativeImport(GetNativeMethod("SDL_GetDisplayForRect"), "SDL_GetDisplayForRect");
        AssertNativeImport(GetNativeMethod("SDL_GetDisplayForWindow"), "SDL_GetDisplayForWindow");
        AssertNativeImport(GetNativeMethod("SDL_GetWindowPixelDensity"), "SDL_GetWindowPixelDensity");
        AssertNativeImport(GetNativeMethod("SDL_GetWindowDisplayScale"), "SDL_GetWindowDisplayScale");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetWindowFullscreenModePointer"), "SDL_SetWindowFullscreenMode");
        MethodInfo setWindowFullscreenModeMode = GetNativeMethod("SDL_SetWindowFullscreenModeMode");
        AssertNativeBoolImport(setWindowFullscreenModeMode, "SDL_SetWindowFullscreenMode");
        TestAssert.Equal(typeof(SDL3.SDL.DisplayMode).MakeByRefType(), setWindowFullscreenModeMode.GetParameters()[1].ParameterType, "SDL_SetWindowFullscreenModeMode must pass DisplayMode by reference.");
        AssertNativeImport(GetNativeMethod("SDL_GetWindowFullscreenMode"), "SDL_GetWindowFullscreenMode");
        AssertNativeImport(GetNativeMethod("SDL_GetWindowICCProfile"), "SDL_GetWindowICCProfile");
        AssertNativeImport(GetNativeMethod("SDL_GetWindowPixelFormat"), "SDL_GetWindowPixelFormat");
        AssertNativeImport(GetNativeMethod("SDL_GetWindows"), "SDL_GetWindows");
        MethodInfo createWindow = GetNativeMethod("SDL_CreateWindow");
        AssertNativeImport(createWindow, "SDL_CreateWindow");
        AssertStringParameterMarshal(createWindow, 0);
        AssertNativeImport(GetNativeMethod("SDL_CreatePopupWindow"), "SDL_CreatePopupWindow");
        AssertNativeImport(GetNativeMethod("SDL_CreateWindowWithProperties"), "SDL_CreateWindowWithProperties");
        AssertNativeImport(GetNativeMethod("SDL_GetWindowID"), "SDL_GetWindowID");
        AssertNativeImport(GetNativeMethod("SDL_GetWindowFromID"), "SDL_GetWindowFromID");
        AssertNativeImport(GetNativeMethod("SDL_GetWindowParent"), "SDL_GetWindowParent");
        AssertNativeImport(GetNativeMethod("SDL_GetWindowProperties"), "SDL_GetWindowProperties");
        AssertNativeImport(GetNativeMethod("SDL_GetWindowFlags"), "SDL_GetWindowFlags");
        MethodInfo setWindowTitle = GetNativeMethod("SDL_SetWindowTitle");
        AssertNativeBoolImport(setWindowTitle, "SDL_SetWindowTitle");
        AssertStringParameterMarshal(setWindowTitle, 1);
        AssertNativeImport(GetNativeMethod("SDL_GetWindowTitle"), "SDL_GetWindowTitle");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetWindowIcon"), "SDL_SetWindowIcon");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetWindowPosition"), "SDL_SetWindowPosition");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetWindowPosition"), "SDL_GetWindowPosition");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetWindowSize"), "SDL_SetWindowSize");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetWindowSize"), "SDL_GetWindowSize");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetWindowSafeArea"), "SDL_GetWindowSafeArea");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetWindowAspectRatio"), "SDL_SetWindowAspectRatio");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetWindowAspectRatio"), "SDL_GetWindowAspectRatio");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetWindowBordersSize"), "SDL_GetWindowBordersSize");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetWindowSizeInPixels"), "SDL_GetWindowSizeInPixels");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetWindowMinimumSize"), "SDL_SetWindowMinimumSize");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetWindowMinimumSize"), "SDL_GetWindowMinimumSize");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetWindowMaximumSize"), "SDL_SetWindowMaximumSize");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetWindowMaximumSize"), "SDL_GetWindowMaximumSize");
        MethodInfo setWindowBordered = GetNativeMethod("SDL_SetWindowBordered");
        AssertNativeBoolImport(setWindowBordered, "SDL_SetWindowBordered");
        AssertBoolParameterMarshal(setWindowBordered, 1);
        MethodInfo setWindowResizable = GetNativeMethod("SDL_SetWindowResizable");
        AssertNativeBoolImport(setWindowResizable, "SDL_SetWindowResizable");
        AssertBoolParameterMarshal(setWindowResizable, 1);
        MethodInfo setWindowAlwaysOnTop = GetNativeMethod("SDL_SetWindowAlwaysOnTop");
        AssertNativeBoolImport(setWindowAlwaysOnTop, "SDL_SetWindowAlwaysOnTop");
        AssertBoolParameterMarshal(setWindowAlwaysOnTop, 1);
        MethodInfo setWindowFillDocument = GetNativeMethod("SDL_SetWindowFillDocument");
        AssertNativeBoolImport(setWindowFillDocument, "SDL_SetWindowFillDocument");
        AssertBoolParameterMarshal(setWindowFillDocument, 1);
        AssertNativeBoolImport(GetNativeMethod("SDL_ShowWindow"), "SDL_ShowWindow");
        AssertNativeBoolImport(GetNativeMethod("SDL_HideWindow"), "SDL_HideWindow");
        AssertNativeBoolImport(GetNativeMethod("SDL_RaiseWindow"), "SDL_RaiseWindow");
        AssertNativeBoolImport(GetNativeMethod("SDL_MaximizeWindow"), "SDL_MaximizeWindow");
        AssertNativeBoolImport(GetNativeMethod("SDL_MinimizeWindow"), "SDL_MinimizeWindow");
        AssertNativeBoolImport(GetNativeMethod("SDL_RestoreWindow"), "SDL_RestoreWindow");
        MethodInfo setWindowFullscreen = GetNativeMethod("SDL_SetWindowFullscreen");
        AssertNativeBoolImport(setWindowFullscreen, "SDL_SetWindowFullscreen");
        AssertBoolParameterMarshal(setWindowFullscreen, 1);
        AssertNativeBoolImport(GetNativeMethod("SDL_SyncWindow"), "SDL_SyncWindow");
        AssertNativeBoolImport(GetNativeMethod("SDL_WindowHasSurface"), "SDL_WindowHasSurface");
        AssertNativeImport(GetNativeMethod("SDL_GetWindowSurface"), "SDL_GetWindowSurface");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetWindowSurfaceVSync"), "SDL_SetWindowSurfaceVSync");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetWindowSurfaceVSync"), "SDL_GetWindowSurfaceVSync");
        AssertNativeBoolImport(GetNativeMethod("SDL_UpdateWindowSurface"), "SDL_UpdateWindowSurface");
        AssertNativeBoolImport(GetNativeMethod("SDL_UpdateWindowSurfaceRects"), "SDL_UpdateWindowSurfaceRects");
        AssertNativeBoolImport(GetNativeMethod("SDL_UpdateWindowSurfaceRectsPointer"), "SDL_UpdateWindowSurfaceRects");
        AssertNativeBoolImport(GetNativeMethod("SDL_DestroyWindowSurface"), "SDL_DestroyWindowSurface");
        MethodInfo setWindowKeyboardGrab = GetNativeMethod("SDL_SetWindowKeyboardGrab");
        AssertNativeBoolImport(setWindowKeyboardGrab, "SDL_SetWindowKeyboardGrab");
        AssertBoolParameterMarshal(setWindowKeyboardGrab, 1);
        MethodInfo setWindowMouseGrab = GetNativeMethod("SDL_SetWindowMouseGrab");
        AssertNativeBoolImport(setWindowMouseGrab, "SDL_SetWindowMouseGrab");
        AssertBoolParameterMarshal(setWindowMouseGrab, 1);
        AssertNativeBoolImport(GetNativeMethod("SDL_GetWindowKeyboardGrab"), "SDL_GetWindowKeyboardGrab");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetWindowMouseGrab"), "SDL_GetWindowMouseGrab");
        AssertNativeImport(GetNativeMethod("SDL_GetGrabbedWindow"), "SDL_GetGrabbedWindow");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetWindowMouseRectPointer"), "SDL_SetWindowMouseRect");
        MethodInfo setWindowMouseRectRect = GetNativeMethod("SDL_SetWindowMouseRectRect");
        AssertNativeBoolImport(setWindowMouseRectRect, "SDL_SetWindowMouseRect");
        TestAssert.Equal(typeof(SDL3.SDL.Rect).MakeByRefType(), setWindowMouseRectRect.GetParameters()[1].ParameterType, "SDL_SetWindowMouseRectRect must pass Rect by reference.");
        AssertNativeImport(GetNativeMethod("SDL_GetWindowMouseRect"), "SDL_GetWindowMouseRect");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetWindowOpacity"), "SDL_SetWindowOpacity");
        AssertNativeImport(GetNativeMethod("SDL_GetWindowOpacity"), "SDL_GetWindowOpacity");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetWindowParent"), "SDL_SetWindowParent");
        MethodInfo setWindowModal = GetNativeMethod("SDL_SetWindowModal");
        AssertNativeBoolImport(setWindowModal, "SDL_SetWindowModal");
        AssertBoolParameterMarshal(setWindowModal, 1);
        MethodInfo setWindowFocusable = GetNativeMethod("SDL_SetWindowFocusable");
        AssertNativeBoolImport(setWindowFocusable, "SDL_SetWindowFocusable");
        AssertBoolParameterMarshal(setWindowFocusable, 1);
        AssertNativeBoolImport(GetNativeMethod("SDL_ShowWindowSystemMenu"), "SDL_ShowWindowSystemMenu");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetWindowHitTest"), "SDL_SetWindowHitTest");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetWindowShape"), "SDL_SetWindowShape");
        AssertNativeBoolImport(GetNativeMethod("SDL_FlashWindow"), "SDL_FlashWindow");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetWindowProgressState"), "SDL_SetWindowProgressState");
        AssertNativeImport(GetNativeMethod("SDL_GetWindowProgressState"), "SDL_GetWindowProgressState");
        AssertNativeBoolImport(GetNativeMethod("SDL_SetWindowProgressValue"), "SDL_SetWindowProgressValue");
        AssertNativeImport(GetNativeMethod("SDL_GetWindowProgressValue"), "SDL_GetWindowProgressValue");
        AssertNativeImport(GetNativeMethod("SDL_DestroyWindow"), "SDL_DestroyWindow");
        AssertNativeBoolImport(GetNativeMethod("SDL_ScreenSaverEnabled"), "SDL_ScreenSaverEnabled");
        AssertNativeBoolImport(GetNativeMethod("SDL_EnableScreenSaver"), "SDL_EnableScreenSaver");
        AssertNativeBoolImport(GetNativeMethod("SDL_DisableScreenSaver"), "SDL_DisableScreenSaver");
        MethodInfo glLoadLibrary = GetNativeMethod("SDL_GL_LoadLibrary");
        AssertNativeBoolImport(glLoadLibrary, "SDL_GL_LoadLibrary");
        AssertStringParameterMarshal(glLoadLibrary, 0);
        MethodInfo glGetProcAddress = GetNativeMethod("SDL_GL_GetProcAddress");
        AssertNativeImport(glGetProcAddress, "SDL_GL_GetProcAddress");
        AssertStringParameterMarshal(glGetProcAddress, 0);
        MethodInfo eglGetProcAddress = GetNativeMethod("SDL_EGL_GetProcAddress");
        AssertNativeImport(eglGetProcAddress, "SDL_EGL_GetProcAddress");
        AssertStringParameterMarshal(eglGetProcAddress, 0);
        AssertNativeImport(GetNativeMethod("SDL_GL_UnloadLibrary"), "SDL_GL_UnloadLibrary");
        MethodInfo glExtensionSupported = GetNativeMethod("SDL_GL_ExtensionSupported");
        AssertNativeBoolImport(glExtensionSupported, "SDL_GL_ExtensionSupported");
        AssertStringParameterMarshal(glExtensionSupported, 0);
        AssertNativeImport(GetNativeMethod("SDL_GL_ResetAttributes"), "SDL_GL_ResetAttributes");
        AssertNativeBoolImport(GetNativeMethod("SDL_GL_SetAttribute"), "SDL_GL_SetAttribute");
        AssertNativeBoolImport(GetNativeMethod("SDL_GL_GetAttribute"), "SDL_GL_GetAttribute");
        AssertNativeImport(GetNativeMethod("SDL_GL_CreateContext"), "SDL_GL_CreateContext");
        AssertNativeBoolImport(GetNativeMethod("SDL_GL_MakeCurrent"), "SDL_GL_MakeCurrent");
        AssertNativeImport(GetNativeMethod("SDL_GL_GetCurrentWindow"), "SDL_GL_GetCurrentWindow");
        AssertNativeImport(GetNativeMethod("SDL_GL_GetCurrentContext"), "SDL_GL_GetCurrentContext");
        AssertNativeImport(GetNativeMethod("SDL_EGL_GetCurrentDisplay"), "SDL_EGL_GetCurrentDisplay");
        AssertNativeImport(GetNativeMethod("SDL_EGL_GetCurrentConfig"), "SDL_EGL_GetCurrentConfig");
        AssertNativeImport(GetNativeMethod("SDL_EGL_GetWindowSurface"), "SDL_EGL_GetWindowSurface");
        AssertNativeImport(GetNativeMethod("SDL_EGL_SetAttributeCallbacks"), "SDL_EGL_SetAttributeCallbacks");
        AssertNativeBoolImport(GetNativeMethod("SDL_GL_SetSwapInterval"), "SDL_GL_SetSwapInterval");
        AssertNativeBoolImport(GetNativeMethod("SDL_GL_GetSwapInterval"), "SDL_GL_GetSwapInterval");
        AssertNativeBoolImport(GetNativeMethod("SDL_GL_SwapWindow"), "SDL_GL_SwapWindow");
        AssertNativeBoolImport(GetNativeMethod("SDL_GL_DestroyContext"), "SDL_GL_DestroyContext");
    }

    public static void VideoDriverFunctions_ForwardAndConvertStrings()
    {
        ResetCaptureState();
        nextInt = 3;
        using (NativeHookScope _ = NativeHookScope.Install("GetNumVideoDriversNativeFunction", nameof(ReturnNextInt)))
        {
            TestAssert.Equal(3, SDL3.SDL.GetNumVideoDrivers(), "SDL.GetNumVideoDrivers must return the native count.");
        }

        ResetCaptureState();
        nextPointer = Marshal.StringToCoTaskMemUTF8("windows");
        using (NativeHookScope _ = NativeHookScope.Install("GetVideoDriverNativeFunction", nameof(CaptureGetVideoDriver)))
        {
            try
            {
                string actual = SDL3.SDL.GetVideoDriver(2);

                TestAssert.Equal(2, capturedIndex, "SDL.GetVideoDriver must forward index.");
                TestAssert.Equal("windows", actual, "SDL.GetVideoDriver must convert native UTF-8 text.");
            }
            finally
            {
                Marshal.FreeCoTaskMem(nextPointer);
                nextPointer = IntPtr.Zero;
            }
        }

        ResetCaptureState();
        using (NativeHookScope _ = NativeHookScope.Install("GetVideoDriverNativeFunction", nameof(CaptureGetVideoDriver)))
        {
            string actual = SDL3.SDL.GetVideoDriver(-1);

            TestAssert.Equal(-1, capturedIndex, "SDL.GetVideoDriver null path must forward index.");
            TestAssert.Equal(string.Empty, actual, "SDL.GetVideoDriver must return an empty string for native null.");
        }

        ResetCaptureState();
        nextPointer = Marshal.StringToCoTaskMemUTF8("wayland");
        using (NativeHookScope _ = NativeHookScope.Install("GetCurrentVideoDriverNativeFunction", nameof(ReturnNextPointer)))
        {
            try
            {
                TestAssert.Equal("wayland", SDL3.SDL.GetCurrentVideoDriver(), "SDL.GetCurrentVideoDriver must convert native UTF-8 text.");
            }
            finally
            {
                Marshal.FreeCoTaskMem(nextPointer);
                nextPointer = IntPtr.Zero;
            }
        }

        ResetCaptureState();
        using (NativeHookScope _ = NativeHookScope.Install("GetCurrentVideoDriverNativeFunction", nameof(ReturnNextPointer)))
        {
            TestAssert.Equal<string?>(null, SDL3.SDL.GetCurrentVideoDriver(), "SDL.GetCurrentVideoDriver must return null for native null.");
        }
    }

    public static void DisplayEnumerationAndScalarFunctions_ForwardInputsAndReturnNativeValues()
    {
        ResetCaptureState();
        uint[] expectedDisplays = [101u, 202u, 303u];
        nextPointer = AllocateUIntArray(expectedDisplays);
        nextCount = expectedDisplays.Length;
        using (NativeHookScope displaysHook = NativeHookScope.Install("GetDisplaysNativeFunction", nameof(ReturnDisplayArray)))
        using (NativeHookScope freeHook = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree)))
        {
            try
            {
                uint[]? actual = SDL3.SDL.GetDisplays(out int count);

                TestAssert.Equal(expectedDisplays.Length, count, "SDL.GetDisplays must return native count.");
                TestAssert.NotNull(actual, "SDL.GetDisplays must copy native display arrays.");
                TestAssert.Equal(expectedDisplays.Length, actual!.Length, "SDL.GetDisplays must preserve display count.");
                for (int i = 0; i < expectedDisplays.Length; i++)
                {
                    TestAssert.Equal(expectedDisplays[i], actual[i], $"SDL.GetDisplays must copy display id {i}.");
                }

                TestAssert.Equal(nextPointer, capturedFreePointer, "SDL.GetDisplays must free the native display array.");
                TestAssert.Equal(1, capturedFreeCallCount, "SDL.GetDisplays must free the native display array once.");
            }
            finally
            {
                Marshal.FreeHGlobal(nextPointer);
                nextPointer = IntPtr.Zero;
            }
        }

        ResetCaptureState();
        nextCount = 4;
        using (NativeHookScope displaysHook = NativeHookScope.Install("GetDisplaysNativeFunction", nameof(ReturnDisplayArray)))
        using (NativeHookScope freeHook = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree)))
        {
            uint[]? actual = SDL3.SDL.GetDisplays(out int count);

            TestAssert.Equal(4, count, "SDL.GetDisplays null path must return native count.");
            TestAssert.Equal<uint[]?>(null, actual, "SDL.GetDisplays must return null for native null.");
            TestAssert.Equal(0, capturedFreeCallCount, "SDL.GetDisplays must not free native null.");
        }

        ResetCaptureState();
        nextUInt = 0xCAFEu;
        using (NativeHookScope _ = NativeHookScope.Install("GetPrimaryDisplayNativeFunction", nameof(ReturnNextUInt)))
        {
            TestAssert.Equal(0xCAFEu, SDL3.SDL.GetPrimaryDisplay(), "SDL.GetPrimaryDisplay must return native display id.");
        }

        ResetCaptureState();
        nextUInt = 0xFACEu;
        using (NativeHookScope _ = NativeHookScope.Install("GetDisplayPropertiesNativeFunction", nameof(CaptureDisplayIdReturnUInt)))
        {
            TestAssert.Equal(0xFACEu, SDL3.SDL.GetDisplayProperties(0xBEEFu), "SDL.GetDisplayProperties must return native properties id.");
            TestAssert.Equal(0xBEEFu, capturedDisplayID, "SDL.GetDisplayProperties must forward displayID.");
        }

        ResetCaptureState();
        nextPointer = Marshal.StringToCoTaskMemUTF8("Display 1");
        using (NativeHookScope _ = NativeHookScope.Install("GetDisplayNameNativeFunction", nameof(CaptureDisplayIdReturnPointer)))
        {
            try
            {
                TestAssert.Equal("Display 1", SDL3.SDL.GetDisplayName(55), "SDL.GetDisplayName must convert native UTF-8 text.");
                TestAssert.Equal(55u, capturedDisplayID, "SDL.GetDisplayName must forward displayID.");
            }
            finally
            {
                Marshal.FreeCoTaskMem(nextPointer);
                nextPointer = IntPtr.Zero;
            }
        }

        ResetCaptureState();
        using (NativeHookScope _ = NativeHookScope.Install("GetDisplayNameNativeFunction", nameof(CaptureDisplayIdReturnPointer)))
        {
            TestAssert.Equal<string?>(null, SDL3.SDL.GetDisplayName(66), "SDL.GetDisplayName must return null for native null.");
            TestAssert.Equal(66u, capturedDisplayID, "SDL.GetDisplayName null path must forward displayID.");
        }
    }

    public static void DisplayBoundsOrientationAndScaleFunctions_ForwardInputsOutputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextSystemTheme = SDL3.SDL.SystemTheme.Dark;
        using (NativeHookScope _ = NativeHookScope.Install("GetSystemThemeNativeFunction", nameof(ReturnNextSystemTheme)))
        {
            TestAssert.Equal(SDL3.SDL.SystemTheme.Dark, SDL3.SDL.GetSystemTheme(), "SDL.GetSystemTheme must return native enum value.");
        }

        ResetCaptureState();
        nextBool = true;
        nextRect = new SDL3.SDL.Rect { X = 1, Y = 2, W = 3, H = 4 };
        using (NativeHookScope _ = NativeHookScope.Install("GetDisplayBoundsNativeFunction", nameof(CaptureDisplayIdReturnRect)))
        {
            bool actual = SDL3.SDL.GetDisplayBounds(77, out SDL3.SDL.Rect rect);

            TestAssert.Equal(true, actual, "SDL.GetDisplayBounds must return native success value.");
            TestAssert.Equal(77u, capturedDisplayID, "SDL.GetDisplayBounds must forward displayID.");
            AssertRect(nextRect, rect, "SDL.GetDisplayBounds must return native rect.");
        }

        ResetCaptureState();
        nextBool = false;
        nextRect = new SDL3.SDL.Rect { X = 5, Y = 6, W = 7, H = 8 };
        using (NativeHookScope _ = NativeHookScope.Install("GetDisplayUsableBoundsNativeFunction", nameof(CaptureDisplayIdReturnRect)))
        {
            bool actual = SDL3.SDL.GetDisplayUsableBounds(88, out SDL3.SDL.Rect rect);

            TestAssert.Equal(false, actual, "SDL.GetDisplayUsableBounds must return native failure value.");
            TestAssert.Equal(88u, capturedDisplayID, "SDL.GetDisplayUsableBounds must forward displayID.");
            AssertRect(nextRect, rect, "SDL.GetDisplayUsableBounds must return native rect.");
        }

        ResetCaptureState();
        nextDisplayOrientation = SDL3.SDL.DisplayOrientation.Landscape;
        using (NativeHookScope _ = NativeHookScope.Install("GetNaturalDisplayOrientationNativeFunction", nameof(CaptureDisplayIdReturnOrientation)))
        {
            TestAssert.Equal(SDL3.SDL.DisplayOrientation.Landscape, SDL3.SDL.GetNaturalDisplayOrientation(99), "SDL.GetNaturalDisplayOrientation must return native enum value.");
            TestAssert.Equal(99u, capturedDisplayID, "SDL.GetNaturalDisplayOrientation must forward displayID.");
        }

        ResetCaptureState();
        nextDisplayOrientation = SDL3.SDL.DisplayOrientation.PortraitFlipped;
        using (NativeHookScope _ = NativeHookScope.Install("GetCurrentDisplayOrientationNativeFunction", nameof(CaptureDisplayIdReturnOrientation)))
        {
            TestAssert.Equal(SDL3.SDL.DisplayOrientation.PortraitFlipped, SDL3.SDL.GetCurrentDisplayOrientation(100), "SDL.GetCurrentDisplayOrientation must return native enum value.");
            TestAssert.Equal(100u, capturedDisplayID, "SDL.GetCurrentDisplayOrientation must forward displayID.");
        }

        ResetCaptureState();
        nextFloat = 1.75f;
        using (NativeHookScope _ = NativeHookScope.Install("GetDisplayContentScaleNativeFunction", nameof(CaptureDisplayIdReturnFloat)))
        {
            TestAssert.Equal(1.75f, SDL3.SDL.GetDisplayContentScale(111), "SDL.GetDisplayContentScale must return native scale.");
            TestAssert.Equal(111u, capturedDisplayID, "SDL.GetDisplayContentScale must forward displayID.");
        }
    }

    public static void DisplayModeFunctions_ForwardInputsOutputsAndManageNativePointers()
    {
        ResetCaptureState();
        SDL3.SDL.DisplayMode[] expectedModes =
        [
            CreateDisplayMode(10, SDL3.SDL.PixelFormat.RGBA8888, 1920, 1080, 1.0f, 60.0f, 60, 1),
            CreateDisplayMode(20, SDL3.SDL.PixelFormat.RGB24, 3840, 2160, 2.0f, 120.0f, 120, 1)
        ];
        IntPtr[] modePointers = AllocateDisplayModes(expectedModes);
        nextPointer = AllocatePointerArray(modePointers);
        nextCount = expectedModes.Length;
        using (NativeHookScope modesHook = NativeHookScope.Install("GetFullscreenDisplayModesNativeFunction", nameof(CaptureFullscreenDisplayModes)))
        using (NativeHookScope freeHook = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree)))
        {
            try
            {
                SDL3.SDL.DisplayMode[]? actual = SDL3.SDL.GetFullscreenDisplayModes(121, out int count);

                TestAssert.Equal(121u, capturedDisplayID, "SDL.GetFullscreenDisplayModes must forward displayID.");
                TestAssert.Equal(expectedModes.Length, count, "SDL.GetFullscreenDisplayModes must return native count.");
                TestAssert.NotNull(actual, "SDL.GetFullscreenDisplayModes must copy native display mode pointer arrays.");
                TestAssert.Equal(expectedModes.Length, actual!.Length, "SDL.GetFullscreenDisplayModes must preserve mode count.");
                for (int i = 0; i < expectedModes.Length; i++)
                {
                    AssertDisplayMode(expectedModes[i], actual[i], $"SDL.GetFullscreenDisplayModes must copy display mode {i}.");
                }

                TestAssert.Equal(nextPointer, capturedFreePointer, "SDL.GetFullscreenDisplayModes must free the native pointer array.");
                TestAssert.Equal(1, capturedFreeCallCount, "SDL.GetFullscreenDisplayModes must free the native pointer array once.");
            }
            finally
            {
                FreeDisplayModePointers(modePointers);
                Marshal.FreeHGlobal(nextPointer);
                nextPointer = IntPtr.Zero;
            }
        }

        ResetCaptureState();
        nextCount = 2;
        using (NativeHookScope modesHook = NativeHookScope.Install("GetFullscreenDisplayModesNativeFunction", nameof(CaptureFullscreenDisplayModes)))
        using (NativeHookScope freeHook = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree)))
        {
            SDL3.SDL.DisplayMode[]? actual = SDL3.SDL.GetFullscreenDisplayModes(122, out int count);

            TestAssert.Equal(122u, capturedDisplayID, "SDL.GetFullscreenDisplayModes null path must forward displayID.");
            TestAssert.Equal(2, count, "SDL.GetFullscreenDisplayModes null path must expose native count.");
            TestAssert.Equal<SDL3.SDL.DisplayMode[]?>(null, actual, "SDL.GetFullscreenDisplayModes must return null for native null.");
            TestAssert.Equal(0, capturedFreeCallCount, "SDL.GetFullscreenDisplayModes must not free native null.");
        }

        ResetCaptureState();
        nextBool = true;
        nextDisplayMode = CreateDisplayMode(30, SDL3.SDL.PixelFormat.ABGR8888, 1280, 720, 1.5f, 144.0f, 144, 1);
        using (NativeHookScope _ = NativeHookScope.Install("GetClosestFullscreenDisplayModeNativeFunction", nameof(CaptureClosestFullscreenDisplayMode)))
        {
            bool actual = SDL3.SDL.GetClosestFullscreenDisplayMode(123, 1270, 710, 143.5f, true, out SDL3.SDL.DisplayMode closest);

            TestAssert.Equal(true, actual, "SDL.GetClosestFullscreenDisplayMode must return native success value.");
            TestAssert.Equal(123u, capturedDisplayID, "SDL.GetClosestFullscreenDisplayMode must forward displayID.");
            TestAssert.Equal(1270, capturedWidth, "SDL.GetClosestFullscreenDisplayMode must forward width.");
            TestAssert.Equal(710, capturedHeight, "SDL.GetClosestFullscreenDisplayMode must forward height.");
            TestAssert.Equal(143.5f, capturedRefreshRate, "SDL.GetClosestFullscreenDisplayMode must forward refresh rate.");
            TestAssert.Equal(true, capturedIncludeHighDensityModes, "SDL.GetClosestFullscreenDisplayMode must forward includeHighDensityModes.");
            AssertDisplayMode(nextDisplayMode, closest, "SDL.GetClosestFullscreenDisplayMode must return native closest mode.");
        }

        ResetCaptureState();
        nextDisplayMode = CreateDisplayMode(40, SDL3.SDL.PixelFormat.XRGB8888, 1600, 900, 1.0f, 75.0f, 75, 1);
        nextPointer = AllocateDisplayMode(nextDisplayMode);
        using (NativeHookScope _ = NativeHookScope.Install("GetDesktopDisplayModeNativeFunction", nameof(CaptureDisplayIdReturnPointer)))
        {
            try
            {
                SDL3.SDL.DisplayMode? actual = SDL3.SDL.GetDesktopDisplayMode(124);

                TestAssert.Equal(124u, capturedDisplayID, "SDL.GetDesktopDisplayMode must forward displayID.");
                TestAssert.True(actual.HasValue, "SDL.GetDesktopDisplayMode must return a managed display mode for native pointers.");
                AssertDisplayMode(nextDisplayMode, actual!.Value, "SDL.GetDesktopDisplayMode must copy native display mode.");
            }
            finally
            {
                Marshal.FreeHGlobal(nextPointer);
                nextPointer = IntPtr.Zero;
            }
        }

        ResetCaptureState();
        using (NativeHookScope _ = NativeHookScope.Install("GetDesktopDisplayModeNativeFunction", nameof(CaptureDisplayIdReturnPointer)))
        {
            SDL3.SDL.DisplayMode? actual = SDL3.SDL.GetDesktopDisplayMode(125);

            TestAssert.Equal(125u, capturedDisplayID, "SDL.GetDesktopDisplayMode null path must forward displayID.");
            TestAssert.Equal<SDL3.SDL.DisplayMode?>(null, actual, "SDL.GetDesktopDisplayMode must return null for native null.");
        }

        ResetCaptureState();
        nextDisplayMode = CreateDisplayMode(50, SDL3.SDL.PixelFormat.RGBX8888, 2560, 1440, 1.25f, 165.0f, 165, 1);
        nextPointer = AllocateDisplayMode(nextDisplayMode);
        using (NativeHookScope _ = NativeHookScope.Install("GetCurrentDisplayModeNativeFunction", nameof(CaptureDisplayIdReturnPointer)))
        {
            try
            {
                SDL3.SDL.DisplayMode? actual = SDL3.SDL.GetCurrentDisplayMode(126);

                TestAssert.Equal(126u, capturedDisplayID, "SDL.GetCurrentDisplayMode must forward displayID.");
                TestAssert.True(actual.HasValue, "SDL.GetCurrentDisplayMode must return a managed display mode for native pointers.");
                AssertDisplayMode(nextDisplayMode, actual!.Value, "SDL.GetCurrentDisplayMode must copy native display mode.");
            }
            finally
            {
                Marshal.FreeHGlobal(nextPointer);
                nextPointer = IntPtr.Zero;
            }
        }

        ResetCaptureState();
        using (NativeHookScope _ = NativeHookScope.Install("GetCurrentDisplayModeNativeFunction", nameof(CaptureDisplayIdReturnPointer)))
        {
            SDL3.SDL.DisplayMode? actual = SDL3.SDL.GetCurrentDisplayMode(127);

            TestAssert.Equal(127u, capturedDisplayID, "SDL.GetCurrentDisplayMode null path must forward displayID.");
            TestAssert.Equal<SDL3.SDL.DisplayMode?>(null, actual, "SDL.GetCurrentDisplayMode must return null for native null.");
        }
    }

    public static void DisplayLookupAndWindowScaleFunctions_ForwardInputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextUInt = 0x501u;
        SDL3.SDL.Point point = new() { X = -10, Y = 20 };
        using (NativeHookScope _ = NativeHookScope.Install("GetDisplayForPointNativeFunction", nameof(CapturePointReturnUInt)))
        {
            TestAssert.Equal(0x501u, SDL3.SDL.GetDisplayForPoint(point), "SDL.GetDisplayForPoint must return native display id.");
            AssertPoint(point, capturedPoint, "SDL.GetDisplayForPoint must forward point.");
        }

        ResetCaptureState();
        nextUInt = 0x502u;
        SDL3.SDL.Rect rect = new() { X = 1, Y = 2, W = 640, H = 480 };
        using (NativeHookScope _ = NativeHookScope.Install("GetDisplayForRectNativeFunction", nameof(CaptureRectReturnUInt)))
        {
            TestAssert.Equal(0x502u, SDL3.SDL.GetDisplayForRect(rect), "SDL.GetDisplayForRect must return native display id.");
            AssertRect(rect, capturedRect, "SDL.GetDisplayForRect must forward rect.");
        }

        ResetCaptureState();
        nextUInt = 0x503u;
        using (NativeHookScope _ = NativeHookScope.Install("GetDisplayForWindowNativeFunction", nameof(CaptureWindowReturnUInt)))
        {
            TestAssert.Equal(0x503u, SDL3.SDL.GetDisplayForWindow((IntPtr)0x504), "SDL.GetDisplayForWindow must return native display id.");
            TestAssert.Equal((IntPtr)0x504, capturedWindow, "SDL.GetDisplayForWindow must forward window.");
        }

        ResetCaptureState();
        nextFloat = 2.25f;
        using (NativeHookScope _ = NativeHookScope.Install("GetWindowPixelDensityNativeFunction", nameof(CaptureWindowReturnFloat)))
        {
            TestAssert.Equal(2.25f, SDL3.SDL.GetWindowPixelDensity((IntPtr)0x505), "SDL.GetWindowPixelDensity must return native density.");
            TestAssert.Equal((IntPtr)0x505, capturedWindow, "SDL.GetWindowPixelDensity must forward window.");
        }

        ResetCaptureState();
        nextFloat = 1.5f;
        using (NativeHookScope _ = NativeHookScope.Install("GetWindowDisplayScaleNativeFunction", nameof(CaptureWindowReturnFloat)))
        {
            TestAssert.Equal(1.5f, SDL3.SDL.GetWindowDisplayScale((IntPtr)0x506), "SDL.GetWindowDisplayScale must return native scale.");
            TestAssert.Equal((IntPtr)0x506, capturedWindow, "SDL.GetWindowDisplayScale must forward window.");
        }
    }

    public static void WindowFullscreenProfileAndPixelFormatFunctions_ForwardInputsOutputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetWindowFullscreenModePointerNativeFunction", nameof(CaptureSetWindowFullscreenModePointer)))
        {
            bool actual = SDL3.SDL.SetWindowFullscreenMode((IntPtr)0x601, (IntPtr)0x602);

            TestAssert.Equal(true, actual, "SDL.SetWindowFullscreenMode(IntPtr) must return native success value.");
            TestAssert.Equal((IntPtr)0x601, capturedWindow, "SDL.SetWindowFullscreenMode(IntPtr) must forward window.");
            TestAssert.Equal((IntPtr)0x602, capturedModePointer, "SDL.SetWindowFullscreenMode(IntPtr) must forward mode pointer.");
        }

        ResetCaptureState();
        nextBool = false;
        SDL3.SDL.DisplayMode mode = CreateDisplayMode(60, SDL3.SDL.PixelFormat.BGRA8888, 1024, 768, 1.0f, 60.0f, 60, 1);
        using (NativeHookScope _ = NativeHookScope.Install("SetWindowFullscreenModeModeNativeFunction", nameof(CaptureSetWindowFullscreenModeMode)))
        {
            bool actual = SDL3.SDL.SetWindowFullscreenMode((IntPtr)0x603, mode);

            TestAssert.Equal(false, actual, "SDL.SetWindowFullscreenMode(DisplayMode) must return native failure value.");
            TestAssert.Equal((IntPtr)0x603, capturedWindow, "SDL.SetWindowFullscreenMode(DisplayMode) must forward window.");
            AssertDisplayMode(mode, capturedDisplayMode, "SDL.SetWindowFullscreenMode(DisplayMode) must forward mode.");
        }

        ResetCaptureState();
        nextDisplayMode = CreateDisplayMode(70, SDL3.SDL.PixelFormat.RGBA8888, 800, 600, 1.25f, 75.0f, 75, 1);
        nextPointer = AllocateDisplayMode(nextDisplayMode);
        using (NativeHookScope _ = NativeHookScope.Install("GetWindowFullscreenModeNativeFunction", nameof(CaptureWindowReturnPointer)))
        {
            try
            {
                SDL3.SDL.DisplayMode? actual = SDL3.SDL.GetWindowFullscreenMode((IntPtr)0x604);

                TestAssert.Equal((IntPtr)0x604, capturedWindow, "SDL.GetWindowFullscreenMode must forward window.");
                TestAssert.True(actual.HasValue, "SDL.GetWindowFullscreenMode must return a managed display mode for native pointers.");
                AssertDisplayMode(nextDisplayMode, actual!.Value, "SDL.GetWindowFullscreenMode must copy native display mode.");
            }
            finally
            {
                Marshal.FreeHGlobal(nextPointer);
                nextPointer = IntPtr.Zero;
            }
        }

        ResetCaptureState();
        using (NativeHookScope _ = NativeHookScope.Install("GetWindowFullscreenModeNativeFunction", nameof(CaptureWindowReturnPointer)))
        {
            SDL3.SDL.DisplayMode? actual = SDL3.SDL.GetWindowFullscreenMode((IntPtr)0x605);

            TestAssert.Equal((IntPtr)0x605, capturedWindow, "SDL.GetWindowFullscreenMode null path must forward window.");
            TestAssert.Equal<SDL3.SDL.DisplayMode?>(null, actual, "SDL.GetWindowFullscreenMode must return null for native null.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x606;
        nextSize = (UIntPtr)32;
        using (NativeHookScope _ = NativeHookScope.Install("GetWindowICCProfileNativeFunction", nameof(CaptureWindowReturnPointerAndSize)))
        {
            IntPtr actual = SDL3.SDL.GetWindowICCProfile((IntPtr)0x607, out UIntPtr size);

            TestAssert.Equal((IntPtr)0x607, capturedWindow, "SDL.GetWindowICCProfile must forward window.");
            TestAssert.Equal((IntPtr)0x606, actual, "SDL.GetWindowICCProfile must return native profile pointer.");
            TestAssert.Equal((UIntPtr)32, size, "SDL.GetWindowICCProfile must return native profile size.");
        }

        ResetCaptureState();
        nextPixelFormat = SDL3.SDL.PixelFormat.ARGB8888;
        using (NativeHookScope _ = NativeHookScope.Install("GetWindowPixelFormatNativeFunction", nameof(CaptureWindowReturnPixelFormat)))
        {
            SDL3.SDL.PixelFormat actual = SDL3.SDL.GetWindowPixelFormat((IntPtr)0x608);

            TestAssert.Equal((IntPtr)0x608, capturedWindow, "SDL.GetWindowPixelFormat must forward window.");
            TestAssert.Equal(SDL3.SDL.PixelFormat.ARGB8888, actual, "SDL.GetWindowPixelFormat must return native pixel format.");
        }
    }

    public static void WindowListFunctions_ManageNativePointerArrays()
    {
        ResetCaptureState();
        IntPtr[] expectedWindows = [(IntPtr)0x701, (IntPtr)0x702, (IntPtr)0x703];
        nextPointer = AllocatePointerArray(expectedWindows);
        nextCount = expectedWindows.Length;
        using (NativeHookScope windowsHook = NativeHookScope.Install("GetWindowsNativeFunction", nameof(ReturnWindowPointerArray)))
        using (NativeHookScope freeHook = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree)))
        {
            try
            {
                IntPtr[]? actual = SDL3.SDL.GetWindows(out int count);

                TestAssert.Equal(expectedWindows.Length, count, "SDL.GetWindows must return native count.");
                TestAssert.NotNull(actual, "SDL.GetWindows must copy native window pointer arrays.");
                TestAssert.Equal(expectedWindows.Length, actual!.Length, "SDL.GetWindows must preserve window count.");
                for (int i = 0; i < expectedWindows.Length; i++)
                {
                    TestAssert.Equal(expectedWindows[i], actual[i], $"SDL.GetWindows must copy window pointer {i}.");
                }

                TestAssert.Equal(nextPointer, capturedFreePointer, "SDL.GetWindows must free the native pointer array.");
                TestAssert.Equal(1, capturedFreeCallCount, "SDL.GetWindows must free the native pointer array once.");
            }
            finally
            {
                Marshal.FreeHGlobal(nextPointer);
                nextPointer = IntPtr.Zero;
            }
        }

        ResetCaptureState();
        nextCount = 2;
        using (NativeHookScope windowsHook = NativeHookScope.Install("GetWindowsNativeFunction", nameof(ReturnWindowPointerArray)))
        using (NativeHookScope freeHook = NativeHookScope.Install("FreeNativeFunction", nameof(CaptureFree)))
        {
            IntPtr[]? actual = SDL3.SDL.GetWindows(out int count);

            TestAssert.Equal(2, count, "SDL.GetWindows null path must expose native count.");
            TestAssert.Equal<IntPtr[]?>(null, actual, "SDL.GetWindows must return null for native null.");
            TestAssert.Equal(0, capturedFreeCallCount, "SDL.GetWindows must not free native null.");
        }
    }

    public static void WindowCreationAndIdentityFunctions_ForwardInputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextPointer = (IntPtr)0x801;
        SDL3.SDL.WindowFlags createFlags = SDL3.SDL.WindowFlags.OpenGL | SDL3.SDL.WindowFlags.Resizable;
        using (NativeHookScope _ = NativeHookScope.Install("CreateWindowNativeFunction", nameof(CaptureCreateWindow)))
        {
            IntPtr actual = SDL3.SDL.CreateWindow("Main window", 800, 600, createFlags);

            TestAssert.Equal((IntPtr)0x801, actual, "SDL.CreateWindow must return native window pointer.");
            TestAssert.Equal("Main window", capturedTitle, "SDL.CreateWindow must forward title.");
            TestAssert.Equal(800, capturedWidth, "SDL.CreateWindow must forward width.");
            TestAssert.Equal(600, capturedHeight, "SDL.CreateWindow must forward height.");
            TestAssert.Equal(createFlags, capturedWindowFlags, "SDL.CreateWindow must forward flags.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x802;
        SDL3.SDL.WindowFlags popupFlags = SDL3.SDL.WindowFlags.Tooltip | SDL3.SDL.WindowFlags.NotFocusable;
        using (NativeHookScope _ = NativeHookScope.Install("CreatePopupWindowNativeFunction", nameof(CaptureCreatePopupWindow)))
        {
            IntPtr actual = SDL3.SDL.CreatePopupWindow((IntPtr)0x803, 10, -20, 320, 240, popupFlags);

            TestAssert.Equal((IntPtr)0x802, actual, "SDL.CreatePopupWindow must return native window pointer.");
            TestAssert.Equal((IntPtr)0x803, capturedWindow, "SDL.CreatePopupWindow must forward parent.");
            TestAssert.Equal(10, capturedOffsetX, "SDL.CreatePopupWindow must forward offsetX.");
            TestAssert.Equal(-20, capturedOffsetY, "SDL.CreatePopupWindow must forward offsetY.");
            TestAssert.Equal(320, capturedWidth, "SDL.CreatePopupWindow must forward width.");
            TestAssert.Equal(240, capturedHeight, "SDL.CreatePopupWindow must forward height.");
            TestAssert.Equal(popupFlags, capturedWindowFlags, "SDL.CreatePopupWindow must forward flags.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x804;
        using (NativeHookScope _ = NativeHookScope.Install("CreateWindowWithPropertiesNativeFunction", nameof(CapturePropertiesReturnPointer)))
        {
            IntPtr actual = SDL3.SDL.CreateWindowWithProperties(0x805u);

            TestAssert.Equal((IntPtr)0x804, actual, "SDL.CreateWindowWithProperties must return native window pointer.");
            TestAssert.Equal(0x805u, capturedProperties, "SDL.CreateWindowWithProperties must forward properties id.");
        }

        ResetCaptureState();
        nextUInt = 0x806u;
        using (NativeHookScope _ = NativeHookScope.Install("GetWindowIDNativeFunction", nameof(CaptureWindowReturnUInt)))
        {
            TestAssert.Equal(0x806u, SDL3.SDL.GetWindowID((IntPtr)0x807), "SDL.GetWindowID must return native window id.");
            TestAssert.Equal((IntPtr)0x807, capturedWindow, "SDL.GetWindowID must forward window.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x808;
        using (NativeHookScope _ = NativeHookScope.Install("GetWindowFromIDNativeFunction", nameof(CaptureWindowIdReturnPointer)))
        {
            TestAssert.Equal((IntPtr)0x808, SDL3.SDL.GetWindowFromID(0x809u), "SDL.GetWindowFromID must return native window pointer.");
            TestAssert.Equal(0x809u, capturedWindowID, "SDL.GetWindowFromID must forward window id.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0x80A;
        using (NativeHookScope _ = NativeHookScope.Install("GetWindowParentNativeFunction", nameof(CaptureWindowReturnPointer)))
        {
            TestAssert.Equal((IntPtr)0x80A, SDL3.SDL.GetWindowParent((IntPtr)0x80B), "SDL.GetWindowParent must return native parent pointer.");
            TestAssert.Equal((IntPtr)0x80B, capturedWindow, "SDL.GetWindowParent must forward window.");
        }

        ResetCaptureState();
        nextUInt = 0x80Cu;
        using (NativeHookScope _ = NativeHookScope.Install("GetWindowPropertiesNativeFunction", nameof(CaptureWindowReturnUInt)))
        {
            TestAssert.Equal(0x80Cu, SDL3.SDL.GetWindowProperties((IntPtr)0x80D), "SDL.GetWindowProperties must return native properties id.");
            TestAssert.Equal((IntPtr)0x80D, capturedWindow, "SDL.GetWindowProperties must forward window.");
        }

        ResetCaptureState();
        nextWindowFlags = SDL3.SDL.WindowFlags.Hidden | SDL3.SDL.WindowFlags.Borderless;
        using (NativeHookScope _ = NativeHookScope.Install("GetWindowFlagsNativeFunction", nameof(CaptureWindowReturnFlags)))
        {
            TestAssert.Equal(nextWindowFlags, SDL3.SDL.GetWindowFlags((IntPtr)0x80E), "SDL.GetWindowFlags must return native flags.");
            TestAssert.Equal((IntPtr)0x80E, capturedWindow, "SDL.GetWindowFlags must forward window.");
        }
    }

    public static void WindowTitleFunctions_ForwardAndConvertStrings()
    {
        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetWindowTitleNativeFunction", nameof(CaptureSetWindowTitle)))
        {
            bool actual = SDL3.SDL.SetWindowTitle((IntPtr)0x901, "Updated title");

            TestAssert.Equal(true, actual, "SDL.SetWindowTitle must return native success value.");
            TestAssert.Equal((IntPtr)0x901, capturedWindow, "SDL.SetWindowTitle must forward window.");
            TestAssert.Equal("Updated title", capturedTitle, "SDL.SetWindowTitle must forward title.");
        }

        ResetCaptureState();
        nextPointer = Marshal.StringToCoTaskMemUTF8("Current title");
        using (NativeHookScope _ = NativeHookScope.Install("GetWindowTitleNativeFunction", nameof(CaptureWindowReturnPointer)))
        {
            try
            {
                TestAssert.Equal("Current title", SDL3.SDL.GetWindowTitle((IntPtr)0x902), "SDL.GetWindowTitle must convert native UTF-8 text.");
                TestAssert.Equal((IntPtr)0x902, capturedWindow, "SDL.GetWindowTitle must forward window.");
            }
            finally
            {
                Marshal.FreeCoTaskMem(nextPointer);
                nextPointer = IntPtr.Zero;
            }
        }

        ResetCaptureState();
        using (NativeHookScope _ = NativeHookScope.Install("GetWindowTitleNativeFunction", nameof(CaptureWindowReturnPointer)))
        {
            TestAssert.Equal(string.Empty, SDL3.SDL.GetWindowTitle((IntPtr)0x903), "SDL.GetWindowTitle must return an empty string for native null.");
            TestAssert.Equal((IntPtr)0x903, capturedWindow, "SDL.GetWindowTitle null path must forward window.");
        }
    }

    public static void WindowGeometryFunctions_ForwardInputsOutputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetWindowIconNativeFunction", nameof(CaptureSetWindowIcon)))
        {
            bool actual = SDL3.SDL.SetWindowIcon((IntPtr)0xA01, (IntPtr)0xA02);

            TestAssert.Equal(true, actual, "SDL.SetWindowIcon must return native success value.");
            TestAssert.Equal((IntPtr)0xA01, capturedWindow, "SDL.SetWindowIcon must forward window.");
            TestAssert.Equal((IntPtr)0xA02, capturedIcon, "SDL.SetWindowIcon must forward icon.");
        }

        ResetCaptureState();
        nextBool = false;
        using (NativeHookScope _ = NativeHookScope.Install("SetWindowPositionNativeFunction", nameof(CaptureSetWindowPosition)))
        {
            bool actual = SDL3.SDL.SetWindowPosition((IntPtr)0xA03, -11, 22);

            TestAssert.Equal(false, actual, "SDL.SetWindowPosition must return native failure value.");
            TestAssert.Equal((IntPtr)0xA03, capturedWindow, "SDL.SetWindowPosition must forward window.");
            TestAssert.Equal(-11, capturedX, "SDL.SetWindowPosition must forward x.");
            TestAssert.Equal(22, capturedY, "SDL.SetWindowPosition must forward y.");
        }

        ResetCaptureState();
        nextBool = true;
        nextX = -30;
        nextY = 40;
        using (NativeHookScope _ = NativeHookScope.Install("GetWindowPositionNativeFunction", nameof(CaptureWindowReturnXY)))
        {
            bool actual = SDL3.SDL.GetWindowPosition((IntPtr)0xA04, out int x, out int y);

            TestAssert.Equal(true, actual, "SDL.GetWindowPosition must return native success value.");
            TestAssert.Equal((IntPtr)0xA04, capturedWindow, "SDL.GetWindowPosition must forward window.");
            TestAssert.Equal(-30, x, "SDL.GetWindowPosition must return native x.");
            TestAssert.Equal(40, y, "SDL.GetWindowPosition must return native y.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetWindowSizeNativeFunction", nameof(CaptureSetWindowSize)))
        {
            bool actual = SDL3.SDL.SetWindowSize((IntPtr)0xA05, 1024, 768);

            TestAssert.Equal(true, actual, "SDL.SetWindowSize must return native success value.");
            TestAssert.Equal((IntPtr)0xA05, capturedWindow, "SDL.SetWindowSize must forward window.");
            TestAssert.Equal(1024, capturedWidth, "SDL.SetWindowSize must forward width.");
            TestAssert.Equal(768, capturedHeight, "SDL.SetWindowSize must forward height.");
        }

        ResetCaptureState();
        nextBool = false;
        nextWidth = 1280;
        nextHeight = 720;
        using (NativeHookScope _ = NativeHookScope.Install("GetWindowSizeNativeFunction", nameof(CaptureWindowReturnSize)))
        {
            bool actual = SDL3.SDL.GetWindowSize((IntPtr)0xA06, out int w, out int h);

            TestAssert.Equal(false, actual, "SDL.GetWindowSize must return native failure value.");
            TestAssert.Equal((IntPtr)0xA06, capturedWindow, "SDL.GetWindowSize must forward window.");
            TestAssert.Equal(1280, w, "SDL.GetWindowSize must return native width.");
            TestAssert.Equal(720, h, "SDL.GetWindowSize must return native height.");
        }

        ResetCaptureState();
        nextBool = true;
        nextRect = new SDL3.SDL.Rect { X = 1, Y = 2, W = 300, H = 200 };
        using (NativeHookScope _ = NativeHookScope.Install("GetWindowSafeAreaNativeFunction", nameof(CaptureWindowReturnRect)))
        {
            bool actual = SDL3.SDL.GetWindowSafeArea((IntPtr)0xA07, out SDL3.SDL.Rect rect);

            TestAssert.Equal(true, actual, "SDL.GetWindowSafeArea must return native success value.");
            TestAssert.Equal((IntPtr)0xA07, capturedWindow, "SDL.GetWindowSafeArea must forward window.");
            AssertRect(nextRect, rect, "SDL.GetWindowSafeArea must return native rect.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetWindowAspectRatioNativeFunction", nameof(CaptureSetWindowAspectRatio)))
        {
            bool actual = SDL3.SDL.SetWindowAspectRatio((IntPtr)0xA08, 1.25f, 2.5f);

            TestAssert.Equal(true, actual, "SDL.SetWindowAspectRatio must return native success value.");
            TestAssert.Equal((IntPtr)0xA08, capturedWindow, "SDL.SetWindowAspectRatio must forward window.");
            TestAssert.Equal(1.25f, capturedMinAspect, "SDL.SetWindowAspectRatio must forward min aspect.");
            TestAssert.Equal(2.5f, capturedMaxAspect, "SDL.SetWindowAspectRatio must forward max aspect.");
        }

        ResetCaptureState();
        nextBool = false;
        nextMinAspect = 1.1f;
        nextMaxAspect = 1.9f;
        using (NativeHookScope _ = NativeHookScope.Install("GetWindowAspectRatioNativeFunction", nameof(CaptureWindowReturnAspectRatio)))
        {
            bool actual = SDL3.SDL.GetWindowAspectRatio((IntPtr)0xA09, out float minAspect, out float maxAspect);

            TestAssert.Equal(false, actual, "SDL.GetWindowAspectRatio must return native failure value.");
            TestAssert.Equal((IntPtr)0xA09, capturedWindow, "SDL.GetWindowAspectRatio must forward window.");
            TestAssert.Equal(1.1f, minAspect, "SDL.GetWindowAspectRatio must return native min aspect.");
            TestAssert.Equal(1.9f, maxAspect, "SDL.GetWindowAspectRatio must return native max aspect.");
        }

        ResetCaptureState();
        nextBool = true;
        nextTop = 3;
        nextLeft = 4;
        nextBottom = 5;
        nextRight = 6;
        using (NativeHookScope _ = NativeHookScope.Install("GetWindowBordersSizeNativeFunction", nameof(CaptureWindowReturnBorders)))
        {
            bool actual = SDL3.SDL.GetWindowBordersSize((IntPtr)0xA0A, out int top, out int left, out int bottom, out int right);

            TestAssert.Equal(true, actual, "SDL.GetWindowBordersSize must return native success value.");
            TestAssert.Equal((IntPtr)0xA0A, capturedWindow, "SDL.GetWindowBordersSize must forward window.");
            TestAssert.Equal(3, top, "SDL.GetWindowBordersSize must return native top.");
            TestAssert.Equal(4, left, "SDL.GetWindowBordersSize must return native left.");
            TestAssert.Equal(5, bottom, "SDL.GetWindowBordersSize must return native bottom.");
            TestAssert.Equal(6, right, "SDL.GetWindowBordersSize must return native right.");
        }

        ResetCaptureState();
        nextBool = true;
        nextWidth = 2560;
        nextHeight = 1440;
        using (NativeHookScope _ = NativeHookScope.Install("GetWindowSizeInPixelsNativeFunction", nameof(CaptureWindowReturnSize)))
        {
            bool actual = SDL3.SDL.GetWindowSizeInPixels((IntPtr)0xA0B, out int w, out int h);

            TestAssert.Equal(true, actual, "SDL.GetWindowSizeInPixels must return native success value.");
            TestAssert.Equal((IntPtr)0xA0B, capturedWindow, "SDL.GetWindowSizeInPixels must forward window.");
            TestAssert.Equal(2560, w, "SDL.GetWindowSizeInPixels must return native width.");
            TestAssert.Equal(1440, h, "SDL.GetWindowSizeInPixels must return native height.");
        }

        ResetCaptureState();
        nextBool = false;
        using (NativeHookScope _ = NativeHookScope.Install("SetWindowMinimumSizeNativeFunction", nameof(CaptureSetWindowSize)))
        {
            bool actual = SDL3.SDL.SetWindowMinimumSize((IntPtr)0xA0C, 320, 200);

            TestAssert.Equal(false, actual, "SDL.SetWindowMinimumSize must return native failure value.");
            TestAssert.Equal((IntPtr)0xA0C, capturedWindow, "SDL.SetWindowMinimumSize must forward window.");
            TestAssert.Equal(320, capturedWidth, "SDL.SetWindowMinimumSize must forward min width.");
            TestAssert.Equal(200, capturedHeight, "SDL.SetWindowMinimumSize must forward min height.");
        }

        ResetCaptureState();
        nextBool = true;
        nextWidth = 321;
        nextHeight = 201;
        using (NativeHookScope _ = NativeHookScope.Install("GetWindowMinimumSizeNativeFunction", nameof(CaptureWindowReturnSize)))
        {
            bool actual = SDL3.SDL.GetWindowMinimumSize((IntPtr)0xA0D, out int w, out int h);

            TestAssert.Equal(true, actual, "SDL.GetWindowMinimumSize must return native success value.");
            TestAssert.Equal((IntPtr)0xA0D, capturedWindow, "SDL.GetWindowMinimumSize must forward window.");
            TestAssert.Equal(321, w, "SDL.GetWindowMinimumSize must return native min width.");
            TestAssert.Equal(201, h, "SDL.GetWindowMinimumSize must return native min height.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetWindowMaximumSizeNativeFunction", nameof(CaptureSetWindowSize)))
        {
            bool actual = SDL3.SDL.SetWindowMaximumSize((IntPtr)0xA0E, 3840, 2160);

            TestAssert.Equal(true, actual, "SDL.SetWindowMaximumSize must return native success value.");
            TestAssert.Equal((IntPtr)0xA0E, capturedWindow, "SDL.SetWindowMaximumSize must forward window.");
            TestAssert.Equal(3840, capturedWidth, "SDL.SetWindowMaximumSize must forward max width.");
            TestAssert.Equal(2160, capturedHeight, "SDL.SetWindowMaximumSize must forward max height.");
        }

        ResetCaptureState();
        nextBool = false;
        nextWidth = 3839;
        nextHeight = 2159;
        using (NativeHookScope _ = NativeHookScope.Install("GetWindowMaximumSizeNativeFunction", nameof(CaptureWindowReturnSize)))
        {
            bool actual = SDL3.SDL.GetWindowMaximumSize((IntPtr)0xA0F, out int w, out int h);

            TestAssert.Equal(false, actual, "SDL.GetWindowMaximumSize must return native failure value.");
            TestAssert.Equal((IntPtr)0xA0F, capturedWindow, "SDL.GetWindowMaximumSize must forward window.");
            TestAssert.Equal(3839, w, "SDL.GetWindowMaximumSize must return native max width.");
            TestAssert.Equal(2159, h, "SDL.GetWindowMaximumSize must return native max height.");
        }
    }

    public static void WindowStateFunctions_ForwardInputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetWindowBorderedNativeFunction", nameof(CaptureWindowBoolReturnBool)))
        {
            bool actual = SDL3.SDL.SetWindowBordered((IntPtr)0xB01, true);

            TestAssert.Equal(true, actual, "SDL.SetWindowBordered must return native success value.");
            TestAssert.Equal((IntPtr)0xB01, capturedWindow, "SDL.SetWindowBordered must forward window.");
            TestAssert.Equal(true, capturedBool, "SDL.SetWindowBordered must forward bordered.");
        }

        ResetCaptureState();
        nextBool = false;
        using (NativeHookScope _ = NativeHookScope.Install("SetWindowResizableNativeFunction", nameof(CaptureWindowBoolReturnBool)))
        {
            bool actual = SDL3.SDL.SetWindowResizable((IntPtr)0xB02, false);

            TestAssert.Equal(false, actual, "SDL.SetWindowResizable must return native failure value.");
            TestAssert.Equal((IntPtr)0xB02, capturedWindow, "SDL.SetWindowResizable must forward window.");
            TestAssert.Equal(false, capturedBool, "SDL.SetWindowResizable must forward resizable.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetWindowAlwaysOnTopNativeFunction", nameof(CaptureWindowBoolReturnBool)))
        {
            bool actual = SDL3.SDL.SetWindowAlwaysOnTop((IntPtr)0xB03, true);

            TestAssert.Equal(true, actual, "SDL.SetWindowAlwaysOnTop must return native success value.");
            TestAssert.Equal((IntPtr)0xB03, capturedWindow, "SDL.SetWindowAlwaysOnTop must forward window.");
            TestAssert.Equal(true, capturedBool, "SDL.SetWindowAlwaysOnTop must forward onTop.");
        }

        ResetCaptureState();
        nextBool = false;
        using (NativeHookScope _ = NativeHookScope.Install("SetWindowFillDocumentNativeFunction", nameof(CaptureWindowBoolReturnBool)))
        {
            bool actual = SDL3.SDL.SetWindowFillDocument((IntPtr)0xB04, false);

            TestAssert.Equal(false, actual, "SDL.SetWindowFillDocument must return native failure value.");
            TestAssert.Equal((IntPtr)0xB04, capturedWindow, "SDL.SetWindowFillDocument must forward window.");
            TestAssert.Equal(false, capturedBool, "SDL.SetWindowFillDocument must forward fill.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("ShowWindowNativeFunction", nameof(CaptureWindowReturnBool)))
        {
            bool actual = SDL3.SDL.ShowWindow((IntPtr)0xB05);

            TestAssert.Equal(true, actual, "SDL.ShowWindow must return native success value.");
            TestAssert.Equal((IntPtr)0xB05, capturedWindow, "SDL.ShowWindow must forward window.");
        }

        ResetCaptureState();
        nextBool = false;
        using (NativeHookScope _ = NativeHookScope.Install("HideWindowNativeFunction", nameof(CaptureWindowReturnBool)))
        {
            bool actual = SDL3.SDL.HideWindow((IntPtr)0xB06);

            TestAssert.Equal(false, actual, "SDL.HideWindow must return native failure value.");
            TestAssert.Equal((IntPtr)0xB06, capturedWindow, "SDL.HideWindow must forward window.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("RaiseWindowNativeFunction", nameof(CaptureWindowReturnBool)))
        {
            bool actual = SDL3.SDL.RaiseWindow((IntPtr)0xB07);

            TestAssert.Equal(true, actual, "SDL.RaiseWindow must return native success value.");
            TestAssert.Equal((IntPtr)0xB07, capturedWindow, "SDL.RaiseWindow must forward window.");
        }

        ResetCaptureState();
        nextBool = false;
        using (NativeHookScope _ = NativeHookScope.Install("MaximizeWindowNativeFunction", nameof(CaptureWindowReturnBool)))
        {
            bool actual = SDL3.SDL.MaximizeWindow((IntPtr)0xB08);

            TestAssert.Equal(false, actual, "SDL.MaximizeWindow must return native failure value.");
            TestAssert.Equal((IntPtr)0xB08, capturedWindow, "SDL.MaximizeWindow must forward window.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("MinimizeWindowNativeFunction", nameof(CaptureWindowReturnBool)))
        {
            bool actual = SDL3.SDL.MinimizeWindow((IntPtr)0xB09);

            TestAssert.Equal(true, actual, "SDL.MinimizeWindow must return native success value.");
            TestAssert.Equal((IntPtr)0xB09, capturedWindow, "SDL.MinimizeWindow must forward window.");
        }

        ResetCaptureState();
        nextBool = false;
        using (NativeHookScope _ = NativeHookScope.Install("RestoreWindowNativeFunction", nameof(CaptureWindowReturnBool)))
        {
            bool actual = SDL3.SDL.RestoreWindow((IntPtr)0xB0A);

            TestAssert.Equal(false, actual, "SDL.RestoreWindow must return native failure value.");
            TestAssert.Equal((IntPtr)0xB0A, capturedWindow, "SDL.RestoreWindow must forward window.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetWindowFullscreenNativeFunction", nameof(CaptureWindowBoolReturnBool)))
        {
            bool actual = SDL3.SDL.SetWindowFullscreen((IntPtr)0xB0B, true);

            TestAssert.Equal(true, actual, "SDL.SetWindowFullscreen must return native success value.");
            TestAssert.Equal((IntPtr)0xB0B, capturedWindow, "SDL.SetWindowFullscreen must forward window.");
            TestAssert.Equal(true, capturedBool, "SDL.SetWindowFullscreen must forward fullscreen.");
        }

        ResetCaptureState();
        nextBool = false;
        using (NativeHookScope _ = NativeHookScope.Install("SyncWindowNativeFunction", nameof(CaptureWindowReturnBool)))
        {
            bool actual = SDL3.SDL.SyncWindow((IntPtr)0xB0C);

            TestAssert.Equal(false, actual, "SDL.SyncWindow must return native timeout value.");
            TestAssert.Equal((IntPtr)0xB0C, capturedWindow, "SDL.SyncWindow must forward window.");
        }
    }

    public static void WindowSurfaceFunctions_ForwardInputsOutputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("WindowHasSurfaceNativeFunction", nameof(CaptureWindowReturnBool)))
        {
            bool actual = SDL3.SDL.WindowHasSurface((IntPtr)0xC01);

            TestAssert.Equal(true, actual, "SDL.WindowHasSurface must return native value.");
            TestAssert.Equal((IntPtr)0xC01, capturedWindow, "SDL.WindowHasSurface must forward window.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0xC02;
        using (NativeHookScope _ = NativeHookScope.Install("GetWindowSurfaceNativeFunction", nameof(CaptureWindowReturnPointer)))
        {
            IntPtr actual = SDL3.SDL.GetWindowSurface((IntPtr)0xC03);

            TestAssert.Equal((IntPtr)0xC02, actual, "SDL.GetWindowSurface must return native surface pointer.");
            TestAssert.Equal((IntPtr)0xC03, capturedWindow, "SDL.GetWindowSurface must forward window.");
        }

        ResetCaptureState();
        nextBool = false;
        using (NativeHookScope _ = NativeHookScope.Install("SetWindowSurfaceVSyncNativeFunction", nameof(CaptureSetWindowSurfaceVSync)))
        {
            bool actual = SDL3.SDL.SetWindowSurfaceVSync((IntPtr)0xC04, -1);

            TestAssert.Equal(false, actual, "SDL.SetWindowSurfaceVSync must return native failure value.");
            TestAssert.Equal((IntPtr)0xC04, capturedWindow, "SDL.SetWindowSurfaceVSync must forward window.");
            TestAssert.Equal(-1, capturedVSync, "SDL.SetWindowSurfaceVSync must forward vsync.");
        }

        ResetCaptureState();
        nextBool = true;
        nextVSync = 2;
        using (NativeHookScope _ = NativeHookScope.Install("GetWindowSurfaceVSyncNativeFunction", nameof(CaptureWindowReturnVSync)))
        {
            bool actual = SDL3.SDL.GetWindowSurfaceVSync((IntPtr)0xC05, out int vsync);

            TestAssert.Equal(true, actual, "SDL.GetWindowSurfaceVSync must return native success value.");
            TestAssert.Equal((IntPtr)0xC05, capturedWindow, "SDL.GetWindowSurfaceVSync must forward window.");
            TestAssert.Equal(2, vsync, "SDL.GetWindowSurfaceVSync must return native vsync.");
        }

        ResetCaptureState();
        nextBool = false;
        using (NativeHookScope _ = NativeHookScope.Install("UpdateWindowSurfaceNativeFunction", nameof(CaptureWindowReturnBool)))
        {
            bool actual = SDL3.SDL.UpdateWindowSurface((IntPtr)0xC06);

            TestAssert.Equal(false, actual, "SDL.UpdateWindowSurface must return native failure value.");
            TestAssert.Equal((IntPtr)0xC06, capturedWindow, "SDL.UpdateWindowSurface must forward window.");
        }

        ResetCaptureState();
        nextBool = true;
        SDL3.SDL.Rect[] rects =
        [
            new SDL3.SDL.Rect { X = 1, Y = 2, W = 3, H = 4 },
            new SDL3.SDL.Rect { X = 5, Y = 6, W = 7, H = 8 }
        ];
        using (NativeHookScope _ = NativeHookScope.Install("UpdateWindowSurfaceRectsNativeFunction", nameof(CaptureUpdateWindowSurfaceRects)))
        {
            bool actual = SDL3.SDL.UpdateWindowSurfaceRects((IntPtr)0xC07, rects, rects.Length);

            TestAssert.Equal(true, actual, "SDL.UpdateWindowSurfaceRects must return native success value.");
            TestAssert.Equal((IntPtr)0xC07, capturedWindow, "SDL.UpdateWindowSurfaceRects must forward window.");
            TestAssert.Equal(rects.Length, capturedNumRects, "SDL.UpdateWindowSurfaceRects must forward numrects.");
            TestAssert.NotNull(capturedRects, "SDL.UpdateWindowSurfaceRects must forward rect array.");
            TestAssert.Equal(rects.Length, capturedRects!.Length, "SDL.UpdateWindowSurfaceRects must preserve rect array length.");
            for (int i = 0; i < rects.Length; i++)
            {
                AssertRect(rects[i], capturedRects[i], $"SDL.UpdateWindowSurfaceRects must forward rect {i}.");
            }
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("UpdateWindowSurfaceRectsPointerNativeFunction", nameof(CaptureUpdateWindowSurfaceRectsPointer)))
        {
            bool actual = SDL3.SDL.UpdateWindowSurfaceRects((IntPtr)0xC09, rects.AsSpan(1), 1);

            TestAssert.Equal(true, actual, "SDL.UpdateWindowSurfaceRects(ReadOnlySpan<Rect>) must return native success value.");
            TestAssert.Equal((IntPtr)0xC09, capturedWindow, "SDL.UpdateWindowSurfaceRects(ReadOnlySpan<Rect>) must forward window.");
            TestAssert.Equal(1, capturedNumRects, "SDL.UpdateWindowSurfaceRects(ReadOnlySpan<Rect>) must forward numrects.");
            TestAssert.NotNull(capturedRects, "SDL.UpdateWindowSurfaceRects(ReadOnlySpan<Rect>) must forward rect span.");
            TestAssert.Equal(1, capturedRects!.Length, "SDL.UpdateWindowSurfaceRects(ReadOnlySpan<Rect>) must preserve span length.");
            AssertRect(rects[1], capturedRects[0], "SDL.UpdateWindowSurfaceRects(ReadOnlySpan<Rect>) must forward rect slice item 0.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("DestroyWindowSurfaceNativeFunction", nameof(CaptureWindowReturnBool)))
        {
            bool actual = SDL3.SDL.DestroyWindowSurface((IntPtr)0xC08);

            TestAssert.Equal(true, actual, "SDL.DestroyWindowSurface must return native success value.");
            TestAssert.Equal((IntPtr)0xC08, capturedWindow, "SDL.DestroyWindowSurface must forward window.");
        }
    }

    public static void WindowGrabMouseFocusAndHitTestFunctions_ForwardInputsOutputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetWindowKeyboardGrabNativeFunction", nameof(CaptureWindowBoolReturnBool)))
        {
            bool actual = SDL3.SDL.SetWindowKeyboardGrab((IntPtr)0xD01, true);

            TestAssert.Equal(true, actual, "SDL.SetWindowKeyboardGrab must return native success value.");
            TestAssert.Equal((IntPtr)0xD01, capturedWindow, "SDL.SetWindowKeyboardGrab must forward window.");
            TestAssert.Equal(true, capturedBool, "SDL.SetWindowKeyboardGrab must forward grabbed.");
        }

        ResetCaptureState();
        nextBool = false;
        using (NativeHookScope _ = NativeHookScope.Install("SetWindowMouseGrabNativeFunction", nameof(CaptureWindowBoolReturnBool)))
        {
            bool actual = SDL3.SDL.SetWindowMouseGrab((IntPtr)0xD02, false);

            TestAssert.Equal(false, actual, "SDL.SetWindowMouseGrab must return native failure value.");
            TestAssert.Equal((IntPtr)0xD02, capturedWindow, "SDL.SetWindowMouseGrab must forward window.");
            TestAssert.Equal(false, capturedBool, "SDL.SetWindowMouseGrab must forward grabbed.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("GetWindowKeyboardGrabNativeFunction", nameof(CaptureWindowReturnBool)))
        {
            bool actual = SDL3.SDL.GetWindowKeyboardGrab((IntPtr)0xD03);

            TestAssert.Equal(true, actual, "SDL.GetWindowKeyboardGrab must return native value.");
            TestAssert.Equal((IntPtr)0xD03, capturedWindow, "SDL.GetWindowKeyboardGrab must forward window.");
        }

        ResetCaptureState();
        nextBool = false;
        using (NativeHookScope _ = NativeHookScope.Install("GetWindowMouseGrabNativeFunction", nameof(CaptureWindowReturnBool)))
        {
            bool actual = SDL3.SDL.GetWindowMouseGrab((IntPtr)0xD04);

            TestAssert.Equal(false, actual, "SDL.GetWindowMouseGrab must return native value.");
            TestAssert.Equal((IntPtr)0xD04, capturedWindow, "SDL.GetWindowMouseGrab must forward window.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0xD05;
        using (NativeHookScope _ = NativeHookScope.Install("GetGrabbedWindowNativeFunction", nameof(ReturnNextPointer)))
        {
            TestAssert.Equal((IntPtr)0xD05, SDL3.SDL.GetGrabbedWindow(), "SDL.GetGrabbedWindow must return native window pointer.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetWindowMouseRectPointerNativeFunction", nameof(CaptureSetWindowMouseRectPointer)))
        {
            bool actual = SDL3.SDL.SetWindowMouseRect((IntPtr)0xD06, (IntPtr)0xD07);

            TestAssert.Equal(true, actual, "SDL.SetWindowMouseRect(IntPtr) must return native success value.");
            TestAssert.Equal((IntPtr)0xD06, capturedWindow, "SDL.SetWindowMouseRect(IntPtr) must forward window.");
            TestAssert.Equal((IntPtr)0xD07, capturedRectPointer, "SDL.SetWindowMouseRect(IntPtr) must forward rect pointer.");
        }

        ResetCaptureState();
        nextBool = false;
        SDL3.SDL.Rect rect = new() { X = 9, Y = 10, W = 11, H = 12 };
        using (NativeHookScope _ = NativeHookScope.Install("SetWindowMouseRectRectNativeFunction", nameof(CaptureSetWindowMouseRectRect)))
        {
            bool actual = SDL3.SDL.SetWindowMouseRect((IntPtr)0xD08, rect);

            TestAssert.Equal(false, actual, "SDL.SetWindowMouseRect(Rect) must return native failure value.");
            TestAssert.Equal((IntPtr)0xD08, capturedWindow, "SDL.SetWindowMouseRect(Rect) must forward window.");
            AssertRect(rect, capturedRect, "SDL.SetWindowMouseRect(Rect) must forward rect.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0xD09;
        using (NativeHookScope _ = NativeHookScope.Install("GetWindowMouseRectNativeFunction", nameof(CaptureWindowReturnPointer)))
        {
            IntPtr actual = SDL3.SDL.GetWindowMouseRect((IntPtr)0xD0A);

            TestAssert.Equal((IntPtr)0xD09, actual, "SDL.GetWindowMouseRect must return native rect pointer.");
            TestAssert.Equal((IntPtr)0xD0A, capturedWindow, "SDL.GetWindowMouseRect must forward window.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetWindowOpacityNativeFunction", nameof(CaptureSetWindowOpacity)))
        {
            bool actual = SDL3.SDL.SetWindowOpacity((IntPtr)0xD0B, 0.75f);

            TestAssert.Equal(true, actual, "SDL.SetWindowOpacity must return native success value.");
            TestAssert.Equal((IntPtr)0xD0B, capturedWindow, "SDL.SetWindowOpacity must forward window.");
            TestAssert.Equal(0.75f, capturedOpacity, "SDL.SetWindowOpacity must forward opacity.");
        }

        ResetCaptureState();
        nextFloat = 0.5f;
        using (NativeHookScope _ = NativeHookScope.Install("GetWindowOpacityNativeFunction", nameof(CaptureWindowReturnFloat)))
        {
            float actual = SDL3.SDL.GetWindowOpacity((IntPtr)0xD0C);

            TestAssert.Equal(0.5f, actual, "SDL.GetWindowOpacity must return native opacity.");
            TestAssert.Equal((IntPtr)0xD0C, capturedWindow, "SDL.GetWindowOpacity must forward window.");
        }

        ResetCaptureState();
        nextBool = false;
        using (NativeHookScope _ = NativeHookScope.Install("SetWindowParentNativeFunction", nameof(CaptureSetWindowParent)))
        {
            bool actual = SDL3.SDL.SetWindowParent((IntPtr)0xD0D, (IntPtr)0xD0E);

            TestAssert.Equal(false, actual, "SDL.SetWindowParent must return native failure value.");
            TestAssert.Equal((IntPtr)0xD0D, capturedWindow, "SDL.SetWindowParent must forward window.");
            TestAssert.Equal((IntPtr)0xD0E, capturedParent, "SDL.SetWindowParent must forward parent.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetWindowModalNativeFunction", nameof(CaptureWindowBoolReturnBool)))
        {
            bool actual = SDL3.SDL.SetWindowModal((IntPtr)0xD0F, true);

            TestAssert.Equal(true, actual, "SDL.SetWindowModal must return native success value.");
            TestAssert.Equal((IntPtr)0xD0F, capturedWindow, "SDL.SetWindowModal must forward window.");
            TestAssert.Equal(true, capturedBool, "SDL.SetWindowModal must forward modal.");
        }

        ResetCaptureState();
        nextBool = false;
        using (NativeHookScope _ = NativeHookScope.Install("SetWindowFocusableNativeFunction", nameof(CaptureWindowBoolReturnBool)))
        {
            bool actual = SDL3.SDL.SetWindowFocusable((IntPtr)0xD10, false);

            TestAssert.Equal(false, actual, "SDL.SetWindowFocusable must return native failure value.");
            TestAssert.Equal((IntPtr)0xD10, capturedWindow, "SDL.SetWindowFocusable must forward window.");
            TestAssert.Equal(false, capturedBool, "SDL.SetWindowFocusable must forward focusable.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("ShowWindowSystemMenuNativeFunction", nameof(CaptureShowWindowSystemMenu)))
        {
            bool actual = SDL3.SDL.ShowWindowSystemMenu((IntPtr)0xD11, 13, 14);

            TestAssert.Equal(true, actual, "SDL.ShowWindowSystemMenu must return native success value.");
            TestAssert.Equal((IntPtr)0xD11, capturedWindow, "SDL.ShowWindowSystemMenu must forward window.");
            TestAssert.Equal(13, capturedX, "SDL.ShowWindowSystemMenu must forward x.");
            TestAssert.Equal(14, capturedY, "SDL.ShowWindowSystemMenu must forward y.");
        }

        ResetCaptureState();
        nextBool = false;
        SDL3.SDL.HitTest callback = ReturnHitTestNormal;
        using (NativeHookScope _ = NativeHookScope.Install("SetWindowHitTestNativeFunction", nameof(CaptureSetWindowHitTest)))
        {
            bool actual = SDL3.SDL.SetWindowHitTest((IntPtr)0xD12, callback, (IntPtr)0xD13);

            TestAssert.Equal(false, actual, "SDL.SetWindowHitTest must return native failure value.");
            TestAssert.Equal((IntPtr)0xD12, capturedWindow, "SDL.SetWindowHitTest must forward window.");
            TestAssert.True(ReferenceEquals(callback, capturedHitTest), "SDL.SetWindowHitTest must forward callback.");
            TestAssert.Equal((IntPtr)0xD13, capturedCallbackData, "SDL.SetWindowHitTest must forward callback data.");
        }
    }

    public static void WindowShapeProgressAndDestroyFunctions_ForwardInputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetWindowShapeNativeFunction", nameof(CaptureSetWindowShape)))
        {
            bool actual = SDL3.SDL.SetWindowShape((IntPtr)0xE01, (IntPtr)0xE02);

            TestAssert.Equal(true, actual, "SDL.SetWindowShape must return native success value.");
            TestAssert.Equal((IntPtr)0xE01, capturedWindow, "SDL.SetWindowShape must forward window.");
            TestAssert.Equal((IntPtr)0xE02, capturedShape, "SDL.SetWindowShape must forward shape.");
        }

        ResetCaptureState();
        nextBool = false;
        using (NativeHookScope _ = NativeHookScope.Install("FlashWindowNativeFunction", nameof(CaptureFlashWindow)))
        {
            bool actual = SDL3.SDL.FlashWindow((IntPtr)0xE03, SDL3.SDL.FlashOperation.UntilFocused);

            TestAssert.Equal(false, actual, "SDL.FlashWindow must return native failure value.");
            TestAssert.Equal((IntPtr)0xE03, capturedWindow, "SDL.FlashWindow must forward window.");
            TestAssert.Equal(SDL3.SDL.FlashOperation.UntilFocused, capturedFlashOperation, "SDL.FlashWindow must forward operation.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("SetWindowProgressStateNativeFunction", nameof(CaptureSetWindowProgressState)))
        {
            bool actual = SDL3.SDL.SetWindowProgressState((IntPtr)0xE04, SDL3.SDL.ProgressState.Paused);

            TestAssert.Equal(true, actual, "SDL.SetWindowProgressState must return native success value.");
            TestAssert.Equal((IntPtr)0xE04, capturedWindow, "SDL.SetWindowProgressState must forward window.");
            TestAssert.Equal(SDL3.SDL.ProgressState.Paused, capturedProgressState, "SDL.SetWindowProgressState must forward state.");
        }

        ResetCaptureState();
        nextProgressState = SDL3.SDL.ProgressState.Error;
        using (NativeHookScope _ = NativeHookScope.Install("GetWindowProgressStateNativeFunction", nameof(CaptureWindowReturnProgressState)))
        {
            SDL3.SDL.ProgressState actual = SDL3.SDL.GetWindowProgressState((IntPtr)0xE05);

            TestAssert.Equal(SDL3.SDL.ProgressState.Error, actual, "SDL.GetWindowProgressState must return native state.");
            TestAssert.Equal((IntPtr)0xE05, capturedWindow, "SDL.GetWindowProgressState must forward window.");
        }

        ResetCaptureState();
        nextBool = false;
        using (NativeHookScope _ = NativeHookScope.Install("SetWindowProgressValueNativeFunction", nameof(CaptureSetWindowProgressValue)))
        {
            bool actual = SDL3.SDL.SetWindowProgressValue((IntPtr)0xE06, 0.25f);

            TestAssert.Equal(false, actual, "SDL.SetWindowProgressValue must return native failure value.");
            TestAssert.Equal((IntPtr)0xE06, capturedWindow, "SDL.SetWindowProgressValue must forward window.");
            TestAssert.Equal(0.25f, capturedProgressValue, "SDL.SetWindowProgressValue must forward value.");
        }

        ResetCaptureState();
        nextFloat = 0.9f;
        using (NativeHookScope _ = NativeHookScope.Install("GetWindowProgressValueNativeFunction", nameof(CaptureWindowReturnFloat)))
        {
            float actual = SDL3.SDL.GetWindowProgressValue((IntPtr)0xE07);

            TestAssert.Equal(0.9f, actual, "SDL.GetWindowProgressValue must return native value.");
            TestAssert.Equal((IntPtr)0xE07, capturedWindow, "SDL.GetWindowProgressValue must forward window.");
        }

        ResetCaptureState();
        using (NativeHookScope _ = NativeHookScope.Install("DestroyWindowNativeFunction", nameof(CaptureDestroyWindow)))
        {
            SDL3.SDL.DestroyWindow((IntPtr)0xE08);

            TestAssert.Equal((IntPtr)0xE08, capturedWindow, "SDL.DestroyWindow must forward window.");
        }
    }

    public static void ScreenSaverFunctions_ReturnNativeValues()
    {
        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("ScreenSaverEnabledNativeFunction", nameof(ReturnNextBool)))
        {
            TestAssert.Equal(true, SDL3.SDL.ScreenSaverEnabled(), "SDL.ScreenSaverEnabled must return native enabled value.");
        }

        ResetCaptureState();
        nextBool = false;
        using (NativeHookScope _ = NativeHookScope.Install("EnableScreenSaverNativeFunction", nameof(ReturnNextBool)))
        {
            TestAssert.Equal(false, SDL3.SDL.EnableScreenSaver(), "SDL.EnableScreenSaver must return native failure value.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("DisableScreenSaverNativeFunction", nameof(ReturnNextBool)))
        {
            TestAssert.Equal(true, SDL3.SDL.DisableScreenSaver(), "SDL.DisableScreenSaver must return native success value.");
        }
    }

    public static void OpenGLFunctions_ForwardInputsOutputsAndReturnNativeValues()
    {
        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("GLLoadLibraryNativeFunction", nameof(CaptureGLLoadLibrary)))
        {
            bool actual = SDL3.SDL.GLLoadLibrary("opengl32.dll");

            TestAssert.Equal(true, actual, "SDL.GLLoadLibrary must return native success value.");
            TestAssert.Equal("opengl32.dll", capturedPath, "SDL.GLLoadLibrary must forward path.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0xF01;
        using (NativeHookScope _ = NativeHookScope.Install("GLGetProcAddressNativeFunction", nameof(CaptureProcReturnPointer)))
        {
            IntPtr actual = SDL3.SDL.GLGetProcAddress("glBindTexture");

            TestAssert.Equal((IntPtr)0xF01, actual, "SDL.GLGetProcAddress must return native function pointer.");
            TestAssert.Equal("glBindTexture", capturedProc, "SDL.GLGetProcAddress must forward proc name.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0xF02;
        using (NativeHookScope _ = NativeHookScope.Install("EGLGetProcAddressNativeFunction", nameof(CaptureProcReturnPointer)))
        {
            IntPtr actual = SDL3.SDL.EGLGetProcAddress("eglCreateContext");

            TestAssert.Equal((IntPtr)0xF02, actual, "SDL.EGLGetProcAddress must return native function pointer.");
            TestAssert.Equal("eglCreateContext", capturedProc, "SDL.EGLGetProcAddress must forward proc name.");
        }

        ResetCaptureState();
        using (NativeHookScope _ = NativeHookScope.Install("GLUnloadLibraryNativeFunction", nameof(CaptureNoArgVoid)))
        {
            SDL3.SDL.GLUnloadLibrary();

            TestAssert.Equal(1, capturedNoArgCallCount, "SDL.GLUnloadLibrary must call native function exactly once.");
        }

        ResetCaptureState();
        nextBool = false;
        using (NativeHookScope _ = NativeHookScope.Install("GLExtensionSupportedNativeFunction", nameof(CaptureGLExtensionSupported)))
        {
            bool actual = SDL3.SDL.GLExtensionSupported("GL_EXT_texture_filter_anisotropic");

            TestAssert.Equal(false, actual, "SDL.GLExtensionSupported must return native support value.");
            TestAssert.Equal("GL_EXT_texture_filter_anisotropic", capturedExtension, "SDL.GLExtensionSupported must forward extension.");
        }

        ResetCaptureState();
        using (NativeHookScope _ = NativeHookScope.Install("GLResetAttributesNativeFunction", nameof(CaptureNoArgVoid)))
        {
            SDL3.SDL.GLResetAttributes();

            TestAssert.Equal(1, capturedNoArgCallCount, "SDL.GLResetAttributes must call native function exactly once.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("GLSetAttributeNativeFunction", nameof(CaptureGLSetAttribute)))
        {
            bool actual = SDL3.SDL.GLSetAttribute(SDL3.SDL.GLAttr.ContextMajorVersion, 4);

            TestAssert.Equal(true, actual, "SDL.GLSetAttribute must return native success value.");
            TestAssert.Equal(SDL3.SDL.GLAttr.ContextMajorVersion, capturedGLAttr, "SDL.GLSetAttribute must forward attr.");
            TestAssert.Equal(4, capturedValue, "SDL.GLSetAttribute must forward value.");
        }

        ResetCaptureState();
        nextBool = true;
        nextInt = 8;
        using (NativeHookScope _ = NativeHookScope.Install("GLGetAttributeNativeFunction", nameof(CaptureGLGetAttribute)))
        {
            bool actual = SDL3.SDL.GLGetAttribute(SDL3.SDL.GLAttr.DepthSize, out int value);

            TestAssert.Equal(true, actual, "SDL.GLGetAttribute must return native success value.");
            TestAssert.Equal(SDL3.SDL.GLAttr.DepthSize, capturedGLAttr, "SDL.GLGetAttribute must forward attr.");
            TestAssert.Equal(8, value, "SDL.GLGetAttribute must return native output value.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0xF03;
        using (NativeHookScope _ = NativeHookScope.Install("GLCreateContextNativeFunction", nameof(CaptureWindowReturnPointer)))
        {
            IntPtr actual = SDL3.SDL.GLCreateContext((IntPtr)0xF04);

            TestAssert.Equal((IntPtr)0xF03, actual, "SDL.GLCreateContext must return native context.");
            TestAssert.Equal((IntPtr)0xF04, capturedWindow, "SDL.GLCreateContext must forward window.");
        }

        ResetCaptureState();
        nextBool = false;
        using (NativeHookScope _ = NativeHookScope.Install("GLMakeCurrentNativeFunction", nameof(CaptureGLMakeCurrent)))
        {
            bool actual = SDL3.SDL.GLMakeCurrent((IntPtr)0xF05, (IntPtr)0xF06);

            TestAssert.Equal(false, actual, "SDL.GLMakeCurrent must return native failure value.");
            TestAssert.Equal((IntPtr)0xF05, capturedWindow, "SDL.GLMakeCurrent must forward window.");
            TestAssert.Equal((IntPtr)0xF06, capturedContext, "SDL.GLMakeCurrent must forward context.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0xF07;
        using (NativeHookScope _ = NativeHookScope.Install("GLGetCurrentWindowNativeFunction", nameof(ReturnNextPointer)))
        {
            TestAssert.Equal((IntPtr)0xF07, SDL3.SDL.GLGetCurrentWindow(), "SDL.GLGetCurrentWindow must return native window.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0xF08;
        using (NativeHookScope _ = NativeHookScope.Install("GLGetCurrentContextNativeFunction", nameof(ReturnNextPointer)))
        {
            TestAssert.Equal((IntPtr)0xF08, SDL3.SDL.GLGetCurrentContext(), "SDL.GLGetCurrentContext must return native context.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0xF09;
        using (NativeHookScope _ = NativeHookScope.Install("EGLGetCurrentDisplayNativeFunction", nameof(ReturnNextPointer)))
        {
            TestAssert.Equal((IntPtr)0xF09, SDL3.SDL.EGLGetCurrentDisplay(), "SDL.EGLGetCurrentDisplay must return native display.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0xF0A;
        using (NativeHookScope _ = NativeHookScope.Install("EGLGetCurrentConfigNativeFunction", nameof(ReturnNextPointer)))
        {
            TestAssert.Equal((IntPtr)0xF0A, SDL3.SDL.EGLGetCurrentConfig(), "SDL.EGLGetCurrentConfig must return native config.");
        }

        ResetCaptureState();
        nextPointer = (IntPtr)0xF0B;
        using (NativeHookScope _ = NativeHookScope.Install("EGLGetWindowSurfaceNativeFunction", nameof(CaptureWindowReturnPointer)))
        {
            IntPtr actual = SDL3.SDL.EGLGetWindowSurface((IntPtr)0xF0C);

            TestAssert.Equal((IntPtr)0xF0B, actual, "SDL.EGLGetWindowSurface must return native surface.");
            TestAssert.Equal((IntPtr)0xF0C, capturedWindow, "SDL.EGLGetWindowSurface must forward window.");
        }

        ResetCaptureState();
        SDL3.SDL.EGLAttribArrayCallback platformCallback = ReturnEGLAttribArray;
        SDL3.SDL.EGLIntArrayCallback surfaceCallback = ReturnEGLIntArray;
        SDL3.SDL.EGLIntArrayCallback contextCallback = ReturnEGLIntArray;
        using (NativeHookScope _ = NativeHookScope.Install("EGLSetAttributeCallbacksNativeFunction", nameof(CaptureEGLSetAttributeCallbacks)))
        {
            SDL3.SDL.EGLSetAttributeCallbacks(platformCallback, surfaceCallback, contextCallback, (IntPtr)0xF0D);

            TestAssert.True(ReferenceEquals(platformCallback, capturedPlatformAttribCallback), "SDL.EGLSetAttributeCallbacks must forward platform callback.");
            TestAssert.True(ReferenceEquals(surfaceCallback, capturedSurfaceAttribCallback), "SDL.EGLSetAttributeCallbacks must forward surface callback.");
            TestAssert.True(ReferenceEquals(contextCallback, capturedContextAttribCallback), "SDL.EGLSetAttributeCallbacks must forward context callback.");
            TestAssert.Equal((IntPtr)0xF0D, capturedUserData, "SDL.EGLSetAttributeCallbacks must forward userdata.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("GLSetSwapIntervalNativeFunction", nameof(CaptureGLSetSwapInterval)))
        {
            bool actual = SDL3.SDL.GLSetSwapInterval(-1);

            TestAssert.Equal(true, actual, "SDL.GLSetSwapInterval must return native success value.");
            TestAssert.Equal(-1, capturedInterval, "SDL.GLSetSwapInterval must forward interval.");
        }

        ResetCaptureState();
        nextBool = false;
        nextInt = 1;
        using (NativeHookScope _ = NativeHookScope.Install("GLGetSwapIntervalNativeFunction", nameof(CaptureGLGetSwapInterval)))
        {
            bool actual = SDL3.SDL.GLGetSwapInterval(out int interval);

            TestAssert.Equal(false, actual, "SDL.GLGetSwapInterval must return native failure value.");
            TestAssert.Equal(1, interval, "SDL.GLGetSwapInterval must return native output interval.");
        }

        ResetCaptureState();
        nextBool = true;
        using (NativeHookScope _ = NativeHookScope.Install("GLSwapWindowNativeFunction", nameof(CaptureWindowReturnBool)))
        {
            bool actual = SDL3.SDL.GLSwapWindow((IntPtr)0xF0E);

            TestAssert.Equal(true, actual, "SDL.GLSwapWindow must return native success value.");
            TestAssert.Equal((IntPtr)0xF0E, capturedWindow, "SDL.GLSwapWindow must forward window.");
        }

        ResetCaptureState();
        nextBool = false;
        using (NativeHookScope _ = NativeHookScope.Install("GLDestroyContextNativeFunction", nameof(CaptureWindowReturnBool)))
        {
            bool actual = SDL3.SDL.GLDestroyContext((IntPtr)0xF0F);

            TestAssert.Equal(false, actual, "SDL.GLDestroyContext must return native failure value.");
            TestAssert.Equal((IntPtr)0xF0F, capturedWindow, "SDL.GLDestroyContext must forward context.");
        }
    }

    private static int ReturnNextInt()
    {
        return nextInt;
    }

    private static bool ReturnNextBool()
    {
        return nextBool;
    }

    private static bool CaptureGLLoadLibrary(string? path)
    {
        capturedPath = path;
        return nextBool;
    }

    private static IntPtr CaptureProcReturnPointer(string proc)
    {
        capturedProc = proc;
        return nextPointer;
    }

    private static void CaptureNoArgVoid()
    {
        capturedNoArgCallCount++;
    }

    private static bool CaptureGLExtensionSupported(string extension)
    {
        capturedExtension = extension;
        return nextBool;
    }

    private static bool CaptureGLSetAttribute(SDL3.SDL.GLAttr attr, int value)
    {
        capturedGLAttr = attr;
        capturedValue = value;
        return nextBool;
    }

    private static bool CaptureGLGetAttribute(SDL3.SDL.GLAttr attr, out int value)
    {
        capturedGLAttr = attr;
        value = nextInt;
        return nextBool;
    }

    private static bool CaptureGLMakeCurrent(IntPtr window, IntPtr context)
    {
        capturedWindow = window;
        capturedContext = context;
        return nextBool;
    }

    private static void CaptureEGLSetAttributeCallbacks(SDL3.SDL.EGLAttribArrayCallback? platformAttribCallback,
        SDL3.SDL.EGLIntArrayCallback? surfaceAttribCallback, SDL3.SDL.EGLIntArrayCallback? contextAttribCallback,
        IntPtr userdata)
    {
        capturedPlatformAttribCallback = platformAttribCallback;
        capturedSurfaceAttribCallback = surfaceAttribCallback;
        capturedContextAttribCallback = contextAttribCallback;
        capturedUserData = userdata;
    }

    private static bool CaptureGLSetSwapInterval(int interval)
    {
        capturedInterval = interval;
        return nextBool;
    }

    private static bool CaptureGLGetSwapInterval(out int interval)
    {
        interval = nextInt;
        return nextBool;
    }

    private static IntPtr ReturnEGLAttribArray(IntPtr userdata)
    {
        return nextPointer;
    }

    private static IntPtr ReturnEGLIntArray(IntPtr userdata, IntPtr display, IntPtr config)
    {
        return nextPointer;
    }

    private static IntPtr CaptureGetVideoDriver(int index)
    {
        capturedIndex = index;
        return nextPointer;
    }

    private static IntPtr ReturnNextPointer()
    {
        return nextPointer;
    }

    private static IntPtr ReturnDisplayArray(out int count)
    {
        count = nextCount;
        return nextPointer;
    }

    private static IntPtr ReturnWindowPointerArray(out int count)
    {
        count = nextCount;
        return nextPointer;
    }

    private static IntPtr CaptureFullscreenDisplayModes(uint displayID, out int count)
    {
        capturedDisplayID = displayID;
        count = nextCount;
        return nextPointer;
    }

    private static uint ReturnNextUInt()
    {
        return nextUInt;
    }

    private static uint CaptureDisplayIdReturnUInt(uint displayID)
    {
        capturedDisplayID = displayID;
        return nextUInt;
    }

    private static uint CapturePointReturnUInt(SDL3.SDL.Point point)
    {
        capturedPoint = point;
        return nextUInt;
    }

    private static uint CaptureRectReturnUInt(SDL3.SDL.Rect rect)
    {
        capturedRect = rect;
        return nextUInt;
    }

    private static uint CaptureWindowReturnUInt(IntPtr window)
    {
        capturedWindow = window;
        return nextUInt;
    }

    private static float CaptureWindowReturnFloat(IntPtr window)
    {
        capturedWindow = window;
        return nextFloat;
    }

    private static bool CaptureSetWindowFullscreenModePointer(IntPtr window, IntPtr mode)
    {
        capturedWindow = window;
        capturedModePointer = mode;
        return nextBool;
    }

    private static bool CaptureSetWindowFullscreenModeMode(IntPtr window, in SDL3.SDL.DisplayMode mode)
    {
        capturedWindow = window;
        capturedDisplayMode = mode;
        return nextBool;
    }

    private static IntPtr CaptureWindowReturnPointer(IntPtr window)
    {
        capturedWindow = window;
        return nextPointer;
    }

    private static SDL3.SDL.WindowFlags CaptureWindowReturnFlags(IntPtr window)
    {
        capturedWindow = window;
        return nextWindowFlags;
    }

    private static IntPtr CaptureWindowReturnPointerAndSize(IntPtr window, out UIntPtr size)
    {
        capturedWindow = window;
        size = nextSize;
        return nextPointer;
    }

    private static SDL3.SDL.PixelFormat CaptureWindowReturnPixelFormat(IntPtr window)
    {
        capturedWindow = window;
        return nextPixelFormat;
    }

    private static IntPtr CaptureCreateWindow(string title, int w, int h, SDL3.SDL.WindowFlags flags)
    {
        capturedTitle = title;
        capturedWidth = w;
        capturedHeight = h;
        capturedWindowFlags = flags;
        return nextPointer;
    }

    private static IntPtr CaptureCreatePopupWindow(IntPtr parent, int offsetX, int offsetY, int w, int h, SDL3.SDL.WindowFlags flags)
    {
        capturedWindow = parent;
        capturedOffsetX = offsetX;
        capturedOffsetY = offsetY;
        capturedWidth = w;
        capturedHeight = h;
        capturedWindowFlags = flags;
        return nextPointer;
    }

    private static IntPtr CapturePropertiesReturnPointer(uint props)
    {
        capturedProperties = props;
        return nextPointer;
    }

    private static IntPtr CaptureWindowIdReturnPointer(uint id)
    {
        capturedWindowID = id;
        return nextPointer;
    }

    private static bool CaptureSetWindowTitle(IntPtr window, string title)
    {
        capturedWindow = window;
        capturedTitle = title;
        return nextBool;
    }

    private static bool CaptureSetWindowIcon(IntPtr window, IntPtr icon)
    {
        capturedWindow = window;
        capturedIcon = icon;
        return nextBool;
    }

    private static bool CaptureSetWindowPosition(IntPtr window, int x, int y)
    {
        capturedWindow = window;
        capturedX = x;
        capturedY = y;
        return nextBool;
    }

    private static bool CaptureSetWindowSize(IntPtr window, int w, int h)
    {
        capturedWindow = window;
        capturedWidth = w;
        capturedHeight = h;
        return nextBool;
    }

    private static bool CaptureWindowReturnXY(IntPtr window, out int x, out int y)
    {
        capturedWindow = window;
        x = nextX;
        y = nextY;
        return nextBool;
    }

    private static bool CaptureWindowReturnSize(IntPtr window, out int w, out int h)
    {
        capturedWindow = window;
        w = nextWidth;
        h = nextHeight;
        return nextBool;
    }

    private static bool CaptureWindowReturnRect(IntPtr window, out SDL3.SDL.Rect rect)
    {
        capturedWindow = window;
        rect = nextRect;
        return nextBool;
    }

    private static bool CaptureSetWindowAspectRatio(IntPtr window, float minAspect, float maxAspect)
    {
        capturedWindow = window;
        capturedMinAspect = minAspect;
        capturedMaxAspect = maxAspect;
        return nextBool;
    }

    private static bool CaptureWindowReturnAspectRatio(IntPtr window, out float minAspect, out float maxAspect)
    {
        capturedWindow = window;
        minAspect = nextMinAspect;
        maxAspect = nextMaxAspect;
        return nextBool;
    }

    private static bool CaptureWindowReturnBorders(IntPtr window, out int top, out int left, out int bottom, out int right)
    {
        capturedWindow = window;
        top = nextTop;
        left = nextLeft;
        bottom = nextBottom;
        right = nextRight;
        return nextBool;
    }

    private static bool CaptureWindowBoolReturnBool(IntPtr window, bool value)
    {
        capturedWindow = window;
        capturedBool = value;
        return nextBool;
    }

    private static bool CaptureWindowReturnBool(IntPtr window)
    {
        capturedWindow = window;
        return nextBool;
    }

    private static bool CaptureSetWindowSurfaceVSync(IntPtr window, int vsync)
    {
        capturedWindow = window;
        capturedVSync = vsync;
        return nextBool;
    }

    private static bool CaptureWindowReturnVSync(IntPtr window, out int vsync)
    {
        capturedWindow = window;
        vsync = nextVSync;
        return nextBool;
    }

    private static bool CaptureUpdateWindowSurfaceRects(IntPtr window, SDL3.SDL.Rect[] rects, int numrects)
    {
        capturedWindow = window;
        capturedRects = rects;
        capturedNumRects = numrects;
        return nextBool;
    }

    private static bool CaptureUpdateWindowSurfaceRectsPointer(IntPtr window, IntPtr rects, int numrects)
    {
        capturedWindow = window;
        capturedRects = CopyUnmanaged<SDL3.SDL.Rect>(rects, numrects);
        capturedNumRects = numrects;
        return nextBool;
    }

    private static bool CaptureSetWindowMouseRectPointer(IntPtr window, IntPtr rect)
    {
        capturedWindow = window;
        capturedRectPointer = rect;
        return nextBool;
    }

    private static bool CaptureSetWindowMouseRectRect(IntPtr window, in SDL3.SDL.Rect rect)
    {
        capturedWindow = window;
        capturedRect = rect;
        return nextBool;
    }

    private static bool CaptureSetWindowOpacity(IntPtr window, float opacity)
    {
        capturedWindow = window;
        capturedOpacity = opacity;
        return nextBool;
    }

    private static bool CaptureSetWindowParent(IntPtr window, IntPtr parent)
    {
        capturedWindow = window;
        capturedParent = parent;
        return nextBool;
    }

    private static bool CaptureShowWindowSystemMenu(IntPtr window, int x, int y)
    {
        capturedWindow = window;
        capturedX = x;
        capturedY = y;
        return nextBool;
    }

    private static bool CaptureSetWindowHitTest(IntPtr window, SDL3.SDL.HitTest? callback, IntPtr callbackData)
    {
        capturedWindow = window;
        capturedHitTest = callback;
        capturedCallbackData = callbackData;
        return nextBool;
    }

    private static bool CaptureSetWindowShape(IntPtr window, IntPtr shape)
    {
        capturedWindow = window;
        capturedShape = shape;
        return nextBool;
    }

    private static bool CaptureFlashWindow(IntPtr window, SDL3.SDL.FlashOperation operation)
    {
        capturedWindow = window;
        capturedFlashOperation = operation;
        return nextBool;
    }

    private static bool CaptureSetWindowProgressState(IntPtr window, SDL3.SDL.ProgressState state)
    {
        capturedWindow = window;
        capturedProgressState = state;
        return nextBool;
    }

    private static SDL3.SDL.ProgressState CaptureWindowReturnProgressState(IntPtr window)
    {
        capturedWindow = window;
        return nextProgressState;
    }

    private static bool CaptureSetWindowProgressValue(IntPtr window, float value)
    {
        capturedWindow = window;
        capturedProgressValue = value;
        return nextBool;
    }

    private static void CaptureDestroyWindow(IntPtr window)
    {
        capturedWindow = window;
    }

    private static SDL3.SDL.HitTestResult ReturnHitTestNormal(IntPtr win, in SDL3.SDL.Point area, IntPtr data)
    {
        return SDL3.SDL.HitTestResult.Normal;
    }

    private static IntPtr CaptureDisplayIdReturnPointer(uint displayID)
    {
        capturedDisplayID = displayID;
        return nextPointer;
    }

    private static SDL3.SDL.SystemTheme ReturnNextSystemTheme()
    {
        return nextSystemTheme;
    }

    private static bool CaptureDisplayIdReturnRect(uint displayID, out SDL3.SDL.Rect rect)
    {
        capturedDisplayID = displayID;
        rect = nextRect;
        return nextBool;
    }

    private static SDL3.SDL.DisplayOrientation CaptureDisplayIdReturnOrientation(uint displayID)
    {
        capturedDisplayID = displayID;
        return nextDisplayOrientation;
    }

    private static float CaptureDisplayIdReturnFloat(uint displayID)
    {
        capturedDisplayID = displayID;
        return nextFloat;
    }

    private static bool CaptureClosestFullscreenDisplayMode(uint displayID, int w, int h, float refreshRate,
        bool includeHighDensityModes, out SDL3.SDL.DisplayMode closest)
    {
        capturedDisplayID = displayID;
        capturedWidth = w;
        capturedHeight = h;
        capturedRefreshRate = refreshRate;
        capturedIncludeHighDensityModes = includeHighDensityModes;
        closest = nextDisplayMode;
        return nextBool;
    }

    private static void CaptureFree(IntPtr mem)
    {
        capturedFreePointer = mem;
        capturedFreeCallCount++;
    }

    private static IntPtr AllocateUIntArray(uint[] values)
    {
        IntPtr pointer = Marshal.AllocHGlobal(sizeof(uint) * values.Length);
        for (int i = 0; i < values.Length; i++)
        {
            Marshal.WriteInt32(pointer, i * sizeof(uint), unchecked((int)values[i]));
        }

        return pointer;
    }

    private static IntPtr[] AllocateDisplayModes(SDL3.SDL.DisplayMode[] values)
    {
        IntPtr[] pointers = new IntPtr[values.Length];
        for (int i = 0; i < values.Length; i++)
        {
            pointers[i] = AllocateDisplayMode(values[i]);
        }

        return pointers;
    }

    private static IntPtr AllocateDisplayMode(SDL3.SDL.DisplayMode value)
    {
        IntPtr pointer = Marshal.AllocHGlobal(Marshal.SizeOf<SDL3.SDL.DisplayMode>());
        Marshal.StructureToPtr(value, pointer, false);
        return pointer;
    }

    private static IntPtr AllocatePointerArray(IntPtr[] values)
    {
        IntPtr pointer = Marshal.AllocHGlobal(IntPtr.Size * values.Length);
        for (int i = 0; i < values.Length; i++)
        {
            Marshal.WriteIntPtr(pointer, i * IntPtr.Size, values[i]);
        }

        return pointer;
    }

    private static void FreeDisplayModePointers(IntPtr[] pointers)
    {
        foreach (IntPtr pointer in pointers)
        {
            Marshal.FreeHGlobal(pointer);
        }
    }

    private static SDL3.SDL.DisplayMode CreateDisplayMode(uint displayID, SDL3.SDL.PixelFormat format, int width, int height,
        float pixelDensity, float refreshRate, int refreshRateNumerator, int refreshRateDenominator)
    {
        return new SDL3.SDL.DisplayMode
        {
            DisplayID = displayID,
            Format = format,
            W = width,
            H = height,
            PixelDensity = pixelDensity,
            RefreshRate = refreshRate,
            RefreshRateNumerator = refreshRateNumerator,
            RefreshRateDenominator = refreshRateDenominator
        };
    }

    private static void AssertRect(SDL3.SDL.Rect expected, SDL3.SDL.Rect actual, string message)
    {
        TestAssert.Equal(expected.X, actual.X, $"{message} X");
        TestAssert.Equal(expected.Y, actual.Y, $"{message} Y");
        TestAssert.Equal(expected.W, actual.W, $"{message} W");
        TestAssert.Equal(expected.H, actual.H, $"{message} H");
    }

    private static void AssertPoint(SDL3.SDL.Point expected, SDL3.SDL.Point actual, string message)
    {
        TestAssert.Equal(expected.X, actual.X, $"{message} X");
        TestAssert.Equal(expected.Y, actual.Y, $"{message} Y");
    }

    private static void AssertDisplayMode(SDL3.SDL.DisplayMode expected, SDL3.SDL.DisplayMode actual, string message)
    {
        TestAssert.Equal(expected.DisplayID, actual.DisplayID, $"{message} DisplayID");
        TestAssert.Equal(expected.Format, actual.Format, $"{message} Format");
        TestAssert.Equal(expected.W, actual.W, $"{message} W");
        TestAssert.Equal(expected.H, actual.H, $"{message} H");
        TestAssert.Equal(expected.PixelDensity, actual.PixelDensity, $"{message} PixelDensity");
        TestAssert.Equal(expected.RefreshRate, actual.RefreshRate, $"{message} RefreshRate");
        TestAssert.Equal(expected.RefreshRateNumerator, actual.RefreshRateNumerator, $"{message} RefreshRateNumerator");
        TestAssert.Equal(expected.RefreshRateDenominator, actual.RefreshRateDenominator, $"{message} RefreshRateDenominator");
    }

    private static void ResetCaptureState()
    {
        nextPointer = IntPtr.Zero;
        capturedFreePointer = IntPtr.Zero;
        capturedWindow = IntPtr.Zero;
        capturedIcon = IntPtr.Zero;
        capturedParent = IntPtr.Zero;
        capturedShape = IntPtr.Zero;
        capturedModePointer = IntPtr.Zero;
        capturedRectPointer = IntPtr.Zero;
        capturedCallbackData = IntPtr.Zero;
        capturedContext = IntPtr.Zero;
        capturedUserData = IntPtr.Zero;
        nextSize = UIntPtr.Zero;
        nextInt = 0;
        capturedIndex = 0;
        nextCount = 0;
        capturedFreeCallCount = 0;
        nextVSync = 0;
        capturedVSync = 0;
        capturedNumRects = 0;
        nextUInt = 0;
        capturedDisplayID = 0;
        capturedWidth = 0;
        capturedHeight = 0;
        nextX = 0;
        nextY = 0;
        nextWidth = 0;
        nextHeight = 0;
        capturedX = 0;
        capturedY = 0;
        nextTop = 0;
        nextLeft = 0;
        nextBottom = 0;
        nextRight = 0;
        nextBool = false;
        capturedBool = false;
        capturedIncludeHighDensityModes = false;
        nextFloat = 0;
        capturedRefreshRate = 0;
        nextMinAspect = 0;
        nextMaxAspect = 0;
        capturedMinAspect = 0;
        capturedMaxAspect = 0;
        capturedOpacity = 0;
        nextRect = default;
        capturedRects = null;
        capturedPoint = default;
        capturedRect = default;
        nextDisplayMode = default;
        capturedDisplayMode = default;
        nextPixelFormat = default;
        nextSystemTheme = default;
        nextDisplayOrientation = default;
        nextWindowFlags = default;
        capturedWindowFlags = default;
        capturedFlashOperation = default;
        nextProgressState = default;
        capturedProgressState = default;
        capturedHitTest = null;
        capturedTitle = null;
        capturedProperties = 0;
        capturedWindowID = 0;
        capturedOffsetX = 0;
        capturedOffsetY = 0;
        capturedValue = 0;
        capturedInterval = 0;
        capturedNoArgCallCount = 0;
        capturedProgressValue = 0;
        capturedGLAttr = default;
        capturedPlatformAttribCallback = null;
        capturedSurfaceAttribCallback = null;
        capturedContextAttribCallback = null;
        capturedPath = null;
        capturedProc = null;
        capturedExtension = null;
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

    private static void AssertBoolParameterMarshal(MethodInfo method, int index)
    {
        MarshalAsAttribute? marshalAs = method.GetParameters()[index].GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} bool parameter must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.I1, marshalAs!.Value, $"SDL.{method.Name} bool parameter must use I1 marshalling.");
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

    private static unsafe T[] CopyUnmanaged<T>(IntPtr pointer, int count) where T : unmanaged
    {
        if (pointer == IntPtr.Zero || count <= 0)
        {
            return [];
        }

        T[] result = new T[count];
        new ReadOnlySpan<T>((void*)pointer, count).CopyTo(result);
        return result;
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
