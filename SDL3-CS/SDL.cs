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

using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    private const string SDLLibrary = "SDL3";
    
    public const UnmanagedType SDLBool = UnmanagedType.I1;

    static SDL()
    {
        NativeLibrary.SetDllImportResolver(typeof(SDL).Assembly, ImportResolver);
    }
    
    private static IntPtr ImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        var libHandle = IntPtr.Zero;
        const string runtimes = "runtimes";
        const string native = "native";
        const string winLib = "SDL3.dll";
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


    /// <summary>
    /// Converts an unmanaged pointer to a managed structure of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the structure to which the unmanaged pointer should be converted. This type must be a value type (struct).
    /// </typeparam>
    /// <param name="pointer">
    /// A pointer to an unmanaged memory location that contains the data to be converted.
    /// </param>
    /// <returns>
    /// The managed structure of type <typeparamref name="T"/>, or <c>null</c> if the pointer is <c>IntPtr.Zero</c>.
    /// </returns>
    /// <remarks>
    /// This method uses the <see cref="Marshal.PtrToStructure{T}(System.IntPtr)"/> method to convert the unmanaged pointer into a managed structure.
    /// If the provided pointer is <c>IntPtr.Zero</c>, the method will return <c>null</c> instead of attempting to convert it.
    /// </remarks>
    /// <exception cref="ArgumentException">The layout of T is not sequential or explicit</exception>
    public static T? PointerToManagedStruct<T>(IntPtr pointer) where T : struct
    {
        return pointer == IntPtr.Zero ? null : Marshal.PtrToStructure<T>(pointer);
    }
    
    
    /// <summary>
    /// Converts a null-terminated array of UTF-8 encoded string pointers in unmanaged memory 
    /// to a managed array of strings.
    /// </summary>
    /// <param name="pointer">
    /// A pointer to the start of the null-terminated array of UTF-8 encoded string pointers in unmanaged memory.
    /// </param>
    /// <returns>
    /// A managed array of strings if the pointer is not null and contains valid data; 
    /// <c>null</c> if the pointer is <see cref="IntPtr.Zero"/> or if the array is empty.
    /// </returns>
    /// <remarks>
    /// This method iterates through the memory starting at the specified pointer. Each pointer in the array is read
    /// and interpreted as a UTF-8 encoded string, which is then converted to a managed string. The process stops
    /// when a null pointer (indicating the end of the array) is encountered.
    /// 
    /// The method assumes that the memory structure follows the format of a null-terminated array of pointers 
    /// to UTF-8 encoded strings. It is the caller's responsibility to ensure that the memory layout is valid
    /// and accessible.
    /// 
    /// Memory allocated for the unmanaged strings and the array itself must be freed manually if it is no longer needed.
    /// </remarks>
    /// <exception cref="AccessViolationException">
    /// Thrown if the specified pointer references invalid or inaccessible memory.
    /// </exception>
    public static string[]? PointerToManagedStringArray(IntPtr pointer)
    {
        if (pointer == IntPtr.Zero) return null;

        var result = new List<string>();

        while (true)
        {
            var currentPtr = Marshal.ReadIntPtr(pointer);
            if (currentPtr == IntPtr.Zero)
                break;

            var str = Marshal.PtrToStringUTF8(currentPtr);
            if (str != null)
                result.Add(str);
            
            pointer += IntPtr.Size;
        }
        
        return result.Count > 0 ? result.ToArray() : null;
    }
}