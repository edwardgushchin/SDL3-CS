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
 * 1. The origin of this software must not be misrepresented; you must not
 * claim that you wrote the original software. If you use this software in a
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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
	
	public struct Joystick(IntPtr handle)
	{
		public IntPtr Handle { get; } = handle;

		public override bool Equals(object? obj)
		{
			return obj is Cursor other && Handle == other.Handle;
		}

		public override int GetHashCode()
		{
			return Handle.GetHashCode();
		}
        
		public static bool operator ==(Joystick left, Joystick right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Joystick left, Joystick right)
		{
			return !(left == right);
		}
	}

	
	public enum JoystickType
	{
		Unknown,
		Gamepad,
		Wheel,
		ArcadeStick,
		FlightStick,
		DancePad,
		Guitar,
		DrumKit,
		ArcadePad,
		Throttle
	}
	
	
	public enum JoystickConnectionState
	{
		Invalid = -1,
		Unknown,
		Wired,
		Wireless
	}
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_LockJoysticks();
	public static void LockJoysticks() => SDL_LockJoysticks();
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_UnlockJoysticks();
	public static void UnlockJoysticks() => SDL_UnlockJoysticks();
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	private static partial bool SDL_HasJoystick();
	public static bool HasJoystick() => SDL_HasJoystick();
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoysticks(out int count);
	public static uint[] GetJoysticks(out int cout)
	{
		var pArray = SDL_GetJoysticks(out cout);
		var joystickArray = new int[cout];
		Marshal.Copy(pArray, joystickArray, 0, cout);
		Free(pArray);
		return Array.ConvertAll(joystickArray, item => (uint)item);
	}
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoystickInstanceName(uint instanceId);
	public static string? GetJoystickInstanceName(uint instanceId) =>
		Marshal.PtrToStringUTF8(SDL_GetJoystickInstanceName(instanceId));
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoystickInstancePath(uint instanceId);
	public static string? GetJoystickInstancePath(uint instanceId) =>
		Marshal.PtrToStringUTF8(SDL_GetJoystickInstancePath(instanceId));
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetJoystickInstancePlayerIndex(uint instanceId);
	public static int GetJoystickInstancePlayerIndex(uint instanceId) => SDL_GetJoystickInstancePlayerIndex(instanceId);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial GUID SDL_GetJoystickInstanceGUID(uint instanceId);
	public static GUID GetJoystickInstanceGUID(uint instanceId) => SDL_GetJoystickInstanceGUID(instanceId);
	
	

	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickInstanceVendor(uint instanceId);
	public static ushort GetJoystickInstanceVendor(uint instanceId) => SDL_GetJoystickInstanceVendor(instanceId);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickInstanceProduct(uint instanceId);
	public static ushort GetJoystickInstanceProduct(uint instanceId) => SDL_GetJoystickInstanceProduct(instanceId);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickInstanceProductVersion(uint instanceId);
	public static ushort GetJoystickInstanceProductVersion(uint instanceId) => 
		SDL_GetJoystickInstanceProductVersion(instanceId);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial JoystickType SDL_GetJoystickInstanceType(uint instanceId);
	public static JoystickType GetJoystickInstanceType(uint instanceId) => 
		SDL_GetJoystickInstanceType(instanceId);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_OpenJoystick(uint instanceId);
	public static Joystick OpenJoystick(uint instanceId) => new(SDL_OpenJoystick(instanceId));
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoystickFromInstanceID(uint instanceId);
	public static Joystick GetJoystickFromInstanceID(uint instanceId) => 
		new(SDL_GetJoystickFromInstanceID(instanceId));

	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoystickFromPlayerIndex(int playerIndex);
	public static Joystick GetJoystickFromPlayerIndex(int playerIndex) =>
		new(SDL_GetJoystickFromPlayerIndex(playerIndex));
	
	
	[StructLayout(LayoutKind.Sequential)]
	public struct VirtualJoystickTouchpadDesc
	{
		public UInt16 NFingers;    /**< the number of simultaneous fingers on this touchpad */
		private unsafe fixed UInt16 Padding[3];
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct VirtualJoystickSensorDesc
	{
		public SensorType Type;
		public float Rate;
	}
	
	
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct VirtualJoystickDesc
	{
		public JoystickType Type;
		private UInt16 padding;
		public UInt16 VendorId;
		public UInt16 ProductId;
		public UInt16 NAxes;
		public UInt16 NButtons;
		public UInt16 NBalls;
		public UInt16 NHats;
		public UInt16 NTouchpads;
		public UInt16 NSensors;
		private unsafe fixed UInt16 padding2[2];
		public UInt32 ButtonMask;
		public UInt32 AxisMask;
		public string? Name;
		public VirtualJoystickTouchpadDesc[]? Touchpads;
		public VirtualJoystickSensorDesc[]? Sensors;
		public IntPtr? UserData;
		public VirtualJoystickUpdateCallback Update;
		public VirtualJoysticSetPlayerIndexCallback? SetPlayerIndex;
		public VirtualJoysticRumbleCallback? Rumble;
		public VirtualJoysticRumbleTriggersCallback? RumbleTriggers;
		public VirtualJoysticSetLEDCallback? SetLED;
		public VirtualJoysticSendEffectCallback? SendEffect;
		public VirtualJoysticSetSensorsEnabledCallback? SetSensorsEnabled;
	}
	
	
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void VirtualJoystickUpdateCallback(IntPtr userData);
	
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void VirtualJoysticSetPlayerIndexCallback(IntPtr userData, int player_index);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate int VirtualJoysticRumbleCallback(IntPtr userData, ushort low_frequency_rumble, ushort high_frequency_rumble);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate int VirtualJoysticRumbleTriggersCallback(IntPtr userData, ushort left_rumble, ushort right_rumble);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate int VirtualJoysticSetLEDCallback(IntPtr userData, byte red, byte green, byte blue);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate int VirtualJoysticSendEffectCallback(IntPtr userData, IntPtr data, int size);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate int VirtualJoysticSetSensorsEnabledCallback(IntPtr userData, bool enabled);


	[DllImport(SDLLibrary, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
	private static extern uint SDL_AttachVirtualJoystick([In] in VirtualJoystickDesc desc);
	public static uint AttachVirtualJoystick(VirtualJoystickDesc desc) => SDL_AttachVirtualJoystick(desc);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_DetachVirtualJoystick(uint instance_id);
	public static int DetachVirtualJoystick(uint instance_id) => SDL_DetachVirtualJoystick(instance_id);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	private static partial bool SDL_IsJoystickVirtual(uint instance_id);
	public static bool IsJoystickVirtual(uint instance_id) => SDL_IsJoystickVirtual(instance_id);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_SetJoystickVirtualAxis(IntPtr joystick, int axis, short value);
	public static int SetJoystickVirtualAxis(Joystick joystick, int axis, short value) => 
		SDL_SetJoystickVirtualAxis(joystick.Handle, axis, value);

	
	public const short JoystickAxisMax = short.MaxValue;
	
	
	public const short JoystickAxisMin = short.MinValue;
	
	
	public const float IPhoneMaxGForce = 5.0f;
	
	
	public enum JoystickHat : byte
	{
		Centered = 0x00,
		Up = 0x01,
		Right = 0x02,
		Down = 0x04,
		Left = 0x08,
		RightUp = Right | Up,
		RightDown = Right | Down,
		LeftUp = Left | Up, 
		LeftDown = Left | Down
	}
}