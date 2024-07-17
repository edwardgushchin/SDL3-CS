#region License
/* SDL3# - C# Wrapper for SDL3
 *
 * Copyright (c) 2024 Eduard Gushchin.
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
 *
 * Eduard "edwardgushchin" Gushchin <eduardgushchin@yandex.ru>
 *
 */
#endregion

using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

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
        var runtimes = "runtimes";
        var native = "native";
        var winLib = "SDL3.DLL";
        var linLib = "libSDL3.so";
        var osxLib = "libSDL3.dylib";
            
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

    
    /// <summary>
    /// Calculates the maximum possible size in bytes of a given string when encoded in UTF-8.
    /// </summary>
    /// <param name="str">The input string to calculate the size for. If the string is null, the size will be 0.</param>
    /// <returns>The maximum possible byte size of the UTF-8 encoded string. If the input string is null, returns 0.</returns>
    /// <remarks>
    /// The calculation is based on the assumption that each character can take up to 4 bytes in UTF-8 encoding, plus an additional byte for the null terminator.
    /// </remarks>
    private static int UTF8Size(string? str)
    {
        if (str == null) return 0;
        return str.Length * 4 + 1;
    }

    
    /// <summary>
    /// Encodes a given string into UTF-8 and stores the result in the provided buffer.
    /// </summary>
    /// <param name="str">The input string to encode. If the string is null, the function returns a null pointer.</param>
    /// <param name="buffer">The buffer where the UTF-8 encoded bytes will be stored.</param>
    /// <param name="bufferSize">The size of the provided buffer.</param>
    /// <returns>A pointer to the buffer containing the UTF-8 encoded bytes. If the input string is null, returns a null pointer.</returns>
    /// <remarks>
    /// This function uses unsafe code to handle pointers and perform the encoding. It assumes that the provided buffer is large enough to hold the encoded string.
    /// </remarks>
    private static unsafe byte* UTF8Encode(string? str, byte* buffer, int bufferSize)
    {
        if (str == null) return (byte*) 0;
        fixed (char* strPtr = str)
        {
            Encoding.UTF8.GetBytes(strPtr, str.Length + 1, buffer, bufferSize);
        }
        return buffer;
    }
    
    
    public readonly struct Window(IntPtr handle)
    {
        public IntPtr Handle { get; } = handle;

        public override bool Equals(object? obj)
        {
            return obj is Window other && Handle == other.Handle;
        }

        public override int GetHashCode()
        {
            return Handle.GetHashCode();
        }
        
        public static bool operator ==(Window left, Window right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Window left, Window right)
        {
            return !(left == right);
        }
    }
    
    
    public readonly struct Render(IntPtr handle)
    {
        public IntPtr Handle { get; } = handle;
        
        public override bool Equals(object? obj)
        {
            return obj is Render other && Handle == other.Handle;
        }

        public override int GetHashCode()
        {
            return Handle.GetHashCode();
        }
        
        public static bool operator ==(Render left, Render right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Render left, Render right)
        {
            return !(left == right);
        }
    }
}