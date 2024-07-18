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
		try
		{
			var joystickArray = new int[cout];
			Marshal.Copy(pArray, joystickArray, 0, cout);
			return Array.ConvertAll(joystickArray, item => (uint)item);
		}
		finally
		{
			Free(pArray);
		}
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
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_SetJoystickVirtualBall(IntPtr joystick, int ball, short xrel, short yrel);
	public static int SetJoystickVirtualBall(Joystick joystick, int ball, short xrel, short yrel) => 
		SDL_SetJoystickVirtualBall(joystick.Handle, ball, xrel, yrel);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_SetJoystickVirtualButton(IntPtr joystick, int button, byte value);
	public static int SetJoystickVirtualButton(Joystick joystick, int button, byte value) => 
		SDL_SetJoystickVirtualButton(joystick.Handle, button, value);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_SetJoystickVirtualHat(IntPtr joystick, int hat, byte value);
	public static int SetJoystickVirtualHat(Joystick joystick, int hat, byte value) => 
		SDL_SetJoystickVirtualHat(joystick.Handle, hat, value);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_SetJoystickVirtualTouchpad(IntPtr joystick, int touchpad, int finger, 
		Keystate state, float x, float y, float pressure);
	public static int SetJoystickVirtualTouchpad(Joystick joystick, int touchpad, int finger, 
		Keystate state, float x, float y, float pressure) => 
		SDL_SetJoystickVirtualTouchpad(joystick.Handle, touchpad, finger, state, x, y, pressure);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_SendJoystickVirtualSensorData(IntPtr joystick, SensorType type, 
		ulong sensor_timestamp, [In] float[] data, int num_values);
	public static int SendJoystickVirtualSensorData(Joystick joystick, SensorType type, 
		ulong sensor_timestamp, float[] data, int num_values) => 
		SDL_SendJoystickVirtualSensorData(joystick.Handle, type, sensor_timestamp, data, num_values);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetJoystickProperties(IntPtr joystick);
	public static uint GetJoystickProperties(Joystick joystick) => SDL_GetJoystickProperties(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoystickName(IntPtr joystick);
	public static string? GetJoystickName(Joystick joystick) => 
		Marshal.PtrToStringUTF8(SDL_GetJoystickName(joystick.Handle));
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoystickPath(IntPtr joystick);
	public static string? GetJoystickPath(Joystick joystick) => 
		Marshal.PtrToStringUTF8(SDL_GetJoystickPath(joystick.Handle));
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetJoystickPlayerIndex(IntPtr joystick);
	public static int GetJoystickPlayerIndex(Joystick joystick) => SDL_GetJoystickPlayerIndex(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_SetJoystickPlayerIndex(IntPtr joystick, int player_index);
	public static int SetJoystickPlayerIndex(Joystick joystick, int player_index) => 
		SDL_SetJoystickPlayerIndex(joystick.Handle, player_index);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial GUID SDL_GetJoystickGUID(IntPtr joystick);
	public static GUID GetJoystickGUID(Joystick joystick) => SDL_GetJoystickGUID(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickVendor(IntPtr joystick);
	public static ushort GetJoystickVendor(Joystick joystick) => SDL_GetJoystickVendor(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickProduct(IntPtr joystick);
	public static ushort GetJoystickProduct(Joystick joystick) => SDL_GetJoystickProduct(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickProductVersion(IntPtr joystick);
	public static ushort GetJoystickProductVersion(Joystick joystick) => SDL_GetJoystickProductVersion(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickFirmwareVersion(IntPtr joystick);
	public static ushort GetJoystickFirmwareVersion(Joystick joystick) => 
		SDL_GetJoystickFirmwareVersion(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoystickSerial(IntPtr joystick);
	public static string? GetJoystickSerial(Joystick joystick) => 
		Marshal.PtrToStringUTF8(SDL_GetJoystickSerial(joystick.Handle));
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial JoystickType SDL_GetJoystickType(IntPtr joystick);
	public static JoystickType GetJoystickType(Joystick joystick) => SDL_GetJoystickType(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetJoystickGUIDString(GUID guid, [MarshalAs(UnmanagedType.LPUTF8Str)]out string pszGUID, int cbGUID);
	public static int GetJoystickGUIDString(GUID guid, out string pszGUID, int cbGUID = 33) =>
		SDL_GetJoystickGUIDString(guid, out pszGUID, cbGUID);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial GUID SDL_GetJoystickGUIDFromString([MarshalAs(UnmanagedType.LPUTF8Str)] string pchGUID);
	public static GUID GetJoystickGUIDFromString(string pchGUID) => SDL_GetJoystickGUIDFromString(pchGUID);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_GetJoystickGUIDInfo(GUID guid, out ushort vendor, out ushort product, 
		out ushort version, out ushort crc16);
	public static void GetJoystickGUIDInfo(GUID guid, out ushort vendor, out ushort product,
		out ushort version, out ushort crc16) =>
		SDL_GetJoystickGUIDInfo(guid, out vendor, out product, out version, out crc16);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	private static partial bool SDL_JoystickConnected(IntPtr joystick);
	public static bool JoystickConnected(Joystick joystick) => SDL_JoystickConnected(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetJoystickID(IntPtr joystick);
	public static uint GetJoystickID(Joystick joystick) => SDL_GetJoystickID(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetNumJoystickAxes(IntPtr joystick);
	public static int GetNumJoystickAxes(Joystick joystick) => SDL_GetNumJoystickAxes(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetNumJoystickBalls(IntPtr joystick);
	public static int GetNumJoystickBalls(Joystick joystick) => SDL_GetNumJoystickBalls(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetNumJoystickHats(IntPtr joystick);
	public static int GetNumJoystickHats(Joystick joystick) => SDL_GetNumJoystickHats(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetNumJoystickButtons(IntPtr joystick);
	public static int GetNumJoystickButtons(Joystick joystick) => SDL_GetNumJoystickButtons(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_SetJoystickEventsEnabled([MarshalAs(UnmanagedType.I1)] bool enabled);
	public static void SetJoystickEventsEnabled(bool enabled) => SDL_SetJoystickEventsEnabled(enabled);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	private static partial bool SDL_JoystickEventsEnabled();
	public static bool JoystickEventsEnabled() => SDL_JoystickEventsEnabled();
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_UpdateJoysticks();
	public static void UpdateJoysticks() => SDL_UpdateJoysticks();
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial short SDL_GetJoystickAxis(IntPtr joystick, int axis);
	public static short GetJoystickAxis(Joystick joystick, int axis) => SDL_GetJoystickAxis(joystick.Handle, axis);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	private static partial bool SDL_GetJoystickAxisInitialState(IntPtr joystick, int axis, out short state);
	public static bool GetJoystickAxisInitialState(Joystick joystick, int axis, out short state) =>
		SDL_GetJoystickAxisInitialState(joystick.Handle, axis, out state);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetJoystickBall(IntPtr joystick, int ball, out int dx, out int dy);
	public static int GetJoystickBall(Joystick joystick, int ball, out int dx, out int dy) =>
		SDL_GetJoystickBall(joystick.Handle, ball, out dx, out dy);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial JoystickHat SDL_GetJoystickHat(IntPtr joystick, int hat);
	public static JoystickHat GetJoystickHat(Joystick joystick, int hat) => SDL_GetJoystickHat(joystick.Handle, hat);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial byte SDL_GetJoystickButton(IntPtr joystick, int button);
	public static byte GetJoystickButton(Joystick joystick, int button) => 
		SDL_GetJoystickButton(joystick.Handle, button);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_RumbleJoystick(IntPtr joystick, ushort low_frequency_rumble, 
		ushort high_frequency_rumble, uint duration_ms);

	public static int RumbleJoystick(Joystick joystick, ushort low_frequency_rumble, ushort high_frequency_rumble,
		uint duration_ms) =>
		SDL_RumbleJoystick(joystick.Handle, low_frequency_rumble, high_frequency_rumble, duration_ms);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_RumbleJoystickTriggers(IntPtr joystick, ushort left_rumble, ushort right_rumble, 
		uint duration_ms);
	public static int RumbleJoystickTriggers(Joystick joystick, ushort left_rumble, ushort right_rumble,
		uint duration_ms) =>
		SDL_RumbleJoystickTriggers(joystick.Handle, left_rumble, right_rumble, duration_ms);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_SetJoystickLED(IntPtr joystick, byte red, byte green, byte blue);
	public static int SetJoystickLED(Joystick joystick, byte red, byte green, byte blue) =>
		SDL_SetJoystickLED(joystick.Handle, red, green, blue);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_SendJoystickEffect(IntPtr joystick, IntPtr data, int size);
	public static int SendJoystickEffect(Joystick joystick, IntPtr data, int size) =>
		SDL_SendJoystickEffect(joystick.Handle, data, size);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_CloseJoystick(IntPtr joystick);
	public static void CloseJoystick(Joystick joystick) => SDL_CloseJoystick(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial JoystickConnectionState SDL_GetJoystickConnectionState(IntPtr joystick);
	public static JoystickConnectionState GetJoystickConnectionState(Joystick joystick) => 
		SDL_GetJoystickConnectionState(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial PowerState SDL_GetJoystickPowerInfo(IntPtr joystick, out int percent);
	public static PowerState GetJoystickPowerInfo(Joystick joystick, out int percent) =>
		SDL_GetJoystickPowerInfo(joystick.Handle, out percent);
}