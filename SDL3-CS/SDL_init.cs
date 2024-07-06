#region License
/* SDL3# - C# Wrapper for SDL3
 *
 * Copyright (c) 2024 Eduard Gushchin.
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
 *
 * Eduard "edwardgushchin" Gushchin <eduardgushchin@yandex.ru>
 *
 */
#endregion

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// Represents initialization flags for SDL subsystems.
    /// </summary>
    [Flags]
    public enum InitFlags : uint
    {
        /// <summary>
        /// Initializes the timer subsystem.
        /// </summary>
        Timer = 0x00000001u,
        
        /// <summary>
        /// Initializes the audio subsystem.
        /// <see cref="Audio"/> implies <see cref="Events"/>
        /// </summary>
        Audio = 0x00000010u,
        
        /// <summary>
        /// Initializes the video subsystem.
        /// <see cref="Video"/> implies <see cref="Events"/>
        /// </summary>
        Video = 0x00000020u,
        
        /// <summary>
        /// Initializes the joystick subsystem.
        /// <see cref="Joystick"/> implies <see cref="Events"/>
        /// Should be initialized on the same thread as <see cref="Video"/>
        /// on Windows if you don't set SDL_HINT_JOYSTICK_THREAD
        /// </summary>
        Joystick = 0x00000200u,
        
        /// <summary>
        /// Initializes the haptic (force feedback) subsystem.
        /// </summary>
        Haptic = 0x00001000u,
        
        /// <summary>
        /// Initializes the gamepad subsystem.
        /// <see cref="Gamepad"/> implies <see cref="Joystick"/>
        /// </summary>
        Gamepad = 0x00002000u,
        
        /// <summary>
        /// Initializes the events subsystem.
        /// </summary>
        Events = 0x00004000u,
        
        /// <summary>
        /// Initializes the sensor subsystem.
        /// <see cref="Sensor"/> implies <see cref="Events"/>
        /// </summary>
        Sensor = 0x00008000u,
        
        /// <summary>
        /// Initializes the camera subsystem.
        /// <see cref="Camera"/> implies <see cref="Events"/>
        /// </summary>
        Camera = 0x00010000u
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_Init(InitFlags flags);
    /// <summary>
    /// Initialize the SDL library.
    /// </summary>
    /// <param name="flags">subsystem initialization flags</param>
    /// <returns>
    /// Returns 0 on success or a negative error code on failure;
    /// call <see cref="GetError"/> for more information.
    /// </returns>
    /// <remarks>
    /// <see cref="Init"/> simply forwards to calling <see cref="InitSubSystem"/>.
    /// Therefore, the two may be used interchangeably.
    /// Though for readability of your code <see cref="InitSubSystem"/> might be preferred.
    /// The file I/O (for example: <see cref="IOFromFile"/>) and threading (<see cref="CreateThread"/>)
    /// subsystems are initialized by default.
    /// Message boxes (<see cref="ShowSimpleMessageBox"/>) also attempt to work without initializing the
    /// video subsystem, in hopes of being useful in showing an error dialog when <see cref="Init"/> fails.
    /// You must specifically initialize other subsystems if you use them in your application.
    /// </remarks>
    public static int Init(InitFlags flags) => SDL_Init(flags);

    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_InitSubSystem(InitFlags flags);
    /// <summary>
    /// Compatibility function to initialize the SDL library.
    /// </summary>
    /// <param name="flags">
    /// any of the flags used by <see cref="Init"/>;
    /// see <see cref="Init"/> for details.
    /// </param>
    /// <returns>
    /// Returns 0 on success or a negative error code on failure;
    /// call <see cref="GetError"/> for more information.
    /// </returns>
    /// <remarks>
    /// This function and SDL_Init() are interchangeable.
    /// </remarks>
    public static int InitSubSystem(InitFlags flags) => SDL_InitSubSystem(flags);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_QuitSubSystem(InitFlags flags);
    /// <summary>
    /// Shut down specific SDL subsystems.
    /// </summary>
    /// <remarks>
    /// You still need to call <see cref="Quit"/>() even if you close all
    /// open subsystems with <see cref="QuitSubSystem"/>().
    /// </remarks>
    /// <param name="flags">any of the flags used by <see cref="Init"/> see <see cref="Init"/> for details.</param>
    public static void QuitSubSystem(InitFlags flags) => SDL_QuitSubSystem(flags);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial InitFlags SDL_WasInit(InitFlags flags);
    /// <summary>
    /// Get a mask of the specified subsystems which are currently initialized.
    /// </summary>
    /// <param name="flags">
    /// any of the flags used by <see cref="Init"/> see <see cref="Init"/> for details.
    /// </param>
    /// <returns>
    /// Returns a mask of all initialized subsystems if flags is 0,
    /// otherwise it returns the initialization status of the specified subsystems.
    /// </returns>
    public static InitFlags WasInit(InitFlags flags) => SDL_WasInit(flags);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_Quit();
    /// <summary>
    /// Clean up all initialized subsystems.
    /// </summary>
    /// <remarks>
    /// You should call this function even if you have already shutdown each initialized subsystem with
    /// <see cref="QuitSubSystem"/>. It is safe to call this function even in the case of errors in initialization.
    /// You can use this function with atexit() to ensure that it is run when your application is shutdown,
    /// but it is not wise to do this from a library or other dynamically loaded code.
    /// </remarks>
    public static void Quit() => SDL_Quit();
}