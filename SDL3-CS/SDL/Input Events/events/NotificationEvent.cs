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
    /// <summary>
    /// <para>Notification dialog event structure (event.notification.*)</para>
    /// <para>An <c>action_id</c> value of 'default' for an
    /// <see cref="EventType.NotificationActionInvoked"/> event indicates that the notification
    /// was interacted with without selecting a specific action (e.g. the body of
    /// the notification was clicked on).</para>
    /// </summary>
    /// <since>This struct is available since SDL 3.6.0.</since>
    [StructLayout(LayoutKind.Sequential)]
    public struct NotificationEvent
    {
        /// <summary>
        /// <see cref="EventType.NotificationActionInvoked"/>
        /// </summary>
        public EventType Type;

        private UInt32 _reserved;

        /// <summary>
        /// In nanoseconds, populated using <see cref="GetTicksNS"/>
        /// </summary>
        public UInt64 Timestamp;

        /// <summary>
        /// The ID of the notification that generated this event.
        /// </summary>
        public UInt32 Which;

        /// <summary>
        /// The identifier string of the action invoked in the notification dialog.
        /// </summary>
        public IntPtr ActionID;
    }
}
