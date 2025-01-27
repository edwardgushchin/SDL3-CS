#region License
/* SDL3# - C# Wrapper for SDL3
 *
 * Copyright (c) 2024-2025 Eduard Gushchin.
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
    /// Cursor types for <see cref="CreateSystemCursor"/>.
    /// </summary>
    /// <since>This enum is available since SDL 3.2.0</since>
    public enum SystemCursor
    {
        /// <summary>
        /// Default cursor. Usually an arrow.
        /// </summary>
        Default,
        
        /// <summary>
        /// Text selection. Usually an I-beam.
        /// </summary>
        Text,
        
        /// <summary>
        /// Wait. Usually an hourglass or watch or spinning ball.
        /// </summary>
        Wait,
        
        /// <summary>
        /// Crosshair.
        /// </summary>
        Crosshair,
        
        /// <summary>
        /// Program is busy but still interactive. Usually it's WAIT with an arrow.
        /// </summary>
        Progress,
        
        /// <summary>
        /// Double arrow pointing northwest and southeast.
        /// </summary>
        NWSEResize,
        
        /// <summary>
        /// Double arrow pointing northeast and southwest.
        /// </summary>
        NESWResize,
        
        /// <summary>
        /// Double arrow pointing west and east.
        /// </summary>
        EWResize,
        
        /// <summary>
        /// Double arrow pointing north and south.
        /// </summary>
        NSResize,
        
        /// <summary>
        /// Four pointed arrow pointing north, south, east, and west.
        /// </summary>
        Move,
        
        /// <summary>
        /// Not permitted. Usually a slashed circle or crossbones.
        /// </summary>
        NotAllowed,
        
        /// <summary>
        /// Pointer that indicates a link. Usually a pointing hand.
        /// </summary>
        Pointer,
        
        /// <summary>
        /// Window resize top-left. This may be a single arrow or a double arrow  like <see cref="NWSEResize"/>.
        /// </summary>
        NWResize,
        
        /// <summary>
        /// Window resize top. May be <see cref="NSResize"/>.
        /// </summary>
        NResize,
        
        /// <summary>
        /// Window resize top-right. May be <see cref="NESWResize"/>.
        /// </summary>
        NEResize,
        
        /// <summary>
        /// Window resize right. May be <see cref="EWResize"/>.
        /// </summary>
        EResize,
        
        /// <summary>
        /// Window resize bottom-right. May be <see cref="NWSEResize"/>.
        /// </summary>
        SEResize,
        
        /// <summary>
        /// Window resize bottom. May be <see cref="NSResize"/>.
        /// </summary>
        SResize,
        
        /// <summary>
        /// Window resize bottom-left. May be <see cref="NESWResize"/>.
        /// </summary>
        SWResize,
        
        /// <summary>
        /// Window resize left. May be <see cref="EWResize"/>.
        /// </summary>
        WResize,
        
        SDLNumSystemCursors
    }
}