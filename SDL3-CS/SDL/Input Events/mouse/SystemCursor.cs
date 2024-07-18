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
    public enum SystemCursor
    {
        Default,      /**< Default cursor. Usually an arrow. */
        Text,         /**< Text selection. Usually an I-beam. */
        Wait,         /**< Wait. Usually an hourglass or watch or spinning ball. */
        Crosshair,    /**< Crosshair. */
        Progress,     /**< Program is busy but still interactive. Usually it's WAIT with an arrow. */
        NWSEResize,  /**< Double arrow pointing northwest and southeast. */
        NESWResize,  /**< Double arrow pointing northeast and southwest. */
        EWResize,    /**< Double arrow pointing west and east. */
        NSResize,    /**< Double arrow pointing north and south. */
        Move,         /**< Four pointed arrow pointing north, south, east, and west. */
        NotAllowed,  /**< Not permitted. Usually a slashed circle or crossbones. */
        Pointer,      /**< Pointer that indicates a link. Usually a pointing hand. */
        NWResize,    /**< Window resize top-left. This may be a single arrow or a double arrow like NWSE_RESIZE. */
        NResize,     /**< Window resize top. May be NS_RESIZE. */
        NEResize,    /**< Window resize top-right. May be NESW_RESIZE. */
        EResize,     /**< Window resize right. May be EW_RESIZE. */
        SEResize,    /**< Window resize bottom-right. May be NWSE_RESIZE. */
        SResize,     /**< Window resize bottom. May be NS_RESIZE. */
        SWResize,    /**< Window resize bottom-left. May be NESW_RESIZE. */
        WResize,     /**< Window resize left. May be EW_RESIZE. */
        NumSystemCursors
    }
}