namespace SDL3;

using System.Runtime.InteropServices.Marshalling;
using System.Runtime.InteropServices;
using System.Text;


[CustomMarshaller(typeof(string), MarshalMode.Default, typeof(WCharStringMarshaller))]
public static class WCharStringMarshaller
{
    public static class WChar16 // Windows (UTF-16)
    {
        public static IntPtr ConvertToUnmanaged(string? managed)
        {
            if (managed is null)
                return IntPtr.Zero;

            var bytes = Encoding.Unicode.GetBytes(managed + '\0'); // null-terminated
            var ptr = Marshal.AllocHGlobal(bytes.Length);
            Marshal.Copy(bytes, 0, ptr, bytes.Length);
            return ptr;
        }

        public static string? ConvertToManaged(IntPtr unmanaged)
        {
            return unmanaged == IntPtr.Zero ? null : Marshal.PtrToStringUni(unmanaged);
        }

        public static void Free(IntPtr ptr) => Marshal.FreeHGlobal(ptr);
    }

    public static class WChar32 // Linux/macOS (UTF-32)
    {
        public static IntPtr ConvertToUnmanaged(string? managed)
        {
            if (managed is null)
                return IntPtr.Zero;

            var utf32 = Encoding.UTF32.GetBytes(managed + '\0');
            var ptr = Marshal.AllocHGlobal(utf32.Length);
            Marshal.Copy(utf32, 0, ptr, utf32.Length);
            return ptr;
        }

        public static string? ConvertToManaged(IntPtr unmanaged)
        {
            return unmanaged == IntPtr.Zero ? null : PtrToStringUTF32(unmanaged);
        }
        
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


        public static void Free(IntPtr ptr) => Marshal.FreeHGlobal(ptr);
    }
    
    // The size in bytes of a wide character for the current runtime
    public static UIntPtr WCharSize
    {
        get => (UIntPtr)(
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? 
                2 : 4
        );
    }
        

    // Выбираем реализацию в зависимости от платформы
    public static IntPtr ConvertToUnmanaged(string? managed)
    {
        return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? 
            WChar16.ConvertToUnmanaged(managed) : WChar32.ConvertToUnmanaged(managed);
    }

    public static string? ConvertToManaged(IntPtr unmanaged)
    {
        return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? 
            WChar16.ConvertToManaged(unmanaged) : WChar32.ConvertToManaged(unmanaged);
    }

    public static void Free(IntPtr ptr)
    {
        Marshal.FreeHGlobal(ptr);
    }
}
