using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using SDL3.Tests;

namespace SDL3.Tests.SDL.AdditionalFunctionality.Guid;

internal static class PInvokeTests
{
    private const string SampleGuid = "030000005e0400008e02000000000000";

    public static void GUIDToString_WritesAsciiGuidString()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.GUIDToString), BindingFlags.Public | BindingFlags.Static, [typeof(SDL3.SDL.GUID), typeof(byte[]), typeof(int)]);
        TestAssert.NotNull(method, "SDL.GUIDToString method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_GUIDToString");
        AssertArrayParameterMarshal(method!, "pszGUID");

        SDL3.SDL.GUID guid = SDL3.SDL.StringToGUID(SampleGuid);
        byte[] buffer = new byte[33];
        SDL3.SDL.GUIDToString(guid, buffer, buffer.Length);
        string value = Encoding.ASCII.GetString(buffer).TrimEnd('\0');
        TestAssert.Equal(SampleGuid, value, "SDL.GUIDToString must write the canonical ASCII GUID string.");
    }

    public static void GUIDToStringSpan_WritesAsciiGuidString()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.GUIDToString), BindingFlags.Public | BindingFlags.Static, [typeof(SDL3.SDL.GUID), typeof(Span<byte>), typeof(int)]);
        TestAssert.NotNull(method, "SDL.GUIDToString span overload must be public static.");

        MethodInfo? nativeMethod = typeof(SDL3.SDL).GetMethod("SDL_GUIDToStringPointer", BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(nativeMethod, "SDL.SDL_GUIDToStringPointer method must be private static.");
        AssertSdlLibraryImport(nativeMethod!, "SDL_GUIDToString");

        SDL3.SDL.GUID guid = SDL3.SDL.StringToGUID(SampleGuid);
        Span<byte> buffer = stackalloc byte[33];
        SDL3.SDL.GUIDToString(guid, buffer, buffer.Length);
        string value = Encoding.ASCII.GetString(buffer).TrimEnd('\0');
        TestAssert.Equal(SampleGuid, value, "SDL.GUIDToString span overload must write the canonical ASCII GUID string.");
    }

    public static void StringToGUID_ParsesAsciiGuidString()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.StringToGUID), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.StringToGUID method must be public static.");
        AssertSdlDllImport(method!, "SDL_StringToGUID");
        AssertStringParameterMarshal(method!, "pchGUID");

        SDL3.SDL.GUID guid = SDL3.SDL.StringToGUID(SampleGuid);
        byte[] buffer = new byte[33];
        SDL3.SDL.GUIDToString(guid, buffer, buffer.Length);
        string value = Encoding.ASCII.GetString(buffer).TrimEnd('\0');
        TestAssert.Equal(SampleGuid, value, "SDL.StringToGUID must parse a GUID that round-trips through GUIDToString.");
    }

    private static void AssertSdlLibraryImport(MethodInfo method, string entryPoint)
    {
        LibraryImportAttribute? libraryImport = method.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, $"SDL.{method.Name} must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3", libraryImport!.LibraryName, $"SDL.{method.Name} must import from SDL3.");
        TestAssert.Equal(entryPoint, libraryImport.EntryPoint, $"SDL.{method.Name} must bind {entryPoint}.");
    }

    private static void AssertSdlDllImport(MethodInfo method, string entryPoint)
    {
        DllImportAttribute? dllImport = method.GetCustomAttribute<DllImportAttribute>();
        TestAssert.NotNull(dllImport, $"SDL.{method.Name} must keep DllImport metadata.");
        TestAssert.Equal("SDL3", dllImport!.Value, $"SDL.{method.Name} must import from SDL3.");
        TestAssert.Equal(entryPoint, dllImport.EntryPoint, $"SDL.{method.Name} must bind {entryPoint}.");
    }

    private static void AssertArrayParameterMarshal(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.LPArray, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use LPArray marshalling.");
        TestAssert.Equal(2, marshalAs.SizeParamIndex, $"SDL.{method.Name} parameter {parameterName} must size from cbGUID.");
    }

    private static void AssertStringParameterMarshal(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.LPUTF8Str, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use UTF-8 string marshalling.");
    }
}
