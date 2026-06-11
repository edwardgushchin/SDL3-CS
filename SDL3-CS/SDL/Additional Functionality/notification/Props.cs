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

namespace SDL3;

public static partial class SDL
{
    public static partial class Props
    {
        /// <summary>
        /// <para>The path to an image to be used as the header icon for system notifications
        /// on some platforms.</para>
        /// <para>This is required on: - Windows - *nix when not running in a container, and
        /// no .desktop entry is available</para>
        /// <para>Image types supported depend on the platform, but .png generally offers the
        /// best compatability.</para>
        /// <para>On *nix platforms, this can also be the name of a system icon, as specified
        /// by the Icon Naming Specification.</para>
        /// <para>Can be set before calling <see cref="ShowNotification"/> or
        /// SDL_ShowSimpleNotification() for the first time.</para>
        /// </summary>
        /// <since>This macro is available since SDL 3.6.0.</since>
        public const string GlobalNotificationHeaderIconString = "SDL.notification.header_icon";

        /// <summary>
        /// An array of pointers to <see cref="NotificationAction"/> structs that will add actions to the notification.
        /// </summary>
        public const string NotificationActionsPointer = "SDL.notification.actions";

        /// <summary>
        /// The number of actions in the array of actions, if it exists.
        /// </summary>
        public const string NotificationActionCountNumber = "SDL.notification.action_count";

        /// <summary>
        /// A pointer to an <see cref="Surface"/> containing an image that will be attached to the notification.
        /// </summary>
        public const string NotificationImagePointer = "SDL.notification.image";

        /// <summary>
        /// The message body of the notification, in UTF-8 encoding.
        /// </summary>
        public const string NotificationMessageString = "SDL.notification.message";

        /// <summary>
        /// A <see cref="NotificationPriority"/> value representing the notification priority.
        /// </summary>
        public const string NotificationPriorityNumber = "SDL.notification.priority";

        /// <summary>
        /// The SDL_NotificationID of a previously shown notification that this notification should replace.
        /// </summary>
        public const string NotificationReplacesNumber = "SDL.notification.replaces";

        /// <summary>
        /// Sets a sound to play when the notification is shown.
        /// </summary>
        public const string NotificationSoundString = "SDL.notification.sound";

        /// <summary>
        /// <c>true</c> if the notification should not persist in the system notification center after initially being shown.
        /// </summary>
        public const string NotificationTransientBoolean = "SDL.notification.transient";

        /// <summary>
        /// The title of the notification, in UTF-8 encoding.
        /// </summary>
        public const string NotificationTitleString = "SDL.notification.title";
    }
}
