using System.Reflection;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Video.Video;

internal static class MacroTests
{
    public static void RunAll()
    {
        WindowPosUndefinedDisplay_PacksDisplayIdAndKeepsMacroAttribute();
        WindowPosUndefined_UsesPrimaryDisplayAndKeepsMacroAttribute();
        WindowPosIsUndefined_ChecksOnlyUndefinedMaskAndKeepsMacroAttribute();
        WindowPosCenteredDisplay_PacksDisplayIdAndKeepsMacroAttribute();
        WindowPosCentered_UsesPrimaryDisplayAndKeepsMacroAttribute();
        WindowPosIsCentered_ChecksOnlyCenteredMaskAndKeepsMacroAttribute();
    }

    public static void WindowPosUndefinedDisplay_PacksDisplayIdAndKeepsMacroAttribute()
    {
        TestAssert.Equal(
            SDL3.SDL.WindowPosUndefinedMask | 7u,
            SDL3.SDL.WindowPosUndefinedDisplay(7),
            "SDL.WindowPosUndefinedDisplay must combine the undefined mask with the display id.");
        TestAssert.Equal(
            uint.MaxValue,
            SDL3.SDL.WindowPosUndefinedDisplay(-1),
            "SDL.WindowPosUndefinedDisplay must preserve SDL macro casting semantics for negative display ids.");

        AssertMacroAttribute(nameof(SDL3.SDL.WindowPosUndefinedDisplay), [typeof(int)]);
    }

    public static void WindowPosUndefined_UsesPrimaryDisplayAndKeepsMacroAttribute()
    {
        TestAssert.Equal(
            SDL3.SDL.WindowPosUndefinedDisplay(0),
            SDL3.SDL.WindowPosUndefined(),
            "SDL.WindowPosUndefined must use the primary display.");

        AssertMacroAttribute(nameof(SDL3.SDL.WindowPosUndefined), Type.EmptyTypes);
    }

    public static void WindowPosIsUndefined_ChecksOnlyUndefinedMaskAndKeepsMacroAttribute()
    {
        TestAssert.Equal(
            true,
            SDL3.SDL.WindowPosIsUndefined(SDL3.SDL.WindowPosUndefinedDisplay(9)),
            "SDL.WindowPosIsUndefined must accept undefined positions with a display id.");
        TestAssert.Equal(
            true,
            SDL3.SDL.WindowPosIsUndefined(SDL3.SDL.WindowPosUndefinedMask | 0xFFFFu),
            "SDL.WindowPosIsUndefined must ignore lower display-id bits.");
        TestAssert.Equal(
            false,
            SDL3.SDL.WindowPosIsUndefined(SDL3.SDL.WindowPosCenteredDisplay(9)),
            "SDL.WindowPosIsUndefined must reject centered positions.");
        TestAssert.Equal(
            false,
            SDL3.SDL.WindowPosIsUndefined(9u),
            "SDL.WindowPosIsUndefined must reject ordinary coordinates.");

        AssertMacroAttribute(nameof(SDL3.SDL.WindowPosIsUndefined), [typeof(uint)]);
    }

    public static void WindowPosCenteredDisplay_PacksDisplayIdAndKeepsMacroAttribute()
    {
        TestAssert.Equal(
            SDL3.SDL.WindowPosCenteredMask | 11u,
            SDL3.SDL.WindowPosCenteredDisplay(11),
            "SDL.WindowPosCenteredDisplay must combine the centered mask with the display id.");
        TestAssert.Equal(
            uint.MaxValue,
            SDL3.SDL.WindowPosCenteredDisplay(-1),
            "SDL.WindowPosCenteredDisplay must preserve SDL macro casting semantics for negative display ids.");

        AssertMacroAttribute(nameof(SDL3.SDL.WindowPosCenteredDisplay), [typeof(int)]);
    }

    public static void WindowPosCentered_UsesPrimaryDisplayAndKeepsMacroAttribute()
    {
        TestAssert.Equal(
            SDL3.SDL.WindowPosCenteredDisplay(0),
            SDL3.SDL.WindowPosCentered(),
            "SDL.WindowPosCentered must use the primary display.");

        AssertMacroAttribute(nameof(SDL3.SDL.WindowPosCentered), Type.EmptyTypes);
    }

    public static void WindowPosIsCentered_ChecksOnlyCenteredMaskAndKeepsMacroAttribute()
    {
        TestAssert.Equal(
            true,
            SDL3.SDL.WindowPosIsCentered(SDL3.SDL.WindowPosCenteredDisplay(13)),
            "SDL.WindowPosIsCentered must accept centered positions with a display id.");
        TestAssert.Equal(
            true,
            SDL3.SDL.WindowPosIsCentered(SDL3.SDL.WindowPosCenteredMask | 0xFFFFu),
            "SDL.WindowPosIsCentered must ignore lower display-id bits.");
        TestAssert.Equal(
            false,
            SDL3.SDL.WindowPosIsCentered(SDL3.SDL.WindowPosUndefinedDisplay(13)),
            "SDL.WindowPosIsCentered must reject undefined positions.");
        TestAssert.Equal(
            false,
            SDL3.SDL.WindowPosIsCentered(13u),
            "SDL.WindowPosIsCentered must reject ordinary coordinates.");

        AssertMacroAttribute(nameof(SDL3.SDL.WindowPosIsCentered), [typeof(uint)]);
    }

    private static void AssertMacroAttribute(string methodName, Type[] parameterTypes)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, parameterTypes);

        TestAssert.NotNull(method, $"SDL.{methodName} must be public static.");
        TestAssert.NotNull(method!.GetCustomAttribute<SDL3.SDL.MacroAttribute>(), $"SDL.{methodName} must keep MacroAttribute.");
    }
}
