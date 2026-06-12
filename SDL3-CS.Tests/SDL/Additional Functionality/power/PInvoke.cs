using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.AdditionalFunctionality.Power;

internal static class PInvokeTests
{
    public static void GetPowerInfo_ReturnsStateAndOutputValues()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.GetPowerInfo), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.GetPowerInfo method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_GetPowerInfo");

        SDL3.SDL.PowerState state = SDL3.SDL.GetPowerInfo(out int seconds, out int percent);

        TestAssert.True(Enum.IsDefined(state), "SDL.GetPowerInfo must return a defined PowerState value.");
        TestAssert.True(seconds >= -1, "SDL.GetPowerInfo seconds output must be -1 or a non-negative value.");
        TestAssert.True(percent >= -1, "SDL.GetPowerInfo percent output must be -1 or a non-negative value.");
        TestAssert.True(percent <= 100, "SDL.GetPowerInfo percent output must not exceed a percentage value.");
    }

    private static void AssertSdlLibraryImport(MethodInfo method, string entryPoint)
    {
        LibraryImportAttribute? libraryImport = method.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, $"SDL.{method.Name} must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3", libraryImport!.LibraryName, $"SDL.{method.Name} must import from SDL3.");
        TestAssert.Equal(entryPoint, libraryImport.EntryPoint, $"SDL.{method.Name} must bind {entryPoint}.");
    }
}
