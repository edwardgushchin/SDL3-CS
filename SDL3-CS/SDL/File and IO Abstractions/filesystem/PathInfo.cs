namespace SDL3;

public static partial class SDL
{
    public struct PathInfo
    {
        /// <summary>
        /// the path type
        /// </summary>
        public PathType Type;
        
        /// <summary>
        /// the file size in bytes
        /// </summary>
        public ulong Size;
        
        /// <summary>
        /// the time when the path was created
        /// </summary>
        public ulong CreateTime;
        
        /// <summary>
        /// the last time the path was modified
        /// </summary>
        public ulong ModifyTime;
        
        /// <summary>
        /// the last time the path was read
        /// </summary>
        public ulong AccessTime;
    }
}