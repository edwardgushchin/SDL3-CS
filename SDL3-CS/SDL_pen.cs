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
        /// <summary>
        /// Pen pressure.  Unidirectional: 0..1.0
        /// </summary>
        Pressure = 0,
        
        /// <summary>
        /// Pen horizontal tilt angle.  Bidirectional: -90.0..90.0 (left-to-right).
        /// The physical max/min tilt may be smaller than -90.0 / 90.0, cf. SDL_PenCapabilityInfo
        /// </summary>
        XTilt,  
        
        /// <summary>
        /// Pen vertical tilt angle.  Bidirectional: -90.0..90.0 (top-to-down).
        /// The physical max/min tilt may be smaller than -90.0 / 90.0, cf. <see cref="PenCapabilityInfo"/> 
        /// </summary>
        YTilt,
        
        /// <summary>
        /// Pen distance to drawing surface.  Unidirectional: 0.0..1.0
        /// </summary>
        Distance,
        
        /// <summary>
        /// Pen barrel rotation.  Bidirectional: -180..179.9 (clockwise, 0 is facing up, -180.0 is facing down).
        /// </summary>
        Rotation,
        
        /// <summary>
        /// Pen finger wheel or slider (e.g., Airbrush Pen).  Unidirectional: 0..1.0
        /// </summary>
        Slider,
        
        /// <summary>
        /// Last valid axis index
        /// </summary>
        NumAxes,
        
        /// <summary>
        /// Last axis index plus 1
        /// </summary>
        Last = NumAxes - 1
    }
    
    /* Pen flags.  These share a bitmask space with SDL_BUTTON_LEFT and friends. */
    
    /// <summary>
    /// Bit for storing that pen is touching the surface 
    /// </summary>
    public const int PenFlagDownBitIndex = 13;
    
    /// <summary>
    /// Bit for storing has-non-eraser-capability status
    /// </summary>
    public const int PenFlagInkBitIndex = 14;
    
    /// <summary>
    /// Bit for storing is-eraser or has-eraser-capability property
    /// </summary>
    public const int PenFlagEraserBitIndex = 15;
    
    /// <summary>
    /// Bit for storing has-axis-0 property
    /// </summary>
    public const int PenFlagAxisBitOffset = 16; 
    
    
    public static ulong PenCapability(int capbit)
    {
        return 1ul << capbit;
    }

    public static ulong PenAxisCapability(int axis)
    {
        return PenCapability(axis + PenFlagAxisBitOffset);
    }


    /// <summary>
    /// Pen tips 
    /// </summary>    
    public enum PenTips
    {
        /// <summary>
        /// Regular pen tip (for drawing) touched the surface
        /// </summary>
        PenTipInk = PenFlagInkBitIndex,
        
        /// <summary>
        /// Eraser pen tip touched the surface
        /// </summary>
        PenTipEraser = PenFlagEraserBitIndex,
    }


    /// <summary>
    /// Pen tip is currently touching the drawing surface.
    /// </summary>
    public static readonly ulong PenDownMask = PenCapability(PenFlagDownBitIndex);
    
    /// <summary>
    /// Pen has a regular drawing tip. For events, this flag is mutually exclusive with <see cref="PenEraserMask"/>
    /// </summary>
    public static readonly ulong PenInkMask = PenCapability(PenFlagInkBitIndex);
    
    /// <summary>
    /// Pen has an eraser tip or is being used as an eraser.
    /// </summary>
    public static readonly ulong PenEraserMask = PenCapability(PenFlagEraserBitIndex);
    
    /// <summary>
    /// Pen provides pressure information in axis <see cref="PenAxis.Pressure"/>
    /// </summary>
    public static readonly ulong PenAxisPressureMask = PenAxisCapability((int)PenAxis.Pressure);
    
    /// <summary>
    /// Pen provides horizontal tilt information in axis <see cref="PenAxis.XTilt"/>
    /// </summary>
    public static readonly ulong PenAxisXTiltMask = PenAxisCapability((int)PenAxis.XTilt);
    
    /// <summary>
    /// Pen provides vertical tilt information in axis <see cref="PenAxis.YTilt"/>
    /// </summary>
    public static readonly ulong PenAxisYTiltMask = PenAxisCapability((int)PenAxis.YTilt);
    
    /// <summary>
    /// Pen provides distance to drawing tablet in <see cref="PenAxis.Distance"/>
    /// </summary>
    public static readonly ulong PenAxisDistanceMask = PenAxisCapability((int)PenAxis.Distance);
    
    /// <summary>
    /// Pen provides barrel rotation information in axis <see cref="PenAxis.Rotation"/>
    /// </summary>
    public static readonly ulong PenAxisRotationMask = PenAxisCapability((int)PenAxis.Rotation);
    
    /// <summary>
    /// Pen provides slider / finger wheel or similar in axis <see cref="PenAxis.Slider"/>
    /// </summary>
    public static readonly ulong PenAxisSliderMask = PenAxisCapability((int)PenAxis.Slider);
    
    /// <summary>
    /// Combined mask for both horizontal and vertical tilt.
    /// </summary>
    public static readonly ulong PenAxisBidirectionalMasks = PenAxisXTiltMask | PenAxisYTiltMask;
}