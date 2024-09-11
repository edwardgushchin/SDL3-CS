namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// Abstract filesystem interface
    /// </summary>
    public enum PathType
    {
        /// <summary>
        /// path does not exist
        /// </summary>
        None,
        
        /// <summary>
        /// a normal file
        /// </summary>
        File,
        
        /// <summary>
        /// a directory
        /// </summary>
        Directory,
        
        /// <summary>
        /// something completely different like a device node (not a symlink, those are always followed)
        /// </summary>
        Other
    }
}