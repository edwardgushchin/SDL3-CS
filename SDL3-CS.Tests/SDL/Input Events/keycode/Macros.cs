using System.Reflection;
using SDL3.Tests;

namespace SDL3.Tests.SDL.InputEvents.Keycode;

internal static class MacroTests
{
    public static void ScancodeToKeycode_CombinesScancodeWithMaskAndKeepsMacroAttribute()
    {
        MethodInfo method = GetPublicMethod(nameof(SDL3.SDL.ScancodeToKeycode), [typeof(SDL3.SDL.Scancode)]);
        AssertMacro(method);
        TestAssert.Equal(typeof(SDL3.SDL.Keycode), method.ReturnType, "SDL.ScancodeToKeycode must return Keycode.");

        AssertScancodeMapping(SDL3.SDL.Scancode.A);
        AssertScancodeMapping(SDL3.SDL.Scancode.Return);
        AssertScancodeMapping(SDL3.SDL.Scancode.Unknown);
    }

    private static void AssertScancodeMapping(SDL3.SDL.Scancode scancode)
    {
        SDL3.SDL.Keycode expected = (SDL3.SDL.Keycode)((uint)scancode | (uint)SDL3.SDL.Keycode.ScanCodeMask);
        SDL3.SDL.Keycode actual = SDL3.SDL.ScancodeToKeycode(scancode);

        TestAssert.Equal(expected, actual, $"SDL.ScancodeToKeycode must OR {scancode} with ScanCodeMask.");
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
