using System.Reflection;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Video.Surface;

internal static class MacroTests
{
    public static void RunAll()
    {
        MustLock_ChecksLockNeededFlagAndKeepsMacroAttribute();
    }

    public static void MustLock_ChecksLockNeededFlagAndKeepsMacroAttribute()
    {
        SDL3.SDL.Surface defaultSurface = default;
        SDL3.SDL.Surface preallocatedSurface = new() { Flags = SDL3.SDL.SurfaceFlags.Preallocated };
        SDL3.SDL.Surface lockNeededSurface = new() { Flags = SDL3.SDL.SurfaceFlags.LockNeeded };
        SDL3.SDL.Surface lockedOnlySurface = new() { Flags = SDL3.SDL.SurfaceFlags.Locked };
        SDL3.SDL.Surface combinedSurface = new()
        {
            Flags = SDL3.SDL.SurfaceFlags.Preallocated | SDL3.SDL.SurfaceFlags.LockNeeded | SDL3.SDL.SurfaceFlags.Locked
        };

        TestAssert.Equal(false, SDL3.SDL.MustLock(defaultSurface), "SDL.MustLock must reject a default surface.");
        TestAssert.Equal(false, SDL3.SDL.MustLock(preallocatedSurface), "SDL.MustLock must reject surfaces without LockNeeded.");
        TestAssert.Equal(true, SDL3.SDL.MustLock(lockNeededSurface), "SDL.MustLock must accept LockNeeded.");
        TestAssert.Equal(false, SDL3.SDL.MustLock(lockedOnlySurface), "SDL.MustLock must not treat Locked as LockNeeded.");
        TestAssert.Equal(true, SDL3.SDL.MustLock(combinedSurface), "SDL.MustLock must accept LockNeeded when combined with other flags.");

        AssertMacroAttribute(nameof(SDL3.SDL.MustLock), [typeof(SDL3.SDL.Surface)]);
    }

    private static void AssertMacroAttribute(string methodName, Type[] parameterTypes)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, parameterTypes);
        TestAssert.NotNull(method, $"SDL.{methodName} must be public static.");
        TestAssert.NotNull(method!.GetCustomAttribute<SDL3.SDL.MacroAttribute>(), $"SDL.{methodName} must keep MacroAttribute.");
    }
}
