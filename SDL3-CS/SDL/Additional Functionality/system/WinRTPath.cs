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

namespace SDL3;


public static partial class SDL
{
    /// <summary>
    /// WinRT / Windows Phone path types
    /// </summary>
    /// <since>This enum is available since SDL 3.0.0.</since>
    public enum  WinRTPath
    {
        /// <summary>
        /// The installed app's root directory.
        /// Files here are likely to be read-only. 
        /// </summary>
        InstalledLocation,
        
        /// <summary>
        /// The app's local data store.  Files may be written here
        /// </summary>
        LocalFolder,
        
        /// <summary>
        /// The app's roaming data store.  Unsupported on Windows Phone.
        /// Files written here may be copied to other machines via a network
        /// connection.
        /// </summary>
        RoamingFolder,
        
        /// <summary>
        /// The app's temporary data store.  Unsupported on Windows Phone.
        ///  Files written here may be deleted at any time.
        /// </summary>
        TempFolder
    }
}