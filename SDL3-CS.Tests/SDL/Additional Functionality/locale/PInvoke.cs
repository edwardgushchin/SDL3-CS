using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.AdditionalFunctionality.Locale;

internal static class PInvokeTests
{
    private static IntPtr localeArray;
    private static IntPtr localeStruct;
    private static IntPtr language;
    private static IntPtr country;

    public static void SDL_GetPreferredLocales_ReturnsNativeLocaleArray()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod("SDL_GetPreferredLocales", BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.SDL_GetPreferredLocales method must be private static.");
        AssertSdlLibraryImport(method!, "SDL_GetPreferredLocales");

        object?[] args = [0];
        IntPtr locales = (IntPtr)method!.Invoke(null, args)!;

        try
        {
            int count = (int)args[0]!;
            TestAssert.True(count >= 0, "SDL.SDL_GetPreferredLocales must report a non-negative locale count.");
        }
        finally
        {
            SDL3.SDL.Free(locales);
        }
    }

    public static void GetPreferredLocales_ConvertsNativeLocalesAndFreesArray()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.GetPreferredLocales), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.GetPreferredLocales method must be public static.");

        using NativeHookScope _ = NativeHookScope.Install("GetPreferredLocalesNativeFunction", nameof(CreateFakePreferredLocales));

        try
        {
            SDL3.SDL.Locale[]? locales = SDL3.SDL.GetPreferredLocales(out int count);
            TestAssert.NotNull(locales, "SDL.GetPreferredLocales must convert a non-null native locale array.");
            TestAssert.Equal(1, count, "SDL.GetPreferredLocales must return the native locale count.");
            TestAssert.Equal(1, locales!.Length, "SDL.GetPreferredLocales must return a managed locale for each native locale pointer.");
            TestAssert.Equal("en", locales[0].Language, "SDL.GetPreferredLocales must convert the language string.");
            TestAssert.Equal("US", locales[0].Country, "SDL.GetPreferredLocales must convert the country string.");
        }
        finally
        {
            FreeFakeLocaleParts();
        }
    }

    private static IntPtr CreateFakePreferredLocales(out int count)
    {
        count = 1;
        language = Marshal.StringToCoTaskMemUTF8("en");
        country = Marshal.StringToCoTaskMemUTF8("US");
        localeStruct = Marshal.AllocHGlobal(IntPtr.Size * 2);
        Marshal.WriteIntPtr(localeStruct, 0, language);
        Marshal.WriteIntPtr(localeStruct, IntPtr.Size, country);
        localeArray = SDL3.SDL.Malloc((UIntPtr)IntPtr.Size);
        Marshal.WriteIntPtr(localeArray, localeStruct);
        return localeArray;
    }

    private static void FreeFakeLocaleParts()
    {
        if (localeStruct != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(localeStruct);
            localeStruct = IntPtr.Zero;
        }

        if (language != IntPtr.Zero)
        {
            Marshal.FreeCoTaskMem(language);
            language = IntPtr.Zero;
        }

        if (country != IntPtr.Zero)
        {
            Marshal.FreeCoTaskMem(country);
            country = IntPtr.Zero;
        }

        localeArray = IntPtr.Zero;
    }

    private static void AssertSdlLibraryImport(MethodInfo method, string entryPoint)
    {
        LibraryImportAttribute? libraryImport = method.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, $"SDL.{method.Name} must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3", libraryImport!.LibraryName, $"SDL.{method.Name} must import from SDL3.");
        TestAssert.Equal(entryPoint, libraryImport.EntryPoint, $"SDL.{method.Name} must bind {entryPoint}.");
    }

    private sealed class NativeHookScope : IDisposable
    {
        private readonly FieldInfo field;
        private readonly object? originalValue;

        private NativeHookScope(FieldInfo field, object? originalValue)
        {
            this.field = field;
            this.originalValue = originalValue;
        }

        public static NativeHookScope Install(string fieldName, string methodName)
        {
            FieldInfo? field = typeof(SDL3.SDL).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
            TestAssert.NotNull(field, $"SDL.{fieldName} native hook field must exist.");
            MethodInfo? method = typeof(PInvokeTests).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
            TestAssert.NotNull(method, $"{methodName} hook method must exist.");
            object? originalValue = field!.GetValue(null);
            Delegate hook = Delegate.CreateDelegate(field.FieldType, method!);
            field.SetValue(null, hook);
            return new NativeHookScope(field, originalValue);
        }

        public void Dispose()
        {
            field.SetValue(null, originalValue);
        }
    }
}
