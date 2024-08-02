#region License
/* Copyright (c) 2024 Eduard Gushchin.
 *
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from
 * the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you must not
 * claim that you wrote the original software. If you use this software in a
 * product, an acknowledgment in the product documentation would be
 * appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not be
 * misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source distribution.
 */
#endregion

using System.Reflection;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    private const string SDLLibrary = "SDL3";

    static SDL()
    {
        NativeLibrary.SetDllImportResolver(typeof(SDL).Assembly, ImportResolver);
    }
    
    private static IntPtr ImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        var libHandle = IntPtr.Zero;
        const string runtimes = "runtimes";
        const string native = "native";
        const string winLib = "SDL3.DLL";
        const string linLib = "libSDL3.so";
        const string osxLib = "libSDL3.dylib";
            
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            var arch = RuntimeInformation.OSArchitecture switch
            {
                Architecture.X64 => "win-x64",
                Architecture.X86 => "win-x86",
                Architecture.Arm64 => "win-arm64",
                _ => ""
            };

            var handlePath = Path.Combine(runtimes, arch, native, winLib);
            libHandle = NativeLibrary.Load(handlePath);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            var arch = RuntimeInformation.OSArchitecture switch
            {
                Architecture.X64 => "linux-x64",
                Architecture.X86 => "linux-x86",
                Architecture.Arm64 => "linux-arm64",
                Architecture.Arm => "linux-arm",
                _ => ""
            };

            var handlePath = Path.Combine(runtimes, arch, native, linLib);
            libHandle = NativeLibrary.Load(handlePath);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            var arch = RuntimeInformation.OSArchitecture switch
            {
                Architecture.X64 => "osx-x64",
                Architecture.Arm64 => "osx-arm64",
                _ => ""
            };

            var handlePath = Path.Combine(runtimes, arch, native, osxLib);
            libHandle = NativeLibrary.Load(handlePath);
        }
        return libHandle;
    }
    
    public const UnmanagedType SDLBool = UnmanagedType.I1;
    
}