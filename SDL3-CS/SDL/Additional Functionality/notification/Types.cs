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

using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    /// <summary>Notification priority.</summary>
    /// <since>This enum is available since SDL 3.6.0.</since>
    public enum NotificationPriority
    {
        /// <summary>Low priority.</summary>
        Low = -1,
        /// <summary>Normal priority.</summary>
        Normal = 0,
        /// <summary>High/important priority.</summary>
        High = 1,
        /// <summary>Highest/critical priority. This may override any "Do Not Disturb" settings and wake the screen.</summary>
        Critical = 2
    }

    /// <summary>Types of notification action.</summary>
    /// <since>This enum is available since SDL 3.6.0.</since>
    public enum NotificationActionType
    {
        /// <summary>Adds a button to the notification that generates feedback when activated.</summary>
        Button = 1
    }

    /// <summary>Button action payload for a notification action.</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct NotificationButtonAction
    {
        /// <summary><see cref="NotificationActionType.Button"/>.</summary>
        public NotificationActionType Type;
        /// <summary>The identifier string for the button. 'default' is a reserved identifier and must not be used.</summary>
        public IntPtr ActionId;
        /// <summary>The localized label for the button associated with the action, in UTF-8 encoding.</summary>
        public IntPtr ActionLabel;
    }

    /// <summary>
    /// Notification structure describing actions that can be used to allow users to interact with notification dialogs.
    /// <para>Exactly how they are presented depends on the platform and implementation.</para>
    /// <para>User interactions with a notification are reported via events with the type <see cref="EventType.NotificationActionInvoked"/>.</para>
    /// </summary>
    /// <since>This union is available since SDL 3.6.0.</since>
    [StructLayout(LayoutKind.Explicit, Size = 128)]
    public struct NotificationAction
    {
        [FieldOffset(0)] public NotificationActionType Type;
        [FieldOffset(0)] public NotificationButtonAction Button;
    }
}
