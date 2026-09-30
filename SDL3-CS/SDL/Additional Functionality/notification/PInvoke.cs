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

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RequestNotificationPermission"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_RequestNotificationPermission();
    private delegate bool RequestNotificationPermissionNative();
    private static RequestNotificationPermissionNative RequestNotificationPermissionNativeFunction = SDL_RequestNotificationPermission;

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
    public static bool RequestNotificationPermission()
    {
        return RequestNotificationPermissionNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ShowNotificationWithProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_ShowNotificationWithProperties(uint props);
    private delegate uint ShowNotificationWithPropertiesNative(uint props);
    private static ShowNotificationWithPropertiesNative ShowNotificationWithPropertiesNativeFunction = SDL_ShowNotificationWithProperties;

    /// <code>extern SDL_DECLSPEC SDL_NotificationID SDLCALL SDL_ShowNotificationWithProperties(SDL_PropertiesID props);</code>
    /// <summary>
    /// <para>Show a system notification.</para>
    /// <para>These properties are supported:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.NotificationTitleString"/>: required UTF-8 title.</item>
    /// <item><see cref="Props.NotificationActionsPointer"/>: array of
    /// <see cref="NotificationAction"/> structs for buttons or menu items.</item>
    /// <item><see cref="Props.NotificationActionCountNumber"/>: number of actions.</item>
    /// <item><see cref="Props.NotificationImagePointer"/>: optional <c>SDL_Surface</c> image.</item>
    /// <item><see cref="Props.NotificationMessageString"/>: optional UTF-8 message.</item>
    /// <item><see cref="Props.NotificationPriorityNumber"/>: a
    /// <see cref="NotificationPriority"/> value.</item>
    /// <item><see cref="Props.NotificationReplacesNumber"/>: ID of a notification to replace.</item>
    /// <item><see cref="Props.NotificationSoundString"/>: system default, silent, or a
    /// platform-supported custom sound.</item>
    /// <item><see cref="Props.NotificationTransientBoolean"/>: whether it should be transient.</item>
    /// </list>
    /// <para>Not all properties are supported by all platforms.</para>
    /// <para>Notifications are available on Windows 10 or higher, macOS 10.14 or higher,
    /// iOS 11 or higher, and Unix platforms that support system notification interfaces.</para>
    /// </summary>
    /// <param name="props">the properties to be used when creating this notification.</param>
    /// <returns>A non-zero notification ID on success or 0 on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.6.0.</since>
    /// <seealso cref="ShowNotification"/>
    /// <seealso cref="NotificationAction"/>
    /// <seealso cref="NotificationPriority"/>
    /// <seealso cref="NotificationEvent"/>
    public static uint ShowNotificationWithProperties(uint props)
    {
        return ShowNotificationWithPropertiesNativeFunction(props);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ShowNotification"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_ShowNotification(
        [MarshalAs(UnmanagedType.LPUTF8Str)] string title,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string? message,
        IntPtr image,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] NotificationAction[]? actions,
        int numActions);
    private delegate uint ShowNotificationNative(string title, string? message, IntPtr image, NotificationAction[]? actions, int numActions);
    private static ShowNotificationNative ShowNotificationNativeFunction = SDL_ShowNotification;

    /// <code>extern SDL_DECLSPEC SDL_NotificationID SDLCALL SDL_ShowNotification(const char *title, const char *message, SDL_Surface *image, SDL_NotificationAction *actions, int num_actions);</code>
    /// <summary>Show a system notification with normal priority.</summary>
    /// <param name="title">UTF-8 title text, required.</param>
    /// <param name="message">UTF-8 message text, may be <c>null</c>.</param>
    /// <param name="image">the image associated with this notification, may be <c>null</c>.</param>
    /// <param name="actions">an array of actions to attach to the notification, may be <c>null</c>.</param>
    /// <param name="numActions">the number of actions in the actions array.</param>
    /// <returns>A non-zero notification ID on success or 0 on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.6.0.</since>
    /// <seealso cref="ShowNotificationWithProperties"/>
    /// <seealso cref="NotificationAction"/>
    /// <seealso cref="NotificationEvent"/>
    public static uint ShowNotification(string title, string? message, IntPtr image, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] NotificationAction[]? actions, int numActions)
    {
        return ShowNotificationNativeFunction(title, message, image, actions, numActions);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RemoveNotification"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool SDL_RemoveNotification(uint notification);
    private delegate bool RemoveNotificationNative(uint notification);
    private static RemoveNotificationNative RemoveNotificationNativeFunction = SDL_RemoveNotification;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RemoveNotification(SDL_NotificationID notification);</code>
    /// <summary>Remove a notification.</summary>
    /// <param name="notification">the ID of the notification to remove.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.6.0.</since>
    /// <seealso cref="ShowNotificationWithProperties"/>
    /// <seealso cref="ShowNotification"/>
    public static bool RemoveNotification(uint notification)
    {
        return RemoveNotificationNativeFunction(notification);
    }
}
