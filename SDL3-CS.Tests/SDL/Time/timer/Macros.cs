using System.Reflection;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Time.Timer;

internal static class MacroTests
{
    public static void RunAll()
    {
        SecondsToNs_ConvertsWholeSecondsAndKeepsMacroAttribute();
        NsToSeconds_DividesNanosecondsAndKeepsMacroAttribute();
        MsToNs_ConvertsWholeMillisecondsAndKeepsMacroAttribute();
        NsToMs_DividesNanosecondsAndKeepsMacroAttribute();
        UsToNs_ConvertsWholeMicrosecondsAndKeepsMacroAttribute();
        NsToUs_DividesNanosecondsAndKeepsMacroAttribute();
    }

    public static void SecondsToNs_ConvertsWholeSecondsAndKeepsMacroAttribute()
    {
        TestAssert.Equal(2_000_000_000ul, SDL3.SDL.SecondsToNs(2), "SDL.SecondsToNs must multiply by nanoseconds per second.");
        AssertMacroAttribute(nameof(SDL3.SDL.SecondsToNs));
    }

    public static void NsToSeconds_DividesNanosecondsAndKeepsMacroAttribute()
    {
        TestAssert.Equal(2ul, SDL3.SDL.NsToSeconds(2_999_999_999), "SDL.NsToSeconds must use integer division.");
        AssertMacroAttribute(nameof(SDL3.SDL.NsToSeconds));
    }

    public static void MsToNs_ConvertsWholeMillisecondsAndKeepsMacroAttribute()
    {
        TestAssert.Equal(3_000_000ul, SDL3.SDL.MsToNs(3), "SDL.MsToNs must multiply by nanoseconds per millisecond.");
        AssertMacroAttribute(nameof(SDL3.SDL.MsToNs));
    }

    public static void NsToMs_DividesNanosecondsAndKeepsMacroAttribute()
    {
        TestAssert.Equal(3ul, SDL3.SDL.NsToMs(3_999_999), "SDL.NsToMs must use integer division.");
        AssertMacroAttribute(nameof(SDL3.SDL.NsToMs));
    }

    public static void UsToNs_ConvertsWholeMicrosecondsAndKeepsMacroAttribute()
    {
        TestAssert.Equal(4_000ul, SDL3.SDL.UsToNs(4), "SDL.UsToNs must multiply by nanoseconds per microsecond.");
        AssertMacroAttribute(nameof(SDL3.SDL.UsToNs));
    }

    public static void NsToUs_DividesNanosecondsAndKeepsMacroAttribute()
    {
        TestAssert.Equal(4ul, SDL3.SDL.NsToUs(4_999), "SDL.NsToUs must use integer division.");
        AssertMacroAttribute(nameof(SDL3.SDL.NsToUs));
    }

    private static void AssertMacroAttribute(string methodName)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);

        TestAssert.NotNull(method, $"SDL.{methodName} must exist.");
        TestAssert.True(method!.GetCustomAttribute<SDL3.SDL.MacroAttribute>() is not null, $"SDL.{methodName} must keep MacroAttribute.");
    }
}
