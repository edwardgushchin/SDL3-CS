using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.AdditionalFunctionality.Notification;

internal static class PInvokeTests
{
    private static uint capturedProperties;
    private static uint capturedNotificationId;
    private static string? capturedTitle;
    private static string? capturedMessage;
    private static IntPtr capturedImage;
    private static SDL3.SDL.NotificationAction[]? capturedActions;
    private static int capturedActionCount;
    private static int capturedCallCount;
    private static bool nextBool;

    public static void RunAll()
    {
        RequestNotificationPermission_ForwardsAndReturnsNativeValue();
        ShowNotificationWithProperties_ForwardsPropertiesAndReturnsNativeId();
        ShowNotification_ForwardsTextImageActionsAndReturnsNativeId();
        ShowNotification_ValidatesCountsAndOptionalData();
        RemoveNotification_ForwardsIdAndReturnsNativeValue();
        if (NativeLibraryProbe.SupportsSDL3Export("SDL_RemoveNotification")) RemoveNotification_InvalidIdReturnsFalse();
        NotificationTypes_MatchNativeAbi();
    }

    public static void RequestNotificationPermission_ForwardsAndReturnsNativeValue()
    {
        MethodInfo method = GetNativeMethod("SDL_RequestNotificationPermission");
        AssertImport(method, "SDL_RequestNotificationPermission");
        AssertBoolReturn(method);
        nextBool = true;
        capturedCallCount = 0;
        using NativeHookScope _ = NativeHookScope.Install("RequestNotificationPermissionNativeFunction", nameof(CaptureNoArgBool));
        TestAssert.Equal(true, SDL3.SDL.RequestNotificationPermission(), "SDL.RequestNotificationPermission must return the native hook value.");
        TestAssert.Equal(1, capturedCallCount, "SDL.RequestNotificationPermission must call its native hook once.");
    }

    public static void ShowNotificationWithProperties_ForwardsPropertiesAndReturnsNativeId()
    {
        MethodInfo method = GetNativeMethod("SDL_ShowNotificationWithProperties");
        AssertImport(method, "SDL_ShowNotificationWithProperties");
        capturedNotificationId = 0xA311u;
        using NativeHookScope _ = NativeHookScope.Install("ShowNotificationWithPropertiesNativeFunction", nameof(CaptureShowNotificationWithProperties));
        TestAssert.Equal(0xA311u, SDL3.SDL.ShowNotificationWithProperties(0xA312u), "SDL.ShowNotificationWithProperties must return native notification ID.");
        TestAssert.Equal(0xA312u, capturedProperties, "SDL.ShowNotificationWithProperties must forward property ID.");
    }

    public static void ShowNotification_ForwardsTextImageActionsAndReturnsNativeId()
    {
        MethodInfo method = GetNativeMethod("SDL_ShowNotification");
        AssertImport(method, "SDL_ShowNotification");
        AssertUtf8(method, "title");
        AssertUtf8(method, "message");
        AssertArray(method, "actions", 4);
        SDL3.SDL.NotificationAction[] actions = [new() { Type = SDL3.SDL.NotificationActionType.Button }];
        capturedNotificationId = 0xA321u;
        capturedCallCount = 0;
        using NativeHookScope _ = NativeHookScope.Install("ShowNotificationNativeFunction", nameof(CaptureShowNotification));
        uint result = SDL3.SDL.ShowNotification("title", "message", (IntPtr)0xA322, actions, 1);
        TestAssert.Equal(0xA321u, result, "SDL.ShowNotification must return native ID.");
        TestAssert.Equal("title", capturedTitle, "SDL.ShowNotification must forward title.");
        TestAssert.Equal("message", capturedMessage, "SDL.ShowNotification must forward message.");
        TestAssert.Equal((IntPtr)0xA322, capturedImage, "SDL.ShowNotification must forward image.");
        TestAssert.True(ReferenceEquals(actions, capturedActions), "SDL.ShowNotification must forward actions array.");
        TestAssert.Equal(1, capturedActionCount, "SDL.ShowNotification must forward action count.");
        TestAssert.Equal(1, capturedCallCount, "SDL.ShowNotification must call native hook once.");
    }

    public static void RemoveNotification_ForwardsIdAndReturnsNativeValue()
    {
        MethodInfo method = GetNativeMethod("SDL_RemoveNotification");
        AssertImport(method, "SDL_RemoveNotification");
        AssertBoolReturn(method);
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("RemoveNotificationNativeFunction", nameof(CaptureRemoveNotification));
        TestAssert.Equal(true, SDL3.SDL.RemoveNotification(0xA331u), "SDL.RemoveNotification must return native hook value.");
        TestAssert.Equal(0xA331u, capturedNotificationId, "SDL.RemoveNotification must forward ID.");
    }

    public static void ShowNotification_ValidatesCountsAndOptionalData()
    {
        using NativeHookScope hook = NativeHookScope.Install("ShowNotificationNativeFunction", nameof(CaptureShowNotification));
        capturedNotificationId = 0;
        TestAssert.Equal(0u, SDL3.SDL.ShowNotification("title", null, IntPtr.Zero, null, 0), "Optional null data must preserve native failure ID.");
        TestAssert.Equal<string?>(null, capturedMessage, "Optional message must preserve null.");
        TestAssert.Equal<SDL3.SDL.NotificationAction[]?>(null, capturedActions, "Optional actions must preserve null.");
        Action[] invalid = [
            () => SDL3.SDL.ShowNotification("title", null, IntPtr.Zero, [], -1),
            () => SDL3.SDL.ShowNotification("title", null, IntPtr.Zero, null, 1)
        ];
        foreach (Action action in invalid)
        {
            bool caught = false;
            try { action(); }
            catch (ArgumentOutOfRangeException) { caught = true; }
            TestAssert.True(caught, "Notification count must be within the actual action array.");
        }
    }

