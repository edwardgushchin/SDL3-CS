namespace SDL3;

public static partial class SDL
{
    public class Sensor(IntPtr handle)
    {
        internal IntPtr Handle { get; } = handle;

        public override bool Equals(object? obj)
        {
            if (obj is Window other) 
                return Handle == other.Handle;
            return false;
        }

        public override int GetHashCode()
        {
            return Handle.GetHashCode();
        }
        
        public static bool operator ==(Sensor? left, Sensor? right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
                return true;
            
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                return false;
            
            return left.Handle == right.Handle;
        }

        public static bool operator !=(Sensor? left, Sensor? right)
        {
            return !(left == right);
        }
    }
}