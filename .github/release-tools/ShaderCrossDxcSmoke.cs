using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

internal static class ShaderCrossDxcSmoke
{
    private const uint SpirvShaderFormat = 0x2u;

    [StructLayout(LayoutKind.Sequential)]
    private struct HlslInfo
    {
        public IntPtr Source;
        public IntPtr Entrypoint;
        public IntPtr IncludeDir;
        public IntPtr Defines;
        public int ShaderStage;
        public uint Props;
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    private delegate bool InitDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate void QuitDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate uint GetHlslShaderFormatsDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate IntPtr CompileSpirvFromHlslDelegate(ref HlslInfo info, out UIntPtr size);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate IntPtr GetErrorDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate void FreeDelegate(IntPtr memory);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetDllDirectory(string pathName);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern IntPtr LoadLibrary(string fileName);

    [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern IntPtr GetProcAddress(IntPtr module, string procedureName);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool FreeLibrary(IntPtr module);

    private static T LoadProcedure<T>(IntPtr module, string name) where T : class
    {
        IntPtr address = GetProcAddress(module, name);
        if (address == IntPtr.Zero)
        {
            throw new InvalidOperationException(
                string.Format("Required native procedure '{0}' was not found (Win32 error {1}).", name, Marshal.GetLastWin32Error()));
        }

        return (T)(object)Marshal.GetDelegateForFunctionPointer(address, typeof(T));
    }

    private static IntPtr StringToUtf8(string value)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(value + "\0");
        IntPtr result = Marshal.AllocHGlobal(bytes.Length);
        Marshal.Copy(bytes, 0, result, bytes.Length);
        return result;
    }

    private static string PtrToUtf8(IntPtr value)
    {
        if (value == IntPtr.Zero)
        {
            return string.Empty;
        }

        int length = 0;
        while (Marshal.ReadByte(value, length) != 0)
        {
            checked
            {
                length++;
            }
        }

        byte[] bytes = new byte[length];
        Marshal.Copy(value, bytes, 0, length);
        return Encoding.UTF8.GetString(bytes);
    }

    private static int Main(string[] args)
    {
        if (args.Length != 2 || !string.Equals(args[0], "win-x86", StringComparison.Ordinal))
        {
            Console.Error.WriteLine("Usage: ShaderCrossDxcSmoke-x86.exe win-x86 <runtime-directory>");
            return 2;
        }

        string rid = args[0];
        string runtimeDirectory = Path.GetFullPath(args[1]);
        string shaderCrossLibrary = Path.Combine(runtimeDirectory, "SDL3_shadercross.dll");
        string sdlLibrary = Path.Combine(runtimeDirectory, "SDL3.dll");
        if (!Directory.Exists(runtimeDirectory) || !File.Exists(shaderCrossLibrary) || !File.Exists(sdlLibrary))
        {
            Console.Error.WriteLine("Required win-x86 ShaderCross runtime files were not found under: " + runtimeDirectory);
            return 2;
        }

        IntPtr sdlModule = IntPtr.Zero;
        IntPtr shaderCrossModule = IntPtr.Zero;
        IntPtr sourcePointer = IntPtr.Zero;
        IntPtr entrypointPointer = IntPtr.Zero;
        IntPtr compiled = IntPtr.Zero;
        bool initialized = false;
        QuitDelegate quit = null;
        FreeDelegate free = null;

        try
        {
            if (!SetDllDirectory(runtimeDirectory))
            {
                throw new InvalidOperationException(
                    string.Format("SetDllDirectory failed for {0} with Win32 error {1}.", rid, Marshal.GetLastWin32Error()));
            }

            sdlModule = LoadLibrary(sdlLibrary);
            if (sdlModule == IntPtr.Zero)
            {
                throw new InvalidOperationException(
                    string.Format("Loading SDL3.dll failed for {0} with Win32 error {1}.", rid, Marshal.GetLastWin32Error()));
            }

            shaderCrossModule = LoadLibrary(shaderCrossLibrary);
            if (shaderCrossModule == IntPtr.Zero)
            {
                throw new InvalidOperationException(
                    string.Format("Loading SDL3_shadercross.dll failed for {0} with Win32 error {1}.", rid, Marshal.GetLastWin32Error()));
            }

            InitDelegate init = LoadProcedure<InitDelegate>(shaderCrossModule, "SDL_ShaderCross_Init");
            quit = LoadProcedure<QuitDelegate>(shaderCrossModule, "SDL_ShaderCross_Quit");
            GetHlslShaderFormatsDelegate getHlslShaderFormats =
                LoadProcedure<GetHlslShaderFormatsDelegate>(shaderCrossModule, "SDL_ShaderCross_GetHLSLShaderFormats");
            CompileSpirvFromHlslDelegate compileSpirvFromHlsl =
                LoadProcedure<CompileSpirvFromHlslDelegate>(shaderCrossModule, "SDL_ShaderCross_CompileSPIRVFromHLSL");
            GetErrorDelegate getError = LoadProcedure<GetErrorDelegate>(sdlModule, "SDL_GetError");
            free = LoadProcedure<FreeDelegate>(sdlModule, "SDL_free");

            initialized = init();
            if (!initialized)
            {
                throw new InvalidOperationException("SDL_ShaderCross_Init failed for " + rid + ": " + PtrToUtf8(getError()));
            }

            uint formats = getHlslShaderFormats();
            if ((formats & SpirvShaderFormat) == 0)
            {
                throw new InvalidOperationException(
                    string.Format("SDL_ShaderCross_GetHLSLShaderFormats returned 0x{0:x8} for {1} without SPIR-V support.", formats, rid));
            }

            sourcePointer = StringToUtf8("float4 main(float4 position : POSITION) : SV_Position { return position; }");
            entrypointPointer = StringToUtf8("main");
            HlslInfo info = new HlslInfo();
            info.Source = sourcePointer;
            info.Entrypoint = entrypointPointer;
            info.ShaderStage = 0;

            UIntPtr size;
            compiled = compileSpirvFromHlsl(ref info, out size);
            if (compiled == IntPtr.Zero || size.ToUInt64() == 0)
            {
                throw new InvalidOperationException(
                    "SDL_ShaderCross_CompileSPIRVFromHLSL failed for " + rid + ": " + PtrToUtf8(getError()));
            }

            Console.WriteLine(
                "ShaderCross DXC runtime smoke test passed for {0} (formats=0x{1:x8}, SPIR-V bytes={2}).",
                rid,
                formats,
                size.ToUInt64());
            return 0;
        }
        catch (Exception error)
        {
            Console.Error.WriteLine(error.Message);
            return 1;
        }
        finally
        {
            if (compiled != IntPtr.Zero && free != null)
            {
                free(compiled);
            }
            if (initialized && quit != null)
            {
                quit();
            }
            if (sourcePointer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(sourcePointer);
            }
            if (entrypointPointer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(entrypointPointer);
            }
            if (shaderCrossModule != IntPtr.Zero)
            {
                FreeLibrary(shaderCrossModule);
            }
            if (sdlModule != IntPtr.Zero)
            {
                FreeLibrary(sdlModule);
            }
            SetDllDirectory(null);
        }
    }
}
