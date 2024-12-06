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

using System.ComponentModel;
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

    
    // IntPtr to struct
    public static T? PointerToStruct<T>(IntPtr pointer) where T : struct
    {
        return pointer == IntPtr.Zero ? null : Marshal.PtrToStructure<T>(pointer);
    }
    
    
    // struct to IntPtr
    // After calling Marshal.FreeHGlobal()
    public static IntPtr StructToPointer<T>(T? strct) where T : struct
    {
        if (!strct.HasValue) return IntPtr.Zero;
        
        var ptr = Marshal.AllocHGlobal(Marshal.SizeOf<T>());
        
        Marshal.StructureToPtr(strct, ptr, false);
    
        return ptr;
    }

    
    // struct[] to IntPtr
    public static IntPtr StructArrayToPointer<T>(T[] array) where T : struct
    {
        if (array == null || array.Length == 0) return IntPtr.Zero;
        
        var structSize = Marshal.SizeOf<T>();
        
        var pointer = Marshal.AllocHGlobal(structSize * array.Length);

        try
        {
            if (typeof(T).IsPrimitive)
            {
                for (var i = 0; i < array.Length; i++)
                {
                    var elementPtr = IntPtr.Add(pointer, i * structSize);
                    Marshal.StructureToPtr(array[i], elementPtr, false);
                }
            }
            else
            {
                for (var i = 0; i < array.Length; i++)
                {
                    var elementPtr = Marshal.ReadIntPtr(pointer, i * IntPtr.Size);
                    Marshal.StructureToPtr(array[i], elementPtr, false);
                }
            }
        }
        catch
        {
            Marshal.FreeHGlobal(pointer);
            throw;
        }

        return pointer;
    }

    
    // IntPtr to IntPtr[]
    public static IntPtr[]? PointerToPointerArray(IntPtr pointer, int size)
    {
        if (pointer == IntPtr.Zero) return null;
        
        if (size == 0) return [];
        
        var pointers = new IntPtr[size];
        
        Marshal.Copy(pointer, pointers, 0, pointers.Length);
        
        return pointers;
    }
    
    
    // IntPtr to string[]?
    public static string[]? PointerToStringArray(IntPtr pointer)
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
    
    
    // IntPtr to struct[]?
    public static unsafe T[]? PointerToStructArray<T>(IntPtr pointer, int count) where T : struct
    {
        if (pointer == IntPtr.Zero || count <= 0) return null;

        var array = new T[count];
        
        if (typeof(T).IsPrimitive)
        {
            new Span<T>((void*)pointer, count).CopyTo(new Span<T>(array, 0, count));
        }
        else
        {
            for (var i = 0; i < count; i++)
            {
                var elementPtr = Marshal.ReadIntPtr(pointer);
                array[i] = Marshal.PtrToStructure<T>(elementPtr);
            }
        }
    
        return array;
    }
    
    
    /// <summary>
    /// Indicates that a method is a <c>#define</c> macro.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class MacroAttribute : Attribute;
}