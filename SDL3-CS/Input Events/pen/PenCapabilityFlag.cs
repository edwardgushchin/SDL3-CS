namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// Pen capabilities reported by <see cref="GetPenCapabilities"/>.
    /// </summary>
    /// <since>This datatype is available since SDL 3.0.0.</since>
	[Flags]
	public enum PenCapabilityFlags : uint
	{
		/// <summary>
		/// Pen tip is currently touching the drawing surface.
		/// </summary>
		Down = 1u << PenFlagDownBitIndex,
		
		/// <summary>
		/// Pen has a regular drawing tip (<see cref="GetPenCapabilities"/>).
		/// For events (<see cref="PenButtonEvent"/>, <see cref="PenMotionEvent"/>, <see cref="GetPenStatus"/>)
		/// this flag is mutually exclusive with <see cref="Eraser"/> .
		/// </summary>
		Ink = 1u << PenFlagInkBitIndex,
		
		/// <summary>
		/// Pen has an eraser tip (<see cref="GetPenCapabilities"/>) or is being used as eraser
		/// (<see cref="PenButtonEvent"/> , <see cref="PenMotionEvent"/> , <see cref="GetPenStatus"/>)
		/// </summary>
		Eraser = 1u << PenFlagEraserBitIndex,
		
		/// <summary>
		/// Pen provides pressure information in axis <see cref="PenAxis.Pressure"/>
		/// </summary>
		AxisPressure = 1u << (PenAxis.Pressure + PenFlagAxisBitOffset),
		
		/// <summary>
		/// Pen provides horizontal tilt information in axis <see cref="PenAxis.XTilt"/>
		/// </summary>
		AxisXTilt = 1u << (PenAxis.XTilt + PenFlagAxisBitOffset),
		
		/// <summary>
		/// Pen provides vertical tilt information in axis <see cref="PenAxis.YTilt"/>
		/// </summary>
		AxisYTilt = 1u << (PenAxis.YTilt + PenFlagAxisBitOffset),
		
		/// <summary>
		/// Pen provides distance to drawing tablet in <see cref="PenAxis.Distance"/>
		/// </summary>
		AxisDistance = 1u << (PenAxis.Distance + PenFlagAxisBitOffset),
		
		/// <summary>
		/// Pen provides barrel rotation information in axis <see cref="PenAxis.Rotation"/>
		/// </summary>
		AxisRotation = 1u << (PenAxis.Rotation + PenFlagAxisBitOffset),
		
		/// <summary>
		/// Pen provides slider / finger wheel or similar in axis <see cref="PenAxis.Slider"/>
		/// </summary>
		AxisSlider = 1u << (PenAxis.Slider + PenFlagAxisBitOffset),
		AxisBidirectional = AxisXTilt | AxisYTilt
	}
}