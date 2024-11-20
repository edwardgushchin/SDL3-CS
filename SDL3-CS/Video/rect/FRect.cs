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

using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// A rectangle, with the origin at the upper left (using floating point
    /// values).
    /// </summary>
    /// <since>This struct is available since SDL 3.0.0.</since>
    /// <seealso cref="RectEmptyFloat"/>
    /// <seealso cref="RectsEqualFloat"/>
    /// <seealso cref="RectsEqualEpsilon"/>
    /// <seealso cref="HasRectIntersectionFloat"/>
    /// <seealso cref="GetRectIntersectionFloat"/>
    /// <seealso cref="GetRectAndLineIntersectionFloat"/>
    /// <seealso cref="GetRectUnionFloat"/>
    /// <seealso cref="GetRectEnclosingPointsFloat"/>
    /// <seealso cref="PointInRectFloat"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct FRect(float x, float y, float w, float h)
    {
        public float X = x;
        public float Y = y;
        public float W = w;
        public float H = h;

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}, W: {W}, H: {H}";
        }
    }
}