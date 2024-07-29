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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

/**
 * # CategoryInit
 *
 * SDL subsystem init and quit functions.
 */

public static partial class SDL
{
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_Init(InitFlags flags);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_Init(SDL_InitFlags flags);</code>
    /// <summary>
    /// <para>Initialize the SDL library.</para>
    /// <para><see cref="Init"/> simply forwards to calling <see cref="InitSubSystem"/>. Therefore, the
    /// two may be used interchangeably. Though for readability of your code
    /// <see cref="InitSubSystem"/> might be preferred.</para>
    /// <para>The file I/O (for example: <see cref="IOFromFile"/>) and threading (<see cref="CreateThread"/>)
    /// subsystems are initialized by default. Message boxes
    /// (<see cref="ShowSimpleMessageBox"/>) also attempt to work without initializing the
    /// video subsystem, in hopes of being useful in showing an error dialog when
    /// <see cref="Init"/> fails. You must specifically initialize other subsystems if you
    /// use them in your application.</para>
    /// <para>Logging (such as <see cref="Log"/>) works without initialization, too.</para>
    /// <para><c>flags</c> may be any of the following OR'd together:</para>
    /// <list type="bullet">
    /// <item><see cref="InitFlags.Timer"/>: timer subsystem</item>
    /// <item><see cref="InitFlags.Audio"/>: audio subsystem; automatically initializes the events</item>
    /// <item><see cref="InitFlags.Video"/>: video subsystem; automatically initializes the events
    /// subsystem</item>
    /// <item><see cref="InitFlags.Joystick"/>: joystick subsystem; automatically initializes the
    /// events subsystem</item>
    /// <item><see cref="InitFlags.Haptic"/>: haptic (force feedback) subsystem</item>
    /// <item><see cref="InitFlags.Gamepad"/>: gamepad subsystem; automatically initializes the
    /// joystick subsystem</item>
    /// <item><see cref="InitFlags.Events"/>: events subsystem</item>
    /// <item><see cref="InitFlags.Sensor"/>: sensor subsystem; automatically initializes the events
    /// subsystem</item>
    /// <item><see cref="InitFlags.Camera"/>: camera subsystem; automatically initializes the events
    /// subsystem</item>
    /// </list>
    /// <para>Subsystem initialization is ref-counted, you must call <see cref="QuitSubSystem"/>
    /// for each <see cref="InitSubSystem"/> to correctly shutdown a subsystem manually (or
    /// call <see cref="Quit"/> to force shutdown). If a subsystem is already loaded then
    /// this call will increase the ref-count and return.</para>
    /// </summary>
    /// <param name="flags">subsystem initialization flags.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="InitSubSystem"/>
    /// <seealso cref="Quit"/>
    /// <seealso cref="SetMainReady"/>
    /// <seealso cref="WasInit"/>
    public static int Init(InitFlags flags) => SDL_Init(flags);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_InitSubSystem(InitFlags flags);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_InitSubSystem(SDL_InitFlags flags);</code>
    /// <summary>
    /// <para>Compatibility function to initialize the SDL library.</para>
    /// <para>This function and <see cref="Init"/> are interchangeable.</para>
    /// </summary>
    /// <param name="flags">any of the flags used by <see cref="Init"/>; see <see cref="Init"/> for details.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="Init"/>
    /// <seealso cref="Quit"/>
    /// <seealso cref="QuitSubSystem"/>
    public static int InitSubSystem(InitFlags flags) => SDL_InitSubSystem(flags);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_QuitSubSystem(InitFlags flags);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_QuitSubSystem(SDL_InitFlags flags);</code>
    /// <summary>
    /// <para>Shut down specific SDL subsystems.</para>
    /// <para>You still need to call <see cref="Quit"/> even if you close all open subsystems
    /// with <see cref="QuitSubSystem"/>.</para>
    /// </summary>
    /// <param name="flags">any of the flags used by <see cref="Init"/>; see <see cref="Init"/> for details.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="InitSubSystem"/>
    /// <seealso cref="Quit"/>
    public static void QuitSubSystem(InitFlags flags) => SDL_QuitSubSystem(flags);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial InitFlags SDL_WasInit(InitFlags flags);
    /// <code>extern SDL_DECLSPEC SDL_InitFlags SDLCALL SDL_WasInit(SDL_InitFlags flags);</code>
    /// <summary>
    /// Get a mask of the specified subsystems which are currently initialized.
    /// </summary>
    /// <param name="flags">any of the flags used by <see cref="Init"/>; see <see cref="Init"/> for details.</param>
    /// <returns>a mask of all initialized subsystems if <c>flags</c> is <c>0</c>, otherwise it
    /// returns the initialization status of the specified subsystems.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="Init"/>
    /// <seealso cref="InitSubSystem"/>
    public static InitFlags WasInit(InitFlags flags) => SDL_WasInit(flags);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_Quit();
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_Quit(void);</code>
    /// <summary>
    /// <para>Clean up all initialized subsystems.</para>
    /// <para>You should call this function even if you have already shutdown each
    /// initialized subsystem with <see cref="QuitSubSystem"/>. It is safe to call this
    /// function even in the case of errors in initialization.</para>
    /// <para>You can use this function with atexit() to ensure that it is run when your
    /// application is shutdown, but it is not wise to do this from a library or
    /// other dynamically loaded code.</para>
    /// </summary>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="Init"/>
    /// <seealso cref="QuitSubSystem"/>
    public static void Quit() => SDL_Quit();
}