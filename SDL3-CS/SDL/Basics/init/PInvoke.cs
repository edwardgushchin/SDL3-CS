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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_Init(SDL_InitFlags flags);</code>
    /// <summary>
    /// <para>Initialize the SDL library.</para>
    /// <para><see cref="Init"/> simply forwards to calling <see cref="InitSubSystem"/>. Therefore, the
    /// two may be used interchangeably. Though for readability of your cod
    /// <see cref="InitSubSystem"/> might be preferred.</para>
    /// <para>The file I/O (for example: <see cref="IOFromFile"/>) and threading (CreateThread)
    /// subsystems are initialized by default. Message boxes
    /// (<see cref="ShowSimpleMessageBox"/>) also attempt to work without initializing the
    /// video subsystem, in hopes of being useful in showing an error dialog when
    /// <see cref="Init"/> fails. You must specifically initialize other subsystems if you
    /// use them in your application.</para>
    /// <para>Logging (such as <see cref="Log"/>) works without initialization, too.</para>
    /// <para>`flags` may be any of the following OR'd together:</para>
    /// <list type="bullet">
    /// <item><see cref="InitFlags.Audio"/>: audio subsystem; automatically initializes the events
    /// subsystem</item>
    /// <item><see cref="InitFlags.Video"/>: video subsystem; automatically initializes the events
    /// subsystem, should be initialized on the main thread.</item>
    /// <item><see cref="InitFlags.Joystick"/>: joystick subsystem; automatically initializes the
    /// events subsystem</item>
    /// <item><see cref="InitFlags.Haptic"/>: haptic (force feedback) subsystem</item>
    /// <item><see cref="InitFlags.Gamepad"/>: gamepad subsystem; automatically initializes the
    /// joystick subsystem</item>
    /// <item><seealso cref="InitFlags.Events"/>: events subsystem</item>
    /// <item><see cref="InitFlags.Sensor"/>: sensor subsystem; automatically initializes the events
    /// subsystem</item>
    /// <item><see cref="InitFlags.Camera"/>: camera subsystem; automatically initializes the events
    /// subsystem</item>
    /// </list>
    /// <para>Subsystem initialization is ref-counted, you must call <see cref="QuitSubSystem"/>
    /// for each <see cref="InitSubSystem"/> to correctly shutdown a subsystem manually (or
    /// call <see cref="Quit"/> to force shutdown). If a subsystem is already loaded then
    /// this call will increase the ref-count and return.</para>
    /// <para>Consider reporting some basic metadata about your application before
    /// calling <see cref="Init"/>, using either <see cref="SetAppMetadata"/> or
    /// <see cref="SetAppMetadataProperty"/>.</para>
    /// </summary>
    /// <param name="flags">subsystem initialization flags.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetAppMetadata"/>
    /// <seealso cref="SetAppMetadataProperty"/>
    /// <seealso cref="InitSubSystem"/>
    /// <seealso cref="Quit"/>
    /// <seealso cref="WasInit"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_Init"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool Init(InitFlags flags);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_InitSubSystem(SDL_InitFlags flags);</code>
    /// <summary>
    /// Compatibility function to initialize the SDL library.
    /// </summary>
    /// <remarks>This function and <see cref="Init"/> are interchangeable.</remarks>
    /// <param name="flags">any of the flags used by <see cref="Init"/>; see <see cref="Init"/> for details.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="Init"/>
    /// <seealso cref="Quit"/>
    /// <seealso cref="QuitSubSystem"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_InitSubSystem"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool InitSubSystem(InitFlags flags);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_QuitSubSystem(SDL_InitFlags flags);</code>
    /// <summary>
    /// Shut down specific SDL subsystems.
    /// </summary>
    /// <remarks>You still need to call <see cref="Quit"/> even if you close all open subsystems
    /// with <see cref="QuitSubSystem"/>.</remarks>
    /// <param name="flags">any of the flags used by <see cref="Init"/>; see <see cref="Init"/> for details.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="InitSubSystem"/>
    /// <seealso cref="Quit"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_QuitSubSystem"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void QuitSubSystem(InitFlags flags);
    
    
    /// <code>extern SDL_DECLSPEC SDL_InitFlags SDLCALL SDL_WasInit(SDL_InitFlags flags);</code>
    /// <summary>
    /// Get a mask of the specified subsystems which are currently initialized.
    /// </summary>
    /// <param name="flags">any of the flags used by <see cref="Init"/>; see <see cref="Init"/> for details.</param>
    /// <returns>a mask of all initialized subsystems if <c>flags</c> is <c>0</c>, otherwise it
    /// returns the initialization status of the specified subsystems.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="Init"/>
    /// <seealso cref="InitSubSystem"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WasInit"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial InitFlags WasInit(InitFlags flags);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_Quit(void);</code>
    /// <summary>
    /// <para>Clean up all initialized subsystems.</para>
    /// <para>You should call this function even if you have already shutdown each
    /// initialized subsystem with <seealso cref="QuitSubSystem"/>. It is safe to call this
    /// function even in the case of errors in initialization.</para>
    /// <para>You can use this function with atexit() to ensure that it is run when your
    /// application is shutdown, but it is not wise to do this from a library or
    /// other dynamically loaded code.</para>
    /// </summary>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="Init"/>
    /// <seealso cref="QuitSubSystem"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_Quit"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Quit();
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_IsMainThread(void);</code>
    /// <summary>
    /// <para>Return whether this is the main thread.</para>
    /// <para>On Apple platforms, the main thread is the thread that runs your program's
    /// main() entry point. On other platforms, the main thread is the one that
    /// calls Init(WindowFlags.Video), which should usually be the one that runs
    /// your program's main() entry point. If you are using the main callbacks,
    /// SDL_AppInit(), SDL_AppIterate(), and SDL_AppQuit() are all called on the
    /// main thread.</para>
    /// </summary>
    /// <returns><c>true</c> if this thread is the main thread, or <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="RunOnMainThread"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_IsMainThread"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsMainThread();
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RunOnMainThread(SDL_MainThreadCallback callback, void *userdata, bool wait_complete);</code>
    /// <summary>
    /// <para>Call a function on the main thread during event processing.</para>
    /// <para>If this is called on the main thread, the callback is executed immediately.
    /// If this is called on another thread, this callback is queued for execution
    /// on the main thread during event processing.</para>
    /// <para>Be careful of deadlocks when using this functionality. You should not have
    /// the main thread wait for the current thread while this function is being
    /// called with <c>waitComplete</c> true.</para>
    /// </summary>
    /// <param name="callback">the callback to call on the main thread.</param>
    /// <param name="userdata">a pointer that is passed to <c>callback</c>.</param>
    /// <param name="waitComplete"><c>true</c> to wait for the callback to complete, <c>false</c> to
    /// return immediately.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="IsMainThread"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RunOnMainThread"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RunOnMainThread(MainThreadCallback callback, IntPtr userdata, [MarshalAs(UnmanagedType.I1)] bool waitComplete);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetAppMetadata(const char *appname, const char *appversion, const char *appidentifier);</code>
    /// <summary>
    /// <para>Specify basic metadata about your app.</para>
    /// <para>You can optionally provide metadata about your app to SDL. This is not
    /// required, but strongly encouraged.</para>
    /// <para>There are several locations where SDL can make use of metadata (an "About"
    /// box in the macOS menu bar, the name of the app can be shown on some audio
    /// mixers, etc). Any piece of metadata can be left as <c>null</c>, if a specific
    /// detail doesn't make sense for the app.</para>
    /// <para>This function should be called as early as possible, before <see cref="Init"/>.
    /// Multiple calls to this function are allowed, but various state might not
    /// change once it has been set up with a previous call to this function.</para>
    /// <para>Passing a <c>null</c> removes any previous metadata.</para>
    /// <para>This is a simplified interface for the most important information. You can
    /// supply significantly more detailed metadata with
    /// <see cref="SetAppMetadataProperty"/>.</para>
    /// </summary>
    /// <param name="appname">The name of the application ("My Game 2: Bad Guy's
    /// Revenge!").</param>
    /// <param name="appversion">The version of the application ("1.0.0beta5" or a git
    /// hash, or whatever makes sense).</param>
    /// <param name="appidentifier">A unique string in reverse-domain format that
    /// identifies this app ("com.example.mygame2").</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetAppMetadataProperty"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetAppMetadata"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetAppMetadata(
        [MarshalAs(UnmanagedType.LPUTF8Str)] string appname,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string appversion,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string appidentifier);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetAppMetadataProperty(const char *name, const char *value);</code>
    /// <summary>
    /// <para>Specify metadata about your app through a set of properties.</para>
    /// <para>You can optionally provide metadata about your app to SDL. This is not
    /// required, but strongly encouraged.</para>
    /// <para>There are several locations where SDL can make use of metadata (an <c>"About"</c>
    /// box in the macOS menu bar, the name of the app can be shown on some audio
    /// mixers, etc). Any piece of metadata can be left out, if a specific detail
    /// doesn't make sense for the app.</para>
    /// <para>This function should be called as early as possible, before <see cref="Init"/>.
    /// Multiple calls to this function are allowed, but various state might not
    /// change once it has been set up with a previous call to this function.</para>
    /// <para>Once set, this metadata can be read using <see cref="GetAppMetadataProperty"/>.</para>
    /// <para>These are the supported properties:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.AppMetadataNameString"/>`: The human-readable name of the
    /// application, like <c>"My Game 2: Bad Guy's Revenge!"</c>. This will show up
    /// anywhere the OS shows the name of the application separately from window
    /// titles, such as volume control applets, etc. This defaults to <c>"SDL
    /// Application"</c>.</item>
    /// <item><see cref="Props.AppMetadataVersionString"/>: The version of the app that is
    /// running; there are no rules on format, so <c>"1.0.3beta2"</c> and <c>"April 22nd,
    /// 2024"</c> and a git hash are all valid options. This has no default.</item>
    /// <item><see cref="Props.AppMetadataIdentifierString"/>: A unique string that
    /// identifies this app. This must be in reverse-domain format, like
    /// <c>"com.example.mygame2"</c>. This string is used by desktop compositors to
    /// identify and group windows together, as well as match applications with
    /// associated desktop settings and icons. If you plan to package your
    /// application in a container such as Flatpak, the app ID should match the
    /// name of your Flatpak container as well. This has no default.</item>
    /// <item><see cref="Props.AppMetadataCreatorString"/>: The human-readable name of the
    /// creator/developer/maker of this app, like <c>"MojoWorkshop, LLC"</c></item>
    /// <item><see cref="Props.AppMetadataCopyrightString"/>: The human-readable copyright
    /// notice, like <c>"Copyright (c) 2024 MojoWorkshop, LLC"</c> or whatnot. Keep this
    /// to one line, don't paste a copy of a whole software license in here. This
    /// has no default.</item>
    /// <item><see cref="Props.AppMetadataURLString"/>: A URL to the app on the web. Maybe a
    /// product page, or a storefront, or even a GitHub repository, for user's
    /// further information This has no default.</item>
    /// <item><see cref="Props.AppMetadataTypeString"/>: The type of application this is.
    /// Currently this string can be <c>"game"</c> for a video game, <c>"mediaplayer"</c> for a
    /// media player, or generically <c>"application"</c> if nothing else applies.
    /// Future versions of SDL might add new types. This defaults to
    /// <c>"application"</c>.</item>
    /// </list>
    /// </summary>
    /// <param name="name">the name of the metadata property to set.</param>
    /// <param name="value">the value of the property, or <c>null</c> to remove that property.</param>
    /// <returns>true on success or false on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetAppMetadataProperty"/>
    /// <seealso cref="SetAppMetadata"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetAppMetadataProperty"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetAppMetadataProperty([MarshalAs(UnmanagedType.LPUTF8Str)] string name, [MarshalAs(UnmanagedType.LPUTF8Str)] string value);
    
    
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetAppMetadataProperty(const char *name);</code>
    /// <summary>
    /// <para>Get metadata about your app.</para>
    /// <para>This returns metadata previously set using <see cref="SetAppMetadata"/> or
    /// <see cref="SetAppMetadataProperty"/>. See <see cref="SetAppMetadataProperty"/> for the list
    /// of available properties and their meanings.</para>
    /// </summary>
    /// <param name="name">the name of the metadata property to get.</param>
    /// <returns>the current value of the metadata property, or the default if it
    /// is not set, <c>null</c> for properties with no default.</returns>
    /// <threadsafety>It is safe to call this function from any thread, although
    /// the string returned is not protected and could potentially be
    /// freed if you call <see cref="SetAppMetadataProperty"/> to set that
    /// property from another thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetAppMetadata"/>
    /// <seealso cref="SetAppMetadataProperty"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetAppMetadataProperty"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetAppMetadataProperty([MarshalAs(UnmanagedType.LPUTF8Str)] string name);
}