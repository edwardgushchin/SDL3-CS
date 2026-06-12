#region License
/* Copyright (c) 2024-2026 Eduard Gushchin.
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
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RequestNotificationPermission(void);</code>
    /// <summary>
    /// <para>Requests permission from the system to display notifications.</para>
    /// <para>A return value of <c>true</c> only means that the system supports notifications,
    /// and that the request for permission was successfully issued. It does not
    /// reflect any user settings to allow or deny notifications.</para>
    /// </summary>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.6.0.</since>
    /// <seealso cref="ShowNotification"/>
    /// <seealso cref="ShowNotificationWithProperties"/>
    /// <seealso cref="NotificationAction"/>
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RequestNotificationPermission"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_RequestNotificationPermission();
    private delegate bool RequestNotificationPermissionNative();
    private static RequestNotificationPermissionNative RequestNotificationPermissionNativeFunction = SDL_RequestNotificationPermission;

    public static bool RequestNotificationPermission()
    {
        return RequestNotificationPermissionNativeFunction();
    }

    /// <code>extern SDL_DECLSPEC SDL_NotificationID SDLCALL SDL_ShowNotificationWithProperties(SDL_PropertiesID props);</code>
    /// <summary>
    /// <para>Show a system notification.</para>
    /// <para>System notifications are small, asynchronous popup windows that notify the
    /// user of some information. How they are displayed is system dependent.</para>
    /// <para>These are the supported properties:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.NotificationTitleString"/>: the title of the notification, in
    /// UTF-8 encoding. This property is required.</item>
    /// <item><see cref="Props.NotificationActionsPointer"/>: An array of pointers to
    /// <see cref="NotificationAction"/> structs that will add actions to the
    /// notification, usually in the form of buttons or menu items. Note that
    /// systems may have a limit on the maximum number of actions that a
    /// notification can have.</item>
    /// <item><see cref="Props.NotificationActionCountNumber"/>: the number of actions in
    /// the array of actions, if it exists.</item>
    /// <item><see cref="Props.NotificationImagePointer"/>: a pointer to an <see cref="Surface"/>
    /// containing an image that will be attached to the notification. In most
    /// cases, the image is displayed in the form of a large icon or thumbnail
    /// alongside the message body. Notifications on Apple platforms can be
    /// expanded to show a larger format image.</item>
    /// <item><see cref="Props.NotificationMessageString"/>: the message body of the
    /// notification, in UTF-8 encoding.</item>
    /// <item><see cref="Props.NotificationPriorityNumber"/>: an <see cref="NotificationPriority"/>
    /// value representing the notification priority.</item>
    /// <item><see cref="Props.NotificationReplacesNumber"/>: the SDL_NotificationID of a
    /// previously shown notification that this notification should replace.</item>
    /// <item><see cref="Props.NotificationSoundString"/>: sets a sound to play when the
    /// notification is shown. This can have the value "default", to play the
    /// system default notification sound, "silent", to play no sound, or contain
    /// the path to a file with a custom sound. The paths and formats that can be
    /// used for custom sounds are system-specific, and can have some
    /// restrictions, depending on the platform:
    /// Apple platforms require that the sound file is contained within the app
    /// bundle. Supported formats are: Linear PCM, MA4 (IMA/ADPCM), uLaw, or
    /// aLaw, in an .aiff, .wav, or .caf file.
    /// Windows can only play custom notification sounds when the app is packaged
    /// inside an MSIX installer. Playback from arbitrary file paths is not
    /// supported. Supported formats are: .aac, .flac, .m4a, .mp3, .wav, and
    /// .wma.
    /// Unix platforms can generally load sounds from any arbitrary path, as long
    /// as the read permissions are correct. Supported formats are: ogg/opus,
    /// ogg/vorbis, and wav/pcm. If this property is not set, the system default
    /// sound will be used.</item>
    /// <item><see cref="Props.NotificationTransientBoolean"/>: <c>true</c> if the notification
    /// should not persist in the system notification center after initially
    /// being shown.</item>
    /// </list>
    /// <para>Not all properties are supported by all platforms.</para>
    /// <para>Notifications are available on: - Windows 10 or higher - macOS 10.14 or
    /// higher - iOS 11 or higher - *nix platforms that support the
    /// org.freedesktop.Notifications, or org.freedesktop.portal.Notification
    /// interfaces</para>
    /// </summary>
    /// <param name="props">the properties to be used when creating this notification.</param>
    /// <returns>A non-zero SDL_NotificationID on success or 0 on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.6.0.</since>
    /// <seealso cref="ShowNotification"/>
    /// <seealso cref="NotificationAction"/>
    /// <seealso cref="NotificationPriority"/>
    /// <seealso href="https://wiki.libsdl.org/SDL3/SDL_NotificationEvent">SDL_NotificationEvent</seealso>
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ShowNotificationWithProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_ShowNotificationWithProperties(uint props);
    private delegate uint ShowNotificationWithPropertiesNative(uint props);
    private static ShowNotificationWithPropertiesNative ShowNotificationWithPropertiesNativeFunction = SDL_ShowNotificationWithProperties;

    public static uint ShowNotificationWithProperties(uint props)
    {
        return ShowNotificationWithPropertiesNativeFunction(props);
    }

    /// <code>extern SDL_DECLSPEC SDL_NotificationID SDLCALL SDL_ShowNotification(const char *title, const char *message, SDL_Surface *image, SDL_NotificationAction *actions, int num_actions);</code>
    /// <summary>
    /// Show a system notification with normal priority.
    /// </summary>
    /// <param name="title">UTF-8 title text, required.</param>
    /// <param name="message">UTF-8 message text, may be <c>null</c>.</param>
    /// <param name="image">The image associated with this notification, may be <c>null</c>.</param>
    /// <param name="actions">An array of actions to attach to the notification, may be
    /// <c>null</c>.</param>
    /// <param name="numActions">The number of actions in the actions array.</param>
    /// <returns>A non-zero SDL_NotificationID on success or 0 on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.6.0.</since>
    /// <seealso cref="ShowNotificationWithProperties"/>
    /// <seealso cref="NotificationAction"/>
    /// <seealso href="https://wiki.libsdl.org/SDL3/SDL_NotificationEvent">SDL_NotificationEvent</seealso>
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ShowNotification"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_ShowNotification(
        [MarshalAs(UnmanagedType.LPUTF8Str)] string title,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string? message,
        IntPtr image,
        IntPtr actions,
        int numActions);
    private delegate uint ShowNotificationNative(string title, string? message, IntPtr image, IntPtr actions, int numActions);
    private static ShowNotificationNative ShowNotificationNativeFunction = SDL_ShowNotification;

    public static uint ShowNotification(string title, string? message, IntPtr image, IntPtr actions, int numActions)
    {
        return ShowNotificationNativeFunction(title, message, image, actions, numActions);
    }

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RemoveNotification(SDL_NotificationID notification);</code>
    /// <summary>
    /// Remove a notification.
    /// </summary>
    /// <param name="notification">the ID of the notification to remove.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.6.0.</since>
    /// <seealso cref="ShowNotificationWithProperties"/>
    /// <seealso cref="ShowNotification"/>
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RemoveNotification"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_RemoveNotification(uint notification);
    private delegate bool RemoveNotificationNative(uint notification);
    private static RemoveNotificationNative RemoveNotificationNativeFunction = SDL_RemoveNotification;

    public static bool RemoveNotification(uint notification)
    {
        return RemoveNotificationNativeFunction(notification);
    }
}
