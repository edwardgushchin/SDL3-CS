namespace SDL3;

public static partial class SDL
{
    public readonly struct Sensor(IntPtr handle)
    {
        public IntPtr Handle { get; } = handle;

        public override bool Equals(object? obj)
        {
            return obj is Sensor other && Handle == other.Handle;
        }

        public override int GetHashCode()
        {
            return Handle.GetHashCode();
        }
        
        public static bool operator ==(Sensor left, Sensor right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Sensor left, Sensor right)
        {
            return !(left == right);
        }
    }
}