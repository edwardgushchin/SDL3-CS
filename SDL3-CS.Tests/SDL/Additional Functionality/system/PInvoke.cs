using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.AdditionalFunctionality.System;

internal static class PInvokeTests
{
    private static SDL3.SDL.WindowsMessageHook? capturedWindowsMessageHook;
    private static SDL3.SDL.X11EventHook? capturedX11EventHook;
    private static SDL3.SDL.IOSAnimationCallback? capturedIOSAnimationCallback;
    private static SDL3.SDL.RequestAndroidPermissionCallback? capturedAndroidPermissionCallback;
    private static IntPtr capturedWindow;
    private static IntPtr capturedUserdata;
    private static IntPtr capturedCallbackParam;
    private static IntPtr capturedOutHandle;
    private static IntPtr nextPointer;
    private static uint capturedDisplayID;
    private static long capturedThreadID;
    private static int capturedPriority;
    private static int capturedSchedPolicy;
    private static int capturedInterval;
    private static bool capturedEnabled;
    private static string? capturedPermission;
    private static string? capturedMessage;
    private static int capturedDuration;
    private static int capturedGravity;
    private static int capturedXOffset;
    private static int capturedYOffset;
    private static uint capturedCommand;
    private static int capturedParam;
    private static int capturedCallCount;

