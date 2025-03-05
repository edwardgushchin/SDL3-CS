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
    /// <para>A callback used to transform mouse motion delta from raw values.</para>
    /// <para>This is called during SDL's handling of platform mouse events to scale the
    /// values of the resulting motion delta.</para>
    /// </summary>
    /// <param name="userdata">what was passed as <c>userdata</c> to
    /// <see cref="SetRelativeMouseTransform"/>.</param>
    /// <param name="timestamp">the associated time at which this mouse motion event was
    /// received.</param>
    /// <param name="window">the associated window to which this mouse motion event was
    /// addressed.</param>
    /// <param name="mouseId">the associated mouse from which this mouse motion event was
    /// emitted.</param>
    /// <param name="x">pointer to a variable that will be treated as the resulting x-axis
    /// motion.</param>
    /// <param name="y">pointer to a variable that will be treated as the resulting y-axis
    /// motion.</param>
    /// <threadsafety>This callback is called by SDL's internal mouse input
    /// processing procedure, which may be a thread separate from the
    /// main event loop that is run at realtime priority. Stalling
    /// this thread with too much work in the callback can therefore
    /// potentially freeze the entire system. Care should be taken
    /// with proper synchronization practices when adding other side
    /// effects beyond mutation of the x and y values.</threadsafety>
    /// <since>This datatype is available since SDL 3.2.6.</since>
    /// <seealso cref="SetRelativeMouseTransform"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MouseMotionTransformCallback(IntPtr userdata, ulong timestamp, IntPtr window, uint mouseId, ref float x, ref float y);
    
}