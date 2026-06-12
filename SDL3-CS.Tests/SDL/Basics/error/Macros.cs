using System.Reflection;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Basics.Error;

internal static class MacroTests
{
    public static void Unsupported_SetsStandardErrorAndReturnsFalse()
    {
        MethodInfo method = GetPublicMethod(nameof(SDL3.SDL.Unsupported), Type.EmptyTypes);
        AssertMacro(method);
        TestAssert.Equal(typeof(bool), method.ReturnType, "SDL.Unsupported must return bool.");

        SDL3.SDL.ClearError();
        bool result = SDL3.SDL.Unsupported();
        string error = SDL3.SDL.GetError();
        SDL3.SDL.ClearError();

        TestAssert.Equal(false, result, "SDL.Unsupported must return the SDL_SetError false result.");
        TestAssert.Equal("That operation is not supported", error, "SDL.Unsupported must set the standardized unsupported error.");
    }

    public static void InvalidParamError_SetsParameterErrorAndReturnsFalse()
    {
        MethodInfo method = GetPublicMethod(nameof(SDL3.SDL.InvalidParamError), [typeof(string)]);
        AssertMacro(method);
        TestAssert.Equal(typeof(bool), method.ReturnType, "SDL.InvalidParamError must return bool.");

        SDL3.SDL.ClearError();
        bool result = SDL3.SDL.InvalidParamError("window");
        string error = SDL3.SDL.GetError();

        bool emptyResult = SDL3.SDL.InvalidParamError("");
        string emptyError = SDL3.SDL.GetError();
        SDL3.SDL.ClearError();

        TestAssert.Equal(false, result, "SDL.InvalidParamError must return the SDL_SetError false result.");
        TestAssert.Equal("Parameter 'window' is invalid", error, "SDL.InvalidParamError must include the parameter name.");
        TestAssert.Equal(false, emptyResult, "SDL.InvalidParamError must return false for an empty parameter name.");
        TestAssert.Equal("Parameter '' is invalid", emptyError, "SDL.InvalidParamError must preserve the empty parameter name format.");
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