    public static void RemoveNotification_InvalidIdReturnsFalse()
    {
        TestAssert.Equal(false, SDL3.SDL.RemoveNotification(0), "SDL.RemoveNotification must fail for invalid ID zero.");
    }

    public static void NotificationTypes_MatchNativeAbi()
    {
        TestAssert.Equal(-1, (int)SDL3.SDL.NotificationPriority.Low, "Notification low priority must match native value.");
        TestAssert.Equal(0, (int)SDL3.SDL.NotificationPriority.Normal, "Notification normal priority must match native value.");
        TestAssert.Equal(1, (int)SDL3.SDL.NotificationPriority.High, "Notification high priority must match native value.");
        TestAssert.Equal(2, (int)SDL3.SDL.NotificationPriority.Critical, "Notification critical priority must match native value.");
        TestAssert.Equal(1, (int)SDL3.SDL.NotificationActionType.Button, "Notification button action type must match native value.");
        TestAssert.Equal(128, Marshal.SizeOf<SDL3.SDL.NotificationAction>(), "Notification action union must be 128 bytes.");
        TestAssert.Equal(0, Marshal.OffsetOf<SDL3.SDL.NotificationAction>(nameof(SDL3.SDL.NotificationAction.Button)).ToInt32(), "Notification button action must overlay union start.");
        TestAssert.Equal(IntPtr.Size == 8 ? 8 : 4, Marshal.OffsetOf<SDL3.SDL.NotificationButtonAction>(nameof(SDL3.SDL.NotificationButtonAction.ActionId)).ToInt32(), "Notification action ID pointer must preserve alignment.");
        TestAssert.Equal((IntPtr.Size == 8 ? 8 : 4) + IntPtr.Size, Marshal.OffsetOf<SDL3.SDL.NotificationButtonAction>(nameof(SDL3.SDL.NotificationButtonAction.ActionLabel)).ToInt32(), "Notification action label pointer must preserve layout.");
    }

    private static MethodInfo GetNativeMethod(string name) => typeof(SDL3.SDL).GetMethod(name, BindingFlags.NonPublic | BindingFlags.Static)!;

    private static void AssertImport(MethodInfo method, string entryPoint)
    {
        LibraryImportAttribute? import = method.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(import, $"SDL.{method.Name} must use LibraryImport.");
        TestAssert.Equal("SDL3", import!.LibraryName, $"SDL.{method.Name} must use SDL3 library.");
        TestAssert.Equal(entryPoint, import.EntryPoint, $"SDL.{method.Name} must bind the exact entry point.");
        UnmanagedCallConvAttribute? callConv = method.GetCustomAttribute<UnmanagedCallConvAttribute>();
        TestAssert.NotNull(callConv, $"SDL.{method.Name} must declare unmanaged calling convention.");
        TestAssert.Equal(typeof(CallConvCdecl), callConv!.CallConvs![0], $"SDL.{method.Name} must use cdecl.");
    }

    private static void AssertBoolReturn(MethodInfo method)
    {
        MarshalAsAttribute? marshal = method.ReturnParameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshal, $"SDL.{method.Name} must marshal bool as I1.");
        TestAssert.Equal(UnmanagedType.I1, marshal!.Value, $"SDL.{method.Name} must use one-byte bool ABI.");
    }

    private static void AssertUtf8(MethodInfo method, string name)
    {
        MarshalAsAttribute? marshal = method.GetParameters().Single(value => value.Name == name).GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshal, $"SDL.{method.Name} {name} must declare UTF-8 marshalling.");
        TestAssert.Equal(UnmanagedType.LPUTF8Str, marshal!.Value, $"SDL.{method.Name} {name} must use UTF-8.");
    }

    private static void AssertArray(MethodInfo method, string name, short sizeParamIndex)
    {
        MarshalAsAttribute? marshal = method.GetParameters().Single(value => value.Name == name).GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshal, $"SDL.{method.Name} {name} must declare array marshalling.");
        TestAssert.Equal(UnmanagedType.LPArray, marshal!.Value, $"SDL.{method.Name} {name} must marshal as an array.");
        TestAssert.Equal(sizeParamIndex, marshal.SizeParamIndex, $"SDL.{method.Name} {name} must use the action count.");
    }

    private static bool CaptureNoArgBool() { capturedCallCount++; return nextBool; }
    private static uint CaptureShowNotificationWithProperties(uint props) { capturedProperties = props; return capturedNotificationId; }
    private static uint CaptureShowNotification(string title, string? message, IntPtr image, SDL3.SDL.NotificationAction[]? actions, int count)
    {
        capturedCallCount++;
        capturedTitle = title;
        capturedMessage = message;
        capturedImage = image;
        capturedActions = actions;
        capturedActionCount = count;
        return capturedNotificationId;
    }
    private static bool CaptureRemoveNotification(uint notification) { capturedNotificationId = notification; return nextBool; }

    private sealed class NativeHookScope : IDisposable
    {
        private readonly FieldInfo field;
        private readonly object? original;
        private NativeHookScope(FieldInfo field, object? original) { this.field = field; this.original = original; }
        public static NativeHookScope Install(string fieldName, string methodName)
        {
            FieldInfo field = typeof(SDL3.SDL).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static)!;
            MethodInfo method = typeof(PInvokeTests).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static)!;
            object? original = field.GetValue(null);
            field.SetValue(null, Delegate.CreateDelegate(field.FieldType, method));
            return new NativeHookScope(field, original);
        }
        public void Dispose() => field.SetValue(null, original);
    }
}
