namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// <para>The type of the OS-provided default folder for a specific purpose.</para>
    /// <para>Note that the Trash folder isn't included here, because trashing files
    /// usually involves extra OS-specific functionality to remember the file's
    /// original location.</para>
    /// <para>The folders supported per platform are:
    /// (<a href="https://github.com/libsdl-org/SDL/blob/77c569496df90526641cdede56feeb263be0b7b6/include/SDL3/SDL_filesystem.h#L144">see here</a>)</para>
    /// <para>Note that on macOS/iOS, the Videos folder is called "Movies".</para>
    /// </summary>
    /// <since>This enum is available since SDL 3.0.0.</since>
    /// <seealso cref="GetUserFolder"/>
    public enum Folder
    {
        /// <summary>
        /// The folder which contains all of the current user's data, preferences,
        /// and documents. It usually contains most of the other folders. If a
        /// requested folder does not exist, the home folder can be considered a safe
        /// fallback to store a user's documents.
        /// </summary>
        Home,
        
        /// <summary>
        /// The folder of files that are displayed on the desktop. Note that the
        /// existence of a desktop folder does not guarantee that the system does
        /// show icons on its desktop; certain GNU/Linux distros with a graphical
        /// environment may not have desktop icons.
        /// </summary>
        Desktop,
        
        /// <summary>
        /// User document files, possibly application-specific. This is a good
        /// place to save a user's projects.
        /// </summary>
        Documents,
        
        /// <summary>
        /// Standard folder for user files downloaded from the internet.
        /// </summary>
        Downloads,
        
        /// <summary>
        /// Music files that can be played using a standard music player (mp3,
        /// ogg...).
        /// </summary>
        Music,
        
        /// <summary>
        /// Image files that can be displayed using a standard viewer (png,
        /// jpg...).
        /// </summary>
        Pictures,
        
        /// <summary>
        /// Files that are meant to be shared with other users on the same
        /// computer.
        /// </summary>
        PublicShare,
        
        /// <summary>
        /// Save files for games.
        /// </summary>
        SavedGames,
        
        /// <summary>
        /// Application screenshots.
        /// </summary>
        Screenshots,
        
        /// <summary>
        /// Template files to be used when the user requests the desktop environment
        /// to create a new file in a certain folder, such as "New Text File.txt".
        /// Any file in the Templates folder can be used as a starting point for a
        /// new file.
        /// </summary>
        Templates,
        
        /// <summary>
        /// Video files that can be played using a standard video player (mp4,
        /// webm...).
        /// </summary>
        Videos,
        
        /// <summary>
        /// total number of types in this enum, not a folder type by itself.
        /// </summary>
        Total
    }
}