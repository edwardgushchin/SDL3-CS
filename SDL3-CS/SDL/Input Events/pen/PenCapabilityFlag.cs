namespace SDL3;

public static partial class SDL
{
    [Flags]
	public enum PenCapabilityFlags : uint
	{
		Down = 1u << PenFlagDownBitIndex,
		Ink = 1u << PenFlagInkBitIndex,
		Eraser = 1u << PenFlagEraserBitIndex,
		AxisPressure = 1u << (PenAxis.Pressure + PenFlagAxisBitOffset),
		AxisXTilt = 1u << (PenAxis.XTilt + PenFlagAxisBitOffset),
		AxisYTilt = 1u << (PenAxis.YTilt + PenFlagAxisBitOffset),
		AxisDistance = 1u << (PenAxis.Distance + PenFlagAxisBitOffset),
		AxisRotation = 1u << (PenAxis.Rotation + PenFlagAxisBitOffset),
		AxisSlider = 1u << (PenAxis.Slider + PenFlagAxisBitOffset),
		AxisBidirectional = AxisXTilt | AxisYTilt
	}
}