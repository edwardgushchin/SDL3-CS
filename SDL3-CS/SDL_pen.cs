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
    public enum PenAxis
    {
        Pressure = 0,
        XTilt,  
        YTilt,
        Distance,
        Rotation,
        Slider,
        NumAxes,
        Last = NumAxes - 1
    }
    
    
    public const int PenFlagDownBitIndex = 13;
    public const int PenFlagInkBitIndex = 14;
    public const int PenFlagEraserBitIndex = 15;
    public const int PenFlagAxisBitOffset = 16; 
    
    
    public static ulong PenCapability(int capbit)
    {
        return 1ul << capbit;
    }

    
    public static ulong PenAxisCapability(int axis)
    {
        return PenCapability(axis + PenFlagAxisBitOffset);
    }

    
    public enum PenTips
    {
        Ink = PenFlagInkBitIndex,
        Eraser = PenFlagEraserBitIndex,
    }

    
    public static readonly ulong PenDownMask = PenCapability(PenFlagDownBitIndex);
    public static readonly ulong PenInkMask = PenCapability(PenFlagInkBitIndex);
    public static readonly ulong PenEraserMask = PenCapability(PenFlagEraserBitIndex);
    public static readonly ulong PenAxisPressureMask = PenAxisCapability((int)PenAxis.Pressure);
    public static readonly ulong PenAxisXTiltMask = PenAxisCapability((int)PenAxis.XTilt);
    public static readonly ulong PenAxisYTiltMask = PenAxisCapability((int)PenAxis.YTilt);
    public static readonly ulong PenAxisDistanceMask = PenAxisCapability((int)PenAxis.Distance);
    public static readonly ulong PenAxisRotationMask = PenAxisCapability((int)PenAxis.Rotation);
    public static readonly ulong PenAxisSliderMask = PenAxisCapability((int)PenAxis.Slider);
    public static readonly ulong PenAxisBidirectionalMasks = PenAxisXTiltMask | PenAxisYTiltMask;
}