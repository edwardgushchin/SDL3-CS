using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.AdditionalFunctionality.Loadso;

internal static class PInvokeTests
{
    public static void LoadObject_LoadsSdlLibraryAndReturnsNullForMissingFile()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.LoadObject), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.LoadObject method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_LoadObject");
        AssertStringParameterMarshal(method!, "sofile");

        IntPtr missing = SDL3.SDL.LoadObject(Path.Combine(AppContext.BaseDirectory, "missing-sdl3-library.dll"));
        TestAssert.Equal(IntPtr.Zero, missing, "SDL.LoadObject must return IntPtr.Zero for a missing shared object.");

        IntPtr handle = SDL3.SDL.LoadObject(GetSdl3NativePath());
        try
        {
            TestAssert.True(handle != IntPtr.Zero, "SDL.LoadObject must load SDL3.dll from the test output directory.");
        }
        finally
        {
            UnloadObjectIfNeeded(handle);
        }
    }

    public static void LoadFunction_ReturnsKnownSymbolAndNullForMissingSymbol()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.LoadFunction), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.LoadFunction method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_LoadFunction");
        AssertStringParameterMarshal(method!, "name");

        IntPtr handle = SDL3.SDL.LoadObject(GetSdl3NativePath());
        try
        {
            TestAssert.True(handle != IntPtr.Zero, "SDL.LoadObject must load SDL3.dll before LoadFunction.");
            IntPtr version = SDL3.SDL.LoadFunction(handle, "SDL_GetVersion");
            IntPtr missing = SDL3.SDL.LoadFunction(handle, "SDL_Missing_Function_For_Tests");
            TestAssert.True(version != IntPtr.Zero, "SDL.LoadFunction must resolve SDL_GetVersion from SDL3.dll.");
            TestAssert.Equal(IntPtr.Zero, missing, "SDL.LoadFunction must return IntPtr.Zero for a missing symbol.");
        }
        finally
        {
            UnloadObjectIfNeeded(handle);
        }
    }

    public static void UnloadObject_UnloadsLoadedLibrary()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.UnloadObject), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.UnloadObject method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_UnloadObject");

        IntPtr handle = SDL3.SDL.LoadObject(GetSdl3NativePath());
        TestAssert.True(handle != IntPtr.Zero, "SDL.LoadObject must load SDL3.dll before UnloadObject.");
        SDL3.SDL.UnloadObject(handle);
    }

    private static string GetSdl3NativePath()
    {
        string path = Path.Combine(AppContext.BaseDirectory, "SDL3.dll");
        TestAssert.True(File.Exists(path), "SDL3.dll must be copied to the test output directory.");
        return path;
    }

    private static void UnloadObjectIfNeeded(IntPtr handle)
    {
        if (handle != IntPtr.Zero)
        {
            SDL3.SDL.UnloadObject(handle);
        }
    }

    private static void AssertSdlLibraryImport(MethodInfo method, string entryPoint)
    {
        LibraryImportAttribute? libraryImport = method.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, $"SDL.{method.Name} must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3", libraryImport!.LibraryName, $"SDL.{method.Name} must import from SDL3.");
        TestAssert.Equal(entryPoint, libraryImport.EntryPoint, $"SDL.{method.Name} must bind {entryPoint}.");
    }

    private static void AssertStringParameterMarshal(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.LPUTF8Str, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use UTF-8 string marshalling.");
    }
}
