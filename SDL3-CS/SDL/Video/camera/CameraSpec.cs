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
    /// <para>The details of an output format for a camera device.</para>
    /// <para>Cameras often support multiple formats; each one will be encapsulated in
    /// this struct.</para>
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// <seealso cref="GetCameraSupportedFormats"/>
    /// <seealso cref="GetCameraFormat"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct CameraSpec
    {
        /// <summary>
        /// Frame format
        /// </summary>
        public PixelFormat PixelFormat;
        
        /// <summary>
        /// Frame colorspace
        /// </summary>
        public Colorspace Colorspace;
        
        /// <summary>
        /// Frame width
        /// </summary>
        public int Width;
        
        /// <summary>
        /// Frame height
        /// </summary>
        public int Height;
        
        /// <summary>
        /// Frame rate numerator ((num / denom) == FPS, (denom / num) == duration in seconds)
        /// </summary>
        public int FramerateNumerator;  // Frame rate numerator
        
        /// <summary>
        /// Frame rate denominator ((num / denom) == FPS, (denom / num) == duration in seconds)
        /// </summary>
        public int FramerateDenominator;
    }
}
