using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    /// <code>typedef struct SDL_IOStream SDL_IOStream;</code>
    /// <summary>
    /// <para>The read/write operation structure.</para>
    /// <para>This operates as an opaque handle. There are several APIs to create various
    /// types of I/O streams, or an app can supply an <see cref="IOStreamInterface"/> to
    /// <see cref="OpenIO"/> to provide their own stream implementation behind this
    /// struct's abstract interface.</para>
    /// </summary>
    /// <since>This struct is available since SDL 3.0.0.</since>
    public class IOStream(IntPtr handle)
    {
        internal IntPtr Handle { get; } = handle;

        public override bool Equals(object? obj)
        {
            if (obj is IOStream other) 
                return Handle == other.Handle;
            return false;
        }

        public override int GetHashCode()
        {
            return Handle.GetHashCode();
        }
        
        public static bool operator ==(IOStream? left, IOStream? right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
                return true;
            
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                return false;
            
            return left.Handle == right.Handle;
        }

        public static bool operator !=(IOStream? left, IOStream? right)
        {
            return !(left == right);
        }
    }
}