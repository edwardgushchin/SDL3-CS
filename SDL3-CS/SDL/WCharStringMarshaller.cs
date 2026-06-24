#region License
/* Copyright (c) 2024-2026 Eduard Gushchin.
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

namespace SDL3;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.InteropServices;
using System.Text;


[CustomMarshaller(typeof(string), MarshalMode.Default, typeof(WCharStringMarshaller))]
public static class WCharStringMarshaller
{
    private static Func<bool> IsWindowsFunction = IsWindows;

    [ExcludeFromCodeCoverage]
    private static bool IsWindows()
    {
        return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    }

    public static class WChar16 // Windows (UTF-16)
    {
        /// <summary>
        /// Converts a managed string to an unmanaged null-terminated UTF-16 buffer.
        /// </summary>
        /// <param name="managed">the managed string to convert, or <c>null</c>.</param>
        /// <returns>a pointer to unmanaged UTF-16 data, or <see cref="IntPtr.Zero"/> for <c>null</c>.</returns>
        public static IntPtr ConvertToUnmanaged(string? managed)
        {
            if (managed is null)
                return IntPtr.Zero;

            var bytes = Encoding.Unicode.GetBytes(managed + '\0'); // null-terminated
            var ptr = Marshal.AllocHGlobal(bytes.Length);
            Marshal.Copy(bytes, 0, ptr, bytes.Length);
            return ptr;
        }

        /// <summary>
        /// Converts an unmanaged null-terminated UTF-16 buffer to a managed string.
        /// </summary>
        /// <param name="unmanaged">a pointer to unmanaged UTF-16 data.</param>
        /// <returns>the managed string, or <c>null</c> when <paramref name="unmanaged"/> is <see cref="IntPtr.Zero"/>.</returns>
        public static string? ConvertToManaged(IntPtr unmanaged)
        {
            return unmanaged == IntPtr.Zero ? null : Marshal.PtrToStringUni(unmanaged);
        }

        /// <summary>
        /// Frees unmanaged memory allocated by <see cref="ConvertToUnmanaged"/>.
        /// </summary>
        /// <param name="ptr">the unmanaged pointer to free.</param>
        public static void Free(IntPtr ptr) => Marshal.FreeHGlobal(ptr);
    }

    public static class WChar32 // Linux/macOS (UTF-32)
    {
        /// <summary>
        /// Converts a managed string to an unmanaged null-terminated UTF-32 buffer.
        /// </summary>
        /// <param name="managed">the managed string to convert, or <c>null</c>.</param>
        /// <returns>a pointer to unmanaged UTF-32 data, or <see cref="IntPtr.Zero"/> for <c>null</c>.</returns>
        public static IntPtr ConvertToUnmanaged(string? managed)
        {
            if (managed is null)
                return IntPtr.Zero;

            var utf32 = Encoding.UTF32.GetBytes(managed + '\0');
            var ptr = Marshal.AllocHGlobal(utf32.Length);
            Marshal.Copy(utf32, 0, ptr, utf32.Length);
            return ptr;
        }

        /// <summary>
        /// Converts an unmanaged null-terminated UTF-32 buffer to a managed string.
        /// </summary>
        /// <param name="unmanaged">a pointer to unmanaged UTF-32 data.</param>
        /// <returns>the managed string, or <c>null</c> when <paramref name="unmanaged"/> is <see cref="IntPtr.Zero"/>.</returns>
        public static string? ConvertToManaged(IntPtr unmanaged)
        {
            return unmanaged == IntPtr.Zero ? null : PtrToStringUTF32(unmanaged);
        }

        /// <summary>
        /// Converts an unmanaged null-terminated UTF-32 buffer to a managed string.
        /// </summary>
        /// <param name="ptr">a pointer to unmanaged UTF-32 data.</param>
        /// <returns>the managed string, or <c>null</c> when <paramref name="ptr"/> is <see cref="IntPtr.Zero"/>.</returns>
        public static string? PtrToStringUTF32(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return null;

            List<byte> bytes = [];

            unsafe
            {
                var p = (uint*)ptr;
                while (*p != 0)
                {
                    var utf32Char = BitConverter.GetBytes(*p);
                    bytes.AddRange(utf32Char);
                    p++;
                }
            }

            return Encoding.UTF32.GetString(bytes.ToArray());
        }


        /// <summary>
        /// Frees unmanaged memory allocated by <see cref="ConvertToUnmanaged"/>.
        /// </summary>
        /// <param name="ptr">the unmanaged pointer to free.</param>
        public static void Free(IntPtr ptr) => Marshal.FreeHGlobal(ptr);
    }

    /// <summary>
    /// The size in bytes of a wide character for the current runtime.
    /// </summary>
    public static UIntPtr WCharSize
    {
        get => (UIntPtr)(
            IsWindowsFunction() ?
                2 : 4
        );
    }


    // Выбираем реализацию в зависимости от платформы
    /// <summary>
    /// Converts a managed string to an unmanaged null-terminated wide-character buffer for the current runtime.
    /// </summary>
    /// <param name="managed">the managed string to convert, or <c>null</c>.</param>
    /// <returns>a pointer to unmanaged wide-character data, or <see cref="IntPtr.Zero"/> for <c>null</c>.</returns>
    public static IntPtr ConvertToUnmanaged(string? managed)
    {
        return IsWindowsFunction() ?
            WChar16.ConvertToUnmanaged(managed) : WChar32.ConvertToUnmanaged(managed);
    }

    /// <summary>
    /// Converts an unmanaged null-terminated wide-character buffer for the current runtime to a managed string.
    /// </summary>
    /// <param name="unmanaged">a pointer to unmanaged wide-character data.</param>
    /// <returns>the managed string, or <c>null</c> when <paramref name="unmanaged"/> is <see cref="IntPtr.Zero"/>.</returns>
    public static string? ConvertToManaged(IntPtr unmanaged)
    {
        return IsWindowsFunction() ?
            WChar16.ConvertToManaged(unmanaged) : WChar32.ConvertToManaged(unmanaged);
    }

    /// <summary>
    /// Frees unmanaged memory allocated by <see cref="ConvertToUnmanaged"/>.
    /// </summary>
    /// <param name="ptr">the unmanaged pointer to free.</param>
    public static void Free(IntPtr ptr)
    {
        Marshal.FreeHGlobal(ptr);
    }
}