    public static void SetWindowsMessageHook_ForwardsCallbackAndUserdata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetWindowsMessageHook");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetWindowsMessageHook");

        SDL3.SDL.WindowsMessageHook callback = TestWindowsMessageHook;
        using NativeHookScope _ = NativeHookScope.Install("SetWindowsMessageHookNativeFunction", nameof(CaptureSetWindowsMessageHook));
        SDL3.SDL.SetWindowsMessageHook(callback, (IntPtr)101);

        TestAssert.Equal((IntPtr)101, capturedUserdata, "SDL.SetWindowsMessageHook must forward userdata.");
        TestAssert.NotNull(capturedWindowsMessageHook, "SDL.SetWindowsMessageHook must forward callback.");
        TestAssert.Equal(true, capturedWindowsMessageHook!((IntPtr)1, (IntPtr)2), "SDL.SetWindowsMessageHook callback must remain callable.");
    }

    public static void GetDirect3D9AdapterIndex_ForwardsDisplayID()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetDirect3D9AdapterIndex");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetDirect3D9AdapterIndex");

        using NativeHookScope _ = NativeHookScope.Install("GetDirect3D9AdapterIndexNativeFunction", nameof(CaptureGetDirect3D9AdapterIndex));
        int adapterIndex = SDL3.SDL.GetDirect3D9AdapterIndex(102);

        TestAssert.Equal(9, adapterIndex, "SDL.GetDirect3D9AdapterIndex must return the native hook value.");
        TestAssert.Equal(102u, capturedDisplayID, "SDL.GetDirect3D9AdapterIndex must forward displayID.");
    }

    public static void GetDXGIOutputInfo_ForwardsDisplayIDAndOutputs()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetDXGIOutputInfo");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetDXGIOutputInfo");
        AssertBoolReturnMarshal(nativeMethod);

        using NativeHookScope _ = NativeHookScope.Install("GetDXGIOutputInfoNativeFunction", nameof(CaptureGetDXGIOutputInfo));
        bool result = SDL3.SDL.GetDXGIOutputInfo(103, out int adapterIndex, out int outputIndex);

        TestAssert.Equal(true, result, "SDL.GetDXGIOutputInfo must return the native hook value.");
        TestAssert.Equal(103u, capturedDisplayID, "SDL.GetDXGIOutputInfo must forward displayID.");
        TestAssert.Equal(7, adapterIndex, "SDL.GetDXGIOutputInfo must forward adapterIndex from native.");
        TestAssert.Equal(8, outputIndex, "SDL.GetDXGIOutputInfo must forward outputIndex from native.");
    }

    public static void SetX11EventHook_ForwardsCallbackAndUserdata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetX11EventHook");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetX11EventHook");

        SDL3.SDL.X11EventHook callback = TestX11EventHook;
        using NativeHookScope _ = NativeHookScope.Install("SetX11EventHookNativeFunction", nameof(CaptureSetX11EventHook));
        SDL3.SDL.SetX11EventHook(callback, (IntPtr)104);

        TestAssert.Equal((IntPtr)104, capturedUserdata, "SDL.SetX11EventHook must forward userdata.");
        TestAssert.NotNull(capturedX11EventHook, "SDL.SetX11EventHook must forward callback.");
        TestAssert.Equal(false, capturedX11EventHook!((IntPtr)1, (IntPtr)2), "SDL.SetX11EventHook callback must remain callable.");
    }

    public static void SetLinuxThreadPriority_ForwardsThreadAndPriority()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetLinuxThreadPriority");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetLinuxThreadPriority");
        AssertBoolReturnMarshal(nativeMethod);

        using NativeHookScope _ = NativeHookScope.Install("SetLinuxThreadPriorityNativeFunction", nameof(CaptureSetLinuxThreadPriority));
        bool result = SDL3.SDL.SetLinuxThreadPriority(105, 6);

        TestAssert.Equal(true, result, "SDL.SetLinuxThreadPriority must return the native hook value.");
        TestAssert.Equal(105L, capturedThreadID, "SDL.SetLinuxThreadPriority must forward threadID.");
        TestAssert.Equal(6, capturedPriority, "SDL.SetLinuxThreadPriority must forward priority.");
    }

    public static void SetLinuxThreadPriorityAndPolicy_ForwardsThreadPriorityAndPolicy()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetLinuxThreadPriorityAndPolicy");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetLinuxThreadPriorityAndPolicy");
        AssertBoolReturnMarshal(nativeMethod);

        using NativeHookScope _ = NativeHookScope.Install("SetLinuxThreadPriorityAndPolicyNativeFunction", nameof(CaptureSetLinuxThreadPriorityAndPolicy));
        bool result = SDL3.SDL.SetLinuxThreadPriorityAndPolicy(106, 7, 8);

        TestAssert.Equal(true, result, "SDL.SetLinuxThreadPriorityAndPolicy must return the native hook value.");
        TestAssert.Equal(106L, capturedThreadID, "SDL.SetLinuxThreadPriorityAndPolicy must forward threadID.");
        TestAssert.Equal(7, capturedPriority, "SDL.SetLinuxThreadPriorityAndPolicy must forward priority.");
        TestAssert.Equal(8, capturedSchedPolicy, "SDL.SetLinuxThreadPriorityAndPolicy must forward schedPolicy.");
    }

    public static void SetiOSAnimationCallback_ForwardsWindowIntervalCallbackAndParam()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetiOSAnimationCallback");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetiOSAnimationCallback");
        AssertBoolReturnMarshal(nativeMethod);

        SDL3.SDL.IOSAnimationCallback callback = TestIOSAnimationCallback;
        using NativeHookScope _ = NativeHookScope.Install("SetiOSAnimationCallbackNativeFunction", nameof(CaptureSetiOSAnimationCallback));
        bool result = SDL3.SDL.SetiOSAnimationCallback((IntPtr)107, 2, callback, (IntPtr)108);

        TestAssert.Equal(true, result, "SDL.SetiOSAnimationCallback must return the native hook value.");
        TestAssert.Equal((IntPtr)107, capturedWindow, "SDL.SetiOSAnimationCallback must forward window.");
        TestAssert.Equal(2, capturedInterval, "SDL.SetiOSAnimationCallback must forward interval.");
        TestAssert.Equal((IntPtr)108, capturedCallbackParam, "SDL.SetiOSAnimationCallback must forward callbackParam.");
        TestAssert.NotNull(capturedIOSAnimationCallback, "SDL.SetiOSAnimationCallback must forward callback.");
    }

    public static void SetiOSEventPump_ForwardsEnabled()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SetiOSEventPump");
        AssertSdlLibraryImport(nativeMethod, "SDL_SetiOSEventPump");
        AssertBoolParameterMarshal(nativeMethod, "enabled");

        using NativeHookScope _ = NativeHookScope.Install("SetiOSEventPumpNativeFunction", nameof(CaptureSetiOSEventPump));
        SDL3.SDL.SetiOSEventPump(true);

        TestAssert.Equal(true, capturedEnabled, "SDL.SetiOSEventPump must forward enabled.");
    }

    public static void GetAndroidJNIEnv_ReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetAndroidJNIEnv");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetAndroidJNIEnv");

        using NativeHookScope _ = NativeHookScope.Install("GetAndroidJNIEnvNativeFunction", nameof(CaptureGetAndroidJNIEnv));
        IntPtr result = SDL3.SDL.GetAndroidJNIEnv();

        TestAssert.Equal((IntPtr)109, result, "SDL.GetAndroidJNIEnv must return the native hook pointer.");
    }

    public static void GetAndroidActivity_ReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetAndroidActivity");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetAndroidActivity");

        using NativeHookScope _ = NativeHookScope.Install("GetAndroidActivityNativeFunction", nameof(CaptureGetAndroidActivity));
        IntPtr result = SDL3.SDL.GetAndroidActivity();

        TestAssert.Equal((IntPtr)110, result, "SDL.GetAndroidActivity must return the native hook pointer.");
    }

    public static void GetAndroidSDKVersion_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetAndroidSDKVersion");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetAndroidSDKVersion");

        using NativeHookScope _ = NativeHookScope.Install("GetAndroidSDKVersionNativeFunction", nameof(CaptureGetAndroidSDKVersion));
        int result = SDL3.SDL.GetAndroidSDKVersion();

        TestAssert.Equal(35, result, "SDL.GetAndroidSDKVersion must return the native hook value.");
    }

    public static void IsChromebook_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_IsChromebook");
        AssertSdlLibraryImport(nativeMethod, "SDL_IsChromebook");
        AssertBoolReturnMarshal(nativeMethod);

        using NativeHookScope _ = NativeHookScope.Install("IsChromebookNativeFunction", nameof(CaptureTrue));
        bool result = SDL3.SDL.IsChromebook();

        TestAssert.Equal(true, result, "SDL.IsChromebook must return the native hook value.");
    }

    public static void IsDeXMode_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_IsDeXMode");
        AssertSdlLibraryImport(nativeMethod, "SDL_IsDeXMode");
        AssertBoolReturnMarshal(nativeMethod);

        using NativeHookScope _ = NativeHookScope.Install("IsDeXModeNativeFunction", nameof(CaptureTrue));
        bool result = SDL3.SDL.IsDeXMode();

        TestAssert.Equal(true, result, "SDL.IsDeXMode must return the native hook value.");
    }

    public static void SendAndroidBackButton_ForwardsCall()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SendAndroidBackButton");
        AssertSdlLibraryImport(nativeMethod, "SDL_SendAndroidBackButton");

        capturedCallCount = 0;
        using NativeHookScope _ = NativeHookScope.Install("SendAndroidBackButtonNativeFunction", nameof(CaptureVoidCall));
        SDL3.SDL.SendAndroidBackButton();

        TestAssert.Equal(1, capturedCallCount, "SDL.SendAndroidBackButton must call the native hook once.");
    }

    public static void SDL_GetAndroidInternalStoragePath_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetAndroidInternalStoragePath");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetAndroidInternalStoragePath");
    }

    public static void GetAndroidInternalStoragePath_ReturnsStringAndNull()
    {
        using NativeHookScope _ = NativeHookScope.Install("GetAndroidInternalStoragePathNativeFunction", nameof(CapturePathPointer));

        string? value = CaptureUtf8Path(() => SDL3.SDL.GetAndroidInternalStoragePath(), "/data/data/app/files");
        TestAssert.Equal("/data/data/app/files", value, "SDL.GetAndroidInternalStoragePath must convert UTF-8 native path.");

        nextPointer = IntPtr.Zero;
        TestAssert.Equal<string?>(null, SDL3.SDL.GetAndroidInternalStoragePath(), "SDL.GetAndroidInternalStoragePath must return null for native null.");
    }

    public static void GetAndroidExternalStorageState_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetAndroidExternalStorageState");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetAndroidExternalStorageState");

        using NativeHookScope _ = NativeHookScope.Install("GetAndroidExternalStorageStateNativeFunction", nameof(CaptureGetAndroidExternalStorageState));
        uint result = SDL3.SDL.GetAndroidExternalStorageState();

        TestAssert.Equal(3u, result, "SDL.GetAndroidExternalStorageState must return the native hook value.");
    }

    public static void SDL_GetAndroidExternalStoragePath_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetAndroidExternalStoragePath");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetAndroidExternalStoragePath");
    }

    public static void GetAndroidExternalStoragePath_ReturnsStringAndNull()
    {
        using NativeHookScope _ = NativeHookScope.Install("GetAndroidExternalStoragePathNativeFunction", nameof(CapturePathPointer));

        string? value = CaptureUtf8Path(() => SDL3.SDL.GetAndroidExternalStoragePath(), "/storage/app/files");
        TestAssert.Equal("/storage/app/files", value, "SDL.GetAndroidExternalStoragePath must convert UTF-8 native path.");

        nextPointer = IntPtr.Zero;
        TestAssert.Equal<string?>(null, SDL3.SDL.GetAndroidExternalStoragePath(), "SDL.GetAndroidExternalStoragePath must return null for native null.");
    }

    public static void SDL_GetAndroidCachePath_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetAndroidCachePath");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetAndroidCachePath");
    }

    public static void GetAndroidCachePath_ReturnsStringAndNull()
    {
        using NativeHookScope _ = NativeHookScope.Install("GetAndroidCachePathNativeFunction", nameof(CapturePathPointer));

        string? value = CaptureUtf8Path(() => SDL3.SDL.GetAndroidCachePath(), "/data/data/app/cache");
        TestAssert.Equal("/data/data/app/cache", value, "SDL.GetAndroidCachePath must convert UTF-8 native path.");

        nextPointer = IntPtr.Zero;
        TestAssert.Equal<string?>(null, SDL3.SDL.GetAndroidCachePath(), "SDL.GetAndroidCachePath must return null for native null.");
    }

    public static void RequestAndroidPermission_ForwardsPermissionCallbackAndUserdata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_RequestAndroidPermission");
        AssertSdlLibraryImport(nativeMethod, "SDL_RequestAndroidPermission");
        AssertBoolReturnMarshal(nativeMethod);
        AssertStringParameterMarshal(nativeMethod, "permission");

        SDL3.SDL.RequestAndroidPermissionCallback callback = TestAndroidPermissionCallback;
        using NativeHookScope _ = NativeHookScope.Install("RequestAndroidPermissionNativeFunction", nameof(CaptureRequestAndroidPermission));
        bool result = SDL3.SDL.RequestAndroidPermission("android.permission.CAMERA", callback, (IntPtr)111);

        TestAssert.Equal(true, result, "SDL.RequestAndroidPermission must return the native hook value.");
        TestAssert.Equal("android.permission.CAMERA", capturedPermission, "SDL.RequestAndroidPermission must forward permission.");
        TestAssert.Equal((IntPtr)111, capturedUserdata, "SDL.RequestAndroidPermission must forward userdata.");
        TestAssert.NotNull(capturedAndroidPermissionCallback, "SDL.RequestAndroidPermission must forward callback.");
    }

    public static void ShowAndroidToast_ForwardsMessageAndLayout()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_ShowAndroidToast");
        AssertSdlLibraryImport(nativeMethod, "SDL_ShowAndroidToast");
        AssertBoolReturnMarshal(nativeMethod);
        AssertStringParameterMarshal(nativeMethod, "message");

        using NativeHookScope _ = NativeHookScope.Install("ShowAndroidToastNativeFunction", nameof(CaptureShowAndroidToast));
        bool result = SDL3.SDL.ShowAndroidToast("hello", 1, 2, 3, 4);

        TestAssert.Equal(true, result, "SDL.ShowAndroidToast must return the native hook value.");
        TestAssert.Equal("hello", capturedMessage, "SDL.ShowAndroidToast must forward message.");
        TestAssert.Equal(1, capturedDuration, "SDL.ShowAndroidToast must forward duration.");
        TestAssert.Equal(2, capturedGravity, "SDL.ShowAndroidToast must forward gravity.");
        TestAssert.Equal(3, capturedXOffset, "SDL.ShowAndroidToast must forward xoffset.");
        TestAssert.Equal(4, capturedYOffset, "SDL.ShowAndroidToast must forward yoffset.");
    }

    public static void SendAndroidMessage_ForwardsCommandAndParam()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SendAndroidMessage");
        AssertSdlLibraryImport(nativeMethod, "SDL_SendAndroidMessage");
        AssertBoolReturnMarshal(nativeMethod);

        using NativeHookScope _ = NativeHookScope.Install("SendAndroidMessageNativeFunction", nameof(CaptureSendAndroidMessage));
        bool result = SDL3.SDL.SendAndroidMessage(0x8001, 112);

        TestAssert.Equal(true, result, "SDL.SendAndroidMessage must return the native hook value.");
        TestAssert.Equal(0x8001u, capturedCommand, "SDL.SendAndroidMessage must forward command.");
        TestAssert.Equal(112, capturedParam, "SDL.SendAndroidMessage must forward param.");
    }

    public static void IsPhone_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_IsPhone");
        AssertSdlLibraryImport(nativeMethod, "SDL_IsPhone");
        AssertBoolReturnMarshal(nativeMethod);

        using NativeHookScope _ = NativeHookScope.Install("IsPhoneNativeFunction", nameof(CaptureTrue));
        bool result = SDL3.SDL.IsPhone();

        TestAssert.Equal(true, result, "SDL.IsPhone must return the native hook value.");
    }

    public static void IsTablet_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_IsTablet");
        AssertSdlLibraryImport(nativeMethod, "SDL_IsTablet");
        AssertBoolReturnMarshal(nativeMethod);

        using NativeHookScope _ = NativeHookScope.Install("IsTabletNativeFunction", nameof(CaptureTrue));
        bool result = SDL3.SDL.IsTablet();

        TestAssert.Equal(true, result, "SDL.IsTablet must return the native hook value.");
    }

    public static void IsTV_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_IsTV");
        AssertSdlLibraryImport(nativeMethod, "SDL_IsTV");
        AssertBoolReturnMarshal(nativeMethod);

        using NativeHookScope _ = NativeHookScope.Install("IsTVNativeFunction", nameof(CaptureTrue));
        bool result = SDL3.SDL.IsTV();

        TestAssert.Equal(true, result, "SDL.IsTV must return the native hook value.");
    }

    public static void GetSandbox_ReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetSandbox");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetSandbox");

        using NativeHookScope _ = NativeHookScope.Install("GetSandboxNativeFunction", nameof(CaptureGetSandbox));
        SDL3.SDL.Sandbox result = SDL3.SDL.GetSandbox();

        TestAssert.Equal(SDL3.SDL.Sandbox.Flatpak, result, "SDL.GetSandbox must return the native hook value.");
    }

    public static void OnApplicationWillTerminate_ForwardsCall()
    {
        AssertVoidNoArgForwarder("SDL_OnApplicationWillTerminate", "SDL_OnApplicationWillTerminate", "OnApplicationWillTerminateNativeFunction", SDL3.SDL.OnApplicationWillTerminate);
    }

    public static void OnApplicationDidReceiveMemoryWarning_ForwardsCall()
    {
        AssertVoidNoArgForwarder("SDL_OnApplicationDidReceiveMemoryWarning", "SDL_OnApplicationDidReceiveMemoryWarning", "OnApplicationDidReceiveMemoryWarningNativeFunction", SDL3.SDL.OnApplicationDidReceiveMemoryWarning);
    }

    public static void OnApplicationWillEnterBackground_ForwardsCall()
    {
        AssertVoidNoArgForwarder("SDL_OnApplicationWillEnterBackground", "SDL_OnApplicationWillEnterBackground", "OnApplicationWillEnterBackgroundNativeFunction", SDL3.SDL.OnApplicationWillEnterBackground);
    }

    public static void OnApplicationDidEnterBackground_ForwardsCall()
    {
        AssertVoidNoArgForwarder("SDL_OnApplicationDidEnterBackground", "SDL_OnApplicationDidEnterBackground", "OnApplicationDidEnterBackgroundNativeFunction", SDL3.SDL.OnApplicationDidEnterBackground);
    }

    public static void OnApplicationWillEnterForeground_ForwardsCall()
    {
        AssertVoidNoArgForwarder("SDL_OnApplicationWillEnterForeground", "SDL_OnApplicationWillEnterForeground", "OnApplicationWillEnterForegroundNativeFunction", SDL3.SDL.OnApplicationWillEnterForeground);
    }

    public static void OnApplicationDidEnterForeground_ForwardsCall()
    {
        AssertVoidNoArgForwarder("SDL_OnApplicationDidEnterForeground", "SDL_OnApplicationDidEnterForeground", "OnApplicationDidEnterForegroundNativeFunction", SDL3.SDL.OnApplicationDidEnterForeground);
    }

    public static void OnApplicationDidChangeStatusBarOrientation_ForwardsCall()
    {
        AssertVoidNoArgForwarder("SDL_OnApplicationDidChangeStatusBarOrientation", "SDL_OnApplicationDidChangeStatusBarOrientation", "OnApplicationDidChangeStatusBarOrientationNativeFunction", SDL3.SDL.OnApplicationDidChangeStatusBarOrientation);
    }

    public static void GetGDKTaskQueue_ReturnsOutputHandle()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGDKTaskQueue");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetGDKTaskQueue");
        AssertBoolReturnMarshal(nativeMethod);

        using NativeHookScope _ = NativeHookScope.Install("GetGDKTaskQueueNativeFunction", nameof(CaptureGetGDKTaskQueue));
        bool result = SDL3.SDL.GetGDKTaskQueue(out IntPtr outTaskQueue);

        TestAssert.Equal(true, result, "SDL.GetGDKTaskQueue must return the native hook value.");
        TestAssert.Equal((IntPtr)113, outTaskQueue, "SDL.GetGDKTaskQueue must forward the native output handle.");
    }

    public static void GetGDKDefaultUser_ReturnsOutputHandle()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetGDKDefaultUser");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetGDKDefaultUser");
        AssertBoolReturnMarshal(nativeMethod);

        using NativeHookScope _ = NativeHookScope.Install("GetGDKDefaultUserNativeFunction", nameof(CaptureGetGDKDefaultUser));
        bool result = SDL3.SDL.GetGDKDefaultUser(out IntPtr outUserHandle);

        TestAssert.Equal(true, result, "SDL.GetGDKDefaultUser must return the native hook value.");
        TestAssert.Equal((IntPtr)114, outUserHandle, "SDL.GetGDKDefaultUser must forward the native output handle.");
    }

    private static void CaptureSetWindowsMessageHook(SDL3.SDL.WindowsMessageHook callback, IntPtr userdata)
    {
        capturedWindowsMessageHook = callback;
        capturedUserdata = userdata;
    }

    private static int CaptureGetDirect3D9AdapterIndex(uint displayID)
    {
        capturedDisplayID = displayID;
        return 9;
    }

    private static bool CaptureGetDXGIOutputInfo(uint displayID, out int adapterIndex, out int outputIndex)
    {
        capturedDisplayID = displayID;
        adapterIndex = 7;
        outputIndex = 8;
        return true;
    }

    private static void CaptureSetX11EventHook(SDL3.SDL.X11EventHook callback, IntPtr userdata)
    {
        capturedX11EventHook = callback;
        capturedUserdata = userdata;
    }

    private static bool CaptureSetLinuxThreadPriority(long threadID, int priority)
    {
        capturedThreadID = threadID;
        capturedPriority = priority;
        return true;
    }

    private static bool CaptureSetLinuxThreadPriorityAndPolicy(long threadID, int priority, int schedPolicy)
    {
        capturedThreadID = threadID;
        capturedPriority = priority;
        capturedSchedPolicy = schedPolicy;
        return true;
    }

    private static bool CaptureSetiOSAnimationCallback(IntPtr window, int interval, SDL3.SDL.IOSAnimationCallback callback, IntPtr callbackParam)
    {
        capturedWindow = window;
        capturedInterval = interval;
        capturedIOSAnimationCallback = callback;
        capturedCallbackParam = callbackParam;
        return true;
    }

    private static void CaptureSetiOSEventPump(bool enabled)
    {
        capturedEnabled = enabled;
    }

    private static IntPtr CaptureGetAndroidJNIEnv()
    {
        return (IntPtr)109;
    }

    private static IntPtr CaptureGetAndroidActivity()
    {
        return (IntPtr)110;
    }

    private static int CaptureGetAndroidSDKVersion()
    {
        return 35;
    }

    private static bool CaptureTrue()
    {
        return true;
    }

    private static void CaptureVoidCall()
    {
        capturedCallCount++;
    }

    private static IntPtr CapturePathPointer()
    {
        return nextPointer;
    }

    private static uint CaptureGetAndroidExternalStorageState()
    {
        return 3;
    }

    private static bool CaptureRequestAndroidPermission(string permission, SDL3.SDL.RequestAndroidPermissionCallback cb, IntPtr userdata)
    {
        capturedPermission = permission;
        capturedAndroidPermissionCallback = cb;
        capturedUserdata = userdata;
        return true;
    }

    private static bool CaptureShowAndroidToast(string message, int duration, int gravity, int xoffset, int yoffset)
    {
        capturedMessage = message;
        capturedDuration = duration;
        capturedGravity = gravity;
        capturedXOffset = xoffset;
        capturedYOffset = yoffset;
        return true;
    }

    private static bool CaptureSendAndroidMessage(uint command, int param)
    {
        capturedCommand = command;
        capturedParam = param;
        return true;
    }

    private static SDL3.SDL.Sandbox CaptureGetSandbox()
    {
        return SDL3.SDL.Sandbox.Flatpak;
    }

    private static bool CaptureGetGDKTaskQueue(out IntPtr outTaskQueue)
    {
        capturedOutHandle = (IntPtr)113;
        outTaskQueue = capturedOutHandle;
        return true;
    }

    private static bool CaptureGetGDKDefaultUser(out IntPtr outUserHandle)
    {
        capturedOutHandle = (IntPtr)114;
        outUserHandle = capturedOutHandle;
        return true;
    }

    private static bool TestWindowsMessageHook(IntPtr userdata, IntPtr msg)
    {
        return true;
    }

    private static bool TestX11EventHook(IntPtr userdata, IntPtr xevent)
    {
        return false;
    }

    private static void TestIOSAnimationCallback(IntPtr userdata)
    {
    }

    private static void TestAndroidPermissionCallback(IntPtr userdata, string permission, bool granted)
    {
    }

    private static string? CaptureUtf8Path(Func<string?> action, string path)
    {
        nextPointer = Marshal.StringToCoTaskMemUTF8(path);

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

    private static void AssertVoidNoArgForwarder(string methodName, string entryPoint, string hookFieldName, Action action)
    {
        MethodInfo nativeMethod = GetNativeMethod(methodName);
        AssertSdlLibraryImport(nativeMethod, entryPoint);

        capturedCallCount = 0;
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureVoidCall));
        action();

        TestAssert.Equal(1, capturedCallCount, $"SDL.{methodName} public wrapper must call the native hook once.");
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
    }

    private static void AssertBoolReturnMarshal(MethodInfo method)
    {
        MarshalAsAttribute? marshalAs = method.ReturnParameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} return value must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.I1, marshalAs!.Value, $"SDL.{method.Name} return value must use I1 marshalling.");
    }

    private static void AssertBoolParameterMarshal(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.I1, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use I1 marshalling.");
    }

    private static void AssertStringParameterMarshal(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.LPUTF8Str, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use UTF-8 string marshalling.");
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
