#region License
/* Copyright (c) 2024-2025 Eduard Gushchin.
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
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    private const string SDLLibrary = "SDL3";
    
    
    /// <summary>
    /// Converts a pointer to a structure of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="pointer">
    /// The <see cref="IntPtr"/> representing the memory address of the structure.
    /// </param>
    /// <typeparam name="T">
    /// The type of the structure to which the pointer will be converted. 
    /// Must be a value type (struct).
    /// </typeparam>
    /// <returns>
    /// An instance of type <typeparamref name="T"/> containing the data from the memory location pointed to by <paramref name="pointer"/>, 
    /// or <see langword="null"/> if <paramref name="pointer"/> is <see cref="IntPtr.Zero"/>.
    /// </returns>
    /// <remarks>
    /// This method is typically used when interop with unmanaged code is required, such as reading data from 
    /// unmanaged memory into a managed structure.
    /// </remarks>
    /// <seealso cref="StructureToPointer{T}"/>
    /// <seealso cref="StructureArrayToPointer{T}"/>
    /// <seealso cref="PointerToPointerArray"/>
    /// <seealso cref="PointerToStringArray(nint)"/>
    /// <seealso cref="PointerToStringArray(nint, int)"/>
    /// <seealso cref="PointerToStructureArray{T}"/>
    public static T? PointerToStructure<T>(IntPtr pointer) where T : struct
    {
        return pointer == IntPtr.Zero ? null : Marshal.PtrToStructure<T>(pointer);
    }
    
    
    /// <summary>
    /// Allocates unmanaged memory and copies the data of a structure of type <typeparamref name="T"/> into the allocated memory.
    /// </summary>
    /// <param name="structure">
    /// The instance of the structure to copy into unmanaged memory. 
    /// If <paramref name="structure"/> is <see langword="null"/>, the method returns <see cref="IntPtr.Zero"/>.
    /// </param>
    /// <typeparam name="T">
    /// The type of the structure to be converted. Must be a value type (struct).
    /// </typeparam>
    /// <returns>
    /// An <see cref="IntPtr"/> pointing to the allocated unmanaged memory containing the structure data, 
    /// or <see cref="IntPtr.Zero"/> if <paramref name="structure"/> is <see langword="null"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// This method allocates unmanaged memory using <see cref="Marshal.AllocHGlobal(int)"/> and copies the structure data 
    /// into the allocated memory using <see cref="Marshal.StructureToPtr(object, IntPtr, bool)"/>. 
    /// </para>
    /// <para>
    /// The caller is responsible for releasing the allocated memory by calling <see cref="Marshal.FreeHGlobal(IntPtr)"/> 
    /// when the memory is no longer needed to prevent memory leaks.
    /// </para>
    /// <para>
    /// Be cautious when working with unmanaged memory, as improper memory management can lead to resource leaks 
    /// or application instability.
    /// </para>
    /// </remarks>
    /// <seealso cref="PointerToStructure{T}"/>
    /// <seealso cref="StructureArrayToPointer{T}"/>
    /// <seealso cref="PointerToPointerArray"/>
    /// <seealso cref="PointerToStringArray(nint)"/>
    /// <seealso cref="PointerToStringArray(nint, int)"/>
    /// <seealso cref="PointerToStructureArray{T}"/>
    public static IntPtr StructureToPointer<T>(T? structure) where T : struct
    {
        if (!structure.HasValue) return IntPtr.Zero;
        
        var ptr = Marshal.AllocHGlobal(Marshal.SizeOf<T>());
        
        Marshal.StructureToPtr(structure, ptr, false);
    
        return ptr;
    }

    
    /// <summary>
    /// Allocates unmanaged memory and copies the data of an array of structures of type <typeparamref name="T"/> 
    /// into the allocated memory.
    /// </summary>
    /// <param name="array">
    /// The array of structures to copy into unmanaged memory. 
    /// If <paramref name="array"/> is <see langword="null"/> or empty, the method returns <see cref="IntPtr.Zero"/>.
    /// </param>
    /// <typeparam name="T">
    /// The type of the structures in the array. Must be a value type (struct).
    /// </typeparam>
    /// <returns>
    /// An <see cref="IntPtr"/> pointing to the allocated unmanaged memory containing the data of the array, 
    /// or <see cref="IntPtr.Zero"/> if <paramref name="array"/> is <see langword="null"/> or empty.
    /// </returns>
    /// <remarks>
    /// <para>
    /// This method allocates unmanaged memory using <see cref="Marshal.AllocHGlobal(int)"/> and copies each element 
    /// of the array into the allocated memory using <see cref="Marshal.StructureToPtr(object, IntPtr, bool)"/>. 
    /// The structures are stored contiguously in memory.
    /// </para>
    /// <para>
    /// The caller is responsible for releasing the allocated memory by calling <see cref="Marshal.FreeHGlobal(IntPtr)"/> 
    /// when the memory is no longer needed to prevent memory leaks.
    /// </para>
    /// <para>
    /// Be cautious when working with unmanaged memory, as improper memory management can lead to resource leaks 
    /// or application instability.
    /// </para>
    /// </remarks>
    /// <seealso cref="PointerToStructure{T}"/>
    /// <seealso cref="StructureToPointer{T}"/>
    /// <seealso cref="PointerToPointerArray"/>
    /// <seealso cref="PointerToStringArray(nint)"/>
    /// <seealso cref="PointerToStringArray(nint, int)"/>
    /// <seealso cref="PointerToStructureArray{T}"/>
    public static IntPtr StructureArrayToPointer<T>(T[] array) where T : struct
    {
        if (array == null || array.Length == 0) return IntPtr.Zero;

        var sizeOfT = Marshal.SizeOf<T>();
        var unmanagedPointer = Marshal.AllocHGlobal(sizeOfT * array.Length);

        for (var i = 0; i < array.Length; i++)
        {
            var offset = IntPtr.Add(unmanagedPointer, i * sizeOfT);
            Marshal.StructureToPtr(array[i], offset, false);
        }

        return unmanagedPointer;
    }

    
    /// <summary>
    /// Converts a block of unmanaged memory pointed to by an <see cref="IntPtr"/> into an array of <see cref="IntPtr"/> instances.
    /// </summary>
    /// <param name="pointer">
    /// A pointer to the start of the unmanaged memory block containing the array of pointers. 
    /// If <paramref name="pointer"/> is <see cref="IntPtr.Zero"/>, the method returns <see langword="null"/>.
    /// </param>
    /// <param name="size">
    /// The number of <see cref="IntPtr"/> elements in the array. If <paramref name="size"/> is 0, 
    /// the method returns an empty array.
    /// </param>
    /// <returns>
    /// An array of <see cref="IntPtr"/> instances representing the pointers stored in the unmanaged memory block, 
    /// or <see langword="null"/> if <paramref name="pointer"/> is <see cref="IntPtr.Zero"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// This method uses <see cref="Marshal.Copy(IntPtr, IntPtr[], int, int)"/> to copy the contents of the unmanaged 
    /// memory block into a managed array of <see cref="IntPtr"/>.
    /// </para>
    /// <para>
    /// The caller is responsible for ensuring that the memory block pointed to by <paramref name="pointer"/> 
    /// is valid and that its size matches the specified <paramref name="size"/>.
    /// </para>
    /// <para>
    /// Be cautious when working with unmanaged memory, as accessing invalid memory can cause application crashes or undefined behavior.
    /// </para>
    /// </remarks>
    /// <seealso cref="PointerToStructure{T}"/>
    /// <seealso cref="StructureToPointer{T}"/>
    /// <seealso cref="StructureArrayToPointer{T}"/>
    /// <seealso cref="PointerToStringArray(nint)"/>
    /// <seealso cref="PointerToStringArray(nint, int)"/>
    /// <seealso cref="PointerToStructureArray{T}"/>
    public static IntPtr[]? PointerToPointerArray(IntPtr pointer, int size)
    {
        if (pointer == IntPtr.Zero) return null;
        
        if (size == 0) return [];
        
        var pointers = new IntPtr[size];
        
        Marshal.Copy(pointer, pointers, 0, pointers.Length);
        
        return pointers;
    }
    
    
    /// <summary>
    /// Converts an unmanaged array of null-terminated UTF-8 strings, represented as an <see cref="IntPtr"/>, 
    /// into a managed array of <see cref="string"/>.
    /// </summary>
    /// <param name="pointer">
    /// A pointer to the start of an unmanaged array of pointers, where each pointer refers to a null-terminated UTF-8 string.
    /// The array is terminated by a <see cref="IntPtr.Zero"/>.
    /// If <paramref name="pointer"/> is <see cref="IntPtr.Zero"/>, the method returns <see langword="null"/>.
    /// </param>
    /// <returns>
    /// A managed array of <see cref="string"/> containing the strings from the unmanaged array, 
    /// or <see langword="null"/> if <paramref name="pointer"/> is <see cref="IntPtr.Zero"/> or the array is empty.
    /// </returns>
    /// <remarks>
    /// <para>
    /// This method reads an unmanaged array of pointers from the memory location specified by <paramref name="pointer"/>.
    /// Each pointer is assumed to reference a null-terminated UTF-8 string in unmanaged memory.
    /// The array of pointers is terminated by a <see cref="IntPtr.Zero"/> entry, indicating the end of the array.
    /// </para>
    /// <para>
    /// The method uses <see cref="Marshal.ReadIntPtr(IntPtr)"/> to read pointers from the unmanaged array and
    /// <see cref="Marshal.PtrToStringUTF8(IntPtr)"/> to convert each pointer to a managed string.
    /// </para>
    /// <para>
    /// The caller is responsible for ensuring that the memory block pointed to by <paramref name="pointer"/> 
    /// is valid and correctly structured (i.e., an array of pointers to null-terminated UTF-8 strings followed by <see cref="IntPtr.Zero"/>).
    /// </para>
    /// <para>
    /// If the unmanaged array contains only a terminating <see cref="IntPtr.Zero"/>, the method returns <see langword="null"/>.
    /// </para>
    /// </remarks>
    /// <seealso cref="PointerToStructure{T}"/>
    /// <seealso cref="StructureToPointer{T}"/>
    /// <seealso cref="StructureArrayToPointer{T}"/>
    /// <seealso cref="PointerToPointerArray"/>
    /// <seealso cref="PointerToStringArray(nint, int)"/>
    /// <seealso cref="PointerToStructureArray{T}"/>
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
    
    
    /// <summary>
    /// Converts an unmanaged array of pointers to null-terminated UTF-8 strings, represented by an <see cref="IntPtr"/>, 
    /// into a managed array of <see cref="string"/> of a specified size.
    /// </summary>
    /// <param name="pointer">
    /// A pointer to the start of an unmanaged array of pointers, where each pointer refers to a null-terminated UTF-8 string.
    /// The size of the array is determined by the <paramref name="size"/> parameter.
    /// If <paramref name="pointer"/> is <see cref="IntPtr.Zero"/>, the method returns <see langword="null"/>.
    /// </param>
    /// <param name="size">
    /// The number of strings in the unmanaged array. If <paramref name="size"/> is zero, the method returns an empty array.
    /// </param>
    /// <returns>
    /// A managed array of <see cref="string"/> containing the strings from the unmanaged array, 
    /// or <see langword="null"/> if <paramref name="pointer"/> is <see cref="IntPtr.Zero"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// This method reads an unmanaged array of pointers from the memory location specified by <paramref name="pointer"/>.
    /// Each pointer is assumed to reference a null-terminated UTF-8 string in unmanaged memory.
    /// The number of elements in the array is specified by the <paramref name="size"/> parameter.
    /// </para>
    /// <para>
    /// The method uses <see cref="Marshal.Copy(IntPtr, IntPtr[], int, int)"/> to copy the pointers from unmanaged memory
    /// into a managed array of <see cref="IntPtr"/>. Then, <see cref="Marshal.PtrToStringUTF8(IntPtr)"/> is used to convert 
    /// each pointer to a managed string.
    /// </para>
    /// <para>
    /// If the array contains any invalid pointers or strings that are not properly null-terminated, the behavior is undefined.
    /// </para>
    /// <para>
    /// The caller is responsible for ensuring that the memory block pointed to by <paramref name="pointer"/> 
    /// is valid and correctly structured (i.e., an array of pointers to null-terminated UTF-8 strings).
    /// </para>
    /// </remarks>
    /// <seealso cref="PointerToStructure{T}"/>
    /// <seealso cref="StructureToPointer{T}"/>
    /// <seealso cref="StructureArrayToPointer{T}"/>
    /// <seealso cref="PointerToPointerArray"/>
    /// <seealso cref="PointerToStringArray(nint)"/>
    /// <seealso cref="PointerToStructureArray{T}"/>
    public static string[]? PointerToStringArray(IntPtr pointer, int size)
    {
        if (pointer == IntPtr.Zero) return null;
        
        if (size == 0) return [];
        
        var result = new string[size];
        
        var ptrArray = new IntPtr[size];
        
        Marshal.Copy(pointer, ptrArray, 0, size);
            
        for (var i = 0; i < size; i++)
        {
            result[i] = Marshal.PtrToStringUTF8(ptrArray[i])!;
        }

        return result;
    }


    /// <summary>
    /// Converts a managed UTF-16 string to an unmanaged, null-terminated UTF-8 string pointer.
    /// </summary>
    /// <param name="str">The managed string to convert. Can be <c>null</c>.</param>
    /// <returns>
    /// A pointer to unmanaged memory containing the null-terminated UTF-8 encoded version of the input string,
    /// or <see cref="IntPtr.Zero"/> if <paramref name="str"/> is <c>null</c>.
    /// </returns>
    /// <remarks>
    /// The returned pointer must be freed manually using <see cref="Marshal.FreeHGlobal(IntPtr)"/> 
    /// to avoid memory leaks. If the returned pointer is <see cref="IntPtr.Zero"/>, no deallocation is needed.
    /// </remarks>
    public static IntPtr StringToPointer(string? str)
    {
        if (str == null) return IntPtr.Zero;
        
        var utf8Bytes = System.Text.Encoding.UTF8.GetBytes(str + '\0');
        var unmanagedPointer = Marshal.AllocHGlobal(utf8Bytes.Length);
        Marshal.Copy(utf8Bytes, 0, unmanagedPointer, utf8Bytes.Length);
        return unmanagedPointer;
    }
    
    
    /// <summary>
    /// Converts an unmanaged array of pointers (or raw memory) to a managed array of structures.
    /// </summary>
    /// <param name="pointer">
    /// A pointer to the start of an unmanaged block of memory. This block should contain a sequence of structures 
    /// or a series of pointers to structures. If <paramref name="pointer"/> is <see cref="IntPtr.Zero"/> or invalid,
    /// the method will return <see langword="null"/>.
    /// </param>
    /// <param name="count">
    /// The number of structures in the unmanaged memory block. If <paramref name="count"/> is zero, the method 
    /// will return an empty array. If <paramref name="count"/> is negative, the method will return <see langword="null"/>.
    /// </param>
    /// <typeparam name="T">
    /// The type of the structure. This type must be a value type (i.e., a struct).
    /// </typeparam>
    /// <returns>
    /// A managed array of <typeparamref name="T"/> structures. Returns <see langword="null"/> if the <paramref name="pointer"/> 
    /// is invalid or if <paramref name="count"/> is less than 0.
    /// </returns>
    /// <remarks>
    /// This method assumes that the unmanaged memory block pointed to by <paramref name="pointer"/> contains either:
    /// 1. A raw memory block representing a contiguous array of structures.
    /// 2. A set of pointers (as an array of <see cref="IntPtr"/>) to the individual structures.
    ///
    /// If the structures are of a primitive type (e.g., <see cref="int"/>, <see cref="float"/>), the method uses a more efficient 
    /// memory copy operation. For non-primitive types, the method will iterate over the array and use <see cref="Marshal.PtrToStructure{T}(IntPtr)"/> 
    /// to convert each pointer into its corresponding structure.
    /// </remarks>
    /// <seealso cref="PointerToStructure{T}"/>
    /// <seealso cref="StructureToPointer{T}"/>
    /// <seealso cref="StructureArrayToPointer{T}"/>
    /// <seealso cref="PointerToPointerArray"/>
    /// <seealso cref="PointerToStringArray(nint)"/>
    /// <seealso cref="PointerToStringArray(nint, int)"/>
    public static unsafe T[]? PointerToStructureArray<T>(IntPtr pointer, int count) where T : struct
    {
        if (pointer == IntPtr.Zero || count < 0) return null;

        if (count == 0) return [];

        var array = new T[count];
        
        if (typeof(T).IsPrimitive)
        {
            new Span<T>((void*)pointer, count).CopyTo(new Span<T>(array, 0, count));
        }
        else
        {
            var sizePtr = IntPtr.Size;
            for (var i = 0; i < count; i++)
            {
                var elementPtr = Marshal.ReadIntPtr(pointer, i * sizePtr);
                array[i] = Marshal.PtrToStructure<T>(elementPtr)!;
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