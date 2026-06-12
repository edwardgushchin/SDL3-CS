using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using SDL3.Tests;

namespace SDL3.Tests.SDL.AdditionalFunctionality.Process;

internal static class PInvokeTests
{
    public static void CreateProcess_StartsShortProcess()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.CreateProcess), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.CreateProcess method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_CreateProcess");
        AssertStringArrayParameterMarshal(method!, "args");
        AssertBooleanParameterMarshal(method!, "pipeStdio");

        IntPtr process = SDL3.SDL.CreateProcess(CreateExitArgs(), pipeStdio: false);
        TestAssert.True(process != IntPtr.Zero, "SDL.CreateProcess must start a short command process.");

        try
        {
            bool exited = SDL3.SDL.WaitProcess(process, block: true, out int exitcode);
            TestAssert.Equal(true, exited, "SDL.WaitProcess must report the short process exit.");
            TestAssert.Equal(0, exitcode, "The short command process must exit successfully.");
        }
        finally
        {
            SDL3.SDL.DestroyProcess(process);
        }
    }

    public static void CreateProcessWithProperties_StartsShortProcess()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.CreateProcessWithProperties), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.CreateProcessWithProperties method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_CreateProcessWithProperties");

        const string marker = "SDL3_CS_PROCESS_PROPERTIES";
        using PropertiesScope properties = new();
        using Utf8PointerArray args = new(CreateEchoArgs(marker));

        TestAssert.True(SDL3.SDL.SetPointerProperty(properties.Handle, SDL3.SDL.Props.ProcessCreateArgsPointer, args.Pointer), "SDL.SetPointerProperty must set process args.");
        TestAssert.True(SDL3.SDL.SetNumberProperty(properties.Handle, SDL3.SDL.Props.ProcessCreateSTDOutNumber, (long)SDL3.SDL.ProcessIO.App), "SDL.SetNumberProperty must request stdout app pipe.");

        IntPtr process = SDL3.SDL.CreateProcessWithProperties(properties.Handle);
        TestAssert.True(process != IntPtr.Zero, "SDL.CreateProcessWithProperties must start a short command process.");

        IntPtr output = IntPtr.Zero;
        try
        {
            output = SDL3.SDL.ReadProcess(process, out UIntPtr datasize, out int exitcode);
            TestAssert.True(output != IntPtr.Zero, "SDL.ReadProcess must return captured output for the properties-created process.");
            string text = ReadUtf8(output, datasize);
            TestAssert.True(text.Contains(marker, StringComparison.Ordinal), "SDL.CreateProcessWithProperties must run the configured command args.");
            TestAssert.Equal(0, exitcode, "The properties-created command process must exit successfully.");
        }
        finally
        {
            FreeIfNeeded(output);
            SDL3.SDL.DestroyProcess(process);
        }
    }

    public static void GetProcessProperties_ReturnsPropertiesForProcess()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.GetProcessProperties), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.GetProcessProperties method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_GetProcessProperties");

        IntPtr process = SDL3.SDL.CreateProcess(CreateExitArgs(), pipeStdio: false);
        TestAssert.True(process != IntPtr.Zero, "SDL.CreateProcess must start a process for property queries.");

        try
        {
            uint properties = SDL3.SDL.GetProcessProperties(process);
            TestAssert.True(properties != 0, "SDL.GetProcessProperties must return a property set for a valid process.");
            long pid = SDL3.SDL.GetNumberProperty(properties, SDL3.SDL.Props.ProcessPIDNumber, defaultValue: -1);
            TestAssert.True(pid > 0, "SDL.GetProcessProperties must expose a positive process id.");
            bool exited = SDL3.SDL.WaitProcess(process, block: true, out int exitcode);
            TestAssert.Equal(true, exited, "SDL.WaitProcess must report the property-query process exit.");
            TestAssert.Equal(0, exitcode, "The property-query command process must exit successfully.");
        }
        finally
        {
            SDL3.SDL.DestroyProcess(process);
        }
    }

    public static void ReadProcess_ReturnsOutputAndExitCode()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.ReadProcess), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.ReadProcess method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_ReadProcess");

        const string marker = "SDL3_CS_READ_PROCESS";
        IntPtr process = SDL3.SDL.CreateProcess(CreateEchoArgs(marker), pipeStdio: true);
        TestAssert.True(process != IntPtr.Zero, "SDL.CreateProcess must start a piped process for reading.");

        IntPtr output = IntPtr.Zero;
        try
        {
            output = SDL3.SDL.ReadProcess(process, out UIntPtr datasize, out int exitcode);
            TestAssert.True(output != IntPtr.Zero, "SDL.ReadProcess must return captured process output.");
            string text = ReadUtf8(output, datasize);
            TestAssert.True(text.Contains(marker, StringComparison.Ordinal), "SDL.ReadProcess must capture stdout bytes.");
            TestAssert.Equal(0, exitcode, "SDL.ReadProcess must return the process exit code.");
        }
        finally
        {
            FreeIfNeeded(output);
            SDL3.SDL.DestroyProcess(process);
        }
    }

    public static void GetProcessInput_ReturnsStreamForPipedProcess()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.GetProcessInput), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.GetProcessInput method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_GetProcessInput");

        IntPtr process = SDL3.SDL.CreateProcess(CreateEchoArgs("SDL3_CS_PROCESS_INPUT"), pipeStdio: true);
        TestAssert.True(process != IntPtr.Zero, "SDL.CreateProcess must start a piped process for input stream queries.");

        IntPtr output = IntPtr.Zero;
        try
        {
            IntPtr input = SDL3.SDL.GetProcessInput(process);
            TestAssert.True(input != IntPtr.Zero, "SDL.GetProcessInput must return an input stream for a piped process.");
            output = SDL3.SDL.ReadProcess(process, out _, out int exitcode);
            TestAssert.True(output != IntPtr.Zero, "SDL.ReadProcess must complete the input-stream process.");
            TestAssert.Equal(0, exitcode, "The input-stream command process must exit successfully.");
        }
        finally
        {
            FreeIfNeeded(output);
            SDL3.SDL.DestroyProcess(process);
        }
    }

    public static void GetProcessOutput_ReturnsStreamForPipedProcess()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.GetProcessOutput), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.GetProcessOutput method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_GetProcessOutput");

        IntPtr process = SDL3.SDL.CreateProcess(CreateEchoArgs("SDL3_CS_PROCESS_OUTPUT"), pipeStdio: true);
        TestAssert.True(process != IntPtr.Zero, "SDL.CreateProcess must start a piped process for output stream queries.");

        IntPtr output = IntPtr.Zero;
        try
        {
            IntPtr stream = SDL3.SDL.GetProcessOutput(process);
            TestAssert.True(stream != IntPtr.Zero, "SDL.GetProcessOutput must return an output stream for a piped process.");
            output = SDL3.SDL.ReadProcess(process, out _, out int exitcode);
            TestAssert.True(output != IntPtr.Zero, "SDL.ReadProcess must complete the output-stream process.");
            TestAssert.Equal(0, exitcode, "The output-stream command process must exit successfully.");
        }
        finally
        {
            FreeIfNeeded(output);
            SDL3.SDL.DestroyProcess(process);
        }
    }

    public static void KillProcess_StopsRunningProcess()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.KillProcess), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.KillProcess method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_KillProcess");
        AssertBoolReturnMarshal(method!);
        AssertBooleanParameterMarshal(method!, "force");

        IntPtr process = SDL3.SDL.CreateProcess(CreateLongRunningArgs(), pipeStdio: false);
        TestAssert.True(process != IntPtr.Zero, "SDL.CreateProcess must start a long-running command process.");

        try
        {
            bool killed = SDL3.SDL.KillProcess(process, force: true);
            TestAssert.Equal(true, killed, "SDL.KillProcess must stop a running process.");
            bool exited = SDL3.SDL.WaitProcess(process, block: true, out _);
            TestAssert.Equal(true, exited, "SDL.WaitProcess must report the killed process exit.");
        }
        finally
        {
            SDL3.SDL.DestroyProcess(process);
        }
    }

    public static void WaitProcess_ReportsShortProcessExit()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.WaitProcess), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.WaitProcess method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_WaitProcess");
        AssertBoolReturnMarshal(method!);
        AssertBooleanParameterMarshal(method!, "block");

        IntPtr process = SDL3.SDL.CreateProcess(CreateExitArgs(), pipeStdio: false);
        TestAssert.True(process != IntPtr.Zero, "SDL.CreateProcess must start a process for waiting.");

        try
        {
            bool exited = SDL3.SDL.WaitProcess(process, block: true, out int exitcode);
            TestAssert.Equal(true, exited, "SDL.WaitProcess must report a completed process.");
            TestAssert.Equal(0, exitcode, "SDL.WaitProcess must return the successful process exit code.");
        }
        finally
        {
            SDL3.SDL.DestroyProcess(process);
        }
    }

    public static void DestroyProcess_DestroysProcessHandle()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.DestroyProcess), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.DestroyProcess method must be public static.");
        AssertSdlLibraryImport(method!, "SDL_DestroyProcess");

        IntPtr process = SDL3.SDL.CreateProcess(CreateExitArgs(), pipeStdio: false);
        TestAssert.True(process != IntPtr.Zero, "SDL.CreateProcess must start a process for destruction.");
        bool exited = SDL3.SDL.WaitProcess(process, block: true, out int exitcode);
        TestAssert.Equal(true, exited, "SDL.WaitProcess must report the destroy-test process exit.");
        TestAssert.Equal(0, exitcode, "The destroy-test command process must exit successfully.");

        SDL3.SDL.DestroyProcess(process);
    }

    [ExcludeFromCodeCoverage]
    private static string[] CreateEchoArgs(string marker)
    {
        if (OperatingSystem.IsWindows())
        {
            return [GetWindowsCommandProcessor(), "/c", "echo " + marker, null!];
        }

        return ["/bin/sh", "-c", "printf " + marker, null!];
    }

    [ExcludeFromCodeCoverage]
    private static string[] CreateExitArgs()
    {
        if (OperatingSystem.IsWindows())
        {
            return [GetWindowsCommandProcessor(), "/c", "exit 0", null!];
        }

        return ["/bin/sh", "-c", "exit 0", null!];
    }

    [ExcludeFromCodeCoverage]
    private static string[] CreateLongRunningArgs()
    {
        if (OperatingSystem.IsWindows())
        {
            return [GetWindowsCommandProcessor(), "/c", "ping 127.0.0.1 -n 30 > nul", null!];
        }

        return ["/bin/sh", "-c", "sleep 30", null!];
    }

    [ExcludeFromCodeCoverage]
    private static string GetWindowsCommandProcessor()
    {
        string? commandProcessor = Environment.GetEnvironmentVariable("ComSpec");
        TestAssert.NotNull(commandProcessor, "The ComSpec environment variable must point to the Windows command processor.");
        return commandProcessor!;
    }

    private static string ReadUtf8(IntPtr data, UIntPtr size)
    {
        int byteCount = checked((int)size);
        byte[] bytes = new byte[byteCount];
        Marshal.Copy(data, bytes, 0, byteCount);
        return Encoding.UTF8.GetString(bytes);
    }

    private static void FreeIfNeeded(IntPtr value)
    {
        if (value != IntPtr.Zero)
        {
            SDL3.SDL.Free(value);
        }
    }

    private static void AssertSdlLibraryImport(MethodInfo method, string entryPoint)
    {
        LibraryImportAttribute? libraryImport = method.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, $"SDL.{method.Name} must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3", libraryImport!.LibraryName, $"SDL.{method.Name} must import from SDL3.");
        TestAssert.Equal(entryPoint, libraryImport.EntryPoint, $"SDL.{method.Name} must bind {entryPoint}.");
    }

    private static void AssertBoolReturnMarshal(MethodInfo method)
    {
        MarshalAsAttribute? marshalAs = method.ReturnParameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} return value must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.I1, marshalAs!.Value, $"SDL.{method.Name} return value must use I1 marshalling.");
    }

    private static void AssertBooleanParameterMarshal(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.I1, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use I1 marshalling.");
    }

    private static void AssertStringArrayParameterMarshal(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.LPArray, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use LPArray marshalling.");
        TestAssert.Equal(UnmanagedType.LPUTF8Str, marshalAs.ArraySubType, $"SDL.{method.Name} parameter {parameterName} array elements must use UTF-8 string marshalling.");
    }

    private sealed class PropertiesScope : IDisposable
    {
        public PropertiesScope()
        {
            Handle = SDL3.SDL.CreateProperties();
            TestAssert.True(Handle != 0, "SDL.CreateProperties must create a properties object.");
        }

        public uint Handle { get; }

        public void Dispose()
        {
            SDL3.SDL.DestroyProperties(Handle);
        }
    }

    private sealed class Utf8PointerArray : IDisposable
    {
        private readonly IntPtr[] strings;

        public Utf8PointerArray(string[] values)
        {
            strings = new IntPtr[values.Length];
            Pointer = Marshal.AllocCoTaskMem(IntPtr.Size * values.Length);

            for (int index = 0; index < values.Length; index++)
            {
                strings[index] = values[index] is null ? IntPtr.Zero : Marshal.StringToCoTaskMemUTF8(values[index]);
                Marshal.WriteIntPtr(Pointer, index * IntPtr.Size, strings[index]);
            }
        }

        public IntPtr Pointer { get; }

        public void Dispose()
        {
            foreach (IntPtr value in strings)
            {
                FreeCoTaskMemIfNeeded(value);
            }

            Marshal.FreeCoTaskMem(Pointer);
        }

        private static void FreeCoTaskMemIfNeeded(IntPtr value)
        {
            if (value != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(value);
            }
        }
    }
}
