using System.Reflection;
using SDL3.Tests;

namespace SDL3.Tests.SDL.InputEvents.Mouse;

internal static class MacroTests
{
    public static void ButtonMask_ShiftsButtonNumberAndKeepsMacroAttribute()
    {
        MethodInfo method = GetPublicMethod(nameof(SDL3.SDL.ButtonMask), [typeof(int)]);
        AssertMacro(method);
        TestAssert.Equal(typeof(uint), method.ReturnType, "SDL.ButtonMask must return uint.");

        TestAssert.Equal(1u, SDL3.SDL.ButtonMask(SDL3.SDL.ButtonLeft), "SDL.ButtonMask must map button 1 to bit 0.");
        TestAssert.Equal(2u, SDL3.SDL.ButtonMask(SDL3.SDL.ButtonMiddle), "SDL.ButtonMask must map button 2 to bit 1.");
        TestAssert.Equal(4u, SDL3.SDL.ButtonMask(SDL3.SDL.ButtonRight), "SDL.ButtonMask must map button 3 to bit 2.");
        TestAssert.Equal(8u, SDL3.SDL.ButtonMask(SDL3.SDL.ButtonX1), "SDL.ButtonMask must map button 4 to bit 3.");
        TestAssert.Equal(16u, SDL3.SDL.ButtonMask(SDL3.SDL.ButtonX2), "SDL.ButtonMask must map button 5 to bit 4.");
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
