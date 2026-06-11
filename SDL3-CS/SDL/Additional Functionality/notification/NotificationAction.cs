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
    /// <code>typedef union SDL_NotificationAction { SDL_NotificationActionType type; struct { SDL_NotificationActionType type; const char *action_id; const char *action_label; } button; Uint8 padding[128]; } SDL_NotificationAction;</code>
    /// <summary>
    /// <para>Notification structure describing actions that can be used to allow users
    /// to interact with notification dialogs.</para>
    /// <para>Exactly How they are presented depends on the platform and implementation.</para>
    /// <para>User interactions with a notification are reported via events with the type
    /// <see cref="EventType.NotificationActionInvoked"/>.</para>
    /// <para>Action types: - button: A button with a localized text label, which
    /// generates feedback when activated.</para>
    /// </summary>
    /// <since>This union is available since SDL 3.6.0.</since>
    /// <seealso href="https://wiki.libsdl.org/SDL3/SDL_NotificationEvent">SDL_NotificationEvent</seealso>
    /// <seealso cref="NotificationActionType"/>
    [StructLayout(LayoutKind.Explicit, Size = 128)]
    public unsafe struct NotificationAction
    {
        /// <summary>
        /// The notification action type.
        /// </summary>
        [FieldOffset(0)] public NotificationActionType Type;

        /// <summary>
        /// Button action data.
        /// </summary>
        [FieldOffset(0)] public ButtonAction Button;

        [FieldOffset(0)] private fixed byte _padding[128];

        /// <summary>
        /// Button action data.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct ButtonAction
        {
            /// <summary>
            /// <see cref="NotificationActionType.Button"/>.
            /// </summary>
            public NotificationActionType Type;

            /// <summary>
            /// The identifier string for the button. 'default' is a reserved identifier and must not be used.
            /// </summary>
            public IntPtr ActionID;

            /// <summary>
            /// The localized label for the button associated with the action, in UTF-8 encoding.
            /// </summary>
            public IntPtr ActionLabel;
        }
    }
}
