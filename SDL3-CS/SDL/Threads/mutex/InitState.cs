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

using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// <para>A structure used for thread-safe initialization and shutdown.</para>
    /// <para>Here is an example of using this:</para>
    /// <code>
    /// static SDL_InitState init;
    ///
    ///    bool InitSystem(void)
    ///    {
    ///        if (!SDL_ShouldInit(ref init)) {
    ///            // The system is initialized
    ///            return true;
    ///        }
    ///
    ///        // At this point, you should not leave this function without calling SDL_SetInitialized()
    ///
    ///        bool initialized = DoInitTasks();
    ///        SDL_SetInitialized(ref init, initialized);
    ///        return initialized;
    ///    }
    ///
    ///    bool UseSubsystem(void)
    ///    {
    ///        if (SDL_ShouldInit(ref init)) {
    ///            // Error, the subsystem isn't initialized
    ///            SDL_SetInitialized(ref init, false);
    ///            return false;
    ///        }
    ///
    ///        // Do work using the initialized subsystem
    ///
    ///        return true;
    ///    }
    ///
    ///    void QuitSystem(void)
    ///    {
    ///        if (!SDL_ShouldQuit(ref init)) {
    ///            // The system is not initialized
    ///            return;
    ///        }
    ///
    ///        // At this point, you should not leave this function without calling SDL_SetInitialized()
    ///
    ///        DoQuitTasks();
    ///        SDL_SetInitialized(ref init, false);
    ///    }
    /// </code>
    /// <para>Note that this doesn't protect any resources created during initialization,
    /// or guarantee that nobody is using those resources during cleanup. You
    /// should use other mechanisms to protect those, if that's a concern for your
    /// code.</para>
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    [StructLayout(LayoutKind.Sequential)]
    public struct InitState
    {
        public AtomicInt status;
        public UInt64 Thread;

        private IntPtr _reserved;
    }
}