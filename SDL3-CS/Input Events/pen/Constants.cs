#region License
/* SDL3# - C# Wrapper for SDL3
 *
 * Copyright (c) 2024 Eduard Gushchin.
 *
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from
 * the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you, must not
 * claim that you, wrote the original software. If you, use this software in a
 * product, an acknowledgment in the product documentation would be
 * appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not be
 * misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source distribution.
 *
 * Eduard "edwardgushchin" Gushchin <eduardgushchin@yandex.ru>
 *
 */
#endregion

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// Bit for storing that pen is touching the surface
    /// </summary>
    internal const int PenFlagDownBitIndex = 13;
    
    /// <summary>
    /// Bit for storing has-non-eraser-capability status
    /// </summary>
    internal const int PenFlagInkBitIndex = 14;
    
    /// <summary>
    /// Bit for storing is-eraser or has-eraser-capability property
    /// </summary>
    internal const int PenFlagEraserBitIndex = 15;
    
    /// <summary>
    /// Bit for storing has-axis-0 property
    /// </summary>
    internal const int PenFlagAxisBitOffset = 16;
    
    /// <summary>
    /// Marks unknown information when querying the pen
    /// </summary>
    public const int PenInfoUnknown = -1;
    
    /// <summary>
    /// Reserved invalid SDL_PenID is valid
    /// </summary>
    public const int PenInvalid = 0;
}