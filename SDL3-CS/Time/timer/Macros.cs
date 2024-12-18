﻿#region License
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

namespace SDL3;

public static partial class SDL
{
    [Macro]
    public static ulong SecondsToNs(ulong seconds) => seconds * NsPerSecond;
    
    [Macro]
    public static ulong NsToSeconds(ulong nanoseconds) => nanoseconds / NsPerSecond;
    
    [Macro]
    public static ulong MsToNs(ulong milliseconds) => milliseconds * NsPerMs;
    
    [Macro]
    public static ulong NsToMs(ulong nanoseconds) => nanoseconds / NsPerMs;
    
    [Macro]
    public static ulong UsToNs(ulong microseconds) => microseconds * NsPerUs;
    
    [Macro]
    public static ulong NsToUs(ulong nanoseconds) => nanoseconds / NsPerUs;
}