using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.AdditionalFunctionality.Notification;

internal static class PInvokeTests
{
    private static bool requestPermissionCalled;
    private static uint capturedProperties;
    private static string? capturedTitle;
    private static string? capturedMessage;
    private static IntPtr capturedImage;
    private static IntPtr capturedActions;
    private static int capturedNumActions;
    private static uint capturedNotification;

    public static void RequestNotificationPermission_ForwardsCall()
    {
        MethodInfo? nativeMethod = typeof(SDL3.SDL).GetMethod("SDL_RequestNotificationPermission", BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(nativeMethod, "SDL.SDL_RequestNotificationPermission method must be private static.");
        AssertSdlLibraryImport(nativeMethod!, "SDL_RequestNotificationPermission");
        AssertBoolReturnMarshal(nativeMethod!);

        using NativeHookScope _ = NativeHookScope.Install("RequestNotificationPermissionNativeFunction", nameof(CaptureRequestNotificationPermission));
        bool result = SDL3.SDL.RequestNotificationPermission();

        TestAssert.Equal(true, result, "SDL.RequestNotificationPermission must return the native hook result.");
        TestAssert.Equal(true, requestPermissionCalled, "SDL.RequestNotificationPermission must call the native hook.");
    }

    public static void ShowNotificationWithProperties_ForwardsProperties()
    {
        MethodInfo? nativeMethod = typeof(SDL3.SDL).GetMethod("SDL_ShowNotificationWithProperties", BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(nativeMethod, "SDL.SDL_ShowNotificationWithProperties method must be private static.");
        AssertSdlLibraryImport(nativeMethod!, "SDL_ShowNotificationWithProperties");

        using NativeHookScope _ = NativeHookScope.Install("ShowNotificationWithPropertiesNativeFunction", nameof(CaptureShowNotificationWithProperties));
        uint id = SDL3.SDL.ShowNotificationWithProperties(1234);

        TestAssert.Equal(777u, id, "SDL.ShowNotificationWithProperties must return the native hook notification id.");
        TestAssert.Equal(1234u, capturedProperties, "SDL.ShowNotificationWithProperties must forward properties.");
    }

    public static void ShowNotification_ForwardsTextAndPointers()
    {
        MethodInfo? nativeMethod = typeof(SDL3.SDL).GetMethod("SDL_ShowNotification", BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(nativeMethod, "SDL.SDL_ShowNotification method must be private static.");
        AssertSdlLibraryImport(nativeMethod!, "SDL_ShowNotification");
        AssertStringParameterMarshal(nativeMethod!, "title");
        AssertStringParameterMarshal(nativeMethod!, "message");

        using NativeHookScope _ = NativeHookScope.Install("ShowNotificationNativeFunction", nameof(CaptureShowNotification));
        uint id = SDL3.SDL.ShowNotification("Title", "Message", (IntPtr)1, (IntPtr)2, 3);

        TestAssert.Equal(778u, id, "SDL.ShowNotification must return the native hook notification id.");
        TestAssert.Equal("Title", capturedTitle, "SDL.ShowNotification must forward title.");
        TestAssert.Equal("Message", capturedMessage, "SDL.ShowNotification must forward message.");
        TestAssert.Equal((IntPtr)1, capturedImage, "SDL.ShowNotification must forward image pointer.");
        TestAssert.Equal((IntPtr)2, capturedActions, "SDL.ShowNotification must forward actions pointer.");
        TestAssert.Equal(3, capturedNumActions, "SDL.ShowNotification must forward action count.");
    }

    public static void RemoveNotification_ForwardsNotificationId()
    {
        MethodInfo? nativeMethod = typeof(SDL3.SDL).GetMethod("SDL_RemoveNotification", BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(nativeMethod, "SDL.SDL_RemoveNotification method must be private static.");
        AssertSdlLibraryImport(nativeMethod!, "SDL_RemoveNotification");
        AssertBoolReturnMarshal(nativeMethod!);

        using NativeHookScope _ = NativeHookScope.Install("RemoveNotificationNativeFunction", nameof(CaptureRemoveNotification));
        bool removed = SDL3.SDL.RemoveNotification(779);

        TestAssert.Equal(true, removed, "SDL.RemoveNotification must return the native hook result.");
        TestAssert.Equal(779u, capturedNotification, "SDL.RemoveNotification must forward notification id.");
    }

    private static bool CaptureRequestNotificationPermission()
    {
        requestPermissionCalled = true;
        return true;
    }

    private static uint CaptureShowNotificationWithProperties(uint props)
    {
        capturedProperties = props;
        return 777;
    }

    private static uint CaptureShowNotification(string title, string? message, IntPtr image, IntPtr actions, int numActions)
    {
        capturedTitle = title;
        capturedMessage = message;
        capturedImage = image;
        capturedActions = actions;
        capturedNumActions = numActions;
        return 778;
    }

    private static bool CaptureRemoveNotification(uint notification)
    {
        capturedNotification = notification;
        return true;
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
