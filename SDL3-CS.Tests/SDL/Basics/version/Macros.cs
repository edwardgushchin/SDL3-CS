using System.Reflection;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Basics.Version;

internal static class MacroTests
{
    public static void VersionNum_ComposesNumericVersionAndKeepsMacroAttribute()
    {
        MethodInfo method = GetPublicMethod(nameof(SDL3.SDL.VersionNum), [typeof(int), typeof(int), typeof(int)]);
        AssertMacro(method);
        TestAssert.Equal(typeof(int), method.ReturnType, "SDL.VersionNum must return int.");

        TestAssert.Equal(1002003, SDL3.SDL.VersionNum(1, 2, 3), "SDL.VersionNum must compose 1.2.3 as documented.");
        TestAssert.Equal(0, SDL3.SDL.VersionNum(0, 0, 0), "SDL.VersionNum must preserve zero components.");
        TestAssert.Equal(3033007, SDL3.SDL.VersionNum(3, 33, 7), "SDL.VersionNum must use SDL major/minor/patch multipliers.");
    }

    public static void VersionNumMajor_ExtractsMajorAndKeepsMacroAttribute()
    {
        MethodInfo method = GetPublicMethod(nameof(SDL3.SDL.VersionNumMajor), [typeof(int)]);
        AssertMacro(method);
        TestAssert.Equal(typeof(int), method.ReturnType, "SDL.VersionNumMajor must return int.");

        TestAssert.Equal(1, SDL3.SDL.VersionNumMajor(1002003), "SDL.VersionNumMajor must extract the documented major component.");
        TestAssert.Equal(0, SDL3.SDL.VersionNumMajor(999999), "SDL.VersionNumMajor must return zero when the major field is absent.");
        TestAssert.Equal(12, SDL3.SDL.VersionNumMajor(SDL3.SDL.VersionNum(12, 34, 56)), "SDL.VersionNumMajor must extract a composed major component.");
    }

    public static void VersionNumMinor_ExtractsMinorAndKeepsMacroAttribute()
    {
        MethodInfo method = GetPublicMethod(nameof(SDL3.SDL.VersionNumMinor), [typeof(int)]);
        AssertMacro(method);
        TestAssert.Equal(typeof(int), method.ReturnType, "SDL.VersionNumMinor must return int.");

        TestAssert.Equal(2, SDL3.SDL.VersionNumMinor(1002003), "SDL.VersionNumMinor must extract the documented minor component.");
        TestAssert.Equal(999, SDL3.SDL.VersionNumMinor(SDL3.SDL.VersionNum(3, 999, 4)), "SDL.VersionNumMinor must preserve the full three-digit minor component.");
        TestAssert.Equal(0, SDL3.SDL.VersionNumMinor(1000003), "SDL.VersionNumMinor must return zero when the minor field is absent.");
    }

    public static void VersionNumMicro_ExtractsMicroAndKeepsMacroAttribute()
    {
        MethodInfo method = GetPublicMethod(nameof(SDL3.SDL.VersionNumMicro), [typeof(int)]);
        AssertMacro(method);
        TestAssert.Equal(typeof(int), method.ReturnType, "SDL.VersionNumMicro must return int.");

        TestAssert.Equal(3, SDL3.SDL.VersionNumMicro(1002003), "SDL.VersionNumMicro must extract the documented micro component.");
        TestAssert.Equal(999, SDL3.SDL.VersionNumMicro(SDL3.SDL.VersionNum(3, 4, 999)), "SDL.VersionNumMicro must preserve the full three-digit micro component.");
        TestAssert.Equal(0, SDL3.SDL.VersionNumMicro(1002000), "SDL.VersionNumMicro must return zero when the micro field is absent.");
    }

    private static MethodInfo GetPublicMethod(string methodName, Type[] parameterTypes)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, parameterTypes);
        TestAssert.NotNull(method, $"SDL.{methodName} public static method must exist.");
        return method!;
    }

    private static void AssertMacro(MethodInfo method)
    {
        SDL3.SDL.MacroAttribute? macro = method.GetCustomAttribute<SDL3.SDL.MacroAttribute>();
        TestAssert.NotNull(macro, $"SDL.{method.Name} must keep MacroAttribute.");
    }
}
